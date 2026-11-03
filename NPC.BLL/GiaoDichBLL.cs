using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NPC.DAL;
using NPC.DTO;

namespace NPC.BLL
{
    /// <summary>
    /// Tầng nghiệp vụ (BLL) xử lý các giao dịch tài chính, bọc SqlTransaction bảo đảm chuẩn ACID:
    ///   - TraMay()         : Thanh toán tiền giờ, đồ ăn, đóng phiên và trả máy.
    ///   - NapTien()        : Nạp tiền tăng số dư tài khoản hội viên.
    ///   - GuiYeuCauNapTien : Client gửi yêu cầu nạp tiền, chờ Server duyệt.
    ///   - DuyetYeuCauNapTien / TuChoiYeuCauNapTien : Server xử lý yêu cầu.
    /// </summary>
    public static class GiaoDichBLL
    {
        public static KetQuaTraMayDTO TraMay(long maPhienChoi, int maNhanVien, string hinhThucThanhToan)
        {
            using SqlConnection conn = GiaoDichDAL.MoKetNoi();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                PhienChoiDTO? phien = GiaoDichDAL.LockVaLayPhienChoi(conn, tran, maPhienChoi);

                if (phien == null)
                    throw new InvalidOperationException($"Phiên chơi #{maPhienChoi} không tồn tại.");

                if (phien.TrangThaiPhien != "Đang chơi")
                    throw new InvalidOperationException(
                        $"Phiên #{maPhienChoi} đang ở trạng thái '{phien.TrangThaiPhien}', không thể trả máy.");

                DateTime thoiGianKetThuc = DateTime.Now;
                int tongPhutChoi = (int)Math.Ceiling((thoiGianKetThuc - phien.ThoiGianBatDau).TotalMinutes);
                decimal tienGio = Math.Round((decimal)(tongPhutChoi / 60.0) * phien.GiaGioApDung, 2);
                decimal tienDoAn = GiaoDichDAL.LayTienDoAnPhien(conn, tran, maPhienChoi);
                decimal tongTien = tienGio + tienDoAn;

                if (phien.MaHoiVien.HasValue)
                {
                    decimal? soDu = GiaoDichDAL.LockVaLaySoDuHoiVien(conn, tran, phien.MaHoiVien.Value);

                    if (soDu == null)
                        throw new InvalidOperationException("Không tìm thấy thông tin hội viên.");

                    if (soDu.Value < tongTien)
                        throw new InvalidOperationException(
                            $"Số dư hội viên ({soDu.Value:N0} VND) không đủ để thanh toán ({tongTien:N0} VND).");

                    int rows = GiaoDichDAL.TruSoDuHoiVien(conn, tran, phien.MaHoiVien.Value, tongTien, tongPhutChoi);

                    if (rows == 0)
                        throw new InvalidOperationException("Số dư hội viên vừa thay đổi trong lúc xử lý. Vui lòng thử lại.");
                }

                int rowsPhien = GiaoDichDAL.DongPhienChoi(conn, tran, maPhienChoi, thoiGianKetThuc, tongPhutChoi, tienGio);

                if (rowsPhien == 0)
                    throw new InvalidOperationException("Phiên chơi đã bị thay đổi trạng thái bởi tiến trình khác. Vui lòng thử lại.");

                GiaoDichDAL.CapNhatTrangThaiMay(conn, tran, phien.MaMay);

                string ghiChu = $"Thanh toán phiên #{maPhienChoi}: Tiền giờ {tienGio:N0} VND + Đồ ăn {tienDoAn:N0} VND = {tongTien:N0} VND.";
                GiaoDichDAL.GhiGiaoDich(conn, tran, phien.MaHoiVien, maNhanVien, tongTien, hinhThucThanhToan, ghiChu);

                tran.Commit();

                Logger.LogBusiness("System", "Trả máy",
                    $"Phiên #{maPhienChoi} | Máy #{phien.MaMay} | {tongPhutChoi} phút | " +
                    $"Giờ {tienGio:N0} + Đồ ăn {tienDoAn:N0} = Tổng {tongTien:N0} VND | {hinhThucThanhToan}");

                return new KetQuaTraMayDTO
                {
                    TongPhutChoi = tongPhutChoi,
                    TienGio = tienGio,
                    TienDoAn = tienDoAn,
                    TongTienThanhToan = tongTien,
                    ThoiGianKetThuc = thoiGianKetThuc
                };
            }
            catch (InvalidOperationException ex)
            {
                SafeRollback(tran);
                Logger.LogWarning($"[TraMay] Lỗi nghiệp vụ phiên #{maPhienChoi}: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                SafeRollback(tran);
                Logger.LogError($"[TraMay] Lỗi kỹ thuật phiên #{maPhienChoi}:", ex);
                throw new Exception("Lỗi hệ thống khi trả máy. Vui lòng thử lại hoặc liên hệ kỹ thuật.", ex);
            }
        }

        public static KetQuaNapTienDTO NapTien(NapTienDTO dto)
        {
            if (dto.SoTienNap <= 0)
                throw new ArgumentException($"Số tiền nạp phải lớn hơn 0. Giá trị nhận được: {dto.SoTienNap:N0} VND");

            using SqlConnection conn = GiaoDichDAL.MoKetNoi();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                var (hoTen, soDuTruoc, trangThai) = GiaoDichDAL.LockVaLayHoiVien(conn, tran, dto.MaHoiVien);

                if (trangThai == "Bị khóa")
                    throw new InvalidOperationException($"Tài khoản hội viên '{hoTen}' đang bị khóa. Không thể nạp tiền.");

                GiaoDichDAL.CongSoDuHoiVien(conn, tran, dto.MaHoiVien, dto.SoTienNap);

                decimal soDuMoi = GiaoDichDAL.LaySoDuHoiVien(conn, tran, dto.MaHoiVien);

                string ghiChu = string.IsNullOrWhiteSpace(dto.GhiChu)
                    ? $"Nạp tiền cho '{hoTen}': {soDuTruoc:N0} → {soDuMoi:N0} VND (+{dto.SoTienNap:N0} VND)."
                    : dto.GhiChu;

                GiaoDichDAL.GhiGiaoDich(conn, tran, dto.MaHoiVien, dto.MaNhanVien, dto.SoTienNap, dto.HinhThucThanhToan, ghiChu);

                tran.Commit();

                Logger.LogBusiness("System", "Nạp tiền",
                    $"Hội viên #{dto.MaHoiVien} '{hoTen}' | +{dto.SoTienNap:N0} VND | " +
                    $"Số dư: {soDuTruoc:N0} → {soDuMoi:N0} VND | {dto.HinhThucThanhToan}");

                return new KetQuaNapTienDTO
                {
                    HoTen = hoTen,
                    SoDuTruoc = soDuTruoc,
                    SoDuMoi = soDuMoi
                };
            }
            catch (InvalidOperationException ex)
            {
                SafeRollback(tran);
                Logger.LogWarning($"[NapTien] Lỗi nghiệp vụ hội viên #{dto.MaHoiVien}: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                SafeRollback(tran);
                Logger.LogError($"[NapTien] Lỗi kỹ thuật hội viên #{dto.MaHoiVien}:", ex);
                throw new Exception("Lỗi hệ thống khi nạp tiền. Vui lòng thử lại.", ex);
            }
        }

        // ─── Luồng yêu cầu nạp tiền từ Client (cần Server duyệt) ─────────────────

        /// <summary>
        /// Client gọi hàm này khi bấm "Xác nhận nạp" trên màn hình Client.
        /// Ghi yêu cầu vào DB và trả về MaYeuCau để Client theo dõi.
        /// </summary>
        public static int GuiYeuCauNapTien(int maHoiVien, int maMay, string tenMay,
            decimal soTienNap, string hinhThuc)
        {
            if (soTienNap <= 0)
                throw new ArgumentException("Số tiền nạp phải lớn hơn 0.");
            return GiaoDichDAL.GuiYeuCauNapTien(maHoiVien, maMay, tenMay, soTienNap, hinhThuc);
        }

        /// <summary>
        /// Client poll trạng thái yêu cầu: "ChoDuyet" | "DaDuyet" | "TuChoi" | ""
        /// </summary>
        public static string KiemTraTrangThaiYeuCau(int maYeuCau)
            => GiaoDichDAL.LayTrangThaiYeuCau(maYeuCau);

        /// <summary>
        /// Server lấy danh sách yêu cầu nạp tiền đang chờ xử lý.
        /// </summary>
        public static List<YeuCauNapTienDTO> LayYeuCauChoDuyet()
            => GiaoDichDAL.LayYeuCauChoDuyet();

        /// <summary>
        /// Server đồng ý → nạp tiền thật + cập nhật trạng thái "DaDuyet".
        /// </summary>
        public static KetQuaNapTienDTO DuyetYeuCauNapTien(YeuCauNapTienDTO yeuCau, int maNhanVien)
        {
            using SqlConnection conn = GiaoDichDAL.MoKetNoi();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                var (hoTen, soDuTruoc, trangThai) = GiaoDichDAL.LockVaLayHoiVien(conn, tran, yeuCau.MaHoiVien);

                if (trangThai == "Bị khóa")
                    throw new InvalidOperationException($"Tài khoản '{hoTen}' đang bị khóa. Không thể nạp tiền.");

                GiaoDichDAL.CongSoDuHoiVien(conn, tran, yeuCau.MaHoiVien, yeuCau.SoTienNap);
                decimal soDuMoi = GiaoDichDAL.LaySoDuHoiVien(conn, tran, yeuCau.MaHoiVien);

                string ghiChu = $"Duyệt nạp tiền tại máy {yeuCau.TenMay} ({yeuCau.HinhThucThanhToan}): " +
                                $"+{yeuCau.SoTienNap:N0} VND | {soDuTruoc:N0} → {soDuMoi:N0} VND.";
                GiaoDichDAL.GhiGiaoDich(conn, tran, yeuCau.MaHoiVien, maNhanVien,
                    yeuCau.SoTienNap, yeuCau.HinhThucThanhToan, ghiChu);

                // Đánh dấu yêu cầu là đã duyệt
                GiaoDichDAL.CapNhatTrangThaiYeuCau(conn, tran, yeuCau.MaYeuCau, "DaDuyet");

                tran.Commit();

                Logger.LogBusiness("Server", "Duyệt nạp tiền",
                    $"Yêu cầu #{yeuCau.MaYeuCau} | HV #{yeuCau.MaHoiVien} '{hoTen}' | " +
                    $"+{yeuCau.SoTienNap:N0} VND | {soDuTruoc:N0} → {soDuMoi:N0} | Máy {yeuCau.TenMay}");

                return new KetQuaNapTienDTO { HoTen = hoTen, SoDuTruoc = soDuTruoc, SoDuMoi = soDuMoi };
            }
            catch
            {
                SafeRollback(tran);
                throw;
            }
        }

        /// <summary>
        /// Server từ chối → chỉ cập nhật trạng thái "TuChoi", không nạp tiền.
        /// </summary>
        public static void TuChoiYeuCauNapTien(int maYeuCau)
        {
            using SqlConnection conn = GiaoDichDAL.MoKetNoi();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                GiaoDichDAL.CapNhatTrangThaiYeuCau(conn, tran, maYeuCau, "TuChoi");
                tran.Commit();
                Logger.LogBusiness("Server", "Từ chối nạp tiền", $"Yêu cầu #{maYeuCau} bị từ chối.");
            }
            catch
            {
                SafeRollback(tran);
                throw;
            }
        }

        private static void SafeRollback(SqlTransaction tran)
        {
            try { tran.Rollback(); }
            catch (Exception ex) { Logger.LogError("[SafeRollback]", ex); }
        }
    }
}

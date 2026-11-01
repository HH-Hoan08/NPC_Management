using System;
using System.Data;
using System.Data.SqlClient;
using NPC.DAL;
using NPC.DTO;

namespace NPC.BLL
{
    /// <summary>
    /// Tầng nghiệp vụ (BLL) xử lý các giao dịch tài chính, bọc SqlTransaction bảo đảm chuẩn ACID:
    ///   - TraMay() : Thanh toán tiền giờ, đồ ăn, đóng phiên và trả máy.
    ///   - NapTien() : Nạp tiền tăng số dư tài khoản hội viên.
    /// </summary>
    public static class GiaoDichBLL
    {
        /// <summary>
        /// Thực hiện quy trình thanh toán và trả máy bọc trong 1 SqlTransaction:
        /// 1. Khóa và lấy thông tin phiên chơi.
        /// 2. Tính toán tiền giờ thực tế và tổng tiền đồ ăn.
        /// 3. Khấu trừ số dư nếu là hội viên (kiểm tra số dư đủ).
        /// 4. Đóng phiên chơi, cập nhật trạng thái máy về 'Sẵn sàng'.
        /// 5. Ghi lịch sử giao dịch vào bảng GiaoDich và Commit.
        /// </summary>
        /// <example>
        /// Cách gọi từ tầng GUI (Form):
        /// <code>
        /// try
        /// {
        ///     long maPhienChoi = 105;
        ///     int maNhanVien = 1;
        ///     string hinhThuc = "Tiền mặt";
        ///     
        ///     KetQuaTraMayDTO kq = GiaoDichBLL.TraMay(maPhienChoi, maNhanVien, hinhThuc);
        ///     MessageBox.Show($"Trả máy thành công! Tổng thu: {kq.TongTienThanhToan:N0} VND\n(Tiền giờ: {kq.TienGio:N0}đ, Đồ ăn: {kq.TienDoAn:N0}đ)", 
        ///                     "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        /// }
        /// catch (Exception ex)
        /// {
        ///     MessageBox.Show(ex.Message, "Lỗi trả máy", MessageBoxButtons.OK, MessageBoxIcon.Error);
        /// }
        /// </code>
        /// </example>
        /// <param name="maPhienChoi">Mã phiên chơi cần trả máy và thanh toán.</param>
        /// <param name="maNhanVien">Mã nhân viên thu ngân thực hiện giao dịch.</param>
        /// <param name="hinhThucThanhToan">Phương thức thanh toán (ví dụ: "Tiền mặt", "Chuyển khoản VietQR").</param>
        /// <returns>Đối tượng KetQuaTraMayDTO chứa chi tiết thời gian và các khoản tiền thanh toán.</returns>
        /// <exception cref="InvalidOperationException">Ném ngoại lệ khi có lỗi vi phạm điều kiện nghiệp vụ.</exception>
        /// <exception cref="Exception">Ném ngoại lệ chung khi có lỗi kỹ thuật hoặc lỗi hệ thống CSDL.</exception>
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

        /// <summary>
        /// Thực hiện quy trình nạp tiền vào tài khoản hội viên bọc trong 1 SqlTransaction:
        /// 1. Kiểm tra tính hợp lệ của số tiền nạp (> 0).
        /// 2. Khóa và kiểm tra thông tin, trạng thái hội viên (không bị khóa).
        /// 3. Cộng số dư tài khoản hội viên.
        /// 4. Ghi lịch sử giao dịch vào bảng GiaoDich và Commit.
        /// </summary>
        /// <example>
        /// Cách gọi từ tầng GUI (Form):
        /// <code>
        /// try
        /// {
        ///     var dto = new NapTienDTO
        ///     {
        ///         MaHoiVien = 12,
        ///         MaNhanVien = 1,
        ///         SoTienNap = 50000m,
        ///         HinhThucThanhToan = "Tiền mặt",
        ///         GhiChu = "Nạp tiền chơi game"
        ///     };
        ///     
        ///     KetQuaNapTienDTO kq = GiaoDichBLL.NapTien(dto);
        ///     MessageBox.Show($"Nạp tiền thành công cho hội viên {kq.HoTen}!\nSố dư cũ: {kq.SoDuTruoc:N0}đ\nSố dư mới: {kq.SoDuMoi:N0}đ",
        ///                     "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        /// }
        /// catch (Exception ex)
        /// {
        ///     MessageBox.Show(ex.Message, "Lỗi nạp tiền", MessageBoxButtons.OK, MessageBoxIcon.Error);
        /// }
        /// </code>
        /// </example>
        /// <param name="dto">Đối tượng NapTienDTO mang thông tin nạp tiền từ giao diện.</param>
        /// <returns>Đối tượng KetQuaNapTienDTO chứa họ tên, số dư trước và sau khi nạp.</returns>
        /// <exception cref="ArgumentException">Ném ngoại lệ khi số tiền nạp không hợp lệ (nhỏ hơn hoặc bằng 0).</exception>
        /// <exception cref="InvalidOperationException">Ném ngoại lệ khi tài khoản hội viên không tồn tại hoặc đang bị khóa.</exception>
        /// <exception cref="Exception">Ném ngoại lệ chung khi có lỗi kỹ thuật hoặc lỗi hệ thống CSDL.</exception>
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

        private static void SafeRollback(SqlTransaction tran)
        {
            try { tran.Rollback(); }
            catch (Exception ex) { Logger.LogError("[SafeRollback]", ex); }
        }
    }
}

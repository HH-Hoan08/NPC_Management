using System;
using System.Collections.Generic;
using NPC.DAL;
using NPC.DTO;

namespace NPC.BLL
{
    public static class MayTramBLL
    {
        public static List<KhuVucPhongDTO> LayDanhSachKhuVuc()
        {
            try
            {
                return KhuVucPhongDAL.LayDanhSachKhuVuc();
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi tải danh sách khu vực phòng máy:", ex);
                throw new Exception("Không thể tải danh sách khu vực phòng. Vui lòng thử lại sau.", ex);
            }
        }

        public static List<MayTramDTO> LayDanhSachMayTram(int? maPhong = null, string? trangThai = null)
        {
            try
            {
                return MayTramDAL.GetDanhSachMayTram(maPhong, trangThai);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi lấy danh sách máy trạm (KhuVuc: {maPhong}, TrangThai: {trangThai}):", ex);
                throw new Exception("Không thể tải danh sách máy trạm từ cơ sở dữ liệu.", ex);
            }
        }

        public static long MoMayVangLai(int maMay, string tenMay, string nguoiThaoTac = "Thu ngân")
        {
            try
            {
                long maPhien = MayTramDAL.MoPhienChoi(maMay, null);
                if (maPhien <= 0)
                {
                    Logger.LogWarning($"Mở máy vãng lai thất bại cho {tenMay} (Mã máy #{maMay}).");
                    throw new InvalidOperationException($"Không thể mở phiên chơi cho {tenMay}. Vui lòng thử lại.");
                }

                Logger.LogBusiness(nguoiThaoTac, "Mở máy", $"Mở máy {tenMay} (Mã #{maMay}) cho khách vãng lai. Mã phiên: #{maPhien}");
                return maPhien;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi mở máy vãng lai {tenMay} (Mã #{maMay}):", ex);
                throw;
            }
        }

        public static long MoMayHoiVien(int maMay, string tenMay, int maHoiVien, string tenHoiVien, string nguoiThaoTac = "Thu ngân")
        {
            try
            {
                long maPhien = MayTramDAL.MoPhienChoi(maMay, maHoiVien);
                if (maPhien <= 0)
                {
                    Logger.LogWarning($"Mở máy hội viên thất bại cho {tenMay} (Hội viên #{maHoiVien}).");
                    throw new InvalidOperationException($"Không thể mở phiên chơi cho hội viên {tenHoiVien} trên {tenMay}.");
                }

                Logger.LogBusiness(nguoiThaoTac, "Mở máy", $"Mở máy {tenMay} (Mã #{maMay}) cho hội viên '{tenHoiVien}' (#{maHoiVien}). Mã phiên: #{maPhien}");
                return maPhien;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi mở máy cho hội viên {tenHoiVien} trên {tenMay}:", ex);
                throw;
            }
        }

        public static string ChuyenDoiKhoaMay(int maMay, string tenMay, string trangThaiHienTai, string nguoiThaoTac = "Thu ngân")
        {
            try
            {
                string trangThaiMoi = (trangThaiHienTai == "Đang khóa") ? "Đang dùng" : "Đang khóa";
                int rows = MayTramDAL.CapNhatTrangThaiMay(maMay, trangThaiMoi);

                if (rows <= 0)
                {
                    throw new InvalidOperationException($"Cập nhật trạng thái khóa/mở khóa cho {tenMay} thất bại.");
                }

                string hanhDong = (trangThaiMoi == "Đang khóa") ? "Khóa máy" : "Mở khóa máy";
                Logger.LogBusiness(nguoiThaoTac, hanhDong, $"Thay đổi trạng thái {tenMay} (Mã #{maMay}) từ '{trangThaiHienTai}' sang '{trangThaiMoi}'.");
                return trangThaiMoi;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi khóa/mở khóa máy {tenMay} (Mã #{maMay}):", ex);
                throw;
            }
        }

        public static string ChuyenDoiBaoTri(int maMay, string tenMay, string trangThaiHienTai, string nguoiThaoTac = "Thu ngân")
        {
            try
            {
                string trangThaiMoi = (trangThaiHienTai == "Bảo trì") ? "Sẵn sàng" : "Bảo trì";
                int rows = MayTramDAL.CapNhatTrangThaiMay(maMay, trangThaiMoi);

                if (rows <= 0)
                {
                    throw new InvalidOperationException($"Cập nhật bảo trì cho {tenMay} thất bại.");
                }

                string hanhDong = (trangThaiMoi == "Bảo trì") ? "Bật bảo trì" : "Tắt bảo trì";
                Logger.LogBusiness(nguoiThaoTac, hanhDong, $"Cập nhật trạng thái {tenMay} (Mã #{maMay}) từ '{trangThaiHienTai}' sang '{trangThaiMoi}'.");
                return trangThaiMoi;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi cập nhật trạng thái bảo trì cho {tenMay} (Mã #{maMay}):", ex);
                throw;
            }
        }

        public static KetQuaTraMayDTO ThanhToanVaTraMay(long maPhienChoi, string tenMay, int maNhanVien, string hinhThucThanhToan = "Tiền mặt")
        {
            try
            {
                KetQuaTraMayDTO ketQua = GiaoDichBLL.TraMay(maPhienChoi, maNhanVien, hinhThucThanhToan);
                return ketQua;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi thanh toán phiên #{maPhienChoi} của {tenMay}:", ex);
                throw;
            }
        }
    }
}


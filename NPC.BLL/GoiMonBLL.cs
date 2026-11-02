using System;
using System.Collections.Generic;
using System.Linq;
using NPC.DAL;
using NPC.DTO;

namespace NPC.BLL
{
    /// <summary>
    /// Tầng BLL xử lý toàn bộ nghiệp vụ gọi món: kiểm tra số lượng, kiểm tra tồn kho,
    /// tính toán tiền giỏ hàng, đặt món và ghi nhật ký hệ thống (log).
    /// </summary>
    public static class GoiMonBLL
    {
        public const int SoLuongToiDaMoiMon = 99;

        /// <summary>
        /// Nghiệp vụ lấy danh mục thực đơn, tùy chọn chèn thêm mục "Tất cả"
        /// </summary>
        public static List<DanhMucDTO> LayDanhSachDanhMuc(bool themTatCa = true)
        {
            try
            {
                var ds = GoiMonDAL.LayDanhSachDanhMuc();
                if (themTatCa)
                {
                    ds.Insert(0, new DanhMucDTO
                    {
                        MaDanhMuc = 0,
                        TenDanhMuc = "-- Tất cả --"
                    });
                }
                return ds;
            }
            catch (Exception ex)
            {
                Logger.LogError("[GoiMonBLL] Lỗi khi tải danh mục món ăn:", ex);
                throw;
            }
        }

        /// <summary>
        /// Nghiệp vụ lấy danh sách thực đơn theo danh mục và từ khóa tìm kiếm
        /// </summary>
        public static List<MonAnDTO> LayThucDon(int maDanhMuc, string? tuKhoa)
        {
            try
            {
                string kw = tuKhoa?.Trim() ?? string.Empty;
                return GoiMonDAL.LayThucDon(maDanhMuc, kw);
            }
            catch (Exception ex)
            {
                Logger.LogError($"[GoiMonBLL] Lỗi khi tải thực đơn (MaDanhMuc={maDanhMuc}, TuKhoa='{tuKhoa}'):", ex);
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của số lượng món người dùng muốn thêm vào giỏ hàng
        /// </summary>
        /// <param name="soLuongNhap">Số lượng nhập vào ô số lượng</param>
        /// <param name="daCoTrongGio">Số lượng món này hiện đã có trong giỏ hàng</param>
        /// <param name="tonKho">Số lượng tồn kho hiện tại của món</param>
        /// <param name="thongBaoLoi">Chuỗi thông báo lỗi trả về cho ErrorProvider</param>
        /// <returns>True nếu hợp lệ, False nếu có lỗi</returns>
        public static bool KiemTraSoLuongMon(int soLuongNhap, int daCoTrongGio, int tonKho, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;

            if (soLuongNhap <= 0)
            {
                thongBaoLoi = "Số lượng phải lớn hơn 0.";
                return false;
            }

            if (soLuongNhap > SoLuongToiDaMoiMon)
            {
                thongBaoLoi = $"Mỗi lần gọi tối đa {SoLuongToiDaMoiMon} phần.";
                return false;
            }

            int tongYeuCau = soLuongNhap + daCoTrongGio;
            if (tongYeuCau > tonKho)
            {
                int conLai = tonKho - daCoTrongGio;
                if (conLai <= 0)
                {
                    thongBaoLoi = "Món này đã hết hàng (hoặc đã thêm hết số lượng còn lại vào giỏ).";
                }
                else
                {
                    thongBaoLoi = $"Kho chỉ còn {conLai} phần khả dụng.";
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tính tổng tiền các món trong giỏ hàng
        /// </summary>
        public static decimal TinhTongTien(IEnumerable<ChiTietDonHangDTO> gioHang)
        {
            if (gioHang == null) return 0m;
            return gioHang.Sum(x => x.ThanhTien);
        }

        /// <summary>
        /// Kiểm tra giỏ hàng trước khi đặt món
        /// </summary>
        public static bool KiemTraGioHang(List<ChiTietDonHangDTO> gioHang, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            if (gioHang == null || gioHang.Count == 0)
            {
                thongBaoLoi = "Giỏ hàng hiện đang trống. Vui lòng chọn món.";
                return false;
            }

            foreach (var item in gioHang)
            {
                if (item.SoLuong <= 0)
                {
                    thongBaoLoi = $"Số lượng món \"{item.TenMon}\" không hợp lệ.";
                    return false;
                }

                if (item.DonGia < 0)
                {
                    thongBaoLoi = $"Đơn giá món \"{item.TenMon}\" không hợp lệ.";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Xử lý nghiệp vụ đặt món từ client: kiểm tra giỏ, kiểm tra tồn kho thời gian thực,
        /// gọi DAL tạo đơn trong transaction và ghi nhật ký hoạt động (log).
        /// </summary>
        public static DatMonResultDTO DatMon(long maPhienChoi, int maMay, string tenMay, string? tenDangNhap, List<ChiTietDonHangDTO> gioHang)
        {
            // 1. Kiểm tra giỏ hàng
            if (!KiemTraGioHang(gioHang, out string loiGioHang))
            {
                return new DatMonResultDTO
                {
                    ThanhCong = false,
                    ThongBao = loiGioHang
                };
            }

            if (maMay <= 0)
            {
                return new DatMonResultDTO
                {
                    ThanhCong = false,
                    ThongBao = "Thông tin máy trạm không hợp lệ."
                };
            }

            try
            {
                // 2. Kiểm tra tồn kho thời gian thực trước khi lưu
                foreach (var item in gioHang)
                {
                    MonAnDTO? monHienTai = GoiMonDAL.LayMonAnTheoMa(item.MaMon);
                    if (monHienTai == null || !monHienTai.ConPhucVu)
                    {
                        Logger.LogWarning($"[GoiMonBLL] Món '{item.TenMon}' (Mã: {item.MaMon}) không còn phục vụ khi máy {tenMay} đặt món.");
                        return new DatMonResultDTO
                        {
                            ThanhCong = false,
                            ThongBao = $"Món \"{item.TenMon}\" hiện đã ngưng phục vụ. Vui lòng chọn món khác."
                        };
                    }

                    if (monHienTai.SoLuongTon < item.SoLuong)
                    {
                        Logger.LogWarning($"[GoiMonBLL] Món '{item.TenMon}' tồn kho ({monHienTai.SoLuongTon}) không đủ cho yêu cầu ({item.SoLuong}).");
                        return new DatMonResultDTO
                        {
                            ThanhCong = false,
                            ThongBao = $"Món \"{item.TenMon}\" chỉ còn tồn {monHienTai.SoLuongTon} phần trong kho. Vui lòng giảm số lượng."
                        };
                    }
                }

                // 3. Tính tổng tiền
                decimal tongTien = TinhTongTien(gioHang);

                // 4. Lưu đơn hàng qua DAL
                long maDonHang = GoiMonDAL.TaoDonHang(maPhienChoi, maMay, gioHang, tongTien);

                // 5. Ghi log nghiệp vụ hệ thống
                string nguoiGoi = !string.IsNullOrWhiteSpace(tenDangNhap) ? tenDangNhap : (!string.IsNullOrWhiteSpace(tenMay) ? tenMay : "Khách vãng lai");
                string dsMon = string.Join(", ", gioHang.Select(x => $"{x.TenMon} (x{x.SoLuong})"));
                string chiTietLog = $"Đặt đơn #{maDonHang} tại {tenMay} (phiên #{maPhienChoi}). Tổng tiền: {tongTien:N0} đ. Chi tiết món: {dsMon}";

                Logger.LogBusiness(nguoiGoi, "Gọi món", chiTietLog);

                return new DatMonResultDTO
                {
                    ThanhCong = true,
                    MaDonHang = maDonHang,
                    TongTien = tongTien,
                    ThongBao = $"Đã gửi đơn #{maDonHang}. Nhân viên sẽ sớm mang món đến cho bạn!"
                };
            }
            catch (Exception ex)
            {
                Logger.LogError($"[GoiMonBLL] Lỗi khi xử lý đặt món cho máy {tenMay} (phiên #{maPhienChoi}):", ex);
                return new DatMonResultDTO
                {
                    ThanhCong = false,
                    ThongBao = "Không đặt được món: " + ex.Message
                };
            }
        }
    }
}


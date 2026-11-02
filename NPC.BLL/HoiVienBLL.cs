using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using NPC.DAL;
using NPC.DTO;

namespace NPC.BLL
{
    public class HoiVienBLL
    {
        private readonly HoiVienDAL _hoiVienDAL = new HoiVienDAL();

        /// <summary>
        /// Lấy DataTable danh sách hội viên (dùng SqlDataAdapter)
        /// </summary>
        public DataTable LayDataTable(string tuKhoa = "", string trangThai = "")
        {
            return _hoiVienDAL.GetDataToTable(tuKhoa, trangThai);
        }

        /// <summary>
        /// Lấy danh sách hội viên dạng List DTO
        /// </summary>
        public List<HoiVienDTO> LayDanhSach(string tuKhoa = "", string trangThai = "")
        {
            return _hoiVienDAL.GetDanhSachHoiVien(tuKhoa, trangThai);
        }

        public bool ThemHoiVien(HoiVienDTO hv, out string thongBao)
        {
            thongBao = string.Empty;
            if (string.IsNullOrWhiteSpace(hv.TenDangNhap))
            {
                thongBao = "Tên đăng nhập không được để trống";
                return false;
            }
            if (string.IsNullOrWhiteSpace(hv.HoTen))
            {
                thongBao = "Họ tên không được để trống";
                return false;
            }
            if (string.IsNullOrWhiteSpace(hv.MatKhau) || hv.MatKhau.Length < 6)
            {
                thongBao = "Mật khẩu không được để trống và phải có ít nhất 6 ký tự";
                return false;
            }
            if (!Regex.IsMatch(hv.TenDangNhap, @"^[a-zA-Z0-9_]{3,30}$"))
            {
                thongBao = "Tên đăng nhập chỉ được gồm chữ cái, số, dấu gạch dưới và dài từ 3 đến 30 ký tự";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(hv.DienThoai) && !Regex.IsMatch(hv.DienThoai.Trim(), @"^0[0-9]{9}$"))
            {
                thongBao = "Số điện thoại không hợp lệ (phải bắt đầu bằng số 0 và có đúng 10 chữ số)";
                return false;
            }
            if (_hoiVienDAL.KiemTraTrungTenDangNhap(hv.TenDangNhap))
            {
                thongBao = $"Tên đăng nhập '{hv.TenDangNhap}' đã tồn tại! Vui lòng chọn tên khác.";
                return false;
            }

            if (hv.NgayTao.Year < 1753)
            {
                hv.NgayTao = DateTime.Now;
            }

            // Hash mật khẩu an toàn bằng BCrypt
            hv.MatKhau = BCrypt.Net.BCrypt.HashPassword(hv.MatKhau);

            return _hoiVienDAL.ThemHoiVien(hv, out thongBao);
        }

        public bool CapNhatHoiVien(HoiVienDTO hv, string matKhauMoi, out string thongBao)
        {
            thongBao = string.Empty;
            if (hv.MaHoiVien <= 0)
            {
                thongBao = "Vui lòng chọn hội viên cần cập nhật";
                return false;
            }
            if (string.IsNullOrWhiteSpace(hv.HoTen))
            {
                thongBao = "Họ tên hội viên không được để trống";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(hv.DienThoai) && !Regex.IsMatch(hv.DienThoai.Trim(), @"^0[0-9]{9}$"))
            {
                thongBao = "Số điện thoại không hợp lệ (phải bắt đầu bằng số 0 và đủ 10 số)";
                return false;
            }
            bool doiMatKhau = !string.IsNullOrWhiteSpace(matKhauMoi);
            if (doiMatKhau)
            {
                if (matKhauMoi.Length < 6)
                {
                    thongBao = "Mật khẩu mới phải có tối thiểu 6 ký tự!";
                    return false;
                }
                hv.MatKhau = BCrypt.Net.BCrypt.HashPassword(matKhauMoi);
            }
            return _hoiVienDAL.CapNhatHoiVien(hv, doiMatKhau, out thongBao);
        }

        public bool XoaHoiVien(int maHoiVien, out string thongBao)
        {
            if (maHoiVien <= 0)
            {
                thongBao = "Vui lòng chọn hội viên cần xóa";
                return false;
            }
            return _hoiVienDAL.XoaHoiVien(maHoiVien, out thongBao);
        }

        // nạp tiền vào tài khoản của hội viên dùng SqlTransaction bọc ACID qua GiaoDichBLL
        public bool NapTienHoiVien(int maHoiVien, decimal soTienNap, int maNhanVien, string hinhThuc, out string thongBao)
        {
            thongBao = string.Empty;
            if (soTienNap <= 0 || soTienNap % 1000 != 0)
            {
                thongBao = "Số tiền nạp phải lớn hơn 0 và là bội số của 1.000đ";
                return false;
            }
            try
            {
                NapTienDTO napTien = new NapTienDTO
                {
                    MaHoiVien = maHoiVien,
                    SoTienNap = soTienNap,
                    MaNhanVien = maNhanVien,
                    HinhThucThanhToan = string.IsNullOrEmpty(hinhThuc) ? "Tiền mặt" : hinhThuc,
                    GhiChu = $"Nạp tiền vào tài khoản hội viên #{maHoiVien} qua thu ngân #{maNhanVien} nạp {soTienNap:N0} VND"
                };
                KetQuaNapTienDTO ketqua = GiaoDichBLL.NapTien(napTien); 
                thongBao = $"Nạp tiền thành công cho hội viên {ketqua.HoTen}!\nSố dư trước: {ketqua.SoDuTruoc:N0} VNĐ\nSố dư mới: {ketqua.SoDuMoi:N0} VNĐ";
                return true;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi khi nạp tiền: " + ex.Message;
                return false;
            }
        }
    }
}

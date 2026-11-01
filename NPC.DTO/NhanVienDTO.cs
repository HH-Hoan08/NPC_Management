using System;

namespace NPC.DTO
{
    public class NhanVienDTO
    {
        public int MaNhanVien { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public bool TrangThai { get; set; }
    }

    public class DangNhapResultDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public NhanVienDTO? NhanVien { get; set; }
    }
}


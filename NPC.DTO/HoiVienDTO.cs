using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPC.DTO
{
    public class HoiVienDTO
    {
        public int MaHoiVien { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string DienThoai { get; set; } = string.Empty;
        public decimal SoDu { get; set; }
        public int TongThoiGianChoi { get; set; } = 0;
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public string TrangThai { get; set; } = "Hoạt động"; 
    }
}

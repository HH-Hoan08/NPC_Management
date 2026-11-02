using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPC.DTO
{
    public class MonAnDTO
    {
        public int MaMon { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public string TenMon { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public decimal GiaNhap { get; set; }
        public int SoLuongTon { get; set; }
        public string DonViTinh { get; set; } = "Phần";
        public string HinhAnh { get; set; } = string.Empty;
        public bool ConPhucVu { get; set; } = true;

        // Thuộc tính hiển thị phụ trợ trên DataGridView
        public string TrangThaiText => ConPhucVu ? "Còn phục vụ" : "Ngừng phục vụ";
        public string CanhBaoTonKho => SoLuongTon < 5 ? "Sắp hết hàng" : "Đủ hàng";
    }
}

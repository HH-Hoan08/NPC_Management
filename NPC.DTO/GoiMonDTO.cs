using System;

namespace NPC.DTO
{
    /// <summary>
    /// DTO thông tin danh mục món ăn
    /// </summary>
    public class DanhMucDTO
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO thông tin món ăn trên thực đơn
    /// </summary>
    public class MonAnDTO
    {
        public int MaMon { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public string TenMon { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }
        public string? HinhAnh { get; set; }
        public bool ConPhucVu { get; set; }
        public decimal GiaNhap { get; set; }
        public string DonViTinh { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO đại diện cho một món trong giỏ hàng hoặc một chi tiết đơn hàng
    /// </summary>
    public class ChiTietDonHangDTO
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }

    /// <summary>
    /// DTO kết quả xử lý đặt món
    /// </summary>
    public class DatMonResultDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public long MaDonHang { get; set; }
        public decimal TongTien { get; set; }
    }
}


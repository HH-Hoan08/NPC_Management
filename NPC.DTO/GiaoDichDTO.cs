using System;

namespace NPC.DTO
{
    public class PhienChoiDTO
    {
        public long MaPhienChoi { get; set; }
        public int MaMay { get; set; }
        public int? MaHoiVien { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public decimal GiaGioApDung { get; set; }
        public string TrangThaiPhien { get; set; } = string.Empty;
    }

    public class KetQuaTraMayDTO
    {
        public int TongPhutChoi { get; set; }
        public decimal TienGio { get; set; }
        public decimal TienDoAn { get; set; }
        public decimal TongTienThanhToan { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }

    public class NapTienDTO
    {
        public int MaHoiVien { get; set; }
        public int MaNhanVien { get; set; }
        public decimal SoTienNap { get; set; }
        public string HinhThucThanhToan { get; set; } = string.Empty;
        public string GhiChu { get; set; } = string.Empty;
    }

    public class KetQuaNapTienDTO
    {
        public string HoTen { get; set; } = string.Empty;
        public decimal SoDuTruoc { get; set; }
        public decimal SoDuMoi { get; set; }
    }
}


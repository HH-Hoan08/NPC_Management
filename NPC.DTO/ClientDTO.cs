using System;

namespace NPC.DTO
{
    public class ClientThongTinMayDTO
    {
        public int MaMay { get; set; }
        public string TenMay { get; set; } = string.Empty;
        public decimal DonGiaGio { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }

    public class ClientDongBoDTO
    {
        public long MaPhienChoi { get; set; }
        public int MaHoiVien { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public decimal SoDu { get; set; }
        public int GiayDaChoi { get; set; }
        /// <summary>Tiền giờ của phiên đã được trừ vào số dư trong CSDL (PhienChoi.TienGio).</summary>
        public decimal TienGioDaTru { get; set; }
        public string TrangThaiMay { get; set; } = string.Empty;
    }

    public class ClientDangNhapResultDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public long MaPhienChoi { get; set; }
        public int MaHoiVien { get; set; }
        public decimal SoDu { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
    }
}


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPC.DTO
{
    public class MayTramDTO
    {
        // thông tin của máy trạm
        public int MaMay { get; set; }
        public string TenMay { get; set; }
        public string DiaChiIP { get; set; }
        public string DiaChiMAC { get; set; }
        public int MaPhong { get; set; }
        public string TenPhong { get; set; }
        public decimal DonGiaGio { get; set; }

        // trạng thái: 'Sẵn sàng', 'Đang dùng', 'Bảo trì', 'Tắt máy', 'Đang khóa'
        public string TrangThai { get; set; }
        // thông tin phiên chơi hiện tại (nếu máy đang dùng)
        public long? MaPhienChoi { get; set; }
        public int? MaHoiVien { get; set; }
        public string TenHoiVien { get; set; }      // tên đăng nhập hoặc Họ tên hội viên
        public DateTime? ThoiGianBatDau { get; set; }
        public decimal SoDuHoiVien { get; set; }     // số dư tài khoản hội viên
        public decimal TienGioHienTai { get; set; }   // tiền giờ đã tích lũy
    }
}

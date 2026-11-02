using System;
using NPC.DAL;
using NPC.DTO;

namespace NPC.BLL
{
    public static class ClientBLL
    {
        public static ClientThongTinMayDTO? KhoiTaoMayTram(string tenMay)
        {
            try
            {
                ClientThongTinMayDTO? may = ClientDAL.LayThongTinMay(tenMay);
                if (may == null)
                {
                    Logger.LogError($"[ClientBLL] Không tìm thấy máy '{tenMay}' trong CSDL.");
                    return null;
                }

                // Đảm bảo trạng thái ban đầu là chuẩn tiếng Việt nếu máy đang tắt
                ClientDAL.CapNhatTrangThaiMay(may.MaMay, "Sẵn sàng", "Tắt máy");
                Logger.LogBusiness(tenMay, "Bật máy", $"Máy trạm {tenMay} đã khởi động và chuyển sang Sẵn sàng.");
                return may;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ClientBLL] Lỗi khi khởi tạo máy trạm '{tenMay}':", ex);
                throw;
            }
        }

        public static void TatMay(int maMay, string tenMay, long maPhienChoi)
        {
            try
            {
                if (maPhienChoi == 0)
                {
                    ClientDAL.CapNhatTrangThaiMay(maMay, "Offline", "Online");
                    Logger.LogBusiness(tenMay, "Tắt máy", $"Máy trạm {tenMay} đã chuyển sang Offline.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ClientBLL] Lỗi khi tắt máy trạm '{tenMay}':", ex);
            }
        }

        public static ClientDangNhapResultDTO DangNhapHoiVien(int maMay, string tenMay, decimal donGiaGio, string tenDangNhap, string matKhau, decimal soDuToiThieu = 5000m)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                return new ClientDangNhapResultDTO
                {
                    ThanhCong = false,
                    ThongBao = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu."
                };
            }

            try
            {
                string ttMay = ClientDAL.LayTrangThaiMay(maMay);
                if (ttMay == "Locked" || ttMay == "Đang khóa")
                {
                    return new ClientDangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Máy đang bị quầy khóa. Vui lòng liên hệ thu ngân."
                    };
                }

                HoiVienDTO? hv = ClientDAL.LayThongTinHoiVien(tenDangNhap.Trim());
                if (hv == null)
                {
                    Logger.LogWarning($"[ClientBLL] Đăng nhập thất bại: Tài khoản '{tenDangNhap}' không tồn tại.");
                    return new ClientDangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Sai tên đăng nhập hoặc mật khẩu."
                    };
                }

                bool matKhauHopLe = false;
                string mk = matKhau.Trim();
                if (hv.MatKhau.StartsWith("$2a$") || hv.MatKhau.StartsWith("$2b$") || hv.MatKhau.StartsWith("$2y$"))
                {
                    try
                    {
                        matKhauHopLe = BCrypt.Net.BCrypt.Verify(mk, hv.MatKhau);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"[ClientBLL] Lỗi xác thực mật khẩu BCrypt cho '{tenDangNhap}':", ex);
                    }
                }
                else
                {
                    matKhauHopLe = (hv.MatKhau == mk);
                }

                if (!matKhauHopLe)
                {
                    Logger.LogWarning($"[ClientBLL] Đăng nhập thất bại: Sai mật khẩu cho '{tenDangNhap}'.");
                    return new ClientDangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Sai tên đăng nhập hoặc mật khẩu."
                    };
                }

                if (hv.TrangThai == "Bị khóa")
                {
                    return new ClientDangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Tài khoản đang bị khóa. Vui lòng liên hệ quầy."
                    };
                }

                if (hv.TrangThai == "Đang chơi")
                {
                    return new ClientDangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Tài khoản đang được sử dụng ở máy khác."
                    };
                }

                if (hv.SoDu < soDuToiThieu)
                {
                    return new ClientDangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = $"Số dư không đủ (tối thiểu {soDuToiThieu:N0} đ). Vui lòng nạp thêm tại quầy."
                    };
                }

                long maPhien = ClientDAL.MoPhienChoi(maMay, hv.MaHoiVien);
                if (maPhien <= 0)
                {
                    return new ClientDangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Không thể tạo phiên chơi trong CSDL. Vui lòng thử lại."
                    };
                }

                Logger.LogBusiness(tenDangNhap, "Đăng nhập Client", $"Hội viên '{tenDangNhap}' đăng nhập máy {tenMay}. Mã phiên: #{maPhien}.");

                return new ClientDangNhapResultDTO
                {
                    ThanhCong = true,
                    MaPhienChoi = maPhien,
                    MaHoiVien = hv.MaHoiVien,
                    SoDu = hv.SoDu,
                    TenDangNhap = hv.TenDangNhap
                };
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ClientBLL] Lỗi khi xử lý đăng nhập cho '{tenDangNhap}':", ex);
                return new ClientDangNhapResultDTO
                {
                    ThanhCong = false,
                    ThongBao = "Lỗi kết nối CSDL: " + ex.Message
                };
            }
        }

        public static ClientDongBoDTO DongBoTrangThai(int maMay)
        {
            try
            {
                ClientDongBoDTO? dongBo = ClientDAL.LayPhienDangChay(maMay);
                string ttMay = ClientDAL.LayTrangThaiMay(maMay);

                if (dongBo == null)
                {
                    dongBo = new ClientDongBoDTO();
                }

                dongBo.TrangThaiMay = ttMay;
                return dongBo;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ClientBLL] Lỗi đồng bộ trạng thái máy #{maMay}:", ex);
                throw;
            }
        }

        public static void KetThucPhien(long maPhienChoi, int maMay, string tenMay, string tenDangNhap, string lyDo)
        {
            try
            {
                ClientDAL.KetThucPhienChoi(maPhienChoi);
                Logger.LogBusiness(string.IsNullOrEmpty(tenDangNhap) ? "Khách vãng lai" : tenDangNhap, "Trả máy Client", $"Kết thúc phiên #{maPhienChoi} tại {tenMay}. Lý do: {lyDo}.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ClientBLL] Lỗi kết thúc phiên #{maPhienChoi} tại {tenMay}:", ex);
                throw;
            }
        }

        public static void CapNhatTienRealTime(long maPhienChoi, decimal tienGio, int maHoiVien, decimal soDuConLai)
        {
            try
            {
                ClientDAL.CapNhatTienGioPhienChoi(maPhienChoi, tienGio, maHoiVien > 0 ? maHoiVien : (int?)null, soDuConLai);
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ClientBLL] Lỗi cập nhật tiền Realtime phiên #{maPhienChoi}:", ex);
            }
        }

        public static decimal TinhTienGio(TimeSpan daChoi, decimal donGiaGio)
        {
            int phut = Math.Max(1, (int)daChoi.TotalMinutes);
            return Math.Round(phut / 60m * donGiaGio, 0, MidpointRounding.AwayFromZero);
        }

        public static TimeSpan TinhThoiGianConLai(decimal soDuBanDau, decimal donGiaGio, TimeSpan daChoi)
        {
            if (donGiaGio <= 0) return TimeSpan.Zero;
            double tongGiay = (double)(soDuBanDau / donGiaGio) * 3600.0;
            return TimeSpan.FromSeconds(Math.Max(0, tongGiay - daChoi.TotalSeconds));
        }

        public static decimal TinhSoDuHienTai(decimal soDuBanDau, decimal donGiaGio, TimeSpan daChoi)
        {
            return Math.Max(0m, soDuBanDau - TinhTienGio(daChoi, donGiaGio));
        }

        public static string DinhDangThoiGian(TimeSpan t)
        {
            return $"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}";
        }
    }
}


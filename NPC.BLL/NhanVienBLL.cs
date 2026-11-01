using System;
using NPC.DAL;
using NPC.DTO;

namespace NPC.BLL
{
    public static class NhanVienBLL
    {
        public static DangNhapResultDTO DangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                return new DangNhapResultDTO
                {
                    ThanhCong = false,
                    ThongBao = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!"
                };
            }

            if (!HeThongBLL.DaKetNoiCSDL())
            {
                var ketQuaKetNoi = HeThongBLL.KiemTraKetNoiCSDL();
                if (!ketQuaKetNoi.ThanhCong)
                {
                    return new DangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Chưa thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra lại cấu hình!"
                    };
                }
            }

            try
            {
                NhanVienDTO? nv = NhanVienDAL.LayThongTinDangNhap(tenDangNhap);

                if (nv == null)
                {
                    Logger.LogWarning($"Đăng nhập thất bại: Tài khoản '{tenDangNhap}' không tồn tại trong hệ thống.");
                    return new DangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Tên đăng nhập hoặc mật khẩu không chính xác!"
                    };
                }

                if (!nv.TrangThai)
                {
                    Logger.LogWarning($"Tài khoản '{tenDangNhap}' đã bị khóa nhưng cố gắng đăng nhập.");
                    return new DangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Tài khoản này hiện đang bị khóa! Vui lòng liên hệ quản trị viên."
                    };
                }

                string cleanPassword = matKhau.Trim();
                string hashFromDb = nv.MatKhau;
                bool isPasswordValid = false;

                if (hashFromDb.StartsWith("$2a$") || hashFromDb.StartsWith("$2b$") || hashFromDb.StartsWith("$2y$"))
                {
                    try
                    {
                        isPasswordValid = BCrypt.Net.BCrypt.Verify(cleanPassword, hashFromDb);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Lỗi khi kiểm tra mã băm BCrypt cho tài khoản '{tenDangNhap}': {ex.Message}", ex);
                    }
                }

                if (!isPasswordValid && hashFromDb == cleanPassword)
                {
                    isPasswordValid = true;
                    try
                    {
                        string autoHashed = BCrypt.Net.BCrypt.HashPassword(cleanPassword, 11);
                        NhanVienDAL.CapNhatMatKhauBcrypt(tenDangNhap, autoHashed);
                        Logger.LogInfo($"Đã tự động băm BCrypt và cập nhật mật khẩu mới cho tài khoản '{tenDangNhap}'.");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Lỗi khi tự động cập nhật mã băm mật khẩu cho '{tenDangNhap}':", ex);
                    }
                }

                if (isPasswordValid)
                {
                    Logger.LogBusiness(nv.HoTen, "Đăng nhập", "Đăng nhập hệ thống Server thành công.");
                    return new DangNhapResultDTO
                    {
                        ThanhCong = true,
                        ThongBao = "Đăng nhập thành công!",
                        NhanVien = nv
                    };
                }
                else
                {
                    Logger.LogWarning($"Đăng nhập thất bại: Sai mật khẩu cho tài khoản '{tenDangNhap}'.");
                    return new DangNhapResultDTO
                    {
                        ThanhCong = false,
                        ThongBao = "Tên đăng nhập hoặc mật khẩu không chính xác!"
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi ngoại lệ trong quá trình đăng nhập của '{tenDangNhap}':", ex);
                return new DangNhapResultDTO
                {
                    ThanhCong = false,
                    ThongBao = $"Lỗi hệ thống trong quá trình đăng nhập: {ex.Message}"
                };
            }
        }
    }
}


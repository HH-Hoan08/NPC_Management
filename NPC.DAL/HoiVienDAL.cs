using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NPC.DTO;
using System.Data;

namespace NPC.DAL
{
    public class HoiVienDAL
    {
        /// <summary>
        /// Lấy DataTable danh sách hội viên bằng SqlDataAdapter thông qua SQLHelper.GetDataToTable
        /// </summary>
        public DataTable GetDataToTable(string tuKhoa = "", string trangThai = "")
        {
            string query = @"SELECT * FROM dbo.HoiVien WHERE 1=1 ";
            List<SqlParameter> pars = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query += @" AND (TenDangNhap LIKE @TuKhoa OR HoTen LIKE @TuKhoa OR DienThoai LIKE @TuKhoa) ";
                pars.Add(new SqlParameter("@TuKhoa", "%" + tuKhoa.Trim() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
            {
                query += @" AND TrangThai = @TrangThai ";
                pars.Add(new SqlParameter("@TrangThai", trangThai.Trim()));
            }

            query += " ORDER BY MaHoiVien DESC";

            return SQLHelper.GetDataToTable(query, pars.ToArray());
        }

        /// <summary>
        /// Lấy danh sách hội viên dạng List DTO (dùng hàm GetDataToTable)
        /// </summary>
        public List<HoiVienDTO> GetDanhSachHoiVien(string tuKhoa = "", string trangThai = "")
        {
            List<HoiVienDTO> listHV = new List<HoiVienDTO>();
            DataTable dt = GetDataToTable(tuKhoa, trangThai);

            foreach (DataRow row in dt.Rows)
            {
                listHV.Add(new HoiVienDTO
                {
                    MaHoiVien = Convert.ToInt32(row["MaHoiVien"]),
                    TenDangNhap = row["TenDangNhap"]?.ToString() ?? "",
                    MatKhau = row["MatKhau"]?.ToString() ?? "",
                    HoTen = row["HoTen"]?.ToString() ?? "",
                    SoDu = Convert.ToDecimal(row["SoDu"]),
                    TongThoiGianChoi = Convert.ToInt32(row["TongThoiGianChoi"]),
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),
                    TrangThai = row["TrangThai"]?.ToString() ?? "Hoạt động",
                    DienThoai = row["DienThoai"] != DBNull.Value ? row["DienThoai"]?.ToString() ?? "" : ""
                });
            }
            return listHV;
        }

        // kiểm tra tên đăng nhập đã tồn tại hay chưa
        public bool KiemTraTrungTenDangNhap(string tenDangNhap, int? maHoiVienHienTai = null)
        {
            string query = @"SELECT COUNT(1) FROM dbo.HoiVien WHERE TenDangNhap = @TenDangNhap";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@TenDangNhap", tenDangNhap)
            };

            if (maHoiVienHienTai.HasValue && maHoiVienHienTai.Value > 0)
            {
                query += " AND MaHoiVien <> @MaHoiVien";
                pars.Add(new SqlParameter("@MaHoiVien", maHoiVienHienTai.Value));
            }
            object? res = SQLHelper.ExecuteScalar(query, pars.ToArray());

            return res != null && Convert.ToInt32(res) > 0;
        }

        // thêm tài khoản hội viên mới
        public bool ThemHoiVien(HoiVienDTO hv, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            try
            {
                string query = @"INSERT INTO dbo.HoiVien (TenDangNhap, MatKhau, HoTen, SoDu, TongThoiGianChoi, NgayTao, TrangThai, DienThoai)
                                 VALUES (@TenDangNhap, @MatKhau, @HoTen, @SoDu, @TongThoiGianChoi, @NgayTao, @TrangThai, @DienThoai)";
                DateTime ngayTao = (hv.NgayTao.Year < 1753) ? DateTime.Now : hv.NgayTao;
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", hv.TenDangNhap.Trim()),
                    new SqlParameter("@MatKhau", hv.MatKhau),
                    new SqlParameter("@HoTen", hv.HoTen.Trim()),
                    new SqlParameter("@SoDu", hv.SoDu),
                    new SqlParameter("@TongThoiGianChoi", hv.TongThoiGianChoi),
                    new SqlParameter("@NgayTao", ngayTao),
                    new SqlParameter("@TrangThai", hv.TrangThai),
                    new SqlParameter("@DienThoai", string.IsNullOrEmpty(hv.DienThoai) ? DBNull.Value : hv.DienThoai.Trim())
                };
                return SQLHelper.ExecuteNonQuery(query, pars) > 0;
            }
            catch (Exception ex)
            {
                thongBaoLoi = "Lỗi khi thêm hội viên: " + ex.Message;
                return false;
            }
        }

        // cập nhật thông tin hội viên
        public bool CapNhatHoiVien(HoiVienDTO hv, bool doiMatKhau, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            try
            {
                string query = @"UPDATE dbo.HoiVien 
                                 SET HoTen = @HoTen,
                                     DienThoai = @DienThoai,
                                     TrangThai = @TrangThai " + (doiMatKhau ? ", MatKhau = @MatKhau " : " ") +
                                "WHERE MaHoiVien = @MaHoiVien";
                List<SqlParameter> pars = new List<SqlParameter>
                {
                    new SqlParameter("@HoTen", hv.HoTen.Trim()),
                    new SqlParameter("@DienThoai", string.IsNullOrEmpty(hv.DienThoai) ? DBNull.Value : hv.DienThoai.Trim()),
                    new SqlParameter("@TrangThai", hv.TrangThai),
                    new SqlParameter("@MaHoiVien", hv.MaHoiVien)
                };
                if (doiMatKhau)
                {
                    pars.Add(new SqlParameter("@MatKhau", hv.MatKhau));
                }
                return SQLHelper.ExecuteNonQuery(query, pars.ToArray()) > 0;
            }
            catch (Exception ex)
            {
                thongBaoLoi = ex.Message;
                return false;
            }
        }

        // xóa hội viên hoặc vô hiệu hóa tài khoản hội viên nếu có phát sinh giao dịch hoặc phiên chơi
        public bool XoaHoiVien(int maHoiVien, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            try
            {
                string query = @"SELECT (SELECT COUNT(*) FROM dbo.PhienChoi WHERE MaHoiVien = @MaHoiVien) +
                                        (SELECT COUNT(*) FROM dbo.GiaoDich WHERE MaHoiVien = @MaHoiVien)";
                int dem = Convert.ToInt32(SQLHelper.ExecuteScalar(query, new SqlParameter("@MaHoiVien", maHoiVien)) ?? 0);
                
                if (dem > 0)
                {
                    string deleteQuery = @"UPDATE dbo.HoiVien SET TrangThai = N'Bị khóa' WHERE MaHoiVien = @MaHoiVien";
                    SQLHelper.ExecuteNonQuery(deleteQuery, new SqlParameter("@MaHoiVien", maHoiVien));
                    thongBaoLoi = "Hội viên này đã có phát sinh giao dịch hoặc phiên chơi nên trạng thái được chuyển sang 'Bị khóa' để bảo toàn dữ liệu.";
                    return true;
                }
                else
                {
                    string deleteQuery = @"DELETE FROM dbo.HoiVien WHERE MaHoiVien = @MaHoiVien";
                    return SQLHelper.ExecuteNonQuery(deleteQuery, new SqlParameter("@MaHoiVien", maHoiVien)) > 0;
                }
            }
            catch (Exception ex)
            {
                thongBaoLoi = ex.Message;
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using NPC.DTO;

namespace NPC.DAL
{
    public class MonAnDAL
    {
        // lấy datatable danh sách món ăn kết hợp với danh mục
        public DataTable GetDataToTable(string tuKhoa = "", int? maDanhMuc = null, bool chiLayTonKhoThap = false)
        {
            string query = @"SELECT m.MaMon, m.MaDanhMuc, d.TenDanhMuc, m.TenMon, 
                                    m.DonGia, m.GiaNhap, m.SoLuongTon, m.DonViTinh, 
                                    m.HinhAnh, m.ConPhucVu,
                                    CASE WHEN m.ConPhucVu = 1 THEN N'Còn phục vụ' ELSE N'Ngừng phục vụ' END AS TrangThaiText,
                                    CASE WHEN m.SoLuongTon < 5 THEN N'Sắp hết' ELSE N'Bình thường' END AS CanhBaoText
                             FROM dbo.MonAn m
                             INNER JOIN dbo.DanhMuc d ON m.MaDanhMuc = d.MaDanhMuc
                             WHERE 1=1 ";

            List<SqlParameter> pars = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query += @" AND (m.TenMon LIKE @TuKhoa OR d.TenDanhMuc LIKE @TuKhoa OR m.DonViTinh LIKE @TuKhoa) ";
                pars.Add(new SqlParameter("@TuKhoa", "%" + tuKhoa.Trim() + "%"));
            }

            if (maDanhMuc.HasValue && maDanhMuc.Value > 0)
            {
                query += @" AND m.MaDanhMuc = @MaDanhMuc ";
                pars.Add(new SqlParameter("@MaDanhMuc", maDanhMuc.Value));
            }

            if (chiLayTonKhoThap)
            {
                query += @" AND m.SoLuongTon < 5 ";
            }

            query += " ORDER BY m.MaMon DESC";

            return SQLHelper.GetDataToTable(query, pars.ToArray());
        }
        // lấy danh sách danh mục để hiển thị trong combobox 
        public List<DanhMucDTO> GetDanhSachDanhMuc()
        {
            List<DanhMucDTO> listDM = new List<DanhMucDTO>();
            string query = "SELECT MaDanhMuc, TenDanhMuc FROM dbo.DanhMuc ORDER BY TenDanhMuc ASC";
            DataTable dt = SQLHelper.GetDataToTable(query);

            foreach (DataRow row in dt.Rows)
            {
                listDM.Add(new DanhMucDTO
                {
                    MaDanhMuc = Convert.ToInt32(row["MaDanhMuc"]),
                    TenDanhMuc = row["TenDanhMuc"]?.ToString() ?? ""
                });
            }
            return listDM;
        }
        // thêm món ăn mới vào database
        public bool ThemMonAn(MonAnDTO mon, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            try
            {
                string query = @"INSERT INTO dbo.MonAn (MaDanhMuc, TenMon, DonGia, GiaNhap, SoLuongTon, DonViTinh, HinhAnh, ConPhucVu)
                                 VALUES (@MaDanhMuc, @TenMon, @DonGia, @GiaNhap, @SoLuongTon, @DonViTinh, @HinhAnh, @ConPhucVu)";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@MaDanhMuc", mon.MaDanhMuc),
                    new SqlParameter("@TenMon", mon.TenMon.Trim()),
                    new SqlParameter("@DonGia", mon.DonGia),
                    new SqlParameter("@GiaNhap", mon.GiaNhap),
                    new SqlParameter("@SoLuongTon", mon.SoLuongTon),
                    new SqlParameter("@DonViTinh", string.IsNullOrEmpty(mon.DonViTinh) ? "Phần" : mon.DonViTinh.Trim()),
                    new SqlParameter("@HinhAnh", string.IsNullOrEmpty(mon.HinhAnh) ? (object)DBNull.Value : mon.HinhAnh.Trim()),
                    new SqlParameter("@ConPhucVu", mon.ConPhucVu)
                };

                return SQLHelper.ExecuteNonQuery(query, pars) > 0;
            }
            catch (Exception ex)
            {
                thongBaoLoi = "Lỗi khi thêm món ăn: " + ex.Message;
                return false;
            }
        }
        // cập nhật thônng tin món ăn
        public bool CapNhatMonAn(MonAnDTO mon, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            try
            {
                string query = @"UPDATE dbo.MonAn
                                 SET MaDanhMuc = @MaDanhMuc,
                                     TenMon = @TenMon,
                                     DonGia = @DonGia,
                                     GiaNhap = @GiaNhap,
                                     SoLuongTon = @SoLuongTon,
                                     DonViTinh = @DonViTinh,
                                     HinhAnh = @HinhAnh,
                                     ConPhucVu = @ConPhucVu
                                 WHERE MaMon = @MaMon";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@MaMon", mon.MaMon),
                    new SqlParameter("@MaDanhMuc", mon.MaDanhMuc),
                    new SqlParameter("@TenMon", mon.TenMon.Trim()),
                    new SqlParameter("@DonGia", mon.DonGia),
                    new SqlParameter("@GiaNhap", mon.GiaNhap),
                    new SqlParameter("@SoLuongTon", mon.SoLuongTon),
                    new SqlParameter("@DonViTinh", string.IsNullOrEmpty(mon.DonViTinh) ? "Phần" : mon.DonViTinh.Trim()),
                    new SqlParameter("@HinhAnh", string.IsNullOrEmpty(mon.HinhAnh) ? (object)DBNull.Value : mon.HinhAnh.Trim()),
                    new SqlParameter("@ConPhucVu", mon.ConPhucVu)
                };

                return SQLHelper.ExecuteNonQuery(query, pars) > 0;
            }
            catch (Exception ex)
            {
                thongBaoLoi = "Lỗi khi sửa món ăn: " + ex.Message;
                return false;
            }
        }
        // xóa món ăn (nếu món ăn đã có đơn hàng thì chuyển trạng thái sang ngừng phục vụ)
        public bool XoaMonAn(int maMon, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            try
            {
                string queryCheck = "SELECT COUNT(1) FROM dbo.ChiTietDonHang WHERE MaMon = @MaMon";
                int count = Convert.ToInt32(SQLHelper.ExecuteScalar(queryCheck, new SqlParameter("@MaMon", maMon)) ?? 0);

                if (count > 0)
                {
                    string sqlUpdate = "UPDATE dbo.MonAn SET ConPhucVu = 0 WHERE MaMon = @MaMon";
                    SQLHelper.ExecuteNonQuery(sqlUpdate, new SqlParameter("@MaMon", maMon));
                    thongBaoLoi = "Món ăn đã có lịch sử trong các đơn đặt hàng nên hệ thống chuyển trạng thái sang 'Ngừng phục vụ' để bảo toàn dữ liệu.";
                    return true;
                }
                else
                {
                    string sqlDelete = "DELETE FROM dbo.MonAn WHERE MaMon = @MaMon";
                    return SQLHelper.ExecuteNonQuery(sqlDelete, new SqlParameter("@MaMon", maMon)) > 0;
                }
            }
            catch (Exception ex)
            {
                thongBaoLoi = ex.Message;
                return false;
            }
        }
        // nhập kho hàng bọc trong sql transaction
        public bool NhapKhoMonAn(int maMon, int soLuongThem, decimal giaNhapMoi, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlUpdate = @"UPDATE dbo.MonAn
                                             SET SoLuongTon = SoLuongTon + @SoLuongThem,
                                                 GiaNhap = CASE WHEN @GiaNhapMoi > 0 THEN @GiaNhapMoi ELSE GiaNhap END
                                             WHERE MaMon = @MaMon";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@SoLuongThem", soLuongThem);
                            cmd.Parameters.AddWithValue("@GiaNhapMoi", giaNhapMoi);
                            cmd.Parameters.AddWithValue("@MaMon", maMon);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        thongBaoLoi = "Lỗi khi nhập kho: " + ex.Message;
                        return false;
                    }
                }
            }
        }
    }
}

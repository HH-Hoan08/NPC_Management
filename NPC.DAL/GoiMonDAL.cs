#pragma warning disable CS0618
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NPC.DTO;

namespace NPC.DAL
{
    /// <summary>
    /// Tầng DAL chuyên trách truy vấn thực đơn và lưu trữ đơn hàng gọi món
    /// </summary>
    public static class GoiMonDAL
    {
        /// <summary>
        /// Lấy toàn bộ danh mục món ăn đang có trong hệ thống
        /// </summary>
        public static List<DanhMucDTO> LayDanhSachDanhMuc()
        {
            var danhSach = new List<DanhMucDTO>();
            string sql = "SELECT MaDanhMuc, TenDanhMuc FROM dbo.DanhMuc ORDER BY TenDanhMuc";
            DataTable dt = SQLHelper.ExecuteQuery(sql);

            foreach (DataRow r in dt.Rows)
            {
                danhSach.Add(new DanhMucDTO
                {
                    MaDanhMuc = Convert.ToInt32(r["MaDanhMuc"]),
                    TenDanhMuc = r["TenDanhMuc"] != DBNull.Value ? r["TenDanhMuc"].ToString()! : string.Empty
                });
            }

            return danhSach;
        }

        /// <summary>
        /// Lấy danh sách món ăn đang còn phục vụ, có lọc theo danh mục và tìm kiếm theo tên
        /// </summary>
        public static List<MonAnDTO> LayThucDon(int maDanhMuc, string tuKhoa)
        {
            var danhSach = new List<MonAnDTO>();
            string sql = @"SELECT m.MaMon, m.MaDanhMuc, ISNULL(d.TenDanhMuc, N'') AS TenDanhMuc,
                                  m.TenMon, m.DonGia, m.SoLuongTon, m.HinhAnh, m.ConPhucVu, m.GiaNhap, m.DonViTinh
                           FROM dbo.MonAn m
                           LEFT JOIN dbo.DanhMuc d ON m.MaDanhMuc = d.MaDanhMuc
                           WHERE m.ConPhucVu = 1
                             AND (@MaDanhMuc = 0 OR m.MaDanhMuc = @MaDanhMuc)
                             AND (@TuKhoa = '' OR m.TenMon LIKE @TuKhoaPattern)
                           ORDER BY m.TenMon";

            string pattern = string.IsNullOrWhiteSpace(tuKhoa) ? "" : $"%{tuKhoa.Trim()}%";
            SqlParameter[] pars =
            {
                new SqlParameter("@MaDanhMuc", maDanhMuc),
                new SqlParameter("@TuKhoa", tuKhoa.Trim()),
                new SqlParameter("@TuKhoaPattern", pattern)
            };

            DataTable dt = SQLHelper.ExecuteQuery(sql, pars);
            foreach (DataRow r in dt.Rows)
            {
                danhSach.Add(new MonAnDTO
                {
                    MaMon = Convert.ToInt32(r["MaMon"]),
                    MaDanhMuc = r["MaDanhMuc"] != DBNull.Value ? Convert.ToInt32(r["MaDanhMuc"]) : 0,
                    TenDanhMuc = r["TenDanhMuc"] != DBNull.Value ? r["TenDanhMuc"].ToString()! : string.Empty,
                    TenMon = r["TenMon"] != DBNull.Value ? r["TenMon"].ToString()! : string.Empty,
                    DonGia = r["DonGia"] != DBNull.Value ? Convert.ToDecimal(r["DonGia"]) : 0m,
                    SoLuongTon = r["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(r["SoLuongTon"]) : 0,
                    HinhAnh = r["HinhAnh"] != DBNull.Value ? r["HinhAnh"].ToString() : null,
                    ConPhucVu = r["ConPhucVu"] != DBNull.Value && Convert.ToBoolean(r["ConPhucVu"]),
                    GiaNhap = r["GiaNhap"] != DBNull.Value ? Convert.ToDecimal(r["GiaNhap"]) : 0m,
                    DonViTinh = r["DonViTinh"] != DBNull.Value ? r["DonViTinh"].ToString()! : string.Empty
                });
            }

            return danhSach;
        }

        /// <summary>
        /// Lấy thông tin một món ăn theo mã để kiểm tra giá và tồn kho thời gian thực
        /// </summary>
        public static MonAnDTO? LayMonAnTheoMa(int maMon, SqlConnection? conn = null, SqlTransaction? tran = null)
        {
            string sql = @"SELECT m.MaMon, m.MaDanhMuc, ISNULL(d.TenDanhMuc, N'') AS TenDanhMuc,
                                  m.TenMon, m.DonGia, m.SoLuongTon, m.HinhAnh, m.ConPhucVu, m.GiaNhap, m.DonViTinh
                           FROM dbo.MonAn m
                           LEFT JOIN dbo.DanhMuc d ON m.MaDanhMuc = d.MaDanhMuc
                           WHERE m.MaMon = @MaMon";

            DataTable dt;
            if (conn != null && tran != null)
            {
                dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
                {
                    cmd.Parameters.AddWithValue("@MaMon", maMon);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            else
            {
                dt = SQLHelper.ExecuteQuery(sql, new SqlParameter("@MaMon", maMon));
            }

            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new MonAnDTO
            {
                MaMon = Convert.ToInt32(r["MaMon"]),
                MaDanhMuc = r["MaDanhMuc"] != DBNull.Value ? Convert.ToInt32(r["MaDanhMuc"]) : 0,
                TenDanhMuc = r["TenDanhMuc"] != DBNull.Value ? r["TenDanhMuc"].ToString()! : string.Empty,
                TenMon = r["TenMon"] != DBNull.Value ? r["TenMon"].ToString()! : string.Empty,
                DonGia = r["DonGia"] != DBNull.Value ? Convert.ToDecimal(r["DonGia"]) : 0m,
                SoLuongTon = r["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(r["SoLuongTon"]) : 0,
                HinhAnh = r["HinhAnh"] != DBNull.Value ? r["HinhAnh"].ToString() : null,
                ConPhucVu = r["ConPhucVu"] != DBNull.Value && Convert.ToBoolean(r["ConPhucVu"]),
                GiaNhap = r["GiaNhap"] != DBNull.Value ? Convert.ToDecimal(r["GiaNhap"]) : 0m,
                DonViTinh = r["DonViTinh"] != DBNull.Value ? r["DonViTinh"].ToString()! : string.Empty
            };
        }

        /// <summary>
        /// Tạo đơn hàng và các chi tiết đơn hàng trong một SqlTransaction.
        /// Đồng thời trừ số lượng tồn kho của các món tương ứng.
        /// </summary>
        public static long TaoDonHang(long maPhienChoi, int maMay, List<ChiTietDonHangDTO> chiTietList, decimal tongTien)
        {
            using (SqlConnection conn = NPCData.CreateConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        long maDonHang;
                        string sqlDonHang = @"INSERT INTO dbo.DonHang (MaPhienChoi, MaMay, ThoiGianDat, TongTien, TrangThai)
                                              VALUES (@MaPhienChoi, @MaMay, GETDATE(), @TongTien, N'Pending');
                                              SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

                        using (SqlCommand cmd = new SqlCommand(sqlDonHang, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaPhienChoi", maPhienChoi > 0 ? (object)maPhienChoi : DBNull.Value);
                            cmd.Parameters.AddWithValue("@MaMay", maMay);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            maDonHang = Convert.ToInt64(cmd.ExecuteScalar());
                        }
                        foreach (var item in chiTietList)
                        {
                            string sqlTruKho = @"UPDATE dbo.MonAn
                                                 SET SoLuongTon = SoLuongTon - @SoLuong
                                                 WHERE MaMon = @MaMon AND ConPhucVu = 1 AND SoLuongTon >= @SoLuong";

                            using (SqlCommand cmdTru = new SqlCommand(sqlTruKho, conn, tran))
                            {
                                cmdTru.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                                cmdTru.Parameters.AddWithValue("@MaMon", item.MaMon);
                                int rowsAffected = cmdTru.ExecuteNonQuery();
                                if (rowsAffected != 1)
                                {
                                    throw new InvalidOperationException($"Món \"{item.TenMon}\" hiện không đủ số lượng tồn kho hoặc đã ngừng phục vụ.");
                                }
                            }

                            // Chèn chi tiết đơn hàng
                            string sqlChiTiet = @"INSERT INTO dbo.ChiTietDonHang (MaDonHang, MaMon, SoLuong, DonGia)
                                                  VALUES (@MaDonHang, @MaMon, @SoLuong, @DonGia)";

                            using (SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, tran))
                            {
                                cmdChiTiet.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                cmdChiTiet.Parameters.AddWithValue("@MaMon", item.MaMon);
                                cmdChiTiet.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                                cmdChiTiet.Parameters.AddWithValue("@DonGia", item.DonGia);
                                cmdChiTiet.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        return maDonHang;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

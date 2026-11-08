using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NPC.DTO;

namespace NPC.DAL
{
    public static class ClientDAL
    {
        public static ClientThongTinMayDTO? LayThongTinMay(string tenMay)
        {
            string sql = @"SELECT m.MaMay, m.TenMay, k.DonGiaGio, m.TrangThai
                           FROM dbo.MayTram m INNER JOIN dbo.KhuVucPhong k ON m.MaPhong = k.MaPhong
                           WHERE m.TenMay = @Ten";

            DataTable dt = SQLHelper.ExecuteQuery(sql, new SqlParameter("@Ten", tenMay));
            if (dt == null || dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new ClientThongTinMayDTO
            {
                MaMay = Convert.ToInt32(r["MaMay"]),
                TenMay = r["TenMay"].ToString()!,
                DonGiaGio = Convert.ToDecimal(r["DonGiaGio"]),
                TrangThai = r["TrangThai"] != DBNull.Value ? r["TrangThai"].ToString()! : string.Empty
            };
        }

        public static int CapNhatTrangThaiMay(int maMay, string trangThaiMoi, string? trangThaiDieuKien = null)
        {
            string sql;
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@Ma", maMay),
                new SqlParameter("@Moi", trangThaiMoi)
            };

            if (!string.IsNullOrEmpty(trangThaiDieuKien))
            {
                sql = "UPDATE dbo.MayTram SET TrangThai = @Moi WHERE MaMay = @Ma AND TrangThai = @Cu";
                pars.Add(new SqlParameter("@Cu", trangThaiDieuKien));
            }
            else
            {
                sql = "UPDATE dbo.MayTram SET TrangThai = @Moi WHERE MaMay = @Ma";
            }

            return SQLHelper.ExecuteNonQuery(sql, pars.ToArray());
        }

        public static string LayTrangThaiMay(int maMay)
        {
            DataTable dt = SQLHelper.ExecuteQuery("SELECT TrangThai FROM dbo.MayTram WHERE MaMay = @Ma", new SqlParameter("@Ma", maMay));
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            return dt.Rows[0]["TrangThai"] != DBNull.Value ? dt.Rows[0]["TrangThai"].ToString()! : string.Empty;
        }

        public static HoiVienDTO? LayThongTinHoiVien(string tenDangNhap)
        {
            string sql = "SELECT MaHoiVien, TenDangNhap, MatKhau, HoTen, SoDu, TrangThai FROM dbo.HoiVien WHERE TenDangNhap = @Ten";
            DataTable dt = SQLHelper.ExecuteQuery(sql, new SqlParameter("@Ten", tenDangNhap));
            if (dt == null || dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new HoiVienDTO
            {
                MaHoiVien = Convert.ToInt32(r["MaHoiVien"]),
                TenDangNhap = r["TenDangNhap"].ToString()!,
                MatKhau = r["MatKhau"] != DBNull.Value ? r["MatKhau"].ToString()! : string.Empty,
                HoTen = r["HoTen"] != DBNull.Value ? r["HoTen"].ToString()! : string.Empty,
                SoDu = r["SoDu"] != DBNull.Value ? Convert.ToDecimal(r["SoDu"]) : 0m,
                TrangThai = r["TrangThai"] != DBNull.Value ? r["TrangThai"].ToString()! : string.Empty
            };
        }

        public static long MoPhienChoi(int maMay, int? maHoiVien)
        {
            using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("dbo.sp_MoPhienChoi", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.Add(new SqlParameter("@MaMay", SqlDbType.Int) { Value = maMay });
                SqlParameter paramHoiVien = new SqlParameter("@MaHoiVien", SqlDbType.Int)
                {
                    Value = maHoiVien.HasValue ? (object)maHoiVien.Value : DBNull.Value
                };
                cmd.Parameters.Add(paramHoiVien);

                SqlParameter paramOut = new SqlParameter("@MaPhienChoiMoi", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramOut);

                conn.Open();
                cmd.ExecuteNonQuery();

                return paramOut.Value != DBNull.Value ? Convert.ToInt64(paramOut.Value) : 0;
            }
        }

        public static void KetThucPhienChoi(long maPhienChoi)
        {
            using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("dbo.sp_KetThucPhienChoi", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.Add(new SqlParameter("@MaPhienChoi", SqlDbType.BigInt) { Value = maPhienChoi });
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cập nhật tiền giờ của phiên và TRỪ TƯƠNG ĐỐI (SoDu = SoDu - @SoTienTru) vào số dư hội viên.
        /// Không ghi đè số dư tuyệt đối để không làm mất tiền quầy vừa nạp trong lúc phiên đang chạy.
        /// Trả về true nếu phiên vẫn đang chơi và cập nhật thành công.
        /// </summary>
        public static bool CapNhatTienGioPhienChoi(long maPhienChoi, decimal tienGio, int? maHoiVien, decimal soTienTruThem)
        {
            string sql = @"
                UPDATE dbo.PhienChoi SET TienGio = @TienGio
                WHERE MaPhienChoi = @MaPhienChoi AND TrangThaiPhien IN (N'Đang chơi', N'Active');

                DECLARE @SoPhien INT = @@ROWCOUNT;

                IF @SoPhien > 0 AND @MaHoiVien IS NOT NULL AND @SoTienTru > 0
                    UPDATE dbo.HoiVien
                    SET SoDu = CASE WHEN SoDu >= @SoTienTru THEN SoDu - @SoTienTru ELSE 0 END
                    WHERE MaHoiVien = @MaHoiVien;

                SELECT @SoPhien;";

            object? raw = SQLHelper.ExecuteScalar(sql,
                new SqlParameter("@TienGio", tienGio),
                new SqlParameter("@MaPhienChoi", maPhienChoi),
                new SqlParameter("@MaHoiVien", SqlDbType.Int) { Value = (maHoiVien.HasValue && maHoiVien.Value > 0) ? (object)maHoiVien.Value : DBNull.Value },
                new SqlParameter("@SoTienTru", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = Math.Max(0m, soTienTruThem) });

            return raw != null && raw != DBNull.Value && Convert.ToInt32(raw) > 0;
        }

        public static ClientDongBoDTO? LayPhienDangChay(int maMay)
        {
            string sql = @"SELECT TOP 1 p.MaPhienChoi, p.MaHoiVien, h.TenDangNhap, h.SoDu,
                                  DATEDIFF(SECOND, p.ThoiGianBatDau, GETDATE()) AS Giay,
                                  ISNULL(p.TienGio, 0) AS TienGioDaTru
                           FROM dbo.PhienChoi p LEFT JOIN dbo.HoiVien h ON p.MaHoiVien = h.MaHoiVien
                           WHERE p.MaMay = @Ma AND (p.TrangThaiPhien = N'Active' OR p.TrangThaiPhien = N'Đang chơi')
                           ORDER BY p.MaPhienChoi DESC";

            DataTable dt = SQLHelper.ExecuteQuery(sql, new SqlParameter("@Ma", maMay));
            if (dt == null || dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new ClientDongBoDTO
            {
                MaPhienChoi = Convert.ToInt64(r["MaPhienChoi"]),
                MaHoiVien = r["MaHoiVien"] == DBNull.Value ? 0 : Convert.ToInt32(r["MaHoiVien"]),
                TenDangNhap = r["TenDangNhap"] == DBNull.Value ? string.Empty : r["TenDangNhap"].ToString()!,
                SoDu = r["SoDu"] == DBNull.Value ? 0m : Convert.ToDecimal(r["SoDu"]),
                GiayDaChoi = r["Giay"] == DBNull.Value ? 0 : Convert.ToInt32(r["Giay"]),
                TienGioDaTru = r["TienGioDaTru"] == DBNull.Value ? 0m : Convert.ToDecimal(r["TienGioDaTru"])
            };
        }
    }
}


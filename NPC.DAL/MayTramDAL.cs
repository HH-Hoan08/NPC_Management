using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NPC.DTO;

namespace NPC.DAL
{
    public static class MayTramDAL
    {
        public static List<MayTramDTO> GetDanhSachMayTram(int? maPhong = null, string? trangThai = null)
        {
            List<MayTramDTO> list = new List<MayTramDTO>();

            string query = @"
                SELECT 
                    m.MaMay,
                    m.TenMay,
                    m.DiaChiIP,
                    m.DiaChiMAC,
                    m.MaPhong,
                    p.TenPhong,
                    p.DonGiaGio,
                    m.TrangThai,
                    pc.MaPhienChoi,
                    pc.MaHoiVien,
                    ISNULL(hv.HoTen, hv.TenDangNhap) AS TenHoiVien,
                    pc.ThoiGianBatDau,
                    ISNULL(hv.SoDu, 0) AS SoDuHoiVien,
                    ISNULL(pc.TienGio, 0) AS TienGioHienTai
                FROM dbo.MayTram m
                INNER JOIN dbo.KhuVucPhong p ON m.MaPhong = p.MaPhong
                LEFT JOIN dbo.PhienChoi pc ON m.MaMay = pc.MaMay AND (pc.TrangThaiPhien = N'Đang chơi' OR pc.TrangThaiPhien = 'Active')
                LEFT JOIN dbo.HoiVien hv ON pc.MaHoiVien = hv.MaHoiVien
                WHERE 1 = 1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (maPhong.HasValue && maPhong.Value > 0)
            {
                query += " AND m.MaPhong = @MaPhong ";
                parameters.Add(new SqlParameter("@MaPhong", maPhong.Value));
            }

            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
            {
                query += " AND m.TrangThai = @TrangThai ";
                parameters.Add(new SqlParameter("@TrangThai", trangThai));
            }

            query += " ORDER BY m.MaMay ASC ";

            DataTable dt = SQLHelper.ExecuteQuery(query, parameters.ToArray());

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MayTramDTO dto = new MayTramDTO
                    {
                        MaMay = Convert.ToInt32(row["MaMay"]),
                        TenMay = row["TenMay"].ToString()!,
                        DiaChiIP = row["DiaChiIP"] != DBNull.Value ? row["DiaChiIP"].ToString()! : string.Empty,
                        DiaChiMAC = row["DiaChiMAC"] != DBNull.Value ? row["DiaChiMAC"].ToString()! : string.Empty,
                        MaPhong = Convert.ToInt32(row["MaPhong"]),
                        TenPhong = row["TenPhong"].ToString()!,
                        DonGiaGio = Convert.ToDecimal(row["DonGiaGio"]),
                        TrangThai = row["TrangThai"].ToString()!,
                        MaPhienChoi = row["MaPhienChoi"] != DBNull.Value ? Convert.ToInt64(row["MaPhienChoi"]) : (long?)null,
                        MaHoiVien = row["MaHoiVien"] != DBNull.Value ? Convert.ToInt32(row["MaHoiVien"]) : (int?)null,
                        TenHoiVien = row["TenHoiVien"] != DBNull.Value ? row["TenHoiVien"].ToString()! : string.Empty,
                        ThoiGianBatDau = row["ThoiGianBatDau"] != DBNull.Value ? Convert.ToDateTime(row["ThoiGianBatDau"]) : (DateTime?)null,
                        SoDuHoiVien = Convert.ToDecimal(row["SoDuHoiVien"]),
                        TienGioHienTai = Convert.ToDecimal(row["TienGioHienTai"])
                    };

                    list.Add(dto);
                }
            }

            return list;
        }

        public static long MoPhienChoi(int maMay, int? maHoiVien)
        {
            using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_MoPhienChoi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@MaMay", SqlDbType.Int) { Value = maMay });

                    SqlParameter paramHoiVien = new SqlParameter("@MaHoiVien", SqlDbType.Int);
                    if (maHoiVien.HasValue)
                        paramHoiVien.Value = maHoiVien.Value;
                    else
                        paramHoiVien.Value = DBNull.Value;
                    cmd.Parameters.Add(paramHoiVien);

                    SqlParameter paramOutput = new SqlParameter("@MaPhienChoiMoi", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramOutput);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (paramOutput.Value != DBNull.Value)
                    {
                        return Convert.ToInt64(paramOutput.Value);
                    }
                    return 0;
                }
            }
        }

        public static int CapNhatTrangThaiMay(int maMay, string trangThaiMoi)
        {
            string query = "UPDATE dbo.MayTram SET TrangThai = @TrangThai WHERE MaMay = @MaMay";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@TrangThai", trangThaiMoi),
                new SqlParameter("@MaMay", maMay)
            };

            return SQLHelper.ExecuteNonQuery(query, pars);
        }

        public static bool KiemTraCoPhienDangChay(int maMay)
        {
            string query = "SELECT COUNT(1) FROM dbo.PhienChoi WHERE MaMay = @MaMay AND (TrangThaiPhien = N'Đang chơi' OR TrangThaiPhien = 'Active')";
            object? val = SQLHelper.ExecuteScalar(query, new SqlParameter("@MaMay", maMay));
            return val != null && Convert.ToInt32(val) > 0;
        }

        public static void KetThucPhienChoiSP(long maPhienChoi)
        {
            using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_KetThucPhienChoi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@MaPhienChoi", SqlDbType.BigInt) { Value = maPhienChoi });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

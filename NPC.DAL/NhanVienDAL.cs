using System;
using System.Data;
using System.Data.SqlClient;
using NPC.DTO;

namespace NPC.DAL
{
    public static class NhanVienDAL
    {
        public static NhanVienDTO? LayThongTinDangNhap(string tenDangNhap)
        {
            string query = "SELECT MaNhanVien, TenDangNhap, MatKhau, HoTen, TrangThai FROM NhanVien WHERE TenDangNhap = @Username";
            DataTable dt = SQLHelper.ExecuteQuery(query, new SqlParameter("@Username", tenDangNhap));

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            return new NhanVienDTO
            {
                MaNhanVien = row.Table.Columns.Contains("MaNhanVien") && row["MaNhanVien"] != DBNull.Value ? Convert.ToInt32(row["MaNhanVien"]) : 0,
                TenDangNhap = row["TenDangNhap"] != DBNull.Value ? row["TenDangNhap"].ToString()! : tenDangNhap,
                MatKhau = row["MatKhau"] != DBNull.Value ? row["MatKhau"].ToString()!.Trim() : string.Empty,
                HoTen = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString()!.Trim() : tenDangNhap,
                TrangThai = row["TrangThai"] != DBNull.Value && Convert.ToBoolean(row["TrangThai"])
            };
        }

        public static int CapNhatMatKhauBcrypt(string tenDangNhap, string matKhauBcrypt)
        {
            string query = "UPDATE NhanVien SET MatKhau = @Hashed WHERE TenDangNhap = @Username";
            return SQLHelper.ExecuteNonQuery(query,
                new SqlParameter("@Hashed", matKhauBcrypt),
                new SqlParameter("@Username", tenDangNhap));
        }
    }
}


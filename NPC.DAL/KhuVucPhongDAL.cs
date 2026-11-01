using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NPC.DTO;

namespace NPC.DAL
{
    public static class KhuVucPhongDAL
    {
        public static List<KhuVucPhongDTO> LayDanhSachKhuVuc()
        {
            List<KhuVucPhongDTO> list = new List<KhuVucPhongDTO>();
            string query = "SELECT MaPhong, TenPhong, DonGiaGio FROM dbo.KhuVucPhong ORDER BY MaPhong ASC";
            DataTable dt = SQLHelper.ExecuteQuery(query);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new KhuVucPhongDTO
                    {
                        MaPhong = Convert.ToInt32(row["MaPhong"]),
                        TenPhong = row["TenPhong"].ToString()!,
                        DonGiaGio = row["DonGiaGio"] != DBNull.Value ? Convert.ToDecimal(row["DonGiaGio"]) : 0m
                    });
                }
            }
            return list;
        }
    }
}


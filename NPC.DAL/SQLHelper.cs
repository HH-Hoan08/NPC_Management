using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NPC.DAL
{
    public class SQLHelper
    {
        //Thực thi INSERT, UPDATE, DELETE
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            if (!NPCData.IsOK) NPCData.Connect();
            {
                using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            return -1;
        }

        //Thực thi lấy 1 giá trị (COUNT, MAX, IDENTITY...)
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            if (!NPCData.IsOK) NPCData.Connect();
            {
                using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            return null;
        }

        //Thực thi lấy danh sách/bảng dữ liệu (SELECT)
        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            if (!NPCData.IsOK) NPCData.Connect();
            {
                using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            return dt;
        }
    }
}
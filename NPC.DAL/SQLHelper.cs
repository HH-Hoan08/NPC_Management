using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NPC.DAL
{
    public class SQLHelper
    {
        // =====================================================================
        // NHÓM 1: Thực thi độc lập (tự mở kết nối riêng, không dùng transaction)
        // =====================================================================

        /// <summary>Thực thi INSERT, UPDATE, DELETE — tự mở kết nối riêng.</summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            if (NPCData.IsOK == true)
            {
                using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null) cmd.Parameters.AddRange(parameters);
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            return -1;
        }

        /// <summary>Thực thi SELECT lấy 1 giá trị (COUNT, MAX, IDENTITY...) — tự mở kết nối riêng.</summary>
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            if (NPCData.IsOK == true)
            {
                using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null) cmd.Parameters.AddRange(parameters);
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            return null;
        }

        /// <summary>Thực thi SELECT lấy DataTable — tự mở kết nối riêng.</summary>
        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            if (NPCData.IsOK == true)
            {
                using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null) cmd.Parameters.AddRange(parameters);
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

        // =====================================================================
        // NHÓM 2: Thực thi trong Transaction (dùng kết nối + transaction được truyền vào)
        // Dùng khi cần bọc nhiều câu lệnh trong 1 SqlTransaction (ACID).
        // KHÔNG tự mở/đóng kết nối — người gọi chịu trách nhiệm quản lý.
        // =====================================================================

        /// <summary>
        /// Thực thi INSERT, UPDATE, DELETE trong một SqlTransaction đang mở.
        /// </summary>
        /// <param name="conn">Kết nối đang mở.</param>
        /// <param name="tran">Transaction đang hoạt động.</param>
        /// <param name="query">Câu lệnh SQL.</param>
        /// <param name="parameters">Tham số SQL (tùy chọn).</param>
        /// <returns>Số dòng bị ảnh hưởng.</returns>
        public static int ExecuteNonQuery(SqlConnection conn, SqlTransaction tran,
            string query, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Thực thi SELECT lấy 1 giá trị trong một SqlTransaction đang mở.
        /// </summary>
        public static object ExecuteScalar(SqlConnection conn, SqlTransaction tran,
            string query, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Thực thi SELECT lấy DataTable trong một SqlTransaction đang mở.
        /// </summary>
        public static DataTable ExecuteQuery(SqlConnection conn, SqlTransaction tran,
            string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, conn, tran))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;

namespace Net.NPC.DAL
{
    public static class DatabaseHelper
    {
        #region Constants & Internal Fields / Hằng số & Biến nội bộ
        // Fallback default connection string pointing to Net.NPCDB / Chuỗi kết nối mặc định dự phòng trỏ đúng CSDL Net.NPCDB
        private const string DefaultConnectionString = @"Server=.\SQLEXPRESS;Database=Net.NPCDB;Integrated Security=True;";

        // In-memory cache for connection string to avoid repeated file I/O / Biến lưu cache chuỗi kết nối trong bộ nhớ để tránh đọc file config liên tục
        private static string _connectionString = null;
        #endregion

        #region Connection String Property / Thuộc tính chuỗi kết nối
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // 1. Read decrypted connection string from config.ini / Đọc chuỗi kết nối đã giải mã từ file config.ini
                    string savedConnectionString = ConfigHelper.ReadConnectionString();

                    // 2. Load if exists; otherwise auto-detect or use default / Nếu đã có thì nạp, nếu chưa thì tự động dò tìm hoặc lấy mặc định
                    if (!string.IsNullOrWhiteSpace(savedConnectionString))
                    {
                        _connectionString = savedConnectionString;
                    }
                    else if (ConfigHelper.TryAutoConnect(out string autoDetectedConnectionString))
                    {
                        _connectionString = autoDetectedConnectionString;
                    }
                    else
                    {
                        _connectionString = DefaultConnectionString;
                        ConfigHelper.SaveConnectionString(DefaultConnectionString);
                    }
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        #endregion

        #region Connection Initialization / Khởi tạo kết nối
        /// 
        /// Creates and returns a new SqlConnection instance / Tạo và trả về một đối tượng SqlConnection mới
        /// 
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        #endregion

        #region 1. ExecuteQuery (For SELECT queries / Dùng cho câu lệnh SELECT lấy bảng dữ liệu)
        /// 
        /// Executes a SELECT query or Stored Procedure returning a DataTable / Thực thi câu truy vấn SELECT hoặc Stored Procedure trả về DataTable
        /// 
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            DataTable resultTable = new DataTable();

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = commandType;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(resultTable);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Catch SQL Server specific errors and log error number / Bắt riêng lỗi SQL Server và ghi vết kèm mã lỗi (Error Number)
                Logger.WriteLog($"Lỗi SQL [{sqlEx.Number}]: {sqlEx.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }
            catch (Exception ex)
            {
                // Catch general system exceptions / Bắt các lỗi hệ thống chung
                Logger.WriteLog($"Lỗi chung: {ex.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }

            return resultTable;
        }
        #endregion

        #region 2. ExecuteNonQuery (For INSERT, UPDATE, DELETE / Dùng cho INSERT, UPDATE, DELETE)
        /// 
        /// Executes insert, update, delete commands and returns affected rows count / Thực thi câu lệnh thêm, sửa, xóa và trả về số dòng bị ảnh hưởng
        /// 
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            int affectedRows = 0;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = commandType;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        connection.Open();
                        affectedRows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Catch SQL Server specific errors / Bắt riêng lỗi SQL Server
                Logger.WriteLog($"Lỗi SQL [{sqlEx.Number}]: {sqlEx.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }
            catch (Exception ex)
            {
                // Catch general exceptions / Bắt các lỗi ngoại lệ chung
                Logger.WriteLog($"Lỗi chung: {ex.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }

            return affectedRows;
        }

        /// 
        /// Overload: Executes command within an existing SqlTransaction (ACID compliance) / Nạp chồng: Thực thi câu lệnh bên trong một SqlTransaction có sẵn (Bảo vệ dữ liệu ACID)
        /// 
        public static int ExecuteNonQuery(SqlCommand command, SqlTransaction transaction, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.Clear();

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                Logger.WriteLog($"Lỗi Transaction SQL [{sqlEx.Number}]: {sqlEx.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"Lỗi Transaction chung: {ex.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }
        }
        #endregion

        #region 3. ExecuteScalar (For COUNT, SUM, generated IDs / Dùng cho COUNT, SUM, lấy ID vừa sinh)
        /// 
        /// Executes query and returns first column of the first row / Thực thi câu truy vấn và trả về giá trị ở ô đầu tiên của dòng đầu tiên
        /// 
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            object scalarResult = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = commandType;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        connection.Open();
                        scalarResult = command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.WriteLog($"Lỗi SQL [{sqlEx.Number}]: {sqlEx.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"Lỗi chung: {ex.Message} | Query: {query}", "DatabaseHelper");
                throw;
            }

            return scalarResult;
        }
        #endregion
    }
}
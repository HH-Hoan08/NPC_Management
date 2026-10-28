using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace NPC.DAL
{
    public class NPCData
    {
        public static SqlConnection Con;
        public static bool IsOK { get; private set; } = false;

        // Cho phép sửa ConnectionString nếu chuỗi mặc định không nối được
        public static string CustomConnectionString { get; set; }

        public static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(CustomConnectionString))
                    return CustomConnectionString;
                return @"Data Source=.;" +
                       @"Initial Catalog=NPCData;" +
                       @"Integrated Security=True;" +
                       @"Connect Timeout=30;";
            }
        }

        public static void Connect()
        {
            try
            {
                if (Con == null)
                    Con = new SqlConnection(ConnectionString);
                else
                    Con.ConnectionString = ConnectionString;

                if (Con.State != ConnectionState.Open)
                {
                    Con.Open();
                }

                IsOK = true;
            }
            catch (Exception ex)
            {
                IsOK = false;
                LogToFile($"[ERROR] Kết nối DB thất bại: {ex.Message}");

                // Ném exception ra ngoài cho Form xử lý tiếp
                throw new Exception("Không thể kết nối đến CSDL. Vui lòng kiểm tra lại cấu hình!", ex);
            }
        }

        public static void Disconnect()
        {
            if (Con != null && Con.State != ConnectionState.Closed)
            {
                Con.Close();
                Con.Dispose();
                Con = null;
            }
            IsOK = false;
        }

        // Hàm ghi log đơn giản ra file text
        private static void LogToFile(string message)
        {
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log");
                string logContent = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(logPath, logContent);
            }
            catch
            {
                // Bỏ qua nếu lỗi ghi log
            }
        }
    }
}
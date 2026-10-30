using System;
using System.Data;
using System.Data.SqlClient;

namespace NPC.DAL
{
    public class NPCData
    {
        public static SqlConnection Con;
        public static bool IsOK { get; private set; } = false;

        public static string ConnectionString
        {
            get
            {
                string connStr = ConfigManager.ReadConnString();
                if (string.IsNullOrEmpty(connStr))
                {
                    Logger.LogError("Lỗi: Chuỗi kết nối CSDL trong config.ini rỗng hoặc không tồn tại!");
                    return string.Empty;
                }
                return connStr;
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
                Logger.LogInfo("Kết nối CSDL thành công.");
            }
            catch (Exception ex)
            {
                IsOK = false;
                Logger.LogError("Kết nối CSDL thất bại!", ex);
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
            Logger.LogInfo("Đã ngắt kết nối CSDL.");
        }
    }
}
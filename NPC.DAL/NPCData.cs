using System;
using System.Data;
using System.Data.SqlClient;

namespace NPC.DAL
{
    /// <summary>
    /// Quản lý chuỗi kết nối và khởi tạo SqlConnection đến CSDL SQL Server.
    /// </summary>
    public static class NPCData
    {
        private static bool _isOK = false;

        public static bool IsOK
        {
            get => _isOK;
            set => _isOK = value;
        }

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

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            string connStr = ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                errorMessage = "Chuỗi kết nối rỗng hoặc không tồn tại trong config.ini.";
                _isOK = false;
                return false;
            }

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    _isOK = true;
                    Logger.LogInfo("Kiểm tra kết nối CSDL thành công.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _isOK = false;
                errorMessage = ex.Message;
                Logger.LogError("Kiểm tra kết nối CSDL thất bại!", ex);
                return false;
            }
        }

        public static void Disconnect()
        {
            _isOK = false;
            Logger.LogInfo("Đã đặt lại trạng thái kết nối CSDL.");
        }
    }
}

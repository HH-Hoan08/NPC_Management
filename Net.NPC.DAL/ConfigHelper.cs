using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace Net.NPC.DAL
{
    public static class ConfigHelper
    {
        #region Constants & Fields / Hằng số & Biến nội bộ
        // Configuration file name / Tên tệp cấu hình
        private static readonly string ConfigFileName = "config.ini";

        // Full path to config file in the application directory / Đường dẫn đầy đủ đến tệp cấu hình trong thư mục ứng dụng
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);

        // Default database name for the project / Tên cơ sở dữ liệu mặc định của đồ án
        private const string DefaultDatabaseName = "Net.NPCDB";

        // List of local SQL Server candidates for auto-detection / Danh sách các máy chủ SQL cục bộ để tự động dò tìm
        private static readonly string[] CandidateServers = new string[]
        {
            @".\SQLEXPRESS",
            ".",
            "localhost",
            "(local)"
        };
        #endregion

        #region Public Methods / Các phương thức công khai
        /// 
        /// Checks if the config.ini file exists / Kiểm tra tệp config.ini có tồn tại hay không
        /// 
        public static bool IsConfigFileExists()
        {
            return File.Exists(ConfigPath);
        }

        /// 
        /// Builds standard ADO.NET connection string / Tạo chuỗi kết nối ADO.NET chuẩn
        /// 
        public static string BuildConnectionString(string server, string database, bool isWindowsAuth, string username = "", string password = "")
        {
            if (isWindowsAuth)
            {
                return $"Server={server};Database={database};Integrated Security=True;";
            }
            else
            {
                return $"Server={server};Database={database};User Id={username};Password={password};";
            }
        }

        /// 
        /// Encodes connection string to Base64 and writes to config.ini / Mã hóa chuỗi kết nối sang Base64 và ghi vào config.ini
        /// 
        public static bool SaveConnectionString(string connectionString)
        {
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(connectionString);
                string base64String = Convert.ToBase64String(plainBytes);

                using (StreamWriter writer = new StreamWriter(ConfigPath, append: false, encoding: Encoding.UTF8))
                {
                    writer.WriteLine(base64String);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"Failed to write config.ini: {ex.Message} | Ghi file config.ini thất bại: {ex.Message}", "System");
                return false;
            }
        }

        /// 
        /// Reads config.ini and decodes Base64 to connection string / Đọc tệp config.ini và giải mã Base64 thành chuỗi kết nối
        /// 
        public static string ReadConnectionString()
        {
            if (!File.Exists(ConfigPath)) return string.Empty;

            try
            {
                string base64Content = string.Empty;
                using (StreamReader reader = new StreamReader(ConfigPath, Encoding.UTF8))
                {
                    base64Content = reader.ReadLine();
                }

                if (string.IsNullOrWhiteSpace(base64Content)) return string.Empty;

                byte[] encodedBytes = Convert.FromBase64String(base64Content.Trim());
                return Encoding.UTF8.GetString(encodedBytes);
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"Failed to read config.ini: {ex.Message} | Đọc file config.ini thất bại: {ex.Message}", "System");
                return string.Empty;
            }
        }

        /// 
        /// Tests if a connection string is valid / Kiểm tra chuỗi kết nối có hợp lệ hay không
        /// 
        public static bool TestConnection(string connectionString, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// 
        /// Automatically attempts to connect using existing config or auto-detects local SQL Server instances.
        /// If successful, auto-saves config.ini without user intervention.
        /// /
        /// Tự động thử kết nối bằng cấu hình hiện có hoặc dò tìm các phiên bản SQL Server cục bộ.
        /// Nếu thành công, tự động lưu lại vào file config.ini mà không cần người dùng can thiệp.
        /// 
        public static bool TryAutoConnect(out string workingConnectionString)
        {
            workingConnectionString = string.Empty;

            // 1. Try saved configuration first if file exists / 1. Thử dùng cấu hình đã lưu trước nếu tệp tồn tại
            if (IsConfigFileExists())
            {
                string savedConnectionString = ReadConnectionString();
                if (!string.IsNullOrEmpty(savedConnectionString) && TestConnection(savedConnectionString, out _))
                {
                    workingConnectionString = savedConnectionString;
                    return true;
                }
            }

            // 2. Iterate through candidate local servers / 2. Lặp qua các máy chủ cục bộ dự phòng để kiểm tra
            foreach (string server in CandidateServers)
            {
                string candidateConnectionString = BuildConnectionString(server, DefaultDatabaseName, isWindowsAuth: true);
                if (TestConnection(candidateConnectionString, out _))
                {
                    SaveConnectionString(candidateConnectionString);
                    Logger.WriteLog($"Auto-detected database on server [{server}]. Configuration saved. | Tự động dò thấy CSDL trên server [{server}]. Đã lưu cấu hình.", "System");
                    workingConnectionString = candidateConnectionString;
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
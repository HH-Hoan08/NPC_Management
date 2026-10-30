using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NPC.DAL
{
    public static class ConfigManager
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
        private const string DefaultConnectionString = @"Data Source=.;" +
                                                       @"Initial Catalog=NPCData;" +
                                                       @"Integrated Security=True;" +
                                                       @"Connect Timeout=30;";

        #region Mã hóa Base64
        public static string EncryptBase64(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecryptBase64(string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData)) return string.Empty;
            try
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch(Exception ex)
            {
                Logger.LogError("Giải mã Base64 chuỗi kết nối thất bại!", ex);
                return string.Empty;
            }
        
        }
        #endregion

        #region Đọc và ghi System.IO
        public static void SaveConnectionString(string raw)
        {
            try
            {
                string encoded = EncryptBase64(raw);
                using (StreamWriter sw = new StreamWriter(ConfigPath, false, Encoding.UTF8))
                {
                    sw.WriteLine("[Database]");
                    sw.WriteLine($"; File cấu hình CSDL (Ma hoa Base64)");
                    sw.WriteLine($"; Cập nhật: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    sw.WriteLine($"ConnectionString={encoded}");
                }
                Logger.LogInfo("Đã lưu chuỗi kết nối CSDL mới vào config.ini.");
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi ghi file cấu hình config.ini", ex);
                throw;
            }
        }
        public static string ReadConnString()
        {
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    SaveConnectionString(DefaultConnectionString);
                    Logger.LogInfo("Khởi tạo file config.ini mặc định thành công.");
                    return DefaultConnectionString;
                }

                string[] lines = File.ReadAllLines(ConfigPath, Encoding.UTF8);
                foreach (string line in lines)
                {
                    string trimmed = line.Trim();
                    // Bỏ qua chú thích hoặc header section [Database]
                    if (trimmed.StartsWith(";") || trimmed.StartsWith("#") || trimmed.StartsWith("["))
                        continue;
                    if (trimmed.StartsWith("ConnectionString=", StringComparison.OrdinalIgnoreCase))
                    {
                        string encodedValue = trimmed.Substring("ConnectionString=".Length).Trim();
                        string decodedConn = DecryptBase64(encodedValue);
                        if (!string.IsNullOrEmpty(decodedConn))
                            return decodedConn;
                    }
                }

                SaveConnectionString(DefaultConnectionString);
                return DefaultConnectionString;
                }
                catch (Exception ex)
                {
                    Logger.LogError("Lỗi khi đọc file cấu hình config.ini", ex);
                    return DefaultConnectionString;
                }
            }
        #endregion
    }
}

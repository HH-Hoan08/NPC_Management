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
        private static readonly string ConfigPath = AppPaths.ConfigPath;
        private const string DefaultConnectionString = @"Data Source=.\SQLEXPRESS;" +
                                                       @"TrustServerCertificate=True;" +
                                                       @"Initial Catalog=NPCData;" +
                                                       @"Integrated Security=True;" +
                                                       @"Connect Timeout=30;";

        #region MÃ£ hÃ³a Base64
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
                Logger.LogError("Giáº£i mÃ£ Base64 chuá»—i káº¿t ná»‘i tháº¥t báº¡i!", ex);
                return string.Empty;
            }
        
        }
        #endregion

        #region Äá»c vÃ  ghi System.IO
        public static void SaveConnectionString(string raw)
        {
            try
            {
                string dir = AppPaths.DebugDirectory;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string encoded = EncryptBase64(raw);
                using (StreamWriter sw = new StreamWriter(ConfigPath, false, Encoding.UTF8))
                {
                    sw.WriteLine("[Database]");
                    sw.WriteLine($"; File cáº¥u hÃ¬nh CSDL (Ma hoa Base64)");
                    sw.WriteLine($"; Cáº­p nháº­t: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    sw.WriteLine($"ConnectionString={encoded}");
                }
                Logger.LogInfo("ÄÃ£ lÆ°u chuá»—i káº¿t ná»‘i CSDL má»›i vÃ o config.ini.");
            }
            catch (Exception ex)
            {
                Logger.LogError("Lá»—i khi ghi file cáº¥u hÃ¬nh config.ini", ex);
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
                    Logger.LogInfo("Khá»Ÿi táº¡o file config.ini máº·c Ä‘á»‹nh thÃ nh cÃ´ng.");
                    return DefaultConnectionString;
                }

                string[] lines = File.ReadAllLines(ConfigPath, Encoding.UTF8);
                foreach (string line in lines)
                {
                    string trimmed = line.Trim();
                    // Bá» qua chÃº thÃ­ch hoáº·c header section [Database]
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
                Logger.LogError("Lá»—i khi Ä‘á»c file cáº¥u hÃ¬nh config.ini", ex);
                return DefaultConnectionString;
            }
        }
        #endregion
    }
}

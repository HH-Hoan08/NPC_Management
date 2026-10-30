using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace NPC.DAL
{
    public static class Logger
    {
        private static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
        private static readonly object LockObj = new object();

        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        public static void LogError(string message, Exception? ex = null)
        {
            string detail = message;
            if (ex != null)
            {
                detail += $"\n   [Exception]: {ex.Message}\n   [StackTrace]: {ex.StackTrace}";
            }
            WriteLog("ERROR", detail);
        }

        public static void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                lock (LockObj)
                {
                    using (StreamWriter sw = new StreamWriter(LogPath, append: true, Encoding.UTF8))
                    {
                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sw.WriteLine($"[{timestamp}] [{level}] {message}");
                    }
                }
            }
            catch { }
        }
    }
}
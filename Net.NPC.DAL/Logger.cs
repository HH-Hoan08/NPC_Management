using System;
using System.IO;
using System.Text;

namespace Net.NPC.DAL
{
    public static class Logger
    {
        #region Constants & Fields / Hằng số & Biến nội bộ
        // Log file name / Tên tệp nhật ký
        private static readonly string LogFileName = "logs.txt";

        // Full path to log file in application directory / Đường dẫn đầy đủ đến tệp nhật ký trong thư mục ứng dụng
        private static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFileName);

        // Lock object for thread-safe file writing / Đối tượng khóa để đồng bộ luồng khi ghi tệp (Thread-safe)
        private static readonly object SyncLock = new object();
        #endregion

        #region Public Methods / Các phương thức công khai
        /// 
        /// Appends an action log entry to logs.txt / Ghi thêm một dòng nhật ký hoạt động vào tệp logs.txt
        /// 
        public static void WriteLog(string action, string performer = "System")
        {
            try
            {
                string logEntry = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - [{performer}]: {action}";

                lock (SyncLock)
                {
                    using (StreamWriter writer = new StreamWriter(LogPath, append: true, encoding: Encoding.UTF8))
                    {
                        writer.WriteLine(logEntry);
                    }
                }
            }
            catch
            {
                // Silently ignore exceptions to prevent crashing the main application flow / Bỏ qua ngoại lệ để không làm gián đoạn luồng chính của ứng dụng
            }
        }
        #endregion
    }
}
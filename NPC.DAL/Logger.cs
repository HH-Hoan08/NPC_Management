using System;
using System.IO;
using System.Text;

namespace NPC.DAL
{
    /// <summary>
    /// Lớp ghi log hệ thống, phân tách thành 2 file riêng biệt:
    /// 1. logs.txt: Nhật ký Nghiệp vụ Hệ thống (hành vi, thao tác của người dùng, thu ngân quán net).
    /// 2. app.log:  Nhật ký Kỹ thuật & Lỗi Ứng dụng (thông tin kỹ thuật, trạng thái low-level, exceptions).
    /// </summary>
    public static class Logger
    {
        private static readonly object AppLogLock = new object();
        private static readonly object BusinessLogLock = new object();

        #region 1. Nhật ký Kỹ thuật & Lỗi Ứng dụng -> app.log
        /// <summary>
        /// Ghi log thông tin kỹ thuật (kết nối CSDL, trạng thái hệ thống, cấu hình...) vào app.log
        /// </summary>
        public static void LogInfo(string message)
        {
            WriteAppLog("INFO", message);
        }

        /// <summary>
        /// Ghi log cảnh báo kỹ thuật vào app.log
        /// </summary>
        public static void LogWarning(string message)
        {
            WriteAppLog("WARN", message);
        }

        /// <summary>
        /// Ghi log lỗi ngoại lệ (Exception) kỹ thuật vào app.log
        /// </summary>
        public static void LogError(string message, Exception? ex = null)
        {
            string detail = message;
            if (ex != null)
            {
                detail += $"\n   [Exception]: {ex.Message}\n   [StackTrace]: {ex.StackTrace}";
            }
            WriteAppLog("ERROR", detail);
        }

        public static void LogError(string message)
        {
            WriteAppLog("ERROR", message);
        }

        private static void WriteAppLog(string level, string message)
        {
            try
            {
                lock (AppLogLock)
                {
                    EnsureDirectoryExists();
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string logLine = $"[{timestamp}] [{level}] {message}";

                    using (StreamWriter sw = new StreamWriter(AppPaths.AppLogPath, append: true, Encoding.UTF8))
                    {
                        sw.WriteLine(logLine);
                    }
                }
            }
            catch { }
        }
        #endregion

        #region 2. Nhật ký Nghiệp vụ Hệ thống -> logs.txt
        /// <summary>
        /// Ghi lại hành vi, thao tác nghiệp vụ của người dùng / thu ngân vào logs.txt
        /// </summary>
        /// <param name="message">Nội dung thao tác nghiệp vụ</param>
        public static void LogBusiness(string message)
        {
            WriteBusinessLog(message);
        }

        /// <summary>
        /// Ghi lại chi tiết thao tác nghiệp vụ của nhân viên / hội viên vào logs.txt
        /// </summary>
        /// <param name="user">Tài khoản thực hiện (VD: Thu ngân 'admin', Hội viên 'user01')</param>
        /// <param name="action">Tên hành động (VD: 'Nạp tiền', 'Đăng nhập', 'Gọi món', 'Mở máy')</param>
        /// <param name="details">Chi tiết bổ sung (VD: 'Nạp 50,000 VND cho máy 05')</param>
        public static void LogBusiness(string user, string action, string details = "")
        {
            string msg = string.IsNullOrWhiteSpace(details)
                ? $"[Người dùng: {user}] - Hành vi: {action}"
                : $"[Người dùng: {user}] - Hành vi: {action} | Chi tiết: {details}";

            WriteBusinessLog(msg);
        }

        /// <summary>
        /// Alias tiếng Việt cho LogBusiness
        /// </summary>
        public static void LogNghiepVu(string message)
        {
            LogBusiness(message);
        }

        public static void LogNghiepVu(string user, string action, string details = "")
        {
            LogBusiness(user, action, details);
        }

        private static void WriteBusinessLog(string message)
        {
            try
            {
                lock (BusinessLogLock)
                {
                    EnsureDirectoryExists();
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string logLine = $"[{timestamp}] [Log] {message}";

                    using (StreamWriter sw = new StreamWriter(AppPaths.LogPath, append: true, Encoding.UTF8))
                    {
                        sw.WriteLine(logLine);
                    }
                }
            }
            catch { }
        }
        #endregion

        private static void EnsureDirectoryExists()
        {
            string dir = AppPaths.DebugDirectory;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}

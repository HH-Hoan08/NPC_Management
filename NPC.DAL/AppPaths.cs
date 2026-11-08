using System;
using System.IO;

namespace NPC.DAL
{
    public static class AppPaths
    {
        private static string? _debugDirectory;

        public static string DebugDirectory
        {
            get
            {
                if (_debugDirectory != null && Directory.Exists(_debugDirectory))
                    return _debugDirectory;

                try
                {
                    var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    DirectoryInfo? dir = currentDir;
                    while (dir != null)
                    {
                        if (dir.GetFiles("*.slnx").Length > 0 ||
                            dir.GetFiles("*.sln").Length > 0 ||
                            dir.Name.Equals("NPC_Management", StringComparison.OrdinalIgnoreCase))
                        {
                            string target = Path.Combine(dir.FullName, "debug");
                            if (!Directory.Exists(target))
                            {
                                Directory.CreateDirectory(target);
                            }
                            _debugDirectory = target;
                            return _debugDirectory;
                        }
                        dir = dir.Parent;
                    }
                }
                catch
                {
                    // Fallback nếu có lỗi phân quyền hoặc môi trường đặc biệt
                }

                string fallback = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "debug");
                if (!Directory.Exists(fallback))
                {
                    Directory.CreateDirectory(fallback);
                }
                _debugDirectory = fallback;
                return _debugDirectory;
            }
        }

        public static string ConfigPath => Path.Combine(DebugDirectory, "config.ini");
        public static string LogPath => Path.Combine(DebugDirectory, "logs.txt");
        public static string AppLogPath => Path.Combine(DebugDirectory, "app.log");

        /// <summary>
        /// Thư mục Resources dùng chung cho cả Server và Client (nằm cạnh thư mục debug, ở gốc solution).
        /// </summary>
        public static string ResourceDirectory
        {
            get
            {
                string root = Directory.GetParent(DebugDirectory)?.FullName ?? AppDomain.CurrentDomain.BaseDirectory;
                string dir = Path.Combine(root, "Resources");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                return dir;
            }
        }

        /// <summary>Thư mục lưu ảnh món ăn: Resources/Images/Products.</summary>
        public static string ProductImageDirectory
        {
            get
            {
                string dir = Path.Combine(ResourceDirectory, "Images", "Products");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                return dir;
            }
        }

        /// <summary>
        /// Chuyển đường dẫn ảnh lưu trong CSDL (tương đối, vd "Images/Products/abc.png") thành đường dẫn tuyệt đối.
        /// Tìm lần lượt trong: Resources dùng chung → thư mục chạy app → thư mục chạy app/Resources.
        /// Trả về null nếu không tìm thấy file.
        /// </summary>
        public static string? ResolveImagePath(string? duongDan)
        {
            if (string.IsNullOrWhiteSpace(duongDan)) return null;

            string rel = duongDan.Trim().Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            if (Path.IsPathRooted(rel)) return File.Exists(rel) ? rel : null;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] ungVien =
            {
                Path.Combine(ResourceDirectory, rel),
                Path.Combine(Directory.GetParent(ResourceDirectory)?.FullName ?? baseDir, rel),
                Path.Combine(baseDir, rel),
                Path.Combine(baseDir, "Resources", rel)
            };

            foreach (string p in ungVien)
                if (File.Exists(p)) return p;

            return null;
        }
    }
}


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
    }
}


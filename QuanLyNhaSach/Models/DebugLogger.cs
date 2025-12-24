using System;
using System.IO;

namespace QuanLyNhaSach.Models
{
    public static class DebugLogger
    {
        private static readonly string LogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuanLyNhaSach");
        private static readonly string LogFile = Path.Combine(LogDir, "debug.log");

        public static void Log(string message)
        {
            try
            {
                if (!Directory.Exists(LogDir)) Directory.CreateDirectory(LogDir);
                File.AppendAllText(LogFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message + Environment.NewLine);
            }
            catch
            {
                // ignore logging errors
            }
        }
    }
}

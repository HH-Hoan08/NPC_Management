using System;
using System.Windows.Forms;
using Net.NPC.DAL;

namespace Net.NPC.Server
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Tự động kết nối database
            if (ConfigHelper.TryAutoConnect(out string activeConnectionString))
            {
                Logger.WriteLog("Application auto-connected to database successfully.", "System");

                // Kết nối thẳng vào màn hình Đăng nhập (hoặc Form chính của bạn)
                Application.Run(new frmCauHinhKetNoi());
                return;
            }

            // 2. Chỉ hiển thị Form cấu hình khi không tự kết nối được
            MessageBox.Show(
                "Không thể tự động kết nối đến CSDL [Net.NPCDB].\nVui lòng kiểm tra hoặc nhập thông tin kết nối thủ công!",
                "Thông báo kết nối",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            using (frmCauHinhKetNoi configForm = new frmCauHinhKetNoi())
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmCauHinhKetNoi());
                }
            }
        }
    }
}
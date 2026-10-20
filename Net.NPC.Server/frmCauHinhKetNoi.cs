using System;
using System.Windows.Forms;
using Net.NPC.DAL;

namespace Net.NPC.Server
{
    public partial class frmCauHinhKetNoi : Form
    {
        #region Constructor
        public frmCauHinhKetNoi()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void frmCauHinhKetNoi_Load(object sender, EventArgs e)
        {
            cboXacThuc.Items.Clear();
            cboXacThuc.Items.Add("Windows Authentication");
            cboXacThuc.Items.Add("SQL Server Authentication");
            cboXacThuc.SelectedIndex = 0;

            txtTenCSDL.Text = "CyberCafeManagement";
            txtTenServer.Text = @"localhost";
        }

        private void cboXacThuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isSqlAuth = cboXacThuc.SelectedIndex == 1;
            txtUserSQL.Enabled = isSqlAuth;
            txtPassSQL.Enabled = isSqlAuth;

            if (!isSqlAuth)
            {
                txtUserSQL.Clear();
                txtPassSQL.Clear();
            }
        }

        private void btnKiemTraKetNoi_Click(object sender, EventArgs e)
        {
            string serverName = txtTenServer.Text.Trim();
            string databaseName = txtTenCSDL.Text.Trim();

            if (string.IsNullOrWhiteSpace(serverName) || string.IsNullOrWhiteSpace(databaseName))
            {
                MessageBox.Show("Vui lòng nhập Tên Server và Tên CSDL!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isWindowsAuth = cboXacThuc.SelectedIndex == 0;
            string connectionString = ConfigHelper.BuildConnectionString(serverName, databaseName, isWindowsAuth, txtUserSQL.Text.Trim(), txtPassSQL.Text.Trim());

            if (ConfigHelper.TestConnection(connectionString, out string errorMessage))
            {
                MessageBox.Show("Kết nối CSDL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Kết nối thất bại!\nLỗi: {errorMessage}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuuCauHinh_Click(object sender, EventArgs e)
        {
            string serverName = txtTenServer.Text.Trim();
            string databaseName = txtTenCSDL.Text.Trim();
            bool isWindowsAuth = cboXacThuc.SelectedIndex == 0;

            string connectionString = ConfigHelper.BuildConnectionString(serverName, databaseName, isWindowsAuth, txtUserSQL.Text.Trim(), txtPassSQL.Text.Trim());

            if (!ConfigHelper.TestConnection(connectionString, out string errorMessage))
            {
                DialogResult confirmation = MessageBox.Show(
                    $"Chuỗi kết nối chưa thông!\nLỗi: {errorMessage}\nBạn có chắc chắn muốn lưu cấu hình này không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmation != DialogResult.Yes) return;
            }

            if (ConfigHelper.SaveConnectionString(connectionString))
            {
                Logger.WriteLog("Database configuration saved manually via Form.", "Admin");
                MessageBox.Show("Đã lưu cấu hình kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Không thể lưu cấu hình. Vui lòng kiểm tra quyền ghi thư mục!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
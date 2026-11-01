using NPC.BLL;
using NPC.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NPC.Server
{
    public partial class frmDangNhapServer : Form
    {
        public static string CurrentUser { get; set; } = string.Empty;

        public frmDangNhapServer()
        {
            InitializeComponent();
            FormScaler.EnableAutoScaling(this);
        }

        private void frmDangNhapServer_Load(object sender, EventArgs e)
        {
            picLoading.Parent = picNen;

            IsDatabaseConn();
            picLoading.Visible = false;
        }

        private bool IsDatabaseConn()
        {
            var ketQua = HeThongBLL.KiemTraKetNoiCSDL();
            if (!ketQua.ThanhCong)
            {
                MessageBox.Show($"{ketQua.ThongBao}\nChi tiết lỗi đã được ghi vào file log.",
                                "Lỗi kết nối CSDL",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            picLoading.Visible = true;
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            DangNhapResultDTO ketQua = NhanVienBLL.DangNhap(tenDangNhap, matKhau);

            if (ketQua.ThanhCong)
            {
                CurrentUser = ketQua.NhanVien?.HoTen ?? tenDangNhap;
                Hide();
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMainServer fm = new frmMainServer();
                fm.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Text = "";
                txtMatKhau.Focus();
            }
            picLoading.Visible = false;
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMatKhau.Focus();
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnConfirm.PerformClick();
            }
        }

        #region non-use
        private void lblTen_Click(object sender, EventArgs e) { }
        private void lblMatKhau_Click(object sender, EventArgs e) { }
        private void txtTenDangNhap_TextChanged(object sender, EventArgs e) { }
        private void txtMatKhau_TextChanged(object sender, EventArgs e) { }
        private void picLoading_Click(object sender, EventArgs e) { }
        private void picNen_Click(object sender, EventArgs e) { }
        #endregion
    }
}

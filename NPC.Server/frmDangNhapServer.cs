using NPC.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BCrypt.Net;
namespace NPC.Server
{
    public partial class frmDangNhapServer : Form
    {
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
            try
            {
                // Thử kết nối CSDL
                NPCData.Connect();
                // Nếu kết nối thành công
                if (NPCData.IsOK)
                {

                    MessageBox.Show("Đăng nhập vào CSDL thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nChi tiết lỗi đã được ghi vào file log.",
                                "Lỗi kết nối CSDL",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            return false;
        }

        //Check Login
        protected bool Login(string tenDangNhap, string matkhau)
        {
            string getMatKhauQuery = "SELECT MatKhau FROM NhanVien WHERE TenDangNhap = @Username";
            object getMatKhau = SQLHelper.ExecuteScalar(getMatKhauQuery, new SqlParameter("@Username", tenDangNhap));

            if (getMatKhau != null && getMatKhau != DBNull.Value)
            {
                string getStringMatKhau = getMatKhau.ToString();
                string getHoTenQuery =  "SELECT HoTen FROM NhanVien WHERE TenDangNhap = @Username";
                string getHoTen = SQLHelper.ExecuteScalar(getHoTenQuery, new SqlParameter("@Username", tenDangNhap)).ToString();

                Logger.LogBusiness(getHoTen, "Đăng nhập vào quản lý máy thành công!");
                string storedHash = BCrypt.Net.BCrypt.HashPassword(getStringMatKhau.ToString());
                return BCrypt.Net.BCrypt.Verify(matkhau, storedHash);
            }
            return false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            picLoading.Visible = true;
            string tenDangNhap = txtTenDangNhap.Text;
            string MatKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(MatKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (Login(tenDangNhap, MatKhau))
            {
                Hide();
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                NPCData.Disconnect();
            

                frmMainServer fm = new frmMainServer();
                fm.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDangNhap.Text = "";
                txtMatKhau.Text = "";
                txtTenDangNhap.Focus();
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
        private void picNen_Click(object sender, EventArgs e){ }
        #endregion
    }
}
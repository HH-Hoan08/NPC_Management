using NPC.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BCrypt.Net;

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
            try
            {
                // Thử kết nối CSDL
                NPCData.Connect();
                if (NPCData.IsOK)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi kết nối CSDL tại frmDangNhapServer:", ex);
                MessageBox.Show($"{ex.Message}\nChi tiết lỗi đã được ghi vào file log.",
                                "Lỗi kết nối CSDL",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            return false;
        }

        // Kiểm tra đăng nhập
        protected bool Login(string tenDangNhap, string matKhau)
        {
            try
            {
                // kết nối CSDL
                if (!NPCData.IsOK)
                {
                    NPCData.Connect();
                    if (!NPCData.IsOK)
                    {
                        MessageBox.Show("Chưa thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra lại cấu hình!",
                            "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                string query = "SELECT MatKhau, HoTen, TrangThai FROM NhanVien WHERE TenDangNhap = @Username";
                DataTable dt = SQLHelper.ExecuteQuery(query, new SqlParameter("@Username", tenDangNhap));

                if (dt == null || dt.Rows.Count == 0)
                {
                    Logger.LogWarning($"Đăng nhập thất bại: Tài khoản '{tenDangNhap}' không tồn tại trong hệ thống.");
                    return false;
                }

                DataRow row = dt.Rows[0];

                // Kiểm tra trạng thái tài khoản
                bool trangThai = row["TrangThai"] != DBNull.Value && Convert.ToBoolean(row["TrangThai"]);
                if (!trangThai)
                {
                    MessageBox.Show("Tài khoản này hiện đang bị khóa! Vui lòng liên hệ quản trị viên.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logger.LogWarning($"Tài khoản '{tenDangNhap}' đã bị khóa nhưng cố gắng đăng nhập.");
                    return false;
                }

                string hashFromDb = row["MatKhau"] != DBNull.Value ? row["MatKhau"].ToString()!.Trim() : string.Empty;
                string hoTen = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString()!.Trim() : tenDangNhap;
                string cleanPassword = matKhau.Trim();

                bool isPasswordValid = false;

                // Kiểm tra nếu mật khẩu != Hash (restuff)
                if (hashFromDb.StartsWith("$2a$") || hashFromDb.StartsWith("$2b$") || hashFromDb.StartsWith("$2y$"))
                {
                    try
                    {
                        isPasswordValid = BCrypt.Net.BCrypt.Verify(cleanPassword, hashFromDb);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Lỗi khi kiểm tra mã băm BCrypt cho tài khoản '{tenDangNhap}': {ex.Message}", ex);
                    }
                }

                // Hỗ trợ tự động: Nếu DB còn lưu mật khẩu dạng thô (plain-text) chưa băm
                if (!isPasswordValid && hashFromDb == cleanPassword)
                {
                    isPasswordValid = true;

                    // Tự động băm BCrypt và cập nhật ngược lại CSDL
                    try
                    {
                        string autoHashed = BCrypt.Net.BCrypt.HashPassword(cleanPassword, 11);
                        SQLHelper.ExecuteNonQuery("UPDATE NhanVien SET MatKhau = @Hashed WHERE TenDangNhap = @Username",
                            new SqlParameter("@Hashed", autoHashed),
                            new SqlParameter("@Username", tenDangNhap));
                        Logger.LogInfo($"Đã tự động băm BCrypt và cập nhật mật khẩu mới cho tài khoản '{tenDangNhap}'.");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Lỗi khi tự động cập nhật mã băm mật khẩu cho '{tenDangNhap}':", ex);
                    }
                }

                if (isPasswordValid)
                {
                    CurrentUser = hoTen;
                    Logger.LogBusiness(CurrentUser, "Đăng nhập", "Đăng nhập hệ thống Server thành công.");
                    return true;
                }
                else
                {
                    Logger.LogWarning($"Đăng nhập thất bại: Sai mật khẩu cho tài khoản '{tenDangNhap}'.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi ngoại lệ trong quá trình đăng nhập của '{tenDangNhap}':", ex);
                MessageBox.Show($"Lỗi trong quá trình đăng nhập: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            picLoading.Visible = true;
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                picLoading.Visible = false;
                return;
            }

            if (Login(tenDangNhap, matKhau))
            {
                Hide();
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

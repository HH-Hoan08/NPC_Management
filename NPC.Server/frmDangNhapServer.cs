using System;
using System.Windows.Forms;
using NPC.DAL;

namespace NPC.Server
{
    public partial class frmDangNhapServer : Form
    {
        public frmDangNhapServer()
        {
            InitializeComponent();
        }

        private void frmDangNhapServer_Load(object sender, EventArgs e)
        {
            IsDatabaseConn();
        }

        private void IsDatabaseConn()
        {
            try
            {
                // Thử kết nối CSDL
                NPCData.Connect();

                // Nếu kết nối thành công
                if (NPCData.IsOK)
                {
                    MessageBox.Show("Kết nối CSDL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nChi tiết lỗi đã được ghi vào file log.",
                                "Lỗi kết nối CSDL",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                OpenConfigDatabaseForm();
            }
        }

        private void OpenConfigDatabaseForm()
        {
            // Mở form cấu hình chuỗi kết nối dưới dạng Dialog (Giả sử tên form là frmCauHinhDB)
            // using (frmCauHinhDB frm = new frmCauHinhDB())
            // {
            //     if (frm.ShowDialog() == DialogResult.OK)
            //     {
            //         // Sau khi người dùng điền và lưu ConnectionString mới, thử kết nối lại
            //         KiemTraKetNoiDatabase();
            //     }
            //     else
            //     {
            //         // Nếu người dùng đóng form cấu hình mà không sửa -> Thoát ứng dụng
            //         Application.Exit();
            //     }
            // }
        }

        #region non-Label
        private void lblTen_Click(object sender, EventArgs e) { }
        private void lblMatKhau_Click(object sender, EventArgs e) { }
        #endregion

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e) { }
        private void txtMatKhau_TextChanged(object sender, EventArgs e) { }
        private void picLoading_Click(object sender, EventArgs e) { }
    }
}
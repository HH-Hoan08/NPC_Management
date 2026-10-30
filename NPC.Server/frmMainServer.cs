using NPC.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPC.Server
{
    public partial class frmMainServer : Form
    {
        public frmMainServer()
        {
            InitializeComponent();
        }

        private void OpenMdiChildForm<T>() where T : Form, new()
        {
            T checkForm = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (checkForm != null)
            {
                checkForm.WindowState = FormWindowState.Maximized;
                checkForm.BringToFront();
                checkForm.Activate();
                return;
            }
            foreach (Form child in this.MdiChildren.ToList())
            {
                child.Close();
            }
            T fm = new T();
            fm.MdiParent = this; 

            fm.WindowState = FormWindowState.Maximized;

            fm.Show();
        }

        private void frmMainServer_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "select * from tblMay";
        }

        private void tsmiThoat_Click(object sender, EventArgs e)
        {
            string user = !string.IsNullOrEmpty(frmDangNhapServer.CurrentUser) ? frmDangNhapServer.CurrentUser : "admin";
            Logger.LogBusiness(user, "Thoát hệ thống", "Người dùng đã đóng ứng dụng Server.");
            Application.Exit();
        }

        private void tsmiDangXuat_Click(object sender, EventArgs e)
        {
            string user = !string.IsNullOrEmpty(frmDangNhapServer.CurrentUser) ? frmDangNhapServer.CurrentUser : "admin";
            Logger.LogBusiness(user, "Đăng xuất", "Đăng xuất khỏi hệ thống Server.");

            Hide();
            NPCData.Disconnect();
            frmDangNhapServer fm = new frmDangNhapServer();
            fm.ShowDialog();
            Close();
        }

        private void tsmiHoiVien_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmQuanLyHoiVien>();
        }

        private void tsmiMonAn_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmQuanLyMonAn>();
        }

        private void tsmiCaiDatGiaoDien_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmCaiDatGiaoDien>();
        }
    }
}

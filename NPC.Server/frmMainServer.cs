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
            FormScaler.EnableAutoScaling(this);
        }

        private void OpenMdiChildForm<T>() where T : Form, new()
        {
            // Kiểm tra xem Form đã mở chưa
            T checkForm = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (checkForm != null)
            {
                checkForm.BringToFront();
                checkForm.Activate();
            }
            else
            {
                T fm = new T();
                fm.MdiParent = this; // Gán Form cha
                fm.WindowState = FormWindowState.Maximized; // Phóng to tối đa phủ kín Form cha
                fm.Show();
            }
        }

        private void frmMainServer_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "select * from tblMay";
        }
        private void tsmiThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiDangXuat_Click(object sender, EventArgs e)
        {
            Hide();
            NPCData.Disconnect();
            frmDangNhapServer fm = new frmDangNhapServer();
            fm.ShowDialog();
            Close();
        }

        private void tsmiCaiDatGiaoDien_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmCaiDatGiaoDien>();
        }

        private void tsmiHoiVien_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmQuanLyHoiVien>();
        }

        private void tsmiMonAn_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmQuanLyMonAn>();
        }
    }
}

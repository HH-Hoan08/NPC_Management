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

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}

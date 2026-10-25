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
        private Size originalFormSize;
        private Dictionary<Control, (Rectangle Bounds, float FontSize)> originalControls = new Dictionary<Control, (Rectangle, float)>();

        public frmMainServer()
        {
            InitializeComponent();
        }
        private void frmMainServer_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "select * from tblMay";
            originalFormSize = this.ClientSize;
            SaveOriginalControlBounds(this);
        }
        #region Scale Windows
        private void SaveOriginalControlBounds(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (!originalControls.ContainsKey(c))
                {
                    originalControls.Add(c, (new Rectangle(c.Location, c.Size), c.Font.Size));
                }
                if (c.HasChildren)
                {
                    SaveOriginalControlBounds(c);
                }
            }
        }

        private void frmMainServer_Resize(object sender, EventArgs e)
        {
            if (originalFormSize.Width == 0 || originalFormSize.Height == 0) return;

            // Tính tỷ lệ co giãn X và Y
            float scaleX = (float)this.ClientSize.Width / originalFormSize.Width;
            float scaleY = (float)this.ClientSize.Height / originalFormSize.Height;
            float scaleFont = Math.Min(scaleX, scaleY); // Dùng tỉ lệ nhỏ hơn để font không bị vỡ

            // Tự động scale vị trí, kích thước và cỡ chữ của TẤT CẢ control
            foreach (var kvp in originalControls)
            {
                Control c = kvp.Key;
                Rectangle origBounds = kvp.Value.Bounds;
                float origFontSize = kvp.Value.FontSize;

                int newX = (int)(origBounds.X * scaleX);
                int newY = (int)(origBounds.Y * scaleY);
                int newWidth = (int)(origBounds.Width * scaleX);
                int newHeight = (int)(origBounds.Height * scaleY);

                c.Location = new Point(newX, newY);
                c.Size = new Size(newWidth, newHeight);

                // Scale Font chữ
                if (origFontSize > 0)
                {
                    c.Font = new Font(c.Font.FontFamily, origFontSize * scaleFont, c.Font.Style);
                }
            }
        }
        #endregion
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

        }
    }
}

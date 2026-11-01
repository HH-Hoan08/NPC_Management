namespace NPC.Server
{
    partial class ucMayTram
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlHeader = new Panel();
            lblLoaiPhong = new Label();
            lblTenMay = new Label();
            picMayTram = new PictureBox();
            lblTrangThai = new Label();
            lblNguoiDung = new Label();
            lblThoiGian = new Label();
            lblSoDu = new Label();
            cmsMayTram = new ContextMenuStrip(components);
            tsmiMoMay = new ToolStripMenuItem();
            tsmiMoKhachVangLai = new ToolStripMenuItem();
            tsmiMoHoiVien = new ToolStripMenuItem();
            tsmiKhoaMay = new ToolStripMenuItem();
            tsmiNapTien = new ToolStripMenuItem();
            tsmiThanhToan = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            tsmiChuyenMay = new ToolStripMenuItem();
            tsmiBaoTri = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMayTram).BeginInit();
            cmsMayTram.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblLoaiPhong);
            pnlHeader.Controls.Add(lblTenMay);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(160, 32);
            pnlHeader.TabIndex = 0;
            // 
            // lblLoaiPhong
            // 
            lblLoaiPhong.Dock = DockStyle.Right;
            lblLoaiPhong.Font = new Font("Segoe UI", 8F);
            lblLoaiPhong.ForeColor = Color.White;
            lblLoaiPhong.Location = new Point(75, 0);
            lblLoaiPhong.Name = "lblLoaiPhong";
            lblLoaiPhong.Padding = new Padding(0, 0, 5, 0);
            lblLoaiPhong.Size = new Size(85, 32);
            lblLoaiPhong.TabIndex = 1;
            lblLoaiPhong.Text = "VIP";
            lblLoaiPhong.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTenMay
            // 
            lblTenMay.Dock = DockStyle.Left;
            lblTenMay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTenMay.ForeColor = Color.White;
            lblTenMay.Location = new Point(0, 0);
            lblTenMay.Name = "lblTenMay";
            lblTenMay.Padding = new Padding(5, 0, 0, 0);
            lblTenMay.Size = new Size(75, 32);
            lblTenMay.TabIndex = 0;
            lblTenMay.Text = "MÁY 01";
            lblTenMay.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picMayTram
            // 
            picMayTram.Location = new Point(55, 38);
            picMayTram.Name = "picMayTram";
            picMayTram.Size = new Size(50, 45);
            picMayTram.SizeMode = PictureBoxSizeMode.Zoom;
            picMayTram.TabIndex = 1;
            picMayTram.TabStop = false;
            // 
            // lblTrangThai
            // 
            lblTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTrangThai.Location = new Point(5, 88);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(150, 20);
            lblTrangThai.TabIndex = 2;
            lblTrangThai.Text = "Đang sử dụng";
            lblTrangThai.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNguoiDung
            // 
            lblNguoiDung.Font = new Font("Segoe UI", 8.5F);
            lblNguoiDung.ForeColor = Color.DimGray;
            lblNguoiDung.Location = new Point(5, 110);
            lblNguoiDung.Name = "lblNguoiDung";
            lblNguoiDung.Size = new Size(150, 20);
            lblNguoiDung.TabIndex = 3;
            lblNguoiDung.Text = "Nguyen Van A";
            lblNguoiDung.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblThoiGian
            // 
            lblThoiGian.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblThoiGian.ForeColor = Color.DarkSlateGray;
            lblThoiGian.Location = new Point(5, 132);
            lblThoiGian.Name = "lblThoiGian";
            lblThoiGian.Size = new Size(150, 20);
            lblThoiGian.TabIndex = 4;
            lblThoiGian.Text = "00:00:00";
            lblThoiGian.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSoDu
            // 
            lblSoDu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSoDu.ForeColor = Color.Firebrick;
            lblSoDu.Location = new Point(5, 154);
            lblSoDu.Name = "lblSoDu";
            lblSoDu.Size = new Size(150, 22);
            lblSoDu.TabIndex = 5;
            lblSoDu.Text = "20.000 đ";
            lblSoDu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmsMayTram
            // 
            cmsMayTram.ImageScalingSize = new Size(19, 19);
            cmsMayTram.Items.AddRange(new ToolStripItem[] { tsmiMoMay, tsmiKhoaMay, tsmiNapTien, tsmiThanhToan, toolStripMenuItem1, tsmiChuyenMay, tsmiBaoTri, toolStripMenuItem2 });
            cmsMayTram.Name = "cmsMayTram";
            cmsMayTram.Size = new Size(211, 160);
            // 
            // tsmiMoMay
            // 
            tsmiMoMay.DropDownItems.AddRange(new ToolStripItem[] { tsmiMoKhachVangLai, tsmiMoHoiVien });
            tsmiMoMay.Name = "tsmiMoMay";
            tsmiMoMay.Size = new Size(210, 24);
            tsmiMoMay.Text = "Mở máy";
            // 
            // tsmiMoKhachVangLai
            // 
            tsmiMoKhachVangLai.Name = "tsmiMoKhachVangLai";
            tsmiMoKhachVangLai.Size = new Size(175, 26);
            tsmiMoKhachVangLai.Text = "Khách vãng lai";
            // 
            // tsmiMoHoiVien
            // 
            tsmiMoHoiVien.Name = "tsmiMoHoiVien";
            tsmiMoHoiVien.Size = new Size(175, 26);
            tsmiMoHoiVien.Text = "Hội viên";
            // 
            // tsmiKhoaMay
            // 
            tsmiKhoaMay.Name = "tsmiKhoaMay";
            tsmiKhoaMay.Size = new Size(210, 24);
            tsmiKhoaMay.Text = "Khóa máy";
            // 
            // tsmiNapTien
            // 
            tsmiNapTien.Name = "tsmiNapTien";
            tsmiNapTien.Size = new Size(210, 24);
            tsmiNapTien.Text = "Nạp tiền";
            // 
            // tsmiThanhToan
            // 
            tsmiThanhToan.Name = "tsmiThanhToan";
            tsmiThanhToan.Size = new Size(210, 24);
            tsmiThanhToan.Text = "Thanh toán/Trả máy";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(207, 6);
            // 
            // tsmiChuyenMay
            // 
            tsmiChuyenMay.Name = "tsmiChuyenMay";
            tsmiChuyenMay.Size = new Size(210, 24);
            tsmiChuyenMay.Text = "Chuyển máy";
            // 
            // tsmiBaoTri
            // 
            tsmiBaoTri.Name = "tsmiBaoTri";
            tsmiBaoTri.Size = new Size(210, 24);
            tsmiBaoTri.Text = "Bảo trì/Bỏ bảo trì";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(207, 6);
            // 
            // ucMayTram
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            ContextMenuStrip = cmsMayTram;
            Controls.Add(lblSoDu);
            Controls.Add(lblThoiGian);
            Controls.Add(lblNguoiDung);
            Controls.Add(lblTrangThai);
            Controls.Add(picMayTram);
            Controls.Add(pnlHeader);
            Cursor = Cursors.Hand;
            Margin = new Padding(6);
            Name = "ucMayTram";
            Size = new Size(160, 185);
            pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picMayTram).EndInit();
            cmsMayTram.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTenMay;
        private Label lblLoaiPhong;
        private PictureBox picMayTram;
        private Label lblTrangThai;
        private Label lblNguoiDung;
        private Label lblThoiGian;
        private Label lblSoDu;
        private ContextMenuStrip cmsMayTram;
        private ToolStripMenuItem tsmiMoMay;
        private ToolStripMenuItem tsmiMoKhachVangLai;
        private ToolStripMenuItem tsmiMoHoiVien;
        private ToolStripMenuItem tsmiKhoaMay;
        private ToolStripMenuItem tsmiNapTien;
        private ToolStripMenuItem tsmiThanhToan;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem tsmiChuyenMay;
        private ToolStripMenuItem tsmiBaoTri;
        private ToolStripSeparator toolStripMenuItem2;
    }
}

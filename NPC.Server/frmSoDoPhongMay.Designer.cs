namespace NPC.Server
{
    partial class frmSoDoPhongMay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlTopFilter = new Panel();
            btnLamMoi = new Button();
            lblBaoTri = new Label();
            lblMayTrong = new Label();
            lblDangOnline = new Label();
            lblTongSoMay = new Label();
            cboTrangThai = new ComboBox();
            lblTrangThai = new Label();
            lblKhuVuc = new Label();
            cboKhuVuc = new ComboBox();
            cmsMayTram = new ContextMenuStrip(components);
            tsmiMoMay = new ToolStripMenuItem();
            tsmiMoKhach = new ToolStripMenuItem();
            tsmiMoHoiVien = new ToolStripMenuItem();
            tsmiKhoaMay = new ToolStripMenuItem();
            tsmiThanhToan = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            tsmiChuyenMay = new ToolStripMenuItem();
            tsmiBaoTri = new ToolStripMenuItem();
            flpDanhSachMay = new FlowLayoutPanel();
            pnlTopFilter.SuspendLayout();
            cmsMayTram.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopFilter
            // 
            pnlTopFilter.Controls.Add(btnLamMoi);
            pnlTopFilter.Controls.Add(lblBaoTri);
            pnlTopFilter.Controls.Add(lblMayTrong);
            pnlTopFilter.Controls.Add(lblDangOnline);
            pnlTopFilter.Controls.Add(lblTongSoMay);
            pnlTopFilter.Controls.Add(cboTrangThai);
            pnlTopFilter.Controls.Add(lblTrangThai);
            pnlTopFilter.Controls.Add(lblKhuVuc);
            pnlTopFilter.Controls.Add(cboKhuVuc);
            pnlTopFilter.Dock = DockStyle.Top;
            pnlTopFilter.Location = new Point(0, 0);
            pnlTopFilter.Name = "pnlTopFilter";
            pnlTopFilter.Size = new Size(889, 100);
            pnlTopFilter.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(712, 25);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(140, 41);
            btnLamMoi.TabIndex = 8;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblBaoTri
            // 
            lblBaoTri.AutoSize = true;
            lblBaoTri.Location = new Point(551, 67);
            lblBaoTri.Name = "lblBaoTri";
            lblBaoTri.Size = new Size(53, 20);
            lblBaoTri.TabIndex = 7;
            lblBaoTri.Text = "Bảo trì";
            // 
            // lblMayTrong
            // 
            lblMayTrong.AutoSize = true;
            lblMayTrong.Location = new Point(551, 9);
            lblMayTrong.Name = "lblMayTrong";
            lblMayTrong.Size = new Size(47, 20);
            lblMayTrong.TabIndex = 6;
            lblMayTrong.Text = "Trống";
            // 
            // lblDangOnline
            // 
            lblDangOnline.AutoSize = true;
            lblDangOnline.Location = new Point(391, 67);
            lblDangOnline.Name = "lblDangOnline";
            lblDangOnline.Size = new Size(52, 20);
            lblDangOnline.TabIndex = 5;
            lblDangOnline.Text = "Online";
            // 
            // lblTongSoMay
            // 
            lblTongSoMay.AutoSize = true;
            lblTongSoMay.Location = new Point(391, 9);
            lblTongSoMay.Name = "lblTongSoMay";
            lblTongSoMay.Size = new Size(43, 20);
            lblTongSoMay.TabIndex = 4;
            lblTongSoMay.Text = "Tổng";
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(106, 64);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(210, 28);
            cboTrangThai.TabIndex = 3;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(12, 67);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(75, 20);
            lblTrangThai.TabIndex = 2;
            lblTrangThai.Text = "Trạng thái";
            // 
            // lblKhuVuc
            // 
            lblKhuVuc.AutoSize = true;
            lblKhuVuc.Location = new Point(12, 9);
            lblKhuVuc.Name = "lblKhuVuc";
            lblKhuVuc.Size = new Size(61, 20);
            lblKhuVuc.TabIndex = 1;
            lblKhuVuc.Text = "Khu vực";
            // 
            // cboKhuVuc
            // 
            cboKhuVuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhuVuc.FormattingEnabled = true;
            cboKhuVuc.Location = new Point(106, 6);
            cboKhuVuc.Name = "cboKhuVuc";
            cboKhuVuc.Size = new Size(210, 28);
            cboKhuVuc.TabIndex = 0;
            // 
            // cmsMayTram
            // 
            cmsMayTram.ImageScalingSize = new Size(19, 19);
            cmsMayTram.Items.AddRange(new ToolStripItem[] { tsmiMoMay, tsmiKhoaMay, tsmiThanhToan, toolStripMenuItem1, tsmiChuyenMay, tsmiBaoTri });
            cmsMayTram.Name = "cmsMayTram";
            cmsMayTram.Size = new Size(219, 130);
            // 
            // tsmiMoMay
            // 
            tsmiMoMay.DropDownItems.AddRange(new ToolStripItem[] { tsmiMoKhach, tsmiMoHoiVien });
            tsmiMoMay.Name = "tsmiMoMay";
            tsmiMoMay.Size = new Size(218, 24);
            tsmiMoMay.Text = "Mở máy";
            // 
            // tsmiMoKhach
            // 
            tsmiMoKhach.Name = "tsmiMoKhach";
            tsmiMoKhach.Size = new Size(188, 26);
            tsmiMoKhach.Text = "Khách vãng lai";
            // 
            // tsmiMoHoiVien
            // 
            tsmiMoHoiVien.Name = "tsmiMoHoiVien";
            tsmiMoHoiVien.Size = new Size(188, 26);
            tsmiMoHoiVien.Text = "Hội viên";
            // 
            // tsmiKhoaMay
            // 
            tsmiKhoaMay.Name = "tsmiKhoaMay";
            tsmiKhoaMay.Size = new Size(218, 24);
            tsmiKhoaMay.Text = "Khóa máy";
            // 
            // tsmiThanhToan
            // 
            tsmiThanhToan.Name = "tsmiThanhToan";
            tsmiThanhToan.Size = new Size(218, 24);
            tsmiThanhToan.Text = "Thanh toán / Trả máy";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(215, 6);
            // 
            // tsmiChuyenMay
            // 
            tsmiChuyenMay.Name = "tsmiChuyenMay";
            tsmiChuyenMay.Size = new Size(218, 24);
            tsmiChuyenMay.Text = "Chuyển máy";
            // 
            // tsmiBaoTri
            // 
            tsmiBaoTri.Name = "tsmiBaoTri";
            tsmiBaoTri.Size = new Size(218, 24);
            tsmiBaoTri.Text = "Bảo trì / Bỏ bảo trì";
            // 
            // flpDanhSachMay
            // 
            flpDanhSachMay.AutoScroll = true;
            flpDanhSachMay.ContextMenuStrip = cmsMayTram;
            flpDanhSachMay.Dock = DockStyle.Fill;
            flpDanhSachMay.Location = new Point(0, 100);
            flpDanhSachMay.Name = "flpDanhSachMay";
            flpDanhSachMay.Padding = new Padding(10, 11, 10, 11);
            flpDanhSachMay.Size = new Size(889, 600);
            flpDanhSachMay.TabIndex = 1;
            // 
            // frmSoDoPhongMay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 700);
            Controls.Add(flpDanhSachMay);
            Controls.Add(pnlTopFilter);
            Name = "frmSoDoPhongMay";
            Text = "Sơ Đồ Phòng Máy";
            Load += frmSoDoPhongMay_Load;
            pnlTopFilter.ResumeLayout(false);
            pnlTopFilter.PerformLayout();
            cmsMayTram.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTopFilter;
        private Label lblTrangThai;
        private Label lblKhuVuc;
        private ComboBox cboKhuVuc;
        private ComboBox cboTrangThai;
        private Label lblBaoTri;
        private Label lblMayTrong;
        private Label lblDangOnline;
        private Label lblTongSoMay;
        private FlowLayoutPanel flpDanhSachMay;
        private ContextMenuStrip cmsMayTram;
        private ToolStripMenuItem tsmiMoMay;
        private ToolStripMenuItem tsmiMoKhach;
        private ToolStripMenuItem tsmiMoHoiVien;
        private ToolStripMenuItem tsmiKhoaMay;
        private ToolStripMenuItem tsmiThanhToan;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem tsmiChuyenMay;
        private ToolStripMenuItem tsmiBaoTri;
        private Button btnLamMoi;
    }
}
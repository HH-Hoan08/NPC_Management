namespace NPC.Client
{
    partial class frmClient
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlKhoa = new Panel();
            pnlDangNhap = new Panel();
            lblTieuDe = new Label();
            lblTenMay = new Label();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            btnDangNhap = new Button();
            lblThongBao = new Label();
            picNen = new PictureBox();
            pnlThanhNoi = new Panel();
            lblTenHoiVien = new Label();
            lblSoDu = new Label();
            lblThoiGianConLai = new Label();
            lblDaChoi = new Label();
            lblDonGiaGio = new Label();
            btnGoiMon = new Button();
            btnTraMay = new Button();
            tmrDemGio = new System.Windows.Forms.Timer(components);
            tmrTruTien = new System.Windows.Forms.Timer(components);
            tmrDongBo = new System.Windows.Forms.Timer(components);
            pnlKhoa.SuspendLayout();
            pnlDangNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picNen).BeginInit();
            pnlThanhNoi.SuspendLayout();
            SuspendLayout();
            // 
            // pnlKhoa
            // 
            pnlKhoa.BackColor = Color.FromArgb(24, 28, 36);
            pnlKhoa.Controls.Add(pnlDangNhap);
            pnlKhoa.Controls.Add(picNen);
            pnlKhoa.Dock = DockStyle.Fill;
            pnlKhoa.ForeColor = SystemColors.Control;
            pnlKhoa.Location = new Point(0, 0);
            pnlKhoa.Margin = new Padding(4);
            pnlKhoa.Name = "pnlKhoa";
            pnlKhoa.Size = new Size(1250, 750);
            pnlKhoa.TabIndex = 0;
            // 
            // pnlDangNhap
            // 
            pnlDangNhap.Anchor = AnchorStyles.None;
            pnlDangNhap.BackColor = Color.Silver;
            pnlDangNhap.Controls.Add(lblTieuDe);
            pnlDangNhap.Controls.Add(lblTenMay);
            pnlDangNhap.Controls.Add(lblTenDangNhap);
            pnlDangNhap.Controls.Add(txtTenDangNhap);
            pnlDangNhap.Controls.Add(lblMatKhau);
            pnlDangNhap.Controls.Add(txtMatKhau);
            pnlDangNhap.Controls.Add(btnDangNhap);
            pnlDangNhap.Controls.Add(lblThongBao);
            pnlDangNhap.Location = new Point(362, 138);
            pnlDangNhap.Margin = new Padding(4);
            pnlDangNhap.Name = "pnlDangNhap";
            pnlDangNhap.Size = new Size(450, 450);
            pnlDangNhap.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            lblTieuDe.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.Black;
            lblTieuDe.Location = new Point(50, 25);
            lblTieuDe.Margin = new Padding(4, 0, 4, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(350, 50);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "MÁY TRẠM NPC";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenMay
            // 
            lblTenMay.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTenMay.ForeColor = Color.Black;
            lblTenMay.Location = new Point(50, 78);
            lblTenMay.Margin = new Padding(4, 0, 4, 0);
            lblTenMay.Name = "lblTenMay";
            lblTenMay.Size = new Size(349, 35);
            lblTenMay.TabIndex = 1;
            lblTenMay.Text = "MAY-01";
            lblTenMay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenDangNhap.ForeColor = Color.Black;
            lblTenDangNhap.Location = new Point(50, 140);
            lblTenDangNhap.Margin = new Padding(4, 0, 4, 0);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(350, 28);
            lblTenDangNhap.TabIndex = 2;
            lblTenDangNhap.Text = "Tên đăng nhập";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Segoe UI", 12F);
            txtTenDangNhap.Location = new Point(50, 170);
            txtTenDangNhap.Margin = new Padding(4);
            txtTenDangNhap.MaxLength = 50;
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(349, 34);
            txtTenDangNhap.TabIndex = 0;
            // 
            // lblMatKhau
            // 
            lblMatKhau.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMatKhau.ForeColor = Color.Black;
            lblMatKhau.Location = new Point(50, 228);
            lblMatKhau.Margin = new Padding(4, 0, 4, 0);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(350, 28);
            lblMatKhau.TabIndex = 4;
            lblMatKhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Segoe UI", 12F);
            txtMatKhau.Location = new Point(50, 258);
            txtMatKhau.Margin = new Padding(4);
            txtMatKhau.MaxLength = 255;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(349, 34);
            txtMatKhau.TabIndex = 1;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(0, 122, 255);
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(50, 328);
            btnDangNhap.Margin = new Padding(4);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(350, 52);
            btnDangNhap.TabIndex = 2;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // lblThongBao
            // 
            lblThongBao.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThongBao.ForeColor = Color.Yellow;
            lblThongBao.ImageAlign = ContentAlignment.MiddleRight;
            lblThongBao.Location = new Point(50, 395);
            lblThongBao.Margin = new Padding(4, 0, 4, 0);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(350, 45);
            lblThongBao.TabIndex = 7;
            lblThongBao.Text = "Máy đang khóa – vui lòng đăng nhập";
            lblThongBao.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picNen
            // 
            picNen.Dock = DockStyle.Fill;
            picNen.Image = Properties.Resources.NVIDIA_GeForce;
            picNen.Location = new Point(0, 0);
            picNen.Name = "picNen";
            picNen.Size = new Size(1250, 750);
            picNen.SizeMode = PictureBoxSizeMode.StretchImage;
            picNen.TabIndex = 1;
            picNen.TabStop = false;
            // 
            // pnlThanhNoi
            // 
            pnlThanhNoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlThanhNoi.BackColor = Color.FromArgb(33, 38, 49);
            pnlThanhNoi.Controls.Add(lblTenHoiVien);
            pnlThanhNoi.Controls.Add(lblSoDu);
            pnlThanhNoi.Controls.Add(lblThoiGianConLai);
            pnlThanhNoi.Controls.Add(lblDaChoi);
            pnlThanhNoi.Controls.Add(lblDonGiaGio);
            pnlThanhNoi.Controls.Add(btnGoiMon);
            pnlThanhNoi.Controls.Add(btnTraMay);
            pnlThanhNoi.Location = new Point(855, 18);
            pnlThanhNoi.Margin = new Padding(4);
            pnlThanhNoi.Name = "pnlThanhNoi";
            pnlThanhNoi.Size = new Size(380, 270);
            pnlThanhNoi.TabIndex = 1;
            pnlThanhNoi.Visible = false;
            // 
            // lblTenHoiVien
            // 
            lblTenHoiVien.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTenHoiVien.ForeColor = Color.White;
            lblTenHoiVien.Location = new Point(12, 10);
            lblTenHoiVien.Margin = new Padding(4, 0, 4, 0);
            lblTenHoiVien.Name = "lblTenHoiVien";
            lblTenHoiVien.Size = new Size(355, 28);
            lblTenHoiVien.TabIndex = 0;
            lblTenHoiVien.Text = "Hội viên: nguyenvana";
            // 
            // lblSoDu
            // 
            lblSoDu.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSoDu.ForeColor = Color.OrangeRed;
            lblSoDu.Location = new Point(12, 40);
            lblSoDu.Margin = new Padding(4, 0, 4, 0);
            lblSoDu.Name = "lblSoDu";
            lblSoDu.Size = new Size(355, 28);
            lblSoDu.TabIndex = 1;
            lblSoDu.Text = "Số dư: 50.000 đ";
            // 
            // lblThoiGianConLai
            // 
            lblThoiGianConLai.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblThoiGianConLai.ForeColor = Color.White;
            lblThoiGianConLai.Location = new Point(12, 72);
            lblThoiGianConLai.Margin = new Padding(4, 0, 4, 0);
            lblThoiGianConLai.Name = "lblThoiGianConLai";
            lblThoiGianConLai.Size = new Size(355, 55);
            lblThoiGianConLai.TabIndex = 2;
            lblThoiGianConLai.Text = "Còn lại: 02:30:00";
            lblThoiGianConLai.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDaChoi
            // 
            lblDaChoi.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDaChoi.ForeColor = Color.FromArgb(203, 213, 225);
            lblDaChoi.Location = new Point(12, 135);
            lblDaChoi.Margin = new Padding(4, 0, 4, 0);
            lblDaChoi.Name = "lblDaChoi";
            lblDaChoi.Size = new Size(355, 28);
            lblDaChoi.TabIndex = 3;
            lblDaChoi.Text = "Đã chơi: 00:15:00";
            // 
            // lblDonGiaGio
            // 
            lblDonGiaGio.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDonGiaGio.ForeColor = Color.FromArgb(203, 213, 225);
            lblDonGiaGio.Location = new Point(12, 165);
            lblDonGiaGio.Margin = new Padding(4, 0, 4, 0);
            lblDonGiaGio.Name = "lblDonGiaGio";
            lblDonGiaGio.Size = new Size(355, 28);
            lblDonGiaGio.TabIndex = 4;
            lblDonGiaGio.Text = "Giá giờ: 8.000 đ/giờ";
            // 
            // btnGoiMon
            // 
            btnGoiMon.BackColor = Color.FromArgb(16, 185, 129);
            btnGoiMon.FlatAppearance.BorderSize = 0;
            btnGoiMon.FlatStyle = FlatStyle.Flat;
            btnGoiMon.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGoiMon.ForeColor = Color.White;
            btnGoiMon.Location = new Point(12, 208);
            btnGoiMon.Margin = new Padding(4);
            btnGoiMon.Name = "btnGoiMon";
            btnGoiMon.Size = new Size(170, 45);
            btnGoiMon.TabIndex = 5;
            btnGoiMon.Text = "Gọi món";
            btnGoiMon.UseVisualStyleBackColor = false;
            btnGoiMon.Click += btnGoiMon_Click;
            // 
            // btnTraMay
            // 
            btnTraMay.BackColor = Color.FromArgb(225, 29, 72);
            btnTraMay.FlatAppearance.BorderSize = 0;
            btnTraMay.FlatStyle = FlatStyle.Flat;
            btnTraMay.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnTraMay.ForeColor = Color.White;
            btnTraMay.Location = new Point(198, 208);
            btnTraMay.Margin = new Padding(4);
            btnTraMay.Name = "btnTraMay";
            btnTraMay.Size = new Size(170, 45);
            btnTraMay.TabIndex = 6;
            btnTraMay.Text = "Trả máy";
            btnTraMay.UseVisualStyleBackColor = false;
            btnTraMay.Click += btnTraMay_Click;
            // 
            // tmrDemGio
            // 
            tmrDemGio.Interval = 1000;
            tmrDemGio.Tick += tmrDemGio_Tick;
            // 
            // tmrTruTien
            // 
            tmrTruTien.Interval = 1000;
            tmrTruTien.Tick += tmrTruTien_Tick;
            // 
            // tmrDongBo
            // 
            tmrDongBo.Interval = 3000;
            tmrDongBo.Tick += tmrDongBo_Tick;
            // 
            // frmClient
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1250, 750);
            ControlBox = false;
            Controls.Add(pnlThanhNoi);
            Controls.Add(pnlKhoa);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Margin = new Padding(4);
            Name = "frmClient";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Máy trạm NPC";
            TopMost = true;
            FormClosing += frmClient_FormClosing;
            Load += frmClient_Load;
            KeyDown += frmClient_KeyDown;
            Resize += frmClient_Resize;
            pnlKhoa.ResumeLayout(false);
            pnlDangNhap.ResumeLayout(false);
            pnlDangNhap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picNen).EndInit();
            pnlThanhNoi.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlKhoa;
        private PictureBox picNen;
        private Panel pnlDangNhap;
        private Label lblTieuDe;
        private Label lblTenMay;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
        private Label lblThongBao;
        private Panel pnlThanhNoi;
        private Label lblTenHoiVien;
        private Label lblSoDu;
        private Label lblThoiGianConLai;
        private Label lblDaChoi;
        private Label lblDonGiaGio;
        private Button btnGoiMon;
        private Button btnTraMay;
        private System.Windows.Forms.Timer tmrDemGio;
        private System.Windows.Forms.Timer tmrTruTien;
        private System.Windows.Forms.Timer tmrDongBo;
    }
}


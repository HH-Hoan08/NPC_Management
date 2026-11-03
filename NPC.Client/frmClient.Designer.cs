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
            btnThuGon = new Button();
            lblTenHoiVien = new Label();
            lblSoDu = new Label();
            lblThoiGianConLai = new Label();
            lblDaChoi = new Label();
            lblDonGiaGio = new Label();
            btnGoiMon = new Button();
            btnTraMay = new Button();
            btnNapTienClient = new Button();
            btnIconThuGon = new Button();
            pnlTamKhoa = new Panel();
            pnlTamKhoaContent = new Panel();
            lblTamKhoaIcon = new Label();
            lblTamKhoaTieuDe = new Label();
            lblTamKhoaPhu = new Label();
            pnlNapTienClient = new Panel();
            lblNapTienHeader = new Label();
            lblNapTienSoDu = new Label();
            lblNhapTien = new Label();
            txtSoTienNapClient = new TextBox();
            btnMenhGia10k = new Button();
            btnMenhGia20k = new Button();
            btnMenhGia50k = new Button();
            btnMenhGia100k = new Button();
            btnMenhGia200k = new Button();
            btnMenhGia500k = new Button();
            lblHinhThuc = new Label();
            cboHinhThucNap = new ComboBox();
            btnXacNhanNapClient = new Button();
            btnDongNapTienClient = new Button();
            tmrDemGio = new System.Windows.Forms.Timer(components);
            tmrTruTien = new System.Windows.Forms.Timer(components);
            tmrDongBo = new System.Windows.Forms.Timer(components);
            pnlKhoa.SuspendLayout();
            pnlDangNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picNen).BeginInit();
            pnlThanhNoi.SuspendLayout();
            pnlTamKhoa.SuspendLayout();
            pnlTamKhoaContent.SuspendLayout();
            pnlNapTienClient.SuspendLayout();
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
            pnlDangNhap.BackColor = Color.Snow;
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
            lblThongBao.ForeColor = Color.Black;
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
            pnlThanhNoi.BackColor = Color.Navy;
            pnlThanhNoi.Controls.Add(btnThuGon);
            pnlThanhNoi.Controls.Add(lblTenHoiVien);
            pnlThanhNoi.Controls.Add(lblSoDu);
            pnlThanhNoi.Controls.Add(lblThoiGianConLai);
            pnlThanhNoi.Controls.Add(lblDaChoi);
            pnlThanhNoi.Controls.Add(lblDonGiaGio);
            pnlThanhNoi.Controls.Add(btnGoiMon);
            pnlThanhNoi.Controls.Add(btnNapTienClient);
            pnlThanhNoi.Controls.Add(btnTraMay);
            pnlThanhNoi.Location = new Point(855, 18);
            pnlThanhNoi.Margin = new Padding(4);
            pnlThanhNoi.Name = "pnlThanhNoi";
            pnlThanhNoi.Size = new Size(380, 310);
            pnlThanhNoi.TabIndex = 1;
            pnlThanhNoi.Visible = false;
            // 
            // btnThuGon
            // 
            btnThuGon.BackColor = Color.Transparent;
            btnThuGon.FlatAppearance.BorderSize = 0;
            btnThuGon.FlatStyle = FlatStyle.Flat;
            btnThuGon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnThuGon.ForeColor = Color.White;
            btnThuGon.Location = new Point(335, 5);
            btnThuGon.Name = "btnThuGon";
            btnThuGon.Size = new Size(38, 32);
            btnThuGon.TabIndex = 7;
            btnThuGon.Text = "—";
            btnThuGon.UseVisualStyleBackColor = false;
            btnThuGon.Click += btnThuGon_Click;
            // 
            // lblTenHoiVien
            // 
            lblTenHoiVien.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTenHoiVien.ForeColor = Color.White;
            lblTenHoiVien.Location = new Point(12, 10);
            lblTenHoiVien.Margin = new Padding(4, 0, 4, 0);
            lblTenHoiVien.Name = "lblTenHoiVien";
            lblTenHoiVien.Size = new Size(315, 28);
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
            btnGoiMon.Location = new Point(12, 205);
            btnGoiMon.Margin = new Padding(4);
            btnGoiMon.Name = "btnGoiMon";
            btnGoiMon.Size = new Size(170, 42);
            btnGoiMon.TabIndex = 5;
            btnGoiMon.Text = "Gọi món";
            btnGoiMon.UseVisualStyleBackColor = false;
            btnGoiMon.Click += btnGoiMon_Click;
            // 
            // btnNapTienClient
            // 
            btnNapTienClient.BackColor = Color.FromArgb(37, 99, 235);
            btnNapTienClient.FlatAppearance.BorderSize = 0;
            btnNapTienClient.FlatStyle = FlatStyle.Flat;
            btnNapTienClient.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnNapTienClient.ForeColor = Color.White;
            btnNapTienClient.Location = new Point(198, 205);
            btnNapTienClient.Margin = new Padding(4);
            btnNapTienClient.Name = "btnNapTienClient";
            btnNapTienClient.Size = new Size(170, 42);
            btnNapTienClient.TabIndex = 6;
            btnNapTienClient.Text = "Nạp tiền";
            btnNapTienClient.UseVisualStyleBackColor = false;
            btnNapTienClient.Click += btnNapTienClient_Click;
            // 
            // btnTraMay
            // 
            btnTraMay.BackColor = Color.FromArgb(225, 29, 72);
            btnTraMay.FlatAppearance.BorderSize = 0;
            btnTraMay.FlatStyle = FlatStyle.Flat;
            btnTraMay.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnTraMay.ForeColor = Color.White;
            btnTraMay.Location = new Point(12, 255);
            btnTraMay.Margin = new Padding(4);
            btnTraMay.Name = "btnTraMay";
            btnTraMay.Size = new Size(356, 42);
            btnTraMay.TabIndex = 7;
            btnTraMay.Text = "Trả máy";
            btnTraMay.UseVisualStyleBackColor = false;
            btnTraMay.Click += btnTraMay_Click;
            // 
            // btnIconThuGon
            // 
            btnIconThuGon.BackColor = Color.FromArgb(0, 122, 255);
            btnIconThuGon.FlatAppearance.BorderSize = 0;
            btnIconThuGon.FlatStyle = FlatStyle.Flat;
            btnIconThuGon.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnIconThuGon.ForeColor = Color.White;
            btnIconThuGon.Location = new Point(0, 0);
            btnIconThuGon.Name = "btnIconThuGon";
            btnIconThuGon.Size = new Size(50, 50);
            btnIconThuGon.TabIndex = 2;
            btnIconThuGon.Text = "⚡";
            btnIconThuGon.UseVisualStyleBackColor = false;
            btnIconThuGon.Visible = false;
            btnIconThuGon.Click += btnIconThuGon_Click;
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
            // pnlTamKhoa
            // 
            pnlTamKhoa.BackColor = Color.FromArgb(20, 24, 33);
            pnlTamKhoa.Controls.Add(pnlTamKhoaContent);
            pnlTamKhoa.Dock = DockStyle.Fill;
            pnlTamKhoa.Location = new Point(0, 0);
            pnlTamKhoa.Name = "pnlTamKhoa";
            pnlTamKhoa.Size = new Size(1250, 750);
            pnlTamKhoa.TabIndex = 3;
            pnlTamKhoa.Visible = false;
            // 
            // pnlTamKhoaContent
            // 
            pnlTamKhoaContent.Anchor = AnchorStyles.None;
            pnlTamKhoaContent.BackColor = Color.Transparent;
            pnlTamKhoaContent.Controls.Add(lblTamKhoaIcon);
            pnlTamKhoaContent.Controls.Add(lblTamKhoaTieuDe);
            pnlTamKhoaContent.Controls.Add(lblTamKhoaPhu);
            pnlTamKhoaContent.Location = new Point(225, 225);
            pnlTamKhoaContent.Name = "pnlTamKhoaContent";
            pnlTamKhoaContent.Size = new Size(800, 300);
            pnlTamKhoaContent.TabIndex = 0;
            // 
            // lblTamKhoaIcon
            // 
            lblTamKhoaIcon.Font = new Font("Segoe UI", 52F, FontStyle.Bold);
            lblTamKhoaIcon.ForeColor = Color.FromArgb(245, 158, 11);
            lblTamKhoaIcon.Location = new Point(0, 10);
            lblTamKhoaIcon.Name = "lblTamKhoaIcon";
            lblTamKhoaIcon.Size = new Size(800, 100);
            lblTamKhoaIcon.TabIndex = 0;
            lblTamKhoaIcon.Text = "🔒";
            lblTamKhoaIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTamKhoaTieuDe
            // 
            lblTamKhoaTieuDe.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTamKhoaTieuDe.ForeColor = Color.FromArgb(245, 158, 11);
            lblTamKhoaTieuDe.Location = new Point(0, 120);
            lblTamKhoaTieuDe.Name = "lblTamKhoaTieuDe";
            lblTamKhoaTieuDe.Size = new Size(800, 50);
            lblTamKhoaTieuDe.TabIndex = 1;
            lblTamKhoaTieuDe.Text = "MÁY TÍNH ĐANG BỊ TẠM KHÓA BỞI MÁY CHỦ";
            lblTamKhoaTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTamKhoaPhu
            // 
            lblTamKhoaPhu.Font = new Font("Segoe UI", 13F);
            lblTamKhoaPhu.ForeColor = Color.FromArgb(203, 213, 225);
            lblTamKhoaPhu.Location = new Point(0, 180);
            lblTamKhoaPhu.Name = "lblTamKhoaPhu";
            lblTamKhoaPhu.Size = new Size(800, 40);
            lblTamKhoaPhu.TabIndex = 2;
            lblTamKhoaPhu.Text = "Vui lòng liên hệ quầy thu ngân để mở khóa máy tính.";
            lblTamKhoaPhu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlNapTienClient
            // 
            pnlNapTienClient.BackColor = Color.Navy;
            pnlNapTienClient.BorderStyle = BorderStyle.FixedSingle;
            pnlNapTienClient.Controls.Add(lblNapTienHeader);
            pnlNapTienClient.Controls.Add(lblNapTienSoDu);
            pnlNapTienClient.Controls.Add(lblNhapTien);
            pnlNapTienClient.Controls.Add(txtSoTienNapClient);
            pnlNapTienClient.Controls.Add(btnMenhGia10k);
            pnlNapTienClient.Controls.Add(btnMenhGia20k);
            pnlNapTienClient.Controls.Add(btnMenhGia50k);
            pnlNapTienClient.Controls.Add(btnMenhGia100k);
            pnlNapTienClient.Controls.Add(btnMenhGia200k);
            pnlNapTienClient.Controls.Add(btnMenhGia500k);
            pnlNapTienClient.Controls.Add(lblHinhThuc);
            pnlNapTienClient.Controls.Add(cboHinhThucNap);
            pnlNapTienClient.Controls.Add(btnXacNhanNapClient);
            pnlNapTienClient.Controls.Add(btnDongNapTienClient);
            pnlNapTienClient.Location = new Point(855, 18);
            pnlNapTienClient.Name = "pnlNapTienClient";
            pnlNapTienClient.Size = new Size(380, 390);
            pnlNapTienClient.TabIndex = 4;
            pnlNapTienClient.Visible = false;
            // 
            // lblNapTienHeader
            // 
            lblNapTienHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNapTienHeader.ForeColor = Color.White;
            lblNapTienHeader.Location = new Point(12, 10);
            lblNapTienHeader.Name = "lblNapTienHeader";
            lblNapTienHeader.Size = new Size(355, 30);
            lblNapTienHeader.TabIndex = 0;
            lblNapTienHeader.Text = "NẠP TIỀN TÀI KHOẢN";
            lblNapTienHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNapTienSoDu
            // 
            lblNapTienSoDu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNapTienSoDu.ForeColor = Color.OrangeRed;
            lblNapTienSoDu.Location = new Point(12, 45);
            lblNapTienSoDu.Name = "lblNapTienSoDu";
            lblNapTienSoDu.Size = new Size(355, 25);
            lblNapTienSoDu.TabIndex = 1;
            lblNapTienSoDu.Text = "Số dư hiện tại: 0 đ";
            lblNapTienSoDu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNhapTien
            // 
            lblNhapTien.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNhapTien.ForeColor = Color.White;
            lblNhapTien.Location = new Point(12, 75);
            lblNhapTien.Name = "lblNhapTien";
            lblNhapTien.Size = new Size(355, 22);
            lblNhapTien.TabIndex = 2;
            lblNhapTien.Text = "Nhập số tiền nạp (VNĐ):";
            // 
            // txtSoTienNapClient
            // 
            txtSoTienNapClient.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtSoTienNapClient.Location = new Point(12, 100);
            txtSoTienNapClient.Name = "txtSoTienNapClient";
            txtSoTienNapClient.Size = new Size(355, 34);
            txtSoTienNapClient.TabIndex = 3;
            txtSoTienNapClient.Text = "50000";
            // 
            // btnMenhGia10k
            // 
            btnMenhGia10k.BackColor = Color.FromArgb(30, 41, 59);
            btnMenhGia10k.FlatAppearance.BorderSize = 0;
            btnMenhGia10k.FlatStyle = FlatStyle.Flat;
            btnMenhGia10k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMenhGia10k.ForeColor = Color.White;
            btnMenhGia10k.Location = new Point(12, 142);
            btnMenhGia10k.Name = "btnMenhGia10k";
            btnMenhGia10k.Size = new Size(112, 32);
            btnMenhGia10k.TabIndex = 4;
            btnMenhGia10k.Text = "10.000";
            btnMenhGia10k.UseVisualStyleBackColor = false;
            // 
            // btnMenhGia20k
            // 
            btnMenhGia20k.BackColor = Color.FromArgb(30, 41, 59);
            btnMenhGia20k.FlatAppearance.BorderSize = 0;
            btnMenhGia20k.FlatStyle = FlatStyle.Flat;
            btnMenhGia20k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMenhGia20k.ForeColor = Color.White;
            btnMenhGia20k.Location = new Point(134, 142);
            btnMenhGia20k.Name = "btnMenhGia20k";
            btnMenhGia20k.Size = new Size(112, 32);
            btnMenhGia20k.TabIndex = 5;
            btnMenhGia20k.Text = "20.000";
            btnMenhGia20k.UseVisualStyleBackColor = false;
            // 
            // btnMenhGia50k
            // 
            btnMenhGia50k.BackColor = Color.FromArgb(30, 41, 59);
            btnMenhGia50k.FlatAppearance.BorderSize = 0;
            btnMenhGia50k.FlatStyle = FlatStyle.Flat;
            btnMenhGia50k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMenhGia50k.ForeColor = Color.White;
            btnMenhGia50k.Location = new Point(255, 142);
            btnMenhGia50k.Name = "btnMenhGia50k";
            btnMenhGia50k.Size = new Size(112, 32);
            btnMenhGia50k.TabIndex = 6;
            btnMenhGia50k.Text = "50.000";
            btnMenhGia50k.UseVisualStyleBackColor = false;
            // 
            // btnMenhGia100k
            // 
            btnMenhGia100k.BackColor = Color.FromArgb(30, 41, 59);
            btnMenhGia100k.FlatAppearance.BorderSize = 0;
            btnMenhGia100k.FlatStyle = FlatStyle.Flat;
            btnMenhGia100k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMenhGia100k.ForeColor = Color.White;
            btnMenhGia100k.Location = new Point(12, 180);
            btnMenhGia100k.Name = "btnMenhGia100k";
            btnMenhGia100k.Size = new Size(112, 32);
            btnMenhGia100k.TabIndex = 7;
            btnMenhGia100k.Text = "100.000";
            btnMenhGia100k.UseVisualStyleBackColor = false;
            // 
            // btnMenhGia200k
            // 
            btnMenhGia200k.BackColor = Color.FromArgb(30, 41, 59);
            btnMenhGia200k.FlatAppearance.BorderSize = 0;
            btnMenhGia200k.FlatStyle = FlatStyle.Flat;
            btnMenhGia200k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMenhGia200k.ForeColor = Color.White;
            btnMenhGia200k.Location = new Point(134, 180);
            btnMenhGia200k.Name = "btnMenhGia200k";
            btnMenhGia200k.Size = new Size(112, 32);
            btnMenhGia200k.TabIndex = 8;
            btnMenhGia200k.Text = "200.000";
            btnMenhGia200k.UseVisualStyleBackColor = false;
            // 
            // btnMenhGia500k
            // 
            btnMenhGia500k.BackColor = Color.FromArgb(30, 41, 59);
            btnMenhGia500k.FlatAppearance.BorderSize = 0;
            btnMenhGia500k.FlatStyle = FlatStyle.Flat;
            btnMenhGia500k.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnMenhGia500k.ForeColor = Color.White;
            btnMenhGia500k.Location = new Point(255, 180);
            btnMenhGia500k.Name = "btnMenhGia500k";
            btnMenhGia500k.Size = new Size(112, 32);
            btnMenhGia500k.TabIndex = 9;
            btnMenhGia500k.Text = "500.000";
            btnMenhGia500k.UseVisualStyleBackColor = false;
            // 
            // lblHinhThuc
            // 
            lblHinhThuc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHinhThuc.ForeColor = Color.White;
            lblHinhThuc.Location = new Point(12, 222);
            lblHinhThuc.Name = "lblHinhThuc";
            lblHinhThuc.Size = new Size(355, 22);
            lblHinhThuc.TabIndex = 10;
            lblHinhThuc.Text = "Hình thức thanh toán:";
            // 
            // cboHinhThucNap
            // 
            cboHinhThucNap.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHinhThucNap.Font = new Font("Segoe UI", 10F);
            cboHinhThucNap.FormattingEnabled = true;
            cboHinhThucNap.Items.AddRange(new object[] { "Tiền mặt tại quầy", "Chuyển khoản QR" });
            cboHinhThucNap.Location = new Point(12, 248);
            cboHinhThucNap.Name = "cboHinhThucNap";
            cboHinhThucNap.Size = new Size(355, 31);
            cboHinhThucNap.TabIndex = 11;
            // 
            // btnXacNhanNapClient
            // 
            btnXacNhanNapClient.BackColor = Color.FromArgb(16, 185, 129);
            btnXacNhanNapClient.FlatAppearance.BorderSize = 0;
            btnXacNhanNapClient.FlatStyle = FlatStyle.Flat;
            btnXacNhanNapClient.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnXacNhanNapClient.ForeColor = Color.White;
            btnXacNhanNapClient.Location = new Point(12, 315);
            btnXacNhanNapClient.Name = "btnXacNhanNapClient";
            btnXacNhanNapClient.Size = new Size(170, 45);
            btnXacNhanNapClient.TabIndex = 12;
            btnXacNhanNapClient.Text = "Xác nhận nạp";
            btnXacNhanNapClient.UseVisualStyleBackColor = false;
            btnXacNhanNapClient.Click += btnXacNhanNapClient_Click;
            // 
            // btnDongNapTienClient
            // 
            btnDongNapTienClient.BackColor = Color.FromArgb(100, 116, 139);
            btnDongNapTienClient.FlatAppearance.BorderSize = 0;
            btnDongNapTienClient.FlatStyle = FlatStyle.Flat;
            btnDongNapTienClient.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDongNapTienClient.ForeColor = Color.White;
            btnDongNapTienClient.Location = new Point(198, 315);
            btnDongNapTienClient.Name = "btnDongNapTienClient";
            btnDongNapTienClient.Size = new Size(170, 45);
            btnDongNapTienClient.TabIndex = 13;
            btnDongNapTienClient.Text = "Đóng";
            btnDongNapTienClient.UseVisualStyleBackColor = false;
            btnDongNapTienClient.Click += btnDongNapTienClient_Click;
            // 
            // frmClient
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1250, 750);
            ControlBox = false;
            Controls.Add(btnIconThuGon);
            Controls.Add(pnlNapTienClient);
            Controls.Add(pnlThanhNoi);
            Controls.Add(pnlTamKhoa);
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
            pnlTamKhoaContent.ResumeLayout(false);
            pnlTamKhoa.ResumeLayout(false);
            pnlNapTienClient.ResumeLayout(false);
            pnlNapTienClient.PerformLayout();
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
        private Button btnThuGon;
        private Label lblTenHoiVien;
        private Label lblSoDu;
        private Label lblThoiGianConLai;
        private Label lblDaChoi;
        private Label lblDonGiaGio;
        private Button btnGoiMon;
        private Button btnNapTienClient;
        private Button btnTraMay;
        private Button btnIconThuGon;
        private Panel pnlTamKhoa;
        private Panel pnlTamKhoaContent;
        private Label lblTamKhoaIcon;
        private Label lblTamKhoaTieuDe;
        private Label lblTamKhoaPhu;
        private Panel pnlNapTienClient;
        private Label lblNapTienHeader;
        private Label lblNapTienSoDu;
        private Label lblNhapTien;
        private TextBox txtSoTienNapClient;
        private Button btnMenhGia10k;
        private Button btnMenhGia20k;
        private Button btnMenhGia50k;
        private Button btnMenhGia100k;
        private Button btnMenhGia200k;
        private Button btnMenhGia500k;
        private Label lblHinhThuc;
        private ComboBox cboHinhThucNap;
        private Button btnXacNhanNapClient;
        private Button btnDongNapTienClient;
        private System.Windows.Forms.Timer tmrDemGio;
        private System.Windows.Forms.Timer tmrTruTien;
        private System.Windows.Forms.Timer tmrDongBo;
    }
}

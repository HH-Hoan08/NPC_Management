namespace NPC.Server
{
    partial class frmQuanLyHoiVien
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
            lblTieuDe = new Label();
            tableMain = new TableLayoutPanel();
            pnlLeft = new Panel();
            gbThongTin = new GroupBox();
            lblMaHoiVien = new Label();
            txtMaHoiVien = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            lblDienThoai = new Label();
            txtDienThoai = new TextBox();
            lblSoDu = new Label();
            txtSoDu = new TextBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            gbTimKiem = new GroupBox();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            btnLamMoi = new Button();
            gbDanhSach = new GroupBox();
            dgvHoiVien = new DataGridView();
            pnlSummary = new Panel();
            lblTongSoHoiVien = new Label();
            lblTongSoDu = new Label();
            pnlChucNang = new Panel();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnNhapLai = new Button();
            btnNapTien = new Button();
            btnXuatFile = new Button();
            btnDong = new Button();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            saveFileDialog = new SaveFileDialog();
            tableMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            gbThongTin.SuspendLayout();
            gbTimKiem.SuspendLayout();
            gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).BeginInit();
            pnlSummary.SuspendLayout();
            pnlChucNang.SuspendLayout();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTieuDe.Location = new Point(0, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(1004, 45);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ HỘI VIÊN";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableMain
            // 
            tableMain.ColumnCount = 2;
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 450F));
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableMain.Controls.Add(pnlLeft, 0, 0);
            tableMain.Controls.Add(gbDanhSach, 1, 0);
            tableMain.Dock = DockStyle.Fill;
            tableMain.Location = new Point(0, 45);
            tableMain.Name = "tableMain";
            tableMain.RowCount = 1;
            tableMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableMain.Size = new Size(1004, 555);
            tableMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.AutoScroll = true;
            pnlLeft.Controls.Add(gbThongTin);
            pnlLeft.Controls.Add(gbTimKiem);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(3, 3);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(444, 549);
            pnlLeft.TabIndex = 0;
            // 
            // gbThongTin
            // 
            gbThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbThongTin.Controls.Add(lblMaHoiVien);
            gbThongTin.Controls.Add(txtMaHoiVien);
            gbThongTin.Controls.Add(lblHoTen);
            gbThongTin.Controls.Add(txtHoTen);
            gbThongTin.Controls.Add(lblMatKhau);
            gbThongTin.Controls.Add(txtMatKhau);
            gbThongTin.Controls.Add(lblDienThoai);
            gbThongTin.Controls.Add(txtDienThoai);
            gbThongTin.Controls.Add(lblSoDu);
            gbThongTin.Controls.Add(txtSoDu);
            gbThongTin.Controls.Add(lblTrangThai);
            gbThongTin.Controls.Add(cboTrangThai);
            gbThongTin.Location = new Point(5, 5);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Size = new Size(430, 260);
            gbThongTin.TabIndex = 0;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin hội viên";
            // 
            // lblMaHoiVien
            // 
            lblMaHoiVien.AutoSize = true;
            lblMaHoiVien.Location = new Point(15, 28);
            lblMaHoiVien.Name = "lblMaHoiVien";
            lblMaHoiVien.Size = new Size(86, 20);
            lblMaHoiVien.TabIndex = 0;
            lblMaHoiVien.Text = "Mã hội viên";
            // 
            // txtMaHoiVien
            // 
            txtMaHoiVien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMaHoiVien.Location = new Point(130, 24);
            txtMaHoiVien.Name = "txtMaHoiVien";
            txtMaHoiVien.Size = new Size(285, 27);
            txtMaHoiVien.TabIndex = 1;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(15, 66);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(73, 20);
            lblHoTen.TabIndex = 2;
            lblHoTen.Text = "Họ và tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtHoTen.Location = new Point(130, 62);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(285, 27);
            txtHoTen.TabIndex = 3;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(15, 104);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(70, 20);
            lblMatKhau.TabIndex = 4;
            lblMatKhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMatKhau.Location = new Point(130, 100);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(285, 27);
            txtMatKhau.TabIndex = 5;
            // 
            // lblDienThoai
            // 
            lblDienThoai.AutoSize = true;
            lblDienThoai.Location = new Point(15, 142);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(78, 20);
            lblDienThoai.TabIndex = 6;
            lblDienThoai.Text = "Điện thoại";
            // 
            // txtDienThoai
            // 
            txtDienThoai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDienThoai.Location = new Point(130, 138);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(285, 27);
            txtDienThoai.TabIndex = 7;
            // 
            // lblSoDu
            // 
            lblSoDu.AutoSize = true;
            lblSoDu.Location = new Point(15, 180);
            lblSoDu.Name = "lblSoDu";
            lblSoDu.Size = new Size(93, 20);
            lblSoDu.TabIndex = 8;
            lblSoDu.Text = "Số dư (VNĐ)";
            // 
            // txtSoDu
            // 
            txtSoDu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSoDu.Location = new Point(130, 176);
            txtSoDu.Name = "txtSoDu";
            txtSoDu.Size = new Size(285, 27);
            txtSoDu.TabIndex = 9;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(15, 218);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(75, 20);
            lblTrangThai.TabIndex = 10;
            lblTrangThai.Text = "Trạng thái";
            // 
            // cboTrangThai
            // 
            cboTrangThai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(130, 214);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(285, 28);
            cboTrangThai.TabIndex = 11;
            // 
            // gbTimKiem
            // 
            gbTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTimKiem.Controls.Add(lblTimKiem);
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(btnTimKiem);
            gbTimKiem.Controls.Add(btnLamMoi);
            gbTimKiem.Location = new Point(5, 275);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Size = new Size(430, 160);
            gbTimKiem.TabIndex = 1;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(15, 28);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(216, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Nhập Mã HV, Họ tên hoặc SĐT:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTimKiem.Location = new Point(15, 58);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(400, 27);
            txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(15, 98);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(130, 40);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(155, 98);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(130, 40);
            btnLamMoi.TabIndex = 3;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // gbDanhSach
            // 
            gbDanhSach.Controls.Add(dgvHoiVien);
            gbDanhSach.Controls.Add(pnlSummary);
            gbDanhSach.Dock = DockStyle.Fill;
            gbDanhSach.Location = new Point(453, 3);
            gbDanhSach.Name = "gbDanhSach";
            gbDanhSach.Size = new Size(548, 549);
            gbDanhSach.TabIndex = 1;
            gbDanhSach.TabStop = false;
            gbDanhSach.Text = "Danh sách hội viên";
            // 
            // dgvHoiVien
            // 
            dgvHoiVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoiVien.Dock = DockStyle.Fill;
            dgvHoiVien.Location = new Point(3, 23);
            dgvHoiVien.Name = "dgvHoiVien";
            dgvHoiVien.RowHeadersWidth = 51;
            dgvHoiVien.Size = new Size(542, 488);
            dgvHoiVien.TabIndex = 0;
            // 
            // pnlSummary
            // 
            pnlSummary.Controls.Add(lblTongSoHoiVien);
            pnlSummary.Controls.Add(lblTongSoDu);
            pnlSummary.Dock = DockStyle.Bottom;
            pnlSummary.Location = new Point(3, 511);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(542, 35);
            pnlSummary.TabIndex = 1;
            // 
            // lblTongSoHoiVien
            // 
            lblTongSoHoiVien.AutoSize = true;
            lblTongSoHoiVien.Location = new Point(15, 8);
            lblTongSoHoiVien.Name = "lblTongSoHoiVien";
            lblTongSoHoiVien.Size = new Size(133, 20);
            lblTongSoHoiVien.TabIndex = 0;
            lblTongSoHoiVien.Text = "Tổng số: 0 hội viên";
            // 
            // lblTongSoDu
            // 
            lblTongSoDu.AutoSize = true;
            lblTongSoDu.Location = new Point(220, 8);
            lblTongSoDu.Name = "lblTongSoDu";
            lblTongSoDu.Size = new Size(134, 20);
            lblTongSoDu.TabIndex = 1;
            lblTongSoDu.Text = "Tổng số dư: 0 VNĐ";
            // 
            // pnlChucNang
            // 
            pnlChucNang.Controls.Add(btnThem);
            pnlChucNang.Controls.Add(btnSua);
            pnlChucNang.Controls.Add(btnXoa);
            pnlChucNang.Controls.Add(btnNhapLai);
            pnlChucNang.Controls.Add(btnNapTien);
            pnlChucNang.Controls.Add(btnXuatFile);
            pnlChucNang.Controls.Add(btnDong);
            pnlChucNang.Dock = DockStyle.Bottom;
            pnlChucNang.Location = new Point(0, 600);
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.Size = new Size(1004, 55);
            pnlChucNang.TabIndex = 2;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(10, 8);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(100, 40);
            btnThem.TabIndex = 0;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(116, 8);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 40);
            btnSua.TabIndex = 1;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(222, 8);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 40);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnNhapLai
            // 
            btnNhapLai.Location = new Point(328, 8);
            btnNhapLai.Name = "btnNhapLai";
            btnNhapLai.Size = new Size(100, 40);
            btnNhapLai.TabIndex = 3;
            btnNhapLai.Text = "Nhập lại";
            btnNhapLai.UseVisualStyleBackColor = true;
            // 
            // btnNapTien
            // 
            btnNapTien.Location = new Point(434, 8);
            btnNapTien.Name = "btnNapTien";
            btnNapTien.Size = new Size(100, 40);
            btnNapTien.TabIndex = 4;
            btnNapTien.Text = "Nạp tiền";
            btnNapTien.UseVisualStyleBackColor = true;
            // 
            // btnXuatFile
            // 
            btnXuatFile.Location = new Point(540, 8);
            btnXuatFile.Name = "btnXuatFile";
            btnXuatFile.Size = new Size(100, 40);
            btnXuatFile.TabIndex = 5;
            btnXuatFile.Text = "Xuất File";
            btnXuatFile.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.Location = new Point(892, 8);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(100, 40);
            btnDong.TabIndex = 6;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // frmQuanLyHoiVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 540);
            Controls.Add(tableMain);
            Controls.Add(pnlChucNang);
            Controls.Add(lblTieuDe);
            MinimumSize = new Size(880, 480);
            Name = "frmQuanLyHoiVien";
            Text = "Quản Lý Hội Viên";
            Load += frmQuanLyHoiVien_Load;
            tableMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).EndInit();
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            pnlChucNang.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTieuDe;
        private TableLayoutPanel tableMain;
        private Panel pnlLeft;
        private GroupBox gbThongTin;
        private Label lblMaHoiVien;
        private TextBox txtMaHoiVien;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Label lblDienThoai;
        private TextBox txtDienThoai;
        private Label lblSoDu;
        private TextBox txtSoDu;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;
        private GroupBox gbTimKiem;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private Button btnLamMoi;
        private GroupBox gbDanhSach;
        private DataGridView dgvHoiVien;
        private Panel pnlSummary;
        private Label lblTongSoHoiVien;
        private Label lblTongSoDu;
        private Panel pnlChucNang;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnNhapLai;
        private Button btnNapTien;
        private Button btnXuatFile;
        private Button btnDong;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private SaveFileDialog saveFileDialog;
    }
}
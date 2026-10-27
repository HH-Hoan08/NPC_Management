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
            // -- Cột trái --
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
            // -- Cột phải --
            gbDanhSach = new GroupBox();
            dgvHoiVien = new DataGridView();
            pnlSummary = new Panel();
            lblTongSoHoiVien = new Label();
            lblTongSoDu = new Label();
            // -- Panel nút bấm --
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

            // ──────────────────────────────────────────
            // lblTieuDe
            // ──────────────────────────────────────────
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Height = 40;
            lblTieuDe.Text = "QUẢN LÝ HỘI VIÊN";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.TabIndex = 0;

            // ──────────────────────────────────────────
            // pnlChucNang (Dock Bottom)
            // ──────────────────────────────────────────
            pnlChucNang.Dock = DockStyle.Bottom;
            pnlChucNang.Height = 55;
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.TabIndex = 3;
            pnlChucNang.Controls.Add(btnThem);
            pnlChucNang.Controls.Add(btnSua);
            pnlChucNang.Controls.Add(btnXoa);
            pnlChucNang.Controls.Add(btnNhapLai);
            pnlChucNang.Controls.Add(btnNapTien);
            pnlChucNang.Controls.Add(btnXuatFile);
            pnlChucNang.Controls.Add(btnDong);

            btnThem.Location = new Point(10, 8);
            btnThem.Size = new Size(100, 40);
            btnThem.Text = "Thêm";
            btnThem.TabIndex = 0;
            btnThem.UseVisualStyleBackColor = true;

            btnSua.Location = new Point(118, 8);
            btnSua.Size = new Size(100, 40);
            btnSua.Text = "Sửa";
            btnSua.TabIndex = 1;
            btnSua.UseVisualStyleBackColor = true;

            btnXoa.Location = new Point(226, 8);
            btnXoa.Size = new Size(100, 40);
            btnXoa.Text = "Xóa";
            btnXoa.TabIndex = 2;
            btnXoa.UseVisualStyleBackColor = true;

            btnNhapLai.Location = new Point(334, 8);
            btnNhapLai.Size = new Size(100, 40);
            btnNhapLai.Text = "Nhập lại";
            btnNhapLai.TabIndex = 3;
            btnNhapLai.UseVisualStyleBackColor = true;

            btnNapTien.Location = new Point(442, 8);
            btnNapTien.Size = new Size(100, 40);
            btnNapTien.Text = "Nạp tiền";
            btnNapTien.TabIndex = 4;
            btnNapTien.UseVisualStyleBackColor = true;

            btnXuatFile.Location = new Point(550, 8);
            btnXuatFile.Size = new Size(100, 40);
            btnXuatFile.Text = "Xuất File";
            btnXuatFile.TabIndex = 5;
            btnXuatFile.UseVisualStyleBackColor = true;

            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.Location = new Point(772, 8);
            btnDong.Size = new Size(100, 40);
            btnDong.Text = "Đóng";
            btnDong.TabIndex = 6;
            btnDong.UseVisualStyleBackColor = true;

            // ──────────────────────────────────────────
            // tableMain — chia 2 cột: 35% trái | 65% phải
            // ──────────────────────────────────────────
            tableMain.Dock = DockStyle.Fill;
            tableMain.ColumnCount = 2;
            tableMain.RowCount = 1;
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableMain.Name = "tableMain";
            tableMain.TabIndex = 4;
            tableMain.Padding = new Padding(5, 5, 5, 5);
            tableMain.Controls.Add(pnlLeft, 0, 0);
            tableMain.Controls.Add(gbDanhSach, 1, 0);

            // ──────────────────────────────────────────
            // pnlLeft — panel trái, fill cột TableLayout
            // ──────────────────────────────────────────
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Name = "pnlLeft";
            pnlLeft.TabIndex = 0;
            pnlLeft.Controls.Add(gbTimKiem);
            pnlLeft.Controls.Add(gbThongTin);

            // ──────────────────────────────────────────
            // gbThongTin
            // ──────────────────────────────────────────
            gbThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbThongTin.Location = new Point(0, 0);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Height = 246;
            gbThongTin.TabIndex = 0;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin hội viên";
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

            lblMaHoiVien.AutoSize = true;
            lblMaHoiVien.Location = new Point(10, 28);
            lblMaHoiVien.Text = "Mã hội viên";
            lblMaHoiVien.TabIndex = 0;

            txtMaHoiVien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMaHoiVien.Location = new Point(110, 24);
            txtMaHoiVien.Size = new Size(180, 27);
            txtMaHoiVien.TabIndex = 1;

            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(10, 66);
            lblHoTen.Text = "Họ và tên";
            lblHoTen.TabIndex = 2;

            txtHoTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtHoTen.Location = new Point(110, 62);
            txtHoTen.Size = new Size(180, 27);
            txtHoTen.TabIndex = 3;

            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(10, 104);
            lblMatKhau.Text = "Mật khẩu";
            lblMatKhau.TabIndex = 4;

            txtMatKhau.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMatKhau.Location = new Point(110, 100);
            txtMatKhau.Size = new Size(180, 27);
            txtMatKhau.TabIndex = 5;

            lblDienThoai.AutoSize = true;
            lblDienThoai.Location = new Point(10, 142);
            lblDienThoai.Text = "Điện thoại";
            lblDienThoai.TabIndex = 6;

            txtDienThoai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDienThoai.Location = new Point(110, 138);
            txtDienThoai.Size = new Size(180, 27);
            txtDienThoai.TabIndex = 7;

            lblSoDu.AutoSize = true;
            lblSoDu.Location = new Point(10, 180);
            lblSoDu.Text = "Số dư (VNĐ)";
            lblSoDu.TabIndex = 8;

            txtSoDu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSoDu.Location = new Point(110, 176);
            txtSoDu.Size = new Size(180, 27);
            txtSoDu.TabIndex = 9;

            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(10, 218);
            lblTrangThai.Text = "Trạng thái";
            lblTrangThai.TabIndex = 10;

            cboTrangThai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(110, 214);
            cboTrangThai.Size = new Size(180, 28);
            cboTrangThai.TabIndex = 11;

            // ──────────────────────────────────────────
            // gbTimKiem
            // ──────────────────────────────────────────
            gbTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTimKiem.Location = new Point(0, 254);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Height = 160;
            gbTimKiem.TabIndex = 1;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            gbTimKiem.Controls.Add(lblTimKiem);
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(btnTimKiem);
            gbTimKiem.Controls.Add(btnLamMoi);

            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(10, 30);
            lblTimKiem.Text = "Nhập Mã HV, Họ tên hoặc SĐT:";
            lblTimKiem.TabIndex = 0;

            txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTimKiem.Location = new Point(10, 60);
            txtTimKiem.Size = new Size(270, 27);
            txtTimKiem.TabIndex = 1;

            btnTimKiem.Location = new Point(10, 104);
            btnTimKiem.Size = new Size(120, 40);
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.TabIndex = 2;
            btnTimKiem.UseVisualStyleBackColor = true;

            btnLamMoi.Location = new Point(140, 104);
            btnLamMoi.Size = new Size(120, 40);
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.TabIndex = 3;
            btnLamMoi.UseVisualStyleBackColor = true;

            // ──────────────────────────────────────────
            // gbDanhSach — fill cột phải của tableMain
            // ──────────────────────────────────────────
            gbDanhSach.Dock = DockStyle.Fill;
            gbDanhSach.Name = "gbDanhSach";
            gbDanhSach.TabIndex = 1;
            gbDanhSach.TabStop = false;
            gbDanhSach.Text = "Danh sách hội viên";
            gbDanhSach.Controls.Add(dgvHoiVien);
            gbDanhSach.Controls.Add(pnlSummary);

            // pnlSummary — dock bottom trong gbDanhSach (không nhảy khi resize)
            pnlSummary.Dock = DockStyle.Bottom;
            pnlSummary.Height = 30;
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Controls.Add(lblTongSoHoiVien);
            pnlSummary.Controls.Add(lblTongSoDu);

            lblTongSoHoiVien.AutoSize = true;
            lblTongSoHoiVien.Location = new Point(10, 6);
            lblTongSoHoiVien.Text = "Tổng số: 0 hội viên";
            lblTongSoHoiVien.Name = "lblTongSoHoiVien";
            lblTongSoHoiVien.TabIndex = 0;

            lblTongSoDu.AutoSize = true;
            lblTongSoDu.Location = new Point(220, 6);
            lblTongSoDu.Text = "Tổng số dư: 0 VNĐ";
            lblTongSoDu.Name = "lblTongSoDu";
            lblTongSoDu.TabIndex = 1;

            // DataGridView fill phần còn lại
            dgvHoiVien.Dock = DockStyle.Fill;
            dgvHoiVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoiVien.Name = "dgvHoiVien";
            dgvHoiVien.RowHeadersWidth = 49;
            dgvHoiVien.TabIndex = 0;

            // ──────────────────────────────────────────
            // frmQuanLyHoiVien
            // ──────────────────────────────────────────
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(882, 567);
            Controls.Add(tableMain);
            Controls.Add(pnlChucNang);
            Controls.Add(lblTieuDe);
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
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).EndInit();
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
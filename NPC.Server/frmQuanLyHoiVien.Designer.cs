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
            gbDanhSach = new GroupBox();
            lblTongSoDu = new Label();
            lblTongSoHoiVien = new Label();
            dgvHoiVien = new DataGridView();
            pnlChucNang = new Panel();
            btnDong = new Button();
            btnXuatFile = new Button();
            btnNapTien = new Button();
            btnNhapLai = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            saveFileDialog = new SaveFileDialog();
            gbTimKiem = new GroupBox();
            btnLamMoi = new Button();
            btnTimKiem = new Button();
            txtTimKiem = new TextBox();
            lblTimKiem = new Label();
            gbThongTin = new GroupBox();
            cboTrangThai = new ComboBox();
            txtSoDu = new TextBox();
            lblTrangThai = new Label();
            lblSoDu = new Label();
            txtDienThoai = new TextBox();
            lblDienThoai = new Label();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            txtHoTen = new TextBox();
            txtMaHoiVien = new TextBox();
            lblHoTen = new Label();
            lblMaHoiVien = new Label();
            pnlLeft = new Panel();
            gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).BeginInit();
            pnlChucNang.SuspendLayout();
            gbTimKiem.SuspendLayout();
            gbThongTin.SuspendLayout();
            pnlLeft.SuspendLayout();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTieuDe.Location = new Point(0, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(882, 40);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ HỘI VIÊN";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbDanhSach
            // 
            gbDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbDanhSach.Controls.Add(lblTongSoDu);
            gbDanhSach.Controls.Add(lblTongSoHoiVien);
            gbDanhSach.Controls.Add(dgvHoiVien);
            gbDanhSach.Location = new Point(426, 42);
            gbDanhSach.Name = "gbDanhSach";
            gbDanhSach.Size = new Size(448, 472);
            gbDanhSach.TabIndex = 2;
            gbDanhSach.TabStop = false;
            gbDanhSach.Text = "Danh sách hội viên";
            // 
            // lblTongSoDu
            // 
            lblTongSoDu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongSoDu.AutoSize = true;
            lblTongSoDu.Location = new Point(220, 442);
            lblTongSoDu.Name = "lblTongSoDu";
            lblTongSoDu.Size = new Size(134, 20);
            lblTongSoDu.TabIndex = 2;
            lblTongSoDu.Text = "Tổng số dư: 0 VNĐ";
            // 
            // lblTongSoHoiVien
            // 
            lblTongSoHoiVien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongSoHoiVien.AutoSize = true;
            lblTongSoHoiVien.Location = new Point(10, 442);
            lblTongSoHoiVien.Name = "lblTongSoHoiVien";
            lblTongSoHoiVien.Size = new Size(133, 20);
            lblTongSoHoiVien.TabIndex = 1;
            lblTongSoHoiVien.Text = "Tổng số: 0 hội viên";
            // 
            // dgvHoiVien
            // 
            dgvHoiVien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvHoiVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoiVien.Location = new Point(10, 25);
            dgvHoiVien.Name = "dgvHoiVien";
            dgvHoiVien.RowHeadersWidth = 49;
            dgvHoiVien.Size = new Size(428, 409);
            dgvHoiVien.TabIndex = 0;
            // 
            // pnlChucNang
            // 
            pnlChucNang.Controls.Add(btnDong);
            pnlChucNang.Controls.Add(btnXuatFile);
            pnlChucNang.Controls.Add(btnNapTien);
            pnlChucNang.Controls.Add(btnNhapLai);
            pnlChucNang.Controls.Add(btnXoa);
            pnlChucNang.Controls.Add(btnSua);
            pnlChucNang.Controls.Add(btnThem);
            pnlChucNang.Dock = DockStyle.Bottom;
            pnlChucNang.Location = new Point(0, 512);
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.Size = new Size(882, 55);
            pnlChucNang.TabIndex = 3;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.Location = new Point(772, 8);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(100, 40);
            btnDong.TabIndex = 6;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // btnXuatFile
            // 
            btnXuatFile.Location = new Point(640, 8);
            btnXuatFile.Name = "btnXuatFile";
            btnXuatFile.Size = new Size(100, 40);
            btnXuatFile.TabIndex = 5;
            btnXuatFile.Text = "Xuất File";
            btnXuatFile.UseVisualStyleBackColor = true;
            // 
            // btnNapTien
            // 
            btnNapTien.Location = new Point(514, 8);
            btnNapTien.Name = "btnNapTien";
            btnNapTien.Size = new Size(100, 40);
            btnNapTien.TabIndex = 4;
            btnNapTien.Text = "Nạp tiền";
            btnNapTien.UseVisualStyleBackColor = true;
            // 
            // btnNhapLai
            // 
            btnNhapLai.Location = new Point(388, 8);
            btnNhapLai.Name = "btnNhapLai";
            btnNhapLai.Size = new Size(100, 40);
            btnNhapLai.TabIndex = 3;
            btnNhapLai.Text = "Nhập lại";
            btnNhapLai.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(262, 8);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 40);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(136, 8);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 40);
            btnSua.TabIndex = 1;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
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
            // gbTimKiem
            // 
            gbTimKiem.Controls.Add(btnLamMoi);
            gbTimKiem.Controls.Add(btnTimKiem);
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(lblTimKiem);
            gbTimKiem.Location = new Point(0, 270);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Size = new Size(410, 202);
            gbTimKiem.TabIndex = 1;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(230, 105);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(160, 42);
            btnLamMoi.TabIndex = 3;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(20, 105);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(160, 42);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(20, 60);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(370, 27);
            txtTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(15, 30);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(216, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Nhập Mã HV, Họ tên hoặc SĐT:";
            // 
            // gbThongTin
            // 
            gbThongTin.Controls.Add(cboTrangThai);
            gbThongTin.Controls.Add(txtSoDu);
            gbThongTin.Controls.Add(lblTrangThai);
            gbThongTin.Controls.Add(lblSoDu);
            gbThongTin.Controls.Add(txtDienThoai);
            gbThongTin.Controls.Add(lblDienThoai);
            gbThongTin.Controls.Add(lblMatKhau);
            gbThongTin.Controls.Add(txtMatKhau);
            gbThongTin.Controls.Add(txtHoTen);
            gbThongTin.Controls.Add(txtMaHoiVien);
            gbThongTin.Controls.Add(lblHoTen);
            gbThongTin.Controls.Add(lblMaHoiVien);
            gbThongTin.Location = new Point(0, 0);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Size = new Size(410, 260);
            gbThongTin.TabIndex = 0;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin hội viên";
            // 
            // cboTrangThai
            // 
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(110, 210);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(280, 28);
            cboTrangThai.TabIndex = 11;
            // 
            // txtSoDu
            // 
            txtSoDu.Location = new Point(110, 172);
            txtSoDu.Name = "txtSoDu";
            txtSoDu.Size = new Size(280, 27);
            txtSoDu.TabIndex = 10;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(12, 214);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(75, 20);
            lblTrangThai.TabIndex = 9;
            lblTrangThai.Text = "Trạng thái";
            // 
            // lblSoDu
            // 
            lblSoDu.AutoSize = true;
            lblSoDu.Location = new Point(12, 176);
            lblSoDu.Name = "lblSoDu";
            lblSoDu.Size = new Size(93, 20);
            lblSoDu.TabIndex = 8;
            lblSoDu.Text = "Số dư (VND)";
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(110, 134);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(280, 27);
            txtDienThoai.TabIndex = 7;
            // 
            // lblDienThoai
            // 
            lblDienThoai.AutoSize = true;
            lblDienThoai.Location = new Point(12, 138);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(78, 20);
            lblDienThoai.TabIndex = 6;
            lblDienThoai.Text = "Điện thoại";
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(12, 100);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(70, 20);
            lblMatKhau.TabIndex = 5;
            lblMatKhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(110, 96);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(280, 27);
            txtMatKhau.TabIndex = 4;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(110, 58);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(280, 27);
            txtHoTen.TabIndex = 3;
            // 
            // txtMaHoiVien
            // 
            txtMaHoiVien.Location = new Point(110, 20);
            txtMaHoiVien.Name = "txtMaHoiVien";
            txtMaHoiVien.Size = new Size(280, 27);
            txtMaHoiVien.TabIndex = 2;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(12, 62);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(73, 20);
            lblHoTen.TabIndex = 1;
            lblHoTen.Text = "Họ và tên";
            // 
            // lblMaHoiVien
            // 
            lblMaHoiVien.AutoSize = true;
            lblMaHoiVien.Location = new Point(12, 24);
            lblMaHoiVien.Name = "lblMaHoiVien";
            lblMaHoiVien.Size = new Size(86, 20);
            lblMaHoiVien.TabIndex = 0;
            lblMaHoiVien.Text = "Mã hội viên";
            // 
            // pnlLeft
            // 
            pnlLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlLeft.Controls.Add(gbThongTin);
            pnlLeft.Controls.Add(gbTimKiem);
            pnlLeft.Location = new Point(10, 42);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(410, 461);
            pnlLeft.TabIndex = 1;
            // 
            // frmQuanLyHoiVien
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(882, 567);
            Controls.Add(gbDanhSach);
            Controls.Add(pnlLeft);
            Controls.Add(pnlChucNang);
            Controls.Add(lblTieuDe);
            Name = "frmQuanLyHoiVien";
            Text = "Quản Lý Hội Viên";
            Load += frmQuanLyHoiVien_Load;
            gbDanhSach.ResumeLayout(false);
            gbDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).EndInit();
            pnlChucNang.ResumeLayout(false);
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            pnlLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTieuDe;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private Panel pnlChucNang;
        private Button btnDong;
        private Button btnXuatFile;
        private Button btnNapTien;
        private Button btnNhapLai;
        private Button btnXoa;
        private Button btnSua;
        private Button btnThem;
        private GroupBox gbDanhSach;
        private Label lblTongSoDu;
        private Label lblTongSoHoiVien;
        private DataGridView dgvHoiVien;
        private SaveFileDialog saveFileDialog;
        private GroupBox gbTimKiem;
        private Button btnLamMoi;
        private Button btnTimKiem;
        private TextBox txtTimKiem;
        private Label lblTimKiem;
        private GroupBox gbThongTin;
        private ComboBox cboTrangThai;
        private TextBox txtSoDu;
        private Label lblTrangThai;
        private Label lblSoDu;
        private TextBox txtDienThoai;
        private Label lblDienThoai;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private TextBox txtHoTen;
        private TextBox txtMaHoiVien;
        private Label lblHoTen;
        private Label lblMaHoiVien;
        private Panel pnlLeft;
    }
}
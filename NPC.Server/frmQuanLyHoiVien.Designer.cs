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
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            gbTimKiem = new GroupBox();
            btnLamMoi = new Button();
            btnTimKiem = new Button();
            txtTimKiem = new TextBox();
            lblTimKiem = new Label();
            pnlChucNang = new Panel();
            btnDong = new Button();
            btnXuatFile = new Button();
            btnNapTien = new Button();
            btnNhapLai = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            gbDanhSach = new GroupBox();
            lblTongSoDu = new Label();
            lblTongSoHoiVien = new Label();
            dgvHoiVien = new DataGridView();
            saveFileDialog = new SaveFileDialog();
            gbThongTin.SuspendLayout();
            gbTimKiem.SuspendLayout();
            pnlChucNang.SuspendLayout();
            gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 13.7739134F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDe.Location = new Point(444, 9);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(217, 30);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ HỘI VIÊN ";
            // 
            // gbThongTin
            // 
            gbThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
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
            gbThongTin.Location = new Point(12, 55);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Size = new Size(1069, 113);
            gbThongTin.TabIndex = 1;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin hội viên";
            // 
            // cboTrangThai
            // 
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(807, 64);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(198, 27);
            cboTrangThai.TabIndex = 11;
            // 
            // txtSoDu
            // 
            txtSoDu.Location = new Point(807, 32);
            txtSoDu.Name = "txtSoDu";
            txtSoDu.Size = new Size(198, 26);
            txtSoDu.TabIndex = 10;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(698, 67);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(75, 20);
            lblTrangThai.TabIndex = 9;
            lblTrangThai.Text = "Trạng thái";
            // 
            // lblSoDu
            // 
            lblSoDu.AutoSize = true;
            lblSoDu.Location = new Point(698, 35);
            lblSoDu.Name = "lblSoDu";
            lblSoDu.Size = new Size(93, 20);
            lblSoDu.TabIndex = 8;
            lblSoDu.Text = "Số dư (VND)";
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(432, 64);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(207, 26);
            txtDienThoai.TabIndex = 7;
            // 
            // lblDienThoai
            // 
            lblDienThoai.AutoSize = true;
            lblDienThoai.Location = new Point(345, 67);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(81, 20);
            lblDienThoai.TabIndex = 6;
            lblDienThoai.Text = "Điện Thoại";
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(6, 67);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(70, 20);
            lblMatKhau.TabIndex = 5;
            lblMatKhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(98, 64);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(207, 26);
            txtMatKhau.TabIndex = 4;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(432, 32);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(207, 26);
            txtHoTen.TabIndex = 3;
            // 
            // txtMaHoiVien
            // 
            txtMaHoiVien.Location = new Point(98, 32);
            txtMaHoiVien.Name = "txtMaHoiVien";
            txtMaHoiVien.Size = new Size(207, 26);
            txtMaHoiVien.TabIndex = 2;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(345, 35);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(73, 20);
            lblHoTen.TabIndex = 1;
            lblHoTen.Text = "Họ và tên";
            // 
            // lblMaHoiVien
            // 
            lblMaHoiVien.AutoSize = true;
            lblMaHoiVien.Location = new Point(6, 35);
            lblMaHoiVien.Name = "lblMaHoiVien";
            lblMaHoiVien.Size = new Size(86, 20);
            lblMaHoiVien.TabIndex = 0;
            lblMaHoiVien.Text = "Mã hội viên";
            // 
            // gbTimKiem
            // 
            gbTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTimKiem.Controls.Add(btnLamMoi);
            gbTimKiem.Controls.Add(btnTimKiem);
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(lblTimKiem);
            gbTimKiem.Location = new Point(12, 174);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Size = new Size(1069, 87);
            gbTimKiem.TabIndex = 2;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(618, 31);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(122, 36);
            btnLamMoi.TabIndex = 3;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(490, 31);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(122, 36);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(245, 31);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(207, 26);
            txtTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(6, 34);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(216, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Nhập Mã HV, Họ tên hoặc SĐT:";
            // 
            // pnlChucNang
            // 
            pnlChucNang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlChucNang.Controls.Add(btnDong);
            pnlChucNang.Controls.Add(btnXuatFile);
            pnlChucNang.Controls.Add(btnNapTien);
            pnlChucNang.Controls.Add(btnNhapLai);
            pnlChucNang.Controls.Add(btnXoa);
            pnlChucNang.Controls.Add(btnSua);
            pnlChucNang.Controls.Add(btnThem);
            pnlChucNang.Location = new Point(12, 615);
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.Size = new Size(1069, 61);
            pnlChucNang.TabIndex = 3;
            // 
            // btnDong
            // 
            btnDong.Location = new Point(800, 3);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(99, 44);
            btnDong.TabIndex = 6;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // btnXuatFile
            // 
            btnXuatFile.Location = new Point(695, 3);
            btnXuatFile.Name = "btnXuatFile";
            btnXuatFile.Size = new Size(99, 44);
            btnXuatFile.TabIndex = 5;
            btnXuatFile.Text = "Xuất File";
            btnXuatFile.UseVisualStyleBackColor = true;
            // 
            // btnNapTien
            // 
            btnNapTien.Location = new Point(590, 3);
            btnNapTien.Name = "btnNapTien";
            btnNapTien.Size = new Size(99, 44);
            btnNapTien.TabIndex = 4;
            btnNapTien.Text = "Nạp tiền";
            btnNapTien.UseVisualStyleBackColor = true;
            // 
            // btnNhapLai
            // 
            btnNhapLai.Location = new Point(485, 3);
            btnNhapLai.Name = "btnNhapLai";
            btnNhapLai.Size = new Size(99, 44);
            btnNhapLai.TabIndex = 3;
            btnNhapLai.Text = "Nhập lại";
            btnNhapLai.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(380, 3);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(99, 44);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(275, 3);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(99, 44);
            btnSua.TabIndex = 1;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(170, 3);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(99, 44);
            btnThem.TabIndex = 0;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // gbDanhSach
            // 
            gbDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDanhSach.Controls.Add(lblTongSoDu);
            gbDanhSach.Controls.Add(lblTongSoHoiVien);
            gbDanhSach.Controls.Add(dgvHoiVien);
            gbDanhSach.Location = new Point(12, 267);
            gbDanhSach.Name = "gbDanhSach";
            gbDanhSach.Size = new Size(1069, 342);
            gbDanhSach.TabIndex = 4;
            gbDanhSach.TabStop = false;
            gbDanhSach.Text = "Danh sách hội viên";
            // 
            // lblTongSoDu
            // 
            lblTongSoDu.AutoSize = true;
            lblTongSoDu.Location = new Point(8, 310);
            lblTongSoDu.Name = "lblTongSoDu";
            lblTongSoDu.Size = new Size(134, 20);
            lblTongSoDu.TabIndex = 2;
            lblTongSoDu.Text = "Tổng số dư: 0 VNĐ";
            // 
            // lblTongSoHoiVien
            // 
            lblTongSoHoiVien.AutoSize = true;
            lblTongSoHoiVien.Location = new Point(8, 279);
            lblTongSoHoiVien.Name = "lblTongSoHoiVien";
            lblTongSoHoiVien.Size = new Size(133, 20);
            lblTongSoHoiVien.TabIndex = 1;
            lblTongSoHoiVien.Text = "Tổng số: 0 hội viên";
            // 
            // dgvHoiVien
            // 
            dgvHoiVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoiVien.Location = new Point(6, 25);
            dgvHoiVien.Name = "dgvHoiVien";
            dgvHoiVien.RowHeadersWidth = 49;
            dgvHoiVien.Size = new Size(1057, 241);
            dgvHoiVien.TabIndex = 0;
            // 
            // frmQuanLyHoiVien
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1091, 703);
            Controls.Add(gbDanhSach);
            Controls.Add(pnlChucNang);
            Controls.Add(gbTimKiem);
            Controls.Add(gbThongTin);
            Controls.Add(lblTieuDe);
            Name = "frmQuanLyHoiVien";
            Text = "Quản Lý Hội Viên";
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            pnlChucNang.ResumeLayout(false);
            gbDanhSach.ResumeLayout(false);
            gbDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoiVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private GroupBox gbThongTin;
        private Label lblHoTen;
        private Label lblMaHoiVien;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private TextBox txtHoTen;
        private TextBox txtMaHoiVien;
        private TextBox txtDienThoai;
        private Label lblDienThoai;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private TextBox txtSoDu;
        private Label lblTrangThai;
        private Label lblSoDu;
        private ComboBox cboTrangThai;
        private GroupBox gbTimKiem;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Button btnLamMoi;
        private Button btnTimKiem;
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
    }
}
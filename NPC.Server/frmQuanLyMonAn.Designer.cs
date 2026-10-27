namespace NPC.Server
{
    partial class frmQuanLyMonAn
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
            pnlLeft = new Panel();
            gbThongTin = new GroupBox();
            cboDonViTinh = new ComboBox();
            txtSoLuongTon = new TextBox();
            lblDonViTinh = new Label();
            lblSoLuongTon = new Label();
            cboTrangThai = new ComboBox();
            txtGiaNhap = new TextBox();
            txtGiaBan = new TextBox();
            lblTrangThai = new Label();
            lblGiaNhap = new Label();
            lblGiaBan = new Label();
            cboLoaiMon = new ComboBox();
            txtTenMon = new TextBox();
            txtMaMon = new TextBox();
            lblLoaiMon = new Label();
            lblTenMon = new Label();
            lblMaMon = new Label();
            gbHinhAnh = new GroupBox();
            btnXoaAnh = new Button();
            btnChonAnh = new Button();
            picHinhAnh = new PictureBox();
            gbTimKiem = new GroupBox();
            btnLamMoi = new Button();
            chkTonKhoThap = new CheckBox();
            cboTimKiemLoai = new ComboBox();
            lblTimKiemLoai = new Label();
            txtTimKiem = new TextBox();
            lblTimKiem = new Label();
            gbDanhSach = new GroupBox();
            lblCanhBao = new Label();
            lblTongTonKho = new Label();
            lblTongSoMon = new Label();
            dgvMonAn = new DataGridView();
            pnlChucNang = new Panel();
            btnDong = new Button();
            btnNhapKho = new Button();
            btnNhapLai = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            openFileDialog = new OpenFileDialog();
            pnlLeft.SuspendLayout();
            gbThongTin.SuspendLayout();
            gbHinhAnh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).BeginInit();
            gbTimKiem.SuspendLayout();
            gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonAn).BeginInit();
            pnlChucNang.SuspendLayout();
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
            lblTieuDe.Text = "QUẢN LÝ THỰC ĐƠN VÀ KIỂM KÊ KHO HÀNG";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlLeft
            // 
            pnlLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlLeft.Controls.Add(gbThongTin);
            pnlLeft.Controls.Add(gbHinhAnh);
            pnlLeft.Controls.Add(gbTimKiem);
            pnlLeft.Location = new Point(10, 42);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(410, 456);
            pnlLeft.TabIndex = 1;
            // 
            // gbThongTin
            // 
            gbThongTin.Controls.Add(cboDonViTinh);
            gbThongTin.Controls.Add(txtSoLuongTon);
            gbThongTin.Controls.Add(lblDonViTinh);
            gbThongTin.Controls.Add(lblSoLuongTon);
            gbThongTin.Controls.Add(cboTrangThai);
            gbThongTin.Controls.Add(txtGiaNhap);
            gbThongTin.Controls.Add(txtGiaBan);
            gbThongTin.Controls.Add(lblTrangThai);
            gbThongTin.Controls.Add(lblGiaNhap);
            gbThongTin.Controls.Add(lblGiaBan);
            gbThongTin.Controls.Add(cboLoaiMon);
            gbThongTin.Controls.Add(txtTenMon);
            gbThongTin.Controls.Add(txtMaMon);
            gbThongTin.Controls.Add(lblLoaiMon);
            gbThongTin.Controls.Add(lblTenMon);
            gbThongTin.Controls.Add(lblMaMon);
            gbThongTin.Location = new Point(0, 0);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Size = new Size(410, 205);
            gbThongTin.TabIndex = 0;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin món ăn và kho";
            // 
            // cboDonViTinh
            // 
            cboDonViTinh.FormattingEnabled = true;
            cboDonViTinh.Location = new Point(285, 122);
            cboDonViTinh.Name = "cboDonViTinh";
            cboDonViTinh.Size = new Size(115, 28);
            cboDonViTinh.TabIndex = 15;
            // 
            // txtSoLuongTon
            // 
            txtSoLuongTon.Location = new Point(285, 90);
            txtSoLuongTon.Name = "txtSoLuongTon";
            txtSoLuongTon.Size = new Size(115, 27);
            txtSoLuongTon.TabIndex = 14;
            txtSoLuongTon.TextChanged += txtSoLuongTon_TextChanged;
            // 
            // lblDonViTinh
            // 
            lblDonViTinh.AutoSize = true;
            lblDonViTinh.Location = new Point(205, 126);
            lblDonViTinh.Name = "lblDonViTinh";
            lblDonViTinh.Size = new Size(81, 20);
            lblDonViTinh.TabIndex = 13;
            lblDonViTinh.Text = "Đơn vị tính";
            // 
            // lblSoLuongTon
            // 
            lblSoLuongTon.AutoSize = true;
            lblSoLuongTon.Location = new Point(205, 93);
            lblSoLuongTon.Name = "lblSoLuongTon";
            lblSoLuongTon.Size = new Size(50, 20);
            lblSoLuongTon.TabIndex = 12;
            lblSoLuongTon.Text = "SL tồn";
            // 
            // cboTrangThai
            // 
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(285, 156);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(115, 28);
            cboTrangThai.TabIndex = 11;
            // 
            // txtGiaNhap
            // 
            txtGiaNhap.Location = new Point(285, 57);
            txtGiaNhap.Name = "txtGiaNhap";
            txtGiaNhap.Size = new Size(115, 27);
            txtGiaNhap.TabIndex = 10;
            // 
            // txtGiaBan
            // 
            txtGiaBan.Location = new Point(285, 25);
            txtGiaBan.Name = "txtGiaBan";
            txtGiaBan.Size = new Size(115, 27);
            txtGiaBan.TabIndex = 9;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(205, 160);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(75, 20);
            lblTrangThai.TabIndex = 8;
            lblTrangThai.Text = "Trạng thái";
            // 
            // lblGiaNhap
            // 
            lblGiaNhap.AutoSize = true;
            lblGiaNhap.Location = new Point(205, 60);
            lblGiaNhap.Name = "lblGiaNhap";
            lblGiaNhap.Size = new Size(68, 20);
            lblGiaNhap.TabIndex = 7;
            lblGiaNhap.Text = "Giá nhập";
            // 
            // lblGiaBan
            // 
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(205, 28);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(60, 20);
            lblGiaBan.TabIndex = 6;
            lblGiaBan.Text = "Giá bán";
            // 
            // cboLoaiMon
            // 
            cboLoaiMon.FormattingEnabled = true;
            cboLoaiMon.Location = new Point(80, 90);
            cboLoaiMon.Name = "cboLoaiMon";
            cboLoaiMon.Size = new Size(115, 28);
            cboLoaiMon.TabIndex = 5;
            // 
            // txtTenMon
            // 
            txtTenMon.Location = new Point(80, 57);
            txtTenMon.Name = "txtTenMon";
            txtTenMon.Size = new Size(115, 27);
            txtTenMon.TabIndex = 4;
            // 
            // txtMaMon
            // 
            txtMaMon.Location = new Point(80, 25);
            txtMaMon.Name = "txtMaMon";
            txtMaMon.Size = new Size(115, 27);
            txtMaMon.TabIndex = 3;
            txtMaMon.TextChanged += txtMaMon_TextChanged;
            // 
            // lblLoaiMon
            // 
            lblLoaiMon.AutoSize = true;
            lblLoaiMon.Location = new Point(8, 93);
            lblLoaiMon.Name = "lblLoaiMon";
            lblLoaiMon.Size = new Size(71, 20);
            lblLoaiMon.TabIndex = 2;
            lblLoaiMon.Text = "Loại món";
            // 
            // lblTenMon
            // 
            lblTenMon.AutoSize = true;
            lblTenMon.Location = new Point(8, 60);
            lblTenMon.Name = "lblTenMon";
            lblTenMon.Size = new Size(66, 20);
            lblTenMon.TabIndex = 1;
            lblTenMon.Text = "Tên món";
            // 
            // lblMaMon
            // 
            lblMaMon.AutoSize = true;
            lblMaMon.Location = new Point(8, 28);
            lblMaMon.Name = "lblMaMon";
            lblMaMon.Size = new Size(64, 20);
            lblMaMon.TabIndex = 0;
            lblMaMon.Text = "Mã món";
            // 
            // gbHinhAnh
            // 
            gbHinhAnh.Controls.Add(btnXoaAnh);
            gbHinhAnh.Controls.Add(btnChonAnh);
            gbHinhAnh.Controls.Add(picHinhAnh);
            gbHinhAnh.Location = new Point(0, 210);
            gbHinhAnh.Name = "gbHinhAnh";
            gbHinhAnh.Size = new Size(410, 125);
            gbHinhAnh.TabIndex = 1;
            gbHinhAnh.TabStop = false;
            gbHinhAnh.Text = "Hình ảnh món";
            // 
            // btnXoaAnh
            // 
            btnXoaAnh.Location = new Point(310, 72);
            btnXoaAnh.Name = "btnXoaAnh";
            btnXoaAnh.Size = new Size(90, 38);
            btnXoaAnh.TabIndex = 2;
            btnXoaAnh.Text = "Xóa ảnh";
            btnXoaAnh.UseVisualStyleBackColor = true;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Location = new Point(310, 26);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(90, 38);
            btnChonAnh.TabIndex = 1;
            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.UseVisualStyleBackColor = true;
            // 
            // picHinhAnh
            // 
            picHinhAnh.BorderStyle = BorderStyle.FixedSingle;
            picHinhAnh.Location = new Point(8, 25);
            picHinhAnh.Name = "picHinhAnh";
            picHinhAnh.Size = new Size(290, 90);
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picHinhAnh.TabIndex = 0;
            picHinhAnh.TabStop = false;
            // 
            // gbTimKiem
            // 
            gbTimKiem.Controls.Add(btnLamMoi);
            gbTimKiem.Controls.Add(chkTonKhoThap);
            gbTimKiem.Controls.Add(cboTimKiemLoai);
            gbTimKiem.Controls.Add(lblTimKiemLoai);
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(lblTimKiem);
            gbTimKiem.Location = new Point(0, 340);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Size = new Size(410, 116);
            gbTimKiem.TabIndex = 2;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(310, 22);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 60);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // chkTonKhoThap
            // 
            chkTonKhoThap.AutoSize = true;
            chkTonKhoThap.Location = new Point(90, 58);
            chkTonKhoThap.Name = "chkTonKhoThap";
            chkTonKhoThap.Size = new Size(146, 24);
            chkTonKhoThap.TabIndex = 4;
            chkTonKhoThap.Text = "Cảnh báo sắp hết";
            chkTonKhoThap.UseVisualStyleBackColor = true;
            // 
            // cboTimKiemLoai
            // 
            cboTimKiemLoai.FormattingEnabled = true;
            cboTimKiemLoai.Location = new Point(190, 22);
            cboTimKiemLoai.Name = "cboTimKiemLoai";
            cboTimKiemLoai.Size = new Size(110, 28);
            cboTimKiemLoai.TabIndex = 3;
            cboTimKiemLoai.Text = "-- Tất cả --";
            // 
            // lblTimKiemLoai
            // 
            lblTimKiemLoai.AutoSize = true;
            lblTimKiemLoai.Location = new Point(150, 26);
            lblTimKiemLoai.Name = "lblTimKiemLoai";
            lblTimKiemLoai.Size = new Size(37, 20);
            lblTimKiemLoai.TabIndex = 2;
            lblTimKiemLoai.Text = "Loại";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(50, 22);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(95, 27);
            txtTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(8, 26);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(34, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm";
            // 
            // gbDanhSach
            // 
            gbDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbDanhSach.Controls.Add(lblCanhBao);
            gbDanhSach.Controls.Add(lblTongTonKho);
            gbDanhSach.Controls.Add(lblTongSoMon);
            gbDanhSach.Controls.Add(dgvMonAn);
            gbDanhSach.Location = new Point(426, 42);
            gbDanhSach.Name = "gbDanhSach";
            gbDanhSach.Size = new Size(434, 456);
            gbDanhSach.TabIndex = 2;
            gbDanhSach.TabStop = false;
            gbDanhSach.Text = "Danh sách thực đơn và kho";
            // 
            // lblCanhBao
            // 
            lblCanhBao.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCanhBao.AutoSize = true;
            lblCanhBao.Location = new Point(290, 426);
            lblCanhBao.Name = "lblCanhBao";
            lblCanhBao.Size = new Size(111, 20);
            lblCanhBao.TabIndex = 3;
            lblCanhBao.Text = "Sắp hết hàng: 0";
            // 
            // lblTongTonKho
            // 
            lblTongTonKho.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongTonKho.AutoSize = true;
            lblTongTonKho.Location = new Point(135, 426);
            lblTongTonKho.Name = "lblTongTonKho";
            lblTongTonKho.Size = new Size(84, 20);
            lblTongTonKho.TabIndex = 2;
            lblTongTonKho.Text = "Tổng tồn: 0";
            // 
            // lblTongSoMon
            // 
            lblTongSoMon.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTongSoMon.AutoSize = true;
            lblTongSoMon.Location = new Point(10, 426);
            lblTongSoMon.Name = "lblTongSoMon";
            lblTongSoMon.Size = new Size(111, 20);
            lblTongSoMon.TabIndex = 1;
            lblTongSoMon.Text = "Tổng số món: 0";
            // 
            // dgvMonAn
            // 
            dgvMonAn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMonAn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMonAn.Location = new Point(10, 25);
            dgvMonAn.Name = "dgvMonAn";
            dgvMonAn.RowHeadersWidth = 49;
            dgvMonAn.Size = new Size(414, 393);
            dgvMonAn.TabIndex = 0;
            // 
            // pnlChucNang
            // 
            pnlChucNang.Controls.Add(btnDong);
            pnlChucNang.Controls.Add(btnNhapKho);
            pnlChucNang.Controls.Add(btnNhapLai);
            pnlChucNang.Controls.Add(btnXoa);
            pnlChucNang.Controls.Add(btnSua);
            pnlChucNang.Controls.Add(btnThem);
            pnlChucNang.Dock = DockStyle.Bottom;
            pnlChucNang.Location = new Point(0, 504);
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.Size = new Size(882, 55);
            pnlChucNang.TabIndex = 3;
            // 
            // btnDong
            // 
            btnDong.Location = new Point(762, 8);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(110, 40);
            btnDong.TabIndex = 5;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // btnNhapKho
            // 
            btnNhapKho.Location = new Point(611, 8);
            btnNhapKho.Name = "btnNhapKho";
            btnNhapKho.Size = new Size(130, 40);
            btnNhapKho.TabIndex = 4;
            btnNhapKho.Text = "Nhập thêm kho";
            btnNhapKho.UseVisualStyleBackColor = true;
            // 
            // btnNhapLai
            // 
            btnNhapLai.Location = new Point(464, 8);
            btnNhapLai.Name = "btnNhapLai";
            btnNhapLai.Size = new Size(120, 40);
            btnNhapLai.TabIndex = 3;
            btnNhapLai.Text = "Nhập lại";
            btnNhapLai.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(320, 8);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(120, 40);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(170, 8);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(120, 40);
            btnSua.TabIndex = 1;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(18, 8);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(120, 40);
            btnThem.TabIndex = 0;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            // 
            // frmQuanLyMonAn
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(882, 559);
            Controls.Add(gbDanhSach);
            Controls.Add(pnlLeft);
            Controls.Add(pnlChucNang);
            Controls.Add(lblTieuDe);
            Name = "frmQuanLyMonAn";
            Text = "Quản lý món ăn";
            pnlLeft.ResumeLayout(false);
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            gbHinhAnh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).EndInit();
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            gbDanhSach.ResumeLayout(false);
            gbDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonAn).EndInit();
            pnlChucNang.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTieuDe;
        private Panel pnlLeft;
        private GroupBox gbThongTin;
        private TextBox txtTenMon;
        private TextBox txtMaMon;
        private Label lblLoaiMon;
        private Label lblTenMon;
        private Label lblMaMon;
        private ComboBox cboLoaiMon;
        private ComboBox cboTrangThai;
        private TextBox txtGiaNhap;
        private TextBox txtGiaBan;
        private Label lblTrangThai;
        private Label lblGiaNhap;
        private Label lblGiaBan;
        private ComboBox cboDonViTinh;
        private TextBox txtSoLuongTon;
        private Label lblDonViTinh;
        private Label lblSoLuongTon;
        private GroupBox gbHinhAnh;
        private PictureBox picHinhAnh;
        private Button btnXoaAnh;
        private Button btnChonAnh;
        private GroupBox gbTimKiem;
        private CheckBox chkTonKhoThap;
        private ComboBox cboTimKiemLoai;
        private Label lblTimKiemLoai;
        private TextBox txtTimKiem;
        private Label lblTimKiem;
        private Button btnLamMoi;
        private GroupBox gbDanhSach;
        private Label lblCanhBao;
        private Label lblTongTonKho;
        private Label lblTongSoMon;
        private DataGridView dgvMonAn;
        private Panel pnlChucNang;
        private Button btnDong;
        private Button btnNhapKho;
        private Button btnNhapLai;
        private Button btnXoa;
        private Button btnSua;
        private Button btnThem;
        private OpenFileDialog openFileDialog;
    }
}
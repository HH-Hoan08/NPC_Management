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
            tableMain = new TableLayoutPanel();
            pnlLeft = new Panel();
            gbThongTin = new GroupBox();
            lblMaMon = new Label();
            txtMaMon = new TextBox();
            lblTenMon = new Label();
            txtTenMon = new TextBox();
            lblLoaiMon = new Label();
            cboLoaiMon = new ComboBox();
            lblGiaBan = new Label();
            txtGiaBan = new TextBox();
            lblGiaNhap = new Label();
            txtGiaNhap = new TextBox();
            lblSoLuongTon = new Label();
            txtSoLuongTon = new TextBox();
            lblDonViTinh = new Label();
            cboDonViTinh = new ComboBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            gbHinhAnh = new GroupBox();
            picHinhAnh = new PictureBox();
            btnChonAnh = new Button();
            btnXoaAnh = new Button();
            gbTimKiem = new GroupBox();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblTimKiemLoai = new Label();
            cboTimKiemLoai = new ComboBox();
            chkTonKhoThap = new CheckBox();
            btnLamMoi = new Button();
            gbDanhSach = new GroupBox();
            dgvMonAn = new DataGridView();
            pnlSummary = new Panel();
            lblTongSoMon = new Label();
            lblTongTonKho = new Label();
            lblCanhBao = new Label();
            pnlChucNang = new Panel();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnNhapLai = new Button();
            btnNhapKho = new Button();
            btnDong = new Button();
            openFileDialog = new OpenFileDialog();
            tableMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            gbThongTin.SuspendLayout();
            gbHinhAnh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).BeginInit();
            gbTimKiem.SuspendLayout();
            gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonAn).BeginInit();
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
            lblTieuDe.Size = new Size(1000, 43);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ THỰC ĐƠN VÀ KIỂM KÊ KHO HÀNG";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableMain
            // 
            tableMain.ColumnCount = 2;
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 490F));
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableMain.Controls.Add(pnlLeft, 0, 0);
            tableMain.Controls.Add(gbDanhSach, 1, 0);
            tableMain.Dock = DockStyle.Fill;
            tableMain.Location = new Point(0, 43);
            tableMain.Name = "tableMain";
            tableMain.RowCount = 1;
            tableMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableMain.Size = new Size(1000, 447);
            tableMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.AutoScroll = true;
            pnlLeft.Controls.Add(gbThongTin);
            pnlLeft.Controls.Add(gbHinhAnh);
            pnlLeft.Controls.Add(gbTimKiem);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(3, 3);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(484, 441);
            pnlLeft.TabIndex = 0;
            // 
            // gbThongTin
            // 
            gbThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbThongTin.Controls.Add(lblMaMon);
            gbThongTin.Controls.Add(txtMaMon);
            gbThongTin.Controls.Add(lblTenMon);
            gbThongTin.Controls.Add(txtTenMon);
            gbThongTin.Controls.Add(lblLoaiMon);
            gbThongTin.Controls.Add(cboLoaiMon);
            gbThongTin.Controls.Add(lblGiaBan);
            gbThongTin.Controls.Add(txtGiaBan);
            gbThongTin.Controls.Add(lblGiaNhap);
            gbThongTin.Controls.Add(txtGiaNhap);
            gbThongTin.Controls.Add(lblSoLuongTon);
            gbThongTin.Controls.Add(txtSoLuongTon);
            gbThongTin.Controls.Add(lblDonViTinh);
            gbThongTin.Controls.Add(cboDonViTinh);
            gbThongTin.Controls.Add(lblTrangThai);
            gbThongTin.Controls.Add(cboTrangThai);
            gbThongTin.Location = new Point(5, 5);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Size = new Size(450, 204);
            gbThongTin.TabIndex = 0;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin món ăn và kho";
            // 
            // lblMaMon
            // 
            lblMaMon.AutoSize = true;
            lblMaMon.Location = new Point(12, 27);
            lblMaMon.Name = "lblMaMon";
            lblMaMon.Size = new Size(64, 20);
            lblMaMon.TabIndex = 0;
            lblMaMon.Text = "Mã món";
            // 
            // txtMaMon
            // 
            txtMaMon.Location = new Point(85, 23);
            txtMaMon.Name = "txtMaMon";
            txtMaMon.Size = new Size(130, 26);
            txtMaMon.TabIndex = 1;
            txtMaMon.TextChanged += txtMaMon_TextChanged;
            // 
            // lblTenMon
            // 
            lblTenMon.AutoSize = true;
            lblTenMon.Location = new Point(12, 62);
            lblTenMon.Name = "lblTenMon";
            lblTenMon.Size = new Size(66, 20);
            lblTenMon.TabIndex = 2;
            lblTenMon.Text = "Tên món";
            // 
            // txtTenMon
            // 
            txtTenMon.Location = new Point(85, 58);
            txtTenMon.Name = "txtTenMon";
            txtTenMon.Size = new Size(130, 26);
            txtTenMon.TabIndex = 3;
            // 
            // lblLoaiMon
            // 
            lblLoaiMon.AutoSize = true;
            lblLoaiMon.Location = new Point(12, 97);
            lblLoaiMon.Name = "lblLoaiMon";
            lblLoaiMon.Size = new Size(71, 20);
            lblLoaiMon.TabIndex = 4;
            lblLoaiMon.Text = "Loại món";
            // 
            // cboLoaiMon
            // 
            cboLoaiMon.FormattingEnabled = true;
            cboLoaiMon.Location = new Point(85, 93);
            cboLoaiMon.Name = "cboLoaiMon";
            cboLoaiMon.Size = new Size(130, 27);
            cboLoaiMon.TabIndex = 5;
            // 
            // lblGiaBan
            // 
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(230, 27);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(60, 20);
            lblGiaBan.TabIndex = 6;
            lblGiaBan.Text = "Giá bán";
            // 
            // txtGiaBan
            // 
            txtGiaBan.Location = new Point(325, 23);
            txtGiaBan.Name = "txtGiaBan";
            txtGiaBan.Size = new Size(135, 26);
            txtGiaBan.TabIndex = 7;
            // 
            // lblGiaNhap
            // 
            lblGiaNhap.AutoSize = true;
            lblGiaNhap.Location = new Point(230, 62);
            lblGiaNhap.Name = "lblGiaNhap";
            lblGiaNhap.Size = new Size(68, 20);
            lblGiaNhap.TabIndex = 8;
            lblGiaNhap.Text = "Giá nhập";
            // 
            // txtGiaNhap
            // 
            txtGiaNhap.Location = new Point(325, 58);
            txtGiaNhap.Name = "txtGiaNhap";
            txtGiaNhap.Size = new Size(135, 26);
            txtGiaNhap.TabIndex = 9;
            // 
            // lblSoLuongTon
            // 
            lblSoLuongTon.AutoSize = true;
            lblSoLuongTon.Location = new Point(230, 97);
            lblSoLuongTon.Name = "lblSoLuongTon";
            lblSoLuongTon.Size = new Size(50, 20);
            lblSoLuongTon.TabIndex = 10;
            lblSoLuongTon.Text = "SL tồn";
            // 
            // txtSoLuongTon
            // 
            txtSoLuongTon.Location = new Point(325, 93);
            txtSoLuongTon.Name = "txtSoLuongTon";
            txtSoLuongTon.Size = new Size(135, 26);
            txtSoLuongTon.TabIndex = 11;
            txtSoLuongTon.TextChanged += txtSoLuongTon_TextChanged;
            // 
            // lblDonViTinh
            // 
            lblDonViTinh.AutoSize = true;
            lblDonViTinh.Location = new Point(230, 132);
            lblDonViTinh.Name = "lblDonViTinh";
            lblDonViTinh.Size = new Size(81, 20);
            lblDonViTinh.TabIndex = 12;
            lblDonViTinh.Text = "Đơn vị tính";
            // 
            // cboDonViTinh
            // 
            cboDonViTinh.FormattingEnabled = true;
            cboDonViTinh.Location = new Point(325, 128);
            cboDonViTinh.Name = "cboDonViTinh";
            cboDonViTinh.Size = new Size(135, 27);
            cboDonViTinh.TabIndex = 13;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(230, 167);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(75, 20);
            lblTrangThai.TabIndex = 14;
            lblTrangThai.Text = "Trạng thái";
            // 
            // cboTrangThai
            // 
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(325, 163);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(135, 27);
            cboTrangThai.TabIndex = 15;
            // 
            // gbHinhAnh
            // 
            gbHinhAnh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbHinhAnh.Controls.Add(picHinhAnh);
            gbHinhAnh.Controls.Add(btnChonAnh);
            gbHinhAnh.Controls.Add(btnXoaAnh);
            gbHinhAnh.Location = new Point(5, 214);
            gbHinhAnh.Name = "gbHinhAnh";
            gbHinhAnh.Size = new Size(450, 119);
            gbHinhAnh.TabIndex = 1;
            gbHinhAnh.TabStop = false;
            gbHinhAnh.Text = "Hình ảnh món";
            // 
            // picHinhAnh
            // 
            picHinhAnh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            picHinhAnh.BorderStyle = BorderStyle.FixedSingle;
            picHinhAnh.Location = new Point(15, 23);
            picHinhAnh.Name = "picHinhAnh";
            picHinhAnh.Size = new Size(295, 86);
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picHinhAnh.TabIndex = 0;
            picHinhAnh.TabStop = false;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChonAnh.Location = new Point(325, 23);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(110, 38);
            btnChonAnh.TabIndex = 1;
            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.UseVisualStyleBackColor = true;
            btnChonAnh.Click += btnChonAnh_Click;
            // 
            // btnXoaAnh
            // 
            btnXoaAnh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXoaAnh.Location = new Point(325, 68);
            btnXoaAnh.Name = "btnXoaAnh";
            btnXoaAnh.Size = new Size(110, 38);
            btnXoaAnh.TabIndex = 2;
            btnXoaAnh.Text = "Xóa ảnh";
            btnXoaAnh.UseVisualStyleBackColor = true;
            btnXoaAnh.Click += btnXoaAnh_Click;
            // 
            // gbTimKiem
            // 
            gbTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTimKiem.Controls.Add(lblTimKiem);
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(lblTimKiemLoai);
            gbTimKiem.Controls.Add(cboTimKiemLoai);
            gbTimKiem.Controls.Add(chkTonKhoThap);
            gbTimKiem.Controls.Add(btnLamMoi);
            gbTimKiem.Location = new Point(5, 337);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Size = new Size(450, 109);
            gbTimKiem.TabIndex = 2;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(12, 27);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(37, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(50, 23);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(110, 26);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // lblTimKiemLoai
            // 
            lblTimKiemLoai.AutoSize = true;
            lblTimKiemLoai.Location = new Point(170, 27);
            lblTimKiemLoai.Name = "lblTimKiemLoai";
            lblTimKiemLoai.Size = new Size(40, 20);
            lblTimKiemLoai.TabIndex = 2;
            lblTimKiemLoai.Text = "Loại:";
            // 
            // cboTimKiemLoai
            // 
            cboTimKiemLoai.FormattingEnabled = true;
            cboTimKiemLoai.Location = new Point(215, 23);
            cboTimKiemLoai.Name = "cboTimKiemLoai";
            cboTimKiemLoai.Size = new Size(120, 27);
            cboTimKiemLoai.TabIndex = 3;
            cboTimKiemLoai.Text = "-- Tất cả --";
            cboTimKiemLoai.SelectedIndexChanged += cboTimKiemLoai_SelectedIndexChanged;
            // 
            // chkTonKhoThap
            // 
            chkTonKhoThap.AutoSize = true;
            chkTonKhoThap.Location = new Point(50, 65);
            chkTonKhoThap.Name = "chkTonKhoThap";
            chkTonKhoThap.Size = new Size(143, 24);
            chkTonKhoThap.TabIndex = 4;
            chkTonKhoThap.Text = "Cảnh báo sắp hết";
            chkTonKhoThap.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLamMoi.Location = new Point(325, 21);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 71);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // gbDanhSach
            // 
            gbDanhSach.Controls.Add(dgvMonAn);
            gbDanhSach.Controls.Add(pnlSummary);
            gbDanhSach.Dock = DockStyle.Fill;
            gbDanhSach.Location = new Point(493, 3);
            gbDanhSach.Name = "gbDanhSach";
            gbDanhSach.Size = new Size(504, 441);
            gbDanhSach.TabIndex = 1;
            gbDanhSach.TabStop = false;
            gbDanhSach.Text = "Danh sách thực đơn và kho";
            // 
            // dgvMonAn
            // 
            dgvMonAn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMonAn.Dock = DockStyle.Fill;
            dgvMonAn.Location = new Point(3, 22);
            dgvMonAn.Name = "dgvMonAn";
            dgvMonAn.RowHeadersWidth = 51;
            dgvMonAn.Size = new Size(498, 383);
            dgvMonAn.TabIndex = 0;
            dgvMonAn.CellClick += dgvMonAn_CellClick;
            dgvMonAn.SelectionChanged += dgvMonAn_SelectionChanged;
            // 
            // pnlSummary
            // 
            pnlSummary.Controls.Add(lblTongSoMon);
            pnlSummary.Controls.Add(lblTongTonKho);
            pnlSummary.Controls.Add(lblCanhBao);
            pnlSummary.Dock = DockStyle.Bottom;
            pnlSummary.Location = new Point(3, 405);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(498, 33);
            pnlSummary.TabIndex = 1;
            // 
            // lblTongSoMon
            // 
            lblTongSoMon.AutoSize = true;
            lblTongSoMon.Location = new Point(15, 8);
            lblTongSoMon.Name = "lblTongSoMon";
            lblTongSoMon.Size = new Size(111, 20);
            lblTongSoMon.TabIndex = 0;
            lblTongSoMon.Text = "Tổng số món: 0";
            // 
            // lblTongTonKho
            // 
            lblTongTonKho.AutoSize = true;
            lblTongTonKho.Location = new Point(175, 8);
            lblTongTonKho.Name = "lblTongTonKho";
            lblTongTonKho.Size = new Size(84, 20);
            lblTongTonKho.TabIndex = 1;
            lblTongTonKho.Text = "Tổng tồn: 0";
            // 
            // lblCanhBao
            // 
            lblCanhBao.AutoSize = true;
            lblCanhBao.Location = new Point(335, 8);
            lblCanhBao.Name = "lblCanhBao";
            lblCanhBao.Size = new Size(111, 20);
            lblCanhBao.TabIndex = 2;
            lblCanhBao.Text = "Sắp hết hàng: 0";
            // 
            // pnlChucNang
            // 
            pnlChucNang.Controls.Add(btnThem);
            pnlChucNang.Controls.Add(btnSua);
            pnlChucNang.Controls.Add(btnXoa);
            pnlChucNang.Controls.Add(btnNhapLai);
            pnlChucNang.Controls.Add(btnNhapKho);
            pnlChucNang.Controls.Add(btnDong);
            pnlChucNang.Dock = DockStyle.Bottom;
            pnlChucNang.Location = new Point(0, 490);
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.Size = new Size(1000, 52);
            pnlChucNang.TabIndex = 2;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(10, 8);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(110, 38);
            btnThem.TabIndex = 0;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(126, 8);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(110, 38);
            btnSua.TabIndex = 1;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(242, 8);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(110, 38);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnNhapLai
            // 
            btnNhapLai.Location = new Point(358, 8);
            btnNhapLai.Name = "btnNhapLai";
            btnNhapLai.Size = new Size(110, 38);
            btnNhapLai.TabIndex = 3;
            btnNhapLai.Text = "Nhập lại";
            btnNhapLai.UseVisualStyleBackColor = true;
            btnNhapLai.Click += btnNhapLai_Click;
            // 
            // btnNhapKho
            // 
            btnNhapKho.Location = new Point(474, 8);
            btnNhapKho.Name = "btnNhapKho";
            btnNhapKho.Size = new Size(140, 38);
            btnNhapKho.TabIndex = 4;
            btnNhapKho.Text = "Nhập thêm kho";
            btnNhapKho.UseVisualStyleBackColor = true;
            btnNhapKho.Click += btnNhapKho_Click;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.Location = new Point(880, 8);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(110, 38);
            btnDong.TabIndex = 5;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += btnDong_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            // 
            // frmQuanLyMonAn
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 542);
            Controls.Add(tableMain);
            Controls.Add(pnlChucNang);
            Controls.Add(lblTieuDe);
            MinimumSize = new Size(900, 458);
            Name = "frmQuanLyMonAn";
            Text = "Quản lý món ăn";
            Load += frmQuanLyMonAn_Load;
            tableMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            gbHinhAnh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).EndInit();
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMonAn).EndInit();
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
        private Label lblMaMon;
        private TextBox txtMaMon;
        private Label lblTenMon;
        private TextBox txtTenMon;
        private Label lblLoaiMon;
        private ComboBox cboLoaiMon;
        private Label lblGiaBan;
        private TextBox txtGiaBan;
        private Label lblGiaNhap;
        private TextBox txtGiaNhap;
        private Label lblSoLuongTon;
        private TextBox txtSoLuongTon;
        private Label lblDonViTinh;
        private ComboBox cboDonViTinh;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;
        private GroupBox gbHinhAnh;
        private PictureBox picHinhAnh;
        private Button btnChonAnh;
        private Button btnXoaAnh;
        private GroupBox gbTimKiem;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblTimKiemLoai;
        private ComboBox cboTimKiemLoai;
        private CheckBox chkTonKhoThap;
        private Button btnLamMoi;
        private GroupBox gbDanhSach;
        private DataGridView dgvMonAn;
        private Panel pnlSummary;
        private Label lblTongSoMon;
        private Label lblTongTonKho;
        private Label lblCanhBao;
        private Panel pnlChucNang;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnNhapLai;
        private Button btnNhapKho;
        private Button btnDong;
        private OpenFileDialog openFileDialog;
    }
}
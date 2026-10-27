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
            // -- Cột trái --
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
            // -- Cột phải --
            gbDanhSach = new GroupBox();
            dgvMonAn = new DataGridView();
            pnlSummary = new Panel();
            lblTongSoMon = new Label();
            lblTongTonKho = new Label();
            lblCanhBao = new Label();
            // -- Panel nút bấm --
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

            // ──────────────────────────────────────────
            // lblTieuDe
            // ──────────────────────────────────────────
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Height = 40;
            lblTieuDe.Text = "QUẢN LÝ THỰC ĐƠN VÀ KIỂM KÊ KHO HÀNG";
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
            pnlChucNang.Controls.Add(btnNhapKho);
            pnlChucNang.Controls.Add(btnDong);

            btnThem.Location = new Point(10, 8);
            btnThem.Size = new Size(110, 40);
            btnThem.Text = "Thêm";
            btnThem.TabIndex = 0;
            btnThem.UseVisualStyleBackColor = true;

            btnSua.Location = new Point(128, 8);
            btnSua.Size = new Size(110, 40);
            btnSua.Text = "Sửa";
            btnSua.TabIndex = 1;
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;

            btnXoa.Location = new Point(246, 8);
            btnXoa.Size = new Size(110, 40);
            btnXoa.Text = "Xóa";
            btnXoa.TabIndex = 2;
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;

            btnNhapLai.Location = new Point(364, 8);
            btnNhapLai.Size = new Size(110, 40);
            btnNhapLai.Text = "Nhập lại";
            btnNhapLai.TabIndex = 3;
            btnNhapLai.UseVisualStyleBackColor = true;

            btnNhapKho.Location = new Point(482, 8);
            btnNhapKho.Size = new Size(140, 40);
            btnNhapKho.Text = "Nhập thêm kho";
            btnNhapKho.TabIndex = 4;
            btnNhapKho.UseVisualStyleBackColor = true;

            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.Location = new Point(762, 8);
            btnDong.Size = new Size(110, 40);
            btnDong.Text = "Đóng";
            btnDong.TabIndex = 5;
            btnDong.UseVisualStyleBackColor = true;

            // ──────────────────────────────────────────
            // tableMain — chia 2 cột: 38% trái | 62% phải
            // ──────────────────────────────────────────
            tableMain.Dock = DockStyle.Fill;
            tableMain.ColumnCount = 2;
            tableMain.RowCount = 1;
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
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
            pnlLeft.Controls.Add(gbHinhAnh);
            pnlLeft.Controls.Add(gbThongTin);

            // ──────────────────────────────────────────
            // gbThongTin (2 cột dọc bên trong)
            // ──────────────────────────────────────────
            gbThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbThongTin.Location = new Point(0, 0);
            gbThongTin.Name = "gbThongTin";
            gbThongTin.Height = 205;
            gbThongTin.TabIndex = 0;
            gbThongTin.TabStop = false;
            gbThongTin.Text = "Thông tin món ăn và kho";
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

            // Cột trái (x=8..80..195)
            lblMaMon.AutoSize = true;
            lblMaMon.Location = new Point(8, 28);
            lblMaMon.Text = "Mã món";
            lblMaMon.TabIndex = 0;
            txtMaMon.Location = new Point(80, 24);
            txtMaMon.Size = new Size(110, 27);
            txtMaMon.TabIndex = 1;
            txtMaMon.TextChanged += txtMaMon_TextChanged;

            lblTenMon.AutoSize = true;
            lblTenMon.Location = new Point(8, 62);
            lblTenMon.Text = "Tên món";
            lblTenMon.TabIndex = 2;
            txtTenMon.Location = new Point(80, 58);
            txtTenMon.Size = new Size(110, 27);
            txtTenMon.TabIndex = 3;

            lblLoaiMon.AutoSize = true;
            lblLoaiMon.Location = new Point(8, 96);
            lblLoaiMon.Text = "Loại món";
            lblLoaiMon.TabIndex = 4;
            cboLoaiMon.FormattingEnabled = true;
            cboLoaiMon.Location = new Point(80, 92);
            cboLoaiMon.Size = new Size(110, 28);
            cboLoaiMon.TabIndex = 5;

            // Cột phải (x=200..275..390)
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(200, 28);
            lblGiaBan.Text = "Giá bán";
            lblGiaBan.TabIndex = 6;
            txtGiaBan.Location = new Point(275, 24);
            txtGiaBan.Size = new Size(110, 27);
            txtGiaBan.TabIndex = 7;

            lblGiaNhap.AutoSize = true;
            lblGiaNhap.Location = new Point(200, 62);
            lblGiaNhap.Text = "Giá nhập";
            lblGiaNhap.TabIndex = 8;
            txtGiaNhap.Location = new Point(275, 58);
            txtGiaNhap.Size = new Size(110, 27);
            txtGiaNhap.TabIndex = 9;

            lblSoLuongTon.AutoSize = true;
            lblSoLuongTon.Location = new Point(200, 96);
            lblSoLuongTon.Text = "SL tồn";
            lblSoLuongTon.TabIndex = 10;
            txtSoLuongTon.Location = new Point(275, 92);
            txtSoLuongTon.Size = new Size(110, 27);
            txtSoLuongTon.TabIndex = 11;
            txtSoLuongTon.TextChanged += txtSoLuongTon_TextChanged;

            lblDonViTinh.AutoSize = true;
            lblDonViTinh.Location = new Point(200, 130);
            lblDonViTinh.Text = "Đơn vị tính";
            lblDonViTinh.TabIndex = 12;
            cboDonViTinh.FormattingEnabled = true;
            cboDonViTinh.Location = new Point(275, 126);
            cboDonViTinh.Size = new Size(110, 28);
            cboDonViTinh.TabIndex = 13;

            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(200, 164);
            lblTrangThai.Text = "Trạng thái";
            lblTrangThai.TabIndex = 14;
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(275, 160);
            cboTrangThai.Size = new Size(110, 28);
            cboTrangThai.TabIndex = 15;

            // ──────────────────────────────────────────
            // gbHinhAnh
            // ──────────────────────────────────────────
            gbHinhAnh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbHinhAnh.Location = new Point(0, 212);
            gbHinhAnh.Name = "gbHinhAnh";
            gbHinhAnh.Height = 125;
            gbHinhAnh.TabIndex = 1;
            gbHinhAnh.TabStop = false;
            gbHinhAnh.Text = "Hình ảnh món";
            gbHinhAnh.Controls.Add(picHinhAnh);
            gbHinhAnh.Controls.Add(btnChonAnh);
            gbHinhAnh.Controls.Add(btnXoaAnh);

            picHinhAnh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            picHinhAnh.BorderStyle = BorderStyle.FixedSingle;
            picHinhAnh.Location = new Point(8, 24);
            picHinhAnh.Size = new Size(270, 90);
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picHinhAnh.TabIndex = 0;
            picHinhAnh.TabStop = false;

            btnChonAnh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChonAnh.Location = new Point(290, 24);
            btnChonAnh.Size = new Size(90, 38);
            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.TabIndex = 1;
            btnChonAnh.UseVisualStyleBackColor = true;

            btnXoaAnh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXoaAnh.Location = new Point(290, 70);
            btnXoaAnh.Size = new Size(90, 38);
            btnXoaAnh.Text = "Xóa ảnh";
            btnXoaAnh.TabIndex = 2;
            btnXoaAnh.UseVisualStyleBackColor = true;

            // ──────────────────────────────────────────
            // gbTimKiem
            // ──────────────────────────────────────────
            gbTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbTimKiem.Location = new Point(0, 344);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Height = 104;
            gbTimKiem.TabIndex = 2;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm";
            gbTimKiem.Controls.Add(lblTimKiem);
            gbTimKiem.Controls.Add(txtTimKiem);
            gbTimKiem.Controls.Add(lblTimKiemLoai);
            gbTimKiem.Controls.Add(cboTimKiemLoai);
            gbTimKiem.Controls.Add(chkTonKhoThap);
            gbTimKiem.Controls.Add(btnLamMoi);

            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(8, 26);
            lblTimKiem.Text = "Tìm";
            lblTimKiem.TabIndex = 0;

            txtTimKiem.Location = new Point(45, 22);
            txtTimKiem.Size = new Size(90, 27);
            txtTimKiem.TabIndex = 1;

            lblTimKiemLoai.AutoSize = true;
            lblTimKiemLoai.Location = new Point(145, 26);
            lblTimKiemLoai.Text = "Loại";
            lblTimKiemLoai.TabIndex = 2;

            cboTimKiemLoai.FormattingEnabled = true;
            cboTimKiemLoai.Location = new Point(185, 22);
            cboTimKiemLoai.Size = new Size(100, 28);
            cboTimKiemLoai.TabIndex = 3;
            cboTimKiemLoai.Text = "-- Tất cả --";

            chkTonKhoThap.AutoSize = true;
            chkTonKhoThap.Location = new Point(45, 58);
            chkTonKhoThap.Size = new Size(146, 24);
            chkTonKhoThap.Text = "Cảnh báo sắp hết";
            chkTonKhoThap.TabIndex = 4;
            chkTonKhoThap.UseVisualStyleBackColor = true;

            btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLamMoi.Location = new Point(290, 22);
            btnLamMoi.Size = new Size(90, 70);
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.TabIndex = 5;
            btnLamMoi.UseVisualStyleBackColor = true;

            // ──────────────────────────────────────────
            // gbDanhSach — fill cột phải của tableMain
            // ──────────────────────────────────────────
            gbDanhSach.Dock = DockStyle.Fill;
            gbDanhSach.Name = "gbDanhSach";
            gbDanhSach.TabIndex = 1;
            gbDanhSach.TabStop = false;
            gbDanhSach.Text = "Danh sách thực đơn và kho";
            gbDanhSach.Controls.Add(dgvMonAn);
            gbDanhSach.Controls.Add(pnlSummary);

            // pnlSummary — dock bottom (không nhảy khi resize)
            pnlSummary.Dock = DockStyle.Bottom;
            pnlSummary.Height = 30;
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Controls.Add(lblTongSoMon);
            pnlSummary.Controls.Add(lblTongTonKho);
            pnlSummary.Controls.Add(lblCanhBao);

            lblTongSoMon.AutoSize = true;
            lblTongSoMon.Location = new Point(10, 6);
            lblTongSoMon.Text = "Tổng số món: 0";
            lblTongSoMon.Name = "lblTongSoMon";
            lblTongSoMon.TabIndex = 0;

            lblTongTonKho.AutoSize = true;
            lblTongTonKho.Location = new Point(160, 6);
            lblTongTonKho.Text = "Tổng tồn: 0";
            lblTongTonKho.Name = "lblTongTonKho";
            lblTongTonKho.TabIndex = 1;

            lblCanhBao.AutoSize = true;
            lblCanhBao.Location = new Point(300, 6);
            lblCanhBao.Text = "Sắp hết hàng: 0";
            lblCanhBao.Name = "lblCanhBao";
            lblCanhBao.TabIndex = 2;

            // DataGridView fill phần còn lại
            dgvMonAn.Dock = DockStyle.Fill;
            dgvMonAn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMonAn.Name = "dgvMonAn";
            dgvMonAn.RowHeadersWidth = 49;
            dgvMonAn.TabIndex = 0;

            // ──────────────────────────────────────────
            // openFileDialog
            // ──────────────────────────────────────────
            openFileDialog.FileName = "openFileDialog1";

            // ──────────────────────────────────────────
            // frmQuanLyMonAn
            // ──────────────────────────────────────────
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(882, 559);
            Controls.Add(tableMain);
            Controls.Add(pnlChucNang);
            Controls.Add(lblTieuDe);
            Name = "frmQuanLyMonAn";
            Text = "Quản lý món ăn";

            tableMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            gbHinhAnh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).EndInit();
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            gbDanhSach.ResumeLayout(false);
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonAn).EndInit();
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
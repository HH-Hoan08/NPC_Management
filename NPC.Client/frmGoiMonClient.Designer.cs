namespace NPC.Client
{
    partial class frmGoiMonClient
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
            lblTieuDe = new Label();
            gbThucDon = new GroupBox();
            lblDanhMuc = new Label();
            cboDanhMuc = new ComboBox();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            dgvThucDon = new DataGridView();
            gbChiTietMon = new GroupBox();
            picMon = new PictureBox();
            lblTenMon = new Label();
            txtTenMon = new TextBox();
            lblDonGia = new Label();
            txtDonGia = new TextBox();
            lblSoLuong = new Label();
            txtSoLuong = new TextBox();
            btnThemVaoGio = new Button();
            gbGioHang = new GroupBox();
            dgvGioHang = new DataGridView();
            btnXoaMon = new Button();
            lblTongTien = new Label();
            pnlDuoi = new Panel();
            lblMayHienTai = new Label();
            btnDatMon = new Button();
            btnDong = new Button();
            epSoLuong = new ErrorProvider(components);
            gbThucDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThucDon).BeginInit();
            gbChiTietMon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMon).BeginInit();
            gbGioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).BeginInit();
            pnlDuoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epSoLuong).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.BackColor = Color.White;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.DodgerBlue;
            lblTieuDe.Location = new Point(0, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(1096, 52);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "GỌI MÓN – DỊCH VỤ TẠI MÁY";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbThucDon
            // 
            gbThucDon.Controls.Add(lblDanhMuc);
            gbThucDon.Controls.Add(cboDanhMuc);
            gbThucDon.Controls.Add(lblTimKiem);
            gbThucDon.Controls.Add(txtTimKiem);
            gbThucDon.Controls.Add(dgvThucDon);
            gbThucDon.Location = new Point(12, 62);
            gbThucDon.Name = "gbThucDon";
            gbThucDon.Size = new Size(560, 538);
            gbThucDon.TabIndex = 1;
            gbThucDon.TabStop = false;
            gbThucDon.Text = "Thực đơn";
            // 
            // lblDanhMuc
            // 
            lblDanhMuc.Location = new Point(14, 30);
            lblDanhMuc.Name = "lblDanhMuc";
            lblDanhMuc.Size = new Size(91, 24);
            lblDanhMuc.TabIndex = 0;
            lblDanhMuc.Text = "Danh mục";
            lblDanhMuc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboDanhMuc
            // 
            cboDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDanhMuc.FormattingEnabled = true;
            cboDanhMuc.Location = new Point(110, 30);
            cboDanhMuc.Name = "cboDanhMuc";
            cboDanhMuc.Size = new Size(170, 31);
            cboDanhMuc.TabIndex = 0;
            cboDanhMuc.SelectedIndexChanged += cboDanhMuc_SelectedIndexChanged;
            // 
            // lblTimKiem
            // 
            lblTimKiem.Location = new Point(286, 30);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(88, 24);
            lblTimKiem.TabIndex = 2;
            lblTimKiem.Text = "Tìm món";
            lblTimKiem.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(373, 28);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(171, 30);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // dgvThucDon
            // 
            dgvThucDon.AllowUserToAddRows = false;
            dgvThucDon.AllowUserToDeleteRows = false;
            dgvThucDon.AllowUserToResizeRows = false;
            dgvThucDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThucDon.BackgroundColor = Color.White;
            dgvThucDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThucDon.Location = new Point(14, 68);
            dgvThucDon.MultiSelect = false;
            dgvThucDon.Name = "dgvThucDon";
            dgvThucDon.ReadOnly = true;
            dgvThucDon.RowHeadersVisible = false;
            dgvThucDon.RowHeadersWidth = 51;
            dgvThucDon.RowTemplate.Height = 28;
            dgvThucDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThucDon.Size = new Size(530, 456);
            dgvThucDon.TabIndex = 2;
            dgvThucDon.SelectionChanged += dgvThucDon_SelectionChanged;
            // 
            // gbChiTietMon
            // 
            gbChiTietMon.Controls.Add(picMon);
            gbChiTietMon.Controls.Add(lblTenMon);
            gbChiTietMon.Controls.Add(txtTenMon);
            gbChiTietMon.Controls.Add(lblDonGia);
            gbChiTietMon.Controls.Add(txtDonGia);
            gbChiTietMon.Controls.Add(lblSoLuong);
            gbChiTietMon.Controls.Add(txtSoLuong);
            gbChiTietMon.Controls.Add(btnThemVaoGio);
            gbChiTietMon.Location = new Point(584, 62);
            gbChiTietMon.Name = "gbChiTietMon";
            gbChiTietMon.Size = new Size(500, 232);
            gbChiTietMon.TabIndex = 2;
            gbChiTietMon.TabStop = false;
            gbChiTietMon.Text = "Chi tiết món đang chọn";
            // 
            // picMon
            // 
            picMon.BackColor = Color.WhiteSmoke;
            picMon.BorderStyle = BorderStyle.FixedSingle;
            picMon.Location = new Point(16, 30);
            picMon.Name = "picMon";
            picMon.Size = new Size(150, 150);
            picMon.SizeMode = PictureBoxSizeMode.Zoom;
            picMon.TabIndex = 0;
            picMon.TabStop = false;
            // 
            // lblTenMon
            // 
            lblTenMon.Location = new Point(184, 34);
            lblTenMon.Name = "lblTenMon";
            lblTenMon.Size = new Size(80, 24);
            lblTenMon.TabIndex = 1;
            lblTenMon.Text = "Tên món";
            lblTenMon.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTenMon
            // 
            txtTenMon.Location = new Point(270, 32);
            txtTenMon.Name = "txtTenMon";
            txtTenMon.ReadOnly = true;
            txtTenMon.Size = new Size(214, 30);
            txtTenMon.TabIndex = 2;
            txtTenMon.TabStop = false;
            // 
            // lblDonGia
            // 
            lblDonGia.Location = new Point(184, 78);
            lblDonGia.Name = "lblDonGia";
            lblDonGia.Size = new Size(80, 24);
            lblDonGia.TabIndex = 3;
            lblDonGia.Text = "Đơn giá";
            lblDonGia.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDonGia
            // 
            txtDonGia.Location = new Point(270, 76);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.ReadOnly = true;
            txtDonGia.Size = new Size(214, 30);
            txtDonGia.TabIndex = 4;
            txtDonGia.TabStop = false;
            txtDonGia.TextAlign = HorizontalAlignment.Right;
            // 
            // lblSoLuong
            // 
            lblSoLuong.Location = new Point(184, 122);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(80, 24);
            lblSoLuong.TabIndex = 5;
            lblSoLuong.Text = "Số lượng";
            lblSoLuong.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSoLuong
            // 
            txtSoLuong.Location = new Point(270, 120);
            txtSoLuong.MaxLength = 3;
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new Size(120, 30);
            txtSoLuong.TabIndex = 0;
            txtSoLuong.Text = "1";
            txtSoLuong.TextAlign = HorizontalAlignment.Right;
            txtSoLuong.TextChanged += txtSoLuong_TextChanged;
            txtSoLuong.Validating += txtSoLuong_Validating;
            // 
            // btnThemVaoGio
            // 
            btnThemVaoGio.BackColor = Color.FromArgb(14, 165, 233);
            btnThemVaoGio.FlatAppearance.BorderSize = 0;
            btnThemVaoGio.FlatStyle = FlatStyle.Flat;
            btnThemVaoGio.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnThemVaoGio.ForeColor = Color.White;
            btnThemVaoGio.Location = new Point(184, 170);
            btnThemVaoGio.Name = "btnThemVaoGio";
            btnThemVaoGio.Size = new Size(300, 38);
            btnThemVaoGio.TabIndex = 1;
            btnThemVaoGio.Text = "Thêm vào giỏ";
            btnThemVaoGio.UseVisualStyleBackColor = false;
            btnThemVaoGio.Click += btnThemVaoGio_Click;
            // 
            // gbGioHang
            // 
            gbGioHang.Controls.Add(dgvGioHang);
            gbGioHang.Controls.Add(btnXoaMon);
            gbGioHang.Controls.Add(lblTongTien);
            gbGioHang.Location = new Point(584, 304);
            gbGioHang.Name = "gbGioHang";
            gbGioHang.Size = new Size(500, 296);
            gbGioHang.TabIndex = 3;
            gbGioHang.TabStop = false;
            gbGioHang.Text = "Giỏ hàng";
            // 
            // dgvGioHang
            // 
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.AllowUserToDeleteRows = false;
            dgvGioHang.AllowUserToResizeRows = false;
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGioHang.BackgroundColor = Color.White;
            dgvGioHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGioHang.Location = new Point(14, 28);
            dgvGioHang.MultiSelect = false;
            dgvGioHang.Name = "dgvGioHang";
            dgvGioHang.ReadOnly = true;
            dgvGioHang.RowHeadersVisible = false;
            dgvGioHang.RowHeadersWidth = 51;
            dgvGioHang.RowTemplate.Height = 28;
            dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGioHang.Size = new Size(472, 196);
            dgvGioHang.TabIndex = 0;
            // 
            // btnXoaMon
            // 
            btnXoaMon.Location = new Point(14, 238);
            btnXoaMon.Name = "btnXoaMon";
            btnXoaMon.Size = new Size(110, 34);
            btnXoaMon.TabIndex = 1;
            btnXoaMon.Text = "Xóa món";
            btnXoaMon.UseVisualStyleBackColor = true;
            btnXoaMon.Click += btnXoaMon_Click;
            // 
            // lblTongTien
            // 
            lblTongTien.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(220, 38, 38);
            lblTongTien.Location = new Point(160, 238);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(326, 34);
            lblTongTien.TabIndex = 2;
            lblTongTien.Text = "Tổng tiền: 0 đ";
            lblTongTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlDuoi
            // 
            pnlDuoi.BackColor = Color.FromArgb(226, 232, 240);
            pnlDuoi.Controls.Add(lblMayHienTai);
            pnlDuoi.Controls.Add(btnDatMon);
            pnlDuoi.Controls.Add(btnDong);
            pnlDuoi.Dock = DockStyle.Bottom;
            pnlDuoi.Location = new Point(0, 610);
            pnlDuoi.Name = "pnlDuoi";
            pnlDuoi.Size = new Size(1096, 48);
            pnlDuoi.TabIndex = 4;
            // 
            // lblMayHienTai
            // 
            lblMayHienTai.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblMayHienTai.Location = new Point(16, 10);
            lblMayHienTai.Name = "lblMayHienTai";
            lblMayHienTai.Size = new Size(220, 28);
            lblMayHienTai.TabIndex = 0;
            lblMayHienTai.Text = "Máy: MAY-01";
            lblMayHienTai.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnDatMon
            // 
            btnDatMon.BackColor = Color.FromArgb(22, 163, 74);
            btnDatMon.Enabled = false;
            btnDatMon.FlatAppearance.BorderSize = 0;
            btnDatMon.FlatStyle = FlatStyle.Flat;
            btnDatMon.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDatMon.ForeColor = Color.White;
            btnDatMon.Location = new Point(836, 6);
            btnDatMon.Name = "btnDatMon";
            btnDatMon.Size = new Size(120, 36);
            btnDatMon.TabIndex = 1;
            btnDatMon.Text = "Đặt món";
            btnDatMon.UseVisualStyleBackColor = false;
            btnDatMon.Click += btnDatMon_Click;
            // 
            // btnDong
            // 
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(968, 6);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(120, 36);
            btnDong.TabIndex = 2;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // epSoLuong
            // 
            epSoLuong.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            epSoLuong.ContainerControl = this;
            // 
            // frmGoiMonClient
            // 
            AcceptButton = btnThemVaoGio;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnDong;
            ClientSize = new Size(1096, 658);
            Controls.Add(pnlDuoi);
            Controls.Add(gbGioHang);
            Controls.Add(gbChiTietMon);
            Controls.Add(gbThucDon);
            Controls.Add(lblTieuDe);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmGoiMonClient";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gọi món";
            FormClosing += frmGoiMonClient_FormClosing;
            Load += frmGoiMonClient_Load;
            gbThucDon.ResumeLayout(false);
            gbThucDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThucDon).EndInit();
            gbChiTietMon.ResumeLayout(false);
            gbChiTietMon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picMon).EndInit();
            gbGioHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).EndInit();
            pnlDuoi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)epSoLuong).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTieuDe;
        private GroupBox gbThucDon;
        private Label lblDanhMuc;
        private ComboBox cboDanhMuc;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private DataGridView dgvThucDon;
        private DataGridViewTextBoxColumn colTDMaMon;
        private DataGridViewTextBoxColumn colTDTenMon;
        private DataGridViewTextBoxColumn colTDDonGia;
        private DataGridViewTextBoxColumn colTDSoLuongTon;
        private GroupBox gbChiTietMon;
        private PictureBox picMon;
        private Label lblTenMon;
        private TextBox txtTenMon;
        private Label lblDonGia;
        private TextBox txtDonGia;
        private Label lblSoLuong;
        private TextBox txtSoLuong;
        private Button btnThemVaoGio;
        private GroupBox gbGioHang;
        private DataGridView dgvGioHang;
        private DataGridViewTextBoxColumn colGHTenMon;
        private DataGridViewTextBoxColumn colGHSoLuong;
        private DataGridViewTextBoxColumn colGHDonGia;
        private DataGridViewTextBoxColumn colGHThanhTien;
        private Button btnXoaMon;
        private Label lblTongTien;
        private Panel pnlDuoi;
        private Label lblMayHienTai;
        private Button btnDatMon;
        private Button btnDong;
        private ErrorProvider epSoLuong;
    }
}
namespace NPC.Server
{
    partial class frmCaiDatGiaoDien
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
            gbGiaoDien = new GroupBox();
            btnChonFont = new Button();
            txtFontHienThi = new TextBox();
            lblFontHienThi = new Label();
            pnlMauBaoTri = new Panel();
            lblMauBaoTri = new Label();
            btnDoiMauBaoTri = new Button();
            btnDoiMauDangChoi = new Button();
            pnlMauDangChoi = new Panel();
            lblMauDangChoi = new Label();
            btnDoiMauTrong = new Button();
            pnlMauTrong = new Panel();
            lblMauTrong = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            gbXemTruoc = new GroupBox();
            lblPreviewSoDu = new Label();
            lblPreviewTime = new Label();
            lblPreviewHoiVien = new Label();
            lblPreviewTenMay = new Label();
            pnlPereviewMay = new Panel();
            gbSaoLuu = new GroupBox();
            lblThuMucBackup = new Label();
            txtThuMucBackup = new TextBox();
            btnChonThuMuc = new Button();
            btnSaoLuu = new Button();
            btnKhoiPhuc = new Button();
            prgTienTrinh = new ProgressBar();
            lblTrangThaiBackup = new Label();
            pnlChucNang = new Panel();
            btnLuuCaiDat = new Button();
            btnMacDinh = new Button();
            btnDong = new Button();
            gbGiaoDien.SuspendLayout();
            gbXemTruoc.SuspendLayout();
            gbSaoLuu.SuspendLayout();
            pnlChucNang.SuspendLayout();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 13.7739134F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDe.Location = new Point(327, 9);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(474, 30);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "CÀI ĐẶT GIAO DIỆN VÀ SAO LƯU HỆ THỐNG";
            // 
            // gbGiaoDien
            // 
            gbGiaoDien.Controls.Add(btnChonFont);
            gbGiaoDien.Controls.Add(txtFontHienThi);
            gbGiaoDien.Controls.Add(lblFontHienThi);
            gbGiaoDien.Controls.Add(pnlMauBaoTri);
            gbGiaoDien.Controls.Add(lblMauBaoTri);
            gbGiaoDien.Controls.Add(btnDoiMauBaoTri);
            gbGiaoDien.Controls.Add(btnDoiMauDangChoi);
            gbGiaoDien.Controls.Add(pnlMauDangChoi);
            gbGiaoDien.Controls.Add(lblMauDangChoi);
            gbGiaoDien.Controls.Add(btnDoiMauTrong);
            gbGiaoDien.Controls.Add(pnlMauTrong);
            gbGiaoDien.Controls.Add(lblMauTrong);
            gbGiaoDien.Location = new Point(12, 54);
            gbGiaoDien.Name = "gbGiaoDien";
            gbGiaoDien.Size = new Size(540, 260);
            gbGiaoDien.TabIndex = 1;
            gbGiaoDien.TabStop = false;
            gbGiaoDien.Text = "Tùy chỉnh màu sắc và Phông chữ phòng máy";
            // 
            // btnChonFont
            // 
            btnChonFont.Location = new Point(208, 211);
            btnChonFont.Name = "btnChonFont";
            btnChonFont.Size = new Size(125, 30);
            btnChonFont.TabIndex = 11;
            btnChonFont.Text = "Chọn Font";
            btnChonFont.UseVisualStyleBackColor = true;
            // 
            // txtFontHienThi
            // 
            txtFontHienThi.Location = new Point(208, 179);
            txtFontHienThi.Name = "txtFontHienThi";
            txtFontHienThi.ReadOnly = true;
            txtFontHienThi.Size = new Size(189, 26);
            txtFontHienThi.TabIndex = 10;
            txtFontHienThi.Text = "Segoe UI, 10pt";
            // 
            // lblFontHienThi
            // 
            lblFontHienThi.AutoSize = true;
            lblFontHienThi.Location = new Point(20, 185);
            lblFontHienThi.Name = "lblFontHienThi";
            lblFontHienThi.Size = new Size(132, 20);
            lblFontHienThi.TabIndex = 9;
            lblFontHienThi.Text = "Phông chữ hiển thị";
            // 
            // pnlMauBaoTri
            // 
            pnlMauBaoTri.BorderStyle = BorderStyle.FixedSingle;
            pnlMauBaoTri.Location = new Point(208, 129);
            pnlMauBaoTri.Name = "pnlMauBaoTri";
            pnlMauBaoTri.Size = new Size(50, 26);
            pnlMauBaoTri.TabIndex = 8;
            // 
            // lblMauBaoTri
            // 
            lblMauBaoTri.AutoSize = true;
            lblMauBaoTri.Location = new Point(20, 129);
            lblMauBaoTri.Name = "lblMauBaoTri";
            lblMauBaoTri.Size = new Size(166, 20);
            lblMauBaoTri.TabIndex = 7;
            lblMauBaoTri.Text = "Màu máy Bảo trì / Khóa";
            // 
            // btnDoiMauBaoTri
            // 
            btnDoiMauBaoTri.Location = new Point(287, 129);
            btnDoiMauBaoTri.Name = "btnDoiMauBaoTri";
            btnDoiMauBaoTri.Size = new Size(110, 30);
            btnDoiMauBaoTri.TabIndex = 6;
            btnDoiMauBaoTri.Text = "Đổi màu";
            btnDoiMauBaoTri.UseVisualStyleBackColor = true;
            // 
            // btnDoiMauDangChoi
            // 
            btnDoiMauDangChoi.Location = new Point(287, 76);
            btnDoiMauDangChoi.Name = "btnDoiMauDangChoi";
            btnDoiMauDangChoi.Size = new Size(110, 30);
            btnDoiMauDangChoi.TabIndex = 5;
            btnDoiMauDangChoi.Text = "Đổi màu";
            btnDoiMauDangChoi.UseVisualStyleBackColor = true;
            // 
            // pnlMauDangChoi
            // 
            pnlMauDangChoi.BorderStyle = BorderStyle.FixedSingle;
            pnlMauDangChoi.Location = new Point(208, 76);
            pnlMauDangChoi.Name = "pnlMauDangChoi";
            pnlMauDangChoi.Size = new Size(50, 26);
            pnlMauDangChoi.TabIndex = 4;
            // 
            // lblMauDangChoi
            // 
            lblMauDangChoi.AutoSize = true;
            lblMauDangChoi.Location = new Point(20, 76);
            lblMauDangChoi.Name = "lblMauDangChoi";
            lblMauDangChoi.Size = new Size(142, 20);
            lblMauDangChoi.TabIndex = 3;
            lblMauDangChoi.Text = "Màu máy Đang chơi";
            // 
            // btnDoiMauTrong
            // 
            btnDoiMauTrong.Location = new Point(287, 27);
            btnDoiMauTrong.Name = "btnDoiMauTrong";
            btnDoiMauTrong.Size = new Size(110, 30);
            btnDoiMauTrong.TabIndex = 2;
            btnDoiMauTrong.Text = "Đổi màu";
            btnDoiMauTrong.UseVisualStyleBackColor = true;
            // 
            // pnlMauTrong
            // 
            pnlMauTrong.BackColor = SystemColors.Control;
            pnlMauTrong.BorderStyle = BorderStyle.FixedSingle;
            pnlMauTrong.Location = new Point(208, 31);
            pnlMauTrong.Name = "pnlMauTrong";
            pnlMauTrong.Size = new Size(50, 26);
            pnlMauTrong.TabIndex = 1;
            // 
            // lblMauTrong
            // 
            lblMauTrong.AutoSize = true;
            lblMauTrong.Location = new Point(20, 31);
            lblMauTrong.Name = "lblMauTrong";
            lblMauTrong.Size = new Size(112, 20);
            lblMauTrong.TabIndex = 0;
            lblMauTrong.Text = "Màu máy Trống";
            // 
            // gbXemTruoc
            // 
            gbXemTruoc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gbXemTruoc.Controls.Add(lblPreviewSoDu);
            gbXemTruoc.Controls.Add(lblPreviewTime);
            gbXemTruoc.Controls.Add(lblPreviewHoiVien);
            gbXemTruoc.Controls.Add(lblPreviewTenMay);
            gbXemTruoc.Controls.Add(pnlPereviewMay);
            gbXemTruoc.Location = new Point(558, 42);
            gbXemTruoc.Name = "gbXemTruoc";
            gbXemTruoc.Size = new Size(508, 314);
            gbXemTruoc.TabIndex = 2;
            gbXemTruoc.TabStop = false;
            gbXemTruoc.Text = "Xem trước máy trạm (Live Preview)";
            // 
            // lblPreviewSoDu
            // 
            lblPreviewSoDu.AutoSize = true;
            lblPreviewSoDu.Location = new Point(313, 277);
            lblPreviewSoDu.Name = "lblPreviewSoDu";
            lblPreviewSoDu.Size = new Size(133, 20);
            lblPreviewSoDu.TabIndex = 4;
            lblPreviewSoDu.Text = "Số dư: 50.000 VNĐ";
            // 
            // lblPreviewTime
            // 
            lblPreviewTime.AutoSize = true;
            lblPreviewTime.Location = new Point(313, 233);
            lblPreviewTime.Name = "lblPreviewTime";
            lblPreviewTime.Size = new Size(160, 20);
            lblPreviewTime.TabIndex = 3;
            lblPreviewTime.Text = "Thời gian còn: 03:15:20";
            // 
            // lblPreviewHoiVien
            // 
            lblPreviewHoiVien.AutoSize = true;
            lblPreviewHoiVien.Location = new Point(6, 277);
            lblPreviewHoiVien.Name = "lblPreviewHoiVien";
            lblPreviewHoiVien.Size = new Size(162, 20);
            lblPreviewHoiVien.TabIndex = 2;
            lblPreviewHoiVien.Text = "Hội viên: nguyen_van_a";
            // 
            // lblPreviewTenMay
            // 
            lblPreviewTenMay.AutoSize = true;
            lblPreviewTenMay.Location = new Point(6, 233);
            lblPreviewTenMay.Name = "lblPreviewTenMay";
            lblPreviewTenMay.Size = new Size(184, 20);
            lblPreviewTenMay.TabIndex = 1;
            lblPreviewTenMay.Text = "MÁY 01 - ĐANG SỬ DỤNG";
            // 
            // pnlPereviewMay
            // 
            pnlPereviewMay.BorderStyle = BorderStyle.FixedSingle;
            pnlPereviewMay.Location = new Point(6, 25);
            pnlPereviewMay.Name = "pnlPereviewMay";
            pnlPereviewMay.Size = new Size(496, 192);
            pnlPereviewMay.TabIndex = 0;
            // 
            // gbSaoLuu
            // 
            gbSaoLuu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbSaoLuu.Controls.Add(lblTrangThaiBackup);
            gbSaoLuu.Controls.Add(prgTienTrinh);
            gbSaoLuu.Controls.Add(btnKhoiPhuc);
            gbSaoLuu.Controls.Add(btnSaoLuu);
            gbSaoLuu.Controls.Add(btnChonThuMuc);
            gbSaoLuu.Controls.Add(txtThuMucBackup);
            gbSaoLuu.Controls.Add(lblThuMucBackup);
            gbSaoLuu.Location = new Point(12, 362);
            gbSaoLuu.Name = "gbSaoLuu";
            gbSaoLuu.Size = new Size(1054, 200);
            gbSaoLuu.TabIndex = 3;
            gbSaoLuu.TabStop = false;
            gbSaoLuu.Text = "Sao lưu và Phục hồi cơ sở dữ liệu (.bak)";
            // 
            // lblThuMucBackup
            // 
            lblThuMucBackup.AutoSize = true;
            lblThuMucBackup.Location = new Point(20, 34);
            lblThuMucBackup.Name = "lblThuMucBackup";
            lblThuMucBackup.Size = new Size(195, 20);
            lblThuMucBackup.TabIndex = 0;
            lblThuMucBackup.Text = "Thư mục lưu trữ bản sao lưu";
            // 
            // txtThuMucBackup
            // 
            txtThuMucBackup.Location = new Point(221, 31);
            txtThuMucBackup.Name = "txtThuMucBackup";
            txtThuMucBackup.ReadOnly = true;
            txtThuMucBackup.Size = new Size(259, 26);
            txtThuMucBackup.TabIndex = 1;
            txtThuMucBackup.Text = "D:\\LapTrinhWin_DoAn\\Backup";
            // 
            // btnChonThuMuc
            // 
            btnChonThuMuc.Location = new Point(515, 28);
            btnChonThuMuc.Name = "btnChonThuMuc";
            btnChonThuMuc.Size = new Size(160, 31);
            btnChonThuMuc.TabIndex = 2;
            btnChonThuMuc.Text = "Chọn thư mục";
            btnChonThuMuc.UseVisualStyleBackColor = true;
            // 
            // btnSaoLuu
            // 
            btnSaoLuu.Location = new Point(20, 82);
            btnSaoLuu.Name = "btnSaoLuu";
            btnSaoLuu.Size = new Size(200, 38);
            btnSaoLuu.TabIndex = 3;
            btnSaoLuu.Text = "Sao lưu ngay (.bak)";
            btnSaoLuu.UseVisualStyleBackColor = true;
            // 
            // btnKhoiPhuc
            // 
            btnKhoiPhuc.Location = new Point(226, 82);
            btnKhoiPhuc.Name = "btnKhoiPhuc";
            btnKhoiPhuc.Size = new Size(200, 38);
            btnKhoiPhuc.TabIndex = 4;
            btnKhoiPhuc.Text = "Phục hồi từ file .bak";
            btnKhoiPhuc.UseVisualStyleBackColor = true;
            // 
            // prgTienTrinh
            // 
            prgTienTrinh.Location = new Point(20, 126);
            prgTienTrinh.Name = "prgTienTrinh";
            prgTienTrinh.Size = new Size(475, 28);
            prgTienTrinh.Style = ProgressBarStyle.Continuous;
            prgTienTrinh.TabIndex = 5;
            // 
            // lblTrangThaiBackup
            // 
            lblTrangThaiBackup.AutoSize = true;
            lblTrangThaiBackup.Location = new Point(20, 167);
            lblTrangThaiBackup.Name = "lblTrangThaiBackup";
            lblTrangThaiBackup.Size = new Size(308, 20);
            lblTrangThaiBackup.TabIndex = 6;
            lblTrangThaiBackup.Text = "Trạng thái: Sẵn sàng thực hiện sao lưu dữ liệu";
            // 
            // pnlChucNang
            // 
            pnlChucNang.BorderStyle = BorderStyle.FixedSingle;
            pnlChucNang.Controls.Add(btnDong);
            pnlChucNang.Controls.Add(btnMacDinh);
            pnlChucNang.Controls.Add(btnLuuCaiDat);
            pnlChucNang.Location = new Point(12, 568);
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.Size = new Size(1054, 50);
            pnlChucNang.TabIndex = 4;
            // 
            // btnLuuCaiDat
            // 
            btnLuuCaiDat.Location = new Point(249, 3);
            btnLuuCaiDat.Name = "btnLuuCaiDat";
            btnLuuCaiDat.Size = new Size(150, 40);
            btnLuuCaiDat.TabIndex = 0;
            btnLuuCaiDat.Text = "Lưu cấu hình";
            btnLuuCaiDat.UseVisualStyleBackColor = true;
            // 
            // btnMacDinh
            // 
            btnMacDinh.Location = new Point(452, 3);
            btnMacDinh.Name = "btnMacDinh";
            btnMacDinh.Size = new Size(150, 40);
            btnMacDinh.TabIndex = 1;
            btnMacDinh.Text = "Mặc định";
            btnMacDinh.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            btnDong.Location = new Point(655, 3);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(150, 40);
            btnDong.TabIndex = 2;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // frmCaiDatGiaoDien
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 638);
            Controls.Add(pnlChucNang);
            Controls.Add(gbSaoLuu);
            Controls.Add(gbXemTruoc);
            Controls.Add(gbGiaoDien);
            Controls.Add(lblTieuDe);
            Name = "frmCaiDatGiaoDien";
            Text = "Cài Đặt Giao Diện Và Sao Lưu";
            gbGiaoDien.ResumeLayout(false);
            gbGiaoDien.PerformLayout();
            gbXemTruoc.ResumeLayout(false);
            gbXemTruoc.PerformLayout();
            gbSaoLuu.ResumeLayout(false);
            gbSaoLuu.PerformLayout();
            pnlChucNang.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private GroupBox gbGiaoDien;
        private Label lblMauTrong;
        private Button btnDoiMauTrong;
        private Panel pnlMauTrong;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button btnDoiMauDangChoi;
        private Panel pnlMauDangChoi;
        private Label lblMauDangChoi;
        private TextBox txtFontHienThi;
        private Label lblFontHienThi;
        private Panel pnlMauBaoTri;
        private Label lblMauBaoTri;
        private Button btnDoiMauBaoTri;
        private Button btnChonFont;
        private GroupBox gbXemTruoc;
        private Panel pnlPereviewMay;
        private Label lblPreviewHoiVien;
        private Label lblPreviewTenMay;
        private Label lblPreviewSoDu;
        private Label lblPreviewTime;
        private GroupBox gbSaoLuu;
        private TextBox txtThuMucBackup;
        private Label lblThuMucBackup;
        private Button btnChonThuMuc;
        private ProgressBar prgTienTrinh;
        private Button btnKhoiPhuc;
        private Button btnSaoLuu;
        private Label lblTrangThaiBackup;
        private Panel pnlChucNang;
        private Button btnDong;
        private Button btnMacDinh;
        private Button btnLuuCaiDat;
    }
}
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
            tableMain = new TableLayoutPanel();
            pnlLeft = new Panel();
            gbGiaoDien = new GroupBox();
            lblMauTrong = new Label();
            pnlMauTrong = new Panel();
            btnDoiMauTrong = new Button();
            lblMauDangChoi = new Label();
            pnlMauDangChoi = new Panel();
            btnDoiMauDangChoi = new Button();
            lblMauBaoTri = new Label();
            pnlMauBaoTri = new Panel();
            btnDoiMauBaoTri = new Button();
            lblFontHienThi = new Label();
            txtFontHienThi = new TextBox();
            btnChonFont = new Button();
            gbSaoLuu = new GroupBox();
            lblThuMucBackup = new Label();
            txtThuMucBackup = new TextBox();
            btnChonThuMuc = new Button();
            btnSaoLuu = new Button();
            btnKhoiPhuc = new Button();
            prgTienTrinh = new ProgressBar();
            lblTrangThaiBackup = new Label();
            gbXemTruoc = new GroupBox();
            pnlPereviewMay = new Panel();
            pnlPreviewInfo = new Panel();
            lblPreviewTenMay = new Label();
            lblPreviewTime = new Label();
            lblPreviewHoiVien = new Label();
            lblPreviewSoDu = new Label();
            pnlChucNang = new Panel();
            btnLuuCaiDat = new Button();
            btnMacDinh = new Button();
            btnDong = new Button();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            saveFileDialog = new SaveFileDialog();
            tableMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            gbGiaoDien.SuspendLayout();
            gbSaoLuu.SuspendLayout();
            gbXemTruoc.SuspendLayout();
            pnlPreviewInfo.SuspendLayout();
            pnlChucNang.SuspendLayout();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTieuDe.Location = new Point(0, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(960, 45);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "CÀI ĐẶT GIAO DIỆN VÀ SAO LƯU HỆ THỐNG";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableMain
            // 
            tableMain.ColumnCount = 2;
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableMain.Controls.Add(pnlLeft, 0, 0);
            tableMain.Controls.Add(gbXemTruoc, 1, 0);
            tableMain.Dock = DockStyle.Fill;
            tableMain.Location = new Point(0, 45);
            tableMain.Name = "tableMain";
            tableMain.RowCount = 1;
            tableMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableMain.Size = new Size(960, 425);
            tableMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.AutoScroll = true;
            pnlLeft.Controls.Add(gbGiaoDien);
            pnlLeft.Controls.Add(gbSaoLuu);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(3, 3);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(474, 419);
            pnlLeft.TabIndex = 0;
            // 
            // gbGiaoDien
            // 
            gbGiaoDien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbGiaoDien.Controls.Add(lblMauTrong);
            gbGiaoDien.Controls.Add(pnlMauTrong);
            gbGiaoDien.Controls.Add(btnDoiMauTrong);
            gbGiaoDien.Controls.Add(lblMauDangChoi);
            gbGiaoDien.Controls.Add(pnlMauDangChoi);
            gbGiaoDien.Controls.Add(btnDoiMauDangChoi);
            gbGiaoDien.Controls.Add(lblMauBaoTri);
            gbGiaoDien.Controls.Add(pnlMauBaoTri);
            gbGiaoDien.Controls.Add(btnDoiMauBaoTri);
            gbGiaoDien.Controls.Add(lblFontHienThi);
            gbGiaoDien.Controls.Add(txtFontHienThi);
            gbGiaoDien.Controls.Add(btnChonFont);
            gbGiaoDien.Location = new Point(5, 5);
            gbGiaoDien.Name = "gbGiaoDien";
            gbGiaoDien.Size = new Size(460, 180);
            gbGiaoDien.TabIndex = 0;
            gbGiaoDien.TabStop = false;
            gbGiaoDien.Text = "Tùy chỉnh màu sắc và Phông chữ phòng máy";
            // 
            // lblMauTrong
            // 
            lblMauTrong.AutoSize = true;
            lblMauTrong.Location = new Point(15, 26);
            lblMauTrong.Name = "lblMauTrong";
            lblMauTrong.Size = new Size(112, 20);
            lblMauTrong.TabIndex = 0;
            lblMauTrong.Text = "Màu máy Trống";
            // 
            // pnlMauTrong
            // 
            pnlMauTrong.BorderStyle = BorderStyle.FixedSingle;
            pnlMauTrong.Location = new Point(189, 22);
            pnlMauTrong.Name = "pnlMauTrong";
            pnlMauTrong.Size = new Size(45, 26);
            pnlMauTrong.TabIndex = 1;
            // 
            // btnDoiMauTrong
            // 
            btnDoiMauTrong.Location = new Point(249, 20);
            btnDoiMauTrong.Name = "btnDoiMauTrong";
            btnDoiMauTrong.Size = new Size(100, 30);
            btnDoiMauTrong.TabIndex = 2;
            btnDoiMauTrong.Text = "Đổi màu";
            btnDoiMauTrong.UseVisualStyleBackColor = true;
            // 
            // lblMauDangChoi
            // 
            lblMauDangChoi.AutoSize = true;
            lblMauDangChoi.Location = new Point(15, 62);
            lblMauDangChoi.Name = "lblMauDangChoi";
            lblMauDangChoi.Size = new Size(142, 20);
            lblMauDangChoi.TabIndex = 3;
            lblMauDangChoi.Text = "Màu máy Đang chơi";
            // 
            // pnlMauDangChoi
            // 
            pnlMauDangChoi.BorderStyle = BorderStyle.FixedSingle;
            pnlMauDangChoi.Location = new Point(189, 58);
            pnlMauDangChoi.Name = "pnlMauDangChoi";
            pnlMauDangChoi.Size = new Size(45, 26);
            pnlMauDangChoi.TabIndex = 4;
            // 
            // btnDoiMauDangChoi
            // 
            btnDoiMauDangChoi.Location = new Point(249, 56);
            btnDoiMauDangChoi.Name = "btnDoiMauDangChoi";
            btnDoiMauDangChoi.Size = new Size(100, 30);
            btnDoiMauDangChoi.TabIndex = 5;
            btnDoiMauDangChoi.Text = "Đổi màu";
            btnDoiMauDangChoi.UseVisualStyleBackColor = true;
            // 
            // lblMauBaoTri
            // 
            lblMauBaoTri.AutoSize = true;
            lblMauBaoTri.Location = new Point(15, 98);
            lblMauBaoTri.Name = "lblMauBaoTri";
            lblMauBaoTri.Size = new Size(166, 20);
            lblMauBaoTri.TabIndex = 6;
            lblMauBaoTri.Text = "Màu máy Bảo trì / Khóa";
            // 
            // pnlMauBaoTri
            // 
            pnlMauBaoTri.BorderStyle = BorderStyle.FixedSingle;
            pnlMauBaoTri.Location = new Point(189, 94);
            pnlMauBaoTri.Name = "pnlMauBaoTri";
            pnlMauBaoTri.Size = new Size(45, 26);
            pnlMauBaoTri.TabIndex = 7;
            // 
            // btnDoiMauBaoTri
            // 
            btnDoiMauBaoTri.Location = new Point(249, 92);
            btnDoiMauBaoTri.Name = "btnDoiMauBaoTri";
            btnDoiMauBaoTri.Size = new Size(100, 30);
            btnDoiMauBaoTri.TabIndex = 8;
            btnDoiMauBaoTri.Text = "Đổi màu";
            btnDoiMauBaoTri.UseVisualStyleBackColor = true;
            // 
            // lblFontHienThi
            // 
            lblFontHienThi.AutoSize = true;
            lblFontHienThi.Location = new Point(15, 138);
            lblFontHienThi.Name = "lblFontHienThi";
            lblFontHienThi.Size = new Size(132, 20);
            lblFontHienThi.TabIndex = 9;
            lblFontHienThi.Text = "Phông chữ hiển thị";
            // 
            // txtFontHienThi
            // 
            txtFontHienThi.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFontHienThi.Location = new Point(175, 134);
            txtFontHienThi.Name = "txtFontHienThi";
            txtFontHienThi.ReadOnly = true;
            txtFontHienThi.Size = new Size(165, 27);
            txtFontHienThi.TabIndex = 10;
            txtFontHienThi.Text = "Segoe UI, 10pt";
            // 
            // btnChonFont
            // 
            btnChonFont.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChonFont.Location = new Point(350, 132);
            btnChonFont.Name = "btnChonFont";
            btnChonFont.Size = new Size(95, 30);
            btnChonFont.TabIndex = 11;
            btnChonFont.Text = "Chọn Font";
            btnChonFont.UseVisualStyleBackColor = true;
            // 
            // gbSaoLuu
            // 
            gbSaoLuu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbSaoLuu.Controls.Add(lblThuMucBackup);
            gbSaoLuu.Controls.Add(txtThuMucBackup);
            gbSaoLuu.Controls.Add(btnChonThuMuc);
            gbSaoLuu.Controls.Add(btnSaoLuu);
            gbSaoLuu.Controls.Add(btnKhoiPhuc);
            gbSaoLuu.Controls.Add(prgTienTrinh);
            gbSaoLuu.Controls.Add(lblTrangThaiBackup);
            gbSaoLuu.Location = new Point(5, 190);
            gbSaoLuu.Name = "gbSaoLuu";
            gbSaoLuu.Size = new Size(460, 210);
            gbSaoLuu.TabIndex = 1;
            gbSaoLuu.TabStop = false;
            gbSaoLuu.Text = "Sao lưu và Phục hồi cơ sở dữ liệu (.bak)";
            // 
            // lblThuMucBackup
            // 
            lblThuMucBackup.AutoSize = true;
            lblThuMucBackup.Location = new Point(15, 24);
            lblThuMucBackup.Name = "lblThuMucBackup";
            lblThuMucBackup.Size = new Size(195, 20);
            lblThuMucBackup.TabIndex = 0;
            lblThuMucBackup.Text = "Thư mục lưu trữ bản sao lưu";
            // 
            // txtThuMucBackup
            // 
            txtThuMucBackup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtThuMucBackup.Location = new Point(15, 48);
            txtThuMucBackup.Name = "txtThuMucBackup";
            txtThuMucBackup.ReadOnly = true;
            txtThuMucBackup.Size = new Size(325, 27);
            txtThuMucBackup.TabIndex = 1;
            txtThuMucBackup.Text = "D:\\LapTrinhWin_DoAn\\Backup";
            // 
            // btnChonThuMuc
            // 
            btnChonThuMuc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChonThuMuc.Location = new Point(350, 46);
            btnChonThuMuc.Name = "btnChonThuMuc";
            btnChonThuMuc.Size = new Size(95, 30);
            btnChonThuMuc.TabIndex = 2;
            btnChonThuMuc.Text = "Chọn thư mục";
            btnChonThuMuc.UseVisualStyleBackColor = true;
            // 
            // btnSaoLuu
            // 
            btnSaoLuu.Location = new Point(15, 86);
            btnSaoLuu.Name = "btnSaoLuu";
            btnSaoLuu.Size = new Size(150, 36);
            btnSaoLuu.TabIndex = 3;
            btnSaoLuu.Text = "Sao lưu ngay (.bak)";
            btnSaoLuu.UseVisualStyleBackColor = true;
            // 
            // btnKhoiPhuc
            // 
            btnKhoiPhuc.Location = new Point(175, 86);
            btnKhoiPhuc.Name = "btnKhoiPhuc";
            btnKhoiPhuc.Size = new Size(160, 36);
            btnKhoiPhuc.TabIndex = 4;
            btnKhoiPhuc.Text = "Phục hồi từ file .bak";
            btnKhoiPhuc.UseVisualStyleBackColor = true;
            // 
            // prgTienTrinh
            // 
            prgTienTrinh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            prgTienTrinh.Location = new Point(15, 134);
            prgTienTrinh.Name = "prgTienTrinh";
            prgTienTrinh.Size = new Size(430, 24);
            prgTienTrinh.Style = ProgressBarStyle.Continuous;
            prgTienTrinh.TabIndex = 5;
            // 
            // lblTrangThaiBackup
            // 
            lblTrangThaiBackup.AutoSize = true;
            lblTrangThaiBackup.Location = new Point(15, 170);
            lblTrangThaiBackup.Name = "lblTrangThaiBackup";
            lblTrangThaiBackup.Size = new Size(308, 20);
            lblTrangThaiBackup.TabIndex = 6;
            lblTrangThaiBackup.Text = "Trạng thái: Sẵn sàng thực hiện sao lưu dữ liệu";
            // 
            // gbXemTruoc
            // 
            gbXemTruoc.Controls.Add(pnlPereviewMay);
            gbXemTruoc.Controls.Add(pnlPreviewInfo);
            gbXemTruoc.Dock = DockStyle.Fill;
            gbXemTruoc.Location = new Point(483, 3);
            gbXemTruoc.Name = "gbXemTruoc";
            gbXemTruoc.Size = new Size(474, 419);
            gbXemTruoc.TabIndex = 1;
            gbXemTruoc.TabStop = false;
            gbXemTruoc.Text = "Xem trước máy trạm (Live Preview)";
            // 
            // pnlPereviewMay
            // 
            pnlPereviewMay.BorderStyle = BorderStyle.FixedSingle;
            pnlPereviewMay.Dock = DockStyle.Fill;
            pnlPereviewMay.Location = new Point(3, 23);
            pnlPereviewMay.Name = "pnlPereviewMay";
            pnlPereviewMay.Size = new Size(468, 333);
            pnlPereviewMay.TabIndex = 0;
            // 
            // pnlPreviewInfo
            // 
            pnlPreviewInfo.Controls.Add(lblPreviewTenMay);
            pnlPreviewInfo.Controls.Add(lblPreviewTime);
            pnlPreviewInfo.Controls.Add(lblPreviewHoiVien);
            pnlPreviewInfo.Controls.Add(lblPreviewSoDu);
            pnlPreviewInfo.Dock = DockStyle.Bottom;
            pnlPreviewInfo.Location = new Point(3, 356);
            pnlPreviewInfo.Name = "pnlPreviewInfo";
            pnlPreviewInfo.Size = new Size(468, 60);
            pnlPreviewInfo.TabIndex = 1;
            // 
            // lblPreviewTenMay
            // 
            lblPreviewTenMay.AutoSize = true;
            lblPreviewTenMay.Location = new Point(10, 8);
            lblPreviewTenMay.Name = "lblPreviewTenMay";
            lblPreviewTenMay.Size = new Size(184, 20);
            lblPreviewTenMay.TabIndex = 0;
            lblPreviewTenMay.Text = "MÁY 01 - ĐANG SỬ DỤNG";
            // 
            // lblPreviewTime
            // 
            lblPreviewTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPreviewTime.AutoSize = true;
            lblPreviewTime.Location = new Point(298, 8);
            lblPreviewTime.Name = "lblPreviewTime";
            lblPreviewTime.Size = new Size(160, 20);
            lblPreviewTime.TabIndex = 1;
            lblPreviewTime.Text = "Thời gian còn: 03:15:20";
            // 
            // lblPreviewHoiVien
            // 
            lblPreviewHoiVien.AutoSize = true;
            lblPreviewHoiVien.Location = new Point(10, 32);
            lblPreviewHoiVien.Name = "lblPreviewHoiVien";
            lblPreviewHoiVien.Size = new Size(162, 20);
            lblPreviewHoiVien.TabIndex = 2;
            lblPreviewHoiVien.Text = "Hội viên: nguyen_van_a";
            // 
            // lblPreviewSoDu
            // 
            lblPreviewSoDu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPreviewSoDu.AutoSize = true;
            lblPreviewSoDu.Location = new Point(325, 32);
            lblPreviewSoDu.Name = "lblPreviewSoDu";
            lblPreviewSoDu.Size = new Size(133, 20);
            lblPreviewSoDu.TabIndex = 3;
            lblPreviewSoDu.Text = "Số dư: 50.000 VNĐ";
            // 
            // pnlChucNang
            // 
            pnlChucNang.Controls.Add(btnLuuCaiDat);
            pnlChucNang.Controls.Add(btnMacDinh);
            pnlChucNang.Controls.Add(btnDong);
            pnlChucNang.Dock = DockStyle.Bottom;
            pnlChucNang.Location = new Point(0, 470);
            pnlChucNang.Name = "pnlChucNang";
            pnlChucNang.Size = new Size(960, 50);
            pnlChucNang.TabIndex = 2;
            // 
            // btnLuuCaiDat
            // 
            btnLuuCaiDat.Location = new Point(12, 6);
            btnLuuCaiDat.Name = "btnLuuCaiDat";
            btnLuuCaiDat.Size = new Size(140, 38);
            btnLuuCaiDat.TabIndex = 0;
            btnLuuCaiDat.Text = "Lưu cấu hình";
            btnLuuCaiDat.UseVisualStyleBackColor = true;
            // 
            // btnMacDinh
            // 
            btnMacDinh.Location = new Point(162, 6);
            btnMacDinh.Name = "btnMacDinh";
            btnMacDinh.Size = new Size(140, 38);
            btnMacDinh.TabIndex = 1;
            btnMacDinh.Text = "Mặc định";
            btnMacDinh.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.Location = new Point(810, 6);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(140, 38);
            btnDong.TabIndex = 2;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // frmCaiDatGiaoDien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 520);
            Controls.Add(tableMain);
            Controls.Add(pnlChucNang);
            Controls.Add(lblTieuDe);
            MinimumSize = new Size(850, 450);
            Name = "frmCaiDatGiaoDien";
            Text = "Cài Đặt Giao Diện Và Sao Lưu";
            tableMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            gbGiaoDien.ResumeLayout(false);
            gbGiaoDien.PerformLayout();
            gbSaoLuu.ResumeLayout(false);
            gbSaoLuu.PerformLayout();
            gbXemTruoc.ResumeLayout(false);
            pnlPreviewInfo.ResumeLayout(false);
            pnlPreviewInfo.PerformLayout();
            pnlChucNang.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTieuDe;
        private TableLayoutPanel tableMain;
        private Panel pnlLeft;
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
        private Panel pnlPreviewInfo;
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
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private SaveFileDialog saveFileDialog;
    }
}
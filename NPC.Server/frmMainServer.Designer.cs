namespace NPC.Server
{
    partial class frmMainServer
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
            ToolStripMenuItem tsmiDoanhThu;
            msMenuChinh = new MenuStrip();
            tsmiHeThong = new ToolStripMenuItem();
            tsmiCauHinh = new ToolStripMenuItem();
            tsmiTaiKhoan = new ToolStripMenuItem();
            tsmiDangXuat = new ToolStripMenuItem();
            tsmiCaiDatGiaoDien = new ToolStripMenuItem();
            tsmiThoat = new ToolStripMenuItem();
            tsmiQuanLy = new ToolStripMenuItem();
            tsmiHoiVien = new ToolStripMenuItem();
            tsmiMonAn = new ToolStripMenuItem();
            tsmiTroGiup = new ToolStripMenuItem();
            tsmiHuongDan = new ToolStripMenuItem();
            tsmiThongTin = new ToolStripMenuItem();
            tsmiBaoCao = new ToolStripMenuItem();
            tsmiNhatKyHV = new ToolStripMenuItem();
            tsmiThongKe = new ToolStripMenuItem();
            tsmiNhatKyHT = new ToolStripMenuItem();
            tsmiDoanhThu = new ToolStripMenuItem();
            msMenuChinh.SuspendLayout();
            SuspendLayout();
            // 
            // tsmiDoanhThu
            // 
            tsmiDoanhThu.Name = "tsmiDoanhThu";
            tsmiDoanhThu.Size = new Size(267, 26);
            tsmiDoanhThu.Text = "Doanh thu";
            // 
            // msMenuChinh
            // 
            msMenuChinh.ImageScalingSize = new Size(20, 20);
            tsmiSoDoPhongMay = new ToolStripMenuItem();
            msMenuChinh.Items.AddRange(new ToolStripItem[] { tsmiHeThong, tsmiSoDoPhongMay, tsmiQuanLy, tsmiTroGiup, tsmiBaoCao });
            msMenuChinh.Location = new Point(0, 0);
            msMenuChinh.Name = "msMenuChinh";
            msMenuChinh.Size = new Size(1262, 28);
            msMenuChinh.TabIndex = 1;
            msMenuChinh.Text = "menuStrip1";
            // 
            // tsmiHeThong
            // 
            tsmiHeThong.DropDownItems.AddRange(new ToolStripItem[] { tsmiCauHinh, tsmiTaiKhoan, tsmiDangXuat, tsmiCaiDatGiaoDien, tsmiThoat });
            tsmiHeThong.Name = "tsmiHeThong";
            tsmiHeThong.Size = new Size(85, 24);
            tsmiHeThong.Text = "Hệ thống";
            // 
            // tsmiCauHinh
            // 
            tsmiCauHinh.Name = "tsmiCauHinh";
            tsmiCauHinh.Size = new Size(206, 26);
            tsmiCauHinh.Text = "Cấu hình CSDL";
            // 
            // tsmiTaiKhoan
            // 
            tsmiTaiKhoan.Name = "tsmiTaiKhoan";
            tsmiTaiKhoan.Size = new Size(206, 26);
            tsmiTaiKhoan.Text = "Tài khoản";
            // 
            // tsmiDangXuat
            // 
            tsmiDangXuat.Name = "tsmiDangXuat";
            tsmiDangXuat.Size = new Size(206, 26);
            tsmiDangXuat.Text = "Đăng xuất";
            tsmiDangXuat.Click += tsmiDangXuat_Click;
            // 
            // tsmiCaiDatGiaoDien
            // 
            tsmiCaiDatGiaoDien.Name = "tsmiCaiDatGiaoDien";
            tsmiCaiDatGiaoDien.Size = new Size(206, 26);
            tsmiCaiDatGiaoDien.Text = "Cài đặt giao diện";
            tsmiCaiDatGiaoDien.Click += tsmiCaiDatGiaoDien_Click;
            // 
            // tsmiThoat
            // 
            tsmiThoat.Name = "tsmiThoat";
            tsmiThoat.Size = new Size(206, 26);
            tsmiThoat.Text = "Thoát";
            tsmiThoat.Click += tsmiThoat_Click;
            // 
            // tsmiSoDoPhongMay
            // 
            tsmiSoDoPhongMay.Name = "tsmiSoDoPhongMay";
            tsmiSoDoPhongMay.Size = new Size(137, 24);
            tsmiSoDoPhongMay.Text = "Sơ đồ phòng máy";
            tsmiSoDoPhongMay.Click += tsmiSoDoPhongMay_Click;
            //             // 
            // tsmiQuanLy
            // 
            tsmiQuanLy.DropDownItems.AddRange(new ToolStripItem[] { tsmiHoiVien, tsmiMonAn });
            tsmiQuanLy.Name = "tsmiQuanLy";
            tsmiQuanLy.Size = new Size(73, 24);
            tsmiQuanLy.Text = "Quản lý";
            // 
            // tsmiHoiVien
            // 
            tsmiHoiVien.Name = "tsmiHoiVien";
            tsmiHoiVien.Size = new Size(211, 26);
            tsmiHoiVien.Text = "Hội viên/ nạp tiền";
            tsmiHoiVien.Click += tsmiHoiVien_Click;
            // 
            // tsmiMonAn
            // 
            tsmiMonAn.Name = "tsmiMonAn";
            tsmiMonAn.Size = new Size(211, 26);
            tsmiMonAn.Text = "Món ăn";
            tsmiMonAn.Click += tsmiMonAn_Click;
            // 
            // tsmiTroGiup
            // 
            tsmiTroGiup.DropDownItems.AddRange(new ToolStripItem[] { tsmiHuongDan, tsmiThongTin });
            tsmiTroGiup.Name = "tsmiTroGiup";
            tsmiTroGiup.Size = new Size(78, 24);
            tsmiTroGiup.Text = "Trợ giúp";
            // 
            // tsmiHuongDan
            // 
            tsmiHuongDan.Name = "tsmiHuongDan";
            tsmiHuongDan.Size = new Size(224, 26);
            tsmiHuongDan.Text = "Hướng dẫn sử dụng";
            // 
            // tsmiThongTin
            // 
            tsmiThongTin.Name = "tsmiThongTin";
            tsmiThongTin.Size = new Size(224, 26);
            tsmiThongTin.Text = "Thông tin";
            // 
            // tsmiBaoCao
            // 
            tsmiBaoCao.DropDownItems.AddRange(new ToolStripItem[] { tsmiDoanhThu, tsmiNhatKyHV, tsmiThongKe, tsmiNhatKyHT });
            tsmiBaoCao.Name = "tsmiBaoCao";
            tsmiBaoCao.Size = new Size(77, 24);
            tsmiBaoCao.Text = "Báo cáo";
            // 
            // tsmiNhatKyHV
            // 
            tsmiNhatKyHV.Name = "tsmiNhatKyHV";
            tsmiNhatKyHV.Size = new Size(267, 26);
            tsmiNhatKyHV.Text = "Nhật ký hội viên";
            // 
            // tsmiThongKe
            // 
            tsmiThongKe.Name = "tsmiThongKe";
            tsmiThongKe.Size = new Size(267, 26);
            tsmiThongKe.Text = "Thống kê dịch vụ bán chạy";
            // 
            // tsmiNhatKyHT
            // 
            tsmiNhatKyHT.Name = "tsmiNhatKyHT";
            tsmiNhatKyHT.Size = new Size(267, 26);
            tsmiNhatKyHT.Text = "Nhật ký hệ thống";
            // 
            // frmMainServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 721);
            Controls.Add(msMenuChinh);
            HelpButton = true;
            IsMdiContainer = true;
            MainMenuStrip = msMenuChinh;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmMainServer";
            Text = "Quản lý Server Máy";
            Load += frmMainServer_Load;
            msMenuChinh.ResumeLayout(false);
            msMenuChinh.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip msMenuChinh;
        private ToolStripMenuItem tsmiHeThong;
        private ToolStripMenuItem tsmiSoDoPhongMay;
        private ToolStripMenuItem tsmiQuanLy;
        private ToolStripMenuItem tsmiBaoCao;
        private ToolStripMenuItem tsmiTroGiup;
        private ToolStripMenuItem tsmiCauHinh;
        private ToolStripMenuItem tsmiTaiKhoan;
        private ToolStripMenuItem tsmiDangXuat;
        private ToolStripMenuItem tsmiCaiDatGiaoDien;
        private ToolStripMenuItem tsmiHoiVien;
        private ToolStripMenuItem tsmiMonAn;
        private ToolStripMenuItem tsmiDoanhThu;
        private ToolStripMenuItem tsmiNhatKyHV;
        private ToolStripMenuItem tsmiThongKe;
        private ToolStripMenuItem tsmiNhatKyHT;
        private ToolStripMenuItem tsmiHuongDan;
        private ToolStripMenuItem tsmiThongTin;
        private ToolStripMenuItem tsmiThoat;
    }
}
using NPC.BLL;
using NPC.DAL;
using NPC.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPC.Server
{
    public partial class frmMainServer : Form
    {
        // Timer poll yêu cầu nạp tiền từ Client (cứ 3 giây kiểm tra một lần)
        private readonly System.Windows.Forms.Timer _tmrPollNapTien = new System.Windows.Forms.Timer { Interval = 3000 };

        // Tránh hiển thị nhiều dialog cùng lúc
        private bool _dangXuLyYeuCau = false;

        public frmMainServer()
        {
            InitializeComponent();
            _tmrPollNapTien.Tick += TmrPollNapTien_Tick;
        }

        private void OpenMdiChildForm<T>() where T : Form, new()
        {
            OpenMdiChildForm(() => new T());
        }

        private void OpenMdiChildForm<T>(Func<T> formFactory) where T : Form
        {
            T? checkForm = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (checkForm != null)
            {
                checkForm.WindowState = FormWindowState.Maximized;
                checkForm.BringToFront();
                checkForm.Activate();
                return;
            }
            foreach (Form child in this.MdiChildren.ToList())
            {
                child.Close();
            }
            T fm = formFactory();
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }

        private void frmMainServer_Load(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmSoDoPhongMay>();
            _tmrPollNapTien.Start();  
        }

        private void frmMainServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tmrPollNapTien.Stop();
        }

   

        private void TmrPollNapTien_Tick(object? sender, EventArgs e)
        {
            if (_dangXuLyYeuCau) return; 

            try
            {
                List<YeuCauNapTienDTO> danhSach = GiaoDichBLL.LayYeuCauChoDuyet();
                if (danhSach == null || danhSach.Count == 0) return;

                _dangXuLyYeuCau = true;
                _tmrPollNapTien.Stop();   
                foreach (var yc in danhSach)
                {
                    XuLyYeuCauNapTien(yc);
                }
            }
            catch {  }
            finally
            {
                _dangXuLyYeuCau = false;
                _tmrPollNapTien.Start();   
            }
        }

        private void XuLyYeuCauNapTien(YeuCauNapTienDTO yc)
        {
            string hoTen = yc.GhiChu ?? $"Hội viên #{yc.MaHoiVien}";

            string noiDung =
                $"📋  YÊU CẦU NẠP TIỀN TỪ KHÁCH HÀNG\n\n" +
                $"  Máy         : {yc.TenMay}\n" +
                $"  Khách       : {hoTen}  (ID: {yc.MaHoiVien})\n" +
                $"  Số tiền nạp : {yc.SoTienNap:N0} đ\n" +
                $"  Hình thức   : {yc.HinhThucThanhToan}\n" +
                $"  Thời gian   : {yc.ThoiGianGui:HH:mm:ss dd/MM/yyyy}\n\n" +
                "Bạn có xác nhận khách đã thanh toán và đồng ý nạp tiền không?";

            this.Invoke((MethodInvoker)delegate
            {
                this.BringToFront();
                this.Activate();
            });

            DialogResult dr = MessageBox.Show(
                this, noiDung,
                "XÁC NHẬN NẠP TIỀN – YÊU CẦU TỪ MÁY KHÁCH",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);  

            if (dr == DialogResult.Yes)
            {
                try
                {
                    int maNhanVien = 1;
                    KetQuaNapTienDTO kq = GiaoDichBLL.DuyetYeuCauNapTien(yc, maNhanVien);

                    MessageBox.Show(this,
                        $"✅  Đã nạp tiền thành công!\n\n" +
                        $"  Khách : {kq.HoTen}\n" +
                        $"  Nạp   : +{yc.SoTienNap:N0} đ\n" +
                        $"  Số dư : {kq.SoDuTruoc:N0} → {kq.SoDuMoi:N0} đ",
                        "Nạp Tiền Thành Công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                        $"❌  Nạp tiền thất bại!\n\nLỗi: {ex.Message}",
                        "Lỗi nạp tiền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    GiaoDichBLL.TuChoiYeuCauNapTien(yc.MaYeuCau);

                    MessageBox.Show(this,
                        $"Đã từ chối yêu cầu nạp tiền của {hoTen} ({yc.SoTienNap:N0} đ).\n" +
                        "Máy khách sẽ nhận được thông báo.",
                        "Đã Từ Chối",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                        $"Lỗi khi từ chối: {ex.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void tsmiThoat_Click(object sender, EventArgs e)
        {
            string user = !string.IsNullOrEmpty(frmDangNhapServer.CurrentUser) ? frmDangNhapServer.CurrentUser : "admin";
            Logger.LogBusiness(user, "Thoát hệ thống", "Người dùng đã đóng ứng dụng Server.");
            Application.Exit();
        }

        private void tsmiDangXuat_Click(object sender, EventArgs e)
        {
            string user = !string.IsNullOrEmpty(frmDangNhapServer.CurrentUser) ? frmDangNhapServer.CurrentUser : "admin";
            Logger.LogBusiness(user, "Đăng xuất", "Đăng xuất khỏi hệ thống Server.");

            Hide();
            HeThongBLL.DatLaiKetNoi();
            frmDangNhapServer fm = new frmDangNhapServer();
            fm.ShowDialog();
            Close();
        }

        private void tsmiHoiVien_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmQuanLyHoiVien>();
        }

        private void tsmiSoDoPhongMay_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmSoDoPhongMay>();
        }

        private void tsmiMonAn_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm<frmQuanLyMonAn>();
        }

        private void tsmiNhatKyHV_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmXemNhatKy(frmXemNhatKy.LoaiNhatKy.NhatKyHoiVien));
        }

        private void tsmiNhatKyHT_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(() => new frmXemNhatKy(frmXemNhatKy.LoaiNhatKy.NhatKyHeThong));
        }
    }
}

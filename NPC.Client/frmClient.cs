using System;
using System.Drawing;
using System.Windows.Forms;
using NPC.BLL;
using NPC.DTO;

namespace NPC.Client
{
    public partial class frmClient : Form
    {
        private const string TenMay = "MAY-09";
        private const decimal SoDuToiThieu = 5000m;
        private const int PhutCanhBao = 5;

        private int _maMay;
        private decimal _donGiaGio;
        private long _maPhienChoi;
        private int _maHoiVien;
        private decimal _soDuBanDau;
        private DateTime _batDau;
        private bool _choPhepThoat;
        private frmGoiMonClient? _frmGoiMon;

        private bool _isDragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private bool _hasDragged = false;

        public frmClient()
        {
            InitializeComponent();

            btnIconThuGon.MouseDown += BtnIconThuGon_MouseDown;
            btnIconThuGon.MouseMove += BtnIconThuGon_MouseMove;
            btnIconThuGon.MouseUp += BtnIconThuGon_MouseUp;
        }

        private void BtnIconThuGon_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _hasDragged = false;
                _dragCursorPoint = Cursor.Position;
                _dragFormPoint = this.Location;
            }
        }

        private void BtnIconThuGon_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                if (Math.Abs(diff.X) > 3 || Math.Abs(diff.Y) > 3)
                {
                    _hasDragged = true;
                }
                this.Location = Point.Add(_dragFormPoint, new Size(diff));
            }
        }

        private void BtnIconThuGon_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void frmClient_Load(object? sender, EventArgs e)
        {
            try
            {
                ClientThongTinMayDTO? may = ClientBLL.KhoiTaoMayTram(TenMay);
                if (may == null)
                {
                    MessageBox.Show($"Không tìm thấy máy \"{TenMay}\" trong CSDL.", "Lỗi cấu hình",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ThoatUngDung();
                    return;
                }
                _maMay = may.MaMay;
                _donGiaGio = may.DonGiaGio;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL:\n" + ex.Message, "Lỗi kết nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ThoatUngDung();
                return;
            }

            lblTenMay.Text = TenMay;
            KhoaMay("Máy đang khóa – vui lòng đăng nhập", false);
            tmrDongBo.Start();
        }

        private void frmClient_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!_choPhepThoat) e.Cancel = true;
        }

        private void frmClient_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show(this, "Thoát chương trình máy trạm?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ClientBLL.TatMay(_maMay, TenMay, _maPhienChoi);
                    ThoatUngDung();
                }
            }
        }

        private void frmClient_Resize(object? sender, EventArgs e) => CanGiuaPanelDangNhap();

        private void ThoatUngDung()
        {
            _choPhepThoat = true;
            tmrDemGio.Stop();
            tmrTruTien.Stop();
            tmrDongBo.Stop();
            Application.Exit();
        }

        private void CanGiuaPanelDangNhap()
        {
            if (!pnlKhoa.Visible) return;
            pnlDangNhap.Left = Math.Max(0, (pnlKhoa.ClientSize.Width - pnlDangNhap.Width) / 2);
            pnlDangNhap.Top = Math.Max(0, (pnlKhoa.ClientSize.Height - pnlDangNhap.Height) / 2);
            pnlDangNhap.BringToFront();
        }

        private void KhoaMay(string thongBao, bool laLoi)
        {
            tmrDemGio.Stop();
            tmrTruTien.Stop();
            _maPhienChoi = 0;
            _maHoiVien = 0;
            _frmGoiMon?.DongKhongHoi();

            pnlThanhNoi.Visible = false;
            btnIconThuGon.Visible = false;
            WindowState = FormWindowState.Maximized;
            TopMost = !System.Diagnostics.Debugger.IsAttached;
            pnlKhoa.Visible = true;
            pnlKhoa.BringToFront();
            CanGiuaPanelDangNhap();

            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            HienThongBao(thongBao, laLoi);
            Activate();
            txtTenDangNhap.Focus();
        }

        private void BatDauPhienTrenGiaoDien(long maPhien, int maHoiVien, decimal soDu, string tenDangNhap, int giayDaChoi)
        {
            _maPhienChoi = maPhien;
            _maHoiVien = maHoiVien;
            _soDuBanDau = soDu;
            _batDau = DateTime.Now.AddSeconds(-giayDaChoi);

            lblTenHoiVien.Text = maHoiVien > 0 ? "Hội viên: " + tenDangNhap : "Khách vãng lai";
            lblDonGiaGio.Text = $"Giá giờ: {_donGiaGio:N0} đ/giờ";
            lblThoiGianConLai.ForeColor = Color.Black;
            CapNhatThoiGian();
            CapNhatSoDu();

            pnlKhoa.Visible = false;
            btnIconThuGon.Visible = false;
            HienThiThanhNoiDayDu();

            tmrDemGio.Start();
            tmrTruTien.Start();
        }

        private void HienThiThanhNoiDayDu()
        {
            btnIconThuGon.Visible = false;
            WindowState = FormWindowState.Normal;
            MinimumSize = Size.Empty;
            pnlThanhNoi.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pnlThanhNoi.Location = Point.Empty;
            ClientSize = pnlThanhNoi.Size;
            Rectangle vung = Screen.PrimaryScreen?.WorkingArea ?? new Rectangle(0, 0, 1280, 720);
            Location = new Point(vung.Right - Width - 10, vung.Top + 10);
            pnlThanhNoi.Visible = true;
            pnlThanhNoi.BringToFront();
            TopMost = !System.Diagnostics.Debugger.IsAttached;
        }

        private void HienThiIconThuGon()
        {
            pnlThanhNoi.Visible = false;
            WindowState = FormWindowState.Normal;
            MinimumSize = Size.Empty;
            btnIconThuGon.Location = Point.Empty;
            ClientSize = btnIconThuGon.Size;
            Rectangle vung = Screen.PrimaryScreen?.WorkingArea ?? new Rectangle(0, 0, 1280, 720);
            Location = new Point(vung.Right - Width - 10, vung.Top + 10);
            btnIconThuGon.Visible = true;
            btnIconThuGon.BringToFront();
            TopMost = !System.Diagnostics.Debugger.IsAttached;
        }

        private void btnThuGon_Click(object? sender, EventArgs e)
        {
            HienThiIconThuGon();
        }

        private void btnIconThuGon_Click(object? sender, EventArgs e)
        {
            if (_hasDragged) return; // Nếu vừa kéo thả vị trí thì không mở bung thanh nổi
            HienThiThanhNoiDayDu();
        }

        private void HienThongBao(string noiDung, bool laLoi)
        {
            lblThongBao.Text = noiDung;
            lblThongBao.ForeColor = laLoi ? Color.Red : Color.FromArgb(64, 64, 64);
        }

        private void btnDangNhap_Click(object? sender, EventArgs e)
        {
            string ten = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(matKhau))
            {
                HienThongBao("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", true);
                return;
            }

            btnDangNhap.Enabled = false;
            try
            {
                ClientDangNhapResultDTO kq = ClientBLL.DangNhapHoiVien(_maMay, TenMay, _donGiaGio, ten, matKhau, SoDuToiThieu);
                if (!kq.ThanhCong)
                {
                    HienThongBao(kq.ThongBao, true);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                BatDauPhienTrenGiaoDien(kq.MaPhienChoi, kq.MaHoiVien, kq.SoDu, kq.TenDangNhap, 0);
            }
            catch (Exception ex)
            {
                HienThongBao(ex.Message, true);
            }
            finally
            {
                btnDangNhap.Enabled = true;
            }
        }

        private int _demGiaySyncDb = 0;

        private void tmrDemGio_Tick(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0) return;
            CapNhatSoDu();
            if (CapNhatThoiGian()) HetTien();

            // Cứ mỗi 10 giây đồng bộ số dư và tiền giờ về Database một lần
            _demGiaySyncDb++;
            if (_demGiaySyncDb >= 10)
            {
                _demGiaySyncDb = 0;
                DongBoTienRealTimeVeCSDL();
            }
        }

        private void tmrTruTien_Tick(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0) return;
            CapNhatSoDu();
            DongBoTienRealTimeVeCSDL();
        }

        private void DongBoTienRealTimeVeCSDL()
        {
            if (_maPhienChoi == 0) return;
            try
            {
                TimeSpan daChoi = DateTime.Now - _batDau;
                decimal tienGio = ClientBLL.TinhTienGio(daChoi, _donGiaGio);
                decimal soDuConLai = (_maHoiVien > 0) ? ClientBLL.TinhSoDuHienTai(_soDuBanDau, _donGiaGio, daChoi) : 0m;
                ClientBLL.CapNhatTienRealTime(_maPhienChoi, tienGio, _maHoiVien, soDuConLai);
            }
            catch { }
        }

        private bool CapNhatThoiGian()
        {
            TimeSpan daChoi = DateTime.Now - _batDau;
            lblDaChoi.Text = "Đã chơi: " + ClientBLL.DinhDangThoiGian(daChoi);

            if (_maHoiVien == 0)
            {
                lblThoiGianConLai.Text = "Chơi tự do";
                return false;
            }

            TimeSpan conLai = ClientBLL.TinhThoiGianConLai(_soDuBanDau, _donGiaGio, daChoi);
            lblThoiGianConLai.Text = "Còn lại: " + ClientBLL.DinhDangThoiGian(conLai);
            lblThoiGianConLai.ForeColor = conLai.TotalMinutes <= PhutCanhBao ? Color.Red : Color.Black;
            return conLai <= TimeSpan.Zero;
        }

        private void CapNhatSoDu()
        {
            if (_maHoiVien == 0)
            {
                lblSoDu.Text = "Thanh toán tại quầy";
                return;
            }
            TimeSpan daChoi = DateTime.Now - _batDau;
            decimal soDu = ClientBLL.TinhSoDuHienTai(_soDuBanDau, _donGiaGio, daChoi);
            lblSoDu.Text = $"Số dư: {soDu:N0} đ";
            if (soDu <= 0) HetTien();
        }

        private void HetTien()
        {
            if (_maPhienChoi == 0) return;
            long maPhien = _maPhienChoi;
            tmrDemGio.Stop();
            tmrTruTien.Stop();
            try
            {
                ClientBLL.KetThucPhien(maPhien, _maMay, TenMay, lblTenHoiVien.Text, "Hết tiền tự động khóa máy");
            }
            catch { }
            KhoaMay("Hết tiền – máy đã tự khóa. Vui lòng nạp thêm tại quầy.", true);
        }

        private void tmrDongBo_Tick(object? sender, EventArgs e)
        {
            try
            {
                ClientDongBoDTO dongBo = ClientBLL.DongBoTrangThai(_maMay);

                if (_maPhienChoi != 0)
                {
                    if (dongBo.MaPhienChoi == 0 || dongBo.MaPhienChoi != _maPhienChoi)
                        KhoaMay("Phiên chơi đã được kết thúc tại quầy.", false);
                    return;
                }

                if (dongBo.MaPhienChoi > 0)
                {
                    BatDauPhienTrenGiaoDien(dongBo.MaPhienChoi, dongBo.MaHoiVien, dongBo.SoDu, dongBo.TenDangNhap, dongBo.GiayDaChoi);
                    return;
                }

                if (dongBo.TrangThaiMay == "Locked" || dongBo.TrangThaiMay == "Đang khóa")
                {
                    HienThongBao("Máy đang bị quầy khóa.", true);
                }
            }
            catch
            {
            }
        }

        private void btnGoiMon_Click(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0) return;
            using (var frm = new frmGoiMonClient(_maMay, TenMay, _maPhienChoi))
            {
                _frmGoiMon = frm;
                frm.ShowDialog(this);
                _frmGoiMon = null;
            }
        }

        private void btnTraMay_Click(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0) return;
            TimeSpan daChoi = DateTime.Now - _batDau;
            decimal tienGio = ClientBLL.TinhTienGio(daChoi, _donGiaGio);
            string noiDung = $"Bạn muốn trả máy?\n\nThời gian chơi: {ClientBLL.DinhDangThoiGian(daChoi)}\nTiền giờ tạm tính: {tienGio:N0} đ"
                           + (_maHoiVien == 0 ? "\n(Khách vãng lai – thanh toán tại quầy)" : "");
            if (MessageBox.Show(this, noiDung, "Trả máy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                tmrDemGio.Stop();
                tmrTruTien.Stop();
                ClientBLL.KetThucPhien(_maPhienChoi, _maMay, TenMay, lblTenHoiVien.Text, "Hội viên bấm trả máy tại máy trạm");
                KhoaMay("Cảm ơn bạn! Phiên chơi đã kết thúc.", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể kết thúc phiên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tmrDemGio.Start();
                tmrTruTien.Start();
            }
        }
    }
}

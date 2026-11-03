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
        private bool _isTamKhoa = false;

        // ─── Yêu cầu nạp tiền đang chờ Server duyệt ────────────────────────────
        private int _maYeuCauNapDangCho = 0;   // 0 = không có yêu cầu đang chờ

        public frmClient()
        {
            InitializeComponent();

            btnIconThuGon.MouseDown += BtnIconThuGon_MouseDown;
            btnIconThuGon.MouseMove += BtnIconThuGon_MouseMove;
            btnIconThuGon.MouseUp += BtnIconThuGon_MouseUp;

            btnMenhGia10k.Click  += (s, e) => txtSoTienNapClient.Text = "10000";
            btnMenhGia20k.Click  += (s, e) => txtSoTienNapClient.Text = "20000";
            btnMenhGia50k.Click  += (s, e) => txtSoTienNapClient.Text = "50000";
            btnMenhGia100k.Click += (s, e) => txtSoTienNapClient.Text = "100000";
            btnMenhGia200k.Click += (s, e) => txtSoTienNapClient.Text = "200000";
            btnMenhGia500k.Click += (s, e) => txtSoTienNapClient.Text = "500000";
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

        private void frmClient_Resize(object? sender, EventArgs e)
        {
            CanGiuaPanelDangNhap();
            CanGiuaPanelTamKhoa();
        }

        private void CanGiuaPanelTamKhoa()
        {
            if (!pnlTamKhoa.Visible) return;
            pnlTamKhoaContent.Left = Math.Max(0, (pnlTamKhoa.ClientSize.Width - pnlTamKhoaContent.Width) / 2);
            pnlTamKhoaContent.Top  = Math.Max(0, (pnlTamKhoa.ClientSize.Height - pnlTamKhoaContent.Height) / 2);
            pnlTamKhoaContent.BringToFront();
        }

        private void BatTamKhoaMay()
        {
            if (_isTamKhoa) return;
            _isTamKhoa = true;
            _frmGoiMon?.DongKhongHoi();
            pnlNapTienClient.Visible = false;
            pnlThanhNoi.Visible      = false;
            btnIconThuGon.Visible    = false;
            WindowState = FormWindowState.Maximized;
            TopMost     = !System.Diagnostics.Debugger.IsAttached;
            pnlTamKhoa.Visible = true;
            pnlTamKhoa.BringToFront();
            CanGiuaPanelTamKhoa();
            tmrDemGio.Stop();
            tmrTruTien.Stop();
            Activate();
        }

        private void TatTamKhoaMay()
        {
            if (!_isTamKhoa) return;
            _isTamKhoa = false;
            pnlTamKhoa.Visible = false;
            HienThiThanhNoiDayDu();
            tmrDemGio.Start();
            tmrTruTien.Start();
        }

        private void btnNapTienClient_Click(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0) return;
            if (_maHoiVien <= 0)
            {
                MessageBox.Show(this, "Khách vãng lai vui lòng thanh toán và nạp tiền tại quầy thu ngân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Nếu đang có yêu cầu chưa được xử lý thì không cho gửi thêm
            if (_maYeuCauNapDangCho > 0)
            {
                MessageBox.Show(this, "Bạn đã có yêu cầu nạp tiền đang chờ quầy xác nhận.\nVui lòng chờ nhân viên xử lý!", "Đang chờ xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan daChoi = DateTime.Now - _batDau;
            decimal soDuHienTai = ClientBLL.TinhSoDuHienTai(_soDuBanDau, _donGiaGio, daChoi);
            lblNapTienSoDu.Text = $"Số dư hiện tại: {soDuHienTai:N0} đ";
            txtSoTienNapClient.Text = "50000";
            if (cboHinhThucNap.Items.Count > 0 && cboHinhThucNap.SelectedIndex < 0)
                cboHinhThucNap.SelectedIndex = 0;

            pnlThanhNoi.Visible      = false;
            btnIconThuGon.Visible    = false;
            WindowState              = FormWindowState.Normal;
            MinimumSize              = Size.Empty;
            pnlNapTienClient.Anchor  = AnchorStyles.Top | AnchorStyles.Left;
            pnlNapTienClient.Location = Point.Empty;
            ClientSize = pnlNapTienClient.Size;
            Rectangle vung = Screen.PrimaryScreen?.WorkingArea ?? new Rectangle(0, 0, 1280, 720);
            Location = new Point(vung.Right - Width - 10, vung.Top + 10);
            pnlNapTienClient.Visible = true;
            pnlNapTienClient.BringToFront();
            TopMost = !System.Diagnostics.Debugger.IsAttached;
        }

        private void btnDongNapTienClient_Click(object? sender, EventArgs e)
        {
            pnlNapTienClient.Visible = false;
            HienThiThanhNoiDayDu();
        }

        /// <summary>
        /// Client bấm "Xác nhận nạp" → ghi yêu cầu vào DB, chờ Server duyệt.
        /// Không nạp tiền ngay lập tức.
        /// </summary>
        private void btnXacNhanNapClient_Click(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0 || _maHoiVien <= 0) return;

            string raw = txtSoTienNapClient.Text.Replace(",", "").Replace(".", "").Trim();
            if (!decimal.TryParse(raw, out decimal soTien) || soTien <= 0)
            {
                MessageBox.Show(this, "Số tiền nạp không hợp lệ (phải là số tiền dương)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (soTien % 1000 != 0)
            {
                MessageBox.Show(this, "Số tiền nạp phải là bội số của 1.000 đ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnXacNhanNapClient.Enabled = false;
            try
            {
                string hinhThuc = cboHinhThucNap.SelectedItem?.ToString() ?? "Tiền mặt tại quầy";

                // Gửi yêu cầu lên DB, chờ Server xác nhận
                int maYeuCau = GiaoDichBLL.GuiYeuCauNapTien(
                    _maHoiVien, _maMay, TenMay, soTien, hinhThuc);

                if (maYeuCau <= 0)
                {
                    MessageBox.Show(this, "Không thể gửi yêu cầu nạp tiền. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _maYeuCauNapDangCho = maYeuCau;

                MessageBox.Show(this,
                    $"Yêu cầu nạp {soTien:N0} đ đã được gửi đến quầy.\n" +
                    $"Hình thức: {hinhThuc}\n\n" +
                    "Vui lòng chờ nhân viên xác nhận thanh toán.\n" +
                    "Tiền sẽ được cộng tự động sau khi quầy đồng ý.",
                    "Đã gửi yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                pnlNapTienClient.Visible = false;
                HienThiThanhNoiDayDu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Lỗi gửi yêu cầu nạp tiền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnXacNhanNapClient.Enabled = true;
            }
        }

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
            pnlDangNhap.Top  = Math.Max(0, (pnlKhoa.ClientSize.Height - pnlDangNhap.Height) / 2);
            pnlDangNhap.BringToFront();
        }

        private void KhoaMay(string thongBao, bool laLoi)
        {
            tmrDemGio.Stop();
            tmrTruTien.Stop();
            _maPhienChoi = 0;
            _maHoiVien   = 0;
            _maYeuCauNapDangCho = 0;   // reset yêu cầu
            _frmGoiMon?.DongKhongHoi();

            pnlThanhNoi.Visible   = false;
            btnIconThuGon.Visible = false;
            WindowState = FormWindowState.Maximized;
            TopMost     = !System.Diagnostics.Debugger.IsAttached;
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
            _maPhienChoi  = maPhien;
            _maHoiVien    = maHoiVien;
            _soDuBanDau   = soDu;
            _batDau       = DateTime.Now.AddSeconds(-giayDaChoi);

            lblTenHoiVien.Text     = maHoiVien > 0 ? "Hội viên: " + tenDangNhap : "Khách vãng lai";
            lblDonGiaGio.Text      = $"Giá giờ: {_donGiaGio:N0} đ/giờ";
            lblThoiGianConLai.ForeColor = Color.Black;
            CapNhatThoiGian();
            CapNhatSoDu();

            pnlKhoa.Visible       = false;
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
            pnlThanhNoi.Anchor   = AnchorStyles.Top | AnchorStyles.Left;
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
            pnlThanhNoi.Visible   = false;
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
            if (_hasDragged) return;
            HienThiThanhNoiDayDu();
        }

        private void HienThongBao(string noiDung, bool laLoi)
        {
            lblThongBao.Text     = noiDung;
            lblThongBao.ForeColor = laLoi ? Color.Red : Color.FromArgb(64, 64, 64);
        }

        private void btnDangNhap_Click(object? sender, EventArgs e)
        {
            string ten     = txtTenDangNhap.Text.Trim();
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
                TimeSpan daChoi  = DateTime.Now - _batDau;
                decimal tienGio  = ClientBLL.TinhTienGio(daChoi, _donGiaGio);
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
            lblThoiGianConLai.Text      = "Còn lại: " + ClientBLL.DinhDangThoiGian(conLai);
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
            decimal soDu    = ClientBLL.TinhSoDuHienTai(_soDuBanDau, _donGiaGio, daChoi);
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
                    // Phiên đã bị đóng từ quầy
                    if (dongBo.MaPhienChoi == 0 || dongBo.MaPhienChoi != _maPhienChoi)
                    {
                        if (_isTamKhoa) TatTamKhoaMay();
                        pnlNapTienClient.Visible = false;
                        KhoaMay("Phiên chơi đã được kết thúc tại quầy.", false);
                        return;
                    }

                    // Khóa / mở khóa từ Server
                    if (dongBo.TrangThaiMay == "Đang khóa" || dongBo.TrangThaiMay == "Locked")
                    {
                        BatTamKhoaMay();
                        return;
                    }
                    else if (_isTamKhoa && (dongBo.TrangThaiMay == "Đang dùng" || dongBo.TrangThaiMay == "Active"))
                    {
                        TatTamKhoaMay();
                    }

                    // ── Poll kết quả yêu cầu nạp tiền ──────────────────────────
                    if (_maYeuCauNapDangCho > 0)
                    {
                        try
                        {
                            string trangThai = GiaoDichBLL.KiemTraTrangThaiYeuCau(_maYeuCauNapDangCho);

                            if (trangThai == "DaDuyet")
                            {
                                // Server đã nạp tiền → reload số dư từ đồng bộ
                                _maYeuCauNapDangCho = 0;
                                if (_maHoiVien > 0 && dongBo.SoDu > _soDuBanDau)
                                {
                                    decimal napThem = dongBo.SoDu - _soDuBanDau;
                                    _soDuBanDau = dongBo.SoDu;
                                    CapNhatSoDu();
                                    CapNhatThoiGian();
                                    try { System.Media.SystemSounds.Asterisk.Play(); } catch { }
                                    MessageBox.Show(this,
                                        $"Quầy đã xác nhận!\nTài khoản được nạp thêm {napThem:N0} đ.\nSố dư mới: {dongBo.SoDu:N0} đ",
                                        "Nạp Tiền Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    // Trường hợp số dư chưa kịp sync nhưng trạng thái đã DaDuyet
                                    _soDuBanDau = dongBo.SoDu;
                                    CapNhatSoDu();
                                    CapNhatThoiGian();
                                    try { System.Media.SystemSounds.Asterisk.Play(); } catch { }
                                    MessageBox.Show(this,
                                        "Quầy đã xác nhận nạp tiền!\nSố dư đã được cập nhật.",
                                        "Nạp Tiền Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (trangThai == "TuChoi")
                            {
                                _maYeuCauNapDangCho = 0;
                                try { System.Media.SystemSounds.Exclamation.Play(); } catch { }
                                MessageBox.Show(this,
                                    "Quầy đã từ chối yêu cầu nạp tiền của bạn.\nVui lòng liên hệ nhân viên để biết thêm chi tiết.",
                                    "Yêu Cầu Bị Từ Chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            // "ChoDuyet" → tiếp tục chờ, không làm gì
                        }
                        catch { /* im lặng nếu poll thất bại */ }
                    }

                    // Quầy nạp tiền trực tiếp (nạp từ Server không qua yêu cầu)
                    if (_maYeuCauNapDangCho == 0 && _maHoiVien > 0 && dongBo.SoDu > _soDuBanDau)
                    {
                        decimal napThem = dongBo.SoDu - _soDuBanDau;
                        _soDuBanDau = dongBo.SoDu;
                        CapNhatSoDu();
                        CapNhatThoiGian();
                        try { System.Media.SystemSounds.Asterisk.Play(); } catch { }
                        MessageBox.Show(this, $"Tài khoản của bạn vừa được nạp thêm {napThem:N0} đ từ quầy!\nSố dư mới: {dongBo.SoDu:N0} đ", "Nạp Tiền Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    return;
                }

                // Khi máy chưa có phiên
                if (dongBo.MaPhienChoi > 0)
                {
                    BatDauPhienTrenGiaoDien(dongBo.MaPhienChoi, dongBo.MaHoiVien, dongBo.SoDu, dongBo.TenDangNhap, dongBo.GiayDaChoi);
                    return;
                }

                if (dongBo.TrangThaiMay == "Locked" || dongBo.TrangThaiMay == "Đang khóa")
                {
                    txtTenDangNhap.Enabled = false;
                    txtMatKhau.Enabled     = false;
                    btnDangNhap.Enabled    = false;
                    HienThongBao("Máy đang bị quầy khóa. Vui lòng liên hệ nhân viên!", true);
                }
                else
                {
                    txtTenDangNhap.Enabled = true;
                    txtMatKhau.Enabled     = true;
                    btnDangNhap.Enabled    = true;
                    if (lblThongBao.Text == "Máy đang bị quầy khóa. Vui lòng liên hệ nhân viên!")
                        HienThongBao("Máy đang khóa – vui lòng đăng nhập", false);
                }
            }
            catch { }
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
            string noiDung  = $"Bạn muốn trả máy?\n\nThời gian chơi: {ClientBLL.DinhDangThoiGian(daChoi)}\nTiền giờ tạm tính: {tienGio:N0} đ"
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

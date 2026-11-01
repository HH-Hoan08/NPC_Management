using System.Data;
using System.Data.SqlClient;

namespace NPC.Client
{
    /// <summary>
    /// frmClient - Giao diện máy trạm:
    ///   1) Panel khóa toàn màn hình (pnlKhoa) + đăng nhập hội viên.
    ///   2) Sau khi đăng nhập: form thu nhỏ thành thanh nổi (pnlThanhNoi).
    ///   3) Timer đếm ngược thời gian còn lại (mỗi 1 giây) và trừ tiền tự động theo chu kỳ (mỗi 60 giây).
    /// Lưu ý: tiền giờ được ghi thật vào HoiVien.SoDu bởi sp_KetThucPhienChoi khi kết thúc phiên;
    /// mỗi chu kỳ form chỉ tính lại số dư hiển thị rồi tự khóa máy khi hết tiền (tránh trừ 2 lần).
    /// </summary>
    public partial class frmClient : Form
    {
        #region Cấu hình (sửa cho đúng máy)
        private const string ChuoiKetNoi = @"Data Source=.;Initial Catalog=NPCData;Integrated Security=True;Connect Timeout=5;";
        private const string TenMay = "MAY-01";           // phải trùng MayTram.TenMay trong CSDL
        private const decimal SoDuToiThieu = 5000m;       // trùng với sp_MoPhienChoi
        private const int PhutCanhBao = 5;                // còn <= 5 phút thì chữ chuyển đỏ
        #endregion

        #region Biến toàn cục
        private int _maMay;
        private decimal _donGiaGio;
        private long _maPhienChoi;        // 0 = máy đang khóa
        private int _maHoiVien;           // 0 = khách vãng lai (quầy mở máy)
        private decimal _soDuBanDau;
        private DateTime _batDau;         // mốc bắt đầu phiên (đồng hồ máy trạm)
        private bool _choPhepThoat;       // false: chặn Alt+F4
        private frmGoiMonClient? _frmGoiMon;
        #endregion

        public frmClient()
        {
            InitializeComponent();
        }

        #region Load / Closing / Phím tắt
        private void frmClient_Load(object? sender, EventArgs e)
        {
            try
            {
                DataTable dt = LayBang(
                    @"SELECT m.MaMay, k.DonGiaGio
                      FROM dbo.MayTram m INNER JOIN dbo.KhuVucPhong k ON m.MaPhong = k.MaPhong
                      WHERE m.TenMay = @Ten", new SqlParameter("@Ten", TenMay));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy máy \"{TenMay}\" trong CSDL.", "Lỗi cấu hình",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ThoatUngDung();
                    return;
                }
                _maMay = Convert.ToInt32(dt.Rows[0]["MaMay"]);
                _donGiaGio = Convert.ToDecimal(dt.Rows[0]["DonGiaGio"]);

                // Máy trạm bật lên: Offline -> Online
                ThucThi("UPDATE dbo.MayTram SET TrangThai = N'Online' WHERE MaMay = @Ma AND TrangThai = N'Offline'",
                        new SqlParameter("@Ma", _maMay));
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
            if (!_choPhepThoat) e.Cancel = true;       // chặn Alt+F4
        }

        private void frmClient_KeyDown(object? sender, KeyEventArgs e)
        {
            // Phím thoát để test: Ctrl + Shift + F12 (XÓA khi nộp bản cuối)
            if (e.Control && e.Shift && e.KeyCode == Keys.F12
                && MessageBox.Show(this, "Thoát chương trình máy trạm?", "Thoát",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (_maPhienChoi == 0)
                        ThucThi("UPDATE dbo.MayTram SET TrangThai = N'Offline' WHERE MaMay = @Ma AND TrangThai = N'Online'",
                                new SqlParameter("@Ma", _maMay));
                }
                catch { }
                ThoatUngDung();
            }
        }

        private void frmClient_Resize(object? sender, EventArgs e) => CanGiuaPanelDangNhap();

        private void ThoatUngDung()
        {
            _choPhepThoat = true;
            tmrDemGio.Stop(); tmrTruTien.Stop(); tmrDongBo.Stop();
            Application.Exit();
        }

        /// <summary>Canh pnlDangNhap vào giữa màn hình khóa.</summary>
        private void CanGiuaPanelDangNhap()
        {
            if (!pnlKhoa.Visible) return;
            pnlDangNhap.Left = Math.Max(0, (pnlKhoa.ClientSize.Width - pnlDangNhap.Width) / 2);
            pnlDangNhap.Top = Math.Max(0, (pnlKhoa.ClientSize.Height - pnlDangNhap.Height) / 2);
        }
        #endregion

        #region Khóa máy / Mở máy
        /// <summary>Khóa toàn màn hình bằng pnlKhoa và hiện thông báo.</summary>
        private void KhoaMay(string thongBao, bool laLoi)
        {
            tmrDemGio.Stop();
            tmrTruTien.Stop();
            _maPhienChoi = 0;
            _maHoiVien = 0;
            _frmGoiMon?.DongKhongHoi();                 // đóng form gọi món nếu đang mở

            pnlThanhNoi.Visible = false;
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

        /// <summary>Bắt đầu phiên: ẩn pnlKhoa, thu form thành thanh nổi góc phải trên, chạy các Timer.</summary>
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

            tmrDemGio.Start();
            tmrTruTien.Start();
        }

        private void HienThongBao(string noiDung, bool laLoi)
        {
            lblThongBao.Text = noiDung;
            lblThongBao.ForeColor = laLoi ? Color.Red : Color.FromArgb(64, 64, 64);
        }
        #endregion

        #region Đăng nhập
        private void btnDangNhap_Click(object? sender, EventArgs e)
        {
            string ten = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            if (ten == "" || matKhau == "")
            {
                HienThongBao("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", true);
                return;
            }

            btnDangNhap.Enabled = false;
            try
            {
                // Mật khẩu trong dữ liệu mẫu là chữ thường; tham số hóa để chống SQL Injection
                DataTable dt = LayBang(
                    "SELECT MaHoiVien, SoDu, TrangThai FROM dbo.HoiVien WHERE TenDangNhap = @Ten AND MatKhau = @Mk",
                    new SqlParameter("@Ten", ten), new SqlParameter("@Mk", matKhau));
                if (dt.Rows.Count == 0)
                {
                    HienThongBao("Sai tên đăng nhập hoặc mật khẩu.", true);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                int maHoiVien = Convert.ToInt32(dt.Rows[0]["MaHoiVien"]);
                decimal soDu = Convert.ToDecimal(dt.Rows[0]["SoDu"]);
                string trangThai = dt.Rows[0]["TrangThai"].ToString() ?? "";

                if (trangThai == "Bị khóa") { HienThongBao("Tài khoản đang bị khóa. Vui lòng liên hệ quầy.", true); return; }
                if (trangThai == "Đang chơi") { HienThongBao("Tài khoản đang được sử dụng ở máy khác.", true); return; }
                if (soDu < SoDuToiThieu)
                {
                    HienThongBao($"Số dư không đủ (tối thiểu {SoDuToiThieu:N0} đ). Vui lòng nạp thêm tại quầy.", true);
                    return;
                }
                if (LayTrangThaiMay() == "Locked")
                {
                    HienThongBao("Máy đang bị quầy khóa. Vui lòng liên hệ thu ngân.", true);
                    return;
                }

                long maPhien = MoPhienTrongCsdl(maHoiVien);
                BatDauPhienTrenGiaoDien(maPhien, maHoiVien, soDu, ten, 0);
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
        #endregion

        #region Timer đếm ngược + trừ tiền theo chu kỳ
        private void tmrDemGio_Tick(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0) return;
            if (CapNhatThoiGian()) HetTien();
        }

        private void tmrTruTien_Tick(object? sender, EventArgs e)
        {
            if (_maPhienChoi == 0) return;
            CapNhatSoDu();
        }

        private bool CapNhatThoiGian()
        {
            TimeSpan daChoi = DateTime.Now - _batDau;
            lblDaChoi.Text = "Đã chơi: " + DinhDang(daChoi);

            if (_maHoiVien == 0)                     
            {
                lblThoiGianConLai.Text = "Chơi tự do";
                return false;
            }

            double tongGiay = _donGiaGio > 0 ? (double)(_soDuBanDau / _donGiaGio) * 3600.0 : 0;
            TimeSpan conLai = TimeSpan.FromSeconds(Math.Max(0, tongGiay - daChoi.TotalSeconds));
            lblThoiGianConLai.Text = "Còn lại: " + DinhDang(conLai);
            lblThoiGianConLai.ForeColor = conLai.TotalMinutes <= PhutCanhBao ? Color.Red : Color.Black;
            return conLai <= TimeSpan.Zero;
        }

        private void CapNhatSoDu()
        {
            if (_maHoiVien == 0) { lblSoDu.Text = "Thanh toán tại quầy"; return; }
            decimal soDu = Math.Max(0m, _soDuBanDau - TinhTienGio(DateTime.Now - _batDau));
            lblSoDu.Text = $"Số dư: {soDu:N0} đ";
            if (soDu <= 0) HetTien();
        }

        private decimal TinhTienGio(TimeSpan daChoi)
        {
            int phut = Math.Max(1, (int)daChoi.TotalMinutes);
            return Math.Round(phut / 60m * _donGiaGio, 0, MidpointRounding.AwayFromZero);
        }

        private static string DinhDang(TimeSpan t) => $"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}";

        private void HetTien()
        {
            if (_maPhienChoi == 0) return;
            long maPhien = _maPhienChoi;
            tmrDemGio.Stop();
            tmrTruTien.Stop();
            try { KetThucPhienTrongCsdl(maPhien); } catch { }
            KhoaMay("Hết tiền – máy đã tự khóa. Vui lòng nạp thêm tại quầy.", true);
        }

        private void tmrDongBo_Tick(object? sender, EventArgs e)
        {
            try
            {
                DataRow? phien = LayPhienDangChay();

                if (_maPhienChoi != 0)
                {
                    if (phien == null || Convert.ToInt64(phien["MaPhienChoi"]) != _maPhienChoi)
                        KhoaMay("Phiên chơi đã được kết thúc tại quầy.", false);
                    return;
                }

                if (phien != null)
                {
                    int maHv = phien["MaHoiVien"] == DBNull.Value ? 0 : Convert.ToInt32(phien["MaHoiVien"]);
                    decimal soDu = phien["SoDu"] == DBNull.Value ? 0m : Convert.ToDecimal(phien["SoDu"]);
                    string ten = phien["TenDangNhap"] == DBNull.Value ? "" : phien["TenDangNhap"].ToString() ?? "";
                    BatDauPhienTrenGiaoDien(Convert.ToInt64(phien["MaPhienChoi"]), maHv, soDu, ten, Convert.ToInt32(phien["Giay"]));
                    return;
                }

                if (LayTrangThaiMay() == "Locked") HienThongBao("Máy đang bị quầy khóa.", true);
            }
            catch
            {

            }
        }
        #endregion

        #region Gọi món / Trả máy
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
            string noiDung = $"Bạn muốn trả máy?\n\nThời gian chơi: {DinhDang(daChoi)}\nTiền giờ tạm tính: {TinhTienGio(daChoi):N0} đ"
                           + (_maHoiVien == 0 ? "\n(Khách vãng lai – thanh toán tại quầy)" : "");
            if (MessageBox.Show(this, noiDung, "Trả máy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                tmrDemGio.Stop();
                tmrTruTien.Stop();
                KetThucPhienTrongCsdl(_maPhienChoi);
                KhoaMay("Cảm ơn bạn! Phiên chơi đã kết thúc.", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể kết thúc phiên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tmrDemGio.Start();
                tmrTruTien.Start();
            }
        }
        #endregion

        #region Truy vấn CSDL (dùng SqlParameter, mỗi lần mở/đóng kết nối trong using)
        private static DataTable LayBang(string sql, params SqlParameter[] ps)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(ChuoiKetNoi))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(ps);
                conn.Open();
                using (var rd = cmd.ExecuteReader()) dt.Load(rd);
            }
            return dt;
        }

        private static int ThucThi(string sql, params SqlParameter[] ps)
        {
            using (var conn = new SqlConnection(ChuoiKetNoi))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        private string LayTrangThaiMay()
        {
            DataTable dt = LayBang("SELECT TrangThai FROM dbo.MayTram WHERE MaMay = @Ma", new SqlParameter("@Ma", _maMay));
            return dt.Rows.Count == 0 ? "" : dt.Rows[0][0].ToString() ?? "";
        }

        private DataRow? LayPhienDangChay()
        {
            DataTable dt = LayBang(
                @"SELECT TOP 1 p.MaPhienChoi, p.MaHoiVien, h.TenDangNhap, h.SoDu,
                         DATEDIFF(SECOND, p.ThoiGianBatDau, GETDATE()) AS Giay
                  FROM dbo.PhienChoi p LEFT JOIN dbo.HoiVien h ON p.MaHoiVien = h.MaHoiVien
                  WHERE p.MaMay = @Ma AND p.TrangThaiPhien = N'Active'
                  ORDER BY p.MaPhienChoi DESC", new SqlParameter("@Ma", _maMay));
            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        private long MoPhienTrongCsdl(int maHoiVien)
        {
            using (var conn = new SqlConnection(ChuoiKetNoi))
            using (var cmd = new SqlCommand("dbo.sp_MoPhienChoi", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@MaMay", _maMay);
                cmd.Parameters.AddWithValue("@MaHoiVien", maHoiVien > 0 ? (object)maHoiVien : DBNull.Value);
                var pOut = new SqlParameter("@MaPhienChoiMoi", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(pOut);
                conn.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt64(pOut.Value);
            }
        }

        private static void KetThucPhienTrongCsdl(long maPhien)
        {
            using (var conn = new SqlConnection(ChuoiKetNoi))
            using (var cmd = new SqlCommand("dbo.sp_KetThucPhienChoi", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@MaPhienChoi", maPhien);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
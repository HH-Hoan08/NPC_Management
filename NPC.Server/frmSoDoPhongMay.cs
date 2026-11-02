using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NPC.BLL;
using NPC.DTO;

namespace NPC.Server
{
    public partial class frmSoDoPhongMay : Form
    {
        private System.Windows.Forms.Timer _timerDemGio;
        private int _demGiayQuetDb = 0;
        private Dictionary<int, long> _danhSachPhienChoiCu = new Dictionary<int, long>();
        private bool _lanDauLoad = true;

        public frmSoDoPhongMay()
        {
            InitializeComponent();
            KhoiTaoTimer();
        }

        private void KhoiTaoTimer()
        {
            _timerDemGio = new System.Windows.Forms.Timer();
            _timerDemGio.Interval = 1000;
            _timerDemGio.Tick += (s, e) =>
            {
                // Cập nhật tăng giây hiển thị trên các máy đang dùng
                foreach (Control ctrl in flpDanhSachMay.Controls)
                {
                    if (ctrl is ucMayTram uc && uc.Data != null && uc.Data.TrangThai == "Đang dùng")
                    {
                        uc.BindingData(uc.Data);
                    }
                }

                // Cứ mỗi 2 giây quét Database 1 lần để phát hiện Client vừa mở máy / trả máy
                _demGiayQuetDb++;
                if (_demGiayQuetDb >= 2)
                {
                    _demGiayQuetDb = 0;
                    DongBoMayTramTuCSDL();
                }
            };
        }

        private void frmSoDoPhongMay_Load(object sender, EventArgs e)
        {
            LoadComboboxKhuVuc();
            LoadComboboxTrangThai();
            LoadDanhSachMay();
            _timerDemGio.Start();
        }

        private void LoadComboboxKhuVuc()
        {
            try
            {
                List<KhuVucPhongDTO> dsKhuVuc = MayTramBLL.LayDanhSachKhuVuc();
                dsKhuVuc.Insert(0, new KhuVucPhongDTO { MaPhong = 0, TenPhong = "--- Tất cả khu vực ---" });

                cboKhuVuc.DisplayMember = "TenPhong";
                cboKhuVuc.ValueMember = "MaPhong";
                cboKhuVuc.DataSource = dsKhuVuc;
                cboKhuVuc.SelectedIndex = 0;
                cboKhuVuc.SelectedIndexChanged += (s, e) => LoadDanhSachMay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khu vực phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboboxTrangThai()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                "Tất cả",
                "Sẵn sàng",
                "Đang dùng",
                "Đang khóa",
                "Bảo trì",
                "Tắt máy"
            });
            cboTrangThai.SelectedIndex = 0;
            cboTrangThai.SelectedIndexChanged += (s, e) => LoadDanhSachMay();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachMay();
        }

        public void LoadDanhSachMay()
        {
            try
            {
                int? maPhong = null;
                if (cboKhuVuc.SelectedValue != null && int.TryParse(cboKhuVuc.SelectedValue.ToString(), out int p) && p > 0)
                {
                    maPhong = p;
                }

                string? trangThai = cboTrangThai.SelectedItem?.ToString();
                if (trangThai == "Tất cả") trangThai = null;

                List<MayTramDTO> listMay = MayTramBLL.LayDanhSachMayTram(maPhong, trangThai);

                CapNhatThongKe(listMay);

                flpDanhSachMay.SuspendLayout();
                flpDanhSachMay.Controls.Clear();

                foreach (var may in listMay)
                {
                    ucMayTram uc = new ucMayTram();
                    uc.Size = new Size(160, 185);
                    uc.BindingData(may);
                    uc.ActionTriggered += Uc_ActionTriggered;

                    flpDanhSachMay.Controls.Add(uc);

                    if (may.MaPhienChoi.HasValue)
                        _danhSachPhienChoiCu[may.MaMay] = may.MaPhienChoi.Value;
                    else
                        _danhSachPhienChoiCu[may.MaMay] = 0;
                }

                flpDanhSachMay.ResumeLayout();
                _lanDauLoad = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách máy trạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DongBoMayTramTuCSDL()
        {
            try
            {
                int? maPhong = null;
                if (cboKhuVuc.SelectedValue != null && int.TryParse(cboKhuVuc.SelectedValue.ToString(), out int p) && p > 0)
                {
                    maPhong = p;
                }

                string? trangThai = cboTrangThai.SelectedItem?.ToString();
                if (trangThai == "Tất cả") trangThai = null;

                List<MayTramDTO> listMayMoi = MayTramBLL.LayDanhSachMayTram(maPhong, trangThai);
                CapNhatThongKe(listMayMoi);

                Dictionary<int, MayTramDTO> dictMoi = listMayMoi.ToDictionary(m => m.MaMay);

                foreach (Control ctrl in flpDanhSachMay.Controls)
                {
                    if (ctrl is ucMayTram uc && uc.Data != null && dictMoi.ContainsKey(uc.Data.MaMay))
                    {
                        MayTramDTO dataMoi = dictMoi[uc.Data.MaMay];

                        long phienCu = _danhSachPhienChoiCu.ContainsKey(dataMoi.MaMay) ? _danhSachPhienChoiCu[dataMoi.MaMay] : 0;
                        long phienMoi = dataMoi.MaPhienChoi ?? 0;

                        // Kiểm tra nếu vừa có phiên chơi mới mở từ Client
                        if (!_lanDauLoad && phienCu == 0 && phienMoi > 0 && dataMoi.TrangThai == "Đang dùng")
                        {
                            string khach = string.IsNullOrEmpty(dataMoi.TenHoiVien) ? "Khách vãng lai" : dataMoi.TenHoiVien;
                            ThongBaoMoMay(dataMoi.TenMay, khach);
                        }

                        _danhSachPhienChoiCu[dataMoi.MaMay] = phienMoi;
                        uc.BindingData(dataMoi);
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi kết nối tạm thời để không gián đoạn giao diện
            }
        }

        private void ThongBaoMoMay(string tenMay, string tenNguoiDung)
        {
            // Hiển thị thông báo hoặc phát âm thanh báo máy đang sử dụng
            try
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
            catch { }

            MessageBox.Show($"Máy [{tenMay}] vừa đăng nhập sử dụng thành công!\nNgười dùng: {tenNguoiDung}",
                "Thông Báo Máy Bắt Đầu Sử Dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CapNhatThongKe(List<MayTramDTO> list)
        {
            int tong = list.Count;
            int online = list.Count(m => m.TrangThai == "Đang dùng");
            int trong = list.Count(m => m.TrangThai == "Sẵn sàng" || m.TrangThai == "Tắt máy");
            int baotri = list.Count(m => m.TrangThai == "Bảo trì");

            lblTongSoMay.Text = $"Tổng: {tong}";
            lblDangOnline.Text = $"Online: {online}";
            lblMayTrong.Text = $"Trống: {trong}";
            lblBaoTri.Text = $"Bảo trì: {baotri}";
        }

        private void Uc_ActionTriggered(object sender, MayTramEventArgs e)
        {
            var may = e.MayTram;

            switch (e.Action)
            {
                case MayTramAction.MoMayVangLai:
                    XuLyMoMay(may);
                    break;

                case MayTramAction.MoMayHoiVien:
                    XuLyMoMayHoiVien(may);
                    break;

                case MayTramAction.KhoaMay:
                    XuLyKhoaMay(may);
                    break;

                case MayTramAction.ThanhToan:
                    XuLyThanhToan(may);
                    break;

                case MayTramAction.BaoTri:
                    XuLyBaoTri(may);
                    break;

                case MayTramAction.NapTien:
                    MessageBox.Show($"Chức năng nạp tiền cho hội viên: {may.TenHoiVien}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case MayTramAction.ChuyenMay:
                    MessageBox.Show($"Chức năng chuyển máy cho {may.TenMay}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void XuLyMoMayHoiVien(MayTramDTO may)
        {
            MessageBox.Show($"Chức năng mở máy cho Hội viên trên {may.TenMay} (có thể chọn từ danh sách hội viên)!", "Mở máy hội viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void XuLyMoMay(MayTramDTO may)
        {
            DialogResult dr = MessageBox.Show($"Bạn có muốn mở máy {may.TenMay} cho Khách Vãng Lai không?", "Xác nhận mở máy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    string nguoiThaoTac = !string.IsNullOrEmpty(frmDangNhapServer.CurrentUser) ? frmDangNhapServer.CurrentUser : "Thu ngân";
                    long maPhien = MayTramBLL.MoMayVangLai(may.MaMay, may.TenMay, nguoiThaoTac);

                    MessageBox.Show($"Mở máy {may.TenMay} thành công! Mã phiên: {maPhien}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachMay();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mở máy thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XuLyKhoaMay(MayTramDTO may)
        {
            try
            {
                string nguoiThaoTac = !string.IsNullOrEmpty(frmDangNhapServer.CurrentUser) ? frmDangNhapServer.CurrentUser : "Thu ngân";
                MayTramBLL.ChuyenDoiKhoaMay(may.MaMay, may.TenMay, may.TrangThai, nguoiThaoTac);
                LoadDanhSachMay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thao tác thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLyThanhToan(MayTramDTO may)
        {
            if (!may.MaPhienChoi.HasValue)
            {
                MessageBox.Show("Máy này hiện không có phiên chơi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show($"Xác nhận thanh toán và trả máy cho {may.TenMay}?", "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    KetQuaTraMayDTO kq = MayTramBLL.ThanhToanVaTraMay(may.MaPhienChoi.Value, may.TenMay, 1, "Tiền mặt");
                    MessageBox.Show($"Đã kết thúc phiên chơi và thanh toán {may.TenMay} thành công!\nTổng thu: {kq.TongTienThanhToan:N0} VND", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachMay();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thanh toán thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XuLyBaoTri(MayTramDTO may)
        {
            try
            {
                string nguoiThaoTac = !string.IsNullOrEmpty(frmDangNhapServer.CurrentUser) ? frmDangNhapServer.CurrentUser : "Thu ngân";
                MayTramBLL.ChuyenDoiBaoTri(may.MaMay, may.TenMay, may.TrangThai, nguoiThaoTac);
                LoadDanhSachMay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật bảo trì thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

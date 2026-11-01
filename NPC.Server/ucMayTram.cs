using System;
using System.Drawing;
using System.Windows.Forms;
using NPC.DTO;

namespace NPC.Server
{
    public partial class ucMayTram : UserControl
    {
        public MayTramDTO Data { get; private set; }

        public event EventHandler<MayTramEventArgs> ActionTriggered;

        public ucMayTram()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            tsmiMoKhachVangLai.Click += (s, e) => OnAction(MayTramAction.MoMayVangLai);
            tsmiMoHoiVien.Click += (s, e) => OnAction(MayTramAction.MoMayHoiVien);
            tsmiKhoaMay.Click += (s, e) => OnAction(MayTramAction.KhoaMay);
            tsmiNapTien.Click += (s, e) => OnAction(MayTramAction.NapTien);
            tsmiThanhToan.Click += (s, e) => OnAction(MayTramAction.ThanhToan);
            tsmiChuyenMay.Click += (s, e) => OnAction(MayTramAction.ChuyenMay);
            tsmiBaoTri.Click += (s, e) => OnAction(MayTramAction.BaoTri);

            GanContextMenu(this);
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsMayTram.Show(Cursor.Position);
            }
        }

        private void GanContextMenu(Control parent)
        {
            parent.ContextMenuStrip = cmsMayTram;
            parent.MouseDown -= Control_MouseDown;
            parent.MouseDown += Control_MouseDown;
            foreach (Control child in parent.Controls)
            {
                GanContextMenu(child);
            }
        }

        private void OnAction(MayTramAction action)
        {
            if (Data != null)
            {
                ActionTriggered?.Invoke(this, new MayTramEventArgs(Data, action));
            }
        }

        public void BindingData(MayTramDTO dto)
        {
            Data = dto;
            if (Data == null) return;

            lblTenMay.Text = Data.TenMay;
            lblLoaiPhong.Text = string.IsNullOrEmpty(Data.TenPhong) ? "Thường" : Data.TenPhong;

            CapNhatTrangThai();

            bool isDangDung = KiemTraTrangThai(Data.TrangThai, "Đang dùng", "Dang dung", "InUse");

            if (isDangDung)
            {
                lblNguoiDung.Text = string.IsNullOrEmpty(Data.TenHoiVien) ? "Khách vãng lai" : $"HV: {Data.TenHoiVien}";

                if (Data.ThoiGianBatDau.HasValue)
                {
                    TimeSpan duration = DateTime.Now - Data.ThoiGianBatDau.Value;
                    lblThoiGian.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)duration.TotalHours, duration.Minutes, duration.Seconds);
                }
                else
                {
                    lblThoiGian.Text = "00:00:00";
                }

                if (Data.MaHoiVien.HasValue)
                {
                    lblSoDu.Text = $"Dư: {Data.SoDuHoiVien:N0} đ";
                }
                else
                {
                    decimal tienHienTai = 0;
                    if (Data.ThoiGianBatDau.HasValue)
                    {
                        TimeSpan duration = DateTime.Now - Data.ThoiGianBatDau.Value;
                        double totalHours = duration.TotalHours;
                        tienHienTai = Math.Round((decimal)totalHours * Data.DonGiaGio, 0);
                    }
                    else
                    {
                        tienHienTai = Data.TienGioHienTai;
                    }
                    lblSoDu.Text = $"Tiền: {tienHienTai:N0} đ";
                }
            }
            else
            {
                lblNguoiDung.Text = "---";
                lblThoiGian.Text = "--:--:--";
                lblSoDu.Text = $"{Data.DonGiaGio:N0} đ/h";
            }
        }

        public void CapNhatTrangThai()
        {
            if (Data == null) return;

            string tt = Data.TrangThai ?? "";
            lblTrangThai.Text = tt;

            if (KiemTraTrangThai(tt, "Đang dùng", "Dang dung", "InUse"))
            {
                pnlHeader.BackColor = Color.FromArgb(40, 167, 69);     // Xanh lá (Active)
                lblTrangThai.ForeColor = Color.FromArgb(40, 167, 69);
                lblTrangThai.Text = "Đang dùng";
                BorderStyle = BorderStyle.FixedSingle;

                tsmiMoMay.Enabled = false;
                tsmiKhoaMay.Enabled = true;
                tsmiKhoaMay.Text = "Khóa máy";
                tsmiNapTien.Enabled = Data.MaHoiVien.HasValue;
                tsmiThanhToan.Enabled = true;
                tsmiChuyenMay.Enabled = true;
                tsmiBaoTri.Enabled = false;
            }
            else if (KiemTraTrangThai(tt, "Sẵn sàng", "San sang", "Ready", "Online"))
            {
                pnlHeader.BackColor = Color.FromArgb(0, 122, 204);     // Xanh dương (Ready)
                lblTrangThai.ForeColor = Color.FromArgb(0, 122, 204);
                lblTrangThai.Text = "Sẵn sàng";
                BorderStyle = BorderStyle.FixedSingle;

                tsmiMoMay.Enabled = true;
                tsmiKhoaMay.Enabled = false;
                tsmiNapTien.Enabled = false;
                tsmiThanhToan.Enabled = false;
                tsmiChuyenMay.Enabled = false;
                tsmiBaoTri.Enabled = true;
                tsmiBaoTri.Text = "Bật bảo trì";
            }
            else if (KiemTraTrangThai(tt, "Đang khóa", "Dang khoa", "Locked"))
            {
                pnlHeader.BackColor = Color.FromArgb(220, 53, 69);     // Đỏ cam (Locked)
                lblTrangThai.ForeColor = Color.FromArgb(220, 53, 69);
                lblTrangThai.Text = "Đang khóa";
                BorderStyle = BorderStyle.Fixed3D;

                tsmiMoMay.Enabled = false;
                tsmiKhoaMay.Enabled = true;
                tsmiKhoaMay.Text = "Mở khóa máy";
                tsmiNapTien.Enabled = Data.MaHoiVien.HasValue;
                tsmiThanhToan.Enabled = true;
                tsmiChuyenMay.Enabled = false;
                tsmiBaoTri.Enabled = false;
            }
            else if (KiemTraTrangThai(tt, "Bảo trì", "Bao tri", "Maintenance"))
            {
                pnlHeader.BackColor = Color.FromArgb(255, 193, 7);     // Vàng cam (Maintenance)
                lblTrangThai.ForeColor = Color.DarkGoldenrod;
                lblTrangThai.Text = "Bảo trì";
                BorderStyle = BorderStyle.FixedSingle;

                tsmiMoMay.Enabled = false;
                tsmiKhoaMay.Enabled = false;
                tsmiNapTien.Enabled = false;
                tsmiThanhToan.Enabled = false;
                tsmiChuyenMay.Enabled = false;
                tsmiBaoTri.Enabled = true;
                tsmiBaoTri.Text = "Tắt bảo trì";
            }
            else
            {
                pnlHeader.BackColor = Color.FromArgb(108, 117, 125);   // Xám (Offline)
                lblTrangThai.ForeColor = Color.FromArgb(108, 117, 125);
                lblTrangThai.Text = "Tắt máy";
                BorderStyle = BorderStyle.None;

                tsmiMoMay.Enabled = true;
                tsmiKhoaMay.Enabled = false;
                tsmiNapTien.Enabled = false;
                tsmiThanhToan.Enabled = false;
                tsmiChuyenMay.Enabled = false;
                tsmiBaoTri.Enabled = true;
                tsmiBaoTri.Text = "Bật bảo trì";
            }
        }

        private bool KiemTraTrangThai(string nguon, params string[] danhSach)
        {
            if (string.IsNullOrEmpty(nguon)) return false;
            foreach (var s in danhSach)
            {
                if (string.Equals(nguon.Trim(), s.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
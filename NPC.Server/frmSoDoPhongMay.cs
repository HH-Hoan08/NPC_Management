using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NPC.DAL;
using NPC.DTO;

namespace NPC.Server
{
    public partial class frmSoDoPhongMay : Form
    {
        private MayTramDAL _mayTramDAL = new MayTramDAL();
        private System.Windows.Forms.Timer _timerDemGio;

        public frmSoDoPhongMay()
        {
            InitializeComponent();
            KhoiTaoTimer();
        }

        private void KhoiTaoTimer()
        {
            _timerDemGio = new System.Windows.Forms.Timer();
            _timerDemGio.Interval = 1000; // Mỗi giây cập nhật thời gian 1 lần
            _timerDemGio.Tick += (s, e) =>
            {
                foreach (Control ctrl in flpDanhSachMay.Controls)
                {
                    if (ctrl is ucMayTram uc && uc.Data != null && uc.Data.TrangThai == "Đang dùng")
                    {
                        uc.BindingData(uc.Data);
                    }
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
                string query = "SELECT MaPhong, TenPhong FROM dbo.KhuVucPhong";
                DataTable dt = SQLHelper.ExecuteQuery(query);

                DataRow row = dt.NewRow();
                row["MaPhong"] = 0;
                row["TenPhong"] = "--- Tất cả khu vực ---";
                dt.Rows.InsertAt(row, 0);

                cboKhuVuc.DisplayMember = "TenPhong";
                cboKhuVuc.ValueMember = "MaPhong";
                cboKhuVuc.DataSource = dt;
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

                string trangThai = cboTrangThai.SelectedItem?.ToString();
                if (trangThai == "Tất cả") trangThai = null;

                List<MayTramDTO> listMay = _mayTramDAL.GetDanhSachMayTram(maPhong, trangThai);

                // Cập nhật thống kê
                CapNhatThongKe(listMay);

                // Render danh sách máy lên FlowLayoutPanel
                flpDanhSachMay.SuspendLayout();
                flpDanhSachMay.Controls.Clear();

                foreach (var may in listMay)
                {
                    ucMayTram uc = new ucMayTram();
                    uc.Size = new Size(160, 185); // Kích thước gọn gàng hiển thị dạng Grid
                    uc.BindingData(may);
                    uc.ActionTriggered += Uc_ActionTriggered;

                    flpDanhSachMay.Controls.Add(uc);
                }

                flpDanhSachMay.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách máy trạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (_mayTramDAL.MoMayVangLai(may.MaMay, out long maPhien, out string err))
                {
                    MessageBox.Show($"Mở máy {may.TenMay} thành công! Mã phiên: {maPhien}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachMay();
                }
                else
                {
                    MessageBox.Show("Mở máy thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XuLyKhoaMay(MayTramDTO may)
        {
            string ttMoi = (may.TrangThai == "Đang khóa") ? "Đang dùng" : "Đang khóa";
            if (_mayTramDAL.CapNhatTrangThaiMay(may.MaMay, ttMoi, out string err))
            {
                LoadDanhSachMay();
            }
            else
            {
                MessageBox.Show("Thao tác thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (_mayTramDAL.ThanhToanMay(may.MaPhienChoi.Value, out string err))
                {
                    MessageBox.Show($"Đã kết thúc phiên chơi và thanh toán {may.TenMay} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachMay();
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XuLyBaoTri(MayTramDTO may)
        {
            string ttMoi = (may.TrangThai == "Bảo trì") ? "Sẵn sàng" : "Bảo trì";
            if (_mayTramDAL.CapNhatTrangThaiMay(may.MaMay, ttMoi, out string err))
            {
                LoadDanhSachMay();
            }
            else
            {
                MessageBox.Show("Cập nhật bảo trì thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

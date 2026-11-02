using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NPC.BLL;
using NPC.DTO;

namespace NPC.Client
{
    /// <summary>
    /// Giao diện gọi món tại máy trạm client (GUI).
    /// Toàn bộ logic nghiệp vụ, kiểm tra và truy vấn được xử lý qua tầng BLL (GoiMonBLL).
    /// </summary>
    public partial class frmGoiMonClient : Form
    {
        #region Biến toàn cục
        private readonly int _maMay;
        private readonly string _tenMay;
        private readonly long _maPhienChoi;
        private readonly string _tenDangNhap;

        private readonly BindingList<ChiTietDonHangDTO> _gioHang = new BindingList<ChiTietDonHangDTO>();
        private bool _dongYenLang;

        private bool _coMonChon;
        private int _maMonChon;
        private string _tenMonChon = string.Empty;
        private decimal _donGiaChon;
        private int _tonKhoChon;
        #endregion

        #region Khởi tạo
        public frmGoiMonClient() : this(0, "MAY-01", 0, "") { }

        public frmGoiMonClient(int maMay, string tenMay, long maPhienChoi, string tenDangNhap = "")
        {
            InitializeComponent();
            _maMay = maMay;
            _tenMay = string.IsNullOrWhiteSpace(tenMay) ? "MAY-01" : tenMay;
            _maPhienChoi = maPhienChoi;
            _tenDangNhap = tenDangNhap;
        }

        private void frmGoiMonClient_Load(object? sender, EventArgs e)
        {
            lblMayHienTai.Text = "Máy: " + _tenMay;
            KhoiTaoLuoiGioHang();
            try
            {
                NapDanhMuc();
                NapThucDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không tải được thực đơn:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CapNhatTongTien();
        }

        private void frmGoiMonClient_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!_dongYenLang && DialogResult != DialogResult.OK && _gioHang.Count > 0
                && MessageBox.Show(this, "Giỏ hàng còn món chưa đặt. Bạn vẫn muốn đóng?", "Xác nhận",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        public void DongKhongHoi()
        {
            _dongYenLang = true;
            Close();
        }
        #endregion

        #region Nạp danh mục & thực đơn
        private void NapDanhMuc()
        {
            List<DanhMucDTO> ds = GoiMonBLL.LayDanhSachDanhMuc(themTatCa: true);

            cboDanhMuc.SelectedIndexChanged -= cboDanhMuc_SelectedIndexChanged;
            cboDanhMuc.DisplayMember = nameof(DanhMucDTO.TenDanhMuc);
            cboDanhMuc.ValueMember = nameof(DanhMucDTO.MaDanhMuc);
            cboDanhMuc.DataSource = ds;
            cboDanhMuc.SelectedIndex = 0;
            cboDanhMuc.SelectedIndexChanged += cboDanhMuc_SelectedIndexChanged;
        }

        private void NapThucDon()
        {
            int maDanhMuc = cboDanhMuc.SelectedValue is int v ? v : 0;
            string tuKhoa = txtTimKiem.Text.Trim();

            List<MonAnDTO> thucDon = GoiMonBLL.LayThucDon(maDanhMuc, tuKhoa);

            dgvThucDon.DataSource = null;
            dgvThucDon.DataSource = thucDon;

            DinhDangLuoiThucDon();
            HienThiMonDangChon();
        }

        private void DinhDangLuoiThucDon()
        {
            if (dgvThucDon.Columns.Count == 0) return;

            // Ẩn các cột kỹ thuật
            string[] cotAn = { nameof(MonAnDTO.MaMon), nameof(MonAnDTO.MaDanhMuc), nameof(MonAnDTO.HinhAnh),
                               nameof(MonAnDTO.ConPhucVu), nameof(MonAnDTO.GiaNhap), nameof(MonAnDTO.DonViTinh) };
            foreach (string tenCot in cotAn)
            {
                if (dgvThucDon.Columns[tenCot] is DataGridViewColumn col)
                    col.Visible = false;
            }

            if (dgvThucDon.Columns[nameof(MonAnDTO.TenDanhMuc)] is DataGridViewColumn colDanhMuc)
            {
                colDanhMuc.HeaderText = "Danh mục";
                colDanhMuc.FillWeight = 30;
            }

            if (dgvThucDon.Columns[nameof(MonAnDTO.TenMon)] is DataGridViewColumn colTen)
            {
                colTen.HeaderText = "Tên món";
                colTen.FillWeight = 45;
            }

            if (dgvThucDon.Columns[nameof(MonAnDTO.DonGia)] is DataGridViewColumn colGia)
            {
                colGia.HeaderText = "Đơn giá";
                colGia.DefaultCellStyle.Format = "N0";
                colGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colGia.FillWeight = 25;
            }

            if (dgvThucDon.Columns[nameof(MonAnDTO.SoLuongTon)] is DataGridViewColumn colTon)
            {
                colTon.HeaderText = "Tồn";
                colTon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colTon.FillWeight = 15;
            }
        }

        private void cboDanhMuc_SelectedIndexChanged(object? sender, EventArgs e) => NapThucDon();

        private void txtTimKiem_TextChanged(object? sender, EventArgs e) => NapThucDon();
        #endregion

        #region Chọn món & Hiển thị chi tiết
        private void dgvThucDon_SelectionChanged(object? sender, EventArgs e) => HienThiMonDangChon();

        private void HienThiMonDangChon()
        {
            if (dgvThucDon.CurrentRow?.DataBoundItem is not MonAnDTO mon)
            {
                _coMonChon = false;
                _maMonChon = 0;
                _tenMonChon = string.Empty;
                _donGiaChon = 0m;
                _tonKhoChon = 0;

                txtTenMon.Clear();
                txtDonGia.Clear();
                XoaAnh();
                epSoLuong.SetError(txtSoLuong, "");
                return;
            }

            _coMonChon = true;
            _maMonChon = mon.MaMon;
            _tenMonChon = mon.TenMon;
            _donGiaChon = mon.DonGia;
            _tonKhoChon = mon.SoLuongTon;

            txtTenMon.Text = _tenMonChon;
            txtDonGia.Text = _donGiaChon.ToString("N0");
            txtSoLuong.Text = "1";
            HienThiAnh(mon.HinhAnh);
            KiemTraSoLuong();
        }

        private void XoaAnh()
        {
            Image? cu = picMon.Image;
            picMon.Image = null;
            cu?.Dispose();
        }

        private void HienThiAnh(string? hinhAnh)
        {
            XoaAnh();
            if (string.IsNullOrWhiteSpace(hinhAnh)) return;

            try
            {
                string duongDan = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, hinhAnh.Replace('/', Path.DirectorySeparatorChar));
                if (!File.Exists(duongDan)) return;

                using (var ms = new MemoryStream(File.ReadAllBytes(duongDan)))
                using (var anh = Image.FromStream(ms))
                {
                    picMon.Image = new Bitmap(anh);
                }
            }
            catch
            {
                // Bỏ qua lỗi load ảnh hỏng để không làm crash ứng dụng
            }
        }
        #endregion

        #region ErrorProvider kiểm tra số lượng món
        private void txtSoLuong_TextChanged(object? sender, EventArgs e) => KiemTraSoLuong();

        private void txtSoLuong_Validating(object? sender, CancelEventArgs e) => KiemTraSoLuong();

        private bool KiemTraSoLuong()
        {
            string nhap = txtSoLuong.Text.Trim();
            if (string.IsNullOrEmpty(nhap))
            {
                epSoLuong.SetError(txtSoLuong, "Vui lòng nhập số lượng.");
                return false;
            }

            if (!int.TryParse(nhap, out int sl))
            {
                epSoLuong.SetError(txtSoLuong, "Số lượng phải là số nguyên.");
                return false;
            }

            int daCoTrongGio = _coMonChon ? SoLuongTrongGio(_maMonChon) : 0;
            bool hopLe = GoiMonBLL.KiemTraSoLuongMon(sl, daCoTrongGio, _tonKhoChon, out string thongBaoLoi);

            epSoLuong.SetError(txtSoLuong, hopLe ? "" : thongBaoLoi);
            return hopLe;
        }

        private int SoLuongTrongGio(int maMon)
        {
            ChiTietDonHangDTO? daCo = _gioHang.FirstOrDefault(x => x.MaMon == maMon);
            return daCo?.SoLuong ?? 0;
        }
        #endregion

        #region Quản lý giỏ hàng
        private void KhoiTaoLuoiGioHang()
        {
            dgvGioHang.AutoGenerateColumns = false;
            dgvGioHang.Columns.Clear();

            // Cột MaMon (ẩn)
            var colMa = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ChiTietDonHangDTO.MaMon),
                HeaderText = "Mã",
                Visible = false,
                Width = 0
            };

            // Cột TenMon
            var colTen = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ChiTietDonHangDTO.TenMon),
                HeaderText = "Tên món",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            // Cột SoLuong
            var colSl = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ChiTietDonHangDTO.SoLuong),
                HeaderText = "SL",
                Width = 50
            };
            colSl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Cột DonGia
            var colGia = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ChiTietDonHangDTO.DonGia),
                HeaderText = "Đơn giá",
                Width = 80
            };
            colGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGia.DefaultCellStyle.Format = "N0";

            // Cột ThanhTien
            var colTien = new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                DataPropertyName = nameof(ChiTietDonHangDTO.ThanhTien),
                HeaderText = "Thành tiền",
                Width = 90
            };
            colTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTien.DefaultCellStyle.Format = "N0";

            dgvGioHang.Columns.AddRange(colMa, colTen, colSl, colGia, colTien);
            BindGioHang();
        }

        private void BindGioHang()
        {
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = _gioHang;

            // Tính toán lại cột ThanhTien cho tất cả rows
            foreach (DataGridViewRow row in dgvGioHang.Rows)
            {
                if (row.DataBoundItem is ChiTietDonHangDTO item)
                {
                    row.Cells["ThanhTien"].Value = item.ThanhTien;
                }
            }

            dgvGioHang.Refresh();
        }

        private void btnThemVaoGio_Click(object? sender, EventArgs e)
        {
            if (!_coMonChon)
            {
                MessageBox.Show(this, "Vui lòng chọn một món trong thực đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!KiemTraSoLuong())
            {
                txtSoLuong.Focus();
                txtSoLuong.SelectAll();
                return;
            }

            int soLuongThem = int.Parse(txtSoLuong.Text.Trim());
            ChiTietDonHangDTO? daCo = _gioHang.FirstOrDefault(x => x.MaMon == _maMonChon);

            if (daCo != null)
            {
                daCo.SoLuong += soLuongThem;
            }
            else
            {
                _gioHang.Add(new ChiTietDonHangDTO
                {
                    MaMon = _maMonChon,
                    TenMon = _tenMonChon,
                    SoLuong = soLuongThem,
                    DonGia = _donGiaChon
                });
            }

            BindGioHang();
            CapNhatTongTien();
            txtSoLuong.Text = "1";
            KiemTraSoLuong();
        }

        private void btnXoaMon_Click(object? sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow?.DataBoundItem is ChiTietDonHangDTO item)
            {
                _gioHang.Remove(item);
                BindGioHang();
                CapNhatTongTien();
                KiemTraSoLuong();
            }
        }

        private void CapNhatTongTien()
        {
            decimal tong = GoiMonBLL.TinhTongTien(_gioHang);
            lblTongTien.Text = $"Tổng tiền: {tong:N0} đ";
            btnDatMon.Enabled = _gioHang.Count > 0;
            btnXoaMon.Enabled = _gioHang.Count > 0;
        }
        #endregion

        #region Đặt món
        private void btnDatMon_Click(object? sender, EventArgs e)
        {
            if (_gioHang.Count == 0) return;

            string xacNhan = $"Gửi đơn gồm {_gioHang.Count} món (Tổng: {GoiMonBLL.TinhTongTien(_gioHang):N0} đ)?";
            if (MessageBox.Show(this, xacNhan, "Xác nhận đặt món", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                DatMonResultDTO kq = GoiMonBLL.DatMon(_maPhienChoi, _maMay, _tenMay, _tenDangNhap, _gioHang.ToList());

                if (kq.ThanhCong)
                {
                    MessageBox.Show(this, kq.ThongBao, "Đặt món thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(this, kq.ThongBao, "Không thể đặt món", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    try { NapThucDon(); } catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Có lỗi xảy ra khi gửi đơn:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try { NapThucDon(); } catch { }
            }
        }
        #endregion
    }
}

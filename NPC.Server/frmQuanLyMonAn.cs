using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NPC.BLL;
using NPC.DTO;

namespace NPC.Server
{
    public partial class frmQuanLyMonAn : Form
    {
        private readonly MonAnBLL _monAnBLL = new MonAnBLL();
        private string _duongDanAnhHienTai = string.Empty;
        public frmQuanLyMonAn()
        {
            InitializeComponent();
        }

        private void txtMaMon_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtSoLuongTon_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaMon.Text, out int maMon) || maMon <= 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa/ngừng phục vụ món '{txtTenMon.Text}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (_monAnBLL.XoaMonAn(maMon, out string msg))
                {
                    MessageBox.Show(string.IsNullOrEmpty(msg) ? "Xóa món ăn thành công" : msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoiForm();
                    LoadDanhSachMonAn();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaMon.Text, out int maMon) || maMon <= 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn cần sửa trong bảng danh sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cboLoaiMon.SelectedValue == null || !int.TryParse(cboLoaiMon.SelectedValue.ToString(), out int maDM))
            {
                MessageBox.Show("Vui lòng chọn danh mục món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal.TryParse(txtGiaBan.Text.Replace(",", "").Replace(".", ""), out decimal giaBan);
            decimal.TryParse(txtGiaNhap.Text.Replace(",", "").Replace(".", ""), out decimal giaNhap);
            int.TryParse(txtSoLuongTon.Text.Replace(",", "").Replace(".", ""), out int tonKho);

            MonAnDTO mon = new MonAnDTO
            {
                MaMon = maMon,
                TenMon = txtTenMon.Text.Trim(),
                MaDanhMuc = maDM,
                DonGia = giaBan,
                GiaNhap = giaNhap,
                SoLuongTon = tonKho,
                DonViTinh = cboDonViTinh.SelectedItem?.ToString() ?? cboDonViTinh.Text.Trim(),
                HinhAnh = _duongDanAnhHienTai,
                ConPhucVu = cboTrangThai.SelectedIndex == 0
            };

            if (_monAnBLL.CapNhatMonAn(mon, out string msg))
            {
                MessageBox.Show("Cập nhật thông tin món ăn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachMonAn();
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaMon.Text, out int maMon) || maMon <= 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn cần nhập thêm kho trong bảng danh sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string inputSoLuong = Microsoft.VisualBasic.Interaction.InputBox($"Nhập số lượng '{txtTenMon.Text}' nhập thêm vào kho:", "Nhập Kho Hàng", "20");
            if (string.IsNullOrWhiteSpace(inputSoLuong)) return;

            if (!int.TryParse(inputSoLuong.Replace(",", "").Replace(".", ""), out int soLuongThem) || soLuongThem <= 0)
            {
                MessageBox.Show("Số lượng nhập kho phải là số nguyên lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string inputGiaNhap = Microsoft.VisualBasic.Interaction.InputBox("Nhập đơn giá nhập đợt này (để 0 nếu giữ nguyên giá cũ):", "Đơn Giá Nhập Hàng", txtGiaNhap.Text.Replace(",", "").Replace(".", ""));
            decimal giaNhapMoi = 0;
            if (!string.IsNullOrWhiteSpace(inputGiaNhap))
            {
                decimal.TryParse(inputGiaNhap.Replace(",", "").Replace(".", ""), out giaNhapMoi);
            }

            if (_monAnBLL.NhapKhoMonAn(maMon, soLuongThem, giaNhapMoi, out string thongBao))
            {
                MessageBox.Show($"Nhập kho thành công!\nĐã cộng thêm +{soLuongThem} {cboDonViTinh.Text} vào kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachMonAn();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi nhập kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboLoaiMon.SelectedValue == null || !int.TryParse(cboLoaiMon.SelectedValue.ToString(), out int maDM))
            {
                MessageBox.Show("Vui lòng chọn danh mục món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal.TryParse(txtGiaBan.Text.Replace(",", "").Replace(".", ""), out decimal giaBan);
            decimal.TryParse(txtGiaNhap.Text.Replace(",", "").Replace(".", ""), out decimal giaNhap);
            int.TryParse(txtSoLuongTon.Text.Replace(",", "").Replace(".", ""), out int tonKho);

            MonAnDTO mon = new MonAnDTO
            {
                TenMon = txtTenMon.Text.Trim(),
                MaDanhMuc = maDM,
                DonGia = giaBan,
                GiaNhap = giaNhap,
                SoLuongTon = tonKho,
                DonViTinh = cboDonViTinh.SelectedItem?.ToString() ?? cboDonViTinh.Text.Trim(),
                HinhAnh = _duongDanAnhHienTai,
                ConPhucVu = cboTrangThai.SelectedIndex == 0
            };

            if (_monAnBLL.ThemMonAn(mon, out string msg))
            {
                MessageBox.Show("Thêm món ăn mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                LoadDanhSachMonAn();
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LamMoiForm()
        {
            txtMaMon.Text = "";
            txtTenMon.Text = "";
            if (cboLoaiMon.Items.Count > 0) cboLoaiMon.SelectedIndex = 0;
            txtGiaBan.Text = "0";
            txtGiaNhap.Text = "0";
            txtSoLuongTon.Text = "0";
            cboDonViTinh.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            btnXoaAnh_Click(null, EventArgs.Empty);
            txtTenMon.Focus();
        }

        private void frmQuanLyMonAn_Load(object sender, EventArgs e)
        {
            KhoiTaoGiaoDien();
            LoadComboBoxDanhMuc();
            LoadDanhSachMonAn();
        }
        private void KhoiTaoGiaoDien()
        {
            // cấu hình cboDonViTinh
            cboDonViTinh.Items.Clear();
            cboDonViTinh.Items.AddRange(new object[] { "Phần", "Chai", "Lon", "Ly", "Tô", "Dĩa", "Gói", "Thẻ" });
            cboDonViTinh.SelectedIndex = 0;

            // cấu hình cboTrangThai
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Còn phục vụ", "Ngừng phục vụ" });
            cboTrangThai.SelectedIndex = 0;

            // 3. Cấu hình DataGridView
            CauHinhCotDataGrid();

            dgvMonAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMonAn.MultiSelect = false;
            dgvMonAn.ReadOnly = true;
            dgvMonAn.AllowUserToAddRows = false;
            dgvMonAn.AllowUserToDeleteRows = false;

            // Bắt sự kiện chọn dòng trên bảng
            //dgvMonAn.SelectionChanged -= dgvMonAn_SelectionChanged;
            //dgvMonAn.SelectionChanged += dgvMonAn_SelectionChanged;

            // 4. Bắt sự kiện tìm kiếm & lọc nhanh
            //txtTimKiem.TextChanged -= txtTimKiem_TextChanged;
            //txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            //cboTimKiemLoai.SelectedIndexChanged -= cboTimKiemLoai_SelectedIndexChanged;
            //cboTimKiemLoai.SelectedIndexChanged += cboTimKiemLoai_SelectedIndexChanged;

            //chkTonKhoThap.CheckedChanged -= chkTonKhoThap_CheckedChanged;
            //chkTonKhoThap.CheckedChanged += chkTonKhoThap_CheckedChanged;

            LamMoiForm();
        }
        private void CauHinhCotDataGrid()
        {
            dgvMonAn.Columns.Clear();
            dgvMonAn.AutoGenerateColumns = false;

            dgvMonAn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaMon",
                DataPropertyName = "MaMon",
                HeaderText = "Mã Món",
                Width = 70
            });

            dgvMonAn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenMon",
                DataPropertyName = "TenMon",
                HeaderText = "Tên món ăn / Đồ uống",
                Width = 180
            });

            dgvMonAn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenDanhMuc",
                DataPropertyName = "TenDanhMuc",
                HeaderText = "Loại thực đơn",
                Width = 130
            });

            var colDonGia = new DataGridViewTextBoxColumn
            {
                Name = "colDonGia",
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá (VNĐ)",
                Width = 110
            };
            colDonGia.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMonAn.Columns.Add(colDonGia);

            var colGiaNhap = new DataGridViewTextBoxColumn
            {
                Name = "colGiaNhap",
                DataPropertyName = "GiaNhap",
                HeaderText = "Giá nhập (VNĐ)",
                Width = 110
            };
            colGiaNhap.DefaultCellStyle.Format = "N0";
            colGiaNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMonAn.Columns.Add(colGiaNhap);

            var colSoLuong = new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongTon",
                DataPropertyName = "SoLuongTon",
                HeaderText = "Tồn kho",
                Width = 80
            };
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMonAn.Columns.Add(colSoLuong);

            dgvMonAn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDonViTinh",
                DataPropertyName = "DonViTinh",
                HeaderText = "ĐVT",
                Width = 70
            });

            dgvMonAn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                DataPropertyName = "TrangThaiText",
                HeaderText = "Trạng thái",
                Width = 110
            });

            dgvMonAn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCanhBao",
                DataPropertyName = "CanhBaoText",
                HeaderText = "Cảnh báo kho",
                Width = 110
            });
        }
        private void LoadComboBoxDanhMuc()
        {
            try
            {
                List<DanhMucDTO> list = _monAnBLL.LayDanhSachDanhMuc();

                // ComboBox loại món trong phần thông tin chi tiết
                cboLoaiMon.DataSource = new List<DanhMucDTO>(list);
                cboLoaiMon.DisplayMember = "TenDanhMuc";
                cboLoaiMon.ValueMember = "MaDanhMuc";

                // ComboBox lọc tìm kiếm (thêm mục Tất cả)
                List<DanhMucDTO> listTimKiem = new List<DanhMucDTO>();
                listTimKiem.Add(new DanhMucDTO { MaDanhMuc = 0, TenDanhMuc = "--- Tất cả loại ---" });
                listTimKiem.AddRange(list);

                cboTimKiemLoai.DataSource = listTimKiem;
                cboTimKiemLoai.DisplayMember = "TenDanhMuc";
                cboTimKiemLoai.ValueMember = "MaDanhMuc";
                cboTimKiemLoai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDanhSachMonAn()
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                int? maDanhMuc = null;
                if (cboTimKiemLoai.SelectedValue != null && int.TryParse(cboTimKiemLoai.SelectedValue.ToString(), out int id) && id > 0)
                {
                    maDanhMuc = id;
                }

                bool chiLayTonThap = chkTonKhoThap.Checked;

                DataTable dt = _monAnBLL.LayDataTable(tuKhoa, maDanhMuc, chiLayTonThap);
                dgvMonAn.DataSource = dt;

                int tongMon = dt.Rows.Count;
                int tongTonKho = 0;
                int soLuongSapHet = 0;

                foreach (DataRow row in dt.Rows)
                {
                    int ton = Convert.ToInt32(row["SoLuongTon"]);
                    tongTonKho += ton;
                    if (ton < 5) soLuongSapHet++;
                }

                lblTongSoMon.Text = $"Tổng số món: {tongMon}";
                lblTongTonKho.Text = $"Tổng tồn kho: {tongTonKho:N0}";

                if (soLuongSapHet > 0)
                {
                    lblCanhBao.Text = $"⚠️ Có {soLuongSapHet} món sắp hết hàng (< 5)";
                    lblCanhBao.ForeColor = Color.Red;
                }
                else
                {
                    lblCanhBao.Text = "Trạng thái kho: Ổn định";
                    lblCanhBao.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách món ăn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMonAn.Rows.Count) return;
            HienThiThongTinMonAnDaChon();
        }

        private void dgvMonAn_SelectionChanged(object sender, EventArgs e)
        {
            HienThiThongTinMonAnDaChon();
        }
        private void HienThiThongTinMonAnDaChon()
        {
            if (dgvMonAn.CurrentRow == null || dgvMonAn.CurrentRow.Index < 0) return;
            DataGridViewRow row = dgvMonAn.CurrentRow;

            if (row.DataBoundItem is DataRowView drv)
            {
                DataRow dr = drv.Row;
                txtMaMon.Text = dr["MaMon"]?.ToString() ?? "";
                txtTenMon.Text = dr["TenMon"]?.ToString() ?? "";

                if (int.TryParse(dr["MaDanhMuc"]?.ToString(), out int maDM))
                {
                    cboLoaiMon.SelectedValue = maDM;
                }

                txtGiaBan.Text = $"{Convert.ToDecimal(dr["DonGia"]):N0}";
                txtGiaNhap.Text = $"{Convert.ToDecimal(dr["GiaNhap"]):N0}";
                txtSoLuongTon.Text = dr["SoLuongTon"]?.ToString() ?? "0";

                string dvt = dr["DonViTinh"]?.ToString() ?? "Phần";
                cboDonViTinh.SelectedItem = dvt;
                if (cboDonViTinh.SelectedIndex < 0) cboDonViTinh.Text = dvt;

                bool conPhucVu = Convert.ToBoolean(dr["ConPhucVu"]);
                cboTrangThai.SelectedIndex = conPhucVu ? 0 : 1;

                string hinhAnh = dr["HinhAnh"]?.ToString() ?? "";
                _duongDanAnhHienTai = hinhAnh;
                HienThiAnhMonAn(hinhAnh);

                txtMaMon.ReadOnly = true;
            }
        }
        private void HienThiAnhMonAn(string duongDan)
        {
            try
            {
                if (picHinhAnh.Image != null)
                {
                    var oldImg = picHinhAnh.Image;
                    picHinhAnh.Image = null;
                    oldImg.Dispose();
                }

                if (string.IsNullOrWhiteSpace(duongDan))
                {
                    picHinhAnh.Image = null;
                    return;
                }

                string fullPath = Path.IsPathRooted(duongDan)
                    ? duongDan
                    : Path.Combine(Application.StartupPath, duongDan);

                if (File.Exists(fullPath))
                {
                    using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        picHinhAnh.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    picHinhAnh.Image = null;
                }
            }
            catch
            {
                picHinhAnh.Image = null;
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All Files (*.*)|*.*";
                ofd.Title = "Chọn ảnh món ăn";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string folder = Path.Combine(Application.StartupPath, "Resources", "Images", "Products");
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string ext = Path.GetExtension(ofd.FileName);
                        string fileName = $"food_{DateTime.Now:yyyyMMdd_HHmmss}{ext}";
                        string destPath = Path.Combine(folder, fileName);

                        File.Copy(ofd.FileName, destPath, true);

                        _duongDanAnhHienTai = Path.Combine("Resources", "Images", "Products", fileName);
                        HienThiAnhMonAn(destPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi sao chép ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            _duongDanAnhHienTai = string.Empty;
            if (picHinhAnh.Image != null)
            {
                var old = picHinhAnh.Image;
                picHinhAnh.Image = null;
                old.Dispose();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            cboTimKiemLoai.SelectedIndex = 0;
            chkTonKhoThap.Checked = false;
            LamMoiForm();
            LoadDanhSachMonAn();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachMonAn();
        }

        private void cboTimKiemLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDanhSachMonAn();
        }
    }
}

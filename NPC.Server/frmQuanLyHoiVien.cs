using NPC.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPC.DTO;

namespace NPC.Server
{
    public partial class frmQuanLyHoiVien : Form
    {
        private readonly HoiVienBLL _hoiVienBLL = new HoiVienBLL();

        public frmQuanLyHoiVien()
        {
            InitializeComponent();
        }

        private void frmQuanLyHoiVien_Load(object? sender, EventArgs e)
        {
            KhoiTaoGiaoDien();
            LoadDanhSachHoiVien();
        }

        private void KhoiTaoGiaoDien()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Hoạt động", "Bị khóa", "Đang chơi" });
            cboTrangThai.SelectedIndex = 0;

            CauHinhCotDataGrid();

            dgvHoiVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoiVien.MultiSelect = false;
            dgvHoiVien.ReadOnly = true;
            dgvHoiVien.AllowUserToAddRows = false;
            dgvHoiVien.AllowUserToDeleteRows = false;
            dgvHoiVien.SelectionChanged += (s, e) => HienThiThongTinHoiVienDaChon();
            LamMoiForm();
        }
        private void CauHinhCotDataGrid()
        {
            dgvHoiVien.Columns.Clear();
            dgvHoiVien.AutoGenerateColumns = false;

            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaHoiVien",
                DataPropertyName = "MaHoiVien",
                HeaderText = "Mã HV",
                Width = 75
            });

            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenDangNhap",
                DataPropertyName = "TenDangNhap",
                HeaderText = "Tên đăng nhập",
                Width = 120
            });

            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHoTen",
                DataPropertyName = "HoTen",
                HeaderText = "Họ và tên",
                Width = 150
            });

            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDienThoai",
                DataPropertyName = "DienThoai",
                HeaderText = "Số điện thoại",
                Width = 110
            });

            var colSoDu = new DataGridViewTextBoxColumn
            {
                Name = "colSoDu",
                DataPropertyName = "SoDu",
                HeaderText = "Số dư (VNĐ)",
                Width = 120
            };
            colSoDu.DefaultCellStyle.Format = "N0";
            colSoDu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHoiVien.Columns.Add(colSoDu);

            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTongThoiGian",
                DataPropertyName = "TongThoiGianChoi",
                HeaderText = "Tổng phút",
                Width = 90
            });

            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 100
            });

            var colNgayTao = new DataGridViewTextBoxColumn
            {
                Name = "colNgayTao",
                DataPropertyName = "NgayTao",
                HeaderText = "Ngày tạo",
                Width = 110
            };
            colNgayTao.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHoiVien.Columns.Add(colNgayTao);
        }

        private void LamMoiForm()
        {
            txtMaHoiVien.Text = "";
            txtMaHoiVien.ReadOnly = false;
            txtHoTen.Text = "";
            txtMatKhau.Text = "";
            txtDienThoai.Text = "";
            txtSoDu.Text = "0";
            txtSoDu.ReadOnly = false;
            if (cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }
            txtHoTen.Focus();
        }

        private void LoadDanhSachHoiVien(string tuKhoa = "")
        {
            try
            {
                // Sử dụng hàm GetDataToTable thông qua BLL
                DataTable dt = _hoiVienBLL.LayDataTable(tuKhoa);
                dgvHoiVien.DataSource = dt;

                decimal tongSoDu = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongSoDu += Convert.ToDecimal(row["SoDu"]);
                }
                lblTongSoHoiVien.Text = $"Tổng số hội viên: {dt.Rows.Count}";
                lblTongSoDu.Text = $"Tổng số dư tài khoản: {tongSoDu:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hội viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHoiVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHoiVien.Rows.Count) return;
            HienThiThongTinHoiVienDaChon();
        }

        private void HienThiThongTinHoiVienDaChon()
        {
            if (dgvHoiVien.CurrentRow == null || dgvHoiVien.CurrentRow.Index < 0) return;
            DataGridViewRow row = dgvHoiVien.CurrentRow;

            if (row.DataBoundItem is DataRowView drv)
            {
                DataRow dr = drv.Row;
                txtMaHoiVien.Text = dr["MaHoiVien"]?.ToString() ?? "";
                txtHoTen.Text = dr["HoTen"]?.ToString() ?? "";
                txtMatKhau.Text = "";
                txtDienThoai.Text = dr["DienThoai"] != DBNull.Value ? dr["DienThoai"]?.ToString() ?? "" : "";
                txtSoDu.Text = $"{Convert.ToDecimal(dr["SoDu"]):N0}";
                cboTrangThai.SelectedItem = dr["TrangThai"]?.ToString() ?? "Hoạt động";
                txtMaHoiVien.ReadOnly = true;
                txtSoDu.ReadOnly = true;
            }
            else if (row.DataBoundItem is HoiVienDTO hv)
            {
                txtMaHoiVien.Text = hv.MaHoiVien.ToString();
                txtHoTen.Text = hv.HoTen;
                txtMatKhau.Text = "";
                txtDienThoai.Text = hv.DienThoai;
                txtSoDu.Text = $"{hv.SoDu:N0}";
                cboTrangThai.SelectedItem = hv.TrangThai;
                txtMaHoiVien.ReadOnly = true;
                txtSoDu.ReadOnly = true;
            }
            else
            {
                txtMaHoiVien.Text = row.Cells["colMaHoiVien"]?.Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["colHoTen"]?.Value?.ToString() ?? "";
                txtMatKhau.Text = "";
                txtDienThoai.Text = row.Cells["colDienThoai"]?.Value?.ToString() ?? "";
                txtSoDu.Text = row.Cells["colSoDu"]?.Value?.ToString() ?? "0";
                cboTrangThai.SelectedItem = row.Cells["colTrangThai"]?.Value?.ToString() ?? "Hoạt động";
                txtMaHoiVien.ReadOnly = true;
                txtSoDu.ReadOnly = true;
            }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            string tenDangNhap = txtDienThoai.Text.Trim();
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                tenDangNhap = "hv_" + DateTime.Now.ToString("mmss");
            }
            HoiVienDTO hv = new HoiVienDTO
            {
                TenDangNhap = tenDangNhap,
                HoTen = txtHoTen.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                NgayTao = DateTime.Now,
                TongThoiGianChoi = 0,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động"
            };
            if (decimal.TryParse(txtSoDu.Text.Replace(",", "").Replace(".", ""), out decimal sd))
            {
                hv.SoDu = sd;
            }
            if (_hoiVienBLL.ThemHoiVien(hv, out string msg))
            {
                MessageBox.Show("Thêm mới hội viên thành công!\nTên đăng nhập: " + hv.TenDangNhap, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                LoadDanhSachHoiVien();
            }
            else
            {
                MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDong_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoiVien.Text, out int maHoiVien) || maHoiVien <= 0)
            {
                MessageBox.Show("Vui lòng chọn một hội viên trong bảng danh sách để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HoiVienDTO hv = new HoiVienDTO
            {
                MaHoiVien = maHoiVien,
                HoTen = txtHoTen.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động"
            };
            string matKhauMoi = txtMatKhau.Text.Trim();
            if (_hoiVienBLL.CapNhatHoiVien(hv, matKhauMoi, out string msg))
            {
                MessageBox.Show("Cập nhật thông tin hội viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                LoadDanhSachHoiVien();
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoiVien.Text, out int maHoiVien) || maHoiVien <= 0)
            {
                MessageBox.Show("Vui lòng chọn hội viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa hội viên '{txtHoTen.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (_hoiVienBLL.XoaHoiVien(maHoiVien, out string msg))
                {
                    MessageBox.Show(string.IsNullOrEmpty(msg) ? "Xóa hội viên thành công!" : msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoiForm();
                    LoadDanhSachHoiVien();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNhapLai_Click(object? sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void btnNapTien_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoiVien.Text, out int maHoiVien) || maHoiVien <= 0)
            {
                MessageBox.Show("Vui lòng chọn hội viên cần nạp tiền trong danh sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập số tiền cần nạp cho hội viên (VNĐ):", "Nạp Tiền Nhanh", "50000");
            if (string.IsNullOrWhiteSpace(input)) return;
            if (decimal.TryParse(input.Replace(",", "").Replace(".", ""), out decimal soTienNap) && soTienNap > 0)
            {
                if (_hoiVienBLL.NapTienHoiVien(maHoiVien, soTienNap, 1, "Tiền mặt", out string thongBao))
                {
                    MessageBox.Show(thongBao, "Nạp tiền thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachHoiVien();
                }
                else
                {
                    MessageBox.Show(thongBao, "Lỗi nạp tiền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Số tiền nạp không hợp lệ (phải là số tiền dương và là bội số của 1.000đ)", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            LoadDanhSachHoiVien(txtTimKiem.Text.Trim());
        }

        private void txtTimKiem_TextChanged(object? sender, EventArgs e)
        {
            LoadDanhSachHoiVien(txtTimKiem.Text.Trim());
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LamMoiForm();
        }

        private void btnXuatFile_Click(object? sender, EventArgs e)
        {
            saveFileDialog.Filter = "File CSV (*.csv)|*.csv|All Files (*.*)|*.*";
            saveFileDialog.FileName = $"DanhSachHoiVien_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dt = _hoiVienBLL.LayDataTable();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Mã HV,Tên đăng nhập,Họ tên,Điện thoại,Số dư,Tổng phút chơi,Ngày tạo,Trạng thái");
                    foreach (DataRow row in dt.Rows)
                    {
                        string dtStr = row["DienThoai"] != DBNull.Value ? row["DienThoai"]?.ToString() ?? "" : "";
                        DateTime ngayTao = Convert.ToDateTime(row["NgayTao"]);
                        sb.AppendLine($"{row["MaHoiVien"]},\"{row["TenDangNhap"]}\",\"{row["HoTen"]}\",\"{dtStr}\",{row["SoDu"]},{row["TongThoiGianChoi"]},{ngayTao:yyyy-MM-dd HH:mm:ss},\"{row["TrangThai"]}\"");
                    }
                    File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Xuất danh sách hội viên thành công tại:\n" + saveFileDialog.FileName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NPC.DAL;

namespace NPC.Server
{
    public class frmXemNhatKy : Form
    {
        public enum LoaiNhatKy
        {
            NhatKyHoiVien,
            NhatKyHeThong
        }

        private readonly LoaiNhatKy _loaiNhatKy;
        public LoaiNhatKy LoaiHienTai => _loaiNhatKy;
        private readonly string _filePath;

        // UI Controls
        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;
        private Label lblThongTinFile = null!;
        private Panel pnlBoLoc = null!;
        private Label lblTimKiem = null!;
        private TextBox txtTimKiem = null!;
        private Label lblMucDo = null!;
        private ComboBox cboMucDo = null!;
        private Button btnLamMoi = null!;
        private Button btnMoFileTrucTiep = null!;
        private DataGridView dgvLog = null!;
        private StatusStrip statusStrip = null!;
        private ToolStripStatusLabel lblTongSoDong = null!;
        private ToolStripStatusLabel lblDuongDanFile = null!;

        private DataTable _dtLog = null!;

        public frmXemNhatKy() : this(LoaiNhatKy.NhatKyHoiVien) { }

        public frmXemNhatKy(LoaiNhatKy loai)
        {
            _loaiNhatKy = loai;
            _filePath = (loai == LoaiNhatKy.NhatKyHoiVien) ? AppPaths.LogPath : AppPaths.AppLogPath;

            KhoiTaoGiaoDien();
            LoadDuLieu();
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = _loaiNhatKy == LoaiNhatKy.NhatKyHoiVien ? "Báo Cáo Nhật Ký Hội Viên" : "Nhật Ký Kỹ Thuật Hệ Thống";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5F);

            // 1. Panel Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = _loaiNhatKy == LoaiNhatKy.NhatKyHoiVien ? Color.FromArgb(31, 78, 121) : Color.FromArgb(45, 52, 54),
                Padding = new Padding(15, 8, 15, 8)
            };

            lblTieuDe = new Label
            {
                Text = _loaiNhatKy == LoaiNhatKy.NhatKyHoiVien ? "NHẬT KÝ HOẠT ĐỘNG HỘI VIÊN & GIAO DỊCH" : "NHẬT KÝ HOẠT ĐỘNG & LỖI HỆ THỐNG (APP.LOG)",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                AutoSize = true
            };

            lblThongTinFile = new Label
            {
                Text = $"Nguồn file: {_filePath}",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(220, 220, 220),
                Dock = DockStyle.Bottom,
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblThongTinFile);

            // 2. Panel Bộ Lọc & Công Cụ
            pnlBoLoc = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(245, 246, 250),
                Padding = new Padding(10, 8, 10, 8)
            };

            lblTimKiem = new Label
            {
                Text = "Tìm kiếm:",
                Location = new Point(15, 14),
                AutoSize = true
            };

            txtTimKiem = new TextBox
            {
                Location = new Point(85, 11),
                Width = 240,
                PlaceholderText = "Nhập từ khóa cần tìm..."
            };
            txtTimKiem.TextChanged += (s, e) => ApDungBoLoc();

            lblMucDo = new Label
            {
                Text = "Mức độ:",
                Location = new Point(340, 14),
                AutoSize = true
            };

            cboMucDo = new ComboBox
            {
                Location = new Point(400, 11),
                Width = 140,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            if (_loaiNhatKy == LoaiNhatKy.NhatKyHoiVien)
            {
                cboMucDo.Items.AddRange(new object[] { "--- Tất cả ---", "Đăng nhập Client", "Trả máy Client", "Nạp tiền", "Giao dịch" });
            }
            else
            {
                cboMucDo.Items.AddRange(new object[] { "--- Tất cả ---", "INFO", "WARN", "ERROR" });
            }
            cboMucDo.SelectedIndex = 0;
            cboMucDo.SelectedIndexChanged += (s, e) => ApDungBoLoc();

            btnLamMoi = new Button
            {
                Text = "🔄 Làm mới (F5)",
                Location = new Point(560, 9),
                Size = new Size(120, 30),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnLamMoi.Click += (s, e) => LoadDuLieu();

            btnMoFileTrucTiep = new Button
            {
                Text = "📂 Mở file gốc",
                Location = new Point(690, 9),
                Size = new Size(120, 30),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnMoFileTrucTiep.Click += BtnMoFileTrucTiep_Click;

            pnlBoLoc.Controls.AddRange(new Control[] { lblTimKiem, txtTimKiem, lblMucDo, cboMucDo, btnLamMoi, btnMoFileTrucTiep });

            // 3. DataGridView hiển thị dữ liệu log
            dgvLog = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                EnableHeadersVisualStyles = false
            };
            dgvLog.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 245);
            dgvLog.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvLog.ColumnHeadersHeight = 32;
            dgvLog.RowTemplate.Height = 26;

            // 4. StatusStrip
            statusStrip = new StatusStrip();
            lblTongSoDong = new ToolStripStatusLabel { Text = "Tổng: 0 dòng" };
            lblDuongDanFile = new ToolStripStatusLabel { Text = $"| File: {_filePath}", Spring = true, TextAlign = ContentAlignment.MiddleRight };
            statusStrip.Items.AddRange(new ToolStripItem[] { lblTongSoDong, lblDuongDanFile });

            // Thêm controls vào Form
            this.Controls.Add(dgvLog);
            this.Controls.Add(pnlBoLoc);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(statusStrip);

            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F5) LoadDuLieu();
            };
        }

        private void BtnMoFileTrucTiep_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    MessageBox.Show(this, $"File log chưa tồn tại tại:\n{_filePath}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = _filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể mở file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDuLieu()
        {
            _dtLog = new DataTable();

            if (_loaiNhatKy == LoaiNhatKy.NhatKyHoiVien)
            {
                _dtLog.Columns.Add("ThoiGian", typeof(string));
                _dtLog.Columns.Add("NguoiDung", typeof(string));
                _dtLog.Columns.Add("HanhDong", typeof(string));
                _dtLog.Columns.Add("ChiTiet", typeof(string));

                DocFileNhatKyHoiVien();
            }
            else
            {
                _dtLog.Columns.Add("ThoiGian", typeof(string));
                _dtLog.Columns.Add("MucDo", typeof(string));
                _dtLog.Columns.Add("NoiDung", typeof(string));

                DocFileNhatKyHeThong();
            }

            ApDungBoLoc();
            CauHinhCotGrid();
        }

        private void DocFileNhatKyHoiVien()
        {
            if (!File.Exists(_filePath)) return;

            try
            {
                // Dùng FileShare.ReadWrite để không bị xung đột khi Server/Client đang mở file log
                using var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs, Encoding.UTF8);

                // Mẫu dòng log nghiệp vụ:
                // [2026-11-02 11:30:14] [Log] [Người dùng: hoivien01] - Hành vi: Đăng nhập Client | Chi tiết: Hội viên 'hoivien01' đăng nhập...
                // hoặc format cũ: [2026-10-30 15:29:08] [Log] [Người dùng: admin] - Hành vi: ...
                Regex regex = new Regex(@"^\[(?<Time>[^\]]+)\]\s+\[Log\]\s+(?:\[Người dùng:\s*(?<User>[^\]]*)\])?(?:\s*-\s*Hành vi:\s*(?<Action>[^\|]+))?(?:\s*\|\s*Chi tiết:\s*(?<Detail>.*))?", RegexOptions.Compiled);

                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        string user = match.Groups["User"].Value.Trim();
                        string action = match.Groups["Action"].Value.Trim();
                        string detail = match.Groups["Detail"].Value.Trim();

                        // Nếu không parse được chuẩn theo mẫu Người dùng/Hành vi thì đưa line vào ChiTiet
                        if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(action))
                        {
                            detail = line;
                        }

                        // Lọc riêng nhật ký liên quan đến Hội viên nếu người dùng mở báo cáo Hội viên:
                        // bao gồm: user là hội viên, hành vi đăng nhập/trả máy/nạp tiền/đặt món...
                        _dtLog.Rows.Add(match.Groups["Time"].Value.Trim(), user, action, detail);
                    }
                    else
                    {
                        _dtLog.Rows.Add("", "", "", line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Lỗi đọc file logs.txt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DocFileNhatKyHeThong()
        {
            if (!File.Exists(_filePath)) return;

            try
            {
                using var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs, Encoding.UTF8);

                // Mẫu app.log: [2026-10-30 15:29:00] [INFO] Nội dung...
                Regex regex = new Regex(@"^\[(?<Time>[^\]]+)\]\s+\[(?<Level>INFO|WARN|ERROR)\]\s+(?<Message>.*)$", RegexOptions.Compiled);

                string? line;
                string lastTime = "";
                string lastLevel = "INFO";
                StringBuilder currentMsg = new StringBuilder();

                void PushEntry()
                {
                    if (currentMsg.Length > 0)
                    {
                        _dtLog.Rows.Add(lastTime, lastLevel, currentMsg.ToString().Trim());
                        currentMsg.Clear();
                    }
                }

                while ((line = sr.ReadLine()) != null)
                {
                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        PushEntry();
                        lastTime = match.Groups["Time"].Value.Trim();
                        lastLevel = match.Groups["Level"].Value.Trim();
                        currentMsg.Append(match.Groups["Message"].Value);
                    }
                    else
                    {
                        // Hỗ trợ trường hợp Exception in nhiều dòng StackTrace
                        if (currentMsg.Length > 0)
                            currentMsg.AppendLine().Append(line);
                        else
                            _dtLog.Rows.Add("", "INFO", line);
                    }
                }
                PushEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Lỗi đọc file app.log: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApDungBoLoc()
        {
            if (_dtLog == null) return;

            DataView dv = _dtLog.DefaultView;
            string keyword = txtTimKiem.Text.Trim().Replace("'", "''");
            string filter = "";

            if (_loaiNhatKy == LoaiNhatKy.NhatKyHoiVien)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    filter = $"(NguoiDung LIKE '%{keyword}%' OR HanhDong LIKE '%{keyword}%' OR ChiTiet LIKE '%{keyword}%' OR ThoiGian LIKE '%{keyword}%')";
                }

                if (cboMucDo.SelectedIndex > 0)
                {
                    string actionFilter = cboMucDo.SelectedItem?.ToString() ?? "";
                    string sub = $"(HanhDong LIKE '%{actionFilter}%' OR ChiTiet LIKE '%{actionFilter}%')";
                    filter = string.IsNullOrEmpty(filter) ? sub : $"{filter} AND {sub}";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    filter = $"(NoiDung LIKE '%{keyword}%' OR ThoiGian LIKE '%{keyword}%')";
                }

                if (cboMucDo.SelectedIndex > 0)
                {
                    string lvl = cboMucDo.SelectedItem?.ToString() ?? "";
                    string sub = $"MucDo = '{lvl}'";
                    filter = string.IsNullOrEmpty(filter) ? sub : $"{filter} AND {sub}";
                }
            }

            dv.RowFilter = filter;
            // Đảo thứ tự để dòng mới nhất lên đầu
            dgvLog.DataSource = dv;
            lblTongSoDong.Text = $"Hiển thị: {dv.Count:N0} / {_dtLog.Rows.Count:N0} dòng";
        }

        private void CauHinhCotGrid()
        {
            if (dgvLog.Columns.Count == 0) return;

            if (_loaiNhatKy == LoaiNhatKy.NhatKyHoiVien)
            {
                if (dgvLog.Columns["ThoiGian"] != null)
                {
                    dgvLog.Columns["ThoiGian"].HeaderText = "Thời gian";
                    dgvLog.Columns["ThoiGian"].FillWeight = 18;
                }
                if (dgvLog.Columns["NguoiDung"] != null)
                {
                    dgvLog.Columns["NguoiDung"].HeaderText = "Người dùng / Tài khoản";
                    dgvLog.Columns["NguoiDung"].FillWeight = 22;
                }
                if (dgvLog.Columns["HanhDong"] != null)
                {
                    dgvLog.Columns["HanhDong"].HeaderText = "Hành vi / Thao tác";
                    dgvLog.Columns["HanhDong"].FillWeight = 20;
                }
                if (dgvLog.Columns["ChiTiet"] != null)
                {
                    dgvLog.Columns["ChiTiet"].HeaderText = "Chi tiết sự kiện";
                    dgvLog.Columns["ChiTiet"].FillWeight = 40;
                }
            }
            else
            {
                if (dgvLog.Columns["ThoiGian"] != null)
                {
                    dgvLog.Columns["ThoiGian"].HeaderText = "Thời gian";
                    dgvLog.Columns["ThoiGian"].FillWeight = 20;
                }
                if (dgvLog.Columns["MucDo"] != null)
                {
                    dgvLog.Columns["MucDo"].HeaderText = "Mức độ";
                    dgvLog.Columns["MucDo"].FillWeight = 12;
                }
                if (dgvLog.Columns["NoiDung"] != null)
                {
                    dgvLog.Columns["NoiDung"].HeaderText = "Nội dung kỹ thuật / Ngoại lệ";
                    dgvLog.Columns["NoiDung"].FillWeight = 68;
                }
            }

            dgvLog.CellFormatting -= DgvLog_CellFormatting;
            dgvLog.CellFormatting += DgvLog_CellFormatting;
        }

        private void DgvLog_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLog.Rows.Count) return;

            if (_loaiNhatKy == LoaiNhatKy.NhatKyHeThong && dgvLog.Columns[e.ColumnIndex].Name == "MucDo")
            {
                string val = e.Value?.ToString() ?? "";
                if (val == "ERROR")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvLog.Font, FontStyle.Bold);
                }
                else if (val == "WARN")
                {
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = new Font(dgvLog.Font, FontStyle.Bold);
                }
                else if (val == "INFO")
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
            }
        }
    }
}


using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace NPC.Client
{
    /// <summary>
    /// frmGoiMonClient - Gọi món tại máy trạm, tích hợp ErrorProvider (epSoLuong) kiểm tra tính hợp lệ số lượng món:
    /// lọc thực đơn -> chọn món -> nhập số lượng -> thêm vào giỏ -> Đặt món (tạo DonHang trạng thái Pending).
    /// </summary>
    public partial class frmGoiMonClient : Form
    {
        #region Cấu hình và biến toàn cục
        private const string ChuoiKetNoi = @"Data Source=.;Initial Catalog=NPCData;Integrated Security=True;Connect Timeout=5;";
        private const int SoLuongToiDa = 99;

        private readonly int _maMay;
        private readonly string _tenMay;
        private readonly long _maPhienChoi;

        private readonly DataTable _gioHang = new DataTable();   
        private bool _dongYenLang;                               

        private bool _coMonChon;
        private int _maMonChon;
        private string _tenMonChon = "";
        private decimal _donGiaChon;
        private int _tonKhoChon;
        #endregion

        #region Khởi tạo
        public frmGoiMonClient() : this(0, "MAY-01", 0) { }

        public frmGoiMonClient(int maMay, string tenMay, long maPhienChoi)
        {
            InitializeComponent();
            _maMay = maMay;
            _tenMay = tenMay;
            _maPhienChoi = maPhienChoi;
        }

        private void frmGoiMonClient_Load(object? sender, EventArgs e)
        {
            lblMayHienTai.Text = "Máy: " + _tenMay;
            TaoGioHang();
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
            if (!_dongYenLang && DialogResult != DialogResult.OK && _gioHang.Rows.Count > 0
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

        #region Nạp thực đơn
        private void NapDanhMuc()
        {
            DataTable dt = LayBang("SELECT MaDanhMuc, TenDanhMuc FROM dbo.DanhMuc ORDER BY TenDanhMuc");
            DataRow tatCa = dt.NewRow();
            tatCa["MaDanhMuc"] = 0;
            tatCa["TenDanhMuc"] = "-- Tất cả --";
            dt.Rows.InsertAt(tatCa, 0);

            cboDanhMuc.SelectedIndexChanged -= cboDanhMuc_SelectedIndexChanged;   
            cboDanhMuc.DisplayMember = "TenDanhMuc";
            cboDanhMuc.ValueMember = "MaDanhMuc";
            cboDanhMuc.DataSource = dt;
            cboDanhMuc.SelectedIndex = 0;
            cboDanhMuc.SelectedIndexChanged += cboDanhMuc_SelectedIndexChanged;
        }

        private void NapThucDon()
        {
            int maDanhMuc = cboDanhMuc.SelectedValue is int v ? v : 0;
            DataTable dt = LayBang(
                @"SELECT MaMon, TenMon, DonGia, SoLuongTon, HinhAnh
                  FROM dbo.MonAn
                  WHERE ConPhucVu = 1
                    AND (@MaDanhMuc = 0 OR MaDanhMuc = @MaDanhMuc)
                    AND TenMon LIKE @TuKhoa
                  ORDER BY TenMon",
                new SqlParameter("@MaDanhMuc", maDanhMuc),
                new SqlParameter("@TuKhoa", "%" + txtTimKiem.Text.Trim() + "%"));

            dgvThucDon.DataSource = dt;
            dgvThucDon.Columns["MaMon"].Visible = false;
            dgvThucDon.Columns["HinhAnh"].Visible = false;
            dgvThucDon.Columns["TenMon"].HeaderText = "Tên món";
            dgvThucDon.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvThucDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvThucDon.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvThucDon.Columns["SoLuongTon"].HeaderText = "Tồn";
            dgvThucDon.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            HienThiMonDangChon();
        }

        private void cboDanhMuc_SelectedIndexChanged(object? sender, EventArgs e) => NapThucDon();

        private void txtTimKiem_TextChanged(object? sender, EventArgs e) => NapThucDon();
        #endregion

        #region Chọn món
        private void dgvThucDon_SelectionChanged(object? sender, EventArgs e) => HienThiMonDangChon();

        private void HienThiMonDangChon()
        {
            if (dgvThucDon.CurrentRow?.DataBoundItem is not DataRowView drv)
            {
                _coMonChon = false;
                txtTenMon.Clear();
                txtDonGia.Clear();
                XoaAnh();
                epSoLuong.SetError(txtSoLuong, "");
                return;
            }

            DataRow r = drv.Row;
            _coMonChon = true;
            _maMonChon = Convert.ToInt32(r["MaMon"]);
            _tenMonChon = r["TenMon"].ToString() ?? "";
            _donGiaChon = Convert.ToDecimal(r["DonGia"]);
            _tonKhoChon = Convert.ToInt32(r["SoLuongTon"]);

            txtTenMon.Text = _tenMonChon;
            txtDonGia.Text = _donGiaChon.ToString("N0");
            txtSoLuong.Text = "1";
            HienThiAnh(r["HinhAnh"] as string);
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
                
            }
        }
        #endregion

        #region ErrorProvider: kiểm tra số lượng món
        private void txtSoLuong_TextChanged(object? sender, EventArgs e) => KiemTraSoLuong();

        private void txtSoLuong_Validating(object? sender, CancelEventArgs e) => KiemTraSoLuong();

        private bool KiemTraSoLuong()
        {
            string loi = "";
            string nhap = txtSoLuong.Text.Trim();
            int daCoTrongGio = _coMonChon ? SoLuongTrongGio(_maMonChon) : 0;

            if (nhap.Length == 0) loi = "Vui lòng nhập số lượng.";
            else if (!int.TryParse(nhap, out int sl)) loi = "Số lượng phải là số nguyên.";
            else if (sl <= 0) loi = "Số lượng phải lớn hơn 0.";
            else if (sl > SoLuongToiDa) loi = $"Mỗi lần gọi tối đa {SoLuongToiDa} phần.";
            else if (_coMonChon && sl + daCoTrongGio > _tonKhoChon)
                loi = _tonKhoChon - daCoTrongGio <= 0 ? "Món này đã hết hàng (hoặc đã gọi hết số còn lại)."
                                                      : $"Kho chỉ còn {_tonKhoChon - daCoTrongGio} phần.";

            epSoLuong.SetError(txtSoLuong, loi);
            return loi.Length == 0;
        }
        #endregion

        #region Giỏ hàng
        private void TaoGioHang()
        {
            _gioHang.Columns.Add("MaMon", typeof(int));
            _gioHang.Columns.Add("TenMon", typeof(string));
            _gioHang.Columns.Add("SoLuong", typeof(int));
            _gioHang.Columns.Add("DonGia", typeof(decimal));
            _gioHang.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");   

            dgvGioHang.DataSource = _gioHang;
            dgvGioHang.Columns["MaMon"].Visible = false;
            dgvGioHang.Columns["TenMon"].HeaderText = "Tên món";
            dgvGioHang.Columns["SoLuong"].HeaderText = "SL";
            dgvGioHang.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvGioHang.Columns["ThanhTien"].HeaderText = "Thành tiền";
            foreach (string cot in new[] { "SoLuong", "DonGia", "ThanhTien" })
                dgvGioHang.Columns[cot].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private int SoLuongTrongGio(int maMon)
        {
            object kq = _gioHang.Compute("SUM(SoLuong)", "MaMon = " + maMon);
            return kq == DBNull.Value ? 0 : Convert.ToInt32(kq);
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

            int soLuong = int.Parse(txtSoLuong.Text.Trim());
            DataRow[] daCo = _gioHang.Select("MaMon = " + _maMonChon);
            if (daCo.Length > 0)
                daCo[0]["SoLuong"] = Convert.ToInt32(daCo[0]["SoLuong"]) + soLuong;   
            else
                _gioHang.Rows.Add(_maMonChon, _tenMonChon, soLuong, _donGiaChon);

            CapNhatTongTien();
            txtSoLuong.Text = "1";
            KiemTraSoLuong();
        }

        private void btnXoaMon_Click(object? sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow?.DataBoundItem is DataRowView drv)
            {
                drv.Row.Delete();
                _gioHang.AcceptChanges();
                CapNhatTongTien();
                KiemTraSoLuong();
            }
        }

        private void CapNhatTongTien()
        {
            object kq = _gioHang.Compute("SUM(ThanhTien)", "");
            decimal tong = kq == DBNull.Value ? 0m : Convert.ToDecimal(kq);
            lblTongTien.Text = $"Tổng tiền: {tong:N0} đ";
            btnDatMon.Enabled = _gioHang.Rows.Count > 0;
            btnXoaMon.Enabled = _gioHang.Rows.Count > 0;
        }
        #endregion

        #region Đặt món
        private void btnDatMon_Click(object? sender, EventArgs e)
        {
            if (_gioHang.Rows.Count == 0) return;
            if (MessageBox.Show(this, $"Gửi đơn gồm {_gioHang.Rows.Count} món?", "Xác nhận đặt món",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                long maDonHang = TaoDonHang();
                MessageBox.Show(this, $"Đã gửi đơn #{maDonHang}. Nhân viên sẽ mang món đến cho bạn!", "Đặt món thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;            // form tự đóng
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không đặt được món:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try { NapThucDon(); } catch { }             
            }
        }

   
        private long TaoDonHang()
        {
            using (var conn = new SqlConnection(ChuoiKetNoi))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        long maDonHang;
                        using (var cmd = new SqlCommand(
                            @"INSERT INTO dbo.DonHang (MaPhienChoi, MaMay, TrangThai) VALUES (@Phien, @May, N'Pending');
                              SELECT CAST(SCOPE_IDENTITY() AS BIGINT);", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Phien", _maPhienChoi);
                            cmd.Parameters.AddWithValue("@May", _maMay);
                            maDonHang = Convert.ToInt64(cmd.ExecuteScalar());
                        }

                        foreach (DataRow dong in _gioHang.Rows)
                        {
                            using (var cmd = new SqlCommand(
                                @"INSERT INTO dbo.ChiTietDonHang (MaDonHang, MaMon, SoLuong, DonGia)
                                  SELECT @MaDonHang, MaMon, @SoLuong, DonGia
                                  FROM dbo.MonAn WHERE MaMon = @MaMon AND ConPhucVu = 1", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                cmd.Parameters.AddWithValue("@MaMon", Convert.ToInt32(dong["MaMon"]));
                                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(dong["SoLuong"]));
                                if (cmd.ExecuteNonQuery() != 1)
                                    throw new Exception($"Món \"{dong["TenMon"]}\" hiện không còn phục vụ.");
                            }
                        }

                        tran.Commit();
                        return maDonHang;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region Truy vấn CSDL
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
        #endregion
    }
}
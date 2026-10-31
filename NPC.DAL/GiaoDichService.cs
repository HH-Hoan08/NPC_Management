using System;
using System.Data;
using System.Data.SqlClient;
namespace NPC.DAL
{
    /// <summary>
    /// Lớp xử lý các giao dịch tài chính bọc SqlTransaction chuẩn ACID:
    ///   - TraMay()  : Thanh toán (tính tiền giờ + đồ ăn) và trả máy.
    ///   - NapTien() : Nạp tiền vào số dư hội viên.
    /// </summary>
    public static class GiaoDichService
    {
        #region Thanh toán và trả máy
        /// <summary>
        /// Thanh toán toàn bộ phiên chơi: tính tiền giờ + đồ ăn, trừ số dư hội viên (nếu đủ),
        /// đóng phiên, cập nhật trạng thái máy, ghi lịch sử giao dịch.
        /// Toàn bộ trong 1 SqlTransaction — nếu bất kỳ bước nào lỗi → ROLLBACK toàn bộ.
        /// </summary>
        /// <param name="maPhienChoi">Mã phiên chơi cần kết thúc.</param>
        /// <param name="maNhanVien">Mã nhân viên thu ngân xử lý.</param>
        /// <param name="hinhThucThanhToan">Ví dụ: "Tiền mặt", "Chuyển khoản VietQR"</param>
        /// <param name="tongTienThanhToan">Output: Tổng tiền thực tế cần thu.</param>
        /// <returns>true = thành công, false = thất bại.</returns>
        public static bool TraMay(
            long maPhienChoi,
            int maNhanVien,
            string hinhThucThanhToan,
            out decimal tongTienThanhToan)
        {
            tongTienThanhToan = 0m;

            using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    // Lấy và khóa dòng PhienChoi (UPDLOCK + ROWLOCK) và ngăn 2 người cùng thực hiện cùng 1 time
                    string sqlLockPhien =
                        @"SELECT pc.MaPhienChoi, pc.MaMay, pc.MaHoiVien,
                                 pc.ThoiGianBatDau, pc.GiaGioApDung, pc.TrangThaiPhien
                          FROM   dbo.PhienChoi AS pc WITH (UPDLOCK, ROWLOCK)
                          WHERE  pc.MaPhienChoi = @MaPhienChoi";

                    DataTable dtPhien = SQLHelper.ExecuteQuery(conn, tran, sqlLockPhien,P("@MaPhienChoi", maPhienChoi));
                    if (dtPhien.Rows.Count == 0)
                        throw new InvalidOperationException($"Phiên chơi #{maPhienChoi} không tồn tại.");

                    DataRow phien = dtPhien.Rows[0];
                    string trangThaiPhien = phien["TrangThaiPhien"].ToString()!;

                    if (trangThaiPhien != "Đang chơi")
                        throw new InvalidOperationException(
                            $"Phiên #{maPhienChoi} đã ở trạng thái '{trangThaiPhien}', không thể trả máy.");

                    int maMay = Convert.ToInt32(phien["MaMay"]);
                    int? maHoiVien = phien["MaHoiVien"] == DBNull.Value ? null : (int?)Convert.ToInt32(phien["MaHoiVien"]);
                    DateTime thoiGianBatDau = Convert.ToDateTime(phien["ThoiGianBatDau"]);
                    decimal giaGioApDung = Convert.ToDecimal(phien["GiaGioApDung"]);

                    // Tính tiền giờ
                    DateTime thoiGianKetThuc = DateTime.Now;
                    int tongPhutChoi = (int)Math.Ceiling((thoiGianKetThuc - thoiGianBatDau).TotalMinutes); // Làm tròn lên phút
                    decimal tienGio = Math.Round((decimal)(tongPhutChoi / 60.0) * giaGioApDung, 2);

                    // Tổng tiền đồ ăn (các đơn "Đã giao" trong phiên)
                    string sqlTienDoAn =
                        @"SELECT ISNULL(SUM(dh.TongTien), 0)
                          FROM   dbo.DonHang AS dh
                          WHERE  dh.MaPhienChoi = @MaPhienChoi
                            AND  dh.TrangThai   = N'Đã giao'";

                    object rawTienDoAn = SQLHelper.ExecuteScalar(conn, tran, sqlTienDoAn,
                        P("@MaPhienChoi", maPhienChoi));
                    decimal tienDoAn = rawTienDoAn == DBNull.Value ? 0m : Convert.ToDecimal(rawTienDoAn);

                    tongTienThanhToan = tienGio + tienDoAn;

                    // Nếu là hội viên -> khóa SoDu, kiểm tra đủ tiền, trừ số dư
                    if (maHoiVien.HasValue)
                    {
                        string sqlLockHoiVien =
                            @"SELECT SoDu FROM dbo.HoiVien WITH (UPDLOCK, ROWLOCK)
                              WHERE  MaHoiVien = @MaHoiVien";

                        object rawSoDu = SQLHelper.ExecuteScalar(conn, tran, sqlLockHoiVien,P("@MaHoiVien", maHoiVien.Value));

                        if (rawSoDu == null || rawSoDu == DBNull.Value) throw new InvalidOperationException("Không tìm thấy thông tin hội viên.");

                        decimal soDuHienTai = Convert.ToDecimal(rawSoDu);

                        if (soDuHienTai < tongTienThanhToan)
                            throw new InvalidOperationException(
                                $"Số dư hội viên ({soDuHienTai:N0} VND) không đủ " +
                                $"để thanh toán ({tongTienThanhToan:N0} VND).");

                        // Guard clause WHERE SoDu >= @SoTienTru chống race condition
                        string sqlTruSoDu =
                            @"UPDATE dbo.HoiVien
                              SET    SoDu             = SoDu - @SoTienTru,
                                     TongThoiGianChoi = TongThoiGianChoi + @TongPhut,
                                     TrangThai        = N'Hoạt động'
                              WHERE  MaHoiVien        = @MaHoiVien
                                AND  SoDu            >= @SoTienTru";

                        int rowsHV = SQLHelper.ExecuteNonQuery(conn, tran, sqlTruSoDu,
                            P("@SoTienTru", tongTienThanhToan),
                            P("@TongPhut",  tongPhutChoi),
                            P("@MaHoiVien", maHoiVien.Value));

                        // rowsHV = 0 → có race condition → ROLLBACK
                        if (rowsHV == 0)
                            throw new InvalidOperationException(
                                "Số dư hội viên vừa thay đổi trong lúc xử lý. Vui lòng thử lại.");
                    }

                    // Đóng PhienChoi → "Đã kết thúc"
                    string sqlDongPhien =
                        @"UPDATE dbo.PhienChoi
                          SET    ThoiGianKetThuc = @ThoiGianKetThuc,
                                 TongPhutChoi    = @TongPhut,
                                 TienGio         = @TienGio,
                                 TrangThaiPhien  = N'Đã kết thúc'
                          WHERE  MaPhienChoi     = @MaPhienChoi
                            AND  TrangThaiPhien  = N'Đang chơi'"; // Guard clause

                    int rowsPhienUpdate = SQLHelper.ExecuteNonQuery(conn, tran, sqlDongPhien,
                        P("@ThoiGianKetThuc", thoiGianKetThuc),
                        P("@TongPhut",        tongPhutChoi),
                        P("@TienGio",         tienGio),
                        P("@MaPhienChoi",     maPhienChoi));

                    if (rowsPhienUpdate == 0)
                        throw new InvalidOperationException(
                            "Phiên chơi đã bị thay đổi trạng thái bởi tiến trình khác. Vui lòng thử lại.");

                    // Cập nhật trạng thái máy → "Sẵn sàng"
                    SQLHelper.ExecuteNonQuery(conn, tran,
                        "UPDATE dbo.MayTram SET TrangThai = N'Sẵn sàng' WHERE MaMay = @MaMay",
                        P("@MaMay", maMay));

                    // Ghi lịch sử giao dịch
                    string ghiChu =
                        $"Thanh toán phiên #{maPhienChoi}: " +
                        $"Tiền giờ {tienGio:N0} VND + Đồ ăn {tienDoAn:N0} VND = {tongTienThanhToan:N0} VND.";

                    string sqlGiaoDich =
                        @"INSERT INTO dbo.GiaoDich
                            (MaHoiVien, MaNhanVien, SoTien, HinhThucThanhToan, NgayGiaoDich, GhiChu)
                          VALUES
                            (@MaHoiVien, @MaNhanVien, @SoTien, @HinhThuc, GETDATE(), @GhiChu)";

                    SQLHelper.ExecuteNonQuery(conn, tran, sqlGiaoDich,
                        P("@MaHoiVien",  (object?)maHoiVien ?? DBNull.Value),
                        P("@MaNhanVien", maNhanVien),
                        P("@SoTien",     tongTienThanhToan),
                        P("@HinhThuc",   hinhThucThanhToan),
                        P("@GhiChu",     ghiChu));

                    // Ghi log thành công → ghi vĩnh viễn (Durability)
                    tran.Commit();

                    Logger.LogBusiness("System", "Trả máy",
                        $"Phiên #{maPhienChoi} | Máy #{maMay} | {tongPhutChoi} phút | " +
                        $"Giờ {tienGio:N0} + Đồ ăn {tienDoAn:N0} = Tổng {tongTienThanhToan:N0} VND | {hinhThucThanhToan}");

                    return true;
                }
                catch (InvalidOperationException ex)
                {
                    // Lỗi nghiệp vụ → ROLLBACK, ném lại để UI hiển thị MessageBox
                    SafeRollback(tran);
                    Logger.LogWarning($"[TraMay] Lỗi nghiệp vụ phiên #{maPhienChoi}: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    // Lỗi kỹ thuật (SQL, network...) → ROLLBACK, ghi đầy đủ vào app.log
                    SafeRollback(tran);
                    Logger.LogError($"[TraMay] Lỗi kỹ thuật phiên #{maPhienChoi}:", ex);
                    throw new Exception("Lỗi hệ thống khi trả máy. Vui lòng thử lại hoặc liên hệ kỹ thuật.", ex);
                }
            }
        }
        #endregion

        #region Nạp tiền vào hội viên
        /// <summary>
        /// Nạp tiền vào số dư hội viên và ghi lịch sử giao dịch — chuẩn ACID.
        /// Cả 2 thao tác UPDATE HoiVien + INSERT GiaoDich nằm trong 1 Transaction.
        /// </summary>
        /// <param name="maHoiVien">Mã hội viên được nạp tiền.</param>
        /// <param name="maNhanVien">Mã nhân viên thu ngân thực hiện.</param>
        /// <param name="soTienNap">Số tiền nạp (phải > 0).</param>
        /// <param name="hinhThucThanhToan">Ví dụ: "Tiền mặt", "Chuyển khoản VietQR"</param>
        /// <param name="ghiChu">Ghi chú thêm (tùy chọn).</param>
        /// <returns>Số dư mới sau khi nạp.</returns>
        public static decimal NapTien(
            int maHoiVien,
            int maNhanVien,
            decimal soTienNap,
            string hinhThucThanhToan,
            string ghiChu = "")
        {
            // Kiểm tra đầu vào trước khi mở kết nối (Fail Fast)
            if (soTienNap <= 0)
                throw new ArgumentException(
                    $"Số tiền nạp phải lớn hơn 0. Giá trị nhận được: {soTienNap:N0} VND");

            using (SqlConnection conn = new SqlConnection(NPCData.ConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    // Lấy và khóa dòng HoiVien (UPDLOCK)
                    string sqlLockHoiVien =
                        @"SELECT MaHoiVien, HoTen, SoDu, TrangThai
                          FROM   dbo.HoiVien WITH (UPDLOCK, ROWLOCK)
                          WHERE  MaHoiVien = @MaHoiVien";

                    DataTable dtHV = SQLHelper.ExecuteQuery(conn, tran, sqlLockHoiVien,
                        P("@MaHoiVien", maHoiVien));

                    if (dtHV.Rows.Count == 0)
                        throw new InvalidOperationException($"Hội viên #{maHoiVien} không tồn tại.");

                    string trangThaiHV = dtHV.Rows[0]["TrangThai"].ToString()!;
                    string hoTen       = dtHV.Rows[0]["HoTen"].ToString()!;
                    decimal soDuTruoc  = Convert.ToDecimal(dtHV.Rows[0]["SoDu"]);

                    if (trangThaiHV == "Bị khóa")
                        throw new InvalidOperationException(
                            $"Tài khoản hội viên '{hoTen}' đang bị khóa. Không thể nạp tiền.");

                    // Cộng số dư (Atomic update)
                    SQLHelper.ExecuteNonQuery(conn, tran,
                        "UPDATE dbo.HoiVien SET SoDu = SoDu + @SoTienNap WHERE MaHoiVien = @MaHoiVien",
                        P("@SoTienNap", soTienNap),
                        P("@MaHoiVien", maHoiVien));

                    // Đọc số dư mới (trong cùng transaction)
                    object rawSoDuMoi = SQLHelper.ExecuteScalar(conn, tran,
                        "SELECT SoDu FROM dbo.HoiVien WHERE MaHoiVien = @MaHoiVien",
                        P("@MaHoiVien", maHoiVien))!;
                    decimal soDuMoi = Convert.ToDecimal(rawSoDuMoi);

                    // Ghi lịch sử giao dịch nạp tiền
                    if (string.IsNullOrWhiteSpace(ghiChu))
                        ghiChu = $"Nạp tiền cho '{hoTen}': " +
                                 $"{soDuTruoc:N0} → {soDuMoi:N0} VND (+{soTienNap:N0} VND).";

                    string sqlGiaoDich =
                        @"INSERT INTO dbo.GiaoDich
                            (MaHoiVien, MaNhanVien, SoTien, HinhThucThanhToan, NgayGiaoDich, GhiChu)
                          VALUES
                            (@MaHoiVien, @MaNhanVien, @SoTien, @HinhThuc, GETDATE(), @GhiChu)";

                    SQLHelper.ExecuteNonQuery(conn, tran, sqlGiaoDich,
                        P("@MaHoiVien",  maHoiVien),
                        P("@MaNhanVien", maNhanVien),
                        P("@SoTien",     soTienNap),
                        P("@HinhThuc",   hinhThucThanhToan),
                        P("@GhiChu",     ghiChu));

                    // ── COMMIT
                    tran.Commit();

                    Logger.LogBusiness("System", "Nạp tiền",
                        $"Hội viên #{maHoiVien} '{hoTen}' | +{soTienNap:N0} VND | " +
                        $"Số dư: {soDuTruoc:N0} → {soDuMoi:N0} VND | {hinhThucThanhToan}");

                    return soDuMoi;
                }
                catch (InvalidOperationException ex)
                {
                    SafeRollback(tran);
                    Logger.LogWarning($"[NapTien] Lỗi nghiệp vụ hội viên #{maHoiVien}: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    SafeRollback(tran);
                    Logger.LogError($"[NapTien] Lỗi kỹ thuật hội viên #{maHoiVien}:", ex);
                    throw new Exception("Lỗi hệ thống khi nạp tiền. Vui lòng thử lại.", ex);
                }
            }
        }     
        #endregion

        // =========================================================================
        // PRIVATE HELPERS
        // =========================================================================

        /// <summary>Tạo SqlParameter ngắn gọn.</summary>
        private static SqlParameter P(string name, object? value)
            => new SqlParameter(name, value ?? DBNull.Value);

        /// <summary>ROLLBACK an toàn — không ném thêm ngoại lệ nếu transaction đã đóng.</summary>
        private static void SafeRollback(SqlTransaction tran)
        {
            try { tran.Rollback(); }
            catch (Exception ex) { Logger.LogError("[SafeRollback] Rollback thất bại:", ex); }
        }
    }
}

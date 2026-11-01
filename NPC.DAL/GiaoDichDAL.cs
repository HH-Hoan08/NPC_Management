using System;
using System.Data;
using System.Data.SqlClient;
using NPC.DTO;

namespace NPC.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu (DAL) cho các thao tác CSDL liên quan đến giao dịch, phiên chơi và hội viên.
    /// Chứa các hàm truy vấn thuần (SELECT, UPDATE, INSERT) sử dụng SQLHelper với kết nối và giao dịch được quản lý từ bên ngoài.
    /// </summary>
    public static class GiaoDichDAL
    {
        private static SqlParameter P(string name, object? value)
            => new SqlParameter(name, value ?? DBNull.Value);

        /// <summary>
        /// Khóa dòng dữ liệu phiên chơi (UPDLOCK, ROWLOCK) để chống xung đột đồng thời và lấy thông tin phiên.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maPhienChoi">Mã phiên chơi cần truy vấn.</param>
        /// <returns>Đối tượng PhienChoiDTO chứa thông tin phiên, hoặc null nếu không tồn tại.</returns>
        public static PhienChoiDTO? LockVaLayPhienChoi(SqlConnection conn, SqlTransaction tran, long maPhienChoi)
        {
            string sql =
                @"SELECT pc.MaPhienChoi, pc.MaMay, pc.MaHoiVien, pc.ThoiGianBatDau, pc.GiaGioApDung, pc.TrangThaiPhien
                FROM dbo.PhienChoi AS pc WITH (UPDLOCK, ROWLOCK)
                WHERE pc.MaPhienChoi = @MaPhienChoi";

            DataTable dt = SQLHelper.ExecuteQuery(conn, tran, sql, P("@MaPhienChoi", maPhienChoi));
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new PhienChoiDTO
            {
                MaPhienChoi = Convert.ToInt64(r["MaPhienChoi"]),
                MaMay = Convert.ToInt32(r["MaMay"]),
                MaHoiVien = r["MaHoiVien"] == DBNull.Value ? null : (int?)Convert.ToInt32(r["MaHoiVien"]),
                ThoiGianBatDau = Convert.ToDateTime(r["ThoiGianBatDau"]),
                GiaGioApDung = Convert.ToDecimal(r["GiaGioApDung"]),
                TrangThaiPhien = r["TrangThaiPhien"].ToString()!
            };
        }

        /// <summary>
        /// Khóa dòng dữ liệu hội viên (UPDLOCK, ROWLOCK) và lấy số dư hiện tại.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maHoiVien">Mã hội viên cần truy vấn.</param>
        /// <returns>Số dư hiện tại, hoặc null nếu hội viên không tồn tại.</returns>
        public static decimal? LockVaLaySoDuHoiVien(SqlConnection conn, SqlTransaction tran, int maHoiVien)
        {
            string sql =
                @"SELECT SoDu FROM dbo.HoiVien WITH (UPDLOCK, ROWLOCK)
                WHERE MaHoiVien = @MaHoiVien";

            object raw = SQLHelper.ExecuteScalar(conn, tran, sql, P("@MaHoiVien", maHoiVien));
            if (raw == null || raw == DBNull.Value) return null;
            return Convert.ToDecimal(raw);
        }

        /// <summary>
        /// Tính tổng tiền đồ ăn của các đơn hàng có trạng thái 'Đã giao' trong một phiên chơi.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maPhienChoi">Mã phiên chơi cần thống kê.</param>
        /// <returns>Tổng tiền đồ ăn dịch vụ phát sinh trong phiên.</returns>
        public static decimal LayTienDoAnPhien(SqlConnection conn, SqlTransaction tran, long maPhienChoi)
        {
            string sql =
                @"SELECT ISNULL(SUM(dh.TongTien), 0)
                FROM dbo.DonHang AS dh
                WHERE dh.MaPhienChoi = @MaPhienChoi
                AND dh.TrangThai = N'Đã giao'";

            object raw = SQLHelper.ExecuteScalar(conn, tran, sql, P("@MaPhienChoi", maPhienChoi));
            return raw == DBNull.Value ? 0m : Convert.ToDecimal(raw);
        }

        /// <summary>
        /// Trừ số dư tài khoản hội viên và cộng tích lũy tổng thời gian chơi.
        /// Có điều kiện chặn: chỉ trừ nếu số dư hiện tại >= số tiền cần trừ.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maHoiVien">Mã hội viên.</param>
        /// <param name="soTienTru">Số tiền cần khấu trừ vào tài khoản.</param>
        /// <param name="tongPhut">Số phút chơi phát sinh cần cộng thêm.</param>
        /// <returns>Số dòng bị ảnh hưởng (1 nếu thành công, 0 nếu không đủ số dư).</returns>
        public static int TruSoDuHoiVien(SqlConnection conn, SqlTransaction tran,
            int maHoiVien, decimal soTienTru, int tongPhut)
        {
            string sql =
                @"UPDATE dbo.HoiVien
                SET SoDu = SoDu - @SoTienTru,
                    TongThoiGianChoi = TongThoiGianChoi + @TongPhut, TrangThai = N'Hoạt động'
                WHERE MaHoiVien = @MaHoiVien
                AND SoDu >= @SoTienTru";

            return SQLHelper.ExecuteNonQuery(conn, tran, sql,
                P("@SoTienTru", soTienTru),
                P("@TongPhut",  tongPhut),
                P("@MaHoiVien", maHoiVien));
        }

        /// <summary>
        /// Đóng phiên chơi: cập nhật thời gian kết thúc, tổng phút chơi, tiền giờ và trạng thái sang 'Đã kết thúc'.
        /// Có điều kiện chặn: chỉ cập nhật nếu phiên đang có trạng thái 'Đang chơi'.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maPhienChoi">Mã phiên chơi cần đóng.</param>
        /// <param name="thoiGianKetThuc">Thời điểm kết thúc.</param>
        /// <param name="tongPhut">Tổng số phút chơi thực tế.</param>
        /// <param name="tienGio">Tổng tiền giờ phát sinh.</param>
        /// <returns>Số dòng bị ảnh hưởng (1 nếu thành công, 0 nếu phiên đã bị đóng trước đó).</returns>
        public static int DongPhienChoi(SqlConnection conn, SqlTransaction tran,
            long maPhienChoi, DateTime thoiGianKetThuc, int tongPhut, decimal tienGio)
        {
            string sql =
                @"UPDATE dbo.PhienChoi
                SET ThoiGianKetThuc = @ThoiGianKetThuc,
                    TongPhutChoi = @TongPhut,
                    TienGio = @TienGio,
                    TrangThaiPhien = N'Đã kết thúc'
                WHERE MaPhienChoi = @MaPhienChoi
                AND TrangThaiPhien = N'Đang chơi'";

            return SQLHelper.ExecuteNonQuery(conn, tran, sql,
                P("@ThoiGianKetThuc", thoiGianKetThuc),
                P("@TongPhut", tongPhut),
                P("@TienGio", tienGio),
                P("@MaPhienChoi", maPhienChoi));
        }

        /// <summary>
        /// Cập nhật trạng thái máy trạm sang 'Sẵn sàng' sau khi phiên kết thúc.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maMay">Mã máy trạm cần giải phóng.</param>
        public static void CapNhatTrangThaiMay(SqlConnection conn, SqlTransaction tran, int maMay)
        {
            SQLHelper.ExecuteNonQuery(conn, tran,
                "UPDATE dbo.MayTram SET TrangThai = N'Sẵn sàng' WHERE MaMay = @MaMay",
                P("@MaMay", maMay));
        }

        /// <summary>
        /// Ghi nhận một giao dịch mới vào bảng lịch sử giao dịch (GiaoDich).
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maHoiVien">Mã hội viên (null nếu là khách vãng lai).</param>
        /// <param name="maNhanVien">Mã nhân viên thực hiện thu ngân.</param>
        /// <param name="soTien">Số tiền giao dịch.</param>
        /// <param name="hinhThuc">Hình thức thanh toán (Tiền mặt, Chuyển khoản, v.v.).</param>
        /// <param name="ghiChu">Ghi chú diễn giải nội dung giao dịch.</param>
        public static void GhiGiaoDich(SqlConnection conn, SqlTransaction tran,
            int? maHoiVien, int maNhanVien, decimal soTien, string hinhThuc, string ghiChu)
        {
            string sql =
                @"INSERT INTO dbo.GiaoDich
                    (MaHoiVien, MaNhanVien, SoTien, HinhThucThanhToan, NgayGiaoDich, GhiChu)
                VALUES
                    (@MaHoiVien, @MaNhanVien, @SoTien, @HinhThuc, GETDATE(), @GhiChu)";

            SQLHelper.ExecuteNonQuery(conn, tran, sql,
                P("@MaHoiVien",  (object?)maHoiVien ?? DBNull.Value),
                P("@MaNhanVien", maNhanVien),
                P("@SoTien", soTien),
                P("@HinhThuc", hinhThuc),
                P("@GhiChu", ghiChu));
        }

        /// <summary>
        /// Khóa dòng dữ liệu hội viên (UPDLOCK, ROWLOCK) và lấy họ tên, số dư, trạng thái phục vụ việc nạp tiền.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maHoiVien">Mã hội viên cần nạp tiền.</param>
        /// <returns>Tuple gồm (HoTen, SoDu, TrangThai).</returns>
        /// <exception cref="InvalidOperationException">Ném ngoại lệ nếu không tìm thấy hội viên.</exception>
        public static (string HoTen, decimal SoDu, string TrangThai) LockVaLayHoiVien(
            SqlConnection conn, SqlTransaction tran, int maHoiVien)
        {
            string sql =
                @"SELECT HoTen, SoDu, TrangThai
                  FROM dbo.HoiVien WITH (UPDLOCK, ROWLOCK)
                  WHERE MaHoiVien = @MaHoiVien";

            DataTable dt = SQLHelper.ExecuteQuery(conn, tran, sql, P("@MaHoiVien", maHoiVien));
            if (dt.Rows.Count == 0)
                throw new InvalidOperationException($"Hội viên #{maHoiVien} không tồn tại.");

            DataRow r = dt.Rows[0];
            return (r["HoTen"].ToString()!, Convert.ToDecimal(r["SoDu"]), r["TrangThai"].ToString()!);
        }

        /// <summary>
        /// Cộng thêm tiền vào số dư của hội viên.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maHoiVien">Mã hội viên được nạp.</param>
        /// <param name="soTienNap">Số tiền nạp cần cộng vào số dư.</param>
        public static void CongSoDuHoiVien(SqlConnection conn, SqlTransaction tran,
            int maHoiVien, decimal soTienNap)
        {
            SQLHelper.ExecuteNonQuery(conn, tran,
                "UPDATE dbo.HoiVien SET SoDu = SoDu + @SoTienNap WHERE MaHoiVien = @MaHoiVien",
                P("@SoTienNap", soTienNap),
                P("@MaHoiVien", maHoiVien));
        }

        /// <summary>
        /// Lấy số dư hiện tại của hội viên từ CSDL.
        /// </summary>
        /// <param name="conn">Kết nối CSDL.</param>
        /// <param name="tran">Transaction đang thực thi.</param>
        /// <param name="maHoiVien">Mã hội viên cần tra cứu.</param>
        /// <returns>Số dư hiện tại.</returns>
        public static decimal LaySoDuHoiVien(SqlConnection conn, SqlTransaction tran, int maHoiVien)
        {
            object raw = SQLHelper.ExecuteScalar(conn, tran,
                "SELECT SoDu FROM dbo.HoiVien WHERE MaHoiVien = @MaHoiVien",
                P("@MaHoiVien", maHoiVien))!;
            return Convert.ToDecimal(raw);
        }

        /// <summary>
        /// Khởi tạo và trả về một kết nối SqlConnection mới dựa trên chuỗi kết nối của hệ thống.
        /// </summary>
        /// <returns>Đối tượng SqlConnection.</returns>
        public static SqlConnection MoKetNoi() => new SqlConnection(NPCData.ConnectionString);
    }
}

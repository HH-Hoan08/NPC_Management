using System;
using System.Collections.Generic;
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
        /// </summary>
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
        /// </summary>
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
        public static void CapNhatTrangThaiMay(SqlConnection conn, SqlTransaction tran, int maMay)
        {
            SQLHelper.ExecuteNonQuery(conn, tran,
                "UPDATE dbo.MayTram SET TrangThai = N'Sẵn sàng' WHERE MaMay = @MaMay",
                P("@MaMay", maMay));
        }

        /// <summary>
        /// Ghi nhận một giao dịch mới vào bảng lịch sử giao dịch (GiaoDich).
        /// </summary>
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
        public static SqlConnection MoKetNoi() => new SqlConnection(NPCData.ConnectionString);

        // ─── YeuCauNapTien ──────────────────────────────────────────────────────────

        /// <summary>
        /// Ghi yêu cầu nạp tiền từ Client vào DB với trạng thái "ChoDuyet".
        /// Trả về MaYeuCau vừa tạo.
        /// </summary>
        public static int GuiYeuCauNapTien(int maHoiVien, int maMay, string tenMay,
            decimal soTienNap, string hinhThuc)
        {
            string sql = @"
                INSERT INTO dbo.YeuCauNapTien
                    (MaHoiVien, MaMay, TenMay, SoTienNap, HinhThucThanhToan, TrangThai, ThoiGianGui)
                VALUES
                    (@MaHoiVien, @MaMay, @TenMay, @SoTienNap, @HinhThuc, N'ChoDuyet', GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using SqlConnection conn = MoKetNoi();
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaHoiVien", maHoiVien);
            cmd.Parameters.AddWithValue("@MaMay", maMay);
            cmd.Parameters.AddWithValue("@TenMay", tenMay);
            cmd.Parameters.AddWithValue("@SoTienNap", soTienNap);
            cmd.Parameters.AddWithValue("@HinhThuc", hinhThuc);
            object? result = cmd.ExecuteScalar();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Lấy trạng thái hiện tại của 1 yêu cầu nạp tiền theo MaYeuCau.
        /// Trả về "ChoDuyet" | "DaDuyet" | "TuChoi" | "" nếu không tìm thấy.
        /// </summary>
        public static string LayTrangThaiYeuCau(int maYeuCau)
        {
            string sql = "SELECT TrangThai FROM dbo.YeuCauNapTien WHERE MaYeuCau = @Ma";
            object? raw = SQLHelper.ExecuteScalar(sql, new SqlParameter("@Ma", maYeuCau));
            return raw != null && raw != DBNull.Value ? raw.ToString()! : string.Empty;
        }

        /// <summary>
        /// Lấy danh sách tất cả yêu cầu nạp tiền đang ở trạng thái "ChoDuyet".
        /// Server sẽ poll list này để hiển thị xác nhận.
        /// </summary>
        public static List<YeuCauNapTienDTO> LayYeuCauChoDuyet()
        {
            string sql = @"
                SELECT y.MaYeuCau, y.MaHoiVien, y.MaMay, y.TenMay,
                       y.SoTienNap, y.HinhThucThanhToan, y.TrangThai, y.ThoiGianGui,
                       h.HoTen
                FROM dbo.YeuCauNapTien y
                LEFT JOIN dbo.HoiVien h ON h.MaHoiVien = y.MaHoiVien
                WHERE y.TrangThai = N'ChoDuyet'
                ORDER BY y.ThoiGianGui ASC";

            DataTable dt = SQLHelper.ExecuteQuery(sql);
            var list = new List<YeuCauNapTienDTO>();
            if (dt == null) return list;
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new YeuCauNapTienDTO
                {
                    MaYeuCau          = Convert.ToInt32(r["MaYeuCau"]),
                    MaHoiVien         = Convert.ToInt32(r["MaHoiVien"]),
                    MaMay             = Convert.ToInt32(r["MaMay"]),
                    TenMay            = r["TenMay"].ToString()!,
                    SoTienNap         = Convert.ToDecimal(r["SoTienNap"]),
                    HinhThucThanhToan = r["HinhThucThanhToan"].ToString()!,
                    TrangThai         = r["TrangThai"].ToString()!,
                    ThoiGianGui       = Convert.ToDateTime(r["ThoiGianGui"]),
                    // GhiChu tạm dùng để truyền HoTen sang Server UI
                    GhiChu            = r["HoTen"] == DBNull.Value ? null : r["HoTen"].ToString()
                });
            }
            return list;
        }

        /// <summary>
        /// Server cập nhật trạng thái yêu cầu thành "DaDuyet" hoặc "TuChoi".
        /// Phải gọi bên trong transaction đang mở.
        /// </summary>
        public static void CapNhatTrangThaiYeuCau(SqlConnection conn, SqlTransaction tran,
            int maYeuCau, string trangThaiMoi)
        {
            SQLHelper.ExecuteNonQuery(conn, tran,
                @"UPDATE dbo.YeuCauNapTien
                  SET TrangThai = @TrangThai, ThoiGianXuLy = GETDATE()
                  WHERE MaYeuCau = @Ma",
                P("@TrangThai", trangThaiMoi),
                P("@Ma", maYeuCau));
        }
    }
}

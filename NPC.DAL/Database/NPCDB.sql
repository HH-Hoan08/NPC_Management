-- ======================================================================================
-- ĐỒ ÁN LẬP TRÌNH WINDOWS: HỆ THỐNG QUẢN LÝ QUÁN NÉT (NPC MANAGEMENT)
-- CƠ SỞ DỮ LIỆU CHUẨN 3NF: NPCData
-- CHUẨN HÓA 100% TIẾNG VIỆT: TÊN BẢNG, TÊN CỘT, DỮ LIỆU TRẠNG THÁI & HỆ THỐNG VIEWS
-- Phụ trách thiết kế & Cài đặt: Phú
-- Tương thích: Microsoft SQL Server 2019/2022 & SQL Server LocalDB
-- ======================================================================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'NPCData')
BEGIN
    CREATE DATABASE NPCData;
END
GO

USE NPCData;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- ======================================================================================
-- 1. XÓA CÁC ĐỐI TƯỢNG CŨ NẾU ĐÃ TỒN TẠI (ĐẢM BẢO CHẠY LẠI KHÔNG BỊ XUNG ĐỘT)
-- ======================================================================================

-- Xóa Views tiếng Việt
IF OBJECT_ID('dbo.v_GiaoDich', 'V') IS NOT NULL DROP VIEW dbo.v_GiaoDich;
IF OBJECT_ID('dbo.v_ChiTietDonHang', 'V') IS NOT NULL DROP VIEW dbo.v_ChiTietDonHang;
IF OBJECT_ID('dbo.v_DonHang', 'V') IS NOT NULL DROP VIEW dbo.v_DonHang;
IF OBJECT_ID('dbo.v_MonAn', 'V') IS NOT NULL DROP VIEW dbo.v_MonAn;
IF OBJECT_ID('dbo.v_PhienChoi', 'V') IS NOT NULL DROP VIEW dbo.v_PhienChoi;
IF OBJECT_ID('dbo.v_MayTram', 'V') IS NOT NULL DROP VIEW dbo.v_MayTram;
IF OBJECT_ID('dbo.v_HoiVien', 'V') IS NOT NULL DROP VIEW dbo.v_HoiVien;
IF OBJECT_ID('dbo.v_NhanVien', 'V') IS NOT NULL DROP VIEW dbo.v_NhanVien;

-- Xóa Views tương thích tiếng Anh (nếu có)
IF OBJECT_ID('dbo.Transactions', 'V') IS NOT NULL DROP VIEW dbo.Transactions;
IF OBJECT_ID('dbo.OrderDetails', 'V') IS NOT NULL DROP VIEW dbo.OrderDetails;
IF OBJECT_ID('dbo.Orders', 'V') IS NOT NULL DROP VIEW dbo.Orders;
IF OBJECT_ID('dbo.Products', 'V') IS NOT NULL DROP VIEW dbo.Products;
IF OBJECT_ID('dbo.Categories', 'V') IS NOT NULL DROP VIEW dbo.Categories;
IF OBJECT_ID('dbo.Sessions', 'V') IS NOT NULL DROP VIEW dbo.Sessions;
IF OBJECT_ID('dbo.Computers', 'V') IS NOT NULL DROP VIEW dbo.Computers;
IF OBJECT_ID('dbo.ComputerRooms', 'V') IS NOT NULL DROP VIEW dbo.ComputerRooms;
IF OBJECT_ID('dbo.Members', 'V') IS NOT NULL DROP VIEW dbo.Members;
IF OBJECT_ID('dbo.Employees', 'V') IS NOT NULL DROP VIEW dbo.Employees;
IF OBJECT_ID('dbo.Roles', 'V') IS NOT NULL DROP VIEW dbo.Roles;
GO

-- Xóa Triggers
IF OBJECT_ID('dbo.trg_TruTonKhoKhiGiaoHang', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_TruTonKhoKhiGiaoHang;
IF OBJECT_ID('dbo.trg_CapNhatTongTienDonHang', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_CapNhatTongTienDonHang;
IF OBJECT_ID('dbo.trg_ChongXuatAmKho', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_ChongXuatAmKho;
IF OBJECT_ID('dbo.trg_KiemTraTrangThaiMay', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_KiemTraTrangThaiMay;
IF OBJECT_ID('dbo.trg_UpdateStockAfterOrder', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_UpdateStockAfterOrder;
IF OBJECT_ID('dbo.trg_AutoUpdateTotalAmount', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_AutoUpdateTotalAmount;
IF OBJECT_ID('dbo.trg_PreventNegativeStock', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_PreventNegativeStock;
IF OBJECT_ID('dbo.trg_CheckComputerStatus', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_CheckComputerStatus;
GO

-- Xóa Stored Procedures
IF OBJECT_ID('dbo.sp_BaoCaoDoanhThu', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_BaoCaoDoanhThu;
IF OBJECT_ID('dbo.sp_NapTienHoiVien', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_NapTienHoiVien;
IF OBJECT_ID('dbo.sp_KetThucPhienChoi', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_KetThucPhienChoi;
IF OBJECT_ID('dbo.sp_MoPhienChoi', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_MoPhienChoi;
IF OBJECT_ID('dbo.sp_GetRevenueReport', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetRevenueReport;
IF OBJECT_ID('dbo.sp_TopupMemberBalance', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_TopupMemberBalance;
IF OBJECT_ID('dbo.sp_EndSession', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_EndSession;
IF OBJECT_ID('dbo.sp_StartSession', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_StartSession;
GO

-- Xóa Functions
IF OBJECT_ID('dbo.fn_TinhTienGio', 'FN') IS NOT NULL DROP FUNCTION dbo.fn_TinhTienGio;
IF OBJECT_ID('dbo.fn_CalculateSessionCost', 'FN') IS NOT NULL DROP FUNCTION dbo.fn_CalculateSessionCost;
GO

-- Xóa Bảng Tiếng Việt (theo thứ tự khóa ngoại ngược)
IF OBJECT_ID('dbo.GiaoDich', 'U') IS NOT NULL DROP TABLE dbo.GiaoDich;
IF OBJECT_ID('dbo.ChiTietDonHang', 'U') IS NOT NULL DROP TABLE dbo.ChiTietDonHang;
IF OBJECT_ID('dbo.DonHang', 'U') IS NOT NULL DROP TABLE dbo.DonHang;
IF OBJECT_ID('dbo.MonAn', 'U') IS NOT NULL DROP TABLE dbo.MonAn;
IF OBJECT_ID('dbo.DanhMuc', 'U') IS NOT NULL DROP TABLE dbo.DanhMuc;
IF OBJECT_ID('dbo.PhienChoi', 'U') IS NOT NULL DROP TABLE dbo.PhienChoi;
IF OBJECT_ID('dbo.MayTram', 'U') IS NOT NULL DROP TABLE dbo.MayTram;
IF OBJECT_ID('dbo.KhuVucPhong', 'U') IS NOT NULL DROP TABLE dbo.KhuVucPhong;
IF OBJECT_ID('dbo.HoiVien', 'U') IS NOT NULL DROP TABLE dbo.HoiVien;
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL DROP TABLE dbo.NhanVien;
IF OBJECT_ID('dbo.VaiTro', 'U') IS NOT NULL DROP TABLE dbo.VaiTro;
GO

-- ======================================================================================
-- 2. ĐỊNH NGHĨA 11 BẢNG THỰC THỂ TIẾNG VIỆT ĐẠT CHUẨN 3NF & RÀNG BUỘC TOÀN VẸN
-- ======================================================================================

-- 2.1 Bảng VaiTro (Vai trò người dùng: Admin, Thu ngân, Kỹ thuật)
CREATE TABLE dbo.VaiTro
(
    MaVaiTro INT IDENTITY(1,1) NOT NULL,
    TenVaiTro NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_VaiTro PRIMARY KEY CLUSTERED (MaVaiTro),
    CONSTRAINT UQ_VaiTro_TenVaiTro UNIQUE (TenVaiTro)
);
GO

-- 2.2 Bảng NhanVien (Nhân viên / Tài khoản quản trị quầy Server)
CREATE TABLE dbo.NhanVien
(
    MaNhanVien INT IDENTITY(1,1) NOT NULL,
    TenDangNhap VARCHAR(50) NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NULL,
    MaVaiTro INT NOT NULL,
    TrangThai BIT NOT NULL CONSTRAINT DF_NhanVien_TrangThai DEFAULT 1,
    CONSTRAINT PK_NhanVien PRIMARY KEY CLUSTERED (MaNhanVien),
    CONSTRAINT UQ_NhanVien_TenDangNhap UNIQUE (TenDangNhap),
    CONSTRAINT FK_NhanVien_VaiTro FOREIGN KEY (MaVaiTro) REFERENCES dbo.VaiTro(MaVaiTro)
);
GO

-- 2.3 Bảng HoiVien (Khách hàng hội viên quán nét)
-- Trạng thái: N'Hoạt động', N'Đang chơi', N'Bị khóa'
CREATE TABLE dbo.HoiVien
(
    MaHoiVien INT IDENTITY(1,1) NOT NULL,
    TenDangNhap VARCHAR(50) NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SoDu DECIMAL(18,2) NOT NULL CONSTRAINT DF_HoiVien_SoDu DEFAULT 0,
    TongThoiGianChoi INT NOT NULL CONSTRAINT DF_HoiVien_TongThoiGianChoi DEFAULT 0,
    NgayTao DATETIME NOT NULL CONSTRAINT DF_HoiVien_NgayTao DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_HoiVien_TrangThai DEFAULT N'Hoạt động',
    CONSTRAINT PK_HoiVien PRIMARY KEY CLUSTERED (MaHoiVien),
    CONSTRAINT UQ_HoiVien_TenDangNhap UNIQUE (TenDangNhap),
    CONSTRAINT CK_HoiVien_SoDu CHECK (SoDu >= 0),
    CONSTRAINT CK_HoiVien_TongThoiGianChoi CHECK (TongThoiGianChoi >= 0)
);
GO

-- 2.4 Bảng KhuVucPhong (Khu vực / Phòng máy quyết định đơn giá giờ)
CREATE TABLE dbo.KhuVucPhong
(
    MaPhong INT IDENTITY(1,1) NOT NULL,
    TenPhong NVARCHAR(50) NOT NULL,
    DonGiaGio DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_KhuVucPhong PRIMARY KEY CLUSTERED (MaPhong),
    CONSTRAINT UQ_KhuVucPhong_TenPhong UNIQUE (TenPhong),
    CONSTRAINT CK_KhuVucPhong_DonGiaGio CHECK (DonGiaGio >= 0)
);
GO

-- 2.5 Bảng MayTram (Máy trạm phòng nét)
-- Trạng thái full Tiếng Việt: N'Sẵn sàng' (Xanh), N'Đang dùng' (Đỏ), N'Bảo trì' (Vàng), N'Tắt máy' (Xám)
CREATE TABLE dbo.MayTram
(
    MaMay INT IDENTITY(1,1) NOT NULL,
    TenMay NVARCHAR(50) NOT NULL,
    DiaChiIP VARCHAR(45) NULL,
    DiaChiMAC VARCHAR(50) NULL,
    MaPhong INT NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_MayTram_TrangThai DEFAULT N'Tắt máy',
    CONSTRAINT PK_MayTram PRIMARY KEY CLUSTERED (MaMay),
    CONSTRAINT UQ_MayTram_TenMay UNIQUE (TenMay),
    CONSTRAINT FK_MayTram_KhuVucPhong FOREIGN KEY (MaPhong) REFERENCES dbo.KhuVucPhong(MaPhong)
);
GO

-- 2.6 Bảng PhienChoi (Phiên sử dụng máy trạm)
-- Trạng thái phiên full Tiếng Việt: N'Đang chơi', N'Đã kết thúc'
CREATE TABLE dbo.PhienChoi
(
    MaPhienChoi BIGINT IDENTITY(1,1) NOT NULL,
    MaMay INT NOT NULL,
    MaHoiVien INT NULL, -- NULL đối với khách vãng lai
    ThoiGianBatDau DATETIME NOT NULL CONSTRAINT DF_PhienChoi_ThoiGianBatDau DEFAULT GETDATE(),
    ThoiGianKetThuc DATETIME NULL,
    TongPhutChoi INT NOT NULL CONSTRAINT DF_PhienChoi_TongPhutChoi DEFAULT 0,
    GiaGioApDung DECIMAL(18,2) NOT NULL, -- Giá giờ chốt tại thời điểm mở phiên (Snapshot Price)
    TienGio DECIMAL(18,2) NOT NULL CONSTRAINT DF_PhienChoi_TienGio DEFAULT 0,
    TrangThaiPhien NVARCHAR(30) NOT NULL CONSTRAINT DF_PhienChoi_TrangThaiPhien DEFAULT N'Đang chơi',
    CONSTRAINT PK_PhienChoi PRIMARY KEY CLUSTERED (MaPhienChoi),
    CONSTRAINT FK_PhienChoi_MayTram FOREIGN KEY (MaMay) REFERENCES dbo.MayTram(MaMay),
    CONSTRAINT FK_PhienChoi_HoiVien FOREIGN KEY (MaHoiVien) REFERENCES dbo.HoiVien(MaHoiVien),
    CONSTRAINT CK_PhienChoi_GiaGioApDung CHECK (GiaGioApDung >= 0),
    CONSTRAINT CK_PhienChoi_TienGio CHECK (TienGio >= 0)
);
GO

-- 2.7 Bảng DanhMuc (Danh mục món ăn, đồ uống, dịch vụ)
CREATE TABLE dbo.DanhMuc
(
    MaDanhMuc INT IDENTITY(1,1) NOT NULL,
    TenDanhMuc NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_DanhMuc PRIMARY KEY CLUSTERED (MaDanhMuc),
    CONSTRAINT UQ_DanhMuc_TenDanhMuc UNIQUE (TenDanhMuc)
);
GO

-- 2.8 Bảng MonAn (Sản phẩm món ăn, nước uống & tồn kho)
CREATE TABLE dbo.MonAn
(
    MaMon INT IDENTITY(1,1) NOT NULL,
    MaDanhMuc INT NOT NULL,
    TenMon NVARCHAR(150) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    SoLuongTon INT NOT NULL CONSTRAINT DF_MonAn_SoLuongTon DEFAULT 0,
    HinhAnh NVARCHAR(255) NULL,
    ConPhucVu BIT NOT NULL CONSTRAINT DF_MonAn_ConPhucVu DEFAULT 1,
    CONSTRAINT PK_MonAn PRIMARY KEY CLUSTERED (MaMon),
    CONSTRAINT FK_MonAn_DanhMuc FOREIGN KEY (MaDanhMuc) REFERENCES dbo.DanhMuc(MaDanhMuc),
    CONSTRAINT CK_MonAn_DonGia CHECK (DonGia >= 0),
    CONSTRAINT CK_MonAn_SoLuongTon CHECK (SoLuongTon >= 0)
);
GO

-- 2.9 Bảng DonHang (Đơn đặt đồ ăn / dịch vụ từ máy trạm)
-- Trạng thái đơn full Tiếng Việt: N'Chờ duyệt', N'Đang làm', N'Đã giao', N'Đã hủy'
CREATE TABLE dbo.DonHang
(
    MaDonHang BIGINT IDENTITY(1,1) NOT NULL,
    MaPhienChoi BIGINT NULL,
    MaMay INT NOT NULL,
    ThoiGianDat DATETIME NOT NULL CONSTRAINT DF_DonHang_ThoiGianDat DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL CONSTRAINT DF_DonHang_TongTien DEFAULT 0,
    TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_DonHang_TrangThai DEFAULT N'Chờ duyệt',
    CONSTRAINT PK_DonHang PRIMARY KEY CLUSTERED (MaDonHang),
    CONSTRAINT FK_DonHang_PhienChoi FOREIGN KEY (MaPhienChoi) REFERENCES dbo.PhienChoi(MaPhienChoi),
    CONSTRAINT FK_DonHang_MayTram FOREIGN KEY (MaMay) REFERENCES dbo.MayTram(MaMay),
    CONSTRAINT CK_DonHang_TongTien CHECK (TongTien >= 0)
);
GO

-- 2.10 Bảng ChiTietDonHang (Chi tiết các món ăn trong từng đơn hàng)
CREATE TABLE dbo.ChiTietDonHang
(
    MaChiTiet BIGINT IDENTITY(1,1) NOT NULL,
    MaDonHang BIGINT NOT NULL,
    MaMon INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL, -- Đơn giá tại thời điểm đặt (Snapshot Price)
    ThanhTien AS (CAST(SoLuong * DonGia AS DECIMAL(18,2))) PERSISTED,
    CONSTRAINT PK_ChiTietDonHang PRIMARY KEY CLUSTERED (MaChiTiet),
    CONSTRAINT FK_ChiTietDonHang_DonHang FOREIGN KEY (MaDonHang) REFERENCES dbo.DonHang(MaDonHang) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietDonHang_MonAn FOREIGN KEY (MaMon) REFERENCES dbo.MonAn(MaMon),
    CONSTRAINT CK_ChiTietDonHang_SoLuong CHECK (SoLuong > 0),
    CONSTRAINT CK_ChiTietDonHang_DonGia CHECK (DonGia >= 0),
    CONSTRAINT UQ_ChiTietDonHang_DonHang_MonAn UNIQUE (MaDonHang, MaMon)
);
GO

-- 2.11 Bảng GiaoDich (Lịch sử nạp tiền & giao dịch tài chính)
CREATE TABLE dbo.GiaoDich
(
    MaGiaoDich BIGINT IDENTITY(1,1) NOT NULL,
    MaHoiVien INT NULL,
    MaNhanVien INT NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,
    HinhThucThanhToan NVARCHAR(50) NOT NULL CONSTRAINT DF_GiaoDich_HinhThucThanhToan DEFAULT N'Tiền mặt',
    NgayGiaoDich DATETIME NOT NULL CONSTRAINT DF_GiaoDich_NgayGiaoDich DEFAULT GETDATE(),
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT PK_GiaoDich PRIMARY KEY CLUSTERED (MaGiaoDich),
    CONSTRAINT FK_GiaoDich_HoiVien FOREIGN KEY (MaHoiVien) REFERENCES dbo.HoiVien(MaHoiVien),
    CONSTRAINT FK_GiaoDich_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES dbo.NhanVien(MaNhanVien),
    CONSTRAINT CK_GiaoDich_SoTien CHECK (SoTien > 0)
);
GO

-- ======================================================================================
-- 3. CHỈ MỤC TỐI ƯU HÓA TRUY VẤN (INDEXES)
-- ======================================================================================
CREATE NONCLUSTERED INDEX IX_MayTram_MaPhong ON dbo.MayTram(MaPhong);
CREATE NONCLUSTERED INDEX IX_PhienChoi_May_TrangThai ON dbo.PhienChoi(MaMay, TrangThaiPhien);
CREATE NONCLUSTERED INDEX IX_DonHang_Phien_TrangThai ON dbo.DonHang(MaPhienChoi, TrangThai);
CREATE NONCLUSTERED INDEX IX_GiaoDich_Ngay ON dbo.GiaoDich(NgayGiaoDich);
GO

-- ======================================================================================
-- 4. USER-DEFINED FUNCTION (UDF): TÍNH CƯỚC GIỜ CHƠI LINH HOẠT
-- ======================================================================================
CREATE FUNCTION dbo.fn_TinhTienGio
(
    @ThoiGianBatDau DATETIME,
    @ThoiGianKetThuc DATETIME,
    @DonGiaGio DECIMAL(18,2)
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    IF @ThoiGianBatDau IS NULL OR @ThoiGianKetThuc IS NULL OR @DonGiaGio IS NULL
        RETURN 0;
        
    DECLARE @Minutes INT = DATEDIFF(MINUTE, @ThoiGianBatDau, @ThoiGianKetThuc);
    IF @Minutes <= 0 SET @Minutes = 1;
    
    -- Tính tiền: (Số phút / 60.0) * Đơn giá giờ, làm tròn nguyên đồng
    DECLARE @TienGio DECIMAL(18,2) = ROUND((CAST(@Minutes AS DECIMAL(18,4)) / 60.0) * @DonGiaGio, 0);
    RETURN @TienGio;
END;
GO

CREATE FUNCTION dbo.fn_CalculateSessionCost
(
    @StartTime DATETIME,
    @EndTime DATETIME,
    @HourlyRate DECIMAL(18,2)
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    RETURN dbo.fn_TinhTienGio(@StartTime, @EndTime, @HourlyRate);
END;
GO

-- ======================================================================================
-- 5. BỘ 4 TRIGGERS BẢO VỆ TOÀN VẸN DỮ LIỆU (FOOL-PROOF TRIGGERS)
-- ======================================================================================

-- 5.1 Trigger chống mở trùng máy (ngăn 2 người mở cùng 1 máy trạm khi đang dùng)
CREATE TRIGGER dbo.trg_KiemTraTrangThaiMay
ON dbo.PhienChoi
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.MayTram m ON i.MaMay = m.MaMay
        WHERE m.TrangThai = N'Đang dùng'
          AND EXISTS (
              SELECT 1 FROM dbo.PhienChoi p 
              WHERE p.MaMay = i.MaMay 
                AND p.TrangThaiPhien = N'Đang chơi' 
                AND p.MaPhienChoi <> i.MaPhienChoi
          )
    )
    BEGIN
        RAISERROR(N'Máy trạm này hiện đang có người sử dụng! Không thể mở thêm phiên chơi mới.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- 5.2 Trigger chống xuất âm kho khi gọi món
CREATE TRIGGER dbo.trg_ChongXuatAmKho
ON dbo.ChiTietDonHang
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.MonAn m ON i.MaMon = m.MaMon
        WHERE i.SoLuong > m.SoLuongTon
    )
    BEGIN
        RAISERROR(N'Số lượng món ăn/dịch vụ trong kho không đủ để đáp ứng đơn hàng!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- 5.3 Trigger tự động cập nhật tổng tiền đơn hàng khi thêm/sửa/xóa chi tiết
CREATE TRIGGER dbo.trg_CapNhatTongTienDonHang
ON dbo.ChiTietDonHang
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @DanhSachDonHang TABLE (MaDonHang BIGINT PRIMARY KEY);
    INSERT INTO @DanhSachDonHang (MaDonHang)
    SELECT DISTINCT MaDonHang FROM inserted
    UNION
    SELECT DISTINCT MaDonHang FROM deleted;
    
    UPDATE d
    SET d.TongTien = ISNULL(sub.Tong, 0)
    FROM dbo.DonHang d
    INNER JOIN @DanhSachDonHang a ON d.MaDonHang = a.MaDonHang
    LEFT JOIN (
        SELECT MaDonHang, SUM(ThanhTien) AS Tong
        FROM dbo.ChiTietDonHang
        GROUP BY MaDonHang
    ) sub ON d.MaDonHang = sub.MaDonHang;
END;
GO

-- 5.4 Trigger tự động trừ tồn kho khi đơn hàng chuyển sang 'Đã giao'
CREATE TRIGGER dbo.trg_TruTonKhoKhiGiaoHang
ON dbo.DonHang
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF UPDATE(TrangThai)
    BEGIN
        UPDATE m
        SET m.SoLuongTon = m.SoLuongTon - ct.SoLuong
        FROM dbo.MonAn m
        INNER JOIN dbo.ChiTietDonHang ct ON m.MaMon = ct.MaMon
        INNER JOIN inserted i ON ct.MaDonHang = i.MaDonHang
        INNER JOIN deleted d ON i.MaDonHang = d.MaDonHang
        WHERE i.TrangThai = N'Đã giao' AND d.TrangThai <> N'Đã giao';
    END
END;
GO

-- ======================================================================================
-- 6. BỘ STORED PROCEDURES NGHIỆP VỤ BỌC TRANSACTION (ACID)
-- ======================================================================================

-- 6.1 Thủ tục Bắt đầu phiên chơi / Mở máy (sp_MoPhienChoi)
CREATE PROCEDURE dbo.sp_MoPhienChoi
    @MaMay INT,
    @MaHoiVien INT = NULL,
    @MaPhienChoiMoi BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- 1. Kiểm tra máy tồn tại và trạng thái
        DECLARE @TrangThaiMay NVARCHAR(30), @DonGiaGio DECIMAL(18,2);
        SELECT @TrangThaiMay = m.TrangThai, @DonGiaGio = p.DonGiaGio
        FROM dbo.MayTram m
        INNER JOIN dbo.KhuVucPhong p ON m.MaPhong = p.MaPhong
        WHERE m.MaMay = @MaMay;
        
        IF @TrangThaiMay IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy máy trạm với mã đã chỉ định!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        IF @TrangThaiMay = N'Đang dùng'
        BEGIN
            RAISERROR(N'Máy trạm hiện đang có người sử dụng!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @TrangThaiMay = N'Bảo trì'
        BEGIN
            RAISERROR(N'Máy trạm đang trong trạng thái bảo trì, không thể mở máy!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        -- 2. Nếu là hội viên, kiểm tra số dư và trạng thái
        IF @MaHoiVien IS NOT NULL
        BEGIN
            DECLARE @SoDuHoiVien DECIMAL(18,2), @TrangThaiHoiVien NVARCHAR(30);
            SELECT @SoDuHoiVien = SoDu, @TrangThaiHoiVien = TrangThai
            FROM dbo.HoiVien
            WHERE MaHoiVien = @MaHoiVien;
            
            IF @TrangThaiHoiVien IS NULL
            BEGIN
                RAISERROR(N'Không tìm thấy thông tin hội viên!', 16, 1);
                ROLLBACK TRANSACTION;
                RETURN;
            END
            
            IF @TrangThaiHoiVien = N'Bị khóa'
            BEGIN
                RAISERROR(N'Tài khoản hội viên đang bị khóa, không thể mở máy!', 16, 1);
                ROLLBACK TRANSACTION;
                RETURN;
            END
            
            IF @SoDuHoiVien < 5000
            BEGIN
                RAISERROR(N'Số dư tài khoản không đủ để mở máy (tối thiểu 5.000 VNĐ)!', 16, 1);
                ROLLBACK TRANSACTION;
                RETURN;
            END
            
            -- Chuyển trạng thái hội viên sang 'Đang chơi'
            UPDATE dbo.HoiVien SET TrangThai = N'Đang chơi' WHERE MaHoiVien = @MaHoiVien;
        END
        
        -- 3. Cập nhật máy sang 'Đang dùng'
        UPDATE dbo.MayTram SET TrangThai = N'Đang dùng' WHERE MaMay = @MaMay;
        
        -- 4. Tạo bản ghi phiên chơi
        INSERT INTO dbo.PhienChoi (MaMay, MaHoiVien, ThoiGianBatDau, GiaGioApDung, TrangThaiPhien)
        VALUES (@MaMay, @MaHoiVien, GETDATE(), @DonGiaGio, N'Đang chơi');
        
        SET @MaPhienChoiMoi = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;
GO

-- 6.2 Thủ tục Kết thúc phiên chơi / Trả máy (sp_KetThucPhienChoi)
CREATE PROCEDURE dbo.sp_KetThucPhienChoi
    @MaPhienChoi BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        DECLARE @MaMay INT, @MaHoiVien INT, @ThoiGianBatDau DATETIME, @GiaGioApDung DECIMAL(18,2), @TrangThaiPhien NVARCHAR(30);
        SELECT @MaMay = MaMay, 
               @MaHoiVien = MaHoiVien, 
               @ThoiGianBatDau = ThoiGianBatDau, 
               @GiaGioApDung = GiaGioApDung,
               @TrangThaiPhien = TrangThaiPhien
        FROM dbo.PhienChoi
        WHERE MaPhienChoi = @MaPhienChoi;
        
        IF @TrangThaiPhien IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy phiên chơi!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        IF @TrangThaiPhien = N'Đã kết thúc'
        BEGIN
            RAISERROR(N'Phiên chơi này đã kết thúc trước đó!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        DECLARE @ThoiGianKetThuc DATETIME = GETDATE();
        DECLARE @TongPhutChoi INT = DATEDIFF(MINUTE, @ThoiGianBatDau, @ThoiGianKetThuc);
        IF @TongPhutChoi <= 0 SET @TongPhutChoi = 1;
        
        DECLARE @TienGio DECIMAL(18,2) = dbo.fn_TinhTienGio(@ThoiGianBatDau, @ThoiGianKetThuc, @GiaGioApDung);
        
        -- Cập nhật phiên chơi
        UPDATE dbo.PhienChoi
        SET ThoiGianKetThuc = @ThoiGianKetThuc,
            TongPhutChoi = @TongPhutChoi,
            TienGio = @TienGio,
            TrangThaiPhien = N'Đã kết thúc'
        WHERE MaPhienChoi = @MaPhienChoi;
        
        -- Cập nhật máy về 'Sẵn sàng'
        UPDATE dbo.MayTram
        SET TrangThai = N'Sẵn sàng'
        WHERE MaMay = @MaMay;
        
        -- Nếu là hội viên: trừ số dư và cộng dồn thời gian chơi
        IF @MaHoiVien IS NOT NULL
        BEGIN
            UPDATE dbo.HoiVien
            SET SoDu = CASE WHEN SoDu >= @TienGio THEN SoDu - @TienGio ELSE 0 END,
                TongThoiGianChoi = TongThoiGianChoi + @TongPhutChoi,
                TrangThai = N'Hoạt động'
            WHERE MaHoiVien = @MaHoiVien;
        END
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;
GO

-- 6.3 Thủ tục Nạp tiền tài khoản hội viên (sp_NapTienHoiVien)
CREATE PROCEDURE dbo.sp_NapTienHoiVien
    @MaHoiVien INT,
    @MaNhanVien INT,
    @SoTien DECIMAL(18,2),
    @HinhThucThanhToan NVARCHAR(50) = N'Tiền mặt',
    @GhiChu NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF @SoTien <= 0
        BEGIN
            RAISERROR(N'Số tiền nạp phải lớn hơn 0!', 16, 1);
            RETURN;
        END
        
        IF NOT EXISTS (SELECT 1 FROM dbo.HoiVien WHERE MaHoiVien = @MaHoiVien)
        BEGIN
            RAISERROR(N'Không tìm thấy hội viên cần nạp tiền!', 16, 1);
            RETURN;
        END
        
        IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaNhanVien = @MaNhanVien)
        BEGIN
            RAISERROR(N'Không tìm thấy nhân viên thu ngân thực hiện!', 16, 1);
            RETURN;
        END
        
        BEGIN TRANSACTION;
        
        -- Cộng số dư
        UPDATE dbo.HoiVien
        SET SoDu = SoDu + @SoTien
        WHERE MaHoiVien = @MaHoiVien;
        
        -- Ghi vết lịch sử giao dịch
        INSERT INTO dbo.GiaoDich (MaHoiVien, MaNhanVien, SoTien, HinhThucThanhToan, NgayGiaoDich, GhiChu)
        VALUES (@MaHoiVien, @MaNhanVien, @SoTien, @HinhThucThanhToan, GETDATE(), @GhiChu);
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;
GO

-- 6.4 Thủ tục Truy vấn Doanh thu Báo cáo Crystal Report (sp_BaoCaoDoanhThu)
CREATE PROCEDURE dbo.sp_BaoCaoDoanhThu
    @TuNgay DATETIME,
    @DenNgay DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Đảm bảo ngày kết thúc bao trọn hết 23:59:59 của ngày đó
    SET @DenNgay = DATEADD(SECOND, -1, DATEADD(DAY, 1, CAST(CAST(@DenNgay AS DATE) AS DATETIME)));
    
    SELECT 
        CAST(Ngay AS DATE) AS NgayBaoCao,
        LoaiDoanhThu,
        TenDichVu,
        SoLuongGiaoDich,
        TongDoanhThu
    FROM
    (
        -- 1. Doanh thu tiền giờ theo phòng máy
        SELECT 
            CAST(p.ThoiGianBatDau AS DATE) AS Ngay,
            N'Tiền giờ chơi' AS LoaiDoanhThu,
            k.TenPhong AS TenDichVu,
            COUNT(p.MaPhienChoi) AS SoLuongGiaoDich,
            ISNULL(SUM(p.TienGio), 0) AS TongDoanhThu
        FROM dbo.PhienChoi p
        INNER JOIN dbo.MayTram m ON p.MaMay = m.MaMay
        INNER JOIN dbo.KhuVucPhong k ON m.MaPhong = k.MaPhong
        WHERE p.TrangThaiPhien = N'Đã kết thúc'
          AND p.ThoiGianBatDau >= @TuNgay AND p.ThoiGianBatDau <= @DenNgay
        GROUP BY CAST(p.ThoiGianBatDau AS DATE), k.TenPhong
        
        UNION ALL
        
        -- 2. Doanh thu dịch vụ F&B theo danh mục món
        SELECT 
            CAST(d.ThoiGianDat AS DATE) AS Ngay,
            N'Dịch vụ F&B' AS LoaiDoanhThu,
            dm.TenDanhMuc AS TenDichVu,
            ISNULL(SUM(ct.SoLuong), 0) AS SoLuongGiaoDich,
            ISNULL(SUM(ct.ThanhTien), 0) AS TongDoanhThu
        FROM dbo.DonHang d
        INNER JOIN dbo.ChiTietDonHang ct ON d.MaDonHang = ct.MaDonHang
        INNER JOIN dbo.MonAn m ON ct.MaMon = m.MaMon
        INNER JOIN dbo.DanhMuc dm ON m.MaDanhMuc = dm.MaDanhMuc
        WHERE d.TrangThai = N'Đã giao'
          AND d.ThoiGianDat >= @TuNgay AND d.ThoiGianDat <= @DenNgay
        GROUP BY CAST(d.ThoiGianDat AS DATE), dm.TenDanhMuc
        
        UNION ALL
        
        -- 3. Dòng tiền nạp tài khoản hội viên theo phương thức thanh toán
        SELECT 
            CAST(g.NgayGiaoDich AS DATE) AS Ngay,
            N'Nạp tiền hội viên' AS LoaiDoanhThu,
            g.HinhThucThanhToan AS TenDichVu,
            COUNT(g.MaGiaoDich) AS SoLuongGiaoDich,
            ISNULL(SUM(g.SoTien), 0) AS TongDoanhThu
        FROM dbo.GiaoDich g
        WHERE g.NgayGiaoDich >= @TuNgay AND g.NgayGiaoDich <= @DenNgay
        GROUP BY CAST(g.NgayGiaoDich AS DATE), g.HinhThucThanhToan
    ) AS BaoCao
    ORDER BY NgayBaoCao ASC, LoaiDoanhThu ASC;
END;
GO

-- ======================================================================================
-- 7. CÁC PROCEDURES TƯƠNG THÍCH TÊN TIẾNG ANH (COMPATIBILITY WRAPPERS)
-- ======================================================================================
CREATE PROCEDURE dbo.sp_StartSession
    @ComputerID INT,
    @MemberID INT = NULL,
    @NewSessionID BIGINT OUTPUT
AS
BEGIN
    EXEC dbo.sp_MoPhienChoi @MaMay = @ComputerID, @MaHoiVien = @MemberID, @MaPhienChoiMoi = @NewSessionID OUTPUT;
END;
GO

CREATE PROCEDURE dbo.sp_EndSession
    @SessionID BIGINT
AS
BEGIN
    EXEC dbo.sp_KetThucPhienChoi @MaPhienChoi = @SessionID;
END;
GO

CREATE PROCEDURE dbo.sp_TopupMemberBalance
    @MemberID INT,
    @EmployeeID INT,
    @Amount DECIMAL(18,2),
    @PaymentMethod NVARCHAR(50) = N'Tiền mặt',
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    EXEC dbo.sp_NapTienHoiVien @MaHoiVien = @MemberID, @MaNhanVien = @EmployeeID, @SoTien = @Amount, @HinhThucThanhToan = @PaymentMethod, @GhiChu = @Notes;
END;
GO

CREATE PROCEDURE dbo.sp_GetRevenueReport
    @TuNgay DATETIME,
    @DenNgay DATETIME
AS
BEGIN
    EXEC dbo.sp_BaoCaoDoanhThu @TuNgay = @TuNgay, @DenNgay = @DenNgay;
END;
GO

-- ======================================================================================
-- 8. HỆ THỐNG VIEWS NGHIỆP VỤ TIẾNG VIỆT (V_VIEWS)
-- ======================================================================================

-- 8.1 View Nhân viên & Vai trò
CREATE VIEW dbo.v_NhanVien AS
SELECT 
    nv.MaNhanVien,
    nv.TenDangNhap,
    nv.MatKhau,
    nv.HoTen,
    nv.SoDienThoai,
    nv.MaVaiTro,
    vt.TenVaiTro,
    nv.TrangThai,
    CASE WHEN nv.TrangThai = 1 THEN N'Đang làm việc' ELSE N'Nghỉ việc' END AS TenTrangThai
FROM dbo.NhanVien nv
INNER JOIN dbo.VaiTro vt ON nv.MaVaiTro = vt.MaVaiTro;
GO

-- 8.2 View Máy trạm & Khu vực phòng
CREATE VIEW dbo.v_MayTram AS
SELECT 
    m.MaMay,
    m.TenMay,
    m.DiaChiIP,
    m.DiaChiMAC,
    m.MaPhong,
    p.TenPhong,
    p.DonGiaGio,
    m.TrangThai
FROM dbo.MayTram m
INNER JOIN dbo.KhuVucPhong p ON m.MaPhong = p.MaPhong;
GO

-- 8.3 View Danh sách Hội viên
CREATE VIEW dbo.v_HoiVien AS
SELECT 
    MaHoiVien,
    TenDangNhap,
    MatKhau,
    HoTen,
    SoDu,
    TongThoiGianChoi,
    NgayTao,
    TrangThai
FROM dbo.HoiVien;
GO

-- 8.4 View Thực đơn Món ăn & Danh mục
CREATE VIEW dbo.v_MonAn AS
SELECT 
    m.MaMon,
    m.MaDanhMuc,
    dm.TenDanhMuc,
    m.TenMon,
    m.DonGia,
    m.SoLuongTon,
    m.HinhAnh,
    m.ConPhucVu,
    CASE WHEN m.ConPhucVu = 1 AND m.SoLuongTon > 0 THEN N'Còn hàng' ELSE N'Hết hàng' END AS TinhTrang
FROM dbo.MonAn m
INNER JOIN dbo.DanhMuc dm ON m.MaDanhMuc = dm.MaDanhMuc;
GO

-- 8.5 View Danh sách Đơn hàng
CREATE VIEW dbo.v_DonHang AS
SELECT 
    d.MaDonHang,
    d.MaPhienChoi,
    d.MaMay,
    m.TenMay,
    d.ThoiGianDat,
    d.TongTien,
    d.TrangThai
FROM dbo.DonHang d
INNER JOIN dbo.MayTram m ON d.MaMay = m.MaMay;
GO

-- 8.6 View Chi tiết Đơn hàng gọi món
CREATE VIEW dbo.v_ChiTietDonHang AS
SELECT 
    ct.MaChiTiet,
    ct.MaDonHang,
    ct.MaMon,
    m.TenMon,
    ct.SoLuong,
    ct.DonGia,
    ct.ThanhTien
FROM dbo.ChiTietDonHang ct
INNER JOIN dbo.MonAn m ON ct.MaMon = m.MaMon;
GO

-- 8.7 View Lịch sử Phiên chơi
CREATE VIEW dbo.v_PhienChoi AS
SELECT 
    p.MaPhienChoi,
    p.MaMay,
    m.TenMay,
    p.MaHoiVien,
    ISNULL(hv.HoTen, N'Khách vãng lai') AS TenKhachHang,
    p.ThoiGianBatDau,
    p.ThoiGianKetThuc,
    p.TongPhutChoi,
    p.GiaGioApDung,
    p.TienGio,
    p.TrangThaiPhien
FROM dbo.PhienChoi p
INNER JOIN dbo.MayTram m ON p.MaMay = m.MaMay
LEFT JOIN dbo.HoiVien hv ON p.MaHoiVien = hv.MaHoiVien;
GO

-- 8.8 View Lịch sử Giao dịch Nạp tiền
CREATE VIEW dbo.v_GiaoDich AS
SELECT 
    g.MaGiaoDich,
    g.MaHoiVien,
    ISNULL(hv.HoTen, N'Không xác định') AS TenHoiVien,
    g.MaNhanVien,
    nv.HoTen AS TenThuNgan,
    g.SoTien,
    g.HinhThucThanhToan,
    g.NgayGiaoDich,
    g.GhiChu
FROM dbo.GiaoDich g
INNER JOIN dbo.NhanVien nv ON g.MaNhanVien = nv.MaNhanVien
LEFT JOIN dbo.HoiVien hv ON g.MaHoiVien = hv.MaHoiVien;
GO

-- ======================================================================================
-- 9. VIEW PHỤ TRỢ TƯƠNG THÍCH TIẾNG ANH (COMPATIBILITY CHO CODE CỦA ĐỒNG ĐỘI)
-- ======================================================================================
CREATE VIEW dbo.Roles AS 
SELECT MaVaiTro AS RoleID, TenVaiTro AS RoleName FROM dbo.VaiTro;
GO

CREATE VIEW dbo.Employees AS 
SELECT MaNhanVien AS EmployeeID, TenDangNhap AS Username, MatKhau AS PasswordHash, HoTen AS FullName, SoDienThoai AS Phone, MaVaiTro AS RoleID, TrangThai AS IsActive FROM dbo.NhanVien;
GO

CREATE VIEW dbo.Members AS 
SELECT MaHoiVien AS MemberID, TenDangNhap AS Username, MatKhau AS PasswordHash, HoTen AS FullName, SoDu AS Balance, TongThoiGianChoi AS TotalPlayTimeMinutes, NgayTao AS CreatedAt, TrangThai AS Status FROM dbo.HoiVien;
GO

CREATE VIEW dbo.ComputerRooms AS 
SELECT MaPhong AS RoomID, TenPhong AS RoomName, DonGiaGio AS HourlyRate FROM dbo.KhuVucPhong;
GO

CREATE VIEW dbo.Computers AS 
SELECT MaMay AS ComputerID, TenMay AS ComputerName, DiaChiIP AS IPAddress, DiaChiMAC AS MACAddress, MaPhong AS RoomID, TrangThai AS Status FROM dbo.MayTram;
GO

CREATE VIEW dbo.Sessions AS 
SELECT MaPhienChoi AS SessionID, MaMay AS ComputerID, MaHoiVien AS MemberID, ThoiGianBatDau AS StartTime, ThoiGianKetThuc AS EndTime, TongPhutChoi AS TotalMinutes, GiaGioApDung AS HourlyRateApplied, TienGio AS TotalPlayCost, TrangThaiPhien AS SessionStatus FROM dbo.PhienChoi;
GO

CREATE VIEW dbo.Categories AS 
SELECT MaDanhMuc AS CategoryID, TenDanhMuc AS CategoryName FROM dbo.DanhMuc;
GO

CREATE VIEW dbo.Products AS 
SELECT MaMon AS ProductID, MaDanhMuc AS CategoryID, TenMon AS ProductName, DonGia AS UnitPrice, SoLuongTon AS StockQuantity, HinhAnh AS ImageUrl, ConPhucVu AS IsAvailable FROM dbo.MonAn;
GO

CREATE VIEW dbo.Orders AS 
SELECT MaDonHang AS OrderID, MaPhienChoi AS SessionID, MaMay AS ComputerID, ThoiGianDat AS OrderTime, TongTien AS TotalAmount, TrangThai AS Status FROM dbo.DonHang;
GO

CREATE VIEW dbo.OrderDetails AS 
SELECT MaChiTiet AS OrderDetailID, MaDonHang AS OrderID, MaMon AS ProductID, SoLuong AS Quantity, DonGia AS UnitPrice, ThanhTien AS SubTotal FROM dbo.ChiTietDonHang;
GO

CREATE VIEW dbo.Transactions AS 
SELECT MaGiaoDich AS TransactionID, MaHoiVien AS MemberID, MaNhanVien AS EmployeeID, SoTien AS Amount, HinhThucThanhToan AS PaymentMethod, NgayGiaoDich AS TransactionDate, GhiChu AS Notes FROM dbo.GiaoDich;
GO

-- ======================================================================================
-- 10. NẠP BỘ DỮ LIỆU MẪU KHỞI TẠO FULL TIẾNG VIỆT (SEED DATA)
-- ======================================================================================

-- 10.1 Nạp Vai trò (VaiTro)
INSERT INTO dbo.VaiTro (TenVaiTro) VALUES 
(N'Quản trị viên (Admin)'),
(N'Thu ngân'),
(N'Kỹ thuật');
GO

-- 10.2 Nạp Tài khoản Nhân viên Quản trị (NhanVien)
-- Mật khẩu của tài khoản 'admin' là '121212' (tương thích trực tiếp với frmDangNhapServer.cs)
INSERT INTO dbo.NhanVien (TenDangNhap, MatKhau, HoTen, SoDienThoai, MaVaiTro, TrangThai) VALUES 
('admin', '$2a$11$PsurN8TwsyjR3R/QJ/nOzOqu.KBueNRZABhxDgDDmKg6iqJEWpbWu', N'Quản trị viên Hệ thống', '0901234567', 1, 1),
('thungan01', '123456', N'Nguyễn Văn Thu Ngân', '0912345678', 2, 1),
('kythuat01', '123456', N'Trần Kỹ Thuật', '0923456789', 3, 1);
GO

-- 10.3 Nạp Khu vực / Phòng máy (KhuVucPhong)
INSERT INTO dbo.KhuVucPhong (TenPhong, DonGiaGio) VALUES 
(N'Phòng Thường', 8000.00),
(N'Phòng VIP', 12000.00),
(N'Phòng Thi Đấu (FPS)', 15000.00),
(N'Phòng Couple (Đôi)', 14000.00);
GO

-- 10.4 Nạp Danh sách Máy trạm (MayTram)
-- Trạng thái: N'Sẵn sàng' (Xanh), N'Tắt máy' (Xám), N'Bảo trì' (Vàng)
INSERT INTO dbo.MayTram (TenMay, DiaChiIP, DiaChiMAC, MaPhong, TrangThai) VALUES 
(N'MAY-01', '192.168.1.101', '00-14-22-01-23-45', 1, N'Sẵn sàng'),
(N'MAY-02', '192.168.1.102', '00-14-22-01-23-46', 1, N'Sẵn sàng'),
(N'MAY-03', '192.168.1.103', '00-14-22-01-23-47', 1, N'Sẵn sàng'),
(N'MAY-04', '192.168.1.104', '00-14-22-01-23-48', 1, N'Tắt máy'),
(N'MAY-05', '192.168.1.105', '00-14-22-01-23-49', 2, N'Sẵn sàng'),
(N'MAY-06', '192.168.1.106', '00-14-22-01-23-50', 2, N'Sẵn sàng'),
(N'MAY-07', '192.168.1.107', '00-14-22-01-23-51', 2, N'Sẵn sàng'),
(N'MAY-08', '192.168.1.108', '00-14-22-01-23-52', 3, N'Sẵn sàng'),
(N'MAY-09', '192.168.1.109', '00-14-22-01-23-53', 3, N'Bảo trì'),
(N'MAY-10', '192.168.1.110', '00-14-22-01-23-54', 4, N'Sẵn sàng');
GO

-- 10.5 Nạp Danh mục món (DanhMuc)
INSERT INTO dbo.DanhMuc (TenDanhMuc) VALUES 
(N'Nước giải khát'),
(N'Mì & Bún'),
(N'Cơm & Fastfood'),
(N'Thẻ Game & Khác');
GO

-- 10.6 Nạp Thực đơn & Kho hàng (MonAn)
INSERT INTO dbo.MonAn (MaDanhMuc, TenMon, DonGia, SoLuongTon, HinhAnh, ConPhucVu) VALUES 
(1, N'Nước tăng lực Sting dâu', 12000.00, 50, 'Images/Products/sting.png', 1),
(1, N'Coca Cola lon 330ml', 12000.00, 45, 'Images/Products/cocacola.png', 1),
(1, N'Trà sữa trân châu đường đen', 25000.00, 30, 'Images/Products/trasua.png', 1),
(1, N'Nước suối Aquafina 500ml', 8000.00, 100, 'Images/Products/aquafina.png', 1),
(2, N'Mì tôm Hảo Hảo xào bò trứng', 25000.00, 40, 'Images/Products/mixaobo.png', 1),
(2, N'Mì cay hải sản cấp độ 3', 35000.00, 25, 'Images/Products/micay.png', 1),
(2, N'Mì ly Modern lẩu thái', 15000.00, 60, 'Images/Products/mily.png', 1),
(3, N'Cơm sườn nướng trứng ốp la', 35000.00, 20, 'Images/Products/comsuon.png', 1),
(3, N'Cơm chiên dương châu', 30000.00, 25, 'Images/Products/comchien.png', 1),
(3, N'Xúc xích Đức nướng (2 cây)', 18000.00, 50, 'Images/Products/xucxich.png', 1),
(4, N'Thẻ Garena 50K', 50000.00, 20, 'Images/Products/thegarena.png', 1),
(4, N'Thẻ Zing 50K', 50000.00, 20, 'Images/Products/thezing.png', 1);
GO

-- 10.7 Nạp Danh sách Hội viên mẫu (HoiVien)
-- Trạng thái: N'Hoạt động', N'Bị khóa'
INSERT INTO dbo.HoiVien (TenDangNhap, MatKhau, HoTen, SoDu, TongThoiGianChoi, NgayTao, TrangThai) VALUES 
('hoivien01', '123456', N'Nguyễn Hoàng Dũng', 50000.00, 120, GETDATE(), N'Hoạt động'),
('hoivien02', '123456', N'Trần Thị Mai', 100000.00, 240, GETDATE(), N'Hoạt động'),
('hoivien03', '123456', N'Lê Minh Khôi', 0.00, 30, GETDATE(), N'Hoạt động'),
('hoivien_khoa', '123456', N'Phạm Văn Cấm', 20000.00, 10, GETDATE(), N'Bị khóa');
GO

-- 10.8 Nạp Giao dịch nạp tiền ban đầu (GiaoDich)
INSERT INTO dbo.GiaoDich (MaHoiVien, MaNhanVien, SoTien, HinhThucThanhToan, NgayGiaoDich, GhiChu) VALUES 
(1, 2, 50000.00, N'Tiền mặt', DATEADD(HOUR, -3, GETDATE()), N'Nạp tiền tại quầy thu ngân'),
(2, 2, 100000.00, N'Chuyển khoản VietQR', DATEADD(HOUR, -2, GETDATE()), N'Nạp tiền qua VietQR'),
(4, 2, 20000.00, N'Tiền mặt', DATEADD(DAY, -1, GETDATE()), N'Nạp tiền trước khi vi phạm');
GO

PRINT N'>>> CÀI ĐẶT CƠ SỞ DỮ LIỆU NPCData (FULL TIẾNG VIỆT 3NF & HỆ THỐNG V_VIEWS) HOÀN TẤT THÀNH CÔNG! <<<';
GO

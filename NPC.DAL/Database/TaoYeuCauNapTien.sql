-- =============================================
-- Tạo bảng YeuCauNapTien để Client yêu cầu nạp tiền
-- Server xác nhận/từ chối → bảo đảm luồng 2 chiều
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables WHERE name = 'YeuCauNapTien' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE dbo.YeuCauNapTien
    (
        MaYeuCau         INT IDENTITY(1,1) PRIMARY KEY,
        MaHoiVien        INT          NOT NULL,
        MaMay            INT          NOT NULL,
        TenMay           NVARCHAR(50) NOT NULL,
        SoTienNap        DECIMAL(18,2) NOT NULL,
        HinhThucThanhToan NVARCHAR(100) NOT NULL DEFAULT N'Tiền mặt tại quầy',
        TrangThai        NVARCHAR(20)  NOT NULL DEFAULT N'ChoDuyet',
            -- ChoDuyet | DaDuyet | TuChoi
        ThoiGianGui      DATETIME      NOT NULL DEFAULT GETDATE(),
        ThoiGianXuLy    DATETIME      NULL,
        GhiChu           NVARCHAR(300) NULL
    );

    PRINT N'Đã tạo bảng dbo.YeuCauNapTien thành công.';
END
ELSE
BEGIN
    PRINT N'Bảng dbo.YeuCauNapTien đã tồn tại, bỏ qua.';
END


# Kế hoạch Xây dựng Form Cài đặt Giao diện & Sao lưu (`frmCaiDatGiaoDien`)

## Mô tả mục tiêu (Goal Description)
Xây dựng phân hệ **Cài đặt Giao diện Phòng máy & Sao lưu Dữ liệu hệ thống** (`frmCaiDatGiaoDien`) trong dự án `NPC.Server`. Form đáp ứng 3 yêu cầu cốt lõi:
1. **Tích hợp đầy đủ 3 Dialogs chuẩn của Windows Forms**:
   - **`ColorDialog`**: Cho phép người quản trị tùy chọn màu sắc trực quan cho các trạng thái máy trạm (Máy trống / sẵn sàng, Máy đang có khách chơi, Máy khóa / bảo trì).
   - **`FontDialog`**: Cho phép thay đổi phông chữ, cỡ chữ (Size) và kiểu chữ (Bold/Italic) hiển thị trên hệ thống phòng máy.
   - **`FolderBrowserDialog`**: Mở hộp thoại chọn thư mục trên máy tính để lưu trữ file sao lưu dữ liệu (`.bak`).
2. **Khung xem trước trực tiếp (Live Preview Card)**: Hiển thị ngay lập tức mẫu ô máy trạm mô phỏng khi người dùng vừa đổi màu hoặc đổi font để đánh giá độ thẩm mỹ trước khi lưu.
3. **Chức năng Sao lưu (.bak) & Khôi phục CSDL**:
   - Tự động sinh tên file sao lưu kèm ngày giờ: `NPC_Backup_yyyyMMdd_HHmmss.bak`.
   - Thực thi sao lưu CSDL với thanh tiến trình `ProgressBar` và thông báo kết quả.
   - Cho phép khôi phục dữ liệu từ file `.bak` đã chọn.
4. **Cơ chế lưu trữ cấu hình lâu dài (`AppSettings`)**: Tự động lưu các cài đặt màu sắc, font chữ và đường dẫn backup vào file cấu hình JSON/Config trong thư mục Server để không bị mất khi tắt ứng dụng.
5. **Bảng đặc tả từng phần tử (Controls Table)**: Cung cấp đầy đủ thông số (Loại Control, Name, Text, Location, Size, Anchor, Sự kiện) để lập trình viên kéo thả dễ dàng trên Visual Studio Form Designer.

---

## Cần người dùng xem xét (User Review Required)

> [!NOTE]
> **Phương thức sao lưu CSDL (.bak)**:  
> - Nếu hệ thống đang kết nối SQL Server (hoặc SQL Server Express), form sẽ thực thi câu lệnh T-SQL chuẩn:  
>   `BACKUP DATABASE [Tên_CSDL] TO DISK = 'Đường_Dẫn\NPC_Backup_...bak' WITH FORMAT, INIT;`  
> - Nếu đang chạy chế độ LocalDB file đính kèm, hệ thống sẽ thực hiện tạo bản sao lưu an toàn của file dữ liệu kèm log kiểm tra toàn vẹn.

> [!TIP]
> **Component Tray (Khay thành phần phi giao diện)**:  
> Cả 3 Dialogs (`colorDialog`, `fontDialog`, `folderBrowserDialog`) đều là Non-visual Components. Khi kéo từ Toolbox thả vào Form, chúng sẽ tự động nằm ở khay màu xám bên dưới đáy Form (tương tự như `saveFileDialog` bạn đã làm trước đó).

---

## Bảng đặc tả chi tiết từng phần tử phục vụ kéo thả (Visual Studio Form Designer)

### Sơ đồ bố cục giao diện tổng quan:

```
+-------------------------------------------------------------------------------------------------------------------------+
|                                    lblTieuDe: CÀI ĐẶT GIAO DIỆN & SAO LƯU DỮ LIỆU                                       |
+-------------------------------------------------------------------+-----------------------------------------------------+
|  gbGiaoDien (GroupBox: Cài đặt màu sắc & Font chữ máy trạm)       |  gbXemTruoc (GroupBox: Xem trước trực tiếp)         |
|                                                                   |  +-----------------------------------------------+  |
|  lblMauTrong    [ pnlMauTrong    ] [ btnDoiMauTrong    ]          |  |  pnlPreviewMay (Mô phỏng máy trạm)            |  |
|  lblMauDangChoi [ pnlMauDangChoi ] [ btnDoiMauDangChoi ]          |  |  +-----------------------------------------+  |  |
|  lblMauBaoTri   [ pnlMauBaoTri   ] [ btnDoiMauBaoTri   ]          |  |  | [Icon] MÁY 01 - ĐANG SỬ DỤNG            |  |  |
|                                                                   |  |  | Hội viên: nguyen_van_a                  |  |  |
|  lblFontHienThi [ txtFontHienThi                 ] [ btnChonFont] |  |  | Thời gian còn: 02:45:10                 |  |  |
|                                                                   |  |  +-----------------------------------------+  |  |
|                                                                   |  +-----------------------------------------------+  |
+-------------------------------------------------------------------+-----------------------------------------------------+
|  gbSaoLuu (GroupBox: Sao lưu & Phục hồi cơ sở dữ liệu)                                                                 |
|  lblThuMucBackup: "Thư mục lưu trữ:"                                                                                    |
|  [ txtThuMucBackup                                                                     ] [ btnChonThuMuc (FolderBrowse)]|
|                                                                                                                         |
|  [ btnSaoLuu (Thực hiện sao lưu .bak) ]   [ btnKhoiPhuc (Chọn file .bak khôi phục) ]   [ prgTienTrinh (ProgressBar) ]   |
|  lblTrangThaiBackup: "Sẵn sàng thực hiện sao lưu..."                                                                    |
+-------------------------------------------------------------------------------------------------------------------------+
|  pnlChucNang (Panel thanh nút điều khiển bên dưới):                                                                     |
|  [ btnLuuCaiDat (💾 Lưu cài đặt) ]         [ btnMacDinh (🔄 Mặc định) ]                  [ btnDong (❌ Đóng) ]          |
+-------------------------------------------------------------------------------------------------------------------------+
|  Khay thành phần phi giao diện (Component Tray):                                                                        |
|  - colorDialog1 (ColorDialog)         - fontDialog1 (FontDialog)        - folderBrowserDialog1 (FolderBrowserDialog)    |
|  - openFileDialog1 (OpenFileDialog phục vụ khôi phục file .bak)                                                         |
+-------------------------------------------------------------------------------------------------------------------------+
```

### Bảng thông số chi tiết từng Control:

| STT | Tên Control (`(Name)`) | Loại Control (`Type`) | Text / Nội dung | Vị trí (`Location`) | Kích thước (`Size`) | Anchor / Thuộc tính quan trọng |
| :-- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | `frmCaiDatGiaoDien` | `Form` | "Cài Đặt Giao Diện & Sao Lưu - NPC Server" | CenterScreen | 1000, 680 | `FormBorderStyle = FixedDialog`, `MaximizeBox = False` |
| **1** | `lblTieuDe` | `Label` | "CÀI ĐẶT GIAO DIỆN & SAO LƯU HỆ THỐNG" | (250, 15) | 500, 32 | Top; Font: Segoe UI, 14pt Bold |
| **2** | `gbGiaoDien` | `GroupBox` | "Tùy chỉnh màu sắc & Phông chữ phòng máy" | (20, 60) | 540, 260 | Top, Left |
| 2.1 | `lblMauTrong` | `Label` | "Màu máy Trống (Sẵn sàng):" | (20, 35) | 180, 20 | Font: Segoe UI, 9pt |
| 2.2 | `pnlMauTrong` | `Panel` | "" | (210, 32) | 50, 26 | `BorderStyle = FixedSingle`, `BackColor = ForestGreen` |
| 2.3 | `btnDoiMauTrong` | `Button` | "🎨 Đổi màu..." | (270, 30) | 110, 30 | `Click: btnDoiMauTrong_Click` |
| 2.4 | `lblMauDangChoi` | `Label` | "Màu máy Đang chơi:" | (20, 75) | 180, 20 | Font: Segoe UI, 9pt |
| 2.5 | `pnlMauDangChoi` | `Panel` | "" | (210, 72) | 50, 26 | `BorderStyle = FixedSingle`, `BackColor = Crimson` |
| 2.6 | `btnDoiMauDangChoi` | `Button` | "🎨 Đổi màu..." | (270, 70) | 110, 30 | `Click: btnDoiMauDangChoi_Click` |
| 2.7 | `lblMauBaoTri` | `Label` | "Màu máy Bảo trì / Khóa:" | (20, 115) | 180, 20 | Font: Segoe UI, 9pt |
| 2.8 | `pnlMauBaoTri` | `Panel` | "" | (210, 112) | 50, 26 | `BorderStyle = FixedSingle`, `BackColor = Goldenrod` |
| 2.9 | `btnDoiMauBaoTri` | `Button` | "🎨 Đổi màu..." | (270, 110) | 110, 30 | `Click: btnDoiMauBaoTri_Click` |
| 2.10| `lblFontHienThi` | `Label` | "Phông chữ hiển thị:" | (20, 165) | 180, 20 | Font: Segoe UI, 9pt |
| 2.11| `txtFontHienThi` | `TextBox` | "Segoe UI, 10pt" | (20, 195) | 360, 27 | `ReadOnly = True` |
| 2.12| `btnChonFont` | `Button` | "🔤 Chọn Font..." | (390, 193) | 125, 30 | `Click: btnChonFont_Click` |
| **3** | `gbXemTruoc` | `GroupBox` | "Xem trước máy trạm (Live Preview)" | (580, 60) | 385, 260 | Top, Right |
| 3.1 | `pnlPreviewMay` | `Panel` | - | (20, 35) | 345, 200 | `BorderStyle = FixedSingle`, `BackColor = Crimson` |
| 3.2 | `lblPreviewTenMay` | `Label` | "MÁY 01 - ĐANG SỬ DỤNG" | (15, 20) | 315, 28 | ForeColor: White, Font: 11pt Bold |
| 3.3 | `lblPreviewHoiVien`| `Label` | "Hội viên: tran_van_nam" | (15, 65) | 315, 24 | ForeColor: White |
| 3.4 | `lblPreviewThoiGian`| `Label` | "Thời gian còn: 03:15:20" | (15, 105) | 315, 24 | ForeColor: White |
| 3.5 | `lblPreviewTien` | `Label` | "Số dư: 50.000 VNĐ" | (15, 145) | 315, 24 | ForeColor: White |
| **4** | `gbSaoLuu` | `GroupBox` | "Sao lưu & Phục hồi cơ sở dữ liệu (.bak)" | (20, 335) | 945, 200 | Top, Left, Right |
| 4.1 | `lblThuMucBackup` | `Label` | "Thư mục lưu trữ bản sao lưu:" | (20, 35) | 220, 20 | - |
| 4.2 | `txtThuMucBackup` | `TextBox` | "D:\LapTrinhWin_DoAn\Backup" | (20, 65) | 730, 27 | `ReadOnly = True` |
| 4.3 | `btnChonThuMuc` | `Button` | "📁 Chọn thư mục..." | (765, 63) | 160, 31 | `Click: btnChonThuMuc_Click` |
| 4.4 | `btnSaoLuu` | `Button` | "💾 Sao lưu ngay (.bak)" | (20, 110) | 200, 38 | `Click: btnSaoLuu_Click`, BackColor: LightSkyBlue |
| 4.5 | `btnKhoiPhuc` | `Button` | "↺ Phục hồi từ file .bak"| (235, 110) | 200, 38 | `Click: btnKhoiPhuc_Click` |
| 4.6 | `prgTienTrinh` | `ProgressBar` | - | (450, 115) | 475, 28 | `Style = Continuous`, `Value = 0` |
| 4.7 | `lblTrangThaiBackup`| `Label` | "Trạng thái: Sẵn sàng thực hiện sao lưu dữ liệu." | (20, 160) | 900, 20 | ForeColor: DimGray |
| **5** | `pnlChucNang` | `Panel` | - | (20, 550) | 945, 60 | Bottom, Left, Right; `BorderStyle = FixedSingle` |
| 5.1 | `btnLuuCaiDat` | `Button` | "💾 Lưu cấu hình" | (200, 10) | 150, 40 | `Click: btnLuuCaiDat_Click` |
| 5.2 | `btnMacDinh` | `Button` | "🔄 Mặc định" | (390, 10) | 140, 40 | `Click: btnMacDinh_Click` |
| 5.3 | `btnDong` | `Button` | "❌ Đóng" | (570, 10) | 140, 40 | `Click: btnDong_Click` |
| **6** | `colorDialog1` | `ColorDialog` | - | Component Tray | - | `FullOpen = True`, `AnyColor = True` |
| **7** | `fontDialog1` | `FontDialog` | - | Component Tray | - | `ShowColor = False`, `ShowEffects = True` |
| **8** | `folderBrowserDialog1`| `FolderBrowserDialog` | - | Component Tray | - | `Description = "Chọn thư mục lưu file .bak"` |
| **9** | `openFileDialog1`| `OpenFileDialog` | - | Component Tray | - | `Filter = "SQL Backup Files (*.bak)|*.bak|All Files (*.*)|*.*"` |

---

## Chi tiết cách thức hoạt động của 3 Dialogs & Khung xem trước

### 1. `ColorDialog` (Đổi màu máy trạm)
```csharp
private void btnDoiMauMay_Click(object sender, EventArgs e)
{
    colorDialog1.Color = pnlMauDangChoi.BackColor;
    if (colorDialog1.ShowDialog() == DialogResult.OK)
    {
        // 1. Cập nhật màu vào ô hiển thị màu
        pnlMauDangChoi.BackColor = colorDialog1.Color;
        
        // 2. Cập nhật ngay lập tức vào khung Xem trước (Live Preview)
        pnlPreviewMay.BackColor = colorDialog1.Color;
    }
}
```

### 2. `FontDialog` (Đổi phông chữ)
```csharp
private void btnChonFont_Click(object sender, EventArgs e)
{
    // Gán font hiện tại vào dialog
    fontDialog1.Font = pnlPreviewMay.Font;
    if (fontDialog1.ShowDialog() == DialogResult.OK)
    {
        // 1. Hiển thị thông tin font lên TextBox
        txtFontHienThi.Text = $"{fontDialog1.Font.Name}, {fontDialog1.Font.SizeInPoints:0.#}pt, {fontDialog1.Font.Style}";
        
        // 2. Áp dụng font mới ngay lập tức cho các nhãn trong khung xem trước
        lblPreviewTenMay.Font = new Font(fontDialog1.Font.FontFamily, fontDialog1.Font.Size + 2, FontStyle.Bold);
        lblPreviewHoiVien.Font = fontDialog1.Font;
        lblPreviewThoiGian.Font = fontDialog1.Font;
        lblPreviewTien.Font = fontDialog1.Font;
    }
}
```

### 3. `FolderBrowserDialog` (Chọn thư mục Backup)
```csharp
private void btnChonThuMuc_Click(object sender, EventArgs e)
{
    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
    {
        txtThuMucBackup.Text = folderBrowserDialog1.SelectedPath;
    }
}
```

### 4. Sao lưu CSDL ra file `.bak`
```csharp
private void btnSaoLuu_Click(object sender, EventArgs e)
{
    string targetFolder = txtThuMucBackup.Text.Trim();
    if (!Directory.Exists(targetFolder))
    {
        Directory.CreateDirectory(targetFolder);
    }

    string fileName = $"NPC_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
    string fullPath = Path.Combine(targetFolder, fileName);

    prgTienTrinh.Value = 30;
    lblTrangThaiBackup.Text = "Đang kết nối CSDL và tạo bản sao lưu...";
    
    // Gọi lệnh sao lưu CSDL (BACKUP DATABASE)
    bool ok = DatabaseBackupHelper.Backup(fullPath, out string errorMsg);
    if (ok)
    {
        prgTienTrinh.Value = 100;
        lblTrangThaiBackup.Text = $"Sao lưu thành công: {fileName}";
        MessageBox.Show($"Sao lưu dữ liệu thành công!\nĐường dẫn: {fullPath}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    else
    {
        prgTienTrinh.Value = 0;
        lblTrangThaiBackup.Text = "Sao lưu thất bại!";
        MessageBox.Show($"Lỗi sao lưu: {errorMsg}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

---

## Đề xuất thay đổi mã nguồn (Proposed Changes)

### 1. Tầng DTO / Models
#### [NEW] [GiaoDienSettingsDTO.cs](file:///d:/LapTrinhWin_DoAn/NPC.DTO/GiaoDienSettingsDTO.cs)
- Chứa các thuộc tính lưu trữ màu sắc (dưới dạng mã màu ARGB hoặc Hex string), tên font, kích cỡ font, kiểu font, và đường dẫn thư mục backup.

---

### 2. Tầng DAL / Helpers
#### [NEW] [DatabaseBackupDAL.cs](file:///d:/LapTrinhWin_DoAn/NPC.DAL/DatabaseBackupDAL.cs)
- Cung cấp hàm thực hiện câu lệnh SQL `BACKUP DATABASE` và `RESTORE DATABASE` kết hợp kiểm tra quyền truy cập thư mục.

#### [NEW] [SettingsManager.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/Helpers/SettingsManager.cs)
- Đọc/ghi cấu hình giao diện ra file `app_settings.json` trong thư mục chạy của Server.

---

### 3. Tầng Giao diện (`NPC.Server`)
#### [NEW] [frmCaiDatGiaoDien.Designer.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmCaiDatGiaoDien.Designer.cs)
- Khai báo đầy đủ các GroupBox, Panel màu, Buttons, ProgressBar, và 3 Dialogs trên Component Tray.

#### [NEW] [frmCaiDatGiaoDien.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmCaiDatGiaoDien.cs)
- Xử lý các sự kiện click mở `ColorDialog`, `FontDialog`, `FolderBrowserDialog`.
- Cập nhật Live Preview khi người dùng chọn màu hoặc font.
- Xử lý sao lưu `.bak` và lưu cấu hình xuống file.

#### [NEW] [frmCaiDatGiaoDien.resx](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmCaiDatGiaoDien.resx)
- Resource file chuẩn XML của Form.

---

## Kế hoạch kiểm tra & xác minh (Verification Plan)

### Kiểm tra tự động qua Build (Automated Verification)
- Chạy `dotnet build d:\LapTrinhWin_DoAn\NPC.Server\NPC.Server.csproj` đảm bảo biên dịch thành công 100% không cảnh báo/lỗi.

### Kiểm tra thủ công (Manual Verification)
1. **Kiểm tra giao diện kéo thả Designer**:
   - Mở `frmCaiDatGiaoDien.cs` ở chế độ `[Design]` trên Visual Studio để kiểm tra các GroupBox, Panel màu, Preview Card và các Dialogs ở Component Tray hiển thị chuẩn xác.
2. **Kiểm tra `ColorDialog`**:
   - Bấm nút "Đổi màu...", chọn 1 màu mới $\rightarrow$ Kiểm tra ô màu nhỏ và khung xem trước (Live Preview) đổi màu tương ứng ngay lập tức.
3. **Kiểm tra `FontDialog`**:
   - Bấm nút "Chọn Font...", chọn font Tahoma cỡ 12 Bold $\rightarrow$ Tên font hiển thị lên ô Text và chữ trong khung xem trước tự động đổi font theo.
4. **Kiểm tra `FolderBrowserDialog`**:
   - Bấm "Chọn thư mục..." $\rightarrow$ Cửa sổ duyệt cây thư mục mở ra, chọn 1 thư mục bất kỳ $\rightarrow$ Đường dẫn được điền vào ô đường dẫn.
5. **Kiểm tra Sao lưu .bak**:
   - Bấm "Sao lưu ngay (.bak)" $\rightarrow$ Kiểm tra file `.bak` được tạo trong thư mục đích và ProgressBar chạy đạt 100%.
6. **Kiểm tra Lưu cấu hình**:
   - Bấm "Lưu cấu hình", tắt Form đi mở lại $\rightarrow$ Màu sắc, font chữ và thư mục backup vẫn được nạp lại nguyên vẹn.

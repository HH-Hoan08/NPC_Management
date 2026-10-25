# Kế hoạch Xây dựng Form Quản lý Thực đơn & Kho (`frmQuanLyMonAn`)

## Mô tả mục tiêu (Goal Description)
Xây dựng phân hệ **Quản lý Thực đơn & Kho hàng (Món ăn / Đồ uống / Dịch vụ)** cho phòng máy NPC tại dự án `NPC.Server`, bao gồm:
1. Tạo giao diện Windows Forms `frmQuanLyMonAn` hiện đại, bố cục khoa học, chuẩn hóa cho Visual Studio Designer.
2. Cung cấp **Bảng đặc tả chi tiết từng phần tử (Controls Table)**: Liệt kê rõ loại control, Name, Text, Location, Size, Anchor, thuộc tính hình ảnh để lập trình viên kéo thả bằng tay hoặc dùng code sinh tự động.
3. Tích hợp **`OpenFileDialog`** để duyệt và chọn ảnh món ăn từ máy tính hiển thị lên `PictureBox`.
4. Áp dụng kỹ thuật **`FileStream` + `MemoryStream`** khi tải và lưu ảnh để **chống khóa file (File Locking Issue)**, đảm bảo người dùng có thể xóa, sửa, ghi đè ảnh gốc mà không bao giờ gặp lỗi `IOException: The process cannot access the file because it is being used by another process`.
5. Xây dựng cấu trúc theo mô hình 3 lớp:
   - `NPC.DTO`: `MonAnDTO.cs`
   - `NPC.DAL`: `MonAnDAL.cs` (có sẵn dữ liệu mẫu thực đơn đồ ăn, nước uống phong phú)
   - `NPC.BLL`: `MonAnBLL.cs` (kiểm tra nghiệp vụ giá bán, giá nhập, tồn kho, tên món)
   - `NPC.Server`: `frmQuanLyMonAn.cs`, `frmQuanLyMonAn.Designer.cs`, `frmQuanLyMonAn.resx`

---

## Cần người dùng xem xét (User Review Required)

> [!IMPORTANT]
> **Vấn đề khóa file của `Image.FromFile()` trong C# WinForms**:  
> Lệnh thông thường `picAnh.Image = Image.FromFile(path)` sẽ khóa vĩnh viễn file ảnh trên đĩa cứng cho đến khi ứng dụng tắt, khiến người dùng không thể đổi ảnh, xóa ảnh hoặc ghi đè.  
> Chúng tôi sẽ triển khai hàm chuẩn `ImageHelper.LoadImageCopy(string path)` sử dụng cặp `FileStream(FileShare.ReadWrite)` và `MemoryStream` để nạp bản sao độc lập vào RAM, trả lại quyền truy cập tự do cho file ảnh trên ổ cứng.

> [!TIP]
> **Thư mục lưu trữ ảnh món ăn**:  
> Ảnh khi người dùng chọn sẽ được copy tự động vào thư mục `Images/MonAn/` trong thư mục chạy của Server và quản lý bằng tên file mã hóa theo `MaMon` (ví dụ: `MA001.png`, `MA002.jpg`) để tránh trùng tên hoặc đường dẫn quá dài.

---

## Bảng đặc tả chi tiết từng phần tử phục vụ kéo thả (Visual Studio Form Designer)

### Sơ đồ bố cục giao diện tổng quan:

```
+-------------------------------------------------------------------------------------------------------------------------+
|                                             lblTieuDe: QUẢN LÝ THỰC ĐƠN & KHO HÀNG                                      |
+-----------------------------------------------------------------------------------------------+-------------------------+
|  gbThongTin (GroupBox: Thông tin món ăn & kho)                                                | gbHinhAnh (GroupBox)    |
|  lblMaMon   [ txtMaMon    ]  lblDonGia    [ txtDonGia  ]  lblSoLuongTon [ txtSoLuongTon ]     | +---------------------+ |
|  lblTenMon  [ txtTenMon   ]  lblGiaNhap   [ txtGiaNhap ]  lblDonViTinh  [ cboDonViTinh  ]     | | picHinhAnh          | |
|  lblLoaiMon [ cboLoaiMon  ]  lblTrangThai [ cboTrangThai]                                     | | (PictureBox)        | |
|                                                                                               | +---------------------+ |
|                                                                                               | [ btnChonAnh ][btnXoa]| |
+-----------------------------------------------------------------------------------------------+-------------------------+
|  gbTimKiem (GroupBox: Tìm kiếm & Lọc thực đơn)                                                                          |
|  lblTimKiem [ txtTimKiem           ]  lblLocLoai [ cboLocLoai    ]  [ btnTimKiem ]  [ btnLamMoiTim ] [ chkTonKhoThap ]  |
+-------------------------------------------------------------------------------------------------------------------------+
|  pnlChucNang (Panel chứa các nút chức năng):                                                                            |
|  [ btnThem ]  [ btnSua ]  [ btnXoa ]  [ btnNhapLai ]  [ btnNhapKho ]  [ btnDong ]                                       |
+-------------------------------------------------------------------------------------------------------------------------+
|  gbDanhSach (GroupBox: Danh sách món ăn trong kho)                                                                      |
|  +-------------------------------------------------------------------------------------------------------------------+  |
|  | dgvMonAn (DataGridView): Mã Món | Tên Món | Loại | Giá Bán | Giá Vốn | Tồn Kho | ĐVT | Trạng Thái | File Ảnh        |  |
|  +-------------------------------------------------------------------------------------------------------------------+  |
|  lblTongSoMon: "Tổng số: 0 món"       lblTongTonKho: "Tổng tồn kho: 0"       lblCanhBaoHetHang: "Sắp hết hàng: 0 món"   |
+-------------------------------------------------------------------------------------------------------------------------+
|  Thành phần phi giao diện (Non-visual Component Tray):                                                                  |
|  - ofdChonAnh (OpenFileDialog): Filter = "Image Files (*.jpg;*.png;*.bmp;*.jpeg)|*.jpg;*.png;*.bmp;*.jpeg"             |
+-------------------------------------------------------------------------------------------------------------------------+
```

### Bảng thông số chi tiết từng Control:

| STT | Tên Control (`(Name)`) | Loại Control (`Type`) | Text / Nội dung | Vị trí (`Location`) | Kích thước (`Size`) | Anchor / Thuộc tính quan trọng |
| :-- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | `frmQuanLyMonAn` | `Form` | "Quản Lý Thực Đơn & Kho - NPC Server" | CenterScreen | 1120, 720 | `StartPosition = CenterScreen` |
| **1** | `lblTieuDe` | `Label` | "QUẢN LÝ THỰC ĐƠN & KHO HÀNG" | (400, 12) | 350, 32 | Top; Font: Segoe UI, 14pt Bold |
| **2** | `gbThongTin` | `GroupBox` | "Thông tin món ăn & kho" | (15, 50) | 830, 160 | Top, Left, Right |
| 2.1 | `lblMaMon` | `Label` | "Mã món:" | (15, 30) | 80, 20 | - |
| 2.2 | `txtMaMon` | `TextBox` | "" | (95, 27) | 160, 25 | `(Name) = txtMaMon` |
| 2.3 | `lblTenMon` | `Label` | "Tên món:" | (15, 70) | 80, 20 | - |
| 2.4 | `txtTenMon` | `TextBox` | "" | (95, 67) | 160, 25 | `(Name) = txtTenMon` |
| 2.5 | `lblLoaiMon` | `Label` | "Loại món:" | (15, 110) | 80, 20 | - |
| 2.6 | `cboLoaiMon` | `ComboBox` | (Nước uống, Đồ ăn nhanh, Cơm, Mì, Snack, Thẻ...) | (95, 107) | 160, 25 | `DropDownStyle = DropDownList` |
| 2.7 | `lblDonGia` | `Label` | "Giá bán (VNĐ):" | (280, 30) | 95, 20 | - |
| 2.8 | `txtDonGia` | `TextBox` | "0" | (385, 27) | 150, 25 | `(Name) = txtDonGia` |
| 2.9 | `lblGiaNhap` | `Label` | "Giá nhập (VNĐ):" | (280, 70) | 95, 20 | - |
| 2.10 | `txtGiaNhap` | `TextBox` | "0" | (385, 67) | 150, 25 | `(Name) = txtGiaNhap` |
| 2.11 | `lblTrangThai` | `Label` | "Trạng thái:" | (280, 110) | 95, 20 | - |
| 2.12 | `cboTrangThai` | `ComboBox` | (Còn hàng, Hết hàng, Ngừng bán) | (385, 107) | 150, 25 | `DropDownStyle = DropDownList` |
| 2.13 | `lblSoLuongTon`| `Label` | "Số lượng tồn:" | (560, 30) | 95, 20 | - |
| 2.14 | `txtSoLuongTon`| `TextBox` | "0" | (665, 27) | 145, 25 | `(Name) = txtSoLuongTon` |
| 2.15 | `lblDonViTinh` | `Label` | "Đơn vị tính:" | (560, 70) | 95, 20 | - |
| 2.16 | `cboDonViTinh` | `ComboBox` | (Chai, Lon, Tô, Đĩa, Gói, Phần, Cái...) | (665, 67) | 145, 25 | - |
| **3** | `gbHinhAnh` | `GroupBox` | "Hình ảnh món" | (860, 50) | 230, 225 | Top, Right |
| 3.1 | `picHinhAnh` | `PictureBox` | - | (15, 25) | 200, 145 | `BorderStyle = FixedSingle`, `SizeMode = Zoom` |
| 3.2 | `btnChonAnh` | `Button` | "📁 Chọn ảnh" | (15, 180) | 95, 34 | `Click: btnChonAnh_Click` |
| 3.3 | `btnXoaAnh` | `Button` | "✖ Xóa ảnh" | (120, 180) | 95, 34 | `Click: btnXoaAnh_Click` |
| **4** | `gbTimKiem` | `GroupBox` | "Tìm kiếm & Lọc thực đơn" | (15, 215) | 830, 60 | Top, Left, Right |
| 4.1 | `lblTimKiem` | `Label` | "Tìm tên / mã:" | (15, 24) | 90, 20 | - |
| 4.2 | `txtTimKiem` | `TextBox` | "" | (105, 21) | 180, 25 | `TextChanged: txtTimKiem_TextChanged` |
| 4.3 | `lblLocLoai` | `Label` | "Loại món:" | (300, 24) | 70, 20 | - |
| 4.4 | `cboLocLoai` | `ComboBox` | ("-- Tất cả loại --", ...) | (375, 21) | 140, 25 | `SelectedIndexChanged` |
| 4.5 | `chkTonKhoThap`| `CheckBox` | "Cảnh báo sắp hết (< 5)" | (530, 23) | 160, 22 | `CheckedChanged` |
| 4.6 | `btnLamMoiTim`| `Button` | "↺ Đặt lại" | (700, 18) | 110, 32 | `Click: btnLamMoiTim_Click` |
| **5** | `pnlChucNang` | `Panel` | - | (15, 283) | 1075, 52 | Top, Left, Right; `BorderStyle = FixedSingle` |
| 5.1 | `btnThem` | `Button` | "➕ Thêm món" | (80, 8) | 120, 36 | `Click: btnThem_Click` |
| 5.2 | `btnSua` | `Button` | "✏ Sửa món" | (220, 8) | 120, 36 | `Click: btnSua_Click` |
| 5.3 | `btnXoa` | `Button` | "🗑 Xóa món" | (360, 8) | 120, 36 | `Click: btnXoa_Click` |
| 5.4 | `btnNhapLai` | `Button` | "🔄 Nhập lại" | (500, 8) | 120, 36 | `Click: btnNhapLai_Click` |
| 5.5 | `btnNhapKho` | `Button` | "📦 Nhập thêm kho"| (640, 8) | 135, 36 | `Click: btnNhapKho_Click` |
| 5.6 | `btnDong` | `Button` | "❌ Đóng" | (795, 8) | 110, 36 | `Click: btnDong_Click` |
| **6** | `gbDanhSach` | `GroupBox` | "Danh sách thực đơn & kho" | (15, 342) | 1075, 325 | All (Top, Bottom, Left, Right) |
| 6.1 | `dgvMonAn` | `DataGridView` | - | (15, 25) | 1045, 260 | All; `SelectionMode = FullRowSelect`, `ReadOnly = True` |
| 6.2 | `lblTongSoMon` | `Label` | "Tổng số món: 0" | (15, 295) | 180, 20 | Bottom, Left |
| 6.3 | `lblTongTonKho`| `Label` | "Tổng số lượng tồn: 0" | (250, 295) | 200, 20 | Bottom, Left |
| 6.4 | `lblCanhBao` | `Label` | "Sắp hết hàng: 0" | (500, 295) | 200, 20 | Bottom, Left; ForeColor: Red |
| **7** | `ofdChonAnh` | `OpenFileDialog` | - | Component Tray | - | `Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"` |

---

## Giải pháp Kỹ thuật: Chống khóa file ảnh (File Locking Avoidance)

### Vấn đề thường gặp:
```csharp
// ❌ SAI: Gây khóa file vĩnh viễn trên đĩa cứng
picHinhAnh.Image = Image.FromFile(duongDanFile);
// Nếu người dùng cố tình cập nhật ảnh mới đè lên file cũ, hoặc xóa ảnh, Windows sẽ báo:
// System.IO.IOException: The process cannot access the file because it is being used by another process.
```

### Giải pháp kỹ thuật chuẩn `MemoryStream` & `FileStream`:
Chúng ta tạo một class tiện ích `ImageHelper`:
```csharp
public static class ImageHelper
{
    // Đọc file ảnh an toàn bằng MemoryStream để hoàn toàn giải phóng FileStream ngay sau khi đọc
    public static Image? LoadImageNoLock(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            return null;

        try
        {
            // Mở file ở chế độ chỉ đọc và cho phép chia sẻ đọc/ghi (FileShare.ReadWrite)
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    ms.Position = 0; // Đặt con trỏ về đầu luồng bộ nhớ
                    return Image.FromStream(ms); // Tạo Image từ RAM, không giữ handle tới file ổ cứng
                }
            }
        }
        catch
        {
            return null;
        }
    }

    // Giải phóng an toàn ảnh cũ trên PictureBox trước khi gán ảnh mới
    public static void SetImageSafely(PictureBox pic, string? filePath)
    {
        if (pic.Image != null)
        {
            var oldImg = pic.Image;
            pic.Image = null;
            oldImg.Dispose(); // Giải phóng RAM ảnh cũ
        }

        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            pic.Image = LoadImageNoLock(filePath);
        }
    }
}
```

---

## Đề xuất thay đổi mã nguồn (Proposed Changes)

### 1. Tầng DTO (`NPC.DTO`)
#### [NEW] [MonAnDTO.cs](file:///d:/LapTrinhWin_DoAn/NPC.DTO/MonAnDTO.cs)
```csharp
namespace NPC.DTO
{
    public class MonAnDTO
    {
        public string MaMon { get; set; } = string.Empty;
        public string TenMon { get; set; } = string.Empty;
        public string LoaiMon { get; set; } = "Nước uống";
        public decimal DonGia { get; set; } = 0;      // Giá bán cho khách
        public decimal GiaNhap { get; set; } = 0;     // Giá vốn nhập hàng
        public int SoLuongTon { get; set; } = 0;      // Tồn kho
        public string DonViTinh { get; set; } = "Phần";
        public string HinhAnh { get; set; } = string.Empty; // Đường dẫn hoặc tên file ảnh
        public string TrangThai { get; set; } = "Còn hàng"; // Còn hàng, Hết hàng, Ngừng bán
    }
}
```

---

### 2. Tầng DAL (`NPC.DAL`)
#### [NEW] [MonAnDAL.cs](file:///d:/LapTrinhWin_DoAn/NPC.DAL/MonAnDAL.cs)
- Cung cấp:
  - `List<MonAnDTO> LayTatCaMonAn()` (có sẵn danh sách mẫu: Sting dâu, Bò húc, Mì tôm trứng, Cơm chiên dương châu, Xúc xích, v.v.)
  - `bool ThemMonAn(MonAnDTO mon)`
  - `bool CapNhatMonAn(MonAnDTO mon)`
  - `bool XoaMonAn(string maMon)`
  - `bool CongSoLuongTon(string maMon, int soLuongThem)`

---

### 3. Tầng BLL (`NPC.BLL`)
#### [NEW] [MonAnBLL.cs](file:///d:/LapTrinhWin_DoAn/NPC.BLL/MonAnBLL.cs)
- Nghiệp vụ:
  - Kiểm tra `MaMon` không trống và không trùng khi thêm mới.
  - Kiểm tra `TenMon` không để trống.
  - Kiểm tra `DonGia >= 0`, `GiaNhap >= 0`, `SoLuongTon >= 0`.
  - Cảnh báo nếu `DonGia < GiaNhap` (bán lỗ).

---

### 4. Tầng Giao diện (`NPC.Server`)
#### [NEW] [ImageHelper.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/Helpers/ImageHelper.cs)
- Triển khai kỹ thuật `FileStream` + `MemoryStream` chống khóa file và hàm copy ảnh vào thư mục ứng dụng `Images/MonAn/`.

#### [NEW] [frmQuanLyMonAn.Designer.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmQuanLyMonAn.Designer.cs)
- Khai báo đầy đủ các control, kích thước, căn lề và đăng ký sự kiện cho Visual Studio Form Designer.

#### [NEW] [frmQuanLyMonAn.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmQuanLyMonAn.cs)
- Xử lý tương tác:
  - `btnChonAnh_Click`: Bật `ofdChonAnh`, nạp ảnh qua `ImageHelper.LoadImageNoLock()`.
  - `btnXoaAnh_Click`: Xóa ảnh hiện tại và gán `picHinhAnh.Image = null`.
  - `btnThem_Click`, `btnSua_Click`, `btnXoa_Click`, `btnNhapLai_Click`.
  - `btnNhapKho_Click`: Hộp thoại nhanh nhập số lượng cộng thêm vào kho.
  - Sự kiện chọn dòng `dgvMonAn_CellClick`: Đổ dữ liệu và load ảnh tương ứng lên `PictureBox`.
  - Tìm kiếm & Lọc tức thì theo loại món, cảnh báo tồn kho thấp.

#### [NEW] [frmQuanLyMonAn.resx](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmQuanLyMonAn.resx)
- Resource file chuẩn XML.

---

## Kế hoạch kiểm tra & xác minh (Verification Plan)

### Kiểm tra tự động qua Build (Automated Verification)
- Chạy `dotnet build` trên dự án `NPC.Server` để đảm bảo biên dịch 100% thành công không có lỗi cú pháp hay thiếu references.

### Kiểm tra thủ công (Manual Verification)
1. **Kiểm tra giao diện kéo thả**:
   - Mở file `frmQuanLyMonAn.cs` ở chế độ `[Design]` trong Visual Studio để xác nhận các GroupBox, PictureBox, DataGridView, Buttons hiển thị đẹp mắt, ngay ngắn, chuẩn tọa độ.
2. **Kiểm tra chọn ảnh & chống khóa file**:
   - Nhấn "Chọn ảnh", duyệt chọn 1 file ảnh từ máy.
   - Ảnh hiển thị vừa vặn trong PictureBox (SizeMode = Zoom).
   - Trong khi Form đang mở, ra ngoài File Explorer xóa hoặc đổi tên file ảnh gốc đó $\rightarrow$ Windows cho phép thao tác bình thường, **không bị khóa file**.
3. **Kiểm tra CRUD & Quản lý Kho**:
   - Thêm món ăn mới kèm ảnh, kiểm tra dòng mới xuất hiện trên DataGridView.
   - Sửa giá bán, cập nhật số lượng tồn kho.
   - Bấm "Nhập thêm kho" để cộng dồn số lượng.
   - Lọc theo loại món và kiểm tra bộ lọc "Cảnh báo sắp hết (< 5)".

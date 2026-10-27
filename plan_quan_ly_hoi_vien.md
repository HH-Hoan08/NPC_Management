# Kế hoạch Xây dựng Form Quản lý Hội viên (`frmQuanLyHoiVien`)

## Mô tả mục tiêu (Goal Description)
Xây dựng phân hệ **Quản lý Hội viên** cho ứng dụng Server (`NPC.Server`), bao gồm:
1. Tạo giao diện Windows Forms `frmQuanLyHoiVien` trực quan, hiện đại, chuẩn mô hình kéo thả của Visual Studio Designer (`frmQuanLyHoiVien.Designer.cs`).
2. Cung cấp **Bảng đặc tả chi tiết từng phần tử (Controls)**: Liệt kê rõ loại control, Name, Text, Location, Size, Dock/Anchor, Font, Sự kiện để lập trình viên có thể dễ dàng kéo thả bằng tay trên Toolbox của Visual Studio Form Designer.
3. Tích hợp thành phần **`SaveFileDialog`** cho phép xuất danh sách hội viên ra file Text (`.txt`) và CSV (`.csv`) với định dạng cột bảng rõ ràng, chuyên nghiệp.
4. Xây dựng cấu trúc dữ liệu theo mô hình 3 lớp chuẩn:
   - `NPC.DTO`: `HoiVienDTO`
   - `NPC.DAL`: `HoiVienDAL` (tương tác dữ liệu, hỗ trợ nạp sẵn dữ liệu mẫu/mock khi chưa gắn DB LocalDB và truy vấn DB khi có kết nối)
   - `NPC.BLL`: `HoiVienBLL` (kiểm tra ràng buộc nghiệp vụ: kiểm tra trùng mã, số điện thoại, số dư, tên hợp lệ)
   - `NPC.Server`: `frmQuanLyHoiVien` (Giao diện hiển thị, CRUD, Tìm kiếm, Lọc, Xuất Text)
5. Cập nhật `TargetFramework` từ `net10.0` sang `net9.0` trong các file `.csproj` để tương thích hoàn toàn với .NET SDK 9.0 hiện có trên máy.

---

## Cần người dùng xem xét (User Review Required)

> [!IMPORTANT]
> **Phiên bản .NET SDK hiện hành**: Máy tính hiện đang cài .NET SDK 9.0 (`9.0.305`). Các file `.csproj` trong repo đang khai báo `net10.0`, dẫn đến lỗi build `NETSDK1045`. Chúng tôi sẽ cập nhật các file `.csproj` sang `net9.0` / `net9.0-windows` để có thể biên dịch và chạy ngay lập tức.

> [!NOTE]
> **Hỗ trợ chế độ Mock Data**: Để Form có thể chạy thử nghiệm và kiểm tra chức năng xuất file text ngay cả khi bạn chưa cấu hình xong LocalDB SQL Server, tầng DAL sẽ tự động nạp danh sách hội viên mẫu nếu kết nối CSDL chưa sẵn sàng.

---

## Bảng đặc tả chi tiết từng phần tử phục vụ kéo thả (Visual Studio Form Designer)

Dưới đây là sơ đồ bố cục và thông số từng control trong Toolbox của Visual Studio để bạn có thể tự kéo thả hoặc đối chiếu với file `frmQuanLyHoiVien.Designer.cs`:

```
+-----------------------------------------------------------------------------------------------------------------+
|                                           lblTieuDe: QUẢN LÝ HỘI VIÊN                                           |
+-----------------------------------------------------------------------------------------------------------------+
|  grpThongTin (GroupBox: Thông tin hội viên)                     |  grpTimKiem (GroupBox: Tìm kiếm hội viên)     |
|  lblMaHV     [ txtMaHV         ]  lblSoDu   [ txtSoDu        ]  |  lblTimKiem [ txtTimKiem           ]          |
|  lblHoTen    [ txtHoTen        ]  lblTrangThai [ cboTrangThai]  |  [ btnTimKiem ]  [ btnLamMoiTim ]             |
|  lblMatKhau  [ txtMatKhau      ]                                |                                               |
|  lblSDT      [ txtSoDienThoai  ]                                |                                               |
+-----------------------------------------------------------------------------------------------------------------+
|  pnlChucNang (Panel chứa các nút chức năng):                                                                    |
|  [ btnThem ]  [ btnSua ]  [ btnXoa ]  [ btnLamMoi ]  [ btnNapTien ]  |  [ btnXuatText ]  [ btnDong ]            |
+-----------------------------------------------------------------------------------------------------------------+
|  grpDanhSach (GroupBox: Danh sách hội viên)                                                                     |
|  +-----------------------------------------------------------------------------------------------------------+  |
|  | dgvHoiVien (DataGridView): Mã HV | Họ Tên | Mật Khẩu | SĐT | Số Dư (VNĐ) | Thời Gian | Trạng Thái | Ngày Tạo|  |
|  +-----------------------------------------------------------------------------------------------------------+  |
|  lblTongSoHoiVien: "Tổng số: 0 hội viên"                 lblTongSoDu: "Tổng số dư: 0 đ"                        |
+-----------------------------------------------------------------------------------------------------------------+
|  Thành phần phi giao diện (Non-visual Components):                                                             |
|  - sfdXuatFile (SaveFileDialog): Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv"                    |
+-----------------------------------------------------------------------------------------------------------------+
```

### Chi tiết thông số từng Control:

| STT | Tên Control (`(Name)`) | Loại Control (`Type`) | Text / Caption | Vị trí (`Location`) | Kích thước (`Size`) | Anchor / Dock | Sự kiện chính (`Event`) |
| :-- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | `frmQuanLyHoiVien` | `Form` | "Quản Lý Hội Viên - NPC Server" | Giữa màn hình | 960, 680 | - | `Load`, `FormClosing` |
| **1** | `lblTieuDe` | `Label` | "QUẢN LÝ HỘI VIÊN" | (20, 15) | 920, 40 | Top, Left, Right | - |
| **2** | `grpThongTin` | `GroupBox` | "Thông tin hội viên" | (20, 65) | 580, 185 | Top, Left | - |
| 2.1 | `lblMaHV` | `Label` | "Mã / Tên đăng nhập:" | (15, 30) | 130, 20 | - | - |
| 2.2 | `txtMaHV` | `TextBox` | "" | (150, 27) | 140, 25 | - | - |
| 2.3 | `lblHoTen` | `Label` | "Họ và tên:" | (15, 68) | 130, 20 | - | - |
| 2.4 | `txtHoTen` | `TextBox` | "" | (150, 65) | 140, 25 | - | - |
| 2.5 | `lblMatKhau` | `Label` | "Mật khẩu:" | (15, 106) | 130, 20 | - | - |
| 2.6 | `txtMatKhau` | `TextBox` | "" | (150, 103) | 140, 25 | - | - |
| 2.7 | `lblSDT` | `Label` | "Số điện thoại:" | (15, 144) | 130, 20 | - | - |
| 2.8 | `txtSoDienThoai` | `TextBox` | "" | (150, 141) | 140, 25 | - | - |
| 2.9 | `lblSoDu` | `Label` | "Số dư (VNĐ):" | (310, 30) | 100, 20 | - | - |
| 2.10 | `txtSoDu` | `TextBox` | "0" | (415, 27) | 145, 25 | - | - |
| 2.11 | `lblTrangThai` | `Label` | "Trạng thái:" | (310, 68) | 100, 20 | - | - |
| 2.12 | `cboTrangThai` | `ComboBox` | (Hoạt động, Đang online, Khóa) | (415, 65) | 145, 25 | - | - |
| **3** | `grpTimKiem` | `GroupBox` | "Tìm kiếm nhanh" | (615, 65) | 325, 185 | Top, Right | - |
| 3.1 | `lblTimKiem` | `Label` | "Nhập Mã HV, Họ tên hoặc SĐT:" | (15, 30) | 200, 20 | - | - |
| 3.2 | `txtTimKiem` | `TextBox` | "" | (15, 60) | 295, 25 | - | `TextChanged`, `KeyDown` |
| 3.3 | `btnTimKiem` | `Button` | "🔍 Tìm kiếm" | (15, 105) | 140, 35 | - | `Click: btnTimKiem_Click` |
| 3.4 | `btnLamMoiTim` | `Button` | "↺ Tất cả" | (170, 105) | 140, 35 | - | `Click: btnLamMoiTim_Click` |
| **4** | `pnlChucNang` | `Panel` | - | (20, 260) | 920, 48 | Top, Left, Right | - |
| 4.1 | `btnThem` | `Button` | "➕ Thêm mới" | (0, 5) | 110, 38 | - | `Click: btnThem_Click` |
| 4.2 | `btnSua` | `Button` | "✏ Sửa" | (120, 5) | 100, 38 | - | `Click: btnSua_Click` |
| 4.3 | `btnXoa` | `Button` | "🗑 Xóa" | (230, 5) | 100, 38 | - | `Click: btnXoa_Click` |
| 4.4 | `btnLamMoi` | `Button` | "🔄 Nhập lại" | (340, 5) | 110, 38 | - | `Click: btnLamMoi_Click` |
| 4.5 | `btnNapTien` | `Button` | "💰 Nạp tiền" | (460, 5) | 110, 38 | - | `Click: btnNapTien_Click` |
| 4.6 | `btnXuatText` | `Button` | "📄 Xuất File Text" | (660, 5) | 145, 38 | Top, Right | `Click: btnXuatText_Click` |
| 4.7 | `btnDong` | `Button` | "❌ Đóng" | (815, 5) | 100, 38 | Top, Right | `Click: btnDong_Click` |
| **5** | `grpDanhSach` | `GroupBox` | "Danh sách hội viên" | (20, 318) | 920, 310 | All (T, B, L, R) | - |
| 5.1 | `dgvHoiVien` | `DataGridView` | - | (15, 25) | 890, 245 | All (T, B, L, R) | `CellClick`, `SelectionChanged` |
| 5.2 | `lblTongSoHoiVien`| `Label` | "Tổng số: 0 hội viên" | (15, 280) | 250, 20 | Bottom, Left | - |
| 5.3 | `lblTongSoDu` | `Label` | "Tổng số dư: 0 VNĐ" | (650, 280) | 255, 20 | Bottom, Right | - |
| **6** | `sfdXuatFile` | `SaveFileDialog` | - | Component Tray | - | - | Được gọi trong `btnXuatText_Click` |

---

## Chi tiết xử lý `SaveFileDialog` xuất danh sách ra File Text

Khi người dùng nhấn vào nút **"📄 Xuất File Text" (`btnXuatText`)**:
1. Khởi tạo / cấu hình `SaveFileDialog`:
   - `Title`: "Chọn vị trí lưu danh sách hội viên"
   - `Filter`: `Text Document (*.txt)|*.txt|CSV File (*.csv)|*.csv|All Files (*.*)|*.*`
   - `FileName`: `DanhSachHoiVien_yyyyMMdd_HHmmss.txt`
   - `DefaultExt`: `txt`
2. Nếu người dùng chọn file và nhấn **Save** (`DialogResult.OK`):
   - Đọc dữ liệu hiện có trong bảng `DataGridView` (hoặc từ BLL/List).
   - Định dạng bảng văn bản căn lề cân đối, đẹp mắt:
     - Header: Thông tin phòng máy NPC, Ngày giờ xuất, Người thực hiện.
     - Đường phân cách gạch ngang (`--------------------------------------------------------------------------------`).
     - Bảng dữ liệu có tiêu đề cột: `| STT | Mã HV | Họ Và Tên | Số Điện Thoại | Số Dư (VNĐ) | Trạng Thái | Ngày Tạo |`
     - Footer: Tổng số hội viên, Tổng số tiền dư trong tài khoản.
   - Ghi file bằng `StreamWriter` với `Encoding.UTF8` (đảm bảo không lỗi font tiếng Việt).
   - Hiển thị `MessageBox`:
     > "Xuất file thành công! Bạn có muốn mở file vừa xuất ngay bây giờ không?"
   - Nếu người dùng chọn **Yes**, tự động mở file bằng ứng dụng mặc định (Notepad, v.v.) qua `Process.Start`.

---

## Đề xuất thay đổi mã nguồn (Proposed Changes)

### 1. Cấu hình Project & SDK
#### [MODIFY] [NPC.DTO.csproj](file:///d:/LapTrinhWin_DoAn/NPC.DTO/NPC.DTO.csproj)
- Chuyển `<TargetFramework>` từ `net10.0` sang `net9.0`.

#### [MODIFY] [NPC.DAL.csproj](file:///d:/LapTrinhWin_DoAn/NPC.DAL/NPC.DAL.csproj)
- Chuyển `<TargetFramework>` từ `net10.0` sang `net9.0`.
- Thêm tham chiếu đến `NPC.DTO`.

#### [MODIFY] [NPC.BLL.csproj](file:///d:/LapTrinhWin_DoAn/NPC.BLL/NPC.BLL.csproj)
- Chuyển `<TargetFramework>` từ `net10.0` sang `net9.0`.
- Thêm tham chiếu đến `NPC.DTO` và `NPC.DAL`.

#### [MODIFY] [NPC.Server.csproj](file:///d:/LapTrinhWin_DoAn/NPC.Server/NPC.Server.csproj)
- Chuyển `<TargetFramework>` từ `net10.0-windows` sang `net9.0-windows`.
- Thêm tham chiếu đến `NPC.DTO` và `NPC.BLL`.

---

### 2. Tầng DTO (`NPC.DTO`)
#### [NEW] [HoiVienDTO.cs](file:///d:/LapTrinhWin_DoAn/NPC.DTO/HoiVienDTO.cs)
- Lớp dữ liệu đại diện cho hội viên phòng net:
```csharp
public class HoiVienDTO
{
    public string MaHoiVien { get; set; } = string.Empty;
    public string TenHoiVien { get; set; } = string.Empty;
    public string MatKhau { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public decimal SoDu { get; set; } = 0;
    public int ThoiGianConLaiPhut { get; set; } = 0; // Quy đổi theo đơn giá giờ chơi
    public string TrangThai { get; set; } = "Hoạt động"; // Hoạt động, Đang online, Bị khóa
    public DateTime NgayTao { get; set; } = DateTime.Now;
}
```

---

### 3. Tầng DAL (`NPC.DAL`)
#### [NEW] [HoiVienDAL.cs](file:///d:/LapTrinhWin_DoAn/NPC.DAL/HoiVienDAL.cs)
- Cung cấp các phương thức CRUD và tìm kiếm:
  - `List<HoiVienDTO> LayTatCaHoiVien()`
  - `bool ThemHoiVien(HoiVienDTO hv)`
  - `bool CapNhatHoiVien(HoiVienDTO hv)`
  - `bool XoaHoiVien(string maHoiVien)`
  - `bool NapTien(string maHoiVien, decimal soTien)`
  - `List<HoiVienDTO> TimKiemHoiVien(string tuKhoa)`
- Tự động nạp danh sách mẫu (Mock In-Memory) nếu database chưa được cấu hình hoặc bảng chưa tồn tại, giúp form test được ngay.

---

### 4. Tầng BLL (`NPC.BLL`)
#### [NEW] [HoiVienBLL.cs](file:///d:/LapTrinhWin_DoAn/NPC.BLL/HoiVienBLL.cs)
- Validate dữ liệu:
  - Mã hội viên không được để trống, không chứa khoảng trắng.
  - Họ tên không được để trống.
  - Mật khẩu tối thiểu 4 ký tự.
  - Số điện thoại đúng định dạng (10 chữ số).
  - Số dư nạp ban đầu không được âm.
- Điều hướng gọi `HoiVienDAL`.

---

### 5. Tầng Giao diện (`NPC.Server`)
#### [NEW] [frmQuanLyHoiVien.Designer.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmQuanLyHoiVien.Designer.cs)
- Định nghĩa đầy đủ các controls, thuộc tính hiển thị (Font, Color, Anchor, Dock, DataGridView Columns) và đăng ký sự kiện chuẩn Windows Forms Designer.

#### [NEW] [frmQuanLyHoiVien.cs](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmQuanLyHoiVien.cs)
- Logic tương tác:
  - `frmQuanLyHoiVien_Load`: Tải danh sách hội viên lên DataGridView.
  - `dgvHoiVien_CellClick`: Đổ thông tin dòng được chọn lên các TextBox.
  - Các sự kiện Thêm, Sửa, Xóa, Nhập lại, Nạp tiền.
  - Tìm kiếm tức thì khi gõ hoặc click nút Tìm kiếm.
  - **`btnXuatText_Click`**: Tích hợp `SaveFileDialog` và xuất file text định dạng chuyên nghiệp.

#### [NEW] [frmQuanLyHoiVien.resx](file:///d:/LapTrinhWin_DoAn/NPC.Server/frmQuanLyHoiVien.resx)
- Resource file chuẩn XML cho form.

---

## Kế hoạch kiểm tra & xác minh (Verification Plan)

### Kiểm tra tự động qua Build & Test (Automated Verification)
- Chạy `dotnet build` để đảm bảo toàn bộ solution (5 projects: DTO, DAL, BLL, Client, Server) biên dịch thành công 100% không cảnh báo/lỗi:
  ```powershell
  dotnet build d:\LapTrinhWin_DoAn\NPC_Management.slnx
  ```

### Kiểm tra thủ công (Manual Verification)
1. **Kiểm tra giao diện kéo thả**:
   - Mở dự án trên Visual Studio 2022/2026, mở file `frmQuanLyHoiVien.cs` ở chế độ [Design].
   - Xác nhận form mở bình thường, các control hiển thị đúng vị trí và thuộc tính như bảng mô tả.
2. **Kiểm tra chức năng Quản lý**:
   - Thêm hội viên mới, sửa thông tin, xóa hội viên.
   - Nạp thêm tiền vào tài khoản hội viên.
   - Tìm kiếm theo tên hoặc số điện thoại.
3. **Kiểm tra xuất file text với SaveFileDialog**:
   - Bấm nút **"📄 Xuất File Text"**.
   - Hộp thoại `SaveFileDialog` mở ra, chọn nơi lưu và đặt tên file.
   - Nhấn Save, kiểm tra file text được tạo ra: có đầy đủ tiêu đề, các cột căn chỉnh đẹp mắt, font UTF-8 tiếng Việt hiển thị chuẩn.
   - Chọn "Yes" khi được hỏi để kiểm tra file tự động mở trên Notepad.

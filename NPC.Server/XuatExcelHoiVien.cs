using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Text;

namespace NPC.Server
{
    /// <summary>
    /// Xuất danh sách hội viên ra file Excel (.xlsx) có định dạng đẹp
    /// mà không cần thư viện ngoài (tự sinh cấu trúc OpenXML và nén ZIP).
    /// </summary>
    internal static class XuatExcelHoiVien
    {
        private const string TenSheet = "Hội viên";
        private const int SoCot = 9;           // A..I
        private const int DongHeader = 4;
        private const int DongDauDuLieu = 5;

        // ── Chỉ số style (cellXfs) ──────────────────────────────────────────
        private const int S_TieuDe = 1, S_PhuDe = 2, S_Header = 3;
        private const int K_Text = 0, K_Giua = 1, K_So = 2, K_Ngay = 3, K_Xanh = 4, K_Do = 5, K_XanhDuong = 6;
        private static int S_Dong(int kieu, bool dongChan) => 4 + (dongChan ? 7 : 0) + kieu;
        private const int S_TongNhan = 18, S_TongSo = 19, S_TongTrong = 20;

        private static readonly string[] TieuDeCot =
        {
            "STT", "Mã HV", "Tên đăng nhập", "Họ và tên", "Điện thoại",
            "Số dư (VNĐ)", "Thời gian chơi", "Ngày tạo", "Trạng thái"
        };
        private static readonly double[] DoRongCot = { 6, 9, 18, 28, 15, 16, 16, 18, 15 };

        public static void Xuat(DataTable dt, string duongDan, string nguoiXuat)
        {
            if (File.Exists(duongDan)) File.Delete(duongDan);

            using var zip = ZipFile.Open(duongDan, ZipArchiveMode.Create);
            GhiPart(zip, "[Content_Types].xml", ContentTypes());
            GhiPart(zip, "_rels/.rels", RootRels());
            GhiPart(zip, "xl/workbook.xml", Workbook(dt.Rows.Count));
            GhiPart(zip, "xl/_rels/workbook.xml.rels", WorkbookRels());
            GhiPart(zip, "xl/styles.xml", Styles());
            GhiPart(zip, "xl/worksheets/sheet1.xml", Sheet(dt, nguoiXuat));
        }

        // ════════════════════════════════════════════════════════════════════
        //  Sheet
        // ════════════════════════════════════════════════════════════════════
        private static string Sheet(DataTable dt, string nguoiXuat)
        {
            int soDong = dt.Rows.Count;
            int dongCuoiDuLieu = DongDauDuLieu + Math.Max(soDong, 1) - 1;
            int dongTong = dongCuoiDuLieu + 1;
            string cotCuoi = TenCot(SoCot);

            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            sb.Append("<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" ");
            sb.Append("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">");
            sb.Append("<sheetPr><pageSetUpPr fitToPage=\"1\"/></sheetPr>");
            sb.Append($"<dimension ref=\"A1:{cotCuoi}{dongTong}\"/>");

            // Cố định 4 dòng đầu (tiêu đề + header)
            sb.Append("<sheetViews><sheetView workbookViewId=\"0\" showGridLines=\"0\">");
            sb.Append($"<pane ySplit=\"{DongHeader}\" topLeftCell=\"A{DongDauDuLieu}\" activePane=\"bottomLeft\" state=\"frozen\"/>");
            sb.Append($"<selection pane=\"bottomLeft\" activeCell=\"A{DongDauDuLieu}\" sqref=\"A{DongDauDuLieu}\"/>");
            sb.Append("</sheetView></sheetViews>");
            sb.Append("<sheetFormatPr defaultRowHeight=\"18\"/>");

            sb.Append("<cols>");
            for (int i = 0; i < DoRongCot.Length; i++)
                sb.Append($"<col min=\"{i + 1}\" max=\"{i + 1}\" width=\"{So(DoRongCot[i])}\" customWidth=\"1\"/>");
            sb.Append("</cols>");

            sb.Append("<sheetData>");

            // Dòng 1: Tiêu đề
            sb.Append("<row r=\"1\" ht=\"32\" customHeight=\"1\">");
            OChu(sb, "A1", "DANH SÁCH HỘI VIÊN – PHÒNG NET", S_TieuDe);
            for (int c = 2; c <= SoCot; c++) OTrong(sb, $"{TenCot(c)}1", S_TieuDe);
            sb.Append("</row>");

            // Dòng 2: Thông tin xuất
            sb.Append("<row r=\"2\" ht=\"20\" customHeight=\"1\">");
            OChu(sb, "A2",
                $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}   |   Người xuất: {nguoiXuat}   |   Tổng số: {soDong} hội viên",
                S_PhuDe);
            for (int c = 2; c <= SoCot; c++) OTrong(sb, $"{TenCot(c)}2", S_PhuDe);
            sb.Append("</row>");

            // Dòng 4: Header
            sb.Append($"<row r=\"{DongHeader}\" ht=\"30\" customHeight=\"1\">");
            for (int c = 1; c <= SoCot; c++)
                OChu(sb, $"{TenCot(c)}{DongHeader}", TieuDeCot[c - 1], S_Header);
            sb.Append("</row>");

            // Dữ liệu
            decimal tongSoDu = 0;
            long tongPhut = 0;
            for (int i = 0; i < soDong; i++)
            {
                DataRow row = dt.Rows[i];
                int r = DongDauDuLieu + i;
                bool chan = i % 2 == 1;

                decimal soDu = row["SoDu"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SoDu"]);
                int phut = row["TongThoiGianChoi"] == DBNull.Value ? 0 : Convert.ToInt32(row["TongThoiGianChoi"]);
                string trangThai = row["TrangThai"]?.ToString() ?? "";
                tongSoDu += soDu;
                tongPhut += phut;

                sb.Append($"<row r=\"{r}\" ht=\"20\" customHeight=\"1\">");
                OSo(sb, $"A{r}", i + 1, S_Dong(K_Giua, chan));
                OSo(sb, $"B{r}", Convert.ToDouble(row["MaHoiVien"]), S_Dong(K_Giua, chan));
                OChu(sb, $"C{r}", row["TenDangNhap"]?.ToString() ?? "", S_Dong(K_Text, chan));
                OChu(sb, $"D{r}", row["HoTen"]?.ToString() ?? "", S_Dong(K_Text, chan));
                OChu(sb, $"E{r}", row["DienThoai"] == DBNull.Value ? "" : row["DienThoai"].ToString()!, S_Dong(K_Giua, chan));
                OSo(sb, $"F{r}", (double)soDu, S_Dong(K_So, chan));
                OChu(sb, $"G{r}", DinhDangPhut(phut), S_Dong(K_Giua, chan));

                if (row["NgayTao"] != DBNull.Value)
                    OSo(sb, $"H{r}", Convert.ToDateTime(row["NgayTao"]).ToOADate(), S_Dong(K_Ngay, chan));
                else
                    OTrong(sb, $"H{r}", S_Dong(K_Ngay, chan));

                int kieuTT = trangThai switch
                {
                    "Hoạt động" => K_Xanh,
                    "Bị khóa"   => K_Do,
                    "Đang chơi" => K_XanhDuong,
                    _           => K_Giua
                };
                OChu(sb, $"I{r}", trangThai, S_Dong(kieuTT, chan));
                sb.Append("</row>");
            }

            if (soDong == 0)
            {
                sb.Append($"<row r=\"{DongDauDuLieu}\" ht=\"20\" customHeight=\"1\">");
                OChu(sb, $"A{DongDauDuLieu}", "(Không có hội viên nào)", S_Dong(K_Giua, false));
                for (int c = 2; c <= SoCot; c++) OTrong(sb, $"{TenCot(c)}{DongDauDuLieu}", S_Dong(K_Giua, false));
                sb.Append("</row>");
            }

            // Dòng tổng cộng
            sb.Append($"<row r=\"{dongTong}\" ht=\"24\" customHeight=\"1\">");
            OChu(sb, $"A{dongTong}", "TỔNG CỘNG", S_TongNhan);
            for (int c = 2; c <= 5; c++) OTrong(sb, $"{TenCot(c)}{dongTong}", S_TongNhan);
            sb.Append($"<c r=\"F{dongTong}\" s=\"{S_TongSo}\"><f>SUM(F{DongDauDuLieu}:F{dongCuoiDuLieu})</f><v>{So((double)tongSoDu)}</v></c>");
            OChu(sb, $"G{dongTong}", DinhDangPhut(tongPhut), S_TongNhan);
            OTrong(sb, $"H{dongTong}", S_TongTrong);
            OTrong(sb, $"I{dongTong}", S_TongTrong);
            sb.Append("</row>");

            sb.Append("</sheetData>");

            if (soDong > 0)
                sb.Append($"<autoFilter ref=\"A{DongHeader}:{cotCuoi}{dongCuoiDuLieu}\"/>");

            sb.Append("<mergeCells count=\"").Append(soDong == 0 ? 4 : 3).Append("\">");
            sb.Append($"<mergeCell ref=\"A1:{cotCuoi}1\"/>");
            sb.Append($"<mergeCell ref=\"A2:{cotCuoi}2\"/>");
            sb.Append($"<mergeCell ref=\"A{dongTong}:E{dongTong}\"/>");
            if (soDong == 0) sb.Append($"<mergeCell ref=\"A{DongDauDuLieu}:{cotCuoi}{DongDauDuLieu}\"/>");
            sb.Append("</mergeCells>");

            sb.Append("<pageMargins left=\"0.4\" right=\"0.4\" top=\"0.6\" bottom=\"0.6\" header=\"0.3\" footer=\"0.3\"/>");
            sb.Append("<pageSetup paperSize=\"9\" orientation=\"landscape\" fitToWidth=\"1\" fitToHeight=\"0\"/>");
            sb.Append("</worksheet>");
            return sb.ToString();
        }

        // ════════════════════════════════════════════════════════════════════
        //  Styles
        // ════════════════════════════════════════════════════════════════════
        private static string Styles()
        {
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            sb.Append("<styleSheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">");

            sb.Append("<numFmts count=\"2\">");
            sb.Append("<numFmt numFmtId=\"164\" formatCode=\"#,##0\"/>");
            sb.Append("<numFmt numFmtId=\"165\" formatCode=\"dd/mm/yyyy hh:mm\"/>");
            sb.Append("</numFmts>");

            // Fonts: 0 thường | 1 tiêu đề | 2 phụ đề | 3 header trắng | 4 đậm | 5 xanh lá | 6 đỏ | 7 xanh dương
            sb.Append("<fonts count=\"8\">");
            sb.Append("<font><sz val=\"11\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("<font><b/><sz val=\"18\"/><color rgb=\"FF1F4E79\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("<font><i/><sz val=\"10\"/><color rgb=\"FF595959\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("<font><b/><sz val=\"11\"/><color rgb=\"FFFFFFFF\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("<font><b/><sz val=\"11\"/><color rgb=\"FF1F4E79\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("<font><b/><sz val=\"11\"/><color rgb=\"FF00843D\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("<font><b/><sz val=\"11\"/><color rgb=\"FFC00000\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("<font><b/><sz val=\"11\"/><color rgb=\"FF0070C0\"/><name val=\"Calibri\"/><family val=\"2\"/></font>");
            sb.Append("</fonts>");

            // Fills: 0 none | 1 gray125 (bắt buộc) | 2 header | 3 dòng xen kẽ | 4 tổng cộng
            sb.Append("<fills count=\"5\">");
            sb.Append("<fill><patternFill patternType=\"none\"/></fill>");
            sb.Append("<fill><patternFill patternType=\"gray125\"/></fill>");
            sb.Append(Fill("FF1F4E79"));
            sb.Append(Fill("FFDDEBF7"));
            sb.Append(Fill("FFFFF2CC"));
            sb.Append("</fills>");

            // Borders: 0 none | 1 viền mảnh | 2 viền dưới đậm (tiêu đề)
            sb.Append("<borders count=\"3\">");
            sb.Append("<border><left/><right/><top/><bottom/><diagonal/></border>");
            sb.Append("<border><left style=\"thin\"><color rgb=\"FFBFBFBF\"/></left><right style=\"thin\"><color rgb=\"FFBFBFBF\"/></right>");
            sb.Append("<top style=\"thin\"><color rgb=\"FFBFBFBF\"/></top><bottom style=\"thin\"><color rgb=\"FFBFBFBF\"/></bottom><diagonal/></border>");
            sb.Append("<border><left/><right/><top/><bottom style=\"medium\"><color rgb=\"FF1F4E79\"/></bottom><diagonal/></border>");
            sb.Append("</borders>");

            sb.Append("<cellStyleXfs count=\"1\"><xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\"/></cellStyleXfs>");

            sb.Append("<cellXfs count=\"21\">");
            sb.Append(Xf(0, 0, 0, 0, null));                                   // 0 mặc định
            sb.Append(Xf(0, 1, 0, 2, "center"));                               // 1 tiêu đề
            sb.Append(Xf(0, 2, 0, 0, "center"));                               // 2 phụ đề
            sb.Append(Xf(0, 3, 2, 1, "center", wrap: true));                   // 3 header
            for (int chan = 0; chan <= 1; chan++)                              // 4..17 dữ liệu
            {
                int fill = chan == 1 ? 3 : 0;
                sb.Append(Xf(0,   0, fill, 1, "left", indent: true));          // text
                sb.Append(Xf(0,   0, fill, 1, "center"));                      // giữa
                sb.Append(Xf(164, 0, fill, 1, "right"));                       // số tiền
                sb.Append(Xf(165, 0, fill, 1, "center"));                      // ngày
                sb.Append(Xf(0,   5, fill, 1, "center"));                      // trạng thái xanh
                sb.Append(Xf(0,   6, fill, 1, "center"));                      // trạng thái đỏ
                sb.Append(Xf(0,   7, fill, 1, "center"));                      // trạng thái xanh dương
            }
            sb.Append(Xf(0,   4, 4, 1, "center"));                             // 18 nhãn tổng
            sb.Append(Xf(164, 4, 4, 1, "right"));                              // 19 số tổng
            sb.Append(Xf(0,   0, 4, 1, "center"));                             // 20 ô trống tổng
            sb.Append("</cellXfs>");

            sb.Append("<cellStyles count=\"1\"><cellStyle name=\"Normal\" xfId=\"0\" builtinId=\"0\"/></cellStyles>");
            sb.Append("</styleSheet>");
            return sb.ToString();
        }

        private static string Fill(string argb) =>
            $"<fill><patternFill patternType=\"solid\"><fgColor rgb=\"{argb}\"/><bgColor indexed=\"64\"/></patternFill></fill>";

        private static string Xf(int numFmt, int font, int fill, int border, string? ngang, bool wrap = false, bool indent = false)
        {
            var sb = new StringBuilder();
            sb.Append($"<xf numFmtId=\"{numFmt}\" fontId=\"{font}\" fillId=\"{fill}\" borderId=\"{border}\" xfId=\"0\"");
            if (numFmt != 0) sb.Append(" applyNumberFormat=\"1\"");
            if (font != 0) sb.Append(" applyFont=\"1\"");
            if (fill != 0) sb.Append(" applyFill=\"1\"");
            if (border != 0) sb.Append(" applyBorder=\"1\"");
            if (ngang == null) return sb.Append("/>").ToString();

            sb.Append(" applyAlignment=\"1\"><alignment");
            sb.Append($" horizontal=\"{ngang}\" vertical=\"center\"");
            if (wrap) sb.Append(" wrapText=\"1\"");
            if (indent) sb.Append(" indent=\"1\"");
            sb.Append("/></xf>");
            return sb.ToString();
        }

        // ════════════════════════════════════════════════════════════════════
        //  Các part cố định
        // ════════════════════════════════════════════════════════════════════
        private static string ContentTypes() =>
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
            "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>" +
            "<Default Extension=\"xml\" ContentType=\"application/xml\"/>" +
            "<Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/>" +
            "<Override PartName=\"/xl/worksheets/sheet1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/>" +
            "<Override PartName=\"/xl/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml\"/>" +
            "</Types>";

        private static string RootRels() =>
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
            "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/>" +
            "</Relationships>";

        private static string Workbook(int soDong)
        {
            string filter = soDong > 0
                ? $"<definedNames><definedName name=\"_xlnm._FilterDatabase\" localSheetId=\"0\" hidden=\"1\">'{TenSheet}'!$A${DongHeader}:${TenCot(SoCot)}${DongDauDuLieu + soDong - 1}</definedName>" +
                  $"<definedName name=\"_xlnm.Print_Titles\" localSheetId=\"0\">'{TenSheet}'!${DongHeader}:${DongHeader}</definedName></definedNames>"
                : "";
            return "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                   "<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" " +
                   "xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">" +
                   $"<sheets><sheet name=\"{XmlEsc(TenSheet)}\" sheetId=\"1\" r:id=\"rId1\"/></sheets>" +
                   filter +
                   "</workbook>";
        }

        private static string WorkbookRels() =>
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
            "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet1.xml\"/>" +
            "<Relationship Id=\"rId2\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/>" +
            "</Relationships>";

        // ════════════════════════════════════════════════════════════════════
        //  Tiện ích
        // ════════════════════════════════════════════════════════════════════
        private static void GhiPart(ZipArchive zip, string ten, string noiDung)
        {
            ZipArchiveEntry entry = zip.CreateEntry(ten, CompressionLevel.Optimal);
            using var w = new StreamWriter(entry.Open(), new UTF8Encoding(false));
            w.Write(noiDung);
        }

        private static void OChu(StringBuilder sb, string oRef, string giaTri, int style) =>
            sb.Append($"<c r=\"{oRef}\" s=\"{style}\" t=\"inlineStr\"><is><t xml:space=\"preserve\">{XmlEsc(giaTri)}</t></is></c>");

        private static void OSo(StringBuilder sb, string oRef, double giaTri, int style) =>
            sb.Append($"<c r=\"{oRef}\" s=\"{style}\"><v>{So(giaTri)}</v></c>");

        private static void OTrong(StringBuilder sb, string oRef, int style) =>
            sb.Append($"<c r=\"{oRef}\" s=\"{style}\"/>");

        private static string So(double v) => v.ToString("0.##########", CultureInfo.InvariantCulture);

        private static string TenCot(int cot)
        {
            string ten = "";
            while (cot > 0)
            {
                int m = (cot - 1) % 26;
                ten = (char)('A' + m) + ten;
                cot = (cot - 1) / 26;
            }
            return ten;
        }

        private static string DinhDangPhut(long phut) =>
            phut <= 0 ? "0 phút" : (phut >= 60 ? $"{phut / 60} giờ {phut % 60:00} phút" : $"{phut} phút");

        private static string XmlEsc(string s)
        {
            // Loại bỏ ký tự điều khiển không hợp lệ trong XML
            var clean = new StringBuilder(s.Length);
            foreach (char ch in s)
                if (ch == '\t' || ch == '\n' || ch == '\r' || ch >= 0x20) clean.Append(ch);
            return SecurityElement.Escape(clean.ToString()) ?? "";
        }
    }
}


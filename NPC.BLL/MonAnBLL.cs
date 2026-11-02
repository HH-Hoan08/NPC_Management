using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPC.DTO;
using NPC.DAL;
using System.Data;

namespace NPC.BLL
{
    public class MonAnBLL
    {
        private readonly MonAnDAL _monAnDAL = new MonAnDAL();

        public DataTable LayDataTable(string tuKhoa = "", int? maDanhMuc = null, bool chiLayTonKhoThap = false)
        {
            return _monAnDAL.GetDataToTable(tuKhoa, maDanhMuc, chiLayTonKhoThap);
        }

        public List<DanhMucDTO> LayDanhSachDanhMuc()
        {
            return _monAnDAL.GetDanhSachDanhMuc();
        }

        public bool ThemMonAn(MonAnDTO mon, out string thongBao)
        {
            thongBao = string.Empty;

            if (string.IsNullOrWhiteSpace(mon.TenMon))
            {
                thongBao = "Tên món ăn không được để trống";
                return false;
            }

            if (mon.MaDanhMuc <= 0)
            {
                thongBao = "Vui lòng chọn loại danh mục món ăn";
                return false;
            }

            if (mon.DonGia < 0)
            {
                thongBao = "Đơn giá bán không được âm";
                return false;
            }

            if (mon.GiaNhap < 0)
            {
                thongBao = "Giá nhập không được âm";
                return false;
            }

            if (mon.DonGia < mon.GiaNhap)
            {
                thongBao = "Đơn giá bán phải lớn hơn hoặc bằng giá nhập vốn";
                return false;
            }

            if (mon.SoLuongTon < 0)
            {
                thongBao = "Số lượng tồn kho không được âm";
                return false;
            }

            return _monAnDAL.ThemMonAn(mon, out thongBao);
        }

        public bool CapNhatMonAn(MonAnDTO mon, out string thongBao)
        {
            thongBao = string.Empty;

            if (mon.MaMon <= 0)
            {
                thongBao = "Vui lòng chọn món ăn cần cập nhật";
                return false;
            }

            if (string.IsNullOrWhiteSpace(mon.TenMon))
            {
                thongBao = "Tên món ăn không được để trống";
                return false;
            }

            if (mon.MaDanhMuc <= 0)
            {
                thongBao = "Vui lòng chọn loại danh mục";
                return false;
            }

            if (mon.DonGia < mon.GiaNhap)
            {
                thongBao = "Đơn giá bán phải lớn hơn hoặc bằng giá nhập vốn";
                return false;
            }

            if (mon.SoLuongTon < 0)
            {
                thongBao = "Số lượng tồn kho không được âm";
                return false;
            }

            return _monAnDAL.CapNhatMonAn(mon, out thongBao);
        }

        public bool XoaMonAn(int maMon, out string thongBao)
        {
            if (maMon <= 0)
            {
                thongBao = "Vui lòng chọn món ăn cần xóa";
                return false;
            }
            return _monAnDAL.XoaMonAn(maMon, out thongBao);
        }

        public bool NhapKhoMonAn(int maMon, int soLuongThem, decimal giaNhapMoi, out string thongBao)
        {
            thongBao = string.Empty;

            if (maMon <= 0)
            {
                thongBao = "Vui lòng chọn món ăn cần nhập kho";
                return false;
            }

            if (soLuongThem <= 0)
            {
                thongBao = "Số lượng nhập thêm phải lớn hơn 0";
                return false;
            }

            if (giaNhapMoi < 0)
            {
                thongBao = "Giá nhập không được âm";
                return false;
            }

            return _monAnDAL.NhapKhoMonAn(maMon, soLuongThem, giaNhapMoi, out thongBao);
        }
    }
}

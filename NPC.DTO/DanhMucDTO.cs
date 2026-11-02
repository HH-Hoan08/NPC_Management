using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPC.DTO
{
    public class DanhMucDTO
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;

        public override string ToString()
        {
            return TenDanhMuc;
        }
    }
}

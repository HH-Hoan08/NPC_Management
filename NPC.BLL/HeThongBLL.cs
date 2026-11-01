using System;
using NPC.DAL;

namespace NPC.BLL
{
    public static class HeThongBLL
    {
        public static (bool ThanhCong, string ThongBao) KiemTraKetNoiCSDL()
        {
            if (NPCData.TestConnection(out string err))
            {
                return (true, "Kết nối CSDL thành công.");
            }
            return (false, $"Không thể kết nối đến CSDL: {err}");
        }

        public static bool DaKetNoiCSDL()
        {
            return NPCData.IsOK;
        }

        public static void DatLaiKetNoi()
        {
            NPCData.Disconnect();
        }
    }
}


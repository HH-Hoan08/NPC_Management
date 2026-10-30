using System;
using NPC.DTO;

namespace NPC.Server
{
    public enum MayTramAction
    {
        MoMayVangLai,
        MoMayHoiVien,
        KhoaMay,
        NapTien,
        ThanhToan,
        ChuyenMay,
        BaoTri
    }

    public class MayTramEventArgs : EventArgs
    {
        public MayTramDTO MayTram { get; set; }
        public MayTramAction Action { get; set; }

        public MayTramEventArgs(MayTramDTO mayTram, MayTramAction action)
        {
            MayTram = mayTram;
            Action = action;
        }
    }
}

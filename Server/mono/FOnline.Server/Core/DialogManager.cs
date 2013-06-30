using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public interface IDialogManager
    {
        bool RunDialog(Critter player, Critter npc, bool ignore_distance);
        bool RunDialog(Critter player, Critter npc, uint dialog_pack, bool ignore_distance);
        bool RunDialog(Critter player, uint dialog_pack, ushort hx, ushort hy, bool ignore_distance);
    }
    public class DialogManager : IDialogManager
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_RunDialogNpc(IntPtr player, IntPtr npc, bool ignore_distance);
        public bool RunDialog(Critter player, Critter npc, bool ignore_distance)
        {
            return Global_RunDialogNpc(player.ThisPtr, npc.ThisPtr, ignore_distance);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_RunDialogNpcDlgPack(IntPtr player, IntPtr npc, uint dialog_pack, bool ignore_distance);
        public bool RunDialog(Critter player, Critter npc, uint dialog_pack, bool ignore_distance)
        {
            return Global_RunDialogNpcDlgPack(player.ThisPtr, npc.ThisPtr, dialog_pack, ignore_distance);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_RunDialogHex(IntPtr player, uint dialog_pack, ushort hx, ushort hy, bool ignore_distance);
        public bool RunDialog(Critter player, uint dialog_pack, ushort hx, ushort hy, bool ignore_distance)
        {
            return Global_RunDialogHex(player.ThisPtr, dialog_pack, hx, hy, ignore_distance);
        }
    }
}

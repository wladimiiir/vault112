using System;
namespace FOnline
{
    public partial class NpcPlane
    {
        public virtual PlaneType Type { get { return (PlaneType)NativeFields.GetInt32(thisptr, offsetType); } set { NativeFields.SetInt32(thisptr, offsetType, (Int32)value); }}
        public virtual UInt32 Priority { get { return NativeFields.GetUInt32(thisptr, offsetPriority); } set { NativeFields.SetUInt32(thisptr, offsetPriority, value); }}
        public virtual Int32 Identifier { get { return NativeFields.GetInt32(thisptr, offsetIdentifier); } set { NativeFields.SetInt32(thisptr, offsetIdentifier, value); }}
        public virtual UInt32 IdentifierExt { get { return NativeFields.GetUInt32(thisptr, offsetIdentifierExt); } set { NativeFields.SetUInt32(thisptr, offsetIdentifierExt, value); }}
        public virtual Boolean Run { get { return NativeFields.GetBoolean(thisptr, offsetRun); } set { NativeFields.SetBoolean(thisptr, offsetRun, value); }}
        public virtual UInt32 Misc_WaitSecond { get { return NativeFields.GetUInt32(thisptr, offsetMisc_WaitSecond); } set { NativeFields.SetUInt32(thisptr, offsetMisc_WaitSecond, value); }}
        public virtual Int32 Misc_ScriptId { get { return NativeFields.GetInt32(thisptr, offsetMisc_ScriptId); } set { NativeFields.SetInt32(thisptr, offsetMisc_ScriptId, value); }}
        public virtual UInt32 Attack_TargId { get { return NativeFields.GetUInt32(thisptr, offsetAttack_TargId); } set { NativeFields.SetUInt32(thisptr, offsetAttack_TargId, value); }}
        public virtual Int32 Attack_MinHp { get { return NativeFields.GetInt32(thisptr, offsetAttack_MinHp); } set { NativeFields.SetInt32(thisptr, offsetAttack_MinHp, value); }}
        public virtual Boolean Attack_IsGag { get { return NativeFields.GetBoolean(thisptr, offsetAttack_IsGag); } set { NativeFields.SetBoolean(thisptr, offsetAttack_IsGag, value); }}
        public virtual UInt16 Attack_GagHexX { get { return NativeFields.GetUInt16(thisptr, offsetAttack_GagHexX); } set { NativeFields.SetUInt16(thisptr, offsetAttack_GagHexX, value); }}
        public virtual UInt16 Attack_GagHexY { get { return NativeFields.GetUInt16(thisptr, offsetAttack_GagHexY); } set { NativeFields.SetUInt16(thisptr, offsetAttack_GagHexY, value); }}
        public virtual UInt16 Attack_LastHexX { get { return NativeFields.GetUInt16(thisptr, offsetAttack_LastHexX); } set { NativeFields.SetUInt16(thisptr, offsetAttack_LastHexX, value); }}
        public virtual UInt16 Attack_LastHexY { get { return NativeFields.GetUInt16(thisptr, offsetAttack_LastHexY); } set { NativeFields.SetUInt16(thisptr, offsetAttack_LastHexY, value); }}
        public virtual UInt16 Walk_HexX { get { return NativeFields.GetUInt16(thisptr, offsetWalk_HexX); } set { NativeFields.SetUInt16(thisptr, offsetWalk_HexX, value); }}
        public virtual UInt16 Walk_HexY { get { return NativeFields.GetUInt16(thisptr, offsetWalk_HexY); } set { NativeFields.SetUInt16(thisptr, offsetWalk_HexY, value); }}
        public virtual Direction Walk_Dir { get { return (Direction)NativeFields.GetByte(thisptr, offsetWalk_Dir); } set { NativeFields.SetByte(thisptr, offsetWalk_Dir, (Byte)value); }}
        public virtual UInt32 Walk_Cut { get { return NativeFields.GetUInt32(thisptr, offsetWalk_Cut); } set { NativeFields.SetUInt32(thisptr, offsetWalk_Cut, value); }}
        public virtual UInt16 Pick_HexX { get { return NativeFields.GetUInt16(thisptr, offsetPick_HexX); } set { NativeFields.SetUInt16(thisptr, offsetPick_HexX, value); }}
        public virtual UInt16 Pick_HexY { get { return NativeFields.GetUInt16(thisptr, offsetPick_HexY); } set { NativeFields.SetUInt16(thisptr, offsetPick_HexY, value); }}
        public virtual UInt16 Pick_Pid { get { return NativeFields.GetUInt16(thisptr, offsetPick_Pid); } set { NativeFields.SetUInt16(thisptr, offsetPick_Pid, value); }}
        public virtual UInt32 Pick_UseItemId { get { return NativeFields.GetUInt32(thisptr, offsetPick_UseItemId); } set { NativeFields.SetUInt32(thisptr, offsetPick_UseItemId, value); }}
        public virtual Boolean Pick_ToOpen { get { return NativeFields.GetBoolean(thisptr, offsetPick_ToOpen); } set { NativeFields.SetBoolean(thisptr, offsetPick_ToOpen, value); }}

#pragma warning disable 649
	    static int offsetType;
        static int offsetPriority;
        static int offsetIdentifier;
        static int offsetIdentifierExt;
        static int offsetRun;
        static int offsetMisc_WaitSecond;
        static int offsetMisc_ScriptId;
        static int offsetAttack_TargId;
        static int offsetAttack_MinHp;
        static int offsetAttack_IsGag;
        static int offsetAttack_GagHexX;
        static int offsetAttack_GagHexY;
        static int offsetAttack_LastHexX;
        static int offsetAttack_LastHexY;
        static int offsetWalk_HexX;
        static int offsetWalk_HexY;
        static int offsetWalk_Dir;
        static int offsetWalk_Cut;
        static int offsetPick_HexX;
        static int offsetPick_HexY;
        static int offsetPick_Pid;
        static int offsetPick_UseItemId;
        static int offsetPick_ToOpen;
#pragma warning restore 649
	}
}

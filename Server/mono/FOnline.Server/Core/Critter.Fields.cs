using System;
namespace FOnline
{
    public partial class Critter
    {
        public virtual UInt32 Id { get { return NativeFields.GetUInt32(thisptr, offsetId); }}
        public virtual UInt32 CrType { get { return NativeFields.GetUInt32(thisptr, offsetCrType); }}
        public virtual UInt16 HexX { get { return NativeFields.GetUInt16(thisptr, offsetHexX); }}
        public virtual UInt16 HexY { get { return NativeFields.GetUInt16(thisptr, offsetHexY); }}
        public virtual UInt16 WorldX { get { return NativeFields.GetUInt16(thisptr, offsetWorldX); }}
        public virtual UInt16 WorldY { get { return NativeFields.GetUInt16(thisptr, offsetWorldY); }}
        public virtual Byte Dir { get { return NativeFields.GetByte(thisptr, offsetDir); }}
        public virtual Cond Cond { get { return (Cond)NativeFields.GetByte(thisptr, offsetCond); }}
        public virtual UInt32 Anim1Life { get { return NativeFields.GetUInt32(thisptr, offsetAnim1Life); }}
        public virtual UInt32 Anim1Knockout { get { return NativeFields.GetUInt32(thisptr, offsetAnim1Knockout); }}
        public virtual UInt32 Anim1Dead { get { return NativeFields.GetUInt32(thisptr, offsetAnim1Dead); }}
        public virtual UInt32 Anim2Life { get { return NativeFields.GetUInt32(thisptr, offsetAnim2Life); }}
        public virtual UInt32 Anim2Knockout { get { return NativeFields.GetUInt32(thisptr, offsetAnim2Knockout); }}
        public virtual UInt32 Anim2Dead { get { return NativeFields.GetUInt32(thisptr, offsetAnim2Dead); }}
        public virtual UInt32 Flags { get { return NativeFields.GetUInt32(thisptr, offsetFlags); }}
        public virtual UInt32 ShowCritterDist1 { get { return NativeFields.GetUInt32(thisptr, offsetShowCritterDist1); } set { NativeFields.SetUInt32(thisptr, offsetShowCritterDist1, value); }}
        public virtual UInt32 ShowCritterDist2 { get { return NativeFields.GetUInt32(thisptr, offsetShowCritterDist2); } set { NativeFields.SetUInt32(thisptr, offsetShowCritterDist2, value); }}
        public virtual UInt32 ShowCritterDist3 { get { return NativeFields.GetUInt32(thisptr, offsetShowCritterDist3); } set { NativeFields.SetUInt32(thisptr, offsetShowCritterDist3, value); }}
        //public virtual Boolean IsRuning { get { return NativeFields.GetBoolean(thisptr, 16); } set { NativeFields.SetBoolean(thisptr, offsetIsNotValid, value); }}
        public virtual Boolean IsNotValid { get { return NativeFields.GetBoolean(thisptr, 20); }}
        
#pragma warning disable 649
		static int offsetId;
		static int offsetCrType;
		static int offsetHexX;
		static int offsetHexY;
		static int offsetWorldX;
		static int offsetWorldY;
		static int offsetDir;
		static int offsetCond;
		static int offsetAnim1Life;
		static int offsetAnim1Knockout;
		static int offsetAnim1Dead;
		static int offsetAnim2Life;
		static int offsetAnim2Knockout;
		static int offsetAnim2Dead;
		static int offsetFlags;
		static int offsetShowCritterDist1;
		static int offsetShowCritterDist2;
		static int offsetShowCritterDist3;
		//static int offsetIsRunning;
		static int offsetIsNotValid;	
#pragma warning restore 649
    }
}

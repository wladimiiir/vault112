using System;
namespace FOnline
{
    public partial class Location
    {
        public virtual UInt32 Id { get { return NativeFields.GetUInt32(thisptr, offsetId); }}
        public virtual UInt16 WorldX { get { return NativeFields.GetUInt16(thisptr, offsetWorldX); } set { NativeFields.SetUInt16(thisptr, offsetWorldX, value); }}
        public virtual UInt16 WorldY { get { return NativeFields.GetUInt16(thisptr, offsetWorldY); } set { NativeFields.SetUInt16(thisptr, offsetWorldY, value); }}
        public virtual Boolean Visible { get { return NativeFields.GetBoolean(thisptr, offsetVisible); } set { NativeFields.SetBoolean(thisptr, offsetVisible, value); }}
        public virtual Boolean GeckVisible { get { return NativeFields.GetBoolean(thisptr, offsetGeckVisible); } set { NativeFields.SetBoolean(thisptr, offsetGeckVisible, value); }}
        public virtual Boolean AutoGarbage { get { return NativeFields.GetBoolean(thisptr, offsetAutoGarbage); } set { NativeFields.SetBoolean(thisptr, offsetAutoGarbage, value); }}
        public virtual Int32 GeckCount { get { return NativeFields.GetInt32(thisptr, offsetGeckCount); } set { NativeFields.SetInt32(thisptr, offsetGeckCount, value); }}
        public virtual UInt16 Radius { get { return NativeFields.GetUInt16(thisptr, offsetRadius); } set { NativeFields.SetUInt16(thisptr, offsetRadius, value); }}
        public virtual UInt32 Color { get { return NativeFields.GetUInt32(thisptr, offsetColor); } set { NativeFields.SetUInt32(thisptr, offsetColor, value); }}
        public virtual Boolean IsNotValid { get { return NativeFields.GetBoolean(thisptr, offsetIsNotValid); }}
        
#pragma warning disable 649
		static int offsetId;
		static int offsetWorldX;
		static int offsetWorldY;
		static int offsetVisible;
		static int offsetGeckVisible;
		static int offsetAutoGarbage;
		static int offsetGeckCount;
		static int offsetRadius;
		static int offsetColor;
		static int offsetIsNotValid;
#pragma warning restore 649
	}
}

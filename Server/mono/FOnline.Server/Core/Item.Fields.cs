using System;
namespace FOnline
{
    public partial class Item
    {
        public virtual UInt32 Id { get { return NativeFields.GetUInt32(thisptr, offsetId); }}
        public virtual ProtoItem Proto { get { return (ProtoItem)NativeFields.GetIntPtr(thisptr, offsetProto); }}
        public virtual Accessory Accessory { get { return (Accessory)NativeFields.GetByte(thisptr, offsetAccessory); }}
        public virtual UInt32 MapId { get { return NativeFields.GetUInt32(thisptr, offsetMapId); }}
        public virtual UInt16 HexX { get { return NativeFields.GetUInt16(thisptr, offsetHexX); }}
        public virtual UInt16 HexY { get { return NativeFields.GetUInt16(thisptr, offsetHexY); }}
        public virtual UInt32 CritId { get { return NativeFields.GetUInt32(thisptr, offsetCritId); }}
        public virtual Byte CritSlot { get { return NativeFields.GetByte(thisptr, offsetCritSlot); }}
        public virtual UInt32 ContainerId { get { return NativeFields.GetUInt32(thisptr, offsetContainerId); }}
        public virtual UInt32 StackId { get { return NativeFields.GetUInt32(thisptr, offsetStackId); }}
        public virtual Boolean IsNotValid { get { return NativeFields.GetBoolean(thisptr, offsetIsNotValid); }}
        public virtual Byte Mode { get { return NativeFields.GetByte(thisptr, offsetMode); }}
        public virtual UInt16 SortValue { get { return NativeFields.GetUInt16(thisptr, offsetSortValue); } set { NativeFields.SetUInt16(thisptr, offsetSortValue, value); }}
        public virtual Byte Info { get { return NativeFields.GetByte(thisptr, offsetInfo); } set { NativeFields.SetByte(thisptr, offsetInfo, value); }}
        public virtual UInt32 PicMap { get { return NativeFields.GetUInt32(thisptr, offsetPicMap); } set { NativeFields.SetUInt32(thisptr, offsetPicMap, value); }}
        public virtual UInt32 PicInv { get { return NativeFields.GetUInt32(thisptr, offsetPicInv); } set { NativeFields.SetUInt32(thisptr, offsetPicInv, value); }}
        public virtual UInt16 AnimWaitBase { get { return NativeFields.GetUInt16(thisptr, offsetAnimWaitBase); } set { NativeFields.SetUInt16(thisptr, offsetAnimWaitBase, value); }}
        public virtual Byte AnimStayBegin { get { return NativeFields.GetByte(thisptr, offsetAnimStayBegin); } set { NativeFields.SetByte(thisptr, offsetAnimStayBegin, value); }}
        public virtual Byte AnimStayEnd { get { return NativeFields.GetByte(thisptr, offsetAnimStayEnd); } set { NativeFields.SetByte(thisptr, offsetAnimStayEnd, value); }}
        public virtual Byte AnimShowBegin { get { return NativeFields.GetByte(thisptr, offsetAnimShowBegin); } set { NativeFields.SetByte(thisptr, offsetAnimShowBegin, value); }}
        public virtual Byte AnimShowEnd { get { return NativeFields.GetByte(thisptr, offsetAnimShowEnd); } set { NativeFields.SetByte(thisptr, offsetAnimShowEnd, value); }}
        public virtual Byte AnimHideBegin { get { return NativeFields.GetByte(thisptr, offsetAnimHideBegin); } set { NativeFields.SetByte(thisptr, offsetAnimHideBegin, value); }}
        public virtual Byte AnimHideEnd { get { return NativeFields.GetByte(thisptr, offsetAnimHideEnd); } set { NativeFields.SetByte(thisptr, offsetAnimHideEnd, value); }}
        public virtual UInt32 Cost { get { return NativeFields.GetUInt32(thisptr, offsetCost); } set { NativeFields.SetUInt32(thisptr, offsetCost, value); }}
        public virtual Int32 Val0 { get { return NativeFields.GetInt32(thisptr, offsetVal0); } set { NativeFields.SetInt32(thisptr, offsetVal0, value); }}
        public virtual Int32 Val1 { get { return NativeFields.GetInt32(thisptr, offsetVal1); } set { NativeFields.SetInt32(thisptr, offsetVal1, value); }}
        public virtual Int32 Val2 { get { return NativeFields.GetInt32(thisptr, offsetVal2); } set { NativeFields.SetInt32(thisptr, offsetVal2, value); }}
        public virtual Int32 Val3 { get { return NativeFields.GetInt32(thisptr, offsetVal3); } set { NativeFields.SetInt32(thisptr, offsetVal3, value); }}
        public virtual Int32 Val4 { get { return NativeFields.GetInt32(thisptr, offsetVal4); } set { NativeFields.SetInt32(thisptr, offsetVal4, value); }}
        public virtual Int32 Val5 { get { return NativeFields.GetInt32(thisptr, offsetVal5); } set { NativeFields.SetInt32(thisptr, offsetVal5, value); }}
        public virtual Int32 Val6 { get { return NativeFields.GetInt32(thisptr, offsetVal6); } set { NativeFields.SetInt32(thisptr, offsetVal6, value); }}
        public virtual Int32 Val7 { get { return NativeFields.GetInt32(thisptr, offsetVal7); } set { NativeFields.SetInt32(thisptr, offsetVal7, value); }}
        public virtual Int32 Val8 { get { return NativeFields.GetInt32(thisptr, offsetVal8); } set { NativeFields.SetInt32(thisptr, offsetVal8, value); }}
        public virtual Int32 Val9 { get { return NativeFields.GetInt32(thisptr, offsetVal9); } set { NativeFields.SetInt32(thisptr, offsetVal9, value); }}
        public virtual SByte LightIntensity { get { return NativeFields.GetSByte(thisptr, offsetLightIntensity); } set { NativeFields.SetSByte(thisptr, offsetLightIntensity, value); }}
        public virtual Byte LightDistance { get { return NativeFields.GetByte(thisptr, offsetLightDistance); } set { NativeFields.SetByte(thisptr, offsetLightDistance, value); }}
        public virtual Byte LightFlags { get { return NativeFields.GetByte(thisptr, offsetLightFlags); } set { NativeFields.SetByte(thisptr, offsetLightFlags, value); }}
        public virtual UInt32 LightColor { get { return NativeFields.GetUInt32(thisptr, offsetLightColor); } set { NativeFields.SetUInt32(thisptr, offsetLightColor, value); }}
        public virtual Byte Indicator { get { return NativeFields.GetByte(thisptr, offsetIndicator); } set { NativeFields.SetByte(thisptr, offsetIndicator, value); }}
        public virtual BI BrokenFlags { get { return (BI)NativeFields.GetByte(thisptr, offsetBrokenFlags); } set { NativeFields.SetByte(thisptr, offsetBrokenFlags, (Byte)value); }}
        public virtual Byte BrokenCount { get { return NativeFields.GetByte(thisptr, offsetBrokenCount); } set { NativeFields.SetByte(thisptr, offsetBrokenCount, value); }}
        public virtual UInt16 Deterioration { get { return NativeFields.GetUInt16(thisptr, offsetDeterioration); } set { NativeFields.SetUInt16(thisptr, offsetDeterioration, value); }}
        //public virtual UInt16 WeaponAmmoPid { get { return NativeFields.GetUInt16(thisptr, offsetWeaponAmmoPid); } set { NativeFields.SetUInt16(thisptr, offsetWeaponAmmoPid, value); }}
        public virtual UInt16 AmmoCount { get { return NativeFields.GetUInt16(thisptr, offsetAmmoCount); } set { NativeFields.SetUInt16(thisptr, offsetAmmoCount, value); }}
        public virtual UInt32 LockerId { get { return NativeFields.GetUInt32(thisptr, offsetLockerId); } set { NativeFields.SetUInt32(thisptr, offsetLockerId, value); }}
        public virtual UInt16 LockerCondition { get { return NativeFields.GetUInt16(thisptr, offsetLockerCondition); } set { NativeFields.SetUInt16(thisptr, offsetLockerCondition, value); }}
        public virtual UInt16 LockerComplexity { get { return NativeFields.GetUInt16(thisptr, offsetLockerComplexity); } set { NativeFields.SetUInt16(thisptr, offsetLockerComplexity, value); }}
        //public virtual UInt16 CarFuel { get { return NativeFields.GetUInt16(thisptr, offsetCarFuel); } set { NativeFields.SetUInt16(thisptr, offsetCarFuel, value); }}
        //public virtual UInt16 CarDeterioration { get { return NativeFields.GetUInt16(thisptr, offsetCarDeterioration); } set { NativeFields.SetUInt16(thisptr, offsetCarDeterioration, value); }}
        public virtual UInt16 RadioChannel { get { return NativeFields.GetUInt16(thisptr, offsetRadioChannel); } set { NativeFields.SetUInt16(thisptr, offsetRadioChannel, value); }}
        public virtual UInt16 RadioFlags { get { return NativeFields.GetUInt16(thisptr, offsetRadioFlags); } set { NativeFields.SetUInt16(thisptr, offsetRadioFlags, value); }}
        public virtual Byte RadioBroadcastSend { get { return NativeFields.GetByte(thisptr, offsetRadioBroadcastSend); } set { NativeFields.SetByte(thisptr, offsetRadioBroadcastSend, value); }}
        public virtual Byte RadioBroadcastRecv { get { return NativeFields.GetByte(thisptr, offsetRadioBroadcastRecv); } set { NativeFields.SetByte(thisptr, offsetRadioBroadcastRecv, value); }}
        public virtual UInt32 HolodiskNumber { get { return NativeFields.GetUInt32(thisptr, offsetHolodiskNumber); } set { NativeFields.SetUInt32(thisptr, offsetHolodiskNumber, value); }}

#pragma warning disable 649
	    static int offsetId;
        static int offsetProto;
        static int offsetAccessory;
        static int offsetMapId;
        static int offsetHexX;
        static int offsetHexY;
        static int offsetCritId;
        static int offsetCritSlot;
        static int offsetContainerId;
        static int offsetStackId;
        static int offsetIsNotValid;
        static int offsetMode;
        static int offsetSortValue;
        static int offsetInfo;
        static int offsetPicMap;
        static int offsetPicInv;
        static int offsetAnimWaitBase;
        static int offsetAnimStayBegin;
        static int offsetAnimStayEnd;
        static int offsetAnimShowBegin;
        static int offsetAnimShowEnd;
        static int offsetAnimHideBegin;
        static int offsetAnimHideEnd;
        static int offsetCost;
        static int offsetVal0;
        static int offsetVal1;
        static int offsetVal2;
        static int offsetVal3;
        static int offsetVal4;
        static int offsetVal5;
        static int offsetVal6;
        static int offsetVal7;
        static int offsetVal8;
        static int offsetVal9;
        static int offsetLightIntensity;
        static int offsetLightDistance;
        static int offsetLightFlags;
        static int offsetLightColor;
        static int offsetIndicator;
        static int offsetBrokenFlags;
        static int offsetBrokenCount;
        static int offsetDeterioration;
        //static int offsetWeaponAmmoPid;
        static int offsetAmmoCount;
        static int offsetLockerId;
        static int offsetLockerCondition;
        static int offsetLockerComplexity;
        //static int offsetCarFuel;
        //static int offsetCarDeterioration;
        static int offsetRadioChannel;
        static int offsetRadioFlags;
        static int offsetRadioBroadcastSend;
        static int offsetRadioBroadcastRecv;
        static int offsetHolodiskNumber;
#pragma warning restore 649
	}
}

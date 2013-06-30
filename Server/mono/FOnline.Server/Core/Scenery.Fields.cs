using System;
namespace FOnline
{
    public partial class Scenery
    {
        public virtual UInt16 ProtoId { get { return NativeFields.GetUInt16(thisptr, offsetProtoId); }}
        public virtual UInt16 HexX { get { return NativeFields.GetUInt16(thisptr, offsetHexX); }}
        public virtual UInt16 HexY { get { return NativeFields.GetUInt16(thisptr, offsetHexY); }}

#pragma warning disable 649
		static int offsetProtoId;
		static int offsetHexX;
		static int offsetHexY;
#pragma warning restore 649
	}
}

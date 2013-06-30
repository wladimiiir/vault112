using System;
namespace FOnline
{
    public partial class Map
    {
        public virtual Boolean IsNotValid { get { return NativeFields.GetBoolean(thisptr, offsetIsNotValid); }}
        public virtual UInt32 Id { get { return NativeFields.GetUInt32(thisptr, offsetId); }}
        public virtual UInt32 TurnBasedRound { get { return NativeFields.GetUInt32(thisptr, offsetTurnBasedRound); }}
        public virtual UInt32 TurnBasedTurn { get { return NativeFields.GetUInt32(thisptr, offsetTurnBasedTurn); }}
        public virtual UInt32 TurnBasedWholeTurn { get { return NativeFields.GetUInt32(thisptr, offsetTurnBasedWholeTurn); }}

#pragma warning disable 649
		static int offsetIsNotValid;
		static int offsetId;
		static int offsetTurnBasedRound;
		static int offsetTurnBasedTurn;
		static int offsetTurnBasedWholeTurn;
#pragma warning restore 649
	}
}

using System;

namespace FOnline
{
	public class ItemPack
	{
		public static readonly ushort[] HealingDrugs = new ushort[] {
			ItemProtoId.Stimpack,
			ItemProtoId.SuperStimpack,
			ItemProtoId.HealingPowder
		};
	}

	public class ItemProtoId
	{
		//weapons
		public const ushort NeedlerPistol = 388;

		//ammo
		public const ushort HNNeedlerCartridge = 361;

		//armors
		public const ushort LeatherJacket = 361;

		//drugs
		public const ushort Stimpack = 40;
		public const ushort SuperStimpack = 144;
		public const ushort HealingPowder = 273;

		//misc
		public const ushort HiddenContainer = 467;
	}
}


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
		public const ushort Stimpack = 40;
		public const ushort SuperStimpack = 144;
		public const ushort HealingPowder = 273;
		public const ushort HiddenContainer = 467;
	}
}


using System;

namespace FOnline.BT
{
	public class IsInRange : CritterCheckCondition<CritterBlackboard>
	{
		private int range;

		public IsInRange (int range)
		{
			this.range = range;
		}

		public override bool Check (Critter checkEntity)
		{
			var critter = GetBlackboard ().Critter;
			return Global.GetDistantion(checkEntity.HexX, checkEntity.HexY, critter.HexX, critter.HexY) <= range;
		}
	}
}


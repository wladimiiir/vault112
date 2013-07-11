using System;

namespace FOnline.BT
{
	public class IsHurt : CritterCheckCondition<CritterBlackboard>
	{
		private uint underHPPercentage;

		public IsHurt (uint underHPPercentage)
		{
			this.underHPPercentage = underHPPercentage;
		}

		public override bool Check (Critter checkEntity)
		{
			int hurtHp = (int)(checkEntity.Stat [Stats.MaxLife] * (underHPPercentage / 100d));
			return checkEntity.Stat [Stats.CurrentHP] < hurtHp;
		}
	}
}


using System;

namespace FOnline.BT
{
	public class IsSameType : CritterCheckCondition<CritterBlackboard>
	{
		public override bool Check (Critter checkEntity)
		{
			return GetBlackboard ().Critter.Stat [Stats.BodyType] == checkEntity.Stat [Stats.BodyType];
		}
	}
}


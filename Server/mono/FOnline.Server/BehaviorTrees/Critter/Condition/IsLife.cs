using System;

namespace FOnline.BT
{
	public class IsLife : CritterCheckCondition<CritterBlackboard>
	{
		public override bool Check (Critter checkEntity)
		{
			return checkEntity.IsLife;
		}
	}
}


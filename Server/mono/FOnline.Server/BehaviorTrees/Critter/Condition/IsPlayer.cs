using System;

namespace FOnline.BT
{
	public class IsPlayer : CritterCheckCondition<CritterBlackboard>
	{
		public override bool Check (Critter checkCritter)
		{
			return checkCritter.IsPlayer;
		}
	}
}


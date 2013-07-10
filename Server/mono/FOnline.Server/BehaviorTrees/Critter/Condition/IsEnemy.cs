using System;

namespace FOnline.BT
{
	public class IsEnemy : CritterCheckCondition<CritterBlackboard>
	{
		public override bool Check (Critter checkCritter)
		{
			return GetBlackboard().Critter.CheckEnemyInStack(checkCritter.Id);
		}
	}
}


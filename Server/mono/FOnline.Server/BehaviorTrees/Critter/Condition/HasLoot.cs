using System;

namespace FOnline.BT
{
	public class HasLoot : CritterCheckCondition<CritterBlackboard>
	{
		private bool checkMyself;

		public HasLoot (bool checkMyself = true)
		{
			this.checkMyself = checkMyself;
		}
		

		public override bool Check (Critter checkEntity)
		{
			if (checkMyself) {
				return CritterHasLoot (GetBlackboard ().Critter);
			} else {
				return CritterHasLoot (checkEntity);
			}
		}

		private bool CritterHasLoot (Critter critter)
		{
			if (critter.IsPlayer)
				return false;

			return critter.GetPlanes ((int)CritterDefines.PlaneIdentifier.Loot, null) > 0;
		}
	}
}


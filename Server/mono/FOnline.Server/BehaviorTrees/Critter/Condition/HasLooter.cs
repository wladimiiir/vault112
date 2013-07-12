using System;

namespace FOnline.BT
{
	public class HasLooter : CritterCheckCondition<CritterBlackboard>
	{
		public override bool Check (Critter checkEntity)
		{
			var map = checkEntity.GetMap ();
			if (map == null)
				return false;

			var critters = new CritterArray ();
			map.GetCritters (0, Find.OnlyNpc | Find.Life, critters);
			foreach (var critter in critters) {
				if (critter.GetPlanes ((int)CritterDefines.PlaneIdentifier.Loot, checkEntity.Id, null) > 0)
					return true;
			}

			return false;
		}
	}
}


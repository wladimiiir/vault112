using System;

namespace FOnline.BT
{
	public class AmAttacking : CritterCheckCondition<CritterBlackboard>
	{
		private bool anyAttack;

		public AmAttacking (bool anyAttack = true)
		{
			this.anyAttack = anyAttack;
		}

		public override bool Check (Critter checkEntity)
		{
			if (anyAttack || checkEntity == null) {
				return GetBlackboard ().Critter.GetPlanes ((int)PlaneType.Attack, null) > 0;
			} else {
				var planes = new NpcPlaneArray ();
				GetBlackboard ().Critter.GetPlanes ((int)PlaneType.Attack, planes);
				foreach (var plane in planes) {
					if (plane.Attack_TargId == checkEntity.Id)
						return true;
				}
				return false;
			}
		}
	}
}


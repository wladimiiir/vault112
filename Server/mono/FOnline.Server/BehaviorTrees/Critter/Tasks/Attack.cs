using System;

namespace FOnline.BT
{
	public class Attack : CritterTask
	{
		private string[] critterKeys;
		private int specialAttackFlags;

		public Attack (params string[] critterKeys) : this(0, critterKeys)
		{
		}

		public Attack (int specialAttackFlags, params string[] critterKeys)
		{
			this.specialAttackFlags = specialAttackFlags;
			this.critterKeys = critterKeys;
		}

		public override TaskState Execute ()
		{
			if (GetCritter ().GetPlanes ((int)PlaneType.Attack, null) > 0)
				return TaskState.Running; //attacking someone

			bool foundAttacker = false;

			foreach (var key in critterKeys) {
				foreach (var critterToAttack in GetBlackboard().GetCritters(key)) {
					foundAttacker |= TryToAttack (critterToAttack);
				}
			}

			return foundAttacker ? TaskState.Success : TaskState.Failed;
		}

		private bool TryToAttack (Critter critterToAttack)
		{
			if (!Check (critterToAttack))
				return false;

			//TODO: maybe check if not already attacking the critter

			if (specialAttackFlags != 0)
				GetCritter ().Mode [Modes.SpecialAttackFlags] = specialAttackFlags;
			NpcPlanes.AddAttackPlane (GetCritter (), Priorities.Attack, critterToAttack, true);
			return true;
		}
	}
}


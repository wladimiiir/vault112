using System;

namespace FOnline.BT
{
	public enum LootType
	{
		Hold,
		Save,
		Delete
	}

	public class Loot : CritterTask
	{
		private LootType lootType;
		private string[] critterKeys;

		public Loot (params string[] critterKeys) : this(LootType.Hold, critterKeys)
		{
		}

		public Loot (LootType lootType, params string[] critterKeys)
		{
			this.lootType = lootType;
			this.critterKeys = critterKeys;
		}

		public override TaskState Execute ()
		{
			bool found = false;
			foreach (var critter in GetBlackboard().GetCritters(critterKeys)) {
				if (!Check (critter))
					continue;
				found = true;
			}

			return found ? TaskState.Success : TaskState.Failed;
		}
	}
}


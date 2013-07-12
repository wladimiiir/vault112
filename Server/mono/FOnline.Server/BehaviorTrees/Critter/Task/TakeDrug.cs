using System;
using FOnline.AngelScript;

namespace FOnline.BT
{
	public class TakeDrug : CritterTask
	{
		private readonly dynamic drugsModule = ScriptEngine.GetModule("main");
		private ushort[] drugPids;

		public TakeDrug (params ushort[] drugPids)
		{
			this.drugPids = drugPids;
		}

		public override TaskState Execute ()
		{
			var map = GetCritter ().GetMap ();
			var apCost = map != null && map.IsTurnBased() ? Global.TbApCostUseItem : Global.RtApCostUseItem;

			Item itemToUse = GetItemToUse ();
			if (itemToUse == null)
				return TaskState.Failed;

			UseDrug(itemToUse);
			GetCritter ().Animate (0, Anim2.Use, null, true, true);
			GetCritter ().Stat [Stats.CurrentAP] -= (int)apCost * 100;
			GetCritter ().Wait (2000);

			return TaskState.Success;
		}

		private void UseDrug (Item drug)
		{
			drugsModule.UseDrug(GetCritter(), drug);
		}

		private Item GetItemToUse ()
		{
			foreach (var itemPid in drugPids) {
				var item = GetCritter ().GetItem (itemPid);
				if (item != null)
					return item;
			}
		
			return null;
		}
	}
}


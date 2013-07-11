using System;

namespace FOnline.BT
{
	public class IsArmed : CritterCheckCondition<CritterBlackboard>
	{
		public override bool Check (Critter checkEntity)
		{
			var item = checkEntity.GetItemHand ();
			return item != null && item.GetType () == ItemTypes.Weapon;
		}
	}
}


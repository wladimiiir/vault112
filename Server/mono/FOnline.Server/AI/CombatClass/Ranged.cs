using System;
using FOnline.AI;
using System.Globalization;

namespace FOnline
{
	public class Ranged : ICombatClass
	{
		public Critter ChooseNextTarget (Critter npc)
		{
			var enemyStack = new UIntArray ();
			npc.GetEnemyStack (enemyStack);

			for (int i = 0; i < enemyStack.Length; i++) {
				if (enemyStack [i] != 0) {
					var target = npc.GetMap ().GetCritter (enemyStack [i]);
					if (target != null)
						return target;
				}
			}

			return null;
		}

		public UInt16Array ChoosePosition (Critter npc, Critter target)
		{
			return null;
		}

		public AttackChoice ChooseAttack (Critter npc, Critter target)
		{
			var attackChoice = new AttackChoice ();
			var currentItem = npc.GetItemHand ();
			Item weaponItem = null;

			if (currentItem != null) {
				if (CanBeMyWeapon (npc, currentItem)) {
					weaponItem = currentItem;
				}
			} else {
				weaponItem = null;
			}

			if (weaponItem == null) {
				return null;
			}
			attackChoice.WeaponId = weaponItem.Id;
			attackChoice.WeaponUse = 0;
			attackChoice.HitLocation = HitLocation.Uncalled;

			return attackChoice;
		}

		private bool CanBeMyWeapon (Critter npc, Item item)
		{
			if (item.Type != ItemType.Weapon)
				return false;

			int skill = item.Proto.WeaponSkill (0);
			return skill == Skills.SmallGuns || skill == Skills.EnergyWeapons || skill == Skills.BigGuns;
		}

		public ItemChoice ChooseItem (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}

		public SkillChoice ChooseSkill (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}
	}
}


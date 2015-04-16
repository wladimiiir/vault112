using System;
using FOnline.AI;
using System.Globalization;

namespace FOnline
{
	public class Ranged : ICombatClass
	{
		public Critter ChooseNextTarget (Critter npc)
		{
			UIntArray enemyStack = new UIntArray ();
			npc.GetEnemyStack (enemyStack);

			if (enemyStack.Length == 0)
				return null;

			if (enemyStack.Length == 1)
				return Global.GetCritter (enemyStack [0]);

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


using System;
using FOnline.AI;
using System.Globalization;

namespace FOnline
{
	public class Ranged : ICombatClass
	{
		public Critter ChooseNextTarget (Critter npc, Critter currentTarget)
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

		public UInt16Array ChooseAttackPosition (Critter npc, Critter target, AttackChoice attackChoice)
		{

			var weapon = npc.GetItemById (attackChoice.WeaponId);
			if (weapon == null)
				return null;

			var weaponDistance = weapon.Proto.WeaponMaxDist (attackChoice.WeaponUse);
			var distance = Global.GetDistance (npc, target);

			if (weaponDistance < distance) {
				var direction = Global.GetDirection (npc, target);
				var hexX = npc.HexX;
				var hexY = npc.HexY;

				do {
					npc.GetMap ().MoveHexByDir (ref hexX, ref hexY, direction, 1);
				} while(!npc.GetMap ().IsHexPassed (hexX, hexY));

				var result = new UInt16Array ();
				result.Add (hexX);
				result.Add (hexY);
				result.Add ((ushort)direction);
				return result;
			}

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


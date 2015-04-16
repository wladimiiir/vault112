using System;

namespace FOnline.AI
{
	public interface ICombatClass
	{
		Critter ChooseNextTarget (Critter npc);

		UInt16Array ChoosePosition (Critter npc, Critter target);

		AttackChoice ChooseAttack (Critter npc, Critter target);

		ItemChoice ChooseItem (Critter npc, Critter target);

		SkillChoice ChooseSkill (Critter npc, Critter target);

	}

	public class AttackChoice
	{
		public uint WeaponId { get; set; }

		public uint WeaponUse { get; set; }

		public uint UnarmedProtoId { get; set; }

		public HitLocation HitLocation { get; set; }
	}

	public class ItemChoice
	{
		public uint ItemId { get; set; }
	}

	public class SkillChoice
	{
		public uint Skill { get; set; }
	}
}


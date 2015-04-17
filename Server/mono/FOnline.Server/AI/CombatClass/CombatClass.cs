using System;

namespace FOnline.AI
{
	public interface ICombatClass
	{
		Critter ChooseNextTarget (Critter npc, Critter currentTarget);

		UInt16Array ChooseAttackPosition (Critter npc, Critter target, AttackChoice attackChoice);

		AttackChoice ChooseAttack (Critter npc, Critter target);

		ItemChoice ChooseItem (Critter npc, Critter target);

		SkillChoice ChooseSkill (Critter npc, Critter target);

	}

	public class AttackChoice
	{
		public uint WeaponId { get; set; }

		public byte WeaponUse { get; set; }

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


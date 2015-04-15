using System;

namespace FOnline.AI
{
	public interface ICombatClass
	{
		ICombatAction ChooseNextAction (Critter npc);

		Critter ChooseNextTarget (Critter npc);

		UInt16Array GetNextPosition (Critter npc, Critter target);

		int ChooseNextItemId (Critter npc, Critter target);

		uint ChooseWeaponId (Critter npc, Critter target);

		uint ChooseWeaponUse (Critter npc, Critter target);

		uint ChooseUnarmedProtoId (Critter npc, Critter target);

		uint ChooseHitLocation (Critter npc, Critter target);
	}
}


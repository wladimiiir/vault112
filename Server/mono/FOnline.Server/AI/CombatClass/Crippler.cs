using System;

namespace FOnline.AI
{
	public class Crippler : ICombatClass
	{
		public Crippler ()
		{
		}

		public ICombatAction ChooseNextAction (Critter npc)
		{
			throw new NotImplementedException ();
		}

		public Critter ChooseNextTarget (Critter npc)
		{
			throw new NotImplementedException ();
		}

		public UInt16Array GetNextPosition (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}

		public int ChooseNextItemId (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}

		public uint ChooseWeaponId (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}

		public uint ChooseWeaponUse (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}

		public uint ChooseUnarmedProtoId (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}

		public uint ChooseHitLocation (Critter npc, Critter target)
		{
			throw new NotImplementedException ();
		}
	}
}


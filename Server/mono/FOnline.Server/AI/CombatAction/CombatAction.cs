using System;

namespace FOnline.AI
{
	public interface ICombatAction
	{
		void PerformAction(Critter npc, Critter target);
	}
}


using System;
using FOnline.BT;

namespace FOnline
{
	public class VirtualRealityRoles
	{
		/**
		 * Functions to be bind by AS
		 */
		public static Critter InitCritter1(IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitCritter1(critter);
			return critter;
		}

		/**
		 * Init methods for use in Mono
		 */
		private const int VirtualRealityReinforcements = 3001;

		public static void InitCritter1(Critter npc)
		{
			var builder = new CritterBehaviorBuilder(npc);
			
			builder
				.Do(new TakeDrug(ItemPack.HealingDrugs).If(new IsHurt(70)))
				.DoSequence("Attack and call reinforcements")
					.Do(new CallReinforcements(BlackboardKeys.Attackers, BlackboardKeys.SeenAttackers, BlackboardKeys.Killers))
					.Do(new CallReinforcements(VirtualRealityReinforcements, BlackboardKeys.Attackers, BlackboardKeys.Killers))
					.Do(new Attack(BlackboardKeys.Attackers))
				.End()
				.Do(new ProvideReinforcements()).IfNot(new AmAttacking())
				.Do(new ProvideReinforcements(VirtualRealityReinforcements)).IfNot(new AmAttacking()).If(new SeesCritter())
				.DoSequence("Find player and attack him")
					.Do(new FindCritters(Find.Life | Find.OnlyPlayers).Choose(new Weakest())).If(new SeesCritter()).IfNot(new AmAttacking())
					.Do(new Attack())
				.End();
			
			Global.RegisterBehaviorTask(builder.MainTask);
		}
	}
}


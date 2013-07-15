using System;
using FOnline.BT;

namespace FOnline
{
	public class JunkyardRoles
	{
		/**
		 * Functions to be bind by AS
		 */
		public static Critter InitGuard(IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitGuard(critter);
			return critter;
		}

		public static Critter InitGyro(IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitGyro(critter);
			return critter;
		}

		/**
		 * Init methods for use in Mono
		 */ 
		public static void InitGuard(Critter npc)
		{
			var builder = new CritterBehaviorBuilder(npc);
			
			builder
				.DoSequence()
					.Do(new CallReinforcements(BlackboardKeys.Attackers, BlackboardKeys.SeenAttackers, BlackboardKeys.Killers))
					.Do(new Attack(BlackboardKeys.Attackers))
				.End()
				.DoSelection()
					.Do(new ProvideReinforcements())
				.End()
				.DoSequence()
					.Do(new Patrol(Entire.Patrol))
					.Do(new LookAround(3, Time.RealSecond(15)))
				.End();
			
			Global.RegisterBehaviorTask(builder.MainTask);
		}

		public static void InitGyro(Critter npc)
		{
			var builder = new CritterBehaviorBuilder(npc);

			builder
				.DoSequence("I am attacked")
					.Do(new CallReinforcements(BlackboardKeys.Attackers, BlackboardKeys.SeenAttackers, BlackboardKeys.Killers))
			//TODO: maybe run to guard
				.End();

			Global.RegisterBehaviorTask(builder.MainTask);
		}
	}
}


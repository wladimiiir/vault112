using System;
using FOnline.BT;

namespace FOnline
{
	public class Vault112Roles
	{
		/**
		 * Functions to be bind by AS
		 */
		public static Critter InitGuard (IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitGuard (critter);
			return critter;
		}

		public static Critter InitPatrol (IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitPatrol (critter);
			return critter;
		}

		public static Critter InitInhabitant (IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitInhabitant (critter);
			return critter;
		}

		/**
		 * Init methods for use in Mono
		 */ 
		public static void InitGuard (Critter npc)
		{
			var builder = new CritterBehaviorBuilder (npc);
			
			builder
				.DoSequence ()
					.Do (new Attack (BlackboardKeys.Attackers))
					.Do (new BT.Say (FOnline.Say.NormOnHead, TextMsg.Text, 70140))
					.Do (new CallReinforcements (BlackboardKeys.Attackers, BlackboardKeys.Killers))
				.End ()
				.DoSelection ()
					.Do (new ProvideReinforcements ())
				.End ();
			
			Global.RegisterBehaviorTask (builder.MainTask);
		}

		public static void InitPatrol (Critter npc)
		{
			var builder = new CritterBehaviorBuilder (npc);
			
			builder
				.DoSequence ()
					.Do (new Attack (BlackboardKeys.Attackers))
					.Do (new BT.Say (FOnline.Say.NormOnHead, TextMsg.Text, 70140))
					.Do (new CallReinforcements (BlackboardKeys.Attackers, BlackboardKeys.Killers))
				.End ()
				.DoSelection ()
					.Do (new ProvideReinforcements ())
				.End ()
				.DoSequence ()
					.Do (new Patrol (Entire.Patrol))
					.Do (new LookAround (3, Time.RealSecond (3)))
				.End ();
			
			Global.RegisterBehaviorTask (builder.MainTask);
		}

		public static void InitInhabitant (Critter npc)
		{
			var builder = new CritterBehaviorBuilder (npc);

			builder
				.DoSequence ("I am attacked")
					.Do (new CallReinforcements (BlackboardKeys.Attackers, BlackboardKeys.Killers))
			//TODO: maybe run to guard
				.End ();

			Global.RegisterBehaviorTask (builder.MainTask);
		}
	}
}


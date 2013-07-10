using System;
using FOnline.BT;

namespace FOnline
{
	public class Vault112Roles
	{
		public static Critter InitGuard (IntPtr ptr, bool firstTime)
		{
			Critter npc = (Critter)ptr;
			CritterBehaviorBuilder builder = new CritterBehaviorBuilder (npc);
			
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
			return npc;
		}

		public static Critter InitPatrol (IntPtr ptr, bool firstTime)
		{
			Critter npc = (Critter)ptr;
			CritterBehaviorBuilder builder = new CritterBehaviorBuilder (npc);
			
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
			return npc;
		}
	}
}


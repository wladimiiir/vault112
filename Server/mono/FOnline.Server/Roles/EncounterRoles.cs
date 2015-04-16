using System;
using FOnline.BT;

namespace FOnline
{
	public class EncounterRoles
	{
		/**
		 * Functions to be bind by AS
		 */
		public static Critter InitHumanEvil (IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitHumanEvil (critter);
			return critter;
		}

		public static Critter InitHumanGood (IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitHumanGood (critter);
			return critter;
		}

		public static Critter InitAnimalEvil (IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitAnimalEvil (critter);
			return critter;
		}

		public static Critter InitAnimalGood (IntPtr ptr)
		{
			var critter = (Critter)ptr;
			InitAnimalGood (critter);
			return critter;
		}

		/**
		 * Init methods for use in Mono
		 */ 
		public static void InitHumanEvil (Critter critter)
		{
			var builder = new CritterBehaviorBuilder (critter);

			builder
				.Do (new CallReinforcements (BlackboardKeys.Attackers))
				.Do (new TakeDrug (ItemPack.HealingDrugs)).If (new IsHurt (70))
				.DoSequence ("20% to find weakest player and attack him instead")
					.Do (new AtFixedRate<CritterBlackboard> (Time.RealSecond (10)))
					.Do (new AtChance<CritterBlackboard> (20))
					.Do (new FindCritters (Find.Life | Find.OnlyPlayers).Choose (new Weakest ())).If (new SeesCritter ()).IfNot (new IsTeamMember ())
					.Do (new Attack ())
				.End ()
				.DoSequence ("Attack every non member")
					.Do (new FindCritters (Find.Life)).If (new SeesCritter ()).IfNot (new AmAttacking ()).IfNot (new IsTeamMember ())
					.Do (new Attack ())
				.End ()
				.Do (new ProvideReinforcements ()).IfNot (new AmAttacking ());

			Global.RegisterBehaviorTask (builder.MainTask);
		}

		public static void InitHumanGood (Critter critter)
		{
			var builder = new CritterBehaviorBuilder (critter);

			builder
				.Do (new CallReinforcements (BlackboardKeys.Attackers))
				.Do (new TakeDrug (ItemPack.HealingDrugs)).If (new IsHurt (70))
				.DoSequence ("Attack enemies in sight")
					.Do (new FindCritters (Find.Life).Choose (new BT.Random ())).IfNot (new AmAttacking ()).If (new SeesCritter ()).If (new IsEnemy ())
					.Do (new Attack ())
				.End ()
				.Do (new ProvideReinforcements ()).IfNot (new AmAttacking ())
				.Do (new Attack (BlackboardKeys.Attackers));

			Global.RegisterBehaviorTask (builder.MainTask);
		}

		public static void InitAnimalEvil (Critter critter)
		{
			var builder = new CritterBehaviorBuilder (critter);

			builder
				.Do (new CallReinforcements (BlackboardKeys.Attackers))
				.DoSequence ("3% to attack weakest animal from my team - fight over the area")
					.Do (new AtFixedRate<CritterBlackboard> (Time.RealSecond (10)))
					.Do (new AtChance<CritterBlackboard> (3))
					.Do (new FindCritters (Find.Life).Choose (new Weakest ())).If (new SeesCritter ()).If (new IsTeamMember ())
					.Do (new Attack ())
				.End ()
				.DoSequence ("10% to attack animal of same type from other team")
					.Do (new AtFixedRate<CritterBlackboard> (Time.RealSecond (10)))
					.Do (new AtChance<CritterBlackboard> (10))
					.Do (new FindCritters (Find.Life).Choose (new BT.Random ())).If (new SeesCritter ()).IfNot (new IsTeamMember ())
					.Do (new Attack ())
				.End ()
				.DoSequence ("Attack every non member and not same type critter")
					.Do (new FindCritters (Find.Life)).IfNot (new AmAttacking ()).If (new SeesCritter ()).If (new IsTeamMember ()).IfNot (new IsSameType ())
					.Do (new Attack ())
				.End ()
				.Do (new ProvideReinforcements ()).IfNot (new AmAttacking ());

			Global.RegisterBehaviorTask (builder.MainTask);
		}

		public static void InitAnimalGood (Critter critter)
		{
			var builder = new CritterBehaviorBuilder (critter);

			builder
				.Do (new CallReinforcements (BlackboardKeys.Attackers))
				.Do (new ProvideReinforcements ()).IfNot (new AmAttacking ())
				.DoSequence ("Move randomly every 10-20 seconds")
					.Do (new AtFixedRate<CritterBlackboard> (Time.RealSecond (10), Time.RealSecond (20)))
					.Do (new MoveRandomly (1, 5))
				.End ();

			Global.RegisterBehaviorTask (builder.MainTask);
		}
	}
}


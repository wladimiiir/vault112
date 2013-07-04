using System;

namespace FOnline.BT
{
	public static class BlackboardKeys
	{
		public const string Attackers = "Attackers";
		public const string Killers = "Killers";

		public const string ToAttack = "ToAttack";
	}

	public class CritterBlackboard : Blackboard
	{
		private Critter critter;

		public CritterBlackboard (Critter critter)
		{
			this.critter = critter;
			InitEvents (critter);
		}

		private void InitEvents (FOnline.Critter critter)
		{
			critter.Attacked += (sender, e) => {
				AddCritters (BlackboardKeys.Attackers, e.Attacker);
			};
			critter.Dead += (sender, e) => {
				if (e.Killer != null)
					AddCritters (BlackboardKeys.Killers, e.Killer);
			};
		}

		public Critter Critter {
			get {
				return this.critter;
			}
		}
	}

	public static class Message
	{

	}
}


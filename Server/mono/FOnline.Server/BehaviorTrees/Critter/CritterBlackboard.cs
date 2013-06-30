using System;

namespace FOnline.BT
{
	public class CritterBlackboard : Blackboard
	{
		private Critter critter;
		private Critter attacker;
		private Critter killer;

		public CritterBlackboard (Critter critter)
		{
			this.critter = critter;
			InitEvents (critter);
		}

		private void InitEvents (FOnline.Critter critter)
		{
			critter.Attacked += (sender, e) => {
				attacker = e.Attacker;
			};
			critter.Dead += (sender, e) => {
				killer = e.Killer;
			};
		}

		public Critter Critter {
			get {
				return this.critter;
			}
		}

		public Critter Attacker {
			get {
				return this.attacker;
			}
			set {
				attacker = value;
			}
		}

		public Critter Killer {
			get {
				return this.killer;
			}
			set {
				killer = value;
			}
		}
	}
}


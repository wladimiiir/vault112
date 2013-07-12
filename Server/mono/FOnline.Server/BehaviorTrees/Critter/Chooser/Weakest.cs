using System;
using FOnline.BT;

namespace FOnline.BT
{
	public class Weakest : CritterChooser<CritterBlackboard>
	{
		public override Critter Choose (System.Collections.Generic.IList<Critter> entitites)
		{
			Critter weakest = null;
			foreach (var entity in entitites) {
				weakest = GetWeaker (entity, weakest);
			}
			return weakest;
		}

		private Critter GetWeaker (Critter critter, Critter otherCritter)
		{
			if (critter == null) 
				return otherCritter;
			if (otherCritter == null)
				return critter;

			return critter.Stat [Stats.CurrentHP] < otherCritter.Stat [Stats.CurrentHP] ? critter : otherCritter;
		}
	}
}


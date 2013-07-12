using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class Random : CritterFilter<CritterBlackboard>
	{
		public override IList<Critter> Filter (IList<Critter> entitites)
		{
			if (entitites.Count == 0)
				return entitites;



			return new Critter[] { entitites[Global.Random(0, entitites.Count - 1)] };
		}
	}
}


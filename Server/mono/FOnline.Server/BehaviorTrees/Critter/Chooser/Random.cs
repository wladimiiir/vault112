using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class Random : CritterChooser<CritterBlackboard>
	{
		public override Critter Choose (IList<Critter> entitites)
		{
			if (entitites.Count == 0)
				return null;

			return entitites [Global.Random (0, entitites.Count - 1)];
		}
	}
}


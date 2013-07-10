using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class CritterTask : LeafTask<CritterBlackboard>
	{


		protected Critter GetCritter ()
		{
			return GetBlackboard ().Critter;
		}

		protected void SetHomePos ()
		{
			GetCritter ().SetHomePos (GetCritter ().HexX, GetCritter ().HexY, (Direction)GetCritter ().Dir);
		}
	}
}


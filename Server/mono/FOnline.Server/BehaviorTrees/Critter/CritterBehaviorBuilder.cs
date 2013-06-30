using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class CritterBehaviorBuilder : BehaviorBuilder<CritterBehaviorBuilder, CritterTask, CritterBlackboard>
	{
		public CritterBehaviorBuilder (Critter critter) : base(new CritterBlackboard(critter))
		{
		}

		// methods for adding specific critter tasks could be added here for ease of use
		//
		// Example:
		// CritterBehaviorBuilder Say(FOnline.Say how, string text)
		// {
		//	 Do (new Say(how, text));
		//	 return this;
		// }
	}
}


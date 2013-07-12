using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class CritterBehaviorBuilder : BehaviorBuilder<CritterBehaviorBuilder, CritterBlackboard>
	{
		public CritterBehaviorBuilder (Critter critter) : base(new CritterBlackboard(critter))
		{
			InitCritterFinish (critter);

			//do nothing, if critter is dead or KO
			Do (new Nothing ().IfNot (new IsLife ()));
		}

		private void InitCritterFinish (Critter critter)
		{
			critter.Finish += (sender, e) => {
				//deregistering task
				Global.DeregisterBehaviorTask (mainTask);
			};
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


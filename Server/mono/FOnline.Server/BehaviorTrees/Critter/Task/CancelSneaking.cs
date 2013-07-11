using System;

namespace FOnline.BT
{
	public class CancelSneaking : CritterTask
	{
		public override TaskState Execute ()
		{
			bool found = false;
			foreach (var critter in GetBlackboard().GetCritters(BlackboardKeys.FoundCritters)) {
				if (critter.Mode [Modes.Hide] > 0) {
					critter.Mode [Modes.Hide] = 0;
					found = true;
				}
			}
			return found ? TaskState.Success : TaskState.Failed;
		}
	}
}


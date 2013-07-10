using System;

namespace FOnline.BT
{
	public class LookAt : CritterTask
	{
		private string critterKey;

		public LookAt (string critterKey = BlackboardKeys.FoundCritters)
		{
			this.critterKey = critterKey;
		}

		public override TaskState Execute ()
		{
			foreach (var critter in GetBlackboard().GetCritters(critterKey)) {
				GetCritter().SetDir(Global.GetDirection(GetCritter().HexX, GetCritter().HexY, critter.HexX, critter.HexY));
				return TaskState.Success;
			}
			return TaskState.Failed;
		}
	}
}


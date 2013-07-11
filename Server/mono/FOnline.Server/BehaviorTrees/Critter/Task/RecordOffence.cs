using System;
using FOnline.Data;

namespace FOnline.BT
{
	public class RecordOffence : CritterTask
	{
		private uint offenceArea;
		private uint offenceTime;
		private string[] critterKeys;

		public RecordOffence (uint offenceArea, uint offenceTime, params string[] critterKeys)
		{
			this.offenceArea = offenceArea;
			this.offenceTime = offenceTime;
			this.critterKeys = critterKeys.Length == 0 ? new string[]{BlackboardKeys.Attackers} : critterKeys;
		}

		public override TaskState Execute ()
		{
			bool found = false;
			foreach (var critter in GetBlackboard().GetCritters(critterKeys)) {
				var offenceData = new OffenceData(critter);
				offenceData.AddOffence(offenceArea, offenceTime);
			}
			return found ? TaskState.Success : TaskState.Failed;
		}
	}
}


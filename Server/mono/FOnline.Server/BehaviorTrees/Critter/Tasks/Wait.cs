using System;

namespace FOnline.BT
{
	public class Wait : CritterTask
	{
		private uint waitTimeFrom;
		private uint waitTimeTo;
		private uint waitUntil;

		public Wait (uint waitTimeFrom, uint waitTimeTo)
		{
			this.waitTimeFrom = waitTimeFrom;
			this.waitTimeTo = waitTimeTo;
		}

		public Wait (uint waitTime) : this(waitTime, waitTime)
		{
		}

		public override TaskState GetState ()
		{
			TaskState state = base.GetState ();
			if (state == TaskState.Running) {
				if(Global.FullSecond >= waitUntil)
				{
					state = TaskState.Success;
					State = TaskState.Ready;
				}
			}
			return state;
		}

		public override TaskState Execute ()
		{
			waitUntil = (uint) (Global.FullSecond + Global.Random((int) waitTimeFrom, (int) waitTimeTo));
			State = waitUntil > Global.FullSecond ? TaskState.Running : TaskState.Success;
			return state;
		}
	}
}


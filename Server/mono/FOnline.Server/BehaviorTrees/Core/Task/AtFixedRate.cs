using System;

namespace FOnline.BT
{
	public class AtFixedRate : LeafTask<Blackboard>
	{
		private int rateFrom;
		private int rateTo;
		private uint nextRun = 0;

		public AtFixedRate (int rate)
		{
			this.rateFrom = rate;
			this.rateTo = rate;
		}

		public AtFixedRate (int rateRandomFrom, int rateRandomTo)
		{
			this.rateFrom = rateRandomFrom;
			this.rateTo = rateRandomTo;
		}

		public override TaskState Execute ()
		{
			if (Global.FullSecond < nextRun)
				return TaskState.Failed;

			nextRun = Time.After ((uint)Global.Random (rateFrom, rateTo));
			return TaskState.Success;
		}
	}
}


using System;

namespace FOnline.BT
{
	public class AtFixedRate<B> : LeafTask<B> where B : Blackboard
	{
		private uint rateFrom;
		private uint rateTo;
		private uint nextRun = 0;

		public AtFixedRate (uint rate)
		{
			this.rateFrom = rate;
			this.rateTo = rate;
		}

		public AtFixedRate (uint rateRandomFrom, uint rateRandomTo)
		{
			this.rateFrom = rateRandomFrom;
			this.rateTo = rateRandomTo;
		}

		public override TaskState Execute ()
		{
			if (Global.FullSecond < nextRun)
				return TaskState.Failed;

			nextRun = Time.After (Global.Random (rateFrom, rateTo));
			return TaskState.Success;
		}
	}
}


using System;

namespace FOnline.CritterActions
{
	public class Wait : AbstractCritterAction
	{
		private uint waitTime;
		private uint waitingTo = 0;

		public Wait (uint waitTime)
		{
			this.waitTime = waitTime;
		}

		protected override bool PerformAction (Critter critter)
		{
			waitingTo = Time.After (waitTime);
			Listening = true;
			return false;
		}

		public override void Idle (Critter critter)
		{
			if (waitingTo > Global.Time.FullSecond)
				return;

			Listening = false;
			StartNextActions (critter);
		}
	}
}


using System;

namespace FOnline.BT
{
	public class Say : CritterTask
	{
		private FOnline.Say how;
		private string text;

		public Say (FOnline.Say how, string text)
		{
			this.how = how;
			this.text = text;
		}

		public override TaskState Execute ()
		{
			GetBlackboard ().Critter.Say (how, text);
			return TaskState.Success;
		}
	}
}


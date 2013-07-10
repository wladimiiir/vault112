using System;

namespace FOnline.BT
{
	public class Say : CritterTask
	{
		private FOnline.Say how;
		private ushort textMsg;
		private uint strNum;
		private string text;

		public Say (FOnline.Say how, string text)
		{
			this.how = how;
			this.text = text;
		}

		public Say (FOnline.Say how, ushort textMsg, uint strNum)
		{
			this.how = how;
			this.textMsg = textMsg;
			this.strNum = strNum;
		}

		public override TaskState Execute ()
		{
			if(text != null)
				GetCritter().Say (how, text);
			else
				GetCritter().SayMsg(how, textMsg, strNum);
			return TaskState.Success;
		}
	}
}


using System;

namespace FOnline.CritterActions
{
	public class Say : AbstractCritterAction
	{
		private FOnline.Say howSay;
		private string message;
		private ushort textMsg;
		private uint strNum;
		private string lexems;

		public Say (string message, FOnline.Say howSay = FOnline.Say.NormOnHead)
		{
			this.howSay = howSay;
			this.message = message;
		}

		public Say (ushort textMsg, uint strNum, FOnline.Say howSay = FOnline.Say.NormOnHead, string lexems = null)
		{
			this.howSay = howSay;
			this.textMsg = textMsg;
			this.strNum = strNum;
			this.lexems = lexems;
		}

		protected override bool PerformAction (Critter critter)
		{
			if (message == null)
				critter.SayMsg (howSay, textMsg, strNum, lexems);
			else
				critter.Say (howSay, message); 

			return true;
		}
	}
}


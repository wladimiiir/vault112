using System;
using FOnline.BT;

namespace FOnline
{
	class Roles
	{

		public static Critter InitBully (IntPtr ptr, bool firstTime)
		{
			Global.Log ("Initializing bully");
			Critter npc = new Critter (ptr);
			CritterBehaviorBuilder builder = new CritterBehaviorBuilder (npc);

			builder
			.DoSequence ()
				.Do (new BT.Say (FOnline.Say.NormOnHead, "Hello Wasteland!"))
				.Do (new ChangeDirection ())
				.Do (new Wait (Time.RealSecond (3), Time.RealSecond (10)))
			.End ();

			Global.RegisterBehaviorTask (builder.MainTask);
			return npc;
		}
	}
}


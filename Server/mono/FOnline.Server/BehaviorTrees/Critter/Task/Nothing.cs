using System;

namespace FOnline.BT
{
	public class Nothing : CritterTask
	{
		public override TaskState Execute ()
		{
			return Check (GetCritter ()) ? TaskState.Success : TaskState.Failed;
		}
	}
}


using System;

namespace FOnline.BT
{
	public abstract class Condition<BlackboardType> : LeafTask<BlackboardType> where BlackboardType : Blackboard
	{
		protected abstract bool Check();

		public override TaskState Execute ()
		{
			return Check() ? TaskState.Success : TaskState.Failed;
		}
	}
}


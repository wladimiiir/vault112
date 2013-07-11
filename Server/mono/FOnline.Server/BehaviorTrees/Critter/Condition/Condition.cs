using System;

namespace FOnline.BT
{
	public abstract class Condition<B, T> where B : Blackboard
	{
		private B blackboard;

		public B Blackboard {
			set {
				blackboard = value;
			}
		}
		
		protected virtual B GetBlackboard() 
		{
			return blackboard;
		}

		public abstract bool Check (T checkEntity);
	}
}


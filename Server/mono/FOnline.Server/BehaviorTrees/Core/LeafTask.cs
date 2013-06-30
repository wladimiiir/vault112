using System;

namespace FOnline.BT
{
	public abstract class LeafTask<BlackboardType> : Task where BlackboardType : Blackboard 
	{
		protected BlackboardType blackboard;

		public BlackboardType Blackboard {
			set {
				blackboard = value;
			}
		}

		protected virtual BlackboardType GetBlackboard() 
		{
			return blackboard;
		}
	}
}


using System;

namespace FOnline.BT
{
	public class NegativeContidion<B, T> : Condition<B, T> where B : Blackboard
	{
		private Condition<B, T> positiveCondition;

		public NegativeContidion (Condition<B, T> condition)
		{
			positiveCondition = condition;
		}

		public override bool Check (T checkEntity)
		{
			return !positiveCondition.Check(checkEntity);
		}
	}
}


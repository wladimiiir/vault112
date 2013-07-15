using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class LeafTask<B> : Task where B : Blackboard
	{
		protected B blackboard;
		protected IList<Condition<B, Critter>> critterConditions = new List<Condition<B, Critter>> ();
		protected IList<Condition<B, Item>> itemConditions = new List<Condition<B, Item>> ();
		
		internal LeafTask<B> If (CritterCheckCondition<B> condition)
		{
			condition.Blackboard = blackboard;
			critterConditions.Add (condition);
			return this;
		}

		internal LeafTask<B> If (ItemCheckCondition<B> condition)
		{
			condition.Blackboard = blackboard;
			itemConditions.Add (condition);
			return this;
		}

		internal LeafTask<B> IfNot (CritterCheckCondition<B> condition)
		{
			condition.Blackboard = blackboard;
			critterConditions.Add (new NegativeContidion<B, Critter> (condition));
			return this;
		}
		
		internal LeafTask<B> IfNot (ItemCheckCondition<B> condition)
		{
			condition.Blackboard = blackboard;
			itemConditions.Add (new NegativeContidion<B, Item> (condition));
			return this;
		}
		
		protected bool Check (Critter critter)
		{
			foreach (var condition in critterConditions) {
				if (!condition.Check (critter))
					return false;
			}
			return true;
		}
		
		protected bool Check (Item item)
		{
			foreach (var condition in itemConditions) {
				if (!condition.Check (item))
					return false;
			}
			return true;
		}

		public B Blackboard {
			set {
				blackboard = value;
			}
		}

		protected virtual B GetBlackboard ()
		{
			return blackboard;
		}
	}
}


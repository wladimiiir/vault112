using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class Task
	{
		protected IList<ICondition<Critter>> critterConditions = new List<ICondition<Critter>> ();
		protected IList<ICondition<Item>> itemConditions = new List<ICondition<Item>> ();
		protected TaskState state = TaskState.Ready;

		public TaskState State {
			set {
				state = value;
			}
		}

		public virtual Task If (ICondition<Critter> condition)
		{
			critterConditions.Add (condition);
			return this;
		}
		
		public virtual Task If (ICondition<Item> condition)
		{
			itemConditions.Add (condition);
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

		public abstract TaskState Execute ();

		public virtual TaskState GetState ()
		{
			return state;
		}
	}
}


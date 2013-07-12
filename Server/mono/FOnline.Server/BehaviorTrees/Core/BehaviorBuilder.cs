using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class BehaviorBuilder<BuilderType,  BlackboardType> 
		where BuilderType : BehaviorBuilder<BuilderType, BlackboardType> 
			where BlackboardType : Blackboard
	{
		private BlackboardType blackboard;
		private MainTask mainTask;
		private Queue<CompositeTask> compositeQueue;
		private LeafTask<BlackboardType> lastTask;

		public BehaviorBuilder (BlackboardType blackboard)
		{
			mainTask = new MainTask (blackboard);
			compositeQueue = new Queue<CompositeTask> ();
			this.blackboard = blackboard;
		}

		public MainTask MainTask {
			get {
				return this.mainTask;
			}
		}

		private CompositeTask GetCurrentCompositeTask ()
		{
			return compositeQueue.Count == 0 ? mainTask : compositeQueue.Peek ();
		}

		public BuilderType Do (CompositeTask compositeTask)
		{
			foreach (var task in compositeTask.GetTasks()) {
				var leafTask = task as LeafTask<BlackboardType>;
				if (leafTask != null)
					leafTask.Blackboard = this.blackboard;
			}
			GetCurrentCompositeTask ().AddTask (compositeTask);
			return (BuilderType)this;
		}

		public BuilderType Do (LeafTask<BlackboardType> task)
		{

			task.Blackboard = this.blackboard;
			GetCurrentCompositeTask ().AddTask (task);
			lastTask = task;
			return (BuilderType)this;
		}

		public BuilderType DoSequence (string sequenceName = "Unspecified")
		{
			var sequence = new Sequence (sequenceName);
			GetCurrentCompositeTask ().AddTask (sequence);
			compositeQueue.Enqueue (sequence);
			return (BuilderType)this;
		}

		public BuilderType DoSelection (string selectionName = "Unspecified")
		{
			var selector = new Selector (selectionName);
			GetCurrentCompositeTask ().AddTask (selector);
			compositeQueue.Enqueue (selector);
			return (BuilderType)this;
		}

		public BuilderType If (CritterCheckCondition<BlackboardType> condition)
		{
			if (lastTask == null) {
				Global.Log ("Trying to add condition on not existing task");
				return (BuilderType)this;
			}
			lastTask.If (condition);
			return (BuilderType)this;
		}

		public BuilderType If (ItemCheckCondition<BlackboardType> condition)
		{
			if (lastTask == null) {
				Global.Log ("Trying to add condition on not existing task");
				return (BuilderType)this;
			}
			lastTask.If (condition);
			return (BuilderType)this;
		}

		public BuilderType IfNot (CritterCheckCondition<BlackboardType> condition)
		{
			if (lastTask == null) {
				Global.Log ("Trying to add condition on not existing task");
				return (BuilderType)this;
			}
			lastTask.IfNot (condition);
			return (BuilderType)this;
		}

		public BuilderType IfNot (ItemCheckCondition<BlackboardType> condition)
		{
			if (lastTask == null) {
				Global.Log ("Trying to add condition on not existing task");
				return (BuilderType)this;
			}
			lastTask.IfNot (condition);
			return (BuilderType)this;
		}

		public BuilderType End ()
		{
			if (compositeQueue.Count == 0) {
				Global.Log ("End() invoked but there is no started composite task. Too many End() calls?");
			} else {
				compositeQueue.Dequeue ();
			}
			return (BuilderType)this;
		}
	}
}


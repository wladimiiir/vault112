using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class BehaviorBuilder<BuilderType, TaskType, BlackboardType> 
		where BuilderType : BehaviorBuilder<BuilderType, TaskType, BlackboardType> 
		where TaskType : LeafTask<BlackboardType> 
			where BlackboardType : Blackboard
	{
		private BlackboardType blackboard;
		private CompositeTask mainTask;
		private Queue<CompositeTask> compositeQueue;

		public BehaviorBuilder (BlackboardType blackboard)
		{
			mainTask = new Sequence ();
			compositeQueue = new Queue<CompositeTask> ();
			this.blackboard = blackboard;
		}

		public Task MainTask {
			get {
				return this.mainTask;
			}
		}
		
		private CompositeTask GetCurrentTask ()
		{
			return compositeQueue.Count == 0 ? mainTask : compositeQueue.Peek ();
		}
		
		public BuilderType Do (TaskType task)
		{

			task.Blackboard = this.blackboard;
			GetCurrentTask ().AddTask (task);
			return (BuilderType)this;
		}
		
		public BuilderType DoSequence ()
		{
			var sequence = new Sequence ();
			GetCurrentTask ().AddTask (sequence);
			compositeQueue.Enqueue (sequence);
			return (BuilderType)this;
		}
		
		public BuilderType DoSelection ()
		{
			var selector = new Selector ();
			GetCurrentTask ().AddTask (selector);
			compositeQueue.Enqueue (selector);
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


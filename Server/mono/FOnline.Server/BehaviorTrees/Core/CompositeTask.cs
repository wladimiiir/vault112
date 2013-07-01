using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class CompositeTask : Task
	{
		protected IList<Task> SubTasks = new List<Task> ();

		public CompositeTask ()
		{
		}

		public override TaskState GetState ()
		{
			var state = TaskState.Ready;
//			foreach (var task in SubTasks) {
//				switch (task.GetState ()) {
//				case TaskState.Running:
//					return TaskState.Running;
//				case TaskState.Failed:
//					return TaskState.Failed;
//				default:
//					break;
//				}
//			}
			return state;
		}

		public CompositeTask AddTask (Task task)
		{
			SubTasks.Add (task);
			return this;
		}
	}
}


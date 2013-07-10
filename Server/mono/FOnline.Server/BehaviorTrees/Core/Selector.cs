using System;

namespace FOnline.BT
{
	public class Selector : CompositeTask
	{
		protected int currentTaskIndex = 0;

		public Selector ()
		{
		}

		public override TaskState Execute ()
		{
			if (SubTasks.Count == 0)
				return TaskState.Failed;
			var currentTask = SubTasks [currentTaskIndex];

			return ProcessByState (currentTask, currentTask.GetState ());
		}

		private TaskState ProcessByState (Task processedTask, TaskState state)
		{
			//Global.Log ("Processing task in selector (" + ToString () + "): " + processedTask.ToString () + " with state: " + state.ToString ());
			switch (state) {
			case TaskState.Ready:
				return ExecuteSubTask (processedTask);
			case TaskState.Running:
				return TaskState.Running;
			case TaskState.Failed:
				processedTask.State = TaskState.Ready;
				currentTaskIndex++;
				if (currentTaskIndex >= SubTasks.Count) {
					currentTaskIndex = 0;
					return TaskState.Failed;
				}
				return ExecuteSubTask (SubTasks [currentTaskIndex]);
			case TaskState.Success:
				processedTask.State = TaskState.Ready;
				//setting next task to first sub action
				currentTaskIndex = 0;
				return TaskState.Success;
			default:
				//unexpected
				return TaskState.Failed;
			}
		}

		private TaskState ExecuteSubTask (Task subTask)
		{
			//Global.Log ("Executing sequence subtask of selector: " + subTask.ToString ());
			var state = subTask.Execute ();
			if (state == TaskState.Ready)
				return TaskState.Success; //maybe failed as this is unexpected
			return ProcessByState (subTask, state);
		}
	}
}


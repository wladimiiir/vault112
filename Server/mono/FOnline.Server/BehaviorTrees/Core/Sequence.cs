using System;

namespace FOnline.BT
{
	public class Sequence : CompositeTask
	{
		private readonly string name;
		private int currentTaskIndex = 0;

		public Sequence (string name = "Unspecified")
		{
			this.name = name;
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
			//Global.Log ("Processing task of sequence (" + ToString () + "): " + processedTask.ToString() + " with state: " + state.ToString());
			switch (state) {
			case TaskState.Ready:
				return ExecuteSubTask (processedTask);
			case TaskState.Running:
				return TaskState.Running;
			case TaskState.Success:
				processedTask.State = TaskState.Ready;
				currentTaskIndex++;
				if (currentTaskIndex >= SubTasks.Count) {
					currentTaskIndex = 0;
					return TaskState.Success;
				}
				return TaskState.Running;
			case TaskState.Failed:
				processedTask.State = TaskState.Ready;
				//setting next task to first sub action
				currentTaskIndex = 0;
				return TaskState.Failed;
			default:
				//unexpected
				return TaskState.Failed;
			}
		}

		private TaskState ExecuteSubTask (Task subTask)
		{
			//Global.Log ("Executing sequence subtask in sequence: " + subTask.ToString ());
			var state = subTask.Execute ();
			if (state == TaskState.Ready)
				return TaskState.Success; //maybe failed as this is unexpected
			return ProcessByState (subTask, state);
		}
	}
}


using System;

namespace FOnline.BT
{
	public class Sequence : CompositeTask
	{
		private int currentTaskIndex = 0;
		
		public Sequence ()
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
			Global.Log ("Processing task: " + processedTask.ToString() + " with state: " + state.ToString());
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
				//executing next task
				return ExecuteSubTask (SubTasks [currentTaskIndex]);
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
			Global.Log ("Executing sequence subtask: " + subTask.ToString ());
			var state = subTask.Execute ();
			if (state == TaskState.Ready)
				return TaskState.Success; //maybe failed as this is unexpected
			Global.Log ("Executed task returned: " + state);
			return ProcessByState (subTask, state);
		}
	}
}


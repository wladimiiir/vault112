using System;

namespace FOnline.BT
{
	public class MainTask : Selector
	{
		private Blackboard blackboard;

		public MainTask (Blackboard blackboard)
		{
			this.blackboard = blackboard;
		}

		public Blackboard Blackboard {
			get {
				return this.blackboard;
			}
		}

		public override TaskState Execute ()
		{
			//set start time of main task cycle
			blackboard.MainTaskExecutionStart ();
			//always execute tasks from start
			currentTaskIndex = 0;

			var taskState = base.Execute ();
			//Global.Log ("MainTask finished with state: " + taskState);
			//if tasks are successfull, we clean blackboard entities (e.g. from events)
			if (taskState == TaskState.Success)
				blackboard.ClearEntities ();

			return taskState;
		}
	}
}


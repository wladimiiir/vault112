using System;
using System.Collections.Generic;
using System.Threading;

namespace FOnline.BT
{
	public class Controller
	{
		private IList<Task> tasks = new List<Task>();
		private Timer executeTimer;

		public Controller ()
		{
		}

		public void Start()
		{
			executeTimer = new Timer(Execute, null, 0, 100);
		}

		public void Stop ()
		{
			if (executeTimer != null) {
				executeTimer.Dispose();
				executeTimer = null;
			}
		}

		public void RegisterTask(Task task)
		{
			tasks.Add(task);
		}

		void Execute (Object state)
		{
			foreach (var task in tasks) {
				if(task.GetState() == TaskState.Running)
					continue;
				task.Execute();
			}
		}
	}
}


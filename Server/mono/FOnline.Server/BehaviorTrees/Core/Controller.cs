using System;
using System.Collections.Generic;
using System.Threading;

namespace FOnline.BT
{
	public class Controller
	{
		private IList<Task> tasks = new List<Task> ();
		private static bool running = false;

		public void Start ()
		{
			running = true;
			Global.CreateTimeEvent (Global.FullSecond + Time.RealSecond (1), ExecuteTasks, false);
		}

		public void Stop ()
		{
			running = false;
		}

		public void RegisterTask (Task task)
		{
			tasks.Add (task);
		}

		public static uint ExecuteTasks(IntPtr ptr)
		{
			Global.BTController.Execute (null);
			return running ? Time.RealMillisecond (200) : 0;
		}

		void Execute (Object state)
		{
			foreach (var task in tasks) {
				if (task.GetState () == TaskState.Running)
					continue;
				try {
					task.Execute ();
				} catch (Exception ex) {
					Global.Log ("Exception occured: " + ex.Message + "\n" + ex.ToString ());
				}
			}
		}
	}
}


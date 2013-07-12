using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace FOnline.BT
{
	public class Controller
	{
		private IList<MainTask> tasks = new List<MainTask> ();
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

		public void RegisterTask (MainTask task)
		{
			tasks.Add (task);
		}

		public void DeregisterTask (MainTask task)
		{
			tasks.Remove (task);
		}

		public static uint ExecuteTasks (IntPtr ptr)
		{
			Global.BTController.Execute (null);
			return running ? Time.RealMillisecond (300) : 0;
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


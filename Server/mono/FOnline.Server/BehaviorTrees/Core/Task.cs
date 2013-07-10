using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class Task
	{
		protected TaskState state = TaskState.Ready;

		public TaskState State {
			set {
				state = value;
			}
		}

		public abstract TaskState Execute ();

		public virtual TaskState GetState ()
		{
			return state;
		}
	}
}


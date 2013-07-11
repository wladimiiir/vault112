using System;

namespace FOnline.BT
{
	public class LookAround : Sequence
	{
		public LookAround (uint lookCount, uint lookTime)
		{
			for (int i = 0; i < lookCount; i++) {
				AddTask(new ChangeDirection());
				AddTask(new Wait(lookTime));
			}
		}
	}
}


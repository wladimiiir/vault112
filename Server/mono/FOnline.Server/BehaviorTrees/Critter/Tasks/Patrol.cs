using System;

namespace FOnline.BT
{
	public class Patrol : CritterTask
	{
		private int entireNumber;
		private bool run;
		private int currentIdentifier = -1;

		public Patrol (int entireNumber, bool run = false)
		{
			this.entireNumber = entireNumber;
			this.run = run;
		}

		public override TaskState GetState ()
		{
			if (state == TaskState.Running) {
				if (currentIdentifier == -1 
					|| GetCritter ().GetPlanes (CritterDefines.PlaneIdentifier.Patrol, currentIdentifier, null) == 0) {
					currentIdentifier = -1;
					State = TaskState.Ready;
					return TaskState.Success;
				}
			}
			return state;
		}

		public override TaskState Execute ()
		{
			short hexX = GetCritter ().HexX;
			short hexY = GetCritter ().HexY;

			if (!FindPatrolHex (ref hexX, ref hexY))
				return TaskState.Failed;

			var dir = Global.GetDirection (GetCritter ().HexX, GetCritter ().HexY, hexX, hexY);
			currentIdentifier = GetBlackboard ().GenerateUID ();
			NpcPlanes.AddWalkPlane (PlaneType.Walk, CritterDefines.PlaneIdentifier.Patrol, currentIdentifier, dir, run, 0);

			return TaskState.Running;
		}

		private bool FindPatrolHex (ref short hexX, ref short hexY)
		{
			var map = GetCritter ().GetMap ();
			if (map == null)
				return false;

			UIntArray entires = new UIntArray ();
			UInt16Array hexXs = new UInt16Array ();
			UInt16Array hexYs = new UInt16Array ();

			var entireCount = map.GetEntires (entireNumber, entires, hexXs, hexYs);
			if (entireCount == 0)
				return false;

			for (int i = 0; i < 10; i++) {
				var index = Global.Random (0, entireCount - 1);
				if (hexX != hexXs [index] && hexY != hexYs [index]) {
					hexX = hexXs [index];
					hexY = hexYs [index];
					return true;
				}
			}
			//not found any suitable entire
			return false;
		}
	}
}


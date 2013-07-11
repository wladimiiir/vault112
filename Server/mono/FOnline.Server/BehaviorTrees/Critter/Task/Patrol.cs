using System;

namespace FOnline.BT
{
	public class Patrol : CritterTask
	{
		private int entireNumber;
		private bool run;
		private uint currentIdentifier = 0;

		public Patrol (int entireNumber, bool run = false)
		{
			this.entireNumber = entireNumber;
			this.run = run;
		}

		public override TaskState GetState ()
		{
			if (state == TaskState.Running) {
				if (currentIdentifier == 0 
					|| GetCritter ().GetPlanes ((int)CritterDefines.PlaneIdentifier.Patrol, currentIdentifier, null) == 0) {
					currentIdentifier = 0;
					State = TaskState.Ready;
					return TaskState.Success;
				}
			}
			return state;
		}

		public override TaskState Execute ()
		{
			ushort hexX = GetCritter ().HexX;
			ushort hexY = GetCritter ().HexY;

			if (!FindPatrolHex (ref hexX, ref hexY))
				return TaskState.Failed;

			var dir = Global.GetDirection (GetCritter ().HexX, GetCritter ().HexY, hexX, hexY);
			currentIdentifier = GetBlackboard ().GenerateUID ("Patrol");
			NpcPlanes.AddWalkPlane (GetCritter (), (uint)PlaneType.Walk, (int)CritterDefines.PlaneIdentifier.Patrol, currentIdentifier, hexX, hexY, dir, run, 0);
			GetCritter ().SetHomePos (hexX, hexY, dir);
			State = TaskState.Running;

			return TaskState.Running;
		}

		private bool FindPatrolHex (ref ushort hexX, ref ushort hexY)
		{
			var map = GetCritter ().GetMap ();
			if (map == null) {
				return false;
			}

			UIntArray entires = new UIntArray ();
			UInt16Array hexXs = new UInt16Array ();
			UInt16Array hexYs = new UInt16Array ();

			var entireCount = map.GetEntires (entireNumber, entires, hexXs, hexYs);
			if (entireCount == 0)
				return false;

			for (int i = 0; i < 10; i++) {
				var index = Global.Random (0, (int)entireCount - 1);
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


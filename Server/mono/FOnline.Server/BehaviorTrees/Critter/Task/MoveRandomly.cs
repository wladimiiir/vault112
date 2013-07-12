using System;

namespace FOnline.BT
{
	public class MoveRandomly : CritterTask
	{
		private readonly uint minMoveDistance;
		private readonly uint maxMoveDistance;

		public MoveRandomly (uint moveDistance) : this(moveDistance, moveDistance)
		{
		}

		public MoveRandomly (uint minMoveDistance, uint maxMoveDistance)
		{
			this.minMoveDistance = minMoveDistance;
			this.maxMoveDistance = maxMoveDistance;
		}

		public override TaskState Execute ()
		{
			var map = GetCritter ().GetMap ();
			if (map == null) 
				return TaskState.Failed;

			var hexX = GetCritter ().HexX;
			var hexY = GetCritter ().HexY;
			var dir = Global.Random ((int)Direction.NorthEast, (int)Direction.NorthWest);

			map.MoveHexByDir (ref hexX, ref hexY, (Direction)dir, Global.Random (minMoveDistance, maxMoveDistance));
			GetCritter ().SetHomePos (hexX, hexY, (Direction)dir);
			return TaskState.Success;
		}
	}
}


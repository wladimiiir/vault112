using System;

namespace FOnline.BT
{
	public class ChangeDirection : CritterTask
	{
		private bool random;
		private Direction direction;

		public ChangeDirection ()
		{
			random = true;
		}

		public ChangeDirection (Direction direction)
		{
			this.direction = direction;
			random = false;
		}

		public override TaskState Execute ()
		{
			Direction oldDirection = (Direction)GetCritter ().Dir;
			Direction newDirection;
			if (random) {
				var values = Enum.GetValues (typeof(Direction));
				newDirection = oldDirection;
				while (newDirection == oldDirection) {
					newDirection = (Direction)values.GetValue (Global.Random (0, values.Length - 1));	
				}
			} else {
				newDirection = direction;
			}

			var result = (int)oldDirection - (int)newDirection;
			if (result == 0) {
				return TaskState.Success;
			} else if (result == 1 || result == -1 
				|| (oldDirection == Direction.NorthWest && newDirection == Direction.NorthEast)
				|| (oldDirection == Direction.NorthEast && newDirection == Direction.NorthWest)) {
				GetCritter ().SetDir (newDirection);
			} else {
				var map = GetCritter ().GetMap ();
				if (map == null)
					return TaskState.Failed;

				var hexX = GetCritter ().HexX;
				var hexY = GetCritter ().HexY;

				map.MoveHexByDir (ref hexX, ref hexY, newDirection, 1);
				if (!map.IsHexPassed (hexX, hexY))
					return TaskState.Failed;

				GetCritter ().MoveToDir (newDirection);
				Global.Log ("Moving to dir");
			}

			SetHomePos ();

			return TaskState.Success;
		}
	}
}


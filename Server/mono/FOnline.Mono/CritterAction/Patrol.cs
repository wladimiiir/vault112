using System;

namespace FOnline.CritterActions
{
	public class Patrol : AbstractCritterAction
	{
		private int entireNum;
		private bool run;
		private bool setHomePos;
		private bool onlyFreeEntire;
		private PatrollerWrapper patrollerWrapper;

		public Patrol (int entireNum, bool run = false, bool setHomePos = true, bool onlyFreeEntire = false)
		{
			this.entireNum = entireNum;
			this.run = run;
			this.setHomePos = setHomePos;
			this.onlyFreeEntire = onlyFreeEntire;
		}

		protected override bool PerformAction (Critter critter)
		{
			if (critter.GetMap () == null)
				return false;

			var entires = new UIntArray ();
			var hxs = new UInt16Array ();
			var hys = new UInt16Array ();

			critter.GetMap ().GetEntires (entireNum, entires, hxs, hys);

			if (entires.Count == 0)
				return false;

			var hexX = critter.HexX;
			var hexY = critter.HexY;
			var dir = critter.Dir;

			int tryCount = 10;
			if (onlyFreeEntire) {
				while (tryCount-- > 0) {
					int index = Global.Random (0, entires.Count - 1);
					hexX = hxs [index];
					hexY = hys [index];
					var patrollerWrapper = new PatrollerWrapper (critter.GetMap (), hexX, hexY);
					if (!patrollerWrapper.HasPatroller ()) {
						patrollerWrapper.SetPatroller (critter);
						this.patrollerWrapper = patrollerWrapper;
						break;
					}
				}
			} else
				while (tryCount-- > 0 && hexX == critter.HexX && hexY == critter.HexY) {
					int index = Global.Random (0, entires.Count - 1);
					hexX = hxs [index];
					hexY = hys [index];
				}
			if (tryCount == 0)
				return false;

			critter.AddWalkPlane (0, (int)PlaneIndetifier.Patrol, 0, hexX, hexY, (Direction)Global.Random (0, 5), run, 1);
			Listening = true;

			return false;
		}

		public override void Idle (Critter critter)
		{
			NpcPlaneArray planes = new NpcPlaneArray ();
			critter.GetPlanes ((int)PlaneIndetifier.Patrol, planes);

			if (planes.Count == 0)
				return;

			Listening = false;
			if (setHomePos)
				critter.SetHomePos (critter.HexX, critter.HexY, (Direction)critter.Dir);

			if (patrollerWrapper != null)
				patrollerWrapper.SetPatroller (null);

			StartNextActions (critter);
		}

		private class PatrollerWrapper
		{
			private uint mapID;
			private UInt16 hexX;
			private UInt16 hexY;
			private UInt32 patrollerID = 0;

			public PatrollerWrapper (Map map, UInt16 hexX, UInt16 hexY)
			{
				this.mapID = map.Id;
				this.hexX = hexX;
				this.hexY = hexY;
				Load ();
			}

			public void SetPatroller (Critter critter)
			{
				patrollerID = critter == null ? 0 : critter.Id;
				Save ();
			}

			public bool HasPatroller ()
			{
				return patrollerID != 0;
			}

			private string GetKey ()
			{
				return "Pattroller_" + mapID + "_" + hexX + "_" + hexY;
			}

			private void Load ()
			{
				Serializator load = new Serializator ();
				if (!load.Load (GetKey ()))
					return;

				load.Get (out patrollerID);
			}

			private void Save ()
			{
				Serializator save = new Serializator ();
				save.Set (patrollerID);
				save.Save (GetKey ());
			}
		}
	}
}


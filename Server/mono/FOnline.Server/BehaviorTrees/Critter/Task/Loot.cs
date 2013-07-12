using System;
using FOnline.AngelScript;
using FOnline.Data;

namespace FOnline.BT
{
	public enum LootType
	{
		Hold,
		Delete,
		Remember
	}

	public class Loot : CritterTask
	{
		private enum Stage
		{
			MoveToLoot,
			NextToLoot
		}

		private LootType lootType;
		private string[] critterKeys;
		private Critter currentLootCritter;
		private Stage currentStage = Stage.MoveToLoot;

		public Loot (params string[] critterKeys) : this(LootType.Hold, critterKeys)
		{
		}

		public Loot (LootType lootType, params string[] critterKeys)
		{
			this.lootType = lootType;
			this.critterKeys = critterKeys;
		}

		public override TaskState GetState ()
		{
			TaskState state = base.GetState ();
			if (currentStage == Stage.MoveToLoot && state == TaskState.Running) {
				if (GetCritter ().GetPlanes ((int)CritterDefines.PlaneIdentifier.Patrol, currentLootCritter.Id, null) == 0) {
					currentStage = Stage.NextToLoot;
					State = TaskState.Ready;
					return TaskState.Ready;
				}
			} else if (currentStage == Stage.NextToLoot) {
				//reseting, just in case
				currentStage = Stage.MoveToLoot;
			}

			return state;
		}

		public override TaskState Execute ()
		{
			switch (currentStage) {
			case Stage.MoveToLoot:
				return ProcessStageMoveToLoot ();
			case Stage.NextToLoot:
				return ProcessNextToLoot ();
			}

			return TaskState.Failed;
		}

		private TaskState ProcessStageMoveToLoot ()
		{
			foreach (var critter in GetBlackboard().GetCritters(critterKeys)) {
				if (!Check (critter))
					continue;

				var dir = Global.GetDirection (GetCritter ().HexX, GetCritter ().HexY, critter.HexX, critter.HexY);
				NpcPlanes.AddWalkPlane (GetCritter (), Priorities.Walk, (int)CritterDefines.PlaneIdentifier.Loot, critter.Id, critter.HexX, critter.HexY, dir, true, 0);
				currentLootCritter = critter;

				return TaskState.Running;
			}
			return TaskState.Failed;
		}

		private TaskState ProcessNextToLoot ()
		{
			if (currentLootCritter == null)
				return TaskState.Failed;
			//check map and distance
			if (currentLootCritter.GetMapId () != GetCritter ().GetMapId () || Global.GetCrittersDistantion (GetCritter (), currentLootCritter) > 2)
				return TaskState.Failed;

			var items = new ItemArray ();
			currentLootCritter.GetItems (null, items);
			currentLootCritter.GetMap ().GetItems (currentLootCritter.HexX, currentLootCritter.HexY, items);

			switch (lootType) {
			case LootType.Hold:
				Global.MoveItems (items, GetCritter ());
				break;
			case LootType.Delete:
				Global.DeleteItems (items);
				break;
			case LootType.Remember:
				var data = new ItemHolderData (GetCritter ());
				data.PutItems (currentLootCritter, items);
				break;
			default:
				break;
			}

			GetCritter ().Animate (0, Anim2.Pickup, null, true, true);
			GetCritter ().Wait (2000);

			//reset holders
			currentStage = Stage.MoveToLoot;
			currentLootCritter = null;

			return TaskState.Success;
		}
	}
}


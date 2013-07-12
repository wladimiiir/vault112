using System;
using System.Collections.Generic;

namespace FOnline.Data
{
	public class ItemHolderData
	{
		private readonly Critter itemHolder;
		private readonly IDictionary<uint, Item> critterContainerMap = new Dictionary<uint, Item> ();

		public ItemHolderData (Critter itemHolder)
		{
			this.itemHolder = itemHolder;
			Load ();
		}

		private string GetSerKey ()
		{
			return "ItemHolderData_" + itemHolder.Id;
		}

		private void Load ()
		{
			var serializator = new Serializator ();
			if (!serializator.Load (GetSerKey ()))
				return;

			uint size;
			serializator.Get (out size);
			for (uint i = 0; i < size; i++) {
				uint critterId;
				serializator.Get (out critterId);
				Item container;
				serializator.Get (out container);
				if (container == null)
					continue;
				critterContainerMap.Add (critterId, container);
			}
		}

		private void Save ()
		{
			var serializator = new Serializator ();

			serializator.Set ((uint)critterContainerMap.Count);
			foreach (var container in critterContainerMap) {
				serializator.Set (container.Key);
				serializator.Set (container.Value.Id);
			}

			serializator.Save (GetSerKey ());
		}

		private Item GetContainer (Critter critter, bool create)
		{
			var container = critterContainerMap [critter.Id];
			if (container == null && create) {
				container = itemHolder.AddItem (ItemProtoId.HiddenContainer, 1);
				if (container == null) {
					Global.Log ("Could not create item holder's container.");
				} else {
					container.IsHidden = true;
					critterContainerMap.Add (critter.Id, container);
					Save ();
				}
			}
			return container;
		}

		public void PutItems (Critter critter, IList<Item> items)
		{
			var container = GetContainer (critter, true);
			if (container == null) 
				return;

			var itemArray = new ItemArray ();
			itemArray.AddRange (items);

			Global.MoveItems (itemArray, container, 0);
		}

		public IList<Item> GetItems (Critter critter)
		{
			var container = GetContainer (critter, false);
			if (container == null) 
				return new List<Item> (0);

			var itemArray = new ItemArray ();
			container.GetItems (0, itemArray);
			return new List<Item> (itemArray);
		}
	}
}


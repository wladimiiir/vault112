using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class Blackboard
	{
		private EntityContainer<Critter> critterContainer = new EntityContainer<Critter> ();
		private EntityContainer<Item> itemContainer = new EntityContainer<Item> ();
		private EntityContainer<Location> locationContainer = new EntityContainer<Location> ();
		private Dictionary<string, uint> uids = new Dictionary<string, uint> ();
		//private readonly object Lock = new object ();

		public Blackboard ()
		{
		}

		public uint GenerateUID (string key)
		{
			//locking not needed for now as we have only one-thread
			//lock (Lock) {
				if (uids.ContainsKey (key)) {
					uids[key] = uids [key] + 1;
				} else {
					uids.Add (key, 1);
				}
				return uids [key];
			//}
		}

		public void AddCritters (string key, params Critter[] critters)
		{
			critterContainer.AddEntities (key, critters);
		}
		
		public void RemoveCritters (string key, params Critter[] critters)
		{
			critterContainer.RemoveEntities (key, critters);
		}
		
		public void SetCritters (string key, IList<Critter> critters)
		{
			critterContainer.SetEntities (key, critters);
		}
		
		public void ClearCritters (string key)
		{
			critterContainer.Clear (key);
		}

		public bool HasCritter (string key)
		{
			return critterContainer.HasEntity(key);
		}

		public IList<Critter> GetCritters (params string[] keys)
		{
			IList<Critter> critters = new List<Critter>();
			foreach (var key in keys) {
				foreach (var critter in critterContainer.GetEntities(key)) {
					critters.Add(critter);
				}
			}
			return critters;
		}

		public void AddItems (string key, params Item[] items)
		{
			itemContainer.AddEntities (key, items);
		}
		
		public void RemoveItems (string key, params Item[] items)
		{
			itemContainer.RemoveEntities (key, items);
		}
		
		public void SetItems (string key, IList<Item> items)
		{
			itemContainer.SetEntities (key, items);
		}
		
		public void ClearItems (string key)
		{
			itemContainer.Clear (key);
		}

		public void AddLocations (string key, params Location[] locations)
		{
			locationContainer.AddEntities (key, locations);
		}
		
		public void RemoveCLocations (string key, params Location[] locations)
		{
			locationContainer.RemoveEntities (key, locations);
		}
		
		public void SetLocations (string key, IList<Location> locations)
		{
			locationContainer.SetEntities (key, locations);
		}
		
		public void ClearLocations (string key)
		{
			locationContainer.Clear (key);
		}
	}
}


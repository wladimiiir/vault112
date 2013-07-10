using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class Blackboard
	{
		private EntityContainer<TimedEntity<Critter>> critterContainer = new EntityContainer<TimedEntity<Critter>> ();
		private EntityContainer<TimedEntity<Item>> itemContainer = new EntityContainer<TimedEntity<Item>> ();
		private EntityContainer<TimedEntity<Location>> locationContainer = new EntityContainer<TimedEntity<Location>> ();
		private Dictionary<string, uint> uids = new Dictionary<string, uint> ();
		//private readonly object Lock = new object ();

		protected long executionStartTime = 0;

		public void MainTaskExecutionStart ()
		{
			executionStartTime = DateTime.Now.Ticks;
		}

		public uint GenerateUID (string key)
		{
			//locking not needed for now as we have only one-thread
			//lock (Lock) {
			if (uids.ContainsKey (key)) {
				uids [key] = uids [key] + 1;
			} else {
				uids.Add (key, 1);
			}
			return uids [key];
			//}
		}

		public void ClearEntities ()
		{
			ClearContainers();
		}

		protected virtual void ClearContainers ()
		{
			ClearContainer (critterContainer);
			ClearContainer (itemContainer);
			ClearContainer (locationContainer);
		}

		protected void ClearContainer<T> (EntityContainer<TimedEntity<T>> container)
		{
			foreach (var key in container.GetKeys()) {
				var toRemove = new List<TimedEntity<T>> ();
				foreach (var entity in container.GetEntities(key)) {
					if (entity.IsInTime (executionStartTime))
						toRemove.Add (entity);
				}
				if (toRemove.Count > 0)
					container.RemoveEntities (key, toRemove.ToArray ());
			}
		}

		protected TimedEntity<T>[] CreateTimedEntities<T> (T[] entities, long timeAdded)
		{
			var timedEntities = new TimedEntity<T>[entities.Length];
			for (int index = 0; index < entities.Length; index++) {
				timedEntities [index] = new TimedEntity<T> (entities [index], timeAdded);
			}
			return timedEntities;
		}

		protected IList<T> GetEntities<T> (EntityContainer<TimedEntity<T>> container, params string[] keys)
		{
			IList<T> entities = new List<T> ();
			foreach (var key in keys) {
				foreach (var entity in container.GetEntities(key)) {
					if (entity.IsInTime (executionStartTime))
						entities.Add (entity.Entity);
				}
			}
			return entities;
		}

		public void AddCritters (string key, params Critter[] critters)
		{
			critterContainer.AddEntities (key, CreateTimedEntities (critters, 0));
		}

		protected void AddCrittersFromEvent (string key, params Critter[] critters)
		{
			critterContainer.AddEntities (key, CreateTimedEntities (critters, DateTime.Now.Ticks));
		}

		public void SetCritters (string key, List<Critter> critters)
		{
			critterContainer.SetEntities(key, CreateTimedEntities(critters.ToArray(), 0));
		}

		public IList<Critter> GetCritters (params string[] keys)
		{
			return GetEntities (critterContainer, keys);
		}

		public void AddItems (string key, params Item[] items)
		{
			itemContainer.AddEntities (key, CreateTimedEntities (items, 0));
		}

		protected void AddItemsFromEvent (string key, params Item[] items)
		{
			itemContainer.AddEntities (key, CreateTimedEntities (items, DateTime.Now.Ticks));
		}

		public IList<Item> GetItems (params string[] keys)
		{
			return GetEntities (itemContainer, keys);
		}
		
		public void AddLocations (string key, params Location[] locations)
		{
			locationContainer.AddEntities (key, CreateTimedEntities (locations, 0));
		}

		protected void AddLocationsFromEvent (string key, params Location[] locations)
		{
			locationContainer.AddEntities (key, CreateTimedEntities (locations, DateTime.Now.Ticks));
		}

		public IList<Location> GetLocations (params string[] keys)
		{
			return GetEntities (locationContainer, keys);
		}
		
		protected class TimedEntity<T>
		{
			private T entity;
			private long time;

			public TimedEntity (T entity, long time)
			{
				this.entity = entity;
				this.time = time;
			}
			
			public bool IsInTime (long time)
			{
				return time >= this.time;
			}

			public T Entity {
				get {
					return this.entity;
				}
			}
		}
	}
}


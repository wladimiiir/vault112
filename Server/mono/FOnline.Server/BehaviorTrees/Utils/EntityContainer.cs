using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class EntityContainer<T>
	{
		private Dictionary<string, IList<T>> entityMap = new Dictionary<string, IList<T>> ();

		private IList<T> GetEntityList (string key)
		{
			IList<T> entityList;
			if (!entityMap.ContainsKey (key)) {
				entityList = new List<T> ();
				entityMap.Add (key, entityList);
			}
			else {
				entityList = entityMap [key];
			}
			return entityList;
		}

		public void AddEntities (string key, params T[] entities)
		{
			var entityList = GetEntityList (key);
			foreach (var entity in entities) {
				entityList.Add(entity);
			}
		}

		public void RemoveEntities(string key, params T[] entities)
		{
			var entityList = GetEntityList (key);
			foreach (var entity in entities) {
				entityList.Remove(entity);
			}
		}

		public void SetEntities (string key, IList<T> entities)
		{
			entityMap.Add(key, entities);
		}

		public void Clear(string key)
		{
			GetEntityList(key).Clear();
		}

		public bool HasEntity(string key)
		{
			return GetEntityList(key).Count > 0;
		}

		public IList<T> GetEntities(string key)
		{
			return new List<T>(GetEntityList(key));
		}
	}
}


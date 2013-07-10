using System;
using System.Collections.Generic;

namespace FOnline.Data
{
	public class OffenceData
	{
		private string offenceDataKey;
		private IList<uint> offenceAreas = new List<uint> ();
		private IList<ulong> offenceTimes = new List<ulong> ();

		public OffenceData (Critter critter)
		{
			offenceDataKey = "OffenceData_" + critter.Id;
			Load ();
		}

		private void Load ()
		{
			Serializator serializator = new Serializator ();
			if (!serializator.Load (offenceDataKey))
				return;

			offenceAreas = new List<uint> ();
			offenceTimes = new List<ulong> ();
			uint count = 0;
			serializator.Get (out count);
			for (int index = 0; index < count; index++) {
				uint area;
				serializator.Get (out area);
				offenceAreas.Add (area);
				ulong time;
				serializator.Get (out time);
				offenceTimes.Add (time);
			}
		}

		private void Save ()
		{
			Serializator serializator = new Serializator ();

			serializator.Set (offenceAreas.Count);
			for (int index = 0; index < offenceAreas.Count; index++) {
				serializator.Set (offenceAreas [index]);
				serializator.Set (offenceTimes [index]);
			}

			serializator.Save (offenceDataKey);
		}

		private int GetOffenceIndex (uint offenceArea)
		{
			for (int index = 0; index < offenceAreas.Count; index++) {
				if (offenceAreas [index] == offenceArea)
					return index;
			}
			offenceAreas.Add(offenceArea);
			offenceTimes.Add(0);
			return offenceAreas.Count - 1;
		}

		public ulong GetOffenceDuration (uint offenceArea)
		{
			int index = GetOffenceIndex (offenceArea);
			ulong offenceTime = offenceTimes[index];

			return offenceTime < Global.FullSecond ? 0 : offenceTime - Global.FullSecond;
		}

		public void AddOffence (uint offenceArea, uint offenceTime)
		{
			int index = GetOffenceIndex (offenceArea);
			if(offenceTimes[index] < Global.FullSecond)
				offenceTimes[index] = offenceTime;
			else
				offenceTimes[index] += offenceTime;
			Save ();
		}
	}
}


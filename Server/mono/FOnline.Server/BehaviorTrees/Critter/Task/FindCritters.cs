using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public class FindCritters : CritterTask
	{
		private CritterChooser<CritterBlackboard> chooser;
		private Find find;
		private string key;

		public FindCritters (Find find, string crittersFoundKey = BlackboardKeys.FoundCritters)
		{
			this.find = find;
			this.key = crittersFoundKey;
		}

		public FindCritters (string crittersFoundKey = BlackboardKeys.FoundCritters) : this(Find.All, crittersFoundKey)
		{
		}

		public FindCritters Choose (CritterChooser<CritterBlackboard> chooser)
		{
			this.chooser = chooser;
			return this;
		}

		public override TaskState Execute ()
		{
			var map = blackboard.Critter.GetMap ();
			if (map == null)
				return TaskState.Failed;

			var critters = new CritterArray ();
			map.GetCritters (0, find, critters);

			List<Critter> found = new List<Critter> ();

			foreach (var critter in critters) {
				if (critter == GetCritter () || !Check (critter))
					continue;
				found.Add (critter);			
			}
			if (found.Count == 0)
				return TaskState.Failed;

			if (chooser != null) {
				var critter = chooser.Choose (found);
				found.Clear ();
				found.Add (critter);
			}

			GetBlackboard ().SetCritters (key, found);

			return TaskState.Success;
		}
	}
}


using System;
using System.Collections.Generic;

namespace FOnline.CritterActions
{
	public interface ICritterActionManager
	{
		void Start (Critter critter, ICritterAction action);
	}

	public class CritterActionManager : ICritterActionManager
	{
		public CritterActionManager ()
		{
		}

		public void Start (Critter critter, ICritterAction action)
		{
			critter.Idle += (sender, e) => {
				//start on idle if action is not running
				if(!action.IsRunning())
					action.Start(critter);

				ICollection<ICritterAction> actions = action.GetListeningActions ();
				foreach (var listeningAction in actions) {
					listeningAction.Idle (critter);
				}
			};
			critter.Finish += (sender, e) => {

			};
			critter.Dead += (_, e) => {
				ICollection<ICritterAction> actions = action.GetListeningActions ();
				foreach (var listeningAction in actions) {
					listeningAction.Dead (critter, e.Killer);
				}
			};
			critter.Respawn += (sender, e) => {

			};
			critter.ShowCritter += (sender, e) => {

			};
			critter.HideCritter += (sender, e) => {

			};
			critter.ShowItemOnMap += (sender, e) => {

			};
			critter.ChangeItemOnMap += (sender, e) => {

			};
			critter.Attack += (sender, e) => {

			};
			critter.Stealing += (sender, e) => {

			};
			critter.Message += (sender, e) => {

			};
		}
	}
}


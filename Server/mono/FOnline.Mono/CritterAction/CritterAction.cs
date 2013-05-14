using System;
using System.Collections.Generic;

namespace FOnline
{
	public interface ICritterAction
	{
		IList<ICritterAction> GetListeningActions ();

		ICritterAction AddSubAction (ICritterAction action);

		bool IsRunning();

		bool Start (Critter critter);

		void Idle (Critter critter);

		void Dead (Critter critter, Critter killer);
	}

	public abstract class AbstractCritterAction : ICritterAction
	{
		protected IList<ICritterAction> SubActions = new List<ICritterAction> ();
		private bool Running = false;

		public ICritterAction AddSubAction (ICritterAction action)
		{
			SubActions.Add (action);
			return this;
		}

		public IList<ICritterAction> GetListeningActions ()
		{
			var listeningActions = new List<ICritterAction> ();
			if (IsListening ()) {
				listeningActions.Add (this);
			}
			if (IsRunning ()) {
				foreach (var action in SubActions) {
					listeningActions.AddRange (action.GetListeningActions ());
				}
			}
			return listeningActions;
		}

		public bool Start (Critter critter)
		{
			Running = true;
			if (!PerformAction (critter)) {
				Running = false;
				return false;
			}
				
			var result = StartSubActions (critter);
			Running = false;
			return result;
		}

		protected abstract bool PerformAction (Critter critter);

		private bool StartSubActions (Critter critter)
		{
			foreach (var action in SubActions) {
				if (!action.Start (critter))
					return false;
			}
			return true;
		}

		protected abstract bool IsListening ();

		public virtual bool IsRunning ()
		{
			return Running;
		}

		public void Idle (Critter critter)
		{
		}

		public void Dead (Critter critter, Critter killer)
		{
		}
	}

	public abstract class ListeningAction : AbstractCritterAction
	{
		protected override bool PerformAction (Critter critter)
		{
			return false;
		}

		protected override bool IsListening ()
		{
			return true;
		}

		public override bool IsRunning ()
		{
			return false;
		}
	}
}


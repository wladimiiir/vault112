using System;
using System.Collections.Generic;

namespace FOnline.CritterActions
{
	public interface ICritterAction
	{
		IList<ICritterAction> GetListeningActions ();

		ICritterAction AddSubAction (ICritterAction action);

		void WithChance (float chance);

		void If (ICondition condition);

		void IfNot (ICondition condition);

		bool IsRunning ();

		bool Start (Critter critter);

		void Idle (Critter critter);

		void Dead (Critter critter, Critter killer);
	}

	public interface ICondition
	{
		bool Check (Critter critter, Critter someCritter, Item someItem);
	}

	public abstract class AbstractCritterAction : ICritterAction
	{
		private AbstractCritterAction ParentAction;
		protected IList<ICritterAction> SubActions = new List<ICritterAction> ();
		protected bool Listening = false;
		private bool Running = false;
		private float Chance = 100;
		private IList<ICondition> PositiveConditions = new List<ICondition> ();
		private IList<ICondition> NegativeConditions = new List<ICondition> ();

		public ICritterAction AddSubAction (ICritterAction action)
		{
			if (action is AbstractCritterAction)
				(action as AbstractCritterAction).ParentAction = this;
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
			if (!Global.HasChance (Chance) || !CheckConditions (critter, null, null))
				return false;

			Running = true;
			if (!PerformAction (critter)) {
				Running = false;
				return false;
			}
				
			var result = StartSubActions (critter);
			Running = false;
			return result;
		}

		protected bool CheckConditions (Critter critter, Critter someCritter, Item someItem)
		{
			foreach (var condition in PositiveConditions) {
				if (!condition.Check (critter, someCritter, someItem))
					return false;
			}
			foreach (var condition in NegativeConditions) {
				if (condition.Check (critter, someCritter, someItem))
					return false;
			}
			return true;
		}

		protected abstract bool PerformAction (Critter critter);

		protected virtual bool StartSubActions (Critter critter)
		{
			return StartSubActions (critter, null);
		}

		protected virtual bool StartSubActions (Critter critter, ICritterAction afterAction)
		{
			bool running = afterAction == null;
			foreach (var action in SubActions) {
				if (!running && this == action)
					running = true;
				else if (running)
				if (!action.Start (critter))
					return false;
			}
			return true;
		}

		protected virtual bool StartNextActions (Critter critter)
		{
			return ParentAction == null ? false : ParentAction.StartSubActions (critter, this);
		}

		protected virtual bool IsListening ()
		{
			return Listening;
		}

		public virtual bool IsRunning ()
		{
			return Running;
		}

		public void WithChance (float chance)
		{
			Chance = chance;
		}

		public void If (ICondition condition)
		{
			PositiveConditions.Add (condition);
		}

		public void IfNot (ICondition condition)
		{
			NegativeConditions.Add (condition);
		}

		public virtual void Idle (Critter critter)
		{
		}

		public virtual void Dead (Critter critter, Critter killer)
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

	public class Repeat : AbstractCritterAction
	{
		private int repeatTimes;
		private int currentRepeatTime;

		public Repeat (int repeatTimes = -1)
		{
			this.repeatTimes = repeatTimes;
		}

		protected override bool PerformAction (Critter critter)
		{
			currentRepeatTime = repeatTimes;
			return true;
		}

		protected override bool StartSubActions (Critter critter, ICritterAction afterAction)
		{
			if (SubActions.Count == 0)
				return true;
			if (repeatTimes == -1) {
				if (afterAction == SubActions [SubActions.Count - 1])
					Listening = true;

				bool result = base.StartSubActions (critter, afterAction);
				if (result) 
					Listening = true;

				return result;
			} else {
				if (afterAction == SubActions [SubActions.Count - 1])
					currentRepeatTime--;
				while (currentRepeatTime > 0) {
					if (!base.StartSubActions (critter, afterAction))
						return false;

					currentRepeatTime--;
				}
				return true;
			}
		}

		public override void Idle (Critter critter)
		{
			Listening = false;
			Start (critter);
		}
	}

	public class RepeatEvery : AbstractCritterAction
	{
		private int intervalFrom;
		private int intervalTo;
		private int nextStart = 0;

		public RepeatEvery (int interval)
		{
			this.intervalFrom = interval;
			this.intervalTo = interval;
		}

		public RepeatEvery (int intervalFrom, int intervalTo)
		{
			this.intervalFrom = intervalFrom;
			this.intervalTo = intervalTo;
		}
		

		protected override bool PerformAction (Critter critter)
		{
			Listening = true;
			return true;
		}

		public override void Idle (Critter critter)
		{
		}
	}

	public class ChooseRandom : AbstractCritterAction
	{
		protected override bool PerformAction (Critter critter)
		{
			return true;
		}

		protected override bool StartSubActions (Critter critter)
		{
			if (SubActions.Count == 0)
				return false;

			return SubActions [Global.Random (0, SubActions.Count - 1)].Start (critter);
		}
	}

	public class OnlyOneOf : AbstractCritterAction
	{
		protected override bool PerformAction (Critter critter)
		{
			return true;
		}
		
		protected override bool StartSubActions (Critter critter)
		{
			foreach (var action in SubActions) {
				if (action.Start (critter))
					return true;
			}
			return SubActions.Count == 0;
		}
	}
}


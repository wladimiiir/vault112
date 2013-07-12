using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FOnline.BT
{
	public static class BlackboardKeys
	{
		public const string FoundCritters = "FoundCritters";
		public const string Attackers = "Attackers";
		public const string Killers = "Killers";
		public const string SeenAttackers = "SeenAttackers";
		public const string SeenKillers = "SeenKillers";
		public const string SeenDead = "SeenDead";
		public const string ToAttack = "ToAttack";
		public const string FoundInEnemyStack = "FoundInEnemyStack";
	}

	public class CritterBlackboard : Blackboard
	{
		private readonly Critter critter;
		private readonly IList<TimedEntity<CritterMessage>> critterMessages = new List<TimedEntity<CritterMessage>> ();

		public CritterBlackboard (Critter critter)
		{
			this.critter = critter;
			InitEvents (critter);
		}

		protected override void ClearContainers ()
		{
			ResetMessages ();
			base.ClearContainers ();
		}

		private void ResetMessages ()
		{
			for (int index = critterMessages.Count - 1; index >= 0; index--) {
				if (critterMessages [index].IsInTime (executionStartTime))
					critterMessages.RemoveAt (index);
			}
		}

		private void InitEvents (FOnline.Critter critter)
		{
			critter.Attacked += (sender, e) => {
				AddCrittersFromEvent (BlackboardKeys.Attackers, e.Attacker);
			};
			critter.Dead += (sender, e) => {
				if (e.Killer != null)
					AddCrittersFromEvent (BlackboardKeys.Killers, e.Killer);
			};
			critter.Message += (sender, e) => {
				AddMessages (new CritterMessage (e.From, e.Num, e.Val));
			};
			critter.PlaneBegin += (sender, e) => {
				if (e.Reason == NpcPlaneReason.FoundInEnemyStack) {
					//let BT use its own system of attack response
					Debug.Assert (e.SomeCr != null, "Some critter in FoundInEnemyStack plane cannot be null");
					AddCrittersFromEvent (BlackboardKeys.FoundInEnemyStack, e.SomeCr);
					e.Result = NpcPlaneEventResult.Discard;
				}
			};
			critter.SmthDead += (sender, e) => {
				AddCrittersFromEvent (BlackboardKeys.SeenDead, e.From);
				if (e.Killer != null)
					AddCrittersFromEvent (BlackboardKeys.SeenKillers, e.Killer);
			};
		}

		public void AddMessages (params CritterMessage[] messages)
		{
			foreach (var message in CreateTimedEntities(messages, DateTime.Now.Ticks)) {
				critterMessages.Add (message);
			}
		}

		public IList<CritterMessage> GetMessages ()
		{
			IList<CritterMessage> messages = new List<CritterMessage> ();
			foreach (var message in critterMessages) {
				if (message.IsInTime (executionStartTime))
					messages.Add (message.Entity);
			}
			return messages;
		}

		public Critter Critter {
			get {
				return this.critter;
			}
		}
	}

	public class CritterMessage
	{
		private Critter fromCritter;
		private int messageNum;
		private int value;

		public CritterMessage (Critter fromCritter, int messageNum, int value)
		{
			this.fromCritter = fromCritter;
			this.messageNum = messageNum;
			this.value = value;
		}

		public Critter FromCritter {
			get {
				return this.fromCritter;
			}
		}

		public int MessageNum {
			get {
				return this.messageNum;
			}
		}

		public int Value {
			get {
				return this.value;
			}
		}
	}
}


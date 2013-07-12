using System;

namespace FOnline.BT
{
	public class CallReinforcements : CritterTask
	{
		private const int UseTeamId = 0;
		private string[] critterKeys;
		private int reinforcementTeam;

		public CallReinforcements (params string[] critterKeys) : this(UseTeamId, critterKeys)
		{
		}

		public CallReinforcements (int reinforcementTeam, params string[] critterKeys)
		{
			this.reinforcementTeam = reinforcementTeam;
			this.critterKeys = critterKeys;
		}

		public override TaskState Execute ()
		{
			int team = reinforcementTeam == UseTeamId ? GetCritter ().Stat [Stats.TeamId] : reinforcementTeam;

			foreach (var critterToAttack in GetBlackboard().GetCritters(critterKeys)) {
				if (!Check (critterToAttack))
					continue;

				GetCritter ().SendMessage (team, (int)critterToAttack.Id, MessageTo.AllOnMap);
			}

			return TaskState.Success;
		}
	}
}


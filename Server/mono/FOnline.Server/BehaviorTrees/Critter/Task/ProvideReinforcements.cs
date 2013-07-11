using System;

namespace FOnline.BT
{
	public class ProvideReinforcements : CritterTask
	{
		private const int UseTeamId = 0;
		private int reinforcementTeam;

		public ProvideReinforcements (int reinforcementTeam = UseTeamId)
		{
			this.reinforcementTeam = reinforcementTeam;
		}

		public override TaskState Execute ()
		{
			int team = reinforcementTeam == UseTeamId ? GetCritter ().Stat [Stats.TeamId] : reinforcementTeam;
			
			bool messageReceived = false;
			foreach (var message in GetBlackboard().GetMessages()) {
				if (message.MessageNum != team)
					continue;
				if (GetCritter().Id == message.FromCritter.Id || !Check (message.FromCritter))
					continue;
				Critter toAttack = Global.GetCritter ((uint)message.Value);
				if (toAttack == null)
					continue;

				NpcPlanes.AddAttackPlane (GetCritter (), Priorities.Attack, toAttack, true);
				messageReceived = true;
			}
			
			return messageReceived ? TaskState.Success : TaskState.Failed;
		}
	}
}


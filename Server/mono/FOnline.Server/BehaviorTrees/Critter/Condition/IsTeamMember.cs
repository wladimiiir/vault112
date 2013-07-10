using System;

namespace FOnline.BT
{
	public class IsTeamMember : CritterCheckCondition<CritterBlackboard>
	{
		public override bool Check (Critter checkEntity)
		{
			return GetBlackboard().Critter.Stat[Stats.TeamId] == checkEntity.Stat[Stats.TeamId];
		}
	}
}


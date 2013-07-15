using System;
using FOnline.AngelScript;

namespace FOnline.Maps
{
	public class Junkyard
	{
		public static Critter InitGuard(IntPtr ptr, bool firstTime)
		{
			var critter = (Critter)ptr;
			if (firstTime) {
				critter.Stat[Stats.AiId] = AIs.GeneralRangedThoughGuys;
				critter.Stat[Stats.TeamId] = Teams.Junkyard;
				critter.Stat[Stats.ReplicationTime] = (int)Time.RealMinute(5);
				critter.Stat[Stats.CriticalChanceExt] = 30;
				critter.Stat[Stats.BagId] = Bags.JunkyardGuard;
				critter.Mode[Modes.UnlimitedAmmo] = 1;
				critter.SetBagRefreshTime(5);
			}

			JunkyardRoles.InitGuard(critter);
			return critter;
		}

		public static Critter InitGyro(IntPtr ptr, bool firstTime)
		{
			var critter = (Critter)ptr;
			if (firstTime) {
				critter.Stat[Stats.AiId] = AIs.GeneralRangedThoughGuys;
				critter.Stat[Stats.TeamId] = Teams.Junkyard;
				critter.Stat[Stats.DialogId] = Dialogs.Gyro;
			}
			InitTrader(critter);
			JunkyardRoles.InitGyro(critter);
			return critter;
		}
		
		private static void InitTrader(Critter critter)
		{
			dynamic trader = ScriptEngine.GetModule("trader");
			trader.InitTrader(critter);
		}
	}
}


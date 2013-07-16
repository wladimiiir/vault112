using System;

namespace FOnline
{
	public class Visibility
	{
		private static readonly Linetracer linetracer = new Linetracer(new WallTraceContext());
		
		public static bool CheckLook(IntPtr onMapPtr, IntPtr critterPtr, IntPtr opponentPtr)
		{
			var onMap = (Map)onMapPtr;
			var critter = (Critter)critterPtr;
			var opponent = (Critter)opponentPtr;
			
			var visibility = critter.Param[Modes.VisibilityFlags];
			var opponentVisibility = opponent.Param[Modes.VisibilityFlags];
			if ((visibility & VisibilityFlag.SeeAll) != 0)
				return true;
			if (critter.IsDead || (visibility & VisibilityFlag.SeeNothing) != 0)
				return false;
			if ((opponentVisibility & VisibilityFlag.Invisible) != 0)
				return false;
			
			int visibleDistance = 0;
			if (critter.IsKnockout)
				visibleDistance = 3;
			else if (critter.Damage[Damages.Eye] != 0)
				visibleDistance = 3;
			else {
				uint multiplier = 5;
				//if (Time.IsNight (Global.Time.Hour) && critter.Trait [Traits.NIGHT_PERSON] == 0 || !Time.IsNight (Global.Time.Hour) && critter.Trait [Traits.NIGHT_PERSON] != 0)
				//	multiplier = 3;
				visibleDistance = (int)((2 + critter.Stat[Stats.Perception]) * multiplier + critter.Stat[Stats.BonusLook]);
				
				//here we have to use basic (vanilla) formula for visibility and create difference for correct visibility lines (Q) on client
				int basicFormula = 20 + critter.Stat[Stats.Perception] * 3 + critter.Stat[Stats.BonusLook];
				critter.Stat[Stats.BonusLook] = visibleDistance - basicFormula;
			}
			
			var distance = Global.GetDistantion(critter.HexX, critter.HexY, opponent.HexX, opponent.HexY);
			var direction = Global.GetDirection(critter.HexX, critter.HexY, opponent.HexX, opponent.HexY);
			
			visibleDistance -= GetLookDirNerf(visibleDistance, System.Math.Abs(((int)critter.Dir - (int)direction) % 6));
			
			if (visibleDistance < (int)distance)
				return false;
			
			if ((visibility & VisibilityFlag.IgnoreWalls) == 0
				&& linetracer.TraceDistance(onMap, critter.HexX, critter.HexY, opponent.HexX, opponent.HexY, distance) < distance)
				return false;
			
			//sneak
			if (opponent.Mode[Modes.Hide] != 0 && distance > 3 && (visibility & VisibilityFlag.IgnoreSneak) == 0) {
				var sneak = opponent.Skill[Skills.Sneak];
				sneak -= GetSneakSkillNerf(sneak, System.Math.Abs(((int)critter.Dir - (int)direction) % 6));
				visibleDistance -= sneak / 6;
				if (visibleDistance < (int)distance)
					return false;
			}
			
			return true;
		}
		
		private static int GetLookDirNerf(int distance, int lookDir)
		{
			uint nerf = 0;
			switch (lookDir) {
			case 0:
				nerf = Global.LookDir0;
				break;
			case 1:
			case 5:
				nerf = Global.LookDir1;
				break;
			case 2:
			case 4:
				nerf = Global.LookDir2;
				break;
			case 3:
				nerf = Global.LookDir3;
				break;
			}
			
			return (int)(distance * nerf / 100);
		}
		
		private static int GetSneakSkillNerf(int skill, int lookDir)
		{
			uint nerf = 0;
			switch (lookDir) {
			case 0:
				nerf = Global.LookSneakDir0;
				break;
			case 1:
			case 5:
				nerf = Global.LookSneakDir1;
				break;
			case 2:
			case 4:
				nerf = Global.LookSneakDir2;
				break;
			case 3:
				nerf = Global.LookSneakDir3;
				break;
			}
			
			return (int)(skill * nerf / 100);
		}
		
		private class WallTraceContext : ITraceContext
		{
			public bool Check(Map map, ushort hexX, ushort hexY)
			{
				if (map.IsHexRaked(hexX, hexY))
					return true;
				
				var sceneries = new SceneryArray();
				map.GetSceneries(hexX, hexY, sceneries);
				
				foreach (var scenery in sceneries) {
					ProtoItem proto = Global.GetProtoItem(scenery.ProtoId);
					if (proto == null)
						continue;
					if ((proto.Flags & ItemFlag.LightThru) != 0)
						return true;
				}
				
				//cannot see through
				return false;
			}
		}
	}
}

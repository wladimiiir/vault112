using System;
using System.Collections.Generic;

namespace FOnline.AI
{
	public class AIManager
	{
		public static void InitCrippler (IntPtr npcPtr)
		{
			var npc = (Critter)npcPtr;
			var crippler = new Crippler ();
		
			InitAIEvents (npc, crippler);
		}

		private static void InitAIEvents (Critter npc, ICombatClass combatClass)
		{
			Critter currentTarget = null;

			npc.Idle += (sender, e) => {
				currentTarget = combatClass.ChooseNextTarget (npc);
				if (currentTarget != null) {
					npc.AddAttackPlane (Priorities.Attack, currentTarget);
				}
			};
			npc.PlaneBegin += (sender, e) => {
				if (e.Reason == NpcPlaneReason.FoundInEnemyStack) {
					e.Result = NpcPlaneEventResult.Discard;
				}
			};
			npc.PlaneRun += (sender, e) => {
				switch (e.Reason) {
				case NpcPlaneReason.AttackTarget:
					currentTarget = combatClass.ChooseNextTarget (npc);
					e.Result = NpcPlaneEventResult.Keep;
					break;
				case NpcPlaneReason.AttackDistantion:
					var position = combatClass.GetNextPosition (npc, currentTarget);
					if (position != null) {
						var plane = Global.CreatePlane ();
						plane.Type = PlaneType.Walk;
						plane.Priority = 0;
						plane.Run = true;
						plane.Walk_HexX = position [0];
						plane.Walk_HexY = position [1];
						e.Plane.SetChild (plane);
						e.Result = NpcPlaneEventResult.Keep;
					} else {
						var dist = Global.GetDistantion (npc.HexX, npc.HexY, currentTarget.HexX, currentTarget.HexY);
						e.Param0 = dist;
						e.Param1 = dist;
						e.Param2 = dist;
						e.Result = NpcPlaneEventResult.Keep;
					}
					break;
				case NpcPlaneReason.AttackWeapon:
					e.Param0 = combatClass.ChooseWeaponId (npc, currentTarget);
					if (e.Param1 != 0)
						e.Param1 = combatClass.ChooseWeaponUse (npc, currentTarget);
					if (e.Param0 == 0)
						e.Param2 = combatClass.ChooseUnarmedProtoId (npc, currentTarget);
					e.Result = NpcPlaneEventResult.Keep;
					break;
				case NpcPlaneReason.AttackUseAim:
					e.Param0 = combatClass.ChooseWeaponUse (npc, currentTarget);
					e.Param1 = combatClass.ChooseHitLocation (npc, currentTarget);
					e.Result = NpcPlaneEventResult.Keep;
					break;
				}
			};
			npc.PlaneEnd += (sender, e) => {
				Global.Log ("Plane ended with reason: " + e.Reason);
			};
		}
	}
}


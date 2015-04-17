using System;
using System.Collections.Generic;
using Mono.CSharp;
using FOnline.AngelScript;

namespace FOnline.AI
{
	public class AIManager
	{
		private const long PROCESS_INTERVAL = 200;

		private Linetracer shootTracer = new Linetracer (new ShootTraceContext ());
		private System.Diagnostics.Stopwatch Time = System.Diagnostics.Stopwatch.StartNew ();

		public static void InitRanged (IntPtr npcPtr)
		{
			var npc = (Critter)npcPtr;
			Global.AIManager.InitRanged (npc);
		}

		public void InitRanged (Critter npc)
		{
			InitAIEvents (npc, new Ranged ());
		}

		private void InitAIEvents (Critter npc, ICombatClass combatClass)
		{
			Critter currentTarget = null;
			long NextProcessTime = 0;

			npc.Attacked += (sender, e) => {
				if (e.Attacker != null)
					npc.AddEnemyInStack (e.Attacker.Id);
			};
			npc.PlaneBegin += (sender, e) => {
				if (e.Plane.Type == PlaneType.CustomAI) {
					e.Result = NpcPlaneEventResult.Keep;
				} else {
					//TODO: check if discard
					Global.Log ("Plane begin: " + e.Plane.Type);
				}
			};
			npc.PlaneRun += (sender, e) => {
				e.Result = NpcPlaneEventResult.Discard;
			};
			npc.PlaneEnd += (sender, e) => {
				if (e.Plane.Type == PlaneType.CustomAI) {
					if (NextProcessTime <= Time.ElapsedMilliseconds) {
						var breakTime = ProcessCustomAI (npc, ref currentTarget, combatClass, e.Plane);
						NextProcessTime = Time.ElapsedMilliseconds + (breakTime == 0 ? PROCESS_INTERVAL : breakTime);
					}

					e.Result = NpcPlaneEventResult.Keep;
				} else if (e.Plane.Type == PlaneType.Walk) {
					e.Result = ProcessWalkResult (npc, e);
				} else {
					e.Result = NpcPlaneEventResult.Discard;
				}
			};

			npc.DropPlanes ();
			npc.AddPlane (CreateCustomAIPlane ());
		}

		private long ProcessCustomAI (Critter npc, ref Critter currentTarget, ICombatClass combatClass, NpcPlane plane)
		{
			currentTarget = combatClass.ChooseNextTarget (npc, currentTarget);
			if (currentTarget == null)
				return 0;

			var attackChoice = combatClass.ChooseAttack (npc, currentTarget);
			if (attackChoice != null) {
				var position = combatClass.ChooseAttackPosition (npc, currentTarget, attackChoice);
				if (position != null) {
					GoToPosition (npc, plane, position [0], position [1], position [2]);
					return 0;
				}

				return AttackTarget (npc, currentTarget, attackChoice);
			}

			var itemChoice = combatClass.ChooseItem (npc, currentTarget);
			if (itemChoice != null) {
				UseItemOnTarget (npc, currentTarget, itemChoice);
				return 0;
			}

			var skillChoice = combatClass.ChooseSkill (npc, currentTarget);
			if (skillChoice != null) {
				UseSkillOnTarget (npc, currentTarget, skillChoice);
				return 0;
			}

			return 0;
		}

		private long AttackTarget (Critter npc, Critter target, AttackChoice attackChoice)
		{
			if (npc.Stat [Stats.CurrentAP] <= 0) {
				npc.Wait (500);
				return 0;
			}

			var distance = Global.GetDistance (npc, target);
			if (distance > 1) {
				var tracedDistance = shootTracer.TraceDistance (npc.GetMap (), npc.HexX, npc.HexY, target.HexX, target.HexY, distance);
				if (tracedDistance != distance)
					//cannot shoot through objects
					return 0;
			}

			var currentWeapon = npc.GetItemHand ();
			if (currentWeapon.Id != attackChoice.WeaponId) {
				var map = npc.GetMap ();
				var moveItemApCost = map != null && map.IsTurnBased () ? Global.TbApCostDropItem : Global.RtApCostDropItem;

				npc.Stat [Stats.CurrentAP] -= (int)moveItemApCost * 100;
				npc.MoveItem (attackChoice.WeaponId, 1, ItemSlot.Hand1);
				currentWeapon = npc.GetItemById (attackChoice.WeaponId);
			}
			if (currentWeapon == null)
				return 0;

			if (distance > currentWeapon.Proto.Weapon_MaxDist_0) {
				return 0;
			}

			var weaponAttackApCost = currentWeapon.Proto.Weapon_ApCost_0;
			npc.Stat [Stats.CurrentAP] -= (int)weaponAttackApCost * 100;

			dynamic mainModule = ScriptEngine.GetModule ("main");
			mainModule.critter_attack (npc, target, currentWeapon.Proto, attackChoice.WeaponUse, Global.GetProtoItem (currentWeapon.Proto.Weapon_DefaultAmmoPid));
			return Global.Breaktime;
		}

		private void UseItemOnTarget (Critter npc, Critter currentTarget, ItemChoice itemChoice)
		{

		}

		private void UseSkillOnTarget (Critter npc, Critter currentTarget, SkillChoice skillChoice)
		{

		}

		private void GoToPosition (Critter npc, NpcPlane plane, ushort hexX, ushort hexY, ushort dir)
		{
			var walkPlane = Global.CreatePlane ();
			walkPlane.Type = PlaneType.Walk;
			walkPlane.Run = true;
			walkPlane.Walk_Cut = 0;
			walkPlane.Walk_HexX = hexX;
			walkPlane.Walk_HexY = hexY;
			walkPlane.Walk_Dir = (Direction)dir;
			plane.SetChild (walkPlane);
		}

		private NpcPlaneEventResult ProcessWalkResult (Critter npc, CritterEventPlaneBeginEndArgs e)
		{
			Global.Log ("Processing walk result: " + e.Reason);
			switch (e.Reason) {
			case NpcPlaneReason.Success:
				return NpcPlaneEventResult.Discard;
			default:
				return NpcPlaneEventResult.Discard;
			}
		}

		private NpcPlane CreateCustomAIPlane ()
		{
			var plane = Global.CreatePlane ();
			plane.Type = PlaneType.CustomAI;
			plane.Priority = 0;
			return plane;
		}
	}
}


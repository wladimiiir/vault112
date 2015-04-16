using System;
using System.Collections.Generic;
using Mono.CSharp;
using FOnline.AngelScript;

namespace FOnline.AI
{
	public class AIManager
	{
		public static void InitRanged (IntPtr npcPtr)
		{
			var npc = (Critter)npcPtr;
			Global.AIManager.InitRanged (npc);
		}

		public void InitRanged (Critter npc)
		{
			Global.Log ("Initializing Ranged combat class");
			InitAIEvents (npc, new Ranged ());
		}

		private void InitAIEvents (Critter npc, ICombatClass combatClass)
		{
			Critter currentTarget = null;

			npc.Idle += (sender, e) => {
				Global.Log ("Idle raised");
			};
			npc.Attacked += (sender, e) => {
				Global.Log ("Attacked");
				if (e.Attacker != null)
					npc.AddEnemyInStack (e.Attacker.Id);
			};
			npc.PlaneBegin += (sender, e) => {
				if (e.Plane.Type != PlaneType.CustomAI) {
					//TODO: check if discard
					Global.Log ("Plane begin: " + e.Plane.Type);
				}
			};
			npc.PlaneRun += (sender, e) => {
				e.Result = NpcPlaneEventResult.Discard;
			};
			npc.PlaneEnd += (sender, e) => {
				Global.Log ("Plane end");
				if (e.Plane.Type == PlaneType.CustomAI) {
					ProcessCustomAI (npc, ref currentTarget, combatClass, e.Plane);
					e.Result = NpcPlaneEventResult.Keep;
				} else if (e.Plane.Type == PlaneType.Walk) {
					e.Result = ProcessWalkResult (npc, e);
				} else {
					e.Result = NpcPlaneEventResult.Discard;
				}
			};

			npc.DropPlanes ();
			npc.AddPlane (CreateCustomAIPlane ());
			Global.Log ("Successfully initialized");
		}

		private void ProcessCustomAI (Critter npc, ref Critter currentTarget, ICombatClass combatClass, NpcPlane plane)
		{
			currentTarget = combatClass.ChooseNextTarget (npc);
			if (currentTarget == null)
				return;

			var position = combatClass.ChoosePosition (npc, currentTarget);
			if (position != null) {
				GoToPosition (npc, plane, position [0], position [1], position [2]);
				return;
			}

			var attackChoice = combatClass.ChooseAttack (npc, currentTarget);
			if (attackChoice != null) {
				AttackTarget (npc, currentTarget, attackChoice);
				return;
			}

			var itemChoice = combatClass.ChooseItem (npc, currentTarget);
			if (itemChoice != null) {
				UseItemOnTarget (npc, currentTarget, itemChoice);
				return;
			}

			var skillChoice = combatClass.ChooseSkill (npc, currentTarget);
			if (skillChoice != null) {
				UseSkillOnTarget (npc, currentTarget, skillChoice);
				return;
			}
		}

		private void AttackTarget (Critter npc, Critter currentTarget, AttackChoice attackChoice)
		{
			var currentWeapon = npc.GetItemHand ();
			if (currentWeapon.Id != attackChoice.WeaponId) {
				var map = npc.GetMap ();
				var apCost = map != null && map.IsTurnBased () ? Global.TbApCostDropItem : Global.RtApCostDropItem;

				npc.Stat [Stats.CurrentAP] -= (int)apCost * 100;
				npc.MoveItem (attackChoice.WeaponId, 1, ItemSlot.Hand1);
				currentWeapon = npc.GetItemById (attackChoice.WeaponId);
			}

			dynamic mainModule = ScriptEngine.GetModule ("main");
			mainModule.critter_attack (npc, currentTarget, currentWeapon.Proto, attackChoice.WeaponUse, Global.GetProtoItem (currentWeapon.Proto.Weapon_DefaultAmmoPid));
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
			return plane;
		}
	}
}


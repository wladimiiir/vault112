// Original author: cvet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    /// <summary>
    /// Set of extension methods for Critter class helping to create AI plans.
    /// </summary>
    public static class NpcPlanes
    {
        public static bool AddMiscPlane(this Critter npc, uint priority, uint waitSecond, string funcName)
        {
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type = PlaneType.Misc;
	        plane.Priority = priority==0 ? Priorities.Misc : priority;
	        plane.Misc_WaitSecond = waitSecond;
	        if(funcName != null && !plane.Misc_SetScript(funcName))
	        {
		        Global.Log("Set script <{0}> fail", funcName);
		        return false;
	        }
	        return npc.AddPlane(plane);
        }

        public static bool AddWalkPlane(this Critter npc, uint priority, ushort hexX, ushort hexY, Direction dir, bool run, uint cut)
        {
	        if(!npc.IsCanWalk) return false;

	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Walk;
	        plane.Priority=(priority==0?Priorities.Walk:priority);
	        plane.Walk_HexX=hexX;
	        plane.Walk_HexY=hexY;
	        plane.Walk_Dir=dir;
	        plane.Run=run;
	        plane.Walk_Cut=cut;
	        return npc.AddPlane(plane);
        }

        public static bool AddWalkPlane(this Critter npc, uint priority, int identifier, uint identifierExt, ushort hexX, ushort hexY, Direction dir, bool run, uint cut)
        {
	        if(!npc.IsCanWalk) return false;

	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Walk;
	        plane.Priority=(priority==0?Priorities.Walk:priority);
	        plane.Identifier=identifier;
	        plane.IdentifierExt=identifierExt;
	        plane.Walk_HexX=hexX;
	        plane.Walk_HexY=hexY;
	        plane.Walk_Dir=dir;
	        plane.Run=run;
	        plane.Walk_Cut=cut;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, Critter target)
        {
	        if(npc.IsPlayer)
	        {
		        Map map=npc.GetMap();
		        uint loc=0;
		        if(map != null) loc=map.GetLocation().GetProtoId();
		        Global.Log("ERR: adding attack plane to player, loc pid={0}", loc);
	        }
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=Global.DeadHitPoints;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Run=false;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, uint critId)
        {
	        Critter target=Global.GetCritter(critId);
	        if(target == null)
	        {
		        Global.Log("Target not found.");
		        return false;
	        }

	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=Global.DeadHitPoints;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Run=false;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, Critter target, int minHp)
        {
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=minHp;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Run=false;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, uint critId, int minHp)
        {
	        Critter target=Global.GetCritter(critId);
	        if(target == null)
	        {
		        Global.Log("Target not found.");
		        return false;
	        }

	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=minHp;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Run=false;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, Critter target, bool run)
        {
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=Global.DeadHitPoints;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Run=run;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, uint critId, bool run)
        {
	        Critter target=Global.GetCritter(critId);
	        if(target == null)
	        {
		        Global.Log("Target not found.");
		        return false;
	        }

	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=Global.DeadHitPoints;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Run=run;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, Critter target, int minHp, bool run)
        {
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=minHp;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Run=run;
	        return npc.AddPlane(plane);
        }

        public static bool AddAttackPlane(this Critter npc, uint priority, uint critId, int minHp, bool run)
        {
	        Critter target=Global.GetCritter(critId);
	        if(target == null)
	        {
		        Global.Log("Target not found.");
		        return false;
	        }

	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Attack;
	        plane.Priority=(priority==0?Priorities.Attack:priority);
	        plane.Attack_TargId=target.Id;
	        plane.Attack_MinHp=minHp;
	        plane.Attack_IsGag=false;
	        plane.Attack_GagHexX=0;
	        plane.Attack_GagHexY=0;
	        plane.Attack_LastHexX=target.HexX;
	        plane.Attack_LastHexY=target.HexY;
	        plane.Run=run;
	        return npc.AddPlane(plane);
        }

        public static bool AddPickPlane(this Critter npc, uint priority, ushort hexX, ushort hexY, ushort protoId, uint useItemId, bool toOpen)
        {
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Pick;
	        plane.Priority=(priority==0?Priorities.Pick:priority);
	        plane.Pick_HexX=hexX;
	        plane.Pick_HexY=hexY;
	        plane.Pick_Pid=protoId;
	        plane.Pick_UseItemId=useItemId;
	        plane.Pick_ToOpen=toOpen;
	        plane.Run=false;
	        return npc.AddPlane(plane);
        }

        public static bool AddPickPlane(this Critter npc, uint priority, Item item, uint useItemId, bool toOpen)
        {
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Pick;
	        plane.Priority=(priority==0?Priorities.Pick:priority);
	        plane.Pick_HexX=item.HexX;
	        plane.Pick_HexY=item.HexY;
	        plane.Pick_Pid=item.GetProtoId();
	        plane.Pick_UseItemId=useItemId;
	        plane.Pick_ToOpen=toOpen;
	        plane.Run=false;
	        return npc.AddPlane(plane);
        }

        public static bool AddPickPlane(this Critter npc, uint priority, Item item, uint useItemId, bool toOpen, bool run)
        {
	        NpcPlane plane=Global.CreatePlane();
	        plane.Type=PlaneType.Pick;
	        plane.Priority=(priority==0?Priorities.Pick:priority);
	        plane.Pick_HexX=item.HexX;
	        plane.Pick_HexY=item.HexY;
	        plane.Pick_Pid=item.GetProtoId();
	        plane.Pick_UseItemId=useItemId;
	        plane.Pick_ToOpen=toOpen;
	        plane.Run=run;
	        return npc.AddPlane(plane);
        }

        public static uint EraseAttackPlane(this Critter npc, uint priority, Critter target)
        {
	        return EraseAttackPlane(npc, priority, target.Id);
        }

        public static uint EraseAttackPlane(this Critter npc, uint priority, uint critId)
        {
            var planes = new NpcPlaneArray();
	        uint count = npc.GetPlanes(planes);
	        if(count==0) return 0;
	        uint erased=0;

	        for(uint i=0; i<count; i++)
	        {
		        if(planes[i].Attack_TargId==critId && npc.ErasePlane(i-erased)) erased++;
	        }

	        return erased;
        }
    }
}

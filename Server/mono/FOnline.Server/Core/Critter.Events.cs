using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public class CritterEventArgs : EventArgs
    {
        public CritterEventArgs(Critter cr)
        {
            this.Cr = cr;
        }
        public Critter Cr { get; private set; }
    }
    public class CritterFinishEventArgs : CritterEventArgs
    {
        public CritterFinishEventArgs(Critter cr, bool deleted)
            : base(cr)
        {
            this.Deleted = deleted;
        }
        public bool Deleted { get; private set; }
    }
    public class CritterDeadEventArgs : CritterEventArgs
    {
        public CritterDeadEventArgs(Critter cr, Critter killer)
            : base(cr)
        {
            this.Killer = killer;
        }
        public Critter Killer { get; private set; }
    }
    public class CritterVisEventArgs : CritterEventArgs
    {
        public CritterVisEventArgs(Critter cr, Critter show_cr)
            : base(cr)
        {
            this.ShowCr = show_cr;
        }
        public Critter ShowCr { get; private set; }
        public Critter HideCr { get { return ShowCr; } }
    }
    public class CritterShowItemOnMapEventArgs : CritterEventArgs
    {
        public CritterShowItemOnMapEventArgs(Critter cr, Item item, bool added, Critter dropper)
            : base(cr)
        {
            this.Item = item;
            this.Added = added;
            this.Dropper = dropper;
        }
        public Item Item { get; private set; }
        public bool Added { get; private set; }
        public Critter Dropper { get; private set; }
    }
    public class CritterChangeItemOnMapEventArgs : CritterEventArgs
    {
        public CritterChangeItemOnMapEventArgs(Critter cr, Item item)
            : base(cr)
        {
            this.Item = item;
        }
        public Item Item { get; private set; }
    }
    public class CritterHideItemOnMapEventArgs : CritterEventArgs
    {
        public CritterHideItemOnMapEventArgs(Critter cr, Item item, bool removed, Critter picker)
            : base(cr)
        {
            this.Item = item;
            this.Removed = removed;
            this.Picker = picker;
        }
        public Item Item { get; private set; }
        public bool Removed { get; private set; }
        public Critter Picker { get; private set; }
    }
    /// <summary>
    /// Base class for event args that are supposed to return boolean
    /// value to the engine meaning whether we should interrupt further processing or not.
    /// </summary>
    public class CritterChainEventArgs : DefaultEventArgs
    {
        public CritterChainEventArgs(Critter cr)
        {
            this.Cr = cr;
        }
        public Critter Cr { get; private set; }
    }
    public class CritterAttackEventArgs : CritterChainEventArgs
    {
        public CritterAttackEventArgs(Critter cr, Critter target)
            : base(cr)
        {
            this.Target = target;
        }
        public Critter Target { get; private set; }
    }
    public class CritterAttackedEventArgs : CritterChainEventArgs
    {
        public CritterAttackedEventArgs(Critter cr, Critter attacker)
            : base(cr)
        {
            this.Attacker = attacker;
        }
        public Critter Attacker { get; private set; }
    }
    public class CritterStealingEventArgs : CritterEventArgs
    {
        public CritterStealingEventArgs(Critter cr, Critter thief, bool success, Item item, uint count)
            : base(cr)
        {
            this.Thief = thief;
            this.Success = success;
            this.Item = item;
            this.Count = count;
        }
        public Critter Thief { get; private set; }
        public bool Success { get; private set; }
        public Item Item { get; private set; }
        public uint Count { get; private set; }
    }
    public class CritterMessageEventArgs : CritterEventArgs
    {
        public CritterMessageEventArgs(Critter cr, Critter from_cr, int num, int val)
            : base(cr)
        {
            this.From = from_cr;
            this.Num = num;
            this.Val = val;
        }
        public Critter From { get; private set; }
        public int Num { get; private set; }
        public int Val { get; private set; }
    }
    public class CritterUseItemEventArgs : CritterChainEventArgs
    {
        public CritterUseItemEventArgs(Critter cr, Item item, Critter on_cr, Item on_item, Scenery on_scenery)
            : base(cr)
        {
            this.Item = item;
            this.OnCr = on_cr;
            this.OnItem = on_item;
            this.OnScenery = on_scenery;
        }
        public Item Item { get; private set; }
        public Critter OnCr { get; private set; }
        public Item OnItem { get; private set; }
        public Scenery OnScenery { get; private set; }
    }
    public class CritterUseItemOnMeEventArgs : CritterChainEventArgs
    {
        public CritterUseItemOnMeEventArgs(Critter cr, Critter who_use, Item item)
            : base(cr)
        {
            this.WhoUse = who_use;
            this.Item = item;
        }
        public Critter WhoUse { get; private set; }
        public Item Item { get; private set; }
    }
    public class CritterUseSkillEventArgs : CritterChainEventArgs
    {
        public CritterUseSkillEventArgs(Critter cr, int skill, Critter on_cr, Item on_item, Scenery on_scenery)
            : base(cr)
        {
            this.Skill = skill;
            this.OnCr = on_cr;
            this.OnItem = on_item;
            this.OnScenery = on_scenery;
        }
        public int Skill { get; private set; }
        public Critter OnCr { get; private set; }
        public Item OnItem { get; private set; }
        public Scenery OnScenery { get; private set; }
    }
    public class CritterUseSkillOnMeEventArgs : CritterChainEventArgs
    {
        public CritterUseSkillOnMeEventArgs(Critter cr, Critter who_use, int skill)
            : base(cr)
        {
            this.WhoUse = who_use;
            this.Skill = skill;
        }
        public Critter WhoUse { get; private set; }
        public int Skill { get; private set; }
    }
    public class CritterDropItemEventArgs : CritterEventArgs
    {
        public CritterDropItemEventArgs(Critter cr, Item item)
            : base(cr)
        {
            this.Item = Item;
        }
        public Item Item { get; private set; }
    }
    public class CritterMoveItemEventArgs : CritterEventArgs
    {
        public CritterMoveItemEventArgs(Critter cr, Item item, ItemSlot from_slot)
            : base(cr)
        {
            this.Item = item;
            this.FromSlot = from_slot;
        }
        public Item Item { get; private set; }
        public ItemSlot FromSlot { get; private set; }
    }
    public class CritterKnockoutEventArgs : CritterEventArgs
    {
        public CritterKnockoutEventArgs(Critter cr, uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist)
            : base(cr)
        {
            this.Anim2Begin = anim2_begin;
            this.Anim2Idle = anim2_idle;
            this.Anim2End = anim2_end;
            this.LostAp = lost_ap;
            this.KnockDist = knock_dist;
        }
        public uint Anim2Begin { get; private set; }
        public uint Anim2Idle { get; private set; }
        public uint Anim2End { get; private set; }
        public uint LostAp { get; private set; }
        public uint KnockDist { get; private set; }
    }
    public class CritterSmthKnockoutEventArgs : CritterKnockoutEventArgs
    {
        public CritterSmthKnockoutEventArgs(Critter cr, Critter from_cr, uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist)
            : base(cr, anim2_begin, anim2_idle, anim2_end, lost_ap, knock_dist)
        {
            this.From = from_cr;
        }
        public Critter From { get; private set; }
    }
    public class CritterSmthDeadEventArgs : CritterEventArgs
    {
        public CritterSmthDeadEventArgs(Critter cr, Critter from_cr, Critter killer)
            : base(cr)
        {
            this.From = from_cr;
            this.Killer = killer;
        }
        public Critter From { get; private set; }
        public Critter Killer { get; private set; }
    }
    public class CritterSmthStealingEventArgs : CritterStealingEventArgs
    {
        public CritterSmthStealingEventArgs(Critter cr, Critter from_cr, Critter thief, bool success, Item item, uint count)
            : base(cr, thief, success, item, count)
        {
            this.From = from_cr;
        }
        public Critter From { get; private set; }
    }
    public class CritterSmthUseSkillEventArgs : CritterUseSkillEventArgs
    {
        public CritterSmthUseSkillEventArgs(Critter cr, Critter from_cr, int skill, Critter on_cr, Item on_item, Scenery on_scenery)
            : base(cr, skill, on_cr, on_item, on_scenery)
        {
            this.From = from_cr;
        }
        public Critter From { get; private set; }
    }
    public class CritterSmthDropItemEventArgs : CritterDropItemEventArgs
    {
        public CritterSmthDropItemEventArgs(Critter cr, Critter from_cr, Item item)
            : base(cr, item)
        {
            this.From = from_cr;
        }
        public Critter From { get; private set; }
    }
    public class CritterSmthMoveItemEventArgs : CritterMoveItemEventArgs
    {
        public CritterSmthMoveItemEventArgs(Critter cr, Critter from_cr, Item item, ItemSlot from_slot)
            : base(cr, item, from_slot)
        {
            this.From = from_cr;
        }
        public Critter From { get; private set; }
    }
    public class CritterBarterEventArgs : CritterChainEventArgs
    {
        public CritterBarterEventArgs(Critter cr, Critter cr_barter, bool attach, uint barter_count)
            : base(cr)
        {
            this.CrBarter = cr_barter;
            this.Attach = attach;
            this.BarterCount = barter_count;
        }
        public Critter CrBarter { get; private set; }
        public bool Attach { get; private set; }
        public uint BarterCount { get; private set; }
    }
    public class CritterTalkEventArgs : CritterChainEventArgs
    {
        public CritterTalkEventArgs(Critter cr, Critter cr_talk, bool attach, uint talk_count)
            : base(cr)
        {
            this.CrTalk = cr_talk;
            this.Attach = attach;
            this.TalkCount = talk_count;
        }
        public Critter CrTalk { get; private set; }
        public bool Attach { get; private set; }
        public uint TalkCount { get; private set; }
    }
    public class CritterGlobalProcessEventArgs : CritterChainEventArgs
    {
        public CritterGlobalProcessEventArgs(Critter cr, GlobalProcessType type, Item car)
            : base(cr)
        {
            this.Type = type;
            this.Car = car;
        }
        public GlobalProcessType Type { get; private set; }
        public Item Car { get; private set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float ToX { get; set; }
        public float ToY { get; set; }
        public float Speed { get; set; }
        public int EncounterDescriptor { get; set; }
        public bool WaitForAnswer { get; set; }
    }
    /// <summary>
    /// Parameters for GlobalInvite event, the values are used only if Prevent is true 
    /// (that is, handler succeeded and no further processing is performed).
    /// </summary>
    /// <remarks>
    /// Every handler should check Prevent property and perform its actions
    /// only if it's equal to false. That ensures only one handler succeeds.
    /// </remarks>
    public class CritterGlobalInviteEventArgs : CritterChainEventArgs
    {
        public CritterGlobalInviteEventArgs(Critter cr, Item car, uint encounter_descriptor, int combat_mode)
            : base(cr)
        {
            this.Car = car;
            this.EncounterDescriptor = encounter_descriptor;
            this.CombatMode = combat_mode;
        }
        public Item Car { get; private set; }
        public uint EncounterDescriptor { get; private set; }
        public int CombatMode { get; private set; }

        public uint MapId { get; set; }
        public ushort HexX { get; set; }
        public ushort HexY { get; set; }
        public Direction Dir { get; set; }
    }
    public class CritterTurnBasedProcessEventArgs : CritterEventArgs
    {
        public CritterTurnBasedProcessEventArgs(Critter cr, Map map, bool begin_turn)
            : base(cr)
        {
            this.Map = map;
            this.BeginTurn = begin_turn;
        }
        public Map Map { get; private set; }
        public bool BeginTurn { get; private set; }
    }
    public class CritterSmthTurnBasedProcessEventArgs : CritterTurnBasedProcessEventArgs
    {
        public CritterSmthTurnBasedProcessEventArgs(Critter cr, Critter from_cr, Map map, bool begin_turn)
            : base(cr, map, begin_turn)
        {
            this.From = from_cr;
        }
        public Critter From { get; private set; }
    }
    public enum NpcPlaneEventResult : uint
    {
        RunGlobal = 0,
        Keep = 1,
        Discard = 2
    }
    public class CritterEventPlaneBeginEndArgs : CritterEventArgs
    {
        public CritterEventPlaneBeginEndArgs (Critter cr, NpcPlane plane, int reason, Critter some_cr, Item some_item)
        : base(cr)
        {
            this.Plane = plane;
            this.Reason = reason;
            this.SomeCr = some_cr;
            this.SomeItem = some_item;
        }
        public NpcPlane Plane { get; private set; }
        public int Reason { get; private set; }
        public Critter SomeCr { get; private set; }
        public Item SomeItem { get; private set; }
        public NpcPlaneEventResult? Result { get; set; }
    }
    public class CritterEventPlaneRunArgs : CritterEventArgs
    {
        public CritterEventPlaneRunArgs (Critter cr, NpcPlane plane, int reason, uint p0, uint p1, uint p2)
            : base(cr)
        {
            this.Plane = plane;
            this.Reason = reason;
            this.Param0 = p0;
            this.Param1 = p1;
            this.Param2 = p2;
        }
        public NpcPlane Plane { get; private set; }
        public int Reason { get; private set; }
        public uint Param0 { get; set; }
        public uint Param1 { get; set; }
        public uint Param2 { get; set; }
        public NpcPlaneEventResult? Result { get; set; }
    }
    // // // // // // // // // // // // //
    
	public partial class Critter
	{
        public event EventHandler<CritterEventArgs> Idle;
        // called by engine
        void RaiseIdle()
        {
            if (Idle != null)
            {
                Idle(this, new CritterEventArgs(this));
            }
        }
        /// <summary>
        /// Raised when critter object is garbaged.
        /// </summary>
        public event EventHandler<CritterFinishEventArgs> Finish;
        // called by engine
        void RaiseFinish(bool deleted)
        {
            if (Finish != null)
                Finish(this, new CritterFinishEventArgs(this, deleted));
        }        
        /// <summary>
        /// Raised when critter is killed.
        /// </summary>
        public event EventHandler<CritterDeadEventArgs> Dead;
        // called by engine
        void RaiseDead(Critter killer) 
        {
            if (Dead != null)
                Dead(this, new CritterDeadEventArgs(this, killer));
        }
        /// <summary>
        /// Raised when critter respawns.
        /// </summary>
        public event EventHandler<CritterEventArgs> Respawn;
        // called by engine
        void RaiseRespawn()
        {
            if (Respawn != null)
                Respawn(this, new CritterEventArgs(this));
        }
        /// <summary>
        /// Raised when critter spots another.
        /// </summary>
        public event EventHandler<CritterVisEventArgs> ShowCritter;
        public event EventHandler<CritterVisEventArgs> ShowCritter1;
        public event EventHandler<CritterVisEventArgs> ShowCritter2;
        public event EventHandler<CritterVisEventArgs> ShowCritter3;
        // called by engine
        void RaiseShowCritter(Critter show_cr)
        {
            if (ShowCritter != null)
                ShowCritter(this, new CritterVisEventArgs(this, show_cr));
        }
        void RaiseShowCritter1(Critter show_cr)
        {
            if (ShowCritter1 != null)
                ShowCritter1(this, new CritterVisEventArgs(this, show_cr));
        }
        void RaiseShowCritter2(Critter show_cr)
        {
            if (ShowCritter2 != null)
                ShowCritter2(this, new CritterVisEventArgs(this, show_cr));
        }
        void RaiseShowCritter3(Critter show_cr)
        {
            if (ShowCritter3 != null)
                ShowCritter3(this, new CritterVisEventArgs(this, show_cr));
        }
        /// <summary>
        /// Raised when critter leaves another field of sight.
        /// </summary>
        public event EventHandler<CritterVisEventArgs> HideCritter;
        public event EventHandler<CritterVisEventArgs> HideCritter1;
        public event EventHandler<CritterVisEventArgs> HideCritter2;
        public event EventHandler<CritterVisEventArgs> HideCritter3;
        // called by engine
        void RaiseHideCritter(Critter hide_cr)
        {
            if (HideCritter != null)
                HideCritter(this, new CritterVisEventArgs(this, hide_cr));
        }
        void RaiseHideCritter1(Critter hide_cr)
        {
            if (HideCritter1 != null)
                HideCritter1(this, new CritterVisEventArgs(this, hide_cr));
        }
        void RaiseHideCritter2(Critter hide_cr)
        {
            if (HideCritter2 != null)
                HideCritter2(this, new CritterVisEventArgs(this, hide_cr));
        }
        void RaiseHideCritter3(Critter hide_cr)
        {
            if (HideCritter3 != null)
                HideCritter3(this, new CritterVisEventArgs(this, hide_cr));
        }
        /// <summary>
        /// Raised when critter spots an item that's been placed on map (TODO desc)
        /// </summary>
        public event EventHandler<CritterShowItemOnMapEventArgs> ShowItemOnMap;
        // called by engine
        void RaiseShowItemOnMap(Item item, bool added, Critter dropper)
        {
            if (ShowItemOnMap != null)
                ShowItemOnMap(this, new CritterShowItemOnMapEventArgs(this, item, added, dropper));
        }
        public event EventHandler<CritterChangeItemOnMapEventArgs> ChangeItemOnMap;
        // called by engine
        void RaiseChangeItemOnMap(Item item)
        {
            if (ChangeItemOnMap != null)
                ChangeItemOnMap(this, new CritterChangeItemOnMapEventArgs(this, item));
        }
        public event EventHandler<CritterHideItemOnMapEventArgs> HideItemOnMap;
        // called by engine
        void RaiseHideItemOnMap(Item item, bool removed, Critter picker)
        {
            if (HideItemOnMap != null)
                HideItemOnMap(this, new CritterHideItemOnMapEventArgs(this, item, removed, picker));
        }
        public event EventHandler<CritterAttackEventArgs> Attack;
        // called by engine
        bool RaiseAttack(Critter target)
        {
            var e = new CritterAttackEventArgs(this, target);
            if(Attack != null)
                Attack(this, e);
            return e.Prevent;
        }
        /// <summary>
        /// Raised when critter is attacked.
        /// </summary>
        public event EventHandler<CritterAttackedEventArgs> Attacked;
        // called by engine
        bool RaiseAttacked(Critter attacker)
        {
            var e = new CritterAttackedEventArgs(this, attacker);
            if (Attacked != null)
                Attacked(this, e);
            return e.Prevent;
        }        
        /// <summary>
        /// Raised when someone attempts to rob the critter.
        /// </summary>
        public event EventHandler<CritterStealingEventArgs> Stealing;
        // called by engine
        void RaiseStealing(Critter thief, bool success, Item item, uint count)
        {
            if (Stealing != null)
                Stealing(this, new CritterStealingEventArgs(this, thief, success, item, count));
        }
        /// <summary>
        /// Raised when critter receives message via Critter::SendMessage.
        /// </summary>
        public event EventHandler<CritterMessageEventArgs> Message;
        // called by engine
        void RaiseMessage(Critter from_cr, int num, int val)
        {
            if (Message != null)
                Message(this, new CritterMessageEventArgs(this, from_cr, num, val));
        }
        /// <summary>
        /// Raised when critter uses an item on something.
        /// </summary>
        public event EventHandler<CritterUseItemEventArgs> UseItem;
        // called by engine
        bool RaiseUseItem(Item item, Critter on_cr, Item on_item, IntPtr on_scenery)
        {
            if (UseItem != null)
            {
                var e = new CritterUseItemEventArgs(this, item, on_cr, on_item, Scenery.FromNative(on_scenery));
                UseItem(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when someone uses an item on critter.
        /// </summary>
        public event EventHandler<CritterUseItemOnMeEventArgs> UseItemOnMe;
        // called by engine
        bool RaiseUseItemOnMe(Critter who_use, Item item)
        {
            if (UseItemOnMe != null)
            {
                var e = new CritterUseItemOnMeEventArgs(this, who_use, item);
                UseItemOnMe(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when critter uses some skill.
        /// </summary>
        public event EventHandler<CritterUseSkillEventArgs> UseSkill;
        // called by engine
        bool RaiseUseSkill(int skill, Critter on_cr, Item on_item, IntPtr on_scenery)
        {
            if (UseSkill != null)
            {
                var e = new CritterUseSkillEventArgs(this, skill, on_cr, on_item, Scenery.FromNative(on_scenery));
                UseSkill(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when some critter uses skill on another.
        /// </summary>
        public event EventHandler<CritterUseSkillOnMeEventArgs> UseSkillOnMe;
        // called by engine
        bool RaiseUseSkillOnMe(Critter who_use, int skill)
        {
            if (UseSkillOnMe != null)
            {
                var e = new CritterUseSkillOnMeEventArgs(this, who_use, skill);
                UseSkillOnMe(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when critter drops an item.
        /// </summary>
        public event EventHandler<CritterDropItemEventArgs> DropItem;
        // called by engine
        void RaiseDropItem(Item item)
        {
            if (DropItem != null)
                DropItem(this, new CritterDropItemEventArgs(this, item));
        }
        /// <summary>
        /// 
        /// </summary>
        // was supposed to be MoveItem, but it collided with MoveItem method...
        public event EventHandler<CritterMoveItemEventArgs> ItemMove;
        // called by engine
        void RaiseMoveItem(Item item, byte from_slot)
        {
            if (ItemMove != null)
                ItemMove(this, new CritterMoveItemEventArgs(this, item, (ItemSlot)from_slot));
        }
        public event EventHandler<CritterKnockoutEventArgs> Knockout;
        // called by engine
        void RaiseKnockout(uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist)
        {
            if (Knockout != null)
                Knockout(this, new CritterKnockoutEventArgs(this, anim2_begin, anim2_idle, anim2_end, lost_ap, knock_dist));
        }
        public event EventHandler<CritterSmthKnockoutEventArgs> SmthKnockout;
        // called by engine
        void RaiseSmthKnockout(Critter from_cr, uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist)
        {
            if(SmthKnockout != null)
                SmthKnockout(this, new CritterSmthKnockoutEventArgs(this, from_cr, anim2_begin, anim2_idle, anim2_end, lost_ap, knock_dist));
        }
        /// <summary>
        /// Raised when critter sees some critter being killed.
        /// </summary>
        public event EventHandler<CritterSmthDeadEventArgs> SmthDead;
        // called by engine
        void RaiseSmthDead(Critter from_cr, Critter killer)
        {
            if (SmthDead != null)
                SmthDead(this, new CritterSmthDeadEventArgs(this, from_cr, killer));
        }
        /// <summary>
        /// Raised after some critter attempted stealing in critter's field of sight.
        /// </summary>
        public event EventHandler<CritterSmthStealingEventArgs> SmthStealing;
        // called by engine
        void RaiseSmthStealing(Critter from_cr, Critter thief, bool success, Item item, uint count)
        {
            if (SmthStealing != null)
                SmthStealing(this, new CritterSmthStealingEventArgs(this, from_cr, thief, success, item, count));
        }        
        /// <summary>
        /// Raised when critter sees other critter using skill on something.
        /// </summary>
        public event EventHandler<CritterSmthUseSkillEventArgs> SmthUseSkill;
        // called by engine
        void RaiseSmthUseSkill(Critter from_cr, int skill, Critter on_cr, Item on_item, IntPtr on_scenery)
        {
            if (SmthUseSkill != null)
                SmthUseSkill(this, new CritterSmthUseSkillEventArgs(this, from_cr, skill, on_cr, on_item, Scenery.FromNative(on_scenery)));
        }
        /// <summary>
        /// Raised when critter sees other critter dropping an item.
        /// </summary>
        public event EventHandler<CritterSmthDropItemEventArgs> SmthDropItem;
        // called by engine
        void RaiseSmthDropItem(Critter from_cr, Item item)
        {
            if (SmthDropItem != null)
                SmthDropItem(this, new CritterSmthDropItemEventArgs(this, from_cr, item));
        }
        /// <summary>
        /// Raised when critter sees other critter moving an item.
        /// </summary>
        public event EventHandler<CritterSmthMoveItemEventArgs> SmthMoveItem;
        // called by engine
        void RaiseSmthMoveItem(Critter from_cr, Item item, ItemSlot from_slot)
        {
            if (SmthMoveItem != null)
                SmthMoveItem(this, new CritterSmthMoveItemEventArgs(this, from_cr, item, from_slot));
        }
        public event EventHandler<CritterBarterEventArgs> Barter;
        // called by native
        bool RaiseBarter(Critter cr_barter, bool attach, uint barter_count)
        {
            if (Barter != null)
            {
                var e = new CritterBarterEventArgs(this, cr_barter, attach, barter_count);
                Barter(this, e);
                return e.Prevent;
            }
            return false;
        }
        public event EventHandler<CritterTalkEventArgs> Talk;
        // called by native
        bool RaiseTalk(Critter cr_talk, bool attach, uint talk_count)
        {
            if (Talk != null)
            {
                var e = new CritterTalkEventArgs(this, cr_talk, attach, talk_count);
                Talk(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when critter moves on globalmap.
        /// </summary>
        public event EventHandler<CritterGlobalProcessEventArgs> GlobalProcess;
        // called by engine
        bool RaiseGlobalProcess(int type, Item car,
            ref float x, ref float y, ref float to_x, ref float to_y, ref float speed,
            ref int encounter_descriptor, ref bool wait_for_answer)
        {
            if (GlobalProcess != null)
            {
                var e = new CritterGlobalProcessEventArgs(this, (GlobalProcessType)type, car)
                {
                    X = x, Y = y, 
                    ToX = to_x, ToY = to_y,
                    Speed = speed, 
                    EncounterDescriptor = encounter_descriptor,
                    WaitForAnswer = wait_for_answer
                };
                GlobalProcess(this, e);
                x = e.X; y = e.Y;
                to_x = e.ToX; to_y = e.ToY;
                speed = e.Speed;
                encounter_descriptor = e.EncounterDescriptor;
                wait_for_answer = e.WaitForAnswer;
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when critter gets into encounter.
        /// </summary>
        public event EventHandler<CritterGlobalInviteEventArgs> GlobalInvite;
        // called by engine
        bool RaiseGlobalInvite(Item car, uint encounter_descriptor, int combat_mode,
            ref uint map_id, ref ushort hx, ref ushort hy, ref byte dir)
        {
            if (GlobalInvite != null)
            {
                var e = new CritterGlobalInviteEventArgs(this, car, encounter_descriptor, combat_mode)
                {
                    MapId = map_id,
                    HexX = hx,
                    HexY = hy,
                    Dir = (Direction)dir
                };
                GlobalInvite(this, e);
                // extract (only if some handler intercepted)
                if (e.Prevent)
                {
                    map_id = e.MapId;
                    hx = e.HexX;
                    hy = e.HexY;
                    dir = (byte)e.Dir;
                }
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised on every turn begin/end.
        /// </summary>
        public event EventHandler<CritterTurnBasedProcessEventArgs> TurnBasedProcess;
        // called by engine
        void RaiseTurnBasedProcess(Map map, bool begin_turn)
        {
            if (TurnBasedProcess != null)
                TurnBasedProcess(this, new CritterTurnBasedProcessEventArgs(this, map, begin_turn));
        }
        public event EventHandler<CritterSmthTurnBasedProcessEventArgs> SmthTurnBasedProcess;
        // called by engine
        void RaiseSmthTurnBasedProcess(Critter from_cr, Map map, bool begin_turn)
        {
            if (SmthTurnBasedProcess != null)
                SmthTurnBasedProcess(this, new CritterSmthTurnBasedProcessEventArgs(this, from_cr, map, begin_turn));
        }
        public event EventHandler<CritterEventPlaneBeginEndArgs> PlaneBegin;
        // called by engine
        bool RaiseEventPlaneBegin(IntPtr plane, int reason, Critter some_cr, Item some_item, ref uint res)
        {
            var args = new CritterEventPlaneBeginEndArgs(this, new NpcPlane(plane), reason, some_cr, some_item);
            if (PlaneBegin != null)
                PlaneBegin(this, args);
            if (args.Result.HasValue)
            {
                res = (uint)args.Result.Value;
                return true;
            }
            return false;
        }
        public event EventHandler<CritterEventPlaneBeginEndArgs> PlaneEnd;
        // called by engine
        bool RaiseEventPlaneEnd(IntPtr plane, int reason, Critter some_cr, Item some_item, ref uint res)
        {
            var args = new CritterEventPlaneBeginEndArgs(this, new NpcPlane(plane), reason, some_cr, some_item);
            if (PlaneEnd != null)
                PlaneEnd(this, args);
            if (args.Result.HasValue)
            {
                res = (uint)args.Result.Value;
                return true;
            }
            return false;
        }
        public event EventHandler<CritterEventPlaneRunArgs> PlaneRun;
        // called by engine
        bool RaiseEventPlaneRun(IntPtr plane, int reason, ref uint p0, ref uint p1, ref uint p2, ref uint res)
        {
            var args = new CritterEventPlaneRunArgs(this, new NpcPlane(plane), reason, p0, p1, p2);
            if (PlaneRun != null)
                PlaneRun(this, args);
            if (args.Result.HasValue)
            {
                p0 = args.Param0;
                p1 = args.Param1;
                p2 = args.Param2;
                res = (uint)args.Result.Value;
                return true;
            }
            return false;
        }
	}
}

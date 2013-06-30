using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public class KarmaVotingEventArgs : EventArgs
    {
        public Critter From { get; private set; }
        public Critter To { get; private set; }
        public bool ValUp { get; private set; }
        public KarmaVotingEventArgs(Critter from, Critter to, bool val_up)
        {
            this.From = from;
            this.To = to;
            this.ValUp = val_up;
        }
    }
    public class GlobalCritterStealingEventArgs : SuccessEventArgs
    {
        public Critter Cr { get; private set; }
        public Critter Thief { get; private set; }
        public Item Item { get; private set; }
        public int Count { get; private set; }
        public GlobalCritterStealingEventArgs(Critter cr, Critter thief, Item item, int count)
        {
            this.Cr = cr;
            this.Thief = thief;
            this.Item = Item;
            this.Count = Count;
        }
    }
    public class GlobalCritterUseItemEventArgs : CritterUseItemEventArgs
    {
        public uint Param { get; private set; }
        public GlobalCritterUseItemEventArgs(Critter cr, Item item, Critter on_cr, Item on_item, Scenery on_scen, uint param)
            : base(cr, item, on_cr, on_item, on_scen)
        {
            this.Param = param;
        }
    }
    public class CritterReloadWeaponEventArgs : CritterEventArgs
    {
        public Item Weapon { get; private set; }
        public Item Ammo { get; private set; }
        public CritterReloadWeaponEventArgs(Critter cr, Item weapon, Item ammo)
            : base(cr)
        {
            this.Weapon = weapon;
            this.Ammo = Ammo;
        }
    }
    public class CritterInitEventArgs : CritterEventArgs
    {
        public bool FirstTime { get; private set; }
        public CritterInitEventArgs(Critter cr, bool first_time)
            : base(cr)
        {
            this.FirstTime = first_time;
        }
    }
    public class PlayerLevelUpEventArgs : CritterEventArgs
    {
        public int SkillIndex { get; private set; }
        public int SkillUp { get; private set; }
        public int PerkIndex { get; private set; }
        public PlayerLevelUpEventArgs(Critter cr, int skill_index, int skill_up, int perk_index)
            : base(cr)
        {
            this.SkillIndex = skill_index;
            this.SkillUp = skill_up;
            this.PerkIndex = perk_index;
        }
    }
    public class ItemsBarterEventArgs : SuccessEventArgs
    {
        public ItemArray SaleItems { get; private set; }
        public UIntArray SaleItemsCount { get; private set; }
        public ItemArray BuyItems { get; private set; }
        public UIntArray BuyItemsCount { get; private set; }
        public Critter Player { get; private set; }
        public Critter Npc { get; private set; }
        public ItemsBarterEventArgs(ItemArray sale_items, UIntArray sale_items_count, 
                                    ItemArray buy_items, UIntArray buy_items_count, 
                                    Critter player, Critter npc)
        {
            this.SaleItems = sale_items;
            this.SaleItemsCount = sale_items_count;
            this.BuyItems = buy_items;
            this.BuyItemsCount = buy_items_count;
            this.Player = player;
            this.Npc = Npc;
        }
    }
    public class ItemsCraftedEventArgs : EventArgs
    {
        public ItemArray Items { get; private set; }
        public UIntArray ItemsCount { get; private set; }
        public ItemArray Resources { get; private set; }
        public Critter Crafter { get; private set; }
        public ItemsCraftedEventArgs(ItemArray items, UIntArray items_count, ItemArray resources, Critter crafter)
        {
            this.Items = items;
            this.ItemsCount = items_count;
            this.Resources = resources;
            this.Crafter = crafter;
        }
    }
    public class GlobalProcessEventArgs : EventArgs
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float ToX { get; set; }
        public float ToY { get; set; }
        public float Speed { get; set; }
        public uint EncounterDescriptor { get; set; }
        public bool WaitForAnswer { get; set; }

        public GlobalProcessType Type { get; private set; }
        public Critter Cr { get; private set; }
        public Item Car { get; private set; }

        public GlobalProcessEventArgs(GlobalProcessType type, Critter cr, Item car)
        {
            this.Type = type;
            this.Cr = cr;
            this.Car = car;
        }
    }
    /// <summary>
    /// Parameters for GlobalInvite event, the values are used only if marked as successful.
    /// </summary>
    /// <remarks>
    /// Every handler should check Success property of this object, so that only one succeeds.
    /// </remarks>
    public class GlobalInviteEventArgs : SuccessEventArgs
    {
        public uint MapId { get; set; }
        public ushort HexX { get; set; }
        public ushort HexY { get; set; }
        public Direction Dir { get; set; }

        public Critter Cr { get; private set; }
        public Item Car { get; private set; }
        public uint EncounterDescriptor { get; private set; }
        public CombatMode CombatMode { get; private set; }

        public GlobalInviteEventArgs(Critter cr, Item car, uint encounter_descriptor, CombatMode combat_mode)
        {
            this.Cr = cr;
            this.Car = car;
            this.EncounterDescriptor = encounter_descriptor;
            this.CombatMode = combat_mode;
        }
    }
    public class WorldSaveEventArgs : EventArgs
    {
        public uint CurrentIndex { get; private set; }
        public WorldSaveEventArgs(uint current_index)
        {
            this.CurrentIndex = current_index;
        }
    }
    /// <summary>
    /// Represents functionality of 'main' scripting module.
    /// </summary>
    public static class Main
    {
        /// <summary>
        /// Special method, called when engine is ready to serve info about game object field offsets.
        /// </summary>
        static void InitFieldOffsets()
        {
            NativeFields.InitFieldOffsets(typeof(Critter));
            NativeFields.InitFieldOffsets(typeof(Item));
            NativeFields.InitFieldOffsets(typeof(Location));
            NativeFields.InitFieldOffsets(typeof(Map));
            NativeFields.InitFieldOffsets(typeof(NpcPlane));
            NativeFields.InitFieldOffsets(typeof(ProtoItem));
            NativeFields.InitFieldOffsets(typeof(Scenery));
            GlobalProperties.Init();
        }

        /// <summary>
        /// Raised before world generation.
        /// </summary>
        public static event EventHandler Init;
        // called by engine
        static void RaiseInit()
        {
            if (Init != null)
                Init(null, EventArgs.Empty);
        }
        /// <summary>
        /// Raised before main loop.
        /// </summary>
        public static event EventHandler<SuccessEventArgs> Start;
        // called by engine
        static bool RaiseStart()
        {
            SuccessEventArgs e = null;
            if (Start != null)
            {
                e = new SuccessEventArgs();
                Start(null, e);
            }
            return e != null ? e.Success : SuccessEventArgs.Default;
        }
        public static event EventHandler Finish;
        // called by engine
        static void RaiseFinish()
        {
            if(Finish != null)
                Finish(null, null);
        }
        public static event EventHandler<CritterEventArgs> CritterIdle;
        // called by engine
        static void RaiseCritterIdle(Critter cr)
        {
            if (CritterIdle != null)
                CritterIdle(null, new CritterEventArgs(cr));
        }
        public class CritterCheckMoveItemEventArgs : DefaultEventArgs {}
        public delegate void CritterCheckMoveItemHandler(Critter cr, Item item, ItemSlot slot, Item swap_item, CritterCheckMoveItemEventArgs e);
        public static event CritterCheckMoveItemHandler CritterCheckMoveItem;
        // called by engine
        static bool RaiseCritterCheckMoveItem(Critter cr, Item item, byte slot, Item swap_item)
        {
            var e = new CritterCheckMoveItemEventArgs();
            if(CritterCheckMoveItem != null)
                CritterCheckMoveItem(cr, item, (ItemSlot)slot, swap_item, e);
            return e.Prevent;
        }
        public static event EventHandler<CritterMoveItemEventArgs> CritterMoveItem;
        // called by engine
        static void RaiseCritterMoveItem(Critter cr, Item item, byte slot)
        {
            if(CritterMoveItem != null)
                CritterMoveItem(null, new CritterMoveItemEventArgs(cr, item, (ItemSlot)slot));
        }
        /// <summary>
        /// Raised when critter is killed.
        /// </summary>
        public static event EventHandler<CritterDeadEventArgs> CritterDead;
        // called by engine
        static void RaiseCritterDead(Critter cr, Critter killer)
        {
            if (CritterDead != null)
                CritterDead(null, new CritterDeadEventArgs(cr, killer));
        }
        /// <summary>
        /// Raised after critter has respawned.
        /// </summary>
        public static event EventHandler<CritterEventArgs> CritterRespawn;
        // called by engine
        static void RaiseCritterRespawn(Critter cr)
        {
            if (CritterRespawn != null)
                CritterRespawn(null, new CritterEventArgs(cr));
        }
        /// <summary>
        /// Raised when critter enters map.
        /// </summary>
        public static event EventHandler<MapInOutCritterEventArgs> MapCriterIn;
        // called by engine
        static void RaiseMapCritterIn(Map map, Critter cr)
        {
            if (MapCriterIn != null)
                MapCriterIn(null, new MapInOutCritterEventArgs(map, cr));
        }
        /// <summary>
        /// Raised when critter leaves map.
        /// </summary>
        public static event EventHandler<MapInOutCritterEventArgs> MapCritterOut;
        // called by engine
        static void RaiseMapCritterOut(Map map, Critter cr)
        {
            if (MapCritterOut != null)
                MapCritterOut(null, new MapInOutCritterEventArgs(map, cr));
        }
        public delegate void KarmaVotingHandler(Critter crFrom, Critter crTo, bool valUp);
        /// <summary>
        /// Called when player votes for another. Distance and timeout is already checked.
        /// </summary>
        public static event EventHandler<KarmaVotingEventArgs> KarmaVoting;
        // called by engine
        static void RaiseKarmaVoting(Critter cr_from, Critter cr_to, bool val_up)
        {
            if (KarmaVoting != null)
                KarmaVoting(null, new KarmaVotingEventArgs(cr_from, cr_to, val_up));
        }
        public delegate void CritterAttackHandler(Critter cr, Critter target, ProtoItem weapon, byte weaponMode, ProtoItem ammo);
        /// <summary>
        /// Raised when one critter attacks another.
        /// </summary>
        public static event CritterAttackHandler CritterAttack;
        // called by engine
        static void RaiseCritterAttack(Critter cr, Critter target, IntPtr weapon, byte weaponMode, IntPtr ammo)
        {
            if(CritterAttack != null)
                CritterAttack(null, target, new ProtoItem(weapon), weaponMode, ammo == IntPtr.Zero ? null : new ProtoItem(ammo));
        }
        public delegate void CritterAttackedHandler(Critter cr, Critter attacker);
        /// <summary>
        /// Raised when critter is attacked, only when permitted by critter-specific event handlers.
        /// </summary>
        public static event EventHandler<CritterAttackedEventArgs> CritterAttacked;
        // called by engine
        static void RaiseCritterAttacked(Critter cr, Critter attacker)
        {
            if (CritterAttacked != null)
                CritterAttacked(null, new CritterAttackedEventArgs(cr, attacker));
        }
        /// <summary>
        /// Raised when one critter attempts to steal an item from another critter.
        /// </summary>
        public static event EventHandler<GlobalCritterStealingEventArgs> CritterStealing;
        // called by engine
        static bool RaiseCritterStealing(Critter cr, Critter thief, Item item, int count)
        {
            GlobalCritterStealingEventArgs e = null;
            if (CritterStealing != null)
            {
                e = new GlobalCritterStealingEventArgs(cr, thief, item, count);
                CritterStealing(null, e);
            }
            return e != null ? e.Success : GlobalCritterStealingEventArgs.Default;
        }
              /// <summary>
        /// Raised when critter uses an item on something.
        /// </summary>
        public static event EventHandler<GlobalCritterUseItemEventArgs> CritterUseItem;
        // called by engine
        static bool RaiseCritterUseItem(Critter cr, Item item,
            Critter target_cr, Item target_item, Scenery target_scen, uint param)
        {
            GlobalCritterUseItemEventArgs e = null;
            if (CritterUseItem != null)
            {
                e = new GlobalCritterUseItemEventArgs(cr, item, target_cr, target_item, target_scen, param);
                CritterUseItem(null, e);
            }
            return e != null ? e.Prevent : false;
        }
        /// <summary>
        /// Raised when critter uses skill on something.
        /// </summary>
        public static event EventHandler<CritterUseSkillEventArgs> CritterUseSkill;
        // called by engine
        static bool RaiseCritterUseSkill(Critter cr, int skill,
            Critter target_cr, Item target_item, Scenery target_scen)
        {
            CritterUseSkillEventArgs e = null;
            if (CritterUseSkill != null)
            {
                e = new CritterUseSkillEventArgs(cr, skill, target_cr, target_item, target_scen);
                CritterUseSkill(null, e);
            }
            return e != null ? e.Prevent : false;
        }
        public static event EventHandler<CritterReloadWeaponEventArgs> CritterReloadWeapon;
        // called by engine
        static void RaiseCritterReloadWeapon(Critter cr, Item weapon, Item ammo)
        {
            if(CritterReloadWeapon != null)
                CritterReloadWeapon(null, new CritterReloadWeaponEventArgs(cr, weapon, ammo));
        }
        /// <summary>
        /// Raised when critter is initialized.
        /// </summary>
        public static event EventHandler<CritterInitEventArgs> CritterInit;
        // called by engine
        static void RaiseCritterInit(Critter cr, bool first_time)
        {
            if(CritterInit != null)
                CritterInit(null, new CritterInitEventArgs(cr, first_time));
        }
        public delegate void CritterFinishHandler(Critter cr, bool to_delete);
        /// <summary>
        /// Raised when critter is about to be removed.
        /// </summary>
        public static event EventHandler<CritterFinishEventArgs> CritterFinish;
        // called by engine
        static void RaiseCritterFinish(Critter cr, bool to_delete)
        {
            if (CritterFinish != null)
                CritterFinish(null, new CritterFinishEventArgs(cr, to_delete));
        }
        /// <summary>
        /// Raised for every skill/perk when player levels up.
        /// </summary>
        public static event EventHandler<PlayerLevelUpEventArgs> PlayerLevelUp;
        // called by engine
        static void RaisePlayerLevelUp(Critter cr, int skill_index, int skill_up, int perk_index)
        {
            if (PlayerLevelUp != null)
                PlayerLevelUp(null, new PlayerLevelUpEventArgs(cr, skill_index, skill_up, perk_index));
        }
        /// <summary>
        /// Raised when turn based combat begins on a map.
        /// </summary>
        public static event EventHandler<MapEventArgs> TurnBasedBegin;
        // called by engine
        static void RaiseTurnBasedBegin(Map map)
        {
            if (TurnBasedBegin != null)
                TurnBasedBegin(null, new MapEventArgs(map));
        }
        /// <summary>
        /// Raised when turn based combat ends on a map.
        /// </summary>
        public static event EventHandler<MapEventArgs> TurnBasedEnd;
        // called by engine
        static void RaiseTurnBasedEnd(Map map)
        {
            if (TurnBasedEnd != null)
                TurnBasedEnd(null, new MapEventArgs(map));
        }
        /// <summary>
        /// Raised whenever turn starts/ends.
        /// </summary>
        public static event EventHandler<MapTurnBasedProcessEventArgs> TurnBasedProcess;
        // called by engine
        static void RaiseTurnBasedProcess(Map map, Critter cr, bool begin_turn)
        {
            if (TurnBasedProcess != null)
                TurnBasedProcess(null, new MapTurnBasedProcessEventArgs(map, cr, begin_turn));
        }
        /// <summary>
        /// Raised when player attempts to trade with npc.
        /// </summary>
        public static event EventHandler<ItemsBarterEventArgs> ItemsBarter;
        // called by engine
        static bool RaiseItemsBarter(IntPtr sale_items, IntPtr sale_items_count, IntPtr buy_items, IntPtr buy_items_count, Critter player, Critter npc)
        {
            ItemsBarterEventArgs e = null;
            if (ItemsBarter != null)
            {
                e = new ItemsBarterEventArgs(new ItemArray(sale_items), new UIntArray(sale_items_count),
                    new ItemArray(buy_items), new UIntArray(buy_items_count),
                    player, npc);
                ItemsBarter(null, e);
            }
            return e != null ? e.Success : ItemsBarterEventArgs.Default;
        }
        /// <summary>
        /// Raised after items are crafted.
        /// </summary>
        public static event EventHandler<ItemsCraftedEventArgs> ItemsCrafted;
        // called by engine
        static void RaiseItemsCrafted(IntPtr items, IntPtr items_count, IntPtr resources, Critter crafter)
        {
            if (ItemsCrafted != null)
            {
                ItemsCrafted(null, new ItemsCraftedEventArgs(new ItemArray(items), new UIntArray(items_count), new ItemArray(resources), crafter));
            }
        }
        /// <summary>
        /// Raised when group moves on worldmap.
        /// </summary>
        public static event EventHandler<GlobalProcessEventArgs> GlobalProcess;
        // called by engine
        static void RaiseGlobalProcess(int type, Critter cr, Item car,
            ref float x, ref float y, ref float to_x, ref float to_y, ref float speed,
            ref uint encounter_descriptor, ref bool wait_for_answer)
        {
            if (GlobalProcess != null)
            {
                var e = new GlobalProcessEventArgs((GlobalProcessType)type, cr, car)
                {
                    X = x, Y = y,
                    ToX = to_x, ToY = to_y,
                    Speed = speed,
                    EncounterDescriptor = encounter_descriptor,
                    WaitForAnswer = wait_for_answer
                };
                GlobalProcess(null, e);
                // extract values
                x = e.X;
                y = e.Y;
                to_x = e.ToX;
                to_y = e.ToY;
                speed = e.Speed;
                encounter_descriptor = (uint)e.EncounterDescriptor;
                wait_for_answer = e.WaitForAnswer;
            }
        }
        public static event EventHandler<GlobalInviteEventArgs> GlobalInvite;
        // called by engine
        static void RaiseGlobalInvite(Critter cr, Item car, uint encounter_descriptor, int combat_mode,
            ref uint map_id, ref ushort hx, ref ushort hy, ref byte dir)
        {
            if (GlobalInvite != null)
            {
                var e = new GlobalInviteEventArgs(cr, car, encounter_descriptor, (CombatMode)combat_mode);
                GlobalInvite(null, e);
                // extract values if we succeeded
                if (e.Success)
                {
                    map_id = e.MapId;
                    hx = e.HexX;
                    hy = e.HexY;
                    dir = (byte)e.Dir;
                }
            }
        }
        public static event EventHandler<WorldSaveEventArgs> WorldSave;
        // called by engine
        static void RaiseWorldSave(uint current_index)
        {
            if(WorldSave != null)
                WorldSave(null, new WorldSaveEventArgs(current_index));
        }

    }
}

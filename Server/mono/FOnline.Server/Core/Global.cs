using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public class Global
    {
        static Global()
        {
            Time = new Time();
            MapManager = new MapManager();
            Randomizer = new Randomizer();
            GlobalProperties = new GlobalProperties();
            TimeEvents = new TimeEvents();
            Logging = new Logging();
            CritterManager = new CritterManager();
            VarManager = new VarManager();
            DialogManager = new DialogManager();
            ItemManager = new ItemManager();
            Math = new Math();
            AnyData = new AnyData();
        }
        # region Critter manager
        public static ICritterManager CritterManager { get; set; }
        public static Critter GetCritter(uint id)
        {
            return CritterManager.GetCritter(id);
        }
		public static void DeleteNpc(Critter cr)
		{
			CritterManager.DeleteNpc(cr);
		}
        public static NpcPlane CreatePlane()
        {
            return CritterManager.CreatePlane();
        }
        public static bool SetParameterGetBehaviour(uint index, Func<IntPtr, uint, int> func)
        {
            return CritterManager.SetParameterGetBehaviour(index, func);
        }
        public static bool SetParameterGetBehaviour(uint index, string func_name)
        {
            return CritterManager.SetParameterGetBehaviour(index, func_name);
        }
        public static bool SetParameterChangeBehaviour(uint index, Action<IntPtr, uint, int> func)
        {
            return CritterManager.SetParameterChangeBehaviour(index, func);
        }
        public static bool SetParameterChangeBehaviour(uint index, string func_name)
        {
            return CritterManager.SetParameterChangeBehaviour(index, func_name);
        }
        public static void SetChosenSendParameter(int index, bool enabled)
        {
            CritterManager.SetChosenSendParameter(index, enabled);
        }
        public static void SetSendParameter(int index, bool enabled)
        {
            CritterManager.SetSendParameter(index, enabled);
        }
        public static void SetSendParameter(int index, bool enabled, string allow_func)
        {
            CritterManager.SetSendParameter(index, enabled, allow_func);
        }
        public static void SetRegistrationParameter(uint index, bool enabled)
        {
            CritterManager.SetRegistrationParameter(index, enabled);
        }
        public static bool IsCritterCanWalk(uint cr_type)
        {
            return CritterManager.IsCritterCanWalk(cr_type);
        }
        public static bool IsCritterCanRun(uint cr_type)
        {
            return CritterManager.IsCritterCanRun(cr_type);
        }
        public static bool IsCritterCanAim(uint cr_type)
        {
            return CritterManager.IsCritterCanAim(cr_type);
        }
        public static bool IsCritterCanRotate(uint cr_type)
        {
            return CritterManager.IsCritterCanRotate(cr_type);
        }
        public static bool IsCritterCanArmor(uint cr_type)
        {
            return CritterManager.IsCritterCanArmor(cr_type);
        }
        public static bool IsCritterAnim1(uint cr_type)
        {
            return CritterManager.IsCritterAnim1(cr_type);
        }
		public static uint GetCrittersDistantion(Critter cr1, Critter cr2)
		{
			return CritterManager.GetCrittersDistantion(cr1, cr2);
		}
        #endregion

        #region Item manager
        public static IItemManager ItemManager { get; set; }
        public static Item GetItem(uint item_id)
        {
            return ItemManager.GetItem(item_id);
        }
        public static void MoveItem(Item item, uint count, Critter to_cr)
        {
            ItemManager.MoveItem(item, count, to_cr);
        }
        public static void MoveItem(Item item, uint count, Item to_cont, uint stack_id)
        {
            ItemManager.MoveItem(item, count, to_cont, stack_id);
        }
        public static void MoveItem(Item item, uint count, Map to_map, ushort to_hx, ushort to_hy)
        {
            ItemManager.MoveItem(item, count, to_map, to_hx, to_hy);
        }
        public static void MoveItems(ItemArray items, Critter to_cr)
        {
            ItemManager.MoveItems(items, to_cr);
        }
        public static void MoveItems(ItemArray items, Item to_cont, uint stack_id)
        {
            ItemManager.MoveItems(items, to_cont, stack_id);
        }
        public static void MoveItems(ItemArray items, Map to_map, ushort to_hx, ushort to_hy)
        {
            ItemManager.MoveItems(items, to_map, to_hx, to_hy);
        }
        public static void DeleteItem(Item item)
        {
            ItemManager.DeleteItem(item);
        }
        public static void DeleteItems(ItemArray items)
        {
            ItemManager.DeleteItems(items);
        }
        public static ulong WorldItemCount(ushort pid)
        {
            return ItemManager.WorldItemCount(pid);
        }
        #endregion

        #region Dialogs
        public static IDialogManager DialogManager { get; set; }

        public static bool RunDialog(Critter player, Critter npc, bool ignore_distance)
        {
            return DialogManager.RunDialog(player, npc, ignore_distance);
        }
        public static bool RunDialog(Critter player, Critter npc, uint dialog_pack, bool ignore_distance)
        {
            return DialogManager.RunDialog(player, npc, dialog_pack, ignore_distance);
        }
        public static bool RunDialog(Critter player, uint dialog_pack, ushort hx, ushort hy, bool ignore_distance)
        {
            return DialogManager.RunDialog(player, dialog_pack, hx, hy, ignore_distance);
        }
        #endregion

        #region GameVars
        public static IVarManager VarManager { get; set; }

        public static GameVar GetGlobalVar(ushort var_id)
        {
            return VarManager.GetGlobalVar(var_id);
        }
        public static GameVar GetLocalVar(ushort var_id, uint master_id)
        {
            return VarManager.GetLocalVar(var_id, master_id);
        }
        public static GameVar GetUnicumVar(ushort var_id, uint master_id, uint slave_id)
        {
            return VarManager.GetUnicumVar(var_id, master_id, slave_id);
        }
        #endregion

        #region Global properties
        public static IGlobalProperties GlobalProperties { get; set; }
		public static  ushort Year { get { return GlobalProperties.Year; } }
		public static  ushort Month { get { return GlobalProperties.Month; } }
		public static  ushort Day { get { return GlobalProperties.Day; } }
		public static  ushort Hour { get { return GlobalProperties.Hour; } }
		public static  ushort Minute { get { return GlobalProperties.Minute; } }
		public static  ushort Second { get { return GlobalProperties.Second; } }
		public static  uint   FullSecond { get { return GlobalProperties.FullSecond; } }
		public static  ushort TimeMultiplier { get { return GlobalProperties.TimeMultiplier; } }
		public static  bool DisableTcpNagle { get { return GlobalProperties.DisableTcpNagle; } set { GlobalProperties.DisableTcpNagle = value; } }
		public static  bool DisableZlibCompression { get { return GlobalProperties.DisableZlibCompression; } set { GlobalProperties.DisableZlibCompression = value; } }
		public static  uint FloodSize { get { return GlobalProperties.FloodSize; } set { GlobalProperties.FloodSize = value; } }
		public static  bool NoAnswerShuffle { get { return GlobalProperties.NoAnswerShuffle; } set { GlobalProperties.NoAnswerShuffle = value; } }
		public static  bool DialogDemandRecheck { get { return GlobalProperties.DialogDemandRecheck; } set { GlobalProperties.DialogDemandRecheck = value; } }
		public static  uint FixBoyDefaultExperience { get { return GlobalProperties.FixBoyDefaultExperience; } set { GlobalProperties.FixBoyDefaultExperience = value; } }
		public static  uint SneakDivider { get { return GlobalProperties.SneakDivider; } set { GlobalProperties.SneakDivider = value; } }
		public static  uint LevelCap { get { return GlobalProperties.LevelCap; } set { GlobalProperties.LevelCap = value; } }
		public static  bool LevelCapAddExperience { get { return GlobalProperties.LevelCapAddExperience; } set { GlobalProperties.LevelCapAddExperience = value; } }
		public static  uint LookNormal { get { return GlobalProperties.LookNormal; } set { GlobalProperties.LookNormal = value; } }
		public static  uint LookMinimum { get { return GlobalProperties.LookMinimum; } set { GlobalProperties.LookMinimum = value; } }
		public static  uint GlobalMapMaxGroupCount { get { return GlobalProperties.GlobalMapMaxGroupCount; } set { GlobalProperties.GlobalMapMaxGroupCount = value; } }
		public static  uint CritterIdleTick { get { return GlobalProperties.CritterIdleTick; } set { GlobalProperties.CritterIdleTick = value; } }
		public static  uint TurnBasedTick { get { return GlobalProperties.TurnBasedTick; } set { GlobalProperties.TurnBasedTick = value; } }
		public static  int DeadHitPoints { get { return GlobalProperties.DeadHitPoints; } set { GlobalProperties.DeadHitPoints = value; } }
		public static  int Breaktime { get { return GlobalProperties.Breaktime; } set { GlobalProperties.Breaktime = value; } }
		public static  uint TimeoutTransfer { get { return GlobalProperties.TimeoutTransfer; } set { GlobalProperties.TimeoutTransfer = value; } }
		public static  uint TimeoutBattle { get { return GlobalProperties.TimeoutBattle; } set { GlobalProperties.TimeoutBattle = value; } }
		public static  uint ApRegeneration { get { return GlobalProperties.ApRegeneration; } set { GlobalProperties.ApRegeneration = value; } }
		public static  uint RtApCostCritterWalk { get { return GlobalProperties.RtApCostCritterWalk; } set { GlobalProperties.RtApCostCritterWalk = value; } }
		public static  uint RtApCostCritterRun { get { return GlobalProperties.RtApCostCritterRun; } set { GlobalProperties.RtApCostCritterRun = value; } }
		public static  uint RtApCostMoveItemContainer { get { return GlobalProperties.RtApCostMoveItemContainer; } set { GlobalProperties.RtApCostMoveItemContainer = value; } }
		public static  uint RtApCostMoveItemInventory { get { return GlobalProperties.RtApCostMoveItemInventory; } set { GlobalProperties.RtApCostMoveItemInventory = value; } }
		public static  uint RtApCostPickItem { get { return GlobalProperties.RtApCostPickItem; } set { GlobalProperties.RtApCostPickItem = value; } }
		public static  uint RtApCostDropItem { get { return GlobalProperties.RtApCostDropItem; } set { GlobalProperties.RtApCostDropItem = value; } }
		public static  uint RtApCostReloadWeapon { get { return GlobalProperties.RtApCostReloadWeapon; } set { GlobalProperties.RtApCostReloadWeapon = value; } }
		public static  uint RtApCostPickCritter { get { return GlobalProperties.RtApCostPickCritter; } set { GlobalProperties.RtApCostPickCritter = value; } }
		public static  uint RtApCostUseItem { get { return GlobalProperties.RtApCostUseItem; } set { GlobalProperties.RtApCostUseItem = value; } }
		public static  uint RtApCostUseSkill { get { return GlobalProperties.RtApCostUseSkill; } set { GlobalProperties.RtApCostUseSkill = value; } }
		public static  bool RtAlwaysRun { get { return GlobalProperties.RtAlwaysRun; } set { GlobalProperties.RtAlwaysRun = value; } }
		public static  uint TbApCostCritterMove { get { return GlobalProperties.TbApCostCritterMove; } set { GlobalProperties.TbApCostCritterMove = value; } }
		public static  uint TbApCostMoveItemContainer { get { return GlobalProperties.TbApCostMoveItemContainer; } set { GlobalProperties.TbApCostMoveItemContainer = value; } }
		public static  uint TbApCostMoveItemInventory { get { return GlobalProperties.TbApCostMoveItemInventory; } set { GlobalProperties.TbApCostMoveItemInventory = value; } }
		public static  uint TbApCostPickItem { get { return GlobalProperties.TbApCostPickItem; } set { GlobalProperties.TbApCostPickItem = value; } }
		public static  uint TbApCostDropItem { get { return GlobalProperties.TbApCostDropItem; } set { GlobalProperties.TbApCostDropItem = value; } }
		public static  uint TbApCostReloadWeapon { get { return GlobalProperties.TbApCostReloadWeapon; } set { GlobalProperties.TbApCostReloadWeapon = value; } }
		public static  uint TbApCostPickCritter { get { return GlobalProperties.TbApCostPickCritter; } set { GlobalProperties.TbApCostPickCritter = value; } }
		public static  uint TbApCostUseItem { get { return GlobalProperties.TbApCostUseItem; } set { GlobalProperties.TbApCostUseItem = value; } }
		public static  uint TbApCostUseSkill { get { return GlobalProperties.TbApCostUseSkill; } set { GlobalProperties.TbApCostUseSkill = value; } }
		public static  uint ApCostAimEyes { get { return GlobalProperties.ApCostAimEyes; } set { GlobalProperties.ApCostAimEyes = value; } }
		public static  uint ApCostAimHead { get { return GlobalProperties.ApCostAimHead; } set { GlobalProperties.ApCostAimHead = value; } }
		public static  uint ApCostAimGroin { get { return GlobalProperties.ApCostAimGroin; } set { GlobalProperties.ApCostAimGroin = value; } }
		public static  uint ApCostAimTorso { get { return GlobalProperties.ApCostAimTorso; } set { GlobalProperties.ApCostAimTorso = value; } }
		public static  uint ApCostAimArms { get { return GlobalProperties.ApCostAimArms; } set { GlobalProperties.ApCostAimArms = value; } }
		public static  uint ApCostAimLegs { get { return GlobalProperties.ApCostAimLegs; } set { GlobalProperties.ApCostAimLegs = value; } }
		public static  bool TbAlwaysRun { get { return GlobalProperties.TbAlwaysRun; } set { GlobalProperties.TbAlwaysRun = value; } }
		public static  bool RunOnCombat { get { return GlobalProperties.RunOnCombat; } set { GlobalProperties.RunOnCombat = value; } }
		public static  bool RunOnTransfer { get { return GlobalProperties.RunOnTransfer; } set { GlobalProperties.RunOnTransfer = value; } }
		public static  uint GlobalMapWidth { get { return GlobalProperties.GlobalMapWidth; } set { GlobalProperties.GlobalMapWidth = value; } }
		public static  uint GlobalMapHeight { get { return GlobalProperties.GlobalMapHeight; } set { GlobalProperties.GlobalMapHeight = value; } }
		public static  uint GlobalMapZoneLength { get { return GlobalProperties.GlobalMapZoneLength; } set { GlobalProperties.GlobalMapZoneLength = value; } }
		public static  uint GlobalMapMoveTime { get { return GlobalProperties.GlobalMapMoveTime; } set { GlobalProperties.GlobalMapMoveTime = value; } }
		public static  uint BagRefreshTime { get { return GlobalProperties.BagRefreshTime; } set { GlobalProperties.BagRefreshTime = value; } }
		public static  uint AttackAnimationsMinDist { get { return GlobalProperties.AttackAnimationsMinDist; } set { GlobalProperties.AttackAnimationsMinDist = value; } }
		public static  uint WisperDist { get { return GlobalProperties.WisperDist; } set { GlobalProperties.WisperDist = value; } }
		public static  uint ShoutDist { get { return GlobalProperties.ShoutDist; } set { GlobalProperties.ShoutDist = value; } }
		public static  int LookChecks { get { return GlobalProperties.LookChecks; } set { GlobalProperties.LookChecks = value; } }
		public static  uint LookDir0 { get { return GlobalProperties.LookDir0; } set { GlobalProperties.LookDir0 = value; } }
		public static  uint LookDir1 { get { return GlobalProperties.LookDir1; } set { GlobalProperties.LookDir1 = value; } }
		public static  uint LookDir2 { get { return GlobalProperties.LookDir2; } set { GlobalProperties.LookDir2 = value; } }
		public static  uint LookDir3 { get { return GlobalProperties.LookDir3; } set { GlobalProperties.LookDir3 = value; } }
		public static  uint LookDir4 { get { return GlobalProperties.LookDir4; } set { GlobalProperties.LookDir4 = value; } }
		public static  uint LookSneakDir0 { get { return GlobalProperties.LookSneakDir0; } set { GlobalProperties.LookSneakDir0 = value; } }
		public static  uint LookSneakDir1 { get { return GlobalProperties.LookSneakDir1; } set { GlobalProperties.LookSneakDir1 = value; } }
		public static  uint LookSneakDir2 { get { return GlobalProperties.LookSneakDir2; } set { GlobalProperties.LookSneakDir2 = value; } }
		public static  uint LookSneakDir3 { get { return GlobalProperties.LookSneakDir3; } set { GlobalProperties.LookSneakDir3 = value; } }
		public static  uint LookSneakDir4 { get { return GlobalProperties.LookSneakDir4; } set { GlobalProperties.LookSneakDir4 = value; } }
		public static  uint LookWeight { get { return GlobalProperties.LookWeight; } set { GlobalProperties.LookWeight = value; } }
		public static  bool CustomItemCost { get { return GlobalProperties.CustomItemCost; } set { GlobalProperties.CustomItemCost = value; } }
		public static  uint RegistrationTimeout { get { return GlobalProperties.RegistrationTimeout; } set { GlobalProperties.RegistrationTimeout = value; } }
		public static  uint AccountPlayTime { get { return GlobalProperties.AccountPlayTime; } set { GlobalProperties.AccountPlayTime = value; } }
		public static  bool LoggingVars { get { return GlobalProperties.LoggingVars; } set { GlobalProperties.LoggingVars = value; } }
		public static  uint ScriptRunSuspendTimeout { get { return GlobalProperties.ScriptRunSuspendTimeout; } set { GlobalProperties.ScriptRunSuspendTimeout = value; } }
		public static  uint ScriptRunMessageTimeout { get { return GlobalProperties.ScriptRunMessageTimeout; } set { GlobalProperties.ScriptRunMessageTimeout = value; } }
		public static  uint TalkDistance { get { return GlobalProperties.TalkDistance; } set { GlobalProperties.TalkDistance = value; } }
		public static  uint NpcMaxTalkers { get { return GlobalProperties.NpcMaxTalkers; } set { GlobalProperties.NpcMaxTalkers = value; } }
		public static  uint MinNameLength { get { return GlobalProperties.MinNameLength; } set { GlobalProperties.MinNameLength = value; } }
		public static  uint MaxNameLength { get { return GlobalProperties.MaxNameLength; } set { GlobalProperties.MaxNameLength = value; } }
		public static  uint DlgTalkMinTime { get { return GlobalProperties.DlgTalkMinTime; } set { GlobalProperties.DlgTalkMinTime = value; } }
		public static  uint DlgBarterMinTime { get { return GlobalProperties.DlgBarterMinTime; } set { GlobalProperties.DlgBarterMinTime = value; } }
		public static  uint MinimumOfflineTime { get { return GlobalProperties.MinimumOfflineTime; } set { GlobalProperties.MinimumOfflineTime = value; } }
		public static  int StartSpecialPoints { get { return GlobalProperties.StartSpecialPoints; } set { GlobalProperties.StartSpecialPoints = value; } }
		public static  int StartTagSkillPoints { get { return GlobalProperties.StartTagSkillPoints; } set { GlobalProperties.StartTagSkillPoints = value; } }
		public static  int SkillMaxValue { get { return GlobalProperties.SkillMaxValue; } set { GlobalProperties.SkillMaxValue = value; } }
		public static  int SkillModAdd2 { get { return GlobalProperties.SkillModAdd2; } set { GlobalProperties.SkillModAdd2 = value; } }
		public static  int SkillModAdd3 { get { return GlobalProperties.SkillModAdd3; } set { GlobalProperties.SkillModAdd3 = value; } }
		public static  int SkillModAdd4 { get { return GlobalProperties.SkillModAdd4; } set { GlobalProperties.SkillModAdd4 = value; } }
		public static  int SkillModAdd5 { get { return GlobalProperties.SkillModAdd5; } set { GlobalProperties.SkillModAdd5 = value; } }
		public static  int SkillModAdd6 { get { return GlobalProperties.SkillModAdd6; } set { GlobalProperties.SkillModAdd6 = value; } }
		public static  bool AbsoluteOffsets { get { return GlobalProperties.AbsoluteOffsets; } set { GlobalProperties.AbsoluteOffsets = value; } }
		public static  uint SkillBegin { get { return GlobalProperties.SkillBegin; } set { GlobalProperties.SkillBegin = value; } }
		public static  uint SkillEnd { get { return GlobalProperties.SkillEnd; } set { GlobalProperties.SkillEnd = value; } }
		public static  uint TimeoutBegin { get { return GlobalProperties.TimeoutBegin; } set { GlobalProperties.TimeoutBegin = value; } }
		public static  uint TimeoutEnd { get { return GlobalProperties.TimeoutEnd; } set { GlobalProperties.TimeoutEnd = value; } }
		public static  uint KillBegin { get { return GlobalProperties.KillBegin; } set { GlobalProperties.KillBegin = value; } }
		public static  uint KillEnd { get { return GlobalProperties.KillEnd; } set { GlobalProperties.KillEnd = value; } }
		public static  uint PerkBegin { get { return GlobalProperties.PerkBegin; } set { GlobalProperties.PerkBegin = value; } }
		public static  uint PerkEnd { get { return GlobalProperties.PerkEnd; } set { GlobalProperties.PerkEnd = value; } }
		public static  uint AddictionBegin { get { return GlobalProperties.AddictionBegin; } set { GlobalProperties.AddictionBegin = value; } }
		public static  uint AddictionEnd { get { return GlobalProperties.AddictionEnd; } set { GlobalProperties.AddictionEnd = value; } }
		public static  uint KarmaBegin { get { return GlobalProperties.KarmaBegin; } set { GlobalProperties.KarmaBegin = value; } }
		public static  uint KarmaEnd { get { return GlobalProperties.KarmaEnd; } set { GlobalProperties.KarmaEnd = value; } }
		public static  uint DamageBegin { get { return GlobalProperties.DamageBegin; } set { GlobalProperties.DamageBegin = value; } }
		public static  uint DamageEnd { get { return GlobalProperties.DamageEnd; } set { GlobalProperties.DamageEnd = value; } }
		public static  uint TraitBegin { get { return GlobalProperties.TraitBegin; } set { GlobalProperties.TraitBegin = value; } }
		public static  uint TraitEnd { get { return GlobalProperties.TraitEnd; } set { GlobalProperties.TraitEnd = value; } }
		public static  uint ReputationBegin { get { return GlobalProperties.ReputationBegin; } set { GlobalProperties.ReputationBegin = value; } }
		public static  uint ReputationEnd { get { return GlobalProperties.ReputationEnd; } set { GlobalProperties.ReputationEnd = value; } }
		public static  int ReputationLoved { get { return GlobalProperties.ReputationLoved; } set { GlobalProperties.ReputationLoved = value; } }
		public static  int ReputationLiked { get { return GlobalProperties.ReputationLiked; } set { GlobalProperties.ReputationLiked = value; } }
		public static  int ReputationAccepted { get { return GlobalProperties.ReputationAccepted; } set { GlobalProperties.ReputationAccepted = value; } }
		public static  int ReputationNeutral { get { return GlobalProperties.ReputationNeutral; } set { GlobalProperties.ReputationNeutral = value; } }
		public static  int ReputationAntipathy { get { return GlobalProperties.ReputationAntipathy; } set { GlobalProperties.ReputationAntipathy = value; } }
		public static  int ReputationHated { get { return GlobalProperties.ReputationHated; } set { GlobalProperties.ReputationHated = value; } }	
		public static  uint HitAimHead { get { return GlobalProperties.HitAimHead; } set { GlobalProperties.HitAimHead = value; } }
		public static  uint HitAimEyes { get { return GlobalProperties.HitAimEyes; } set { GlobalProperties.HitAimEyes = value; } }
		public static  uint HitAimTorso { get { return GlobalProperties.HitAimTorso; } set { GlobalProperties.HitAimTorso = value; } }
		public static  uint HitAimArms { get { return GlobalProperties.HitAimArms; } set { GlobalProperties.HitAimArms = value; } }
		public static  uint HitAimGroin { get { return GlobalProperties.HitAimGroin; } set { GlobalProperties.HitAimGroin = value; } }
		public static  uint HitAimLegs { get { return GlobalProperties.HitAimLegs; } set { GlobalProperties.HitAimLegs = value; } }
		public static  int PermanentDeath { get { return GlobalProperties.PermanentDeath; } set { GlobalProperties.PermanentDeath = value; } }
		public static bool MapHexagonal { get { return GlobalProperties.MapHexagonal; } }
        #endregion

        #region AnyData
        public static IAnyData AnyData { get; set; }
        public static bool IsAnyData(string name)
        {
            return AnyData.IsAnyData(name);
        }
        public static bool GetAnyData(string name, UInt8Array data)
        {
            return AnyData.Get(name, data);
        }
        public static bool SetAnyData(string name, UInt8Array data)
        {
            return AnyData.Set(name, data);
        }
        public static bool GetAnyData(string name, UInt16Array data)
        {
            return AnyData.Get(name, data);
        }
        public static bool SetAnyData(string name, UInt16Array data)
        {
            return AnyData.Set(name, data);
        }
        public static bool GetAnyData(string name, UIntArray data)
        {
            return AnyData.Get(name, data);
        }
        public static bool SetAnyData(string name, UIntArray data)
        {
            return AnyData.Set(name, data);
        }
        public static bool GetAnyData(string name, Int8Array data)
        {
            return AnyData.Get(name, data);
        }
        public static bool SetAnyData(string name, Int8Array data)
        {
            return AnyData.Set(name, data);
        }
        public static bool GetAnyData(string name, Int16Array data)
        {
            return AnyData.Get(name, data);
        }
        public static bool SetAnyData(string name, Int16Array data)
        {
            return AnyData.Set(name, data);
        }
        public static bool GetAnyData(string name, IntArray data)
        {
            return AnyData.Get(name, data);
        }
        public static bool SetAnyData(string name, IntArray data)
        {
            return AnyData.Set(name, data);
        }
        public static void EraseAnyData(string name)
        {
            AnyData.Erase(name);
        }
        #endregion

        #region Time events
        public static ITimeEvents TimeEvents { get; set; }

        public static uint CreateTimeEvent(uint begin_second, string func_name, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func_name, save);
        }
        public static uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func, save);
        }
        public static uint CreateTimeEvent(uint begin_second, string func_name, int value, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func_name, (uint)value, save);
        }
        public static uint CreateTimeEvent(uint begin_second, string func_name, uint value, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func_name, value, save);
        }
        public static uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, int value, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func, (uint)value, save);
        }
        public static uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, uint value, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func, value, save);
        }
        public static uint CreateTimeEvent(uint begin_second, string func_name, UIntArray values, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func_name, values, save);
        }
        public static uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, UIntArray values, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func, values, save);
        }
        public static uint CreateTimeEvent(uint begin_second, string func_name, IntArray values, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func_name, values, save);
        }
        public static uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, IntArray values, bool save)
        {
            return TimeEvents.CreateTimeEvent(begin_second, func, values, save);
        }
        public static bool EraseTimeEvent(uint id)
        {
            return TimeEvents.EraseTimeEvent(id);
        }
        public static bool GetTimeEvent(uint id, out uint duration, UIntArray values)
        {
            return TimeEvents.GetTimeEvent(id, out duration, values);
        }
        public static bool GetTimeEvent(uint id, out uint duration, IntArray values)
        {
            return TimeEvents.GetTimeEvent(id, out duration, values);
        }
        public static bool SetTimeEvent(uint id, uint duration, UIntArray values)
        {
            return TimeEvents.SetTimeEvent(id, duration, values);
        }
        public static bool SetTimeEvent(uint id, uint duration, IntArray values)
        {
            return TimeEvents.SetTimeEvent(id, duration, values);
        }
        #endregion

        #region Time
        public static ITime Time { get; set; }

        public static uint GetFullSecond(ushort year, ushort month, ushort day, ushort hour, ushort minute, ushort second)
        {
            return Time.GetFullSecond(year, month, day, hour, minute, second);
        }
        public static uint GetFullSecond(DateTime dt)
        {
            return Time.GetFullSecond((ushort)dt.Year, (ushort)dt.Month, (ushort)dt.Day, (ushort)dt.Hour, (ushort)dt.Minute, (ushort)dt.Second);
        }
        public static DateTime GetGameTime(uint full_second)
        {
            ushort year = 0, month = 0, day = 0, dayofweek = 0, hour = 0, minute = 0, second = 0;
            Time.GetGameTime(full_second, ref year, ref month, ref day, ref dayofweek, ref hour, ref minute, ref second);
            return new DateTime(year, month, day, hour, minute, second);
        }
        #endregion

        #region Maps/Locations
        public static IMapManager MapManager { get; set; }

        public static uint CreateLocation(ushort pid, ushort wx, ushort wy, CritterArray critters)
        {
            return MapManager.CreateLocation(pid, wx, wy, critters);
        }
        public static void DeleteLocation(uint loc_id)
        {
            MapManager.DeleteLocation(loc_id);
        }
        public static Location GetLocation(uint loc_id)
        {
            return MapManager.GetLocation(loc_id);
        }
        public static Location GetLocationByPid(ushort pid, uint skip_count)
        {
            return MapManager.GetLocationByPid(pid, skip_count);
        }
        public static uint GetLocations(ushort wx, ushort wy, uint radius, LocationArray locations)
        {
            return MapManager.GetLocations(wx, wy, radius, locations);
        }
        public static uint GetVisibleLocations(ushort wx, ushort wy, uint radius, Critter visible_for, LocationArray locations)
        {
            return MapManager.GetVisibleLocations(wx, wy, radius, visible_for, locations);
        }
        public static uint GetZoneLocationIds(ushort zx, ushort zy, uint zone_radius, UIntArray location_ids)
        {
            return MapManager.GetZoneLocationIds(zx, zy, zone_radius, location_ids);
        }

        public static Map GetMap(uint map_id)
        {
            return MapManager.GetMap(map_id);
        }
        public static Map GetMapByPid(ushort pid, uint skip_count)
        {
            return MapManager.GetMapByPid(pid, skip_count);
        }
        #endregion

        #region Logging
        public static ILogging Logging { get; set; }
        public static void Log(string message)
        {
            Logging.Log(message);
        }
        public static void Log(string message, params object[] args)
        {
            Logging.Log(message, args);
        }
        public static string GetLastError()
        {
            return Logging.GetLastError();
        }
        #endregion

        #region Random
        public static IRandom Randomizer { get; set; }
        public static int Random(int min, int max)
        {
            return Randomizer.Random(min, max);
        }
        public static T Random<T>(IConvertible min, IConvertible max) where T : IConvertible
        {
            return (T)Randomizer.Random(min, max);
        }
        #endregion

        #region Math
        public static IMath Math { get; set; }
        public static uint GetDistantion(ushort hx1, ushort hy1, ushort hx2, ushort hy2)
        {
            return Math.GetDistantion(hx1, hy1, hx2, hy2);
        }
        public static Direction GetDirection(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy)
        {
            return Math.GetDirection(from_hx, from_hy, to_hx, to_hy);
        }
        public static Direction GetOffsetDir(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float offset)
        {
            return Math.GetOffsetDir(from_hx, from_hy, to_hx, to_hy, offset);
        }
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return Math.Distance(x1, y1, x2, y2);
        }
        #endregion
		#region Misc
		public static IMisc Misc { get; set; }
		public static void RadioMessage(ushort channel, string text)
		{
			Misc.RadioMessage(channel, text);
		}
		public static void RadioMessageMsg(ushort channel, ushort text_msg, uint str_num)
		{
			Misc.RadioMessageMsg(channel, text_msg, str_num);
		}
		public static void RadioMessageMsg(ushort channel, ushort text_msg, uint str_num, string lexems)
		{
			Misc.RadioMessageMsg(channel, text_msg, str_num, lexems);
		}
        public static void SetBestScore(int score, Critter player, string name)
        {
            Misc.SetBestScore(score, player, name);
        }
        public static string GetPlayerName(uint id)
        {
            return Misc.GetPlayerName(id);
        }
        public static uint GetBagItems(uint bag_id, UInt16Array pids, UIntArray min_counts, UIntArray max_counts, IntArray slots)
        {
            return Misc.GetBagItems(bag_id, pids, min_counts, max_counts, slots);
        }
        public static uint GetBagItems(uint bag_id, UInt16Array pids, UIntArray min_counts, UIntArray max_counts, List<ItemSlot> slots)
        {
            var slots_ = new IntArray();
            var res = Misc.GetBagItems(bag_id, pids, min_counts, max_counts, slots_);
            slots.AddRange(slots_.Select (s => (ItemSlot)s));
            return res;
        }
        public static bool AddTextListener(Say say_type, string first_str, ushort parameter, string script_name)
        {
            return Misc.AddTextListener(say_type, first_str, parameter, script_name);
        }
        public static void EraseTextListener(Say say_type, string first_str, ushort parameter)
        {
            Misc.EraseTextListener(say_type, first_str, parameter);
        }
		#endregion
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_GC();
        public static void CollectScriptGarbage()
        {
            Global_GC();
        }
    }
}

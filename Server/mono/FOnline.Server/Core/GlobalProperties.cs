using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace FOnline
{
    public interface IGlobalProperties
    {
		//
		// Shared (for AS and mono)
		//
	    ushort Year { get; }
	    ushort Month { get; }
	    ushort Day { get; }
	    ushort Hour { get; }
	    ushort Minute { get; }
	    ushort Second { get; }
	    uint   FullSecond { get; }
		ushort TimeMultiplier { get; }
		
		bool DisableTcpNagle { get; set; }
		bool DisableZlibCompression { get; set; }
		uint FloodSize { get; set; }
		bool NoAnswerShuffle { get; set; }
		bool DialogDemandRecheck { get; set; }
		uint FixBoyDefaultExperience { get; set; }
		uint SneakDivider { get; set; }
		uint LevelCap { get; set; }
		bool LevelCapAddExperience { get; set; }
		uint LookNormal { get; set; }
		uint LookMinimum { get; set; }
		uint GlobalMapMaxGroupCount { get; set; }
		uint CritterIdleTick { get; set; }
		uint TurnBasedTick { get; set; }
				
        int DeadHitPoints { get; set; }
		int Breaktime { get; set; }
		uint TimeoutTransfer {get; set; }
		uint TimeoutBattle { get; set; }
		uint ApRegeneration { get; set; }
		
		uint RtApCostCritterWalk { get; set; }
		uint RtApCostCritterRun { get; set; }
		uint RtApCostMoveItemContainer { get; set; }
		uint RtApCostMoveItemInventory { get; set; }
		uint RtApCostPickItem { get; set; }
		uint RtApCostDropItem { get; set; }
		uint RtApCostReloadWeapon { get; set; }
		uint RtApCostPickCritter { get; set; }
		uint RtApCostUseItem { get; set; }
		uint RtApCostUseSkill { get; set; }
		         bool RtAlwaysRun { get; set; }
         uint TbApCostCritterMove { get; set; }
         uint TbApCostMoveItemContainer { get; set; }
         uint TbApCostMoveItemInventory { get; set; }
         uint TbApCostPickItem { get; set; }
         uint TbApCostDropItem { get; set; }
         uint TbApCostReloadWeapon { get; set; }
         uint TbApCostPickCritter { get; set; }
         uint TbApCostUseItem { get; set; }
         uint TbApCostUseSkill { get; set; }
         uint ApCostAimEyes { get; set; }
         uint ApCostAimHead { get; set; }
         uint ApCostAimGroin { get; set; }
         uint ApCostAimTorso { get; set; }
         uint ApCostAimArms { get; set; }
         uint ApCostAimLegs { get; set; }
         bool TbAlwaysRun { get; set; }
         bool RunOnCombat { get; set; }
         bool RunOnTransfer { get; set; }
         uint GlobalMapWidth { get; set; }
         uint GlobalMapHeight { get; set; }
         uint GlobalMapZoneLength { get; set; }
         uint GlobalMapMoveTime { get; set; }
         uint BagRefreshTime { get; set; }
         uint AttackAnimationsMinDist { get; set; }
         uint WisperDist { get; set; }
         uint ShoutDist { get; set; }
         int LookChecks { get; set; }
         uint LookDir0 { get; set; }
         uint LookDir1 { get; set; }
         uint LookDir2 { get; set; }
         uint LookDir3 { get; set; }
         uint LookDir4 { get; set; }
         uint LookSneakDir0 { get; set; }
         uint LookSneakDir1 { get; set; }
         uint LookSneakDir2 { get; set; }
         uint LookSneakDir3 { get; set; }
         uint LookSneakDir4 { get; set; }
         uint LookWeight { get; set; }
         bool CustomItemCost { get; set; }
         uint RegistrationTimeout { get; set; }
         uint AccountPlayTime { get; set; }
         bool LoggingVars { get; set; }
         uint ScriptRunSuspendTimeout { get; set; }
         uint ScriptRunMessageTimeout { get; set; }
         uint TalkDistance { get; set; }
         uint NpcMaxTalkers { get; set; }
         uint MinNameLength { get; set; }
         uint MaxNameLength { get; set; }
         uint DlgTalkMinTime { get; set; }
         uint DlgBarterMinTime { get; set; }
         uint MinimumOfflineTime { get; set; }

         int StartSpecialPoints { get; set; }
         int StartTagSkillPoints { get; set; }
         int SkillMaxValue { get; set; }
         int SkillModAdd2 { get; set; }
         int SkillModAdd3 { get; set; }
         int SkillModAdd4 { get; set; }
         int SkillModAdd5 { get; set; }
         int SkillModAdd6 { get; set; }

         bool AbsoluteOffsets { get; set; }
         uint SkillBegin { get; set; }
         uint SkillEnd { get; set; }
         uint TimeoutBegin { get; set; }
         uint TimeoutEnd { get; set; }
         uint KillBegin { get; set; }
         uint KillEnd { get; set; }
         uint PerkBegin { get; set; }
         uint PerkEnd { get; set; }
         uint AddictionBegin { get; set; }
         uint AddictionEnd { get; set; }
         uint KarmaBegin { get; set; }
         uint KarmaEnd { get; set; }
         uint DamageBegin { get; set; }
         uint DamageEnd { get; set; }
         uint TraitBegin { get; set; }
         uint TraitEnd { get; set; }
         uint ReputationBegin { get; set; }
         uint ReputationEnd { get; set; }

         int ReputationLoved { get; set; }
         int ReputationLiked { get; set; }
         int ReputationAccepted { get; set; }
         int ReputationNeutral { get; set; }
         int ReputationAntipathy { get; set; }
		int ReputationHated { get; set; }	
		
		uint HitAimHead { get; set; }
		uint HitAimEyes { get; set; }
		uint HitAimTorso { get; set; }
		uint HitAimArms { get; set; }
		uint HitAimGroin { get; set; }
		uint HitAimLegs { get; set; }
		bool MapHexagonal { get; }
		//
		// not visible for AS
		//
		int PermanentDeath { get; set; }
    }
	[AttributeUsage(AttributeTargets.Field)]
	class GlobalPropertyAddressAttribute : Attribute
	{
		public GlobalPropertyAddressAttribute (string name)
		{
			Name = name;			
		}
		public string Name { get; set;	}
	}
    public class GlobalProperties : IGlobalProperties
    {
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static IntPtr GetGlobalPropertyAddress(string name);
		
		public static void Init()
		{
			foreach(var field in typeof(GlobalProperties).GetFields (BindingFlags.NonPublic | BindingFlags.Static).Where (f => f.Name.StartsWith("ptr")))
			{
				string property_name;
				var attr = field.GetCustomAttributes(false).FirstOrDefault (a => a.GetType() == typeof(GlobalPropertyAddressAttribute));
				if(attr == null)
					property_name = "__" + field.Name.Substring(3);
				else
					property_name = (attr as GlobalPropertyAddressAttribute).Name;
				field.SetValue(null, GetGlobalPropertyAddress(property_name));
			}
		}
		
#pragma warning disable 649
		static IntPtr ptrYear;
		public ushort Year
		{
			get { return NativeFields.GetUInt16(ptrYear, 0); }
			set { NativeFields.SetUInt16(ptrYear, 0, value); }
		}
	    static IntPtr ptrMonth;
	    public ushort Month
		{
			get { return NativeFields.GetUInt16(ptrMonth, 0); }
			set { NativeFields.SetUInt16(ptrMonth, 0, value); }
		}
		static IntPtr ptrDay;
	    public ushort Day
		{
			get { return NativeFields.GetUInt16(ptrDay, 0); }
			set { NativeFields.SetUInt16(ptrDay, 0, value); }
		}
		static IntPtr ptrHour;
		public ushort Hour
		{
			get { return NativeFields.GetUInt16(ptrHour, 0); }
			set { NativeFields.SetUInt16(ptrHour, 0, value); }
		}
		static IntPtr ptrMinute;
	    public ushort Minute
		{
			get { return NativeFields.GetUInt16(ptrMinute, 0); }
			set { NativeFields.SetUInt16(ptrMinute, 0, value); }
		}
		static IntPtr ptrSecond;
	    public ushort Second
		{
			get { return NativeFields.GetUInt16(ptrSecond, 0); }
			set { NativeFields.SetUInt16(ptrSecond, 0, value); }
		}
		static IntPtr ptrFullSecond;
	    public uint FullSecond
		{
			get { return NativeFields.GetUInt32(ptrFullSecond, 0); }
			set { NativeFields.SetUInt32(ptrFullSecond, 0, value); }
		}	
		static IntPtr ptrTimeMultiplier;
		public ushort TimeMultiplier
		{
			get { return NativeFields.GetUInt16(ptrTimeMultiplier, 0); }
			set { NativeFields.SetUInt16(ptrTimeMultiplier, 0, value); }
		}
		
		static IntPtr ptrDisableTcpNagle;
		public bool DisableTcpNagle
		{
			get { return NativeFields.GetBoolean(ptrDisableTcpNagle, 0); }
			set { NativeFields.SetBoolean(ptrDisableTcpNagle, 0, value); }
		}
		static IntPtr ptrDisableZlibCompression;
		public bool DisableZlibCompression
		{
			get { return NativeFields.GetBoolean(ptrDisableZlibCompression, 0); }
			set { NativeFields.SetBoolean(ptrDisableTcpNagle, 0, value); }
		}
		static IntPtr ptrFloodSize;
		public uint FloodSize
		{
			get { return NativeFields.GetUInt32(ptrFloodSize, 0); }
			set { NativeFields.SetUInt32(ptrFloodSize, 0, value); }
		}
		static IntPtr ptrNoAnswerShuffle;
		public bool NoAnswerShuffle
		{
			get { return NativeFields.GetBoolean(ptrNoAnswerShuffle, 0); }
			set { NativeFields.SetBoolean(ptrNoAnswerShuffle, 0, value); }
		}
		static IntPtr ptrDialogDemandRecheck;
		public bool DialogDemandRecheck
		{
			get { return NativeFields.GetBoolean(ptrDialogDemandRecheck, 0); }
			set { NativeFields.SetBoolean(ptrDialogDemandRecheck, 0, value); }
		}
		static IntPtr ptrFixBoyDefaultExperience;
		public uint FixBoyDefaultExperience
		{
			get { return NativeFields.GetUInt32(ptrFixBoyDefaultExperience, 0); }
			set { NativeFields.SetUInt32(ptrFixBoyDefaultExperience, 0, value); }
		}
		static IntPtr ptrSneakDivider;
		public uint SneakDivider
		{
			get { return NativeFields.GetUInt32(ptrSneakDivider, 0); }
			set { NativeFields.SetUInt32(ptrSneakDivider, 0, value); }
		}
		static IntPtr ptrLevelCap;
		public uint LevelCap
		{
			get { return NativeFields.GetUInt32(ptrLevelCap, 0); }
			set { NativeFields.SetUInt32(ptrLevelCap, 0, value); }
		}
		static IntPtr ptrLevelCapAddExperience;
		public bool LevelCapAddExperience
		{
			get { return NativeFields.GetBoolean(ptrLevelCapAddExperience, 0); }
			set { NativeFields.SetBoolean(ptrLevelCapAddExperience, 0, value); }
		}
		static IntPtr ptrLookNormal;
		public uint LookNormal
		{
			get { return NativeFields.GetUInt32(ptrLookNormal, 0); }
			set { NativeFields.SetUInt32(ptrLookNormal, 0, value); }
		}
		static IntPtr ptrLookMinimum;
		public uint LookMinimum
		{
			get { return NativeFields.GetUInt32(ptrLookMinimum, 0); }
			set { NativeFields.SetUInt32(ptrLookMinimum, 0, value); }
		}
		static IntPtr ptrGlobalMapMaxGroupCount;
		public uint GlobalMapMaxGroupCount
		{
			get { return NativeFields.GetUInt32(ptrGlobalMapMaxGroupCount, 0); }
			set { NativeFields.SetUInt32(ptrGlobalMapMaxGroupCount, 0, value); }
		}
		static IntPtr ptrCritterIdleTick;
		public uint CritterIdleTick
		{
			get { return NativeFields.GetUInt32(ptrCritterIdleTick, 0); }
			set { NativeFields.SetUInt32(ptrCritterIdleTick, 0, value); }
		}
		static IntPtr ptrTurnBasedTick;
		public uint TurnBasedTick
		{
			get { return NativeFields.GetUInt32(ptrTurnBasedTick, 0); }
			set { NativeFields.SetUInt32(ptrTurnBasedTick, 0, value); }
		}

		static IntPtr ptrDeadHitPoints;
		public int DeadHitPoints
		{
			get { return NativeFields.GetInt32(ptrDeadHitPoints, 0); }
			set { NativeFields.SetInt32(ptrDeadHitPoints, 0, value); }
		}
		static IntPtr ptrBreaktime;
		public int Breaktime
		{
			get { return NativeFields.GetInt32(ptrBreaktime, 0); }
			set { NativeFields.SetInt32(ptrBreaktime, 0, value); }
		}
		static IntPtr ptrTimeoutTransfer;
		public uint TimeoutTransfer
		{
			get { return NativeFields.GetUInt32(ptrTimeoutTransfer, 0); }
			set { NativeFields.SetUInt32(ptrTimeoutTransfer, 0, value); }
		}
		static IntPtr ptrTimeoutBattle;
		public uint TimeoutBattle
		{
			get { return NativeFields.GetUInt32(ptrTimeoutBattle, 0); }
			set { NativeFields.SetUInt32(ptrTimeoutBattle, 0, value); }
		}
		static IntPtr ptrApRegeneration;
		public uint ApRegeneration
		{
			get { return NativeFields.GetUInt32(ptrApRegeneration, 0); }
			set { NativeFields.SetUInt32(ptrApRegeneration, 0, value); }
		}
		
		static IntPtr ptrRtApCostCritterWalk;
		public uint RtApCostCritterWalk
		{
			get { return NativeFields.GetUInt32(ptrRtApCostCritterWalk, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostCritterWalk, 0, value); }
		}
		static IntPtr ptrRtApCostCritterRun;
		public uint RtApCostCritterRun
		{
			get { return NativeFields.GetUInt32(ptrRtApCostCritterRun, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostCritterRun, 0, value); }
		}
		static IntPtr ptrRtApCostMoveItemContainer;
		public uint RtApCostMoveItemContainer
		{
			get { return NativeFields.GetUInt32(ptrRtApCostMoveItemContainer, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostMoveItemContainer, 0, value); }
		}
		static IntPtr ptrRtApCostMoveItemInventory;
		public uint RtApCostMoveItemInventory
		{
			get { return NativeFields.GetUInt32(ptrRtApCostMoveItemInventory, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostMoveItemInventory, 0, value); }
		}
		static IntPtr ptrRtApCostPickItem;
		public uint RtApCostPickItem
		{
			get { return NativeFields.GetUInt32(ptrRtApCostPickItem, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostPickItem, 0, value); }
		}
		static IntPtr ptrRtApCostDropItem;
		public uint RtApCostDropItem
		{
			get { return NativeFields.GetUInt32(ptrRtApCostPickItem, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostPickItem, 0, value); }
		}
		static IntPtr ptrRtApCostReloadWeapon;
		public uint RtApCostReloadWeapon
		{
			get { return NativeFields.GetUInt32(ptrRtApCostReloadWeapon, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostReloadWeapon, 0, value); }
		}
		static IntPtr ptrRtApCostPickCritter;
		public uint RtApCostPickCritter
		{
			get { return NativeFields.GetUInt32(ptrRtApCostPickCritter, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostPickCritter, 0, value); }
		}	
		static IntPtr ptrRtApCostUseItem;
		public uint RtApCostUseItem
		{
			get { return NativeFields.GetUInt32(ptrRtApCostUseItem, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostUseItem, 0, value); }
		}
		static IntPtr ptrRtApCostUseSkill;
		public uint RtApCostUseSkill
		{
			get { return NativeFields.GetUInt32(ptrRtApCostUseSkill, 0); }
			set { NativeFields.SetUInt32(ptrRtApCostUseSkill, 0, value); }
		}	
		
		static IntPtr ptrRtAlwaysRun; 
public bool RtAlwaysRun { get { return NativeFields.GetBoolean(ptrRtAlwaysRun, 0); } set { NativeFields.SetBoolean(ptrRtAlwaysRun, 0, value); } }
static IntPtr ptrTbApCostCritterMove; 
public uint TbApCostCritterMove { get { return NativeFields.GetUInt32(ptrTbApCostCritterMove, 0); } set { NativeFields.SetUInt32(ptrTbApCostCritterMove, 0, value); } }
static IntPtr ptrTbApCostMoveItemContainer; 
public uint TbApCostMoveItemContainer { get { return NativeFields.GetUInt32(ptrTbApCostMoveItemContainer, 0); } set { NativeFields.SetUInt32(ptrTbApCostMoveItemContainer, 0, value); } }
static IntPtr ptrTbApCostMoveItemInventory; 
public uint TbApCostMoveItemInventory { get { return NativeFields.GetUInt32(ptrTbApCostMoveItemInventory, 0); } set { NativeFields.SetUInt32(ptrTbApCostMoveItemInventory, 0, value); } }
static IntPtr ptrTbApCostPickItem; 
public uint TbApCostPickItem { get { return NativeFields.GetUInt32(ptrTbApCostPickItem, 0); } set { NativeFields.SetUInt32(ptrTbApCostPickItem, 0, value); } }
static IntPtr ptrTbApCostDropItem; 
public uint TbApCostDropItem { get { return NativeFields.GetUInt32(ptrTbApCostDropItem, 0); } set { NativeFields.SetUInt32(ptrTbApCostDropItem, 0, value); } }
static IntPtr ptrTbApCostReloadWeapon; 
public uint TbApCostReloadWeapon { get { return NativeFields.GetUInt32(ptrTbApCostReloadWeapon, 0); } set { NativeFields.SetUInt32(ptrTbApCostReloadWeapon, 0, value); } }
static IntPtr ptrTbApCostPickCritter; 
public uint TbApCostPickCritter { get { return NativeFields.GetUInt32(ptrTbApCostPickCritter, 0); } set { NativeFields.SetUInt32(ptrTbApCostPickCritter, 0, value); } }
static IntPtr ptrTbApCostUseItem; 
public uint TbApCostUseItem { get { return NativeFields.GetUInt32(ptrTbApCostUseItem, 0); } set { NativeFields.SetUInt32(ptrTbApCostUseItem, 0, value); } }
static IntPtr ptrTbApCostUseSkill; 
public uint TbApCostUseSkill { get { return NativeFields.GetUInt32(ptrTbApCostUseSkill, 0); } set { NativeFields.SetUInt32(ptrTbApCostUseSkill, 0, value); } }
static IntPtr ptrApCostAimEyes; 
public uint ApCostAimEyes { get { return NativeFields.GetUInt32(ptrApCostAimEyes, 0); } set { NativeFields.SetUInt32(ptrApCostAimEyes, 0, value); } }
static IntPtr ptrApCostAimHead; 
public uint ApCostAimHead { get { return NativeFields.GetUInt32(ptrApCostAimHead, 0); } set { NativeFields.SetUInt32(ptrApCostAimHead, 0, value); } }
static IntPtr ptrApCostAimGroin; 
public uint ApCostAimGroin { get { return NativeFields.GetUInt32(ptrApCostAimGroin, 0); } set { NativeFields.SetUInt32(ptrApCostAimGroin, 0, value); } }
static IntPtr ptrApCostAimTorso; 
public uint ApCostAimTorso { get { return NativeFields.GetUInt32(ptrApCostAimTorso, 0); } set { NativeFields.SetUInt32(ptrApCostAimTorso, 0, value); } }
static IntPtr ptrApCostAimArms; 
public uint ApCostAimArms { get { return NativeFields.GetUInt32(ptrApCostAimArms, 0); } set { NativeFields.SetUInt32(ptrApCostAimArms, 0, value); } }
static IntPtr ptrApCostAimLegs; 
public uint ApCostAimLegs { get { return NativeFields.GetUInt32(ptrApCostAimLegs, 0); } set { NativeFields.SetUInt32(ptrApCostAimLegs, 0, value); } }
static IntPtr ptrTbAlwaysRun; 
public bool TbAlwaysRun { get { return NativeFields.GetBoolean(ptrTbAlwaysRun, 0); } set { NativeFields.SetBoolean(ptrTbAlwaysRun, 0, value); } }
static IntPtr ptrRunOnCombat; 
public bool RunOnCombat { get { return NativeFields.GetBoolean(ptrRunOnCombat, 0); } set { NativeFields.SetBoolean(ptrRunOnCombat, 0, value); } }
static IntPtr ptrRunOnTransfer; 
public bool RunOnTransfer { get { return NativeFields.GetBoolean(ptrRunOnTransfer, 0); } set { NativeFields.SetBoolean(ptrRunOnTransfer, 0, value); } }
static IntPtr ptrGlobalMapWidth; 
public uint GlobalMapWidth { get { return NativeFields.GetUInt32(ptrGlobalMapWidth, 0); } set { NativeFields.SetUInt32(ptrGlobalMapWidth, 0, value); } }
static IntPtr ptrGlobalMapHeight; 
public uint GlobalMapHeight { get { return NativeFields.GetUInt32(ptrGlobalMapHeight, 0); } set { NativeFields.SetUInt32(ptrGlobalMapHeight, 0, value); } }
static IntPtr ptrGlobalMapZoneLength; 
public uint GlobalMapZoneLength { get { return NativeFields.GetUInt32(ptrGlobalMapZoneLength, 0); } set { NativeFields.SetUInt32(ptrGlobalMapZoneLength, 0, value); } }
static IntPtr ptrGlobalMapMoveTime; 
public uint GlobalMapMoveTime { get { return NativeFields.GetUInt32(ptrGlobalMapMoveTime, 0); } set { NativeFields.SetUInt32(ptrGlobalMapMoveTime, 0, value); } }
static IntPtr ptrBagRefreshTime; 
public uint BagRefreshTime { get { return NativeFields.GetUInt32(ptrBagRefreshTime, 0); } set { NativeFields.SetUInt32(ptrBagRefreshTime, 0, value); } }
static IntPtr ptrAttackAnimationsMinDist; 
public uint AttackAnimationsMinDist { get { return NativeFields.GetUInt32(ptrAttackAnimationsMinDist, 0); } set { NativeFields.SetUInt32(ptrAttackAnimationsMinDist, 0, value); } }
static IntPtr ptrWisperDist; 
public uint WisperDist { get { return NativeFields.GetUInt32(ptrWisperDist, 0); } set { NativeFields.SetUInt32(ptrWisperDist, 0, value); } }
static IntPtr ptrShoutDist; 
public uint ShoutDist { get { return NativeFields.GetUInt32(ptrShoutDist, 0); } set { NativeFields.SetUInt32(ptrShoutDist, 0, value); } }
static IntPtr ptrLookChecks; 
public int LookChecks { get { return NativeFields.GetInt32(ptrLookChecks, 0); } set { NativeFields.SetInt32(ptrLookChecks, 0, value); } }
static IntPtr ptrLookDir0; 
public uint LookDir0 { get { return NativeFields.GetUInt32(ptrLookDir0, 0); } set { NativeFields.SetUInt32(ptrLookDir0, 0, value); } }
static IntPtr ptrLookDir1; 
public uint LookDir1 { get { return NativeFields.GetUInt32(ptrLookDir1, 0); } set { NativeFields.SetUInt32(ptrLookDir1, 0, value); } }
static IntPtr ptrLookDir2; 
public uint LookDir2 { get { return NativeFields.GetUInt32(ptrLookDir2, 0); } set { NativeFields.SetUInt32(ptrLookDir2, 0, value); } }
static IntPtr ptrLookDir3; 
public uint LookDir3 { get { return NativeFields.GetUInt32(ptrLookDir3, 0); } set { NativeFields.SetUInt32(ptrLookDir3, 0, value); } }
static IntPtr ptrLookDir4; 
public uint LookDir4 { get { return NativeFields.GetUInt32(ptrLookDir4, 0); } set { NativeFields.SetUInt32(ptrLookDir4, 0, value); } }
static IntPtr ptrLookSneakDir0; 
public uint LookSneakDir0 { get { return NativeFields.GetUInt32(ptrLookSneakDir0, 0); } set { NativeFields.SetUInt32(ptrLookSneakDir0, 0, value); } }
static IntPtr ptrLookSneakDir1; 
public uint LookSneakDir1 { get { return NativeFields.GetUInt32(ptrLookSneakDir1, 0); } set { NativeFields.SetUInt32(ptrLookSneakDir1, 0, value); } }
static IntPtr ptrLookSneakDir2; 
public uint LookSneakDir2 { get { return NativeFields.GetUInt32(ptrLookSneakDir2, 0); } set { NativeFields.SetUInt32(ptrLookSneakDir2, 0, value); } }
static IntPtr ptrLookSneakDir3; 
public uint LookSneakDir3 { get { return NativeFields.GetUInt32(ptrLookSneakDir3, 0); } set { NativeFields.SetUInt32(ptrLookSneakDir3, 0, value); } }
static IntPtr ptrLookSneakDir4; 
public uint LookSneakDir4 { get { return NativeFields.GetUInt32(ptrLookSneakDir4, 0); } set { NativeFields.SetUInt32(ptrLookSneakDir4, 0, value); } }
static IntPtr ptrLookWeight; 
public uint LookWeight { get { return NativeFields.GetUInt32(ptrLookWeight, 0); } set { NativeFields.SetUInt32(ptrLookWeight, 0, value); } }
static IntPtr ptrCustomItemCost; 
public bool CustomItemCost { get { return NativeFields.GetBoolean(ptrCustomItemCost, 0); } set { NativeFields.SetBoolean(ptrCustomItemCost, 0, value); } }
static IntPtr ptrRegistrationTimeout; 
public uint RegistrationTimeout { get { return NativeFields.GetUInt32(ptrRegistrationTimeout, 0); } set { NativeFields.SetUInt32(ptrRegistrationTimeout, 0, value); } }
static IntPtr ptrAccountPlayTime; 
public uint AccountPlayTime { get { return NativeFields.GetUInt32(ptrAccountPlayTime, 0); } set { NativeFields.SetUInt32(ptrAccountPlayTime, 0, value); } }
static IntPtr ptrLoggingVars; 
public bool LoggingVars { get { return NativeFields.GetBoolean(ptrLoggingVars, 0); } set { NativeFields.SetBoolean(ptrLoggingVars, 0, value); } }
static IntPtr ptrScriptRunSuspendTimeout; 
public uint ScriptRunSuspendTimeout { get { return NativeFields.GetUInt32(ptrScriptRunSuspendTimeout, 0); } set { NativeFields.SetUInt32(ptrScriptRunSuspendTimeout, 0, value); } }
static IntPtr ptrScriptRunMessageTimeout; 
public uint ScriptRunMessageTimeout { get { return NativeFields.GetUInt32(ptrScriptRunMessageTimeout, 0); } set { NativeFields.SetUInt32(ptrScriptRunMessageTimeout, 0, value); } }
static IntPtr ptrTalkDistance; 
public uint TalkDistance { get { return NativeFields.GetUInt32(ptrTalkDistance, 0); } set { NativeFields.SetUInt32(ptrTalkDistance, 0, value); } }
static IntPtr ptrNpcMaxTalkers; 
public uint NpcMaxTalkers { get { return NativeFields.GetUInt32(ptrNpcMaxTalkers, 0); } set { NativeFields.SetUInt32(ptrNpcMaxTalkers, 0, value); } }
static IntPtr ptrMinNameLength; 
public uint MinNameLength { get { return NativeFields.GetUInt32(ptrMinNameLength, 0); } set { NativeFields.SetUInt32(ptrMinNameLength, 0, value); } }
static IntPtr ptrMaxNameLength; 
public uint MaxNameLength { get { return NativeFields.GetUInt32(ptrMaxNameLength, 0); } set { NativeFields.SetUInt32(ptrMaxNameLength, 0, value); } }
static IntPtr ptrDlgTalkMinTime; 
public uint DlgTalkMinTime { get { return NativeFields.GetUInt32(ptrDlgTalkMinTime, 0); } set { NativeFields.SetUInt32(ptrDlgTalkMinTime, 0, value); } }
static IntPtr ptrDlgBarterMinTime; 
public uint DlgBarterMinTime { get { return NativeFields.GetUInt32(ptrDlgBarterMinTime, 0); } set { NativeFields.SetUInt32(ptrDlgBarterMinTime, 0, value); } }
static IntPtr ptrMinimumOfflineTime; 
public uint MinimumOfflineTime { get { return NativeFields.GetUInt32(ptrMinimumOfflineTime, 0); } set { NativeFields.SetUInt32(ptrMinimumOfflineTime, 0, value); } }

static IntPtr ptrStartSpecialPoints; 
public int StartSpecialPoints { get { return NativeFields.GetInt32(ptrStartSpecialPoints, 0); } set { NativeFields.SetInt32(ptrStartSpecialPoints, 0, value); } }
static IntPtr ptrStartTagSkillPoints; 
public int StartTagSkillPoints { get { return NativeFields.GetInt32(ptrStartTagSkillPoints, 0); } set { NativeFields.SetInt32(ptrStartTagSkillPoints, 0, value); } }
static IntPtr ptrSkillMaxValue; 
public int SkillMaxValue { get { return NativeFields.GetInt32(ptrSkillMaxValue, 0); } set { NativeFields.SetInt32(ptrSkillMaxValue, 0, value); } }
static IntPtr ptrSkillModAdd2; 
public int SkillModAdd2 { get { return NativeFields.GetInt32(ptrSkillModAdd2, 0); } set { NativeFields.SetInt32(ptrSkillModAdd2, 0, value); } }
static IntPtr ptrSkillModAdd3; 
public int SkillModAdd3 { get { return NativeFields.GetInt32(ptrSkillModAdd3, 0); } set { NativeFields.SetInt32(ptrSkillModAdd3, 0, value); } }
static IntPtr ptrSkillModAdd4; 
public int SkillModAdd4 { get { return NativeFields.GetInt32(ptrSkillModAdd4, 0); } set { NativeFields.SetInt32(ptrSkillModAdd4, 0, value); } }
static IntPtr ptrSkillModAdd5; 
public int SkillModAdd5 { get { return NativeFields.GetInt32(ptrSkillModAdd5, 0); } set { NativeFields.SetInt32(ptrSkillModAdd5, 0, value); } }
static IntPtr ptrSkillModAdd6; 
public int SkillModAdd6 { get { return NativeFields.GetInt32(ptrSkillModAdd6, 0); } set { NativeFields.SetInt32(ptrSkillModAdd6, 0, value); } }

static IntPtr ptrAbsoluteOffsets; 
public bool AbsoluteOffsets { get { return NativeFields.GetBoolean(ptrAbsoluteOffsets, 0); } set { NativeFields.SetBoolean(ptrAbsoluteOffsets, 0, value); } }
static IntPtr ptrSkillBegin; 
public uint SkillBegin { get { return NativeFields.GetUInt32(ptrSkillBegin, 0); } set { NativeFields.SetUInt32(ptrSkillBegin, 0, value); } }
static IntPtr ptrSkillEnd; 
public uint SkillEnd { get { return NativeFields.GetUInt32(ptrSkillEnd, 0); } set { NativeFields.SetUInt32(ptrSkillEnd, 0, value); } }
static IntPtr ptrTimeoutBegin; 
public uint TimeoutBegin { get { return NativeFields.GetUInt32(ptrTimeoutBegin, 0); } set { NativeFields.SetUInt32(ptrTimeoutBegin, 0, value); } }
static IntPtr ptrTimeoutEnd; 
public uint TimeoutEnd { get { return NativeFields.GetUInt32(ptrTimeoutEnd, 0); } set { NativeFields.SetUInt32(ptrTimeoutEnd, 0, value); } }
static IntPtr ptrKillBegin; 
public uint KillBegin { get { return NativeFields.GetUInt32(ptrKillBegin, 0); } set { NativeFields.SetUInt32(ptrKillBegin, 0, value); } }
static IntPtr ptrKillEnd; 
public uint KillEnd { get { return NativeFields.GetUInt32(ptrKillEnd, 0); } set { NativeFields.SetUInt32(ptrKillEnd, 0, value); } }
static IntPtr ptrPerkBegin; 
public uint PerkBegin { get { return NativeFields.GetUInt32(ptrPerkBegin, 0); } set { NativeFields.SetUInt32(ptrPerkBegin, 0, value); } }
static IntPtr ptrPerkEnd; 
public uint PerkEnd { get { return NativeFields.GetUInt32(ptrPerkEnd, 0); } set { NativeFields.SetUInt32(ptrPerkEnd, 0, value); } }
static IntPtr ptrAddictionBegin; 
public uint AddictionBegin { get { return NativeFields.GetUInt32(ptrAddictionBegin, 0); } set { NativeFields.SetUInt32(ptrAddictionBegin, 0, value); } }
static IntPtr ptrAddictionEnd; 
public uint AddictionEnd { get { return NativeFields.GetUInt32(ptrAddictionEnd, 0); } set { NativeFields.SetUInt32(ptrAddictionEnd, 0, value); } }
static IntPtr ptrKarmaBegin; 
public uint KarmaBegin { get { return NativeFields.GetUInt32(ptrKarmaBegin, 0); } set { NativeFields.SetUInt32(ptrKarmaBegin, 0, value); } }
static IntPtr ptrKarmaEnd; 
public uint KarmaEnd { get { return NativeFields.GetUInt32(ptrKarmaEnd, 0); } set { NativeFields.SetUInt32(ptrKarmaEnd, 0, value); } }
static IntPtr ptrDamageBegin; 
public uint DamageBegin { get { return NativeFields.GetUInt32(ptrDamageBegin, 0); } set { NativeFields.SetUInt32(ptrDamageBegin, 0, value); } }
static IntPtr ptrDamageEnd; 
public uint DamageEnd { get { return NativeFields.GetUInt32(ptrDamageEnd, 0); } set { NativeFields.SetUInt32(ptrDamageEnd, 0, value); } }
static IntPtr ptrTraitBegin; 
public uint TraitBegin { get { return NativeFields.GetUInt32(ptrTraitBegin, 0); } set { NativeFields.SetUInt32(ptrTraitBegin, 0, value); } }
static IntPtr ptrTraitEnd; 
public uint TraitEnd { get { return NativeFields.GetUInt32(ptrTraitEnd, 0); } set { NativeFields.SetUInt32(ptrTraitEnd, 0, value); } }
static IntPtr ptrReputationBegin; 
public uint ReputationBegin { get { return NativeFields.GetUInt32(ptrReputationBegin, 0); } set { NativeFields.SetUInt32(ptrReputationBegin, 0, value); } }
static IntPtr ptrReputationEnd; 
public uint ReputationEnd { get { return NativeFields.GetUInt32(ptrReputationEnd, 0); } set { NativeFields.SetUInt32(ptrReputationEnd, 0, value); } }

static IntPtr ptrReputationLoved; 
public int ReputationLoved { get { return NativeFields.GetInt32(ptrReputationLoved, 0); } set { NativeFields.SetInt32(ptrReputationLoved, 0, value); } }
static IntPtr ptrReputationLiked; 
public int ReputationLiked { get { return NativeFields.GetInt32(ptrReputationLiked, 0); } set { NativeFields.SetInt32(ptrReputationLiked, 0, value); } }
static IntPtr ptrReputationAccepted; 
public int ReputationAccepted { get { return NativeFields.GetInt32(ptrReputationAccepted, 0); } set { NativeFields.SetInt32(ptrReputationAccepted, 0, value); } }
static IntPtr ptrReputationNeutral; 
public int ReputationNeutral { get { return NativeFields.GetInt32(ptrReputationNeutral, 0); } set { NativeFields.SetInt32(ptrReputationNeutral, 0, value); } }
static IntPtr ptrReputationAntipathy; 
public int ReputationAntipathy { get { return NativeFields.GetInt32(ptrReputationAntipathy, 0); } set { NativeFields.SetInt32(ptrReputationAntipathy, 0, value); } }
static IntPtr ptrReputationHated; 
public int ReputationHated { get { return NativeFields.GetInt32(ptrReputationHated, 0); } set { NativeFields.SetInt32(ptrReputationHated, 0, value); } }
				
		static IntPtr ptrHitAimHead;
		public uint HitAimHead 
		{ 
			get { return NativeFields.GetUInt32(ptrHitAimHead, 0); } 
			set { NativeFields.SetUInt32(ptrHitAimHead, 0, value); }
		}
		static IntPtr ptrHitAimEyes;
		public uint HitAimEyes 
		{ 
			get { return NativeFields.GetUInt32(ptrHitAimEyes, 0); } 
			set { NativeFields.SetUInt32(ptrHitAimEyes, 0, value); }
		}
		static IntPtr ptrHitAimTorso;
		public uint HitAimTorso 
		{ 
			get { return NativeFields.GetUInt32(ptrHitAimTorso, 0); } 
			set { NativeFields.SetUInt32(ptrHitAimTorso, 0, value); }
		}
		static IntPtr ptrHitAimArms;
		public uint HitAimArms 
		{ 
			get { return NativeFields.GetUInt32(ptrHitAimArms, 0); } 
			set { NativeFields.SetUInt32(ptrHitAimArms, 0, value); }
		}
		static IntPtr ptrHitAimGroin;
		public uint HitAimGroin 
		{ 
			get { return NativeFields.GetUInt32(ptrHitAimGroin, 0); } 
			set { NativeFields.SetUInt32(ptrHitAimGroin, 0, value); }
		}
		static IntPtr ptrHitAimLegs;
		public uint HitAimLegs 
		{ 
			get { return NativeFields.GetUInt32(ptrHitAimLegs, 0); } 
			set { NativeFields.SetUInt32(ptrHitAimLegs, 0, value); }
		}
		
		static IntPtr ptrMapHexagonal;
		public bool MapHexagonal
		{
			get { return NativeFields.GetBoolean(ptrMapHexagonal, 0); }
		}
#pragma warning restore 649	
		public int PermanentDeath {	get; set; }
    }
}

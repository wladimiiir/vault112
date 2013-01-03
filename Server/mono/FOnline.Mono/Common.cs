using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public static class TextMsg
    {
        public const ushort Text = 0;
        public const ushort Dlg = 1;
        public const ushort Item = 2;
        public const ushort Game = 3;
        public const ushort GM = 4;
        public const ushort Combat = 5;
        public const ushort Quest = 6;
        public const ushort Holo = 7;
        public const ushort Craft = 8;
        public const ushort Internal = 9;
        public static uint DlgStr(uint dialog_id, uint str_num) // TODO: good place for this?
        {
            return 1000000000 + dialog_id * 100000 + str_num;
        }
    }
    public enum Say : byte
    {
        Norm = 1,
        NormOnHead,
        Shout,
        ShoutOnHead,
        Emote,
        EmoteOnHead,
        Whisp,
        WhispOnHead,
        Social,
        Radio,
        NetMsg,
        Dialog,
        Append,
        EncounterAny,   // Activate dialog box on global map, using before encounter
        EncounterRT,    // Activate dialog box on global map, using before encounter
        EncounterTB,    // Activate dialog box on global map, using before encounter
        FixResult,
        DialogBoxText,
        DialogBoxButton0 = 19,
        DialogBoxButton1,
        DialogBoxButton2,
        DialogBoxButton3,
        DialogBoxButton4,
        DialogBoxButton5,
        DialogBoxButton6,
        DialogBoxButton7,
        DialogBoxButton8,
        DialogBoxButton9,
        DialogBoxButton10,
        DialogBoxButton11,
        DialogBoxButton12,
        DialogBoxButton13,
        DialogBoxButton14,
        DialogBoxButton15,
        DialogBoxButton16,
        DialogBoxButton17,
        DialogBoxButton18,
        DialogBoxButton19,
        Title = 39,
        Text,
        FlashWindow // Only flash window in tray and beep
    }
            // Show screen modes
// Ouput: it is 'uint param' in Critter::ShowScreen.
// Input: I - integer value 'uint answerI', S - string value 'string& answerS' in 'answer_' function.
    public enum Screen
    {
        Close,
        Timer,
        Bag = 4
/*
#define SCREEN_CLOSE                (0)  // Close top window.
#define SCREEN_TIMER                (1)  // Timer box. Output: picture index in INVEN.LST. Input I: time in game minutes (1..599).
#define SCREEN_DIALOGBOX            (2)  // Dialog box. Output: buttons count - 0..20 (exit button added automatically). Input I: Choosed button - 0..19.
#define SCREEN_SKILLBOX             (3)  // Skill box. Input I: selected skill.
#define SCREEN_BAG                  (4)  // Bag box. Input I: id of selected item.
#define SCREEN_SAY                  (5)  // Say box. Output: all symbols - 0 or only numbers - any other number. Input S: typed string.
#define SCREEN_ELEVATOR             (6)  // Elevator. Output: look ELEVATOR_* macro. Input I: Choosed level button.
#define SCREEN_INVENTORY            (7)  // Inventory.
#define SCREEN_CHARACTER            (8)  // Character.
#define SCREEN_FIXBOY               (9)  // Fix-boy.
#define SCREEN_PIPBOY               (10) // Pip-boy.
#define SCREEN_MINIMAP              (11) // Mini-map.
 * */
    }
    public enum Direction : byte
    {
        NorthEast = 0,
        East = 1,
        SouthEast = 2,
        SouthWest = 3,
        West = 4,
        NorthWest = 5
    }
    public enum ItemSlot : byte
    {
        Inv = 0,
        Hand1 = 1,
        Hand2 = 2,
        Armor = 3,
        Head = 4,
        Ground = 255
    }
    // Global map events
    public enum GlobalProcessType : int
    {
        Move = 0,
        Enter = 1,
        StartFast = 2,
        Start = 3,
        SetMove = 4,
        Stopped = 5,
        NpcIdle = 6,
        Kick = 7
    }
    // Global map walk types
    public enum GlobalMapWalk
    {
        Ground = 0,
        Fly = 1,
        Water = 2
    }
    
    public enum TransferType : uint
    {
        Close = 0,
        HexContUp = 1,
        HexContDown = 2,
        SelfCont = 3,
        CritLoot = 4,
        CritSteal = 5,
        CritBarter = 6,
        FarCont = 7,
        FarCrit = 8
    }

    public static class Scores
    {
        public const int EVIL_OF_HOUR = 0;
        public const int HERO_OF_HOUR = 1;
        public const int KARMA_ON_HOUR = 2;
        public const int SPEAKER = 3; // Hardcoded
        public const int TRADER = 4; // Hardcoded
        public const int ZOMBY = 5;
        public const int PATY = 6;
        public const int MANIAC = 7;
        public const int SCAUT = 8;
        public const int DOCTOR = 9;
        public const int SHOOTER = 10;
        public const int MELEE = 11;
        public const int UNARMED = 12;
        public const int THIEF = 13;
        public const int DRIVER = 14;
        public const int KILLER = 15;
        public const int SNIPER = 16;
        public const int ADVENTURER = 17;
        public const int CRACKER = 18;
        public const int UNARMED_DAMAGE = 19;
        public const int RITCH = 20;
        public const int CHOSEN_ONE = 21;
        public const int SIERRA_CUR = 40;
        public const int MARIPOSA_CUR = 41;
        public const int CATHEDRAL_CUR = 42;
        public const int SIERRA_BEST = 43;
        public const int MARIPOSA_BEST = 44;
        public const int CATHEDRAL_BEST = 45;
        public const int SIERRA_ORG = 46;
        public const int MARIPOSA_ORG = 47;
        public const int CATHEDRAL_ORG = 48;
        public const int BASE_BEST_ORG = 49;
    }
    // Critter find types
    // Combine groups with | operator
    public enum Find
    {
        // First group
        Life = 0x01,
        KO = 0x02,
        Dead = 0x04,
        LifeAndKO = 0x03,
        LifeAndDead = 0x05,
        KOAndDead = 0x06,
        All = 0x0f,
        // Second group
        OnlyPlayers = 0x10,
        OnlyNpc = 0x20
    }
    public enum CombatMode
    {
        Any = 0,
        RealTime = 1,
        TurnBased = 2
    }
    public enum AccessType
    {
        Client = 0,
        Tester = 1,
        Moder = 2,
        Admin = 3
    }
	// Hit locations
	public enum HitLocation : byte
	{
		None = 0,
		Head = 1,
		LeftArm = 2,
		RightArm = 3,
		Torso = 4,
		RightLeg = 5,
		LeftLeg = 6,
		Eyes = 7,
		Groin = 8,
		Uncalled = 9
	}
	public enum Damage
	{
		Uncalled = 0,
		Normal = 1,
		Laser = 2,
		Fire = 3,
		Plasma = 4,
		Electr = 5,
		EMP = 6,
		Explode = 7
	}
	
    public static class Extensions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        //#define _CritFingersDoorId #(critterId) (0x80000000|(critterId))
        //#define _CritEyesDoorId #(critterId) (0x40000000|(critterId))
        public static uint FingersDoorId(this uint crId)
        {
            return crId | 0x80000000;
        }
        public static uint EyesDoorId(this uint crId)
        {
            return crId | 0x40000000;
        }
    }
	public static class Flag
	{
		public static bool Check(uint val, uint flag)
		{
			return (val & flag) != 0;
		}
		public static void Set(ref uint val, uint flag)
		{
			val |= flag;
		}
		public static void Unset(ref uint val, uint flag)
		{
			val = val & ~flag;
		}
	}
}

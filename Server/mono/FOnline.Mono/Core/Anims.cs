using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    // Anim types
    public enum AnimType
    {
        Fallout = 0,
        _3D = 1,
        Tactics = 2,
        Arcanum = 3
    }
    // Anim loading flags
    [Flags]
    public enum AnimFlag
    {
        FirstFrame = 0x01,
        LastFrame = 0x02
    }
    // Anim1 Weapon / Flags
    public static class Anim1
    {
        // 0 1 2 3 4 5 6 7
        // |<  Weapon   >|

        // Manage constants
        public const uint WeaponMask = 0x000000FF;
        public const uint FlagsMask = 0xFFFFFF00;
        public const int FlagsBits = 8;

        // Flags
        // Tactics specific
        // 8        9       10 1 2 3 4 5 6 7 8 9 20  1 2  3 4 5 6  7 8 9 30  1
        // | Crouch | Prone                       | Skin  | | Hair | | Armor |
        public const uint Crouch = 0x0100;
        public const uint Prone = 0x0200;
        public static uint ColorSkin(uint index) { return (((index) & 0xF) << 20); } // 0..15, colors see in colorOffsets animation.fos
        public static uint ColorHair(uint index) { return (((index) & 0xF) << 24); } // 0..15, colors see in colorOffsets animation.fos
        public static uint ColorArmor(uint index) { return (((index) & 0xF) << 28); } // 0..15, colors see in colorOffsets animation.fos
        // Arcanum specific
        // 8 9 10 1 2        3 4 5 6     7     8 9 20 1 2 3 4 5 6 7 8 9 30 1
        //          | Shield       |< Palette >|
        public const uint Shield = 0x1000;
        public static uint Palette(uint num) { return (((num) & 3) << (Anim1.FlagsBits + 8)); }
        // Weapons
        public const uint Unarmed = 1; // Hardcoded
        public const uint Knife = 4;
        public const uint Club = 5;
        public const uint Hammer = 6;
        public const uint Spear = 7;
        public const uint Pistol = 8;
        public const uint SMG = 9;
        public const uint Shotgun = 10;
        public const uint HeavyRifle = 11;
        public const uint Minigun = 12;
        public const uint RocketLauncher = 13;
        public const uint Flamer = 14;
        public const uint Rifle = 15;
        public const uint Sword = 16;
        public const uint LongSword = 17;
        public const uint Axe = 18;
        public const uint Bow = 19;
    }
    // Anim2 Actions
    public static class Anim2
    {
        // Manage constants
        public const uint DeadBegin = 100;
        public const uint DeadEnd = 120;
        // Animations
        public const uint Idle = 1;  // Hardcoded
        public const uint IdleStunned = 2;
        public const uint Walk = 3;  // Hardcoded
        public const uint Limp = 4;  // Hardcoded
        public const uint Run = 5;  // Hardcoded
        public const uint PanicRun = 6;  // Hardcoded
        public const uint SneakWalk = 7;  // Hardcoded
        public const uint SneakRun = 8;  // Hardcoded
        public const uint Stand = 10;
        public const uint Crouch = 11;
        public const uint Prone = 12;
        public const uint ShowWeapon = 20;
        public const uint HideWeapon = 21;
        public const uint PrepareWeapon = 22;
        public const uint TurnoffWeapon = 23;
        public const uint Fidget = 24;
        public const uint Climbing = 26;
        public const uint Pickup = 27;
        public const uint Use = 28;
        public const uint SwitchItems = 29;
        public const uint Reload = 30;
        public const uint Repair = 31;
        public const uint Loot = 35;
        public const uint Steal = 36;
        public const uint Push = 37;
        public const uint BeginCombat = 40;
        public const uint IdleCombat = 41;
        public const uint EndCombat = 42;
        public const uint PunchRight = 43;
        public const uint PunchLeft = 44;
        public const uint PunchCombo = 45;
        public const uint KickHi = 46;
        public const uint KickLo = 47;
        public const uint KickCombo = 48;
        public const uint Thrust1H = 49;
        public const uint Thrust2H = 50;
        public const uint Swing1H = 51;
        public const uint Swing2H = 52;
        public const uint Throw = 53;
        public const uint Single = 54;
        public const uint Burst = 55;
        public const uint Sweep = 56;
        public const uint Butt = 57;
        public const uint Flame = 58;
        public const uint NoRecoil = 59;
        public const uint DodgeFront = 70;
        public const uint DodgeBack = 71;
        public const uint DamageFront = 72;
        public const uint DamangeBack = 73;
        public const uint DamageMulFront = 74;
        public const uint DamageMulBack = 75;
        public const uint WalkDamageFront = 76;
        public const uint WalkDamageBack = 77;
        public const uint LimpDamageFront = 78;
        public const uint LimpDamageBack = 79;
        public const uint RunDamageFront = 80;
        public const uint RunDamageBack = 81;
        public const uint KnockFront = 82;
        public const uint KnockBack = 83;
        public const uint LaydownFront = 84;
        public const uint LaydownBack = 85;
        public const uint IdleProneFront = 86; // Hardcoded
        public const uint IdleProneBack = 87; // Hardcoded
        public const uint StandupFront = 88;
        public const uint Standup_Back = 89;
        public const uint DamageProneFront = 90;
        public const uint DamageProneBack = 91;
        public const uint DamageMulProneFront = 92;
        public const uint DamageMulProneBack = 93;
        public const uint TwitchProneFront = 94;
        public const uint TwitchProneBack = 95;
        public const uint DeadProneFront = 100;
        public const uint DeadProneBack = 101;
        public const uint DeadFront = 102; // Hardcoded
        public const uint DeadBack = 103; // Hardcoded
        public const uint DeadBloodySingle = 110;
        public const uint DeadBloodyBurst = 111;
        public const uint DeadBurst = 112;
        public const uint DeadPulse = 113;
        public const uint DeadPulseDust = 114;
        public const uint DeadLaser = 115;
        public const uint DeadFused = 116;
        public const uint DeadExplode = 117;
        public const uint DeadBurn = 118;
        public const uint DeadBurnRun = 119;
    }
}

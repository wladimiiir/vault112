using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public static class Stats
    {
        public const uint Strength = 0; // Used in engine +++
        public const uint Perception = 1;
        public const uint Endurance = 2;
        public const uint Charisma = 3;
        public const uint Intellect = 4; // for backward compatibility
        public const uint Intelligence = 4;
        public const uint Agility = 5;
        public const uint Luck = 6;
        public const uint MaxLife = 7;
        public const uint ActionPoints = 8;
        public const uint ArmorClass = 9;
        public const uint MeleeDamage = 10;
        public const uint CarryWeight = 11;
        public const uint Sequence = 12;
        public const uint HealingRate = 13;
		public const uint CriticalChance = 14;
        public const uint MaxCritical = 15;
        public const uint NormalAbsorb = 16;
        public const uint LaserAbsorb = 17;
        public const uint FireAbsorb = 18;
        public const uint PlasmaAbsorb = 19;
        public const uint ElectroAbsorb = 20;
        public const uint EmpAbsorb = 21;
        public const uint ExplodeAbsorb = 22;
        public const uint NormalResist = 23;
        public const uint LaserResist = 24;
        public const uint FireResist = 25;
        public const uint PlasmaResist = 26;
        public const uint ElectroResist = 27;
        public const uint EmpResist = 28;
        public const uint ExplodeResist = 29;
        public const uint RadiationResistance = 30;
        public const uint PosionResistance = 31;
        public const uint StrengthExt = 32;
        public const uint PerceptionExt = 33;
        public const uint EnduranceExt = 34;
        public const uint CharismaExt = 35;
        public const uint IntellectExt = 36; // for backward compatibility
        public const uint IntelligenceExt = 36;
        public const uint AgilityExt = 37;
        public const uint LuckExt = 38;
        public const uint MaxLifeExt = 39;
        public const uint ActionPointsExt = 40;
        public const uint ArmorClassExt = 41;
        public const uint MeleeDamageExt = 42;
        public const uint CarryWeightExt = 43;
        public const uint SequenceExt = 44;
        public const uint HealingRateExt = 45;
        public const uint CriticalChanceExt = 46;
        public const uint MaxCriticalExt = 47;
        public const uint NormalAbsorbExt = 48;
        public const uint LaserAbsorbExt = 49;
        public const uint FireAbsorbExt = 50;
        public const uint PlasmaAbsorbExt = 51;
        public const uint ElectroAbsorbExt = 52;
        public const uint EmpAbsorbExt = 53;
        public const uint ExplodeAbsorbExt = 54;
        public const uint NormalResistExt = 55;
        public const uint LaserResistExt = 56;
        public const uint FireResistExt = 57;
        public const uint PlasmaResistExt = 58;
        public const uint ElectroResistExt = 59;
        public const uint EmpResistExt = 60;
        public const uint ExplodeResistExt = 61;
        public const uint RadiationResistanceExt = 62;
        public const uint PoisonResistanceExt = 63; // Used in engine ---
        public const uint Toxic = 64;
        public const uint Radioactive = 65;
        public const uint KillExperience = 66;
        public const uint BodyType = 67;
        public const uint LocomotionType = 68;
        public const uint DamageType = 69;
        public const uint Age = 70; // Used in engine
        public const uint Gender = 71; // Used in engine
        public const uint CurrentHP = 72; // Used in engine    
        public const uint PoisoningLevel = 73; // Used in engine
        public const uint RadiationLevel = 74; // Used in engine
        public const uint CurrentAP = 75; // Used in engine
        public const uint Experience = 76; // Used in engine
        public const uint Level = 77; // Used in engine
        public const uint UnspentSkillPoints = 78; // Used in engine
        public const uint UnspentPerks = 79; // Used in engine
        public const uint Karma = 80; // Used in engine
        public const uint FollowCrit = 81; // Used in engine
        public const uint ReplicationMoney = 82; // Used in engine
        public const uint ReplicationCount = 83; // Used in engine
        public const uint ReplicationTime = 84; // Used in engine
        public const uint ReplicationCost = 85; // Used in engine
        public const uint TurnBasedAC = 86; // Used in engine
        public const uint MaxMoveAP = 87; // Used in engine
        public const uint MoveAP = 88; // Used in engine
        public const uint NpcRole = 89; // Used in engine
        public const uint Var0 = 90;
        public const uint Var1 = 91;
        public const uint Var2 = 92;
        public const uint Var3 = 93;
        public const uint Var4 = 94;
        public const uint Var5 = 95;
        public const uint Var6 = 96;
        public const uint Var7 = 97;
        public const uint Var8 = 98;
        public const uint Var9 = 99;
        public const uint PlayerKarma = 100;
        public const uint BonusLook = 101; // Used in engine
        public const uint HandsItemAndMode = 102; // Used in engine
        public const uint FreeBarterPlayer = 103; // Used in engine
        public const uint DialogId = 104; // Used in engine
        public const uint AiId = 105; // Used in engine
        public const uint TeamId = 106; // Used in engine
        public const uint BagId = 107; // Used in engine
        public const uint LastStealCrId = 108;
        public const uint StealCount = 109;
        public const uint LastWeaponId = 110; // Used in engine
        public const uint LastWeaponUse = 111;
        public const uint BaseCrType = 112; // Used in engine
        public const uint DeadBlockerId = 113;
        public const uint CurrentArmorPerk = 114;
        public const uint TalkDistance = 115; // Used in engine; if zero then Global.TalkDistance is used
        public const uint ScaleFactor = 116; // Used in engine
        public const uint WalkTime = 117; // Hardcoded
        public const uint RunTime = 118; // Hardcoded
        public const uint MaxTalkers = 119; // Hardcoded
        // 120..129 reserved for hardcoded values
        public const uint NextCrType = 130;
        public const uint NextReplicationMap = 131;
        public const uint NextReplicationEntire = 132;
        // 133..149
        //#ifdef PLAYERS_3D
        // Initial values of animation 3d layers
        //# define ST_ANIM3D_LAYERS                        = 150; // Hardcoded
        // 150..179 reserved for 30 layers
        //#endif
        // 180..199
    }
	public static class Timeouts
    {
        public const uint Begin = 230;
        
        public const uint FirstAid = 230;
        public const uint Doctor = 231;
        public const uint Repair = 232;
        public const uint Science = 233;
        public const uint Lockpick = 234;
        public const uint Steal = 235;
        public const uint Weakened = 236;
        public const uint Fixboy = 237;
        public const uint Battle = 238;
        public const uint Transfer = 239;
        public const uint RemoveFromGame = 240;
        public const uint Replication = 241;
        public const uint KarmaVoting = 242;
        public const uint Gathering = 243;
        public const uint Sneak = 244;
        public const uint Healing = 245;
        public const uint Aggressor = 249;

        public const uint End = 249;
    }
    public static class Skills
    {
		public const int MaxSkillVal	  = 300;
		
        public const uint SmallGuns       = 200;
        public const uint BigGuns         = 201;
        public const uint EnergyWeapons   = 202;
        public const uint Unarmed         = 203;
        public const uint Melee           = 204;
        public const uint Throwing        = 205;
        public const uint FirstAid        = 206;
        public const uint Doctor          = 207;
        public const uint Sneak           = 208;
        public const uint Lockpick        = 209;
        public const uint Steal           = 210;
        public const uint Traps           = 211;
        public const uint Science         = 212;
        public const uint Repair          = 213;
        public const uint Speech          = 214;
        public const uint Barter          = 215;
        public const uint Gambling        = 216;
        public const uint Outdoorsman     = 217;
    }
	public static class Kills
	{
		public const uint Begin = 260;
		public const uint End = 280;
		public const uint Count = End - Begin + 1;
	}
    public static class Perks
    {
        public const uint Begin = 300;

        public const uint BOOKWORM = 300; // Todo
        public const uint AWARENESS = 301;
        public const uint BONUS_HTH_ATTACKS = 302;
        public const uint BONUS_HTH_DAMAGE = 303;
        public const uint BONUS_MOVE = 304;
        public const uint BONUS_RANGED_DAMAGE = 305;
        public const uint BONUS_RATE_OF_FIRE = 306;
        public const uint EARLIER_SEQUENCE = 307;
        public const uint FASTER_HEALING = 308;
        public const uint MORE_CRITICALS = 309;
        public const uint NIGHT_VISION = 310; // Todo
        public const uint PRESENCE = 311; // Todo
        public const uint RAD_RESISTANCE = 312;
        public const uint TOUGHNESS = 313;
        public const uint STRONG_BACK = 314;
        public const uint SHARPSHOOTER = 315;
        public const uint SILENT_RUNNING = 316; // Hardcoded
        public const uint SURVIVALIST = 317;
        public const uint MASTER_TRADER = 318; // Hardcoded
        public const uint EDUCATED = 319;
        public const uint HEALER = 320;
        public const uint FORTUNE_FINDER = 321;
        public const uint BETTER_CRITICALS = 322;
        public const uint EMPATHY = 323; // Todo
        public const uint SLAYER = 324;
        public const uint SNIPER = 325;
        public const uint SILENT_DEATH = 326;
        public const uint ACTION_BOY = 327;
        public const uint MENTAL_BLOCK = 328; // Todo
        public const uint LIFEGIVER = 329;
        public const uint DODGER = 330;
        public const uint SNAKEATER = 331;
        public const uint MR_FIXIT = 332;
        public const uint MEDIC = 333;
        public const uint MASTER_THIEF = 334;
        public const uint SPEAKER = 335;
        public const uint HEAVE_HO = 336;
        public const uint FRIENDLY_FOE = 337; // Todo
        public const uint PICKPOCKET = 338;
        public const uint GHOST = 339;
        public const uint CULT_OF_PERSONALITY = 340; // Todo
        public const uint SCROUNGER = 341; // Todo
        public const uint EXPLORER = 342;
        public const uint FLOWER_CHILD = 343; // Todo
        public const uint PATHFINDER = 344;
        public const uint ANIMAL_FRIEND = 345; // Todo
        public const uint SCOUT = 346;
        public const uint MYSTERIOUS_STRANGER = 347; // Todo
        public const uint RANGER = 348;
        public const uint QUICK_POCKETS = 349; // Hardcoded
        public const uint SMOOTH_TALKER = 350; // Hardcoded
        public const uint SWIFT_LEARNER = 351;
        public const uint TAG = 352; // Todo
        public const uint MUTATE = 353; // Todo
        // 354..379
        public const uint ADRENALINE_RUSH = 380;
        public const uint CAUTIOUS_NATURE = 381;
        public const uint COMPREHENSION = 382;
        public const uint DEMOLITION_EXPERT = 383;
        public const uint GAMBLER = 384;
        public const uint GAIN_STRENGTH = 385;
        public const uint GAIN_PERCEPTION = 386;
        public const uint GAIN_ENDURANCE = 387;
        public const uint GAIN_CHARISMA = 388;
        public const uint GAIN_INTELLIGENCE = 389;
        public const uint GAIN_AGILITY = 390;
        public const uint GAIN_LUCK = 391;
        public const uint HARMLESS = 392;
        public const uint HERE_AND_NOW = 393;
        public const uint HTH_EVADE = 394;
        public const uint KAMA_SUTRA_MASTER = 395;
        public const uint KARMA_BEACON = 396;
        public const uint LIGHT_STEP = 397;
        public const uint LIVING_ANATOMY = 398;
        public const uint MAGNETIC_PERSONALITY = 399;
        public const uint NEGOTIATOR = 400;
        public const uint PACK_RAT = 401;
        public const uint PYROMANIAC = 402;
        public const uint QUICK_RECOVERY = 403;
        public const uint SALESMAN = 404;
        public const uint STONEWALL = 405;
        public const uint THIEF = 406;
        public const uint WEAPON_HANDLING = 407;
        public const uint VAULT_CITY_TRAINING = 408;
        public const uint ALCOHOL_RAISED_HP = 409;
        public const uint ALCOHOL_RAISED_HP_II = 410;
        public const uint ALCOHOL_LOWERED_HP = 411;
        public const uint ALCOHOL_LOWERED_HP_II = 412;
        public const uint AUTODOC_RAISED_HP = 413;
        public const uint AUTODOC_RAISED_HP_II = 414;
        public const uint AUTODOC_LOWERED_HP = 415;
        public const uint AUTODOC_LOWERED_HP_II = 416;
        public const uint EXPERT_EXCREMENT = 417;
        // 418
        public const uint JINXED_II = 419;
        public const uint TERMINATOR = 420;
        // 421..429
        // Quest perks
        public const uint GECKO_SKINNING = 430;
        public const uint VAULT_CITY_INOCULATIONS = 431;
        public const uint DERMAL_IMPACT = 432;
        public const uint DERMAL_IMPACT_ENH = 433;
        public const uint PHOENIX_IMPLANTS = 434;
        public const uint PHOENIX_IMPLANTS_ENH = 435;
        public const uint NCR_PERCEPTION = 436;
        public const uint NCR_ENDURANCE = 437;
        public const uint NCR_BARTER = 438;
        public const uint NCR_REPAIR = 439;
        public const uint VAMPIRE_ACCURACY = 440;
        public const uint VAMPIRE_REGENERATION = 441;
        // 442..469
        public const uint End = 469;
    }
    // / Karma perks
    public static class Karma
    {
        //#define KARMA_BEGIN                              ( __KarmaBegin )
        //#define KARMA_END                                ( __KarmaEnd )
        //#define KARMA_COUNT                              ( KARMA_END - KARMA_BEGIN + 1 )
        public const uint Berserker = 480;
        public const uint Champion = 481;
        public const uint Childkiller = 482;
        public const uint Sexpert = 483;
        public const uint PrizeFighter = 484;
        public const uint Gigolo = 485;
        public const uint GraveDigger = 486;
        public const uint Married = 487;
        public const uint PornStar = 488;
        public const uint Slaver = 489;
        public const uint VirginWastes = 490;
        public const uint ManSalvatore = 491;
        public const uint ManBishop = 492;
        public const uint ManMordino = 493;
        public const uint ManWright = 494;
        public const uint Separated = 495;
        public const uint Pedobear = 496;
        public const uint VCGuardsman = 497;
        // 498..499
    }
    public static class Damages
    {
        public const uint Poisoned = 500; // Used in engine
        public const uint Radiated = 501; // Used in engine
        public const uint Eye = 502; // Used in engine
        public const uint RightArm = 503; // Used in engine
        public const uint LeftArm = 504; // Used in engine
        public const uint RightLeg = 505; // Used in engine
        public const uint LeftLeg = 506; // Used in engine
    }

    public static class Modes
    {
        public const uint Hide = 510; // Состояние скрытности                                   Hardcoded
        public const uint NoSteal = 511; // Нельзя обворовать                                      Hardcoded
        public const uint NoBarter = 512; // Нельзя тоговать                                        Hardcoded
        public const uint NoEnemyStack = 513; // Нпц не запоминает врагов                               Hardcoded
        public const uint NoPVP = 514; // Запрещает ПвП для игрока                               Hardcoded
        public const uint EndCombat = 515; // Согласен ли игрок закончить пошаговый бой              Hardcoded
        public const uint DefaultCombat = 516; // Режим боя по-умолчанию                                 Hardcoded
        public const uint NoHome = 517; // Нпц не возвращается в домашнюю позицию автоматически   Hardcoded
        public const uint GECK = 518; // Локация не удаляется при наличии такого нпц            Hardcoded
        public const uint NoFavoriteItem = 519; // Режим установки итемов по-умолчанию                    Hardcoded
        public const uint NoItemGarbager = 520; // Итемы не удаляются движком                             Hardcoded
        public const uint DlgScriptBarter = 521; // Возможно торговать при активном скрипте на диалоге     Hardcoded
        public const uint UnlimitedAmmo = 522; // Бесконечные патроны                                    Hardcoded
        public const uint NoDrop = 523; // Нельзя сбрасывать предметы
        public const uint NoLooseLimbs = 524; // Не может терять конечности
        public const uint DeadAges = 525; // Мёртвое тело не исчезает
        public const uint NoHeal = 526; // Повреждения не излечиваются с течением времени
        public const uint Invulnerable = 527; // Неуязвимый                                             Hardcoded
        public const uint NoFlatten = 528; // Не помещать труп на задний план после смерти           Hardcoded
        public const uint SpecialDead = 529; // Есть особый вид смерти
        public const uint RangeHtH = 530; // Возможна рукопашная атака на расстоянии                Hardcoded
        public const uint NoKnock = 531; // Не может быть сбит с ног
        public const uint NoLoot = 532; // Нельзя лутать                                          Hardcoded
        //public const uint NoSupply = 533; // Не приходят охотники за головами при смерти нпц
        public const uint Ext = 534;
        public const uint BarterOnlyCash = 535; // При бартере нпц принимает оплату только наличными
        public const uint NoPush = 536; // Can't be pushed                                        Hardcoded
        public const uint NoUnarmed = 537; // Not have unarmed attacks                               Hardcoded
        public const uint NoAim = 538; // Critter can't do aim attacks                           Hardcoded
        public const uint NoWalk = 539; // Critter can't walk                                     Hardcoded
        public const uint NoRun = 540; // Critter can't run                                      Hardcoded
        public const uint NoTalk = 541; // Npc can't talk                                         Hardcoded
    }
    // ext mode flags
    public enum ModeExt
    {
        NoSlave = 0x00000001,
        NoWallCheck = 0x00000002, // applies only when sneaking, used in dll
        Mob = 0x00000004,
        Guard = 0x00000008,
        Trader = 0x00000010,
        Follower = 0x00000020,
        PenBrahmin = 0x00000040,
        SlaveHostile = 0x00000080,
        SlaveNormal = 0x00000100,
        NoDeterioration = 0x00000200, // used in dll
        Event = 0x00000400, // Part of event
        NoAttackAuth = 0x00000800, // Don't attack authenticated players
        Militia = 0x00001000, // TC Militia
        TCLeader = 0x00002000, // TC Leader
        Slave = 0x00004000, // to diferentiate slaves within other followers
        LookAdmin = 0x00008000, // used in dll
        LookInvisible = 0x00010000 // used in dll
    }

	public static class Traits
	{
		public const uint FAST_METABOLISM                    = 550;
		public const uint BRUISER                            = 551;
		public const uint SMALL_FRAME                        = 552;
		public const uint ONE_HANDER                         = 553;
		public const uint FINESSE                            = 554;
		public const uint KAMIKAZE                           = 555;
		public const uint HEAVY_HANDED                       = 556;
		public const uint FAST_SHOT                          = 557;
		public const uint BLOODY_MESS                        = 558;
		public const uint JINXED                             = 559;
		public const uint GOOD_NATURED                       = 560;
		public const uint CHEM_RELIANT                       = 561;
		public const uint CHEM_RESISTANT                     = 562;
		public const uint SEX_APPEAL                         = 563;
		public const uint SKILLED                            = 564;
		public const uint NIGHT_PERSON                       = 565;
		// public const uint GIFTED                =566)
	}
}

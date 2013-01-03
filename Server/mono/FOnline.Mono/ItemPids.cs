using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public static class Pid
    {
        /******************************************************************
        ***************       Armor                         ***************
        ******************************************************************/

        // Light
        public const ushort LEATHER_JACKET = 74;
        public const ushort LEATHER_ARMOR = 1;
        public const ushort LEATHER_ARMOR_MK_II = 379;
        public const ushort CURED_LEATHER_ARMOR = 265;

        // Medium
        public const ushort METAL_ARMOR = 2;
        public const ushort METAL_ARMOR_MK_II = 380;
        public const ushort TESLA_ARMOR = 240;
        public const ushort COMBAT_ARMOR = 17;
        public const ushort COMBAT_ARMOR_MK_II = 381;
        public const ushort BROTHERHOOD_COMBAT_ARMOR = 239;
        public const ushort BLACK_COMBAT_ARMOR = 547;
        public const ushort CHITIN_ARMOR = 586;
        public const ushort CHITIN_ARMOR_MK_II = 596;

        // Heavy
        public const ushort POWERED_ARMOR = 3;
        public const ushort HARDENED_POWER_ARMOR = 232;
        public const ushort ADVANCED_POWER_ARMOR = 348;
        public const ushort ADVANCED_POWER_ARMOR_MK2 = 349;

        // Robes
        public const ushort PURPLE_ROBE = 113;
        public const ushort KEEPBRIGE_ROBE = 524; // PID_BRIDGEKEEPERS_ROBE
        public const ushort BLACK_ROBE = 585;

        // Quest
        public const ushort JUMPSUIT = 558;
        public const ushort FAKE_JUMPSUIT = 559;
        public const ushort BOTTLE_AMMIAK = 605;

        /******************************************************************
        ***************       Weapons                       ***************
        ******************************************************************/

        // Small Guns
        // Pistols
        public const ushort ZIP_GUN = 300;
        public const ushort _9MM_MAUSER = 122;
        public const ushort _10MM_PISTOL = 8;
        public const ushort _14MM_PISTOL = 22;
        public const ushort DESERT_EAGLE = 18;
        public const ushort DESERT_EAGLE_EXT_MAG = 404;
        public const ushort _223_PISTOL = 241;
        public const ushort _44_MAGNUM_REVOLVER = 313;
        public const ushort _44_MAGNUM_SPEEDLOADER = 398;
        public const ushort NEEDLER_PISTOL = 388; // HN Needler
        public const ushort PK12_GAUSS_PISTOL = 394; // 2mm EC
        public const ushort MULTI_BARREL_GUN = 603;
        // Rifles
        public const ushort HUNTING_RIFLE = 10;
        public const ushort SCOPED_HUNTING_RIFLE = 287;
        public const ushort SPRINGER_RIFLE = 299;
        public const ushort ASSAULT_RIFLE = 23;
        public const ushort ASSAULT_RIFLE_EXT_MAG = 405;
        public const ushort SNIPER_RIFLE = 143;
        public const ushort FN_FAL = 351;
        public const ushort FN_FAL_NIGHT_SCOPE = 403;
        public const ushort FN_FAL_HPFA = 500;
        public const ushort RED_RYDER_BB_GUN = 161;
        public const ushort RED_RYDER_LE_BB_GUN = 162;
        public const ushort JONNY_BB_GUN = 261;
        public const ushort INDEPENDENT = 353;
        public const ushort M72_GAUSS_RIFLE = 392; // 2mm EC
        public const ushort ELEPHANT_GUN = 592; // .700 cal.
        // Shotguns
        public const ushort SHOTGUN = 94;
        public const ushort SAWED_OFF_SHOTGUN = 385; // 12 ga.
        public const ushort COMBAT_SHOTGUN = 242;
        public const ushort HK_CAWS = 268;
        public const ushort PANCOR_JACKHAMMER = 354;
        // Pistol-machine gun
        public const ushort _10MM_SMG = 9;
        public const ushort HK_P90C = 296;
        public const ushort TOMMY_GUN = 283;
        public const ushort HK_G11 = 352;
        public const ushort HK_G11E = 391; // 4.7mm Caseless
        public const ushort GREASE_GUN = 332;
        public const ushort MAUSER_SMG = 578;

        // Big guns
        public const ushort FLAMER = 11;
        public const ushort IMPROVED_FLAMETHROWER = 400;
        public const ushort ROCKET_LAUNCHER = 13;
        public const ushort MINIGUN = 12;
        public const ushort AVENGER_MINIGUN = 389; // 5mm JHP
        public const ushort VINDICATOR_MINIGUN = 395; // 4.7mm Caseless
        public const ushort BOZAR = 350;
        public const ushort LIGHT_SUPPORT_WEAPON = 355;
        public const ushort M60 = 387; // 7.62
        public const ushort METAL_STORM = 604;

        // Energo
        // Laser
        public const ushort LASER_PISTOL = 16;
        public const ushort MAGNETO_LASER_PISTOL = 402;
        public const ushort SOLAR_SCORCHER = 390; // No ammo
        public const ushort LASER_RIFLE = 118;
        public const ushort LASER_RIFLE_EXT_CAP = 401;
        public const ushort GATLING_LASER = 28;
        public const ushort ALIEN_LASER_PISTOL = 120;
        // Plasma
        public const ushort PLASMA_PISTOL = 24;
        public const ushort PLASMA_PISTOL_EXT_CART = 406;
        public const ushort PLASMA_RIFLE = 15;
        public const ushort TURBO_PLASMA_RIFLE = 233;
        // Pulse
        public const ushort YK32_PULSE_PISTOL = 396; // Small Energy
        public const ushort YK42B_PULSE_RIFLE = 397; // Micro Fusion

        // Throwing
        // Grenade
        public const ushort MOLOTOV_COCKTAIL = 159; // Grouped
        public const ushort FRAG_GRENADE = 25;  // Grouped
        public const ushort PLASMA_GRENADE = 26;  // Grouped
        public const ushort PULSE_GRENADE = 27;  // Grouped
        public const ushort DOG_GRENADE = 602; // Grouped
        public const ushort SMOKE_GRENADE = 608;
        public const ushort MUSTARD_GAS_GRENADE = 609;
        // Other
        public const ushort FLARE = 79;  // Grouped
        public const ushort ACTIVE_FLARE = 205; // Active
        public const ushort PLANT_SPIKE = 365; // Grouped
        public const ushort THROWING_KNIFE = 45;  // Grouped
        public const ushort ROCK = 19;  // Grouped
        public const ushort GOLD_NUGGET = 423; // Grouped
        public const ushort URANIUM_ORE = 426; // Grouped
        public const ushort REFINED_ORE = 486; // Grouped

        // Melee
        // Cutting
        public const ushort KNIFE = 4;
        public const ushort COMBAT_KNIFE = 236;
        public const ushort LIL_JESUS_WEAPON = 517;
        public const ushort SHIV = 383;
        public const ushort SWITCHBLADE = 319;
        public const ushort WAKIZASHI_BLADE = 522;
        public const ushort ELI_KNIFE = 595;
        // Pricking
        public const ushort SPEAR = 7;
        public const ushort SHARP_SPEAR = 280;
        public const ushort SHARPENED_POLE = 320;
        // Shock
        public const ushort CLUB = 5;
        public const ushort CROWBAR = 20;
        public const ushort WRENCH = 384;
        public const ushort SLEDGEHAMMER = 6;
        public const ushort LOUISVILLE_SLUGGER = 386;
        public const ushort SUPER_SLEDGE = 115;
        // Electric
        public const ushort CATTLE_PROD = 160;
        public const ushort SUPER_CATTLE_PROD = 399;
        public const ushort RIPPER = 116;

        // Unarmed
        public const ushort BOXING_GLOVES = 292;
        public const ushort PLATED_BOXING_GLOVES = 293;
        public const ushort RING_BOXING_GLOVES = 496;
        public const ushort RING_PLATED_BOXING_GLOVES = 497;
        public const ushort POWER_FIST = 235;
        public const ushort BRASS_KNUCKLES = 21;
        public const ushort SPIKED_KNUCKLES = 234;
        public const ushort MEGA_POWER_FIST = 407;

        // Special
        public const ushort ROBO_ROCKET_LAUNCHER = 270;
        public const ushort PHAZER = 393; // Small Energy
        public const ushort DEATHCLAW_CLAW_1 = 371;
        public const ushort DEATHCLAW_CLAW_2 = 372;
        public const ushort FIRE_GECKO_FLAME_WEAPON = 427;
        public const ushort SPECIAL_BOXER_WEAPON = 489;
        public const ushort GUN_TURRET_WEAPON = 498;
        public const ushort EYEBALL_FIST_1 = 290;
        public const ushort EYEBALL_FIST_2 = 291;
        public const ushort DUAL_MINIGUN = 518;
        public const ushort HEAVY_DUAL_MINIGUN = 520;
        public const ushort HEAVY_DUAL_MINIGUN_LONG_RANGE = 546;
        public const ushort END_BOSS_KIFE = 530;
        public const ushort END_BOSS_PLASMA_GUN = 531;
        public const ushort HOLY_HAND_GRENADE = 421; // Grenade
        public const ushort SIGNAL_PISTOL = 610;
        public const ushort SIGNAL_ROCKET = 611;

        /******************************************************************
        ***************       Ammo                          ***************
        ******************************************************************/

        // Bullets
        public const ushort _4_7MM_CASELESS = 359;
        public const ushort _5MM_JHP = 35;
        public const ushort _5MM_AP = 36;
        public const ushort _7_62MM_AMMO = 363;
        public const ushort _9MM_AMMO = 360;
        public const ushort _9MM_BALL = 121;
        public const ushort _10MM_JHP = 29;
        public const ushort _10MM_AP = 30;
        public const ushort _14MM_AP = 33;
        public const ushort _44_MAGNUM_JHP = 31;
        public const ushort _44_FMJ_MAGNUM = 111;
        public const ushort _45_CALIBER_AMMO = 357;
        public const ushort _223_FMJ = 34;
        public const ushort SHOTGUN_SHELLS = 95;
        public const ushort _2MM_EC_AMMO = 358;
        public const ushort _700_NITRO_EXPRESS = 591;

        // Other
        public const ushort EXPLOSIVE_ROCKET = 14;
        public const ushort ROCKET_AP = 37;
        public const ushort FLAMETHROWER_FUEL = 32;
        public const ushort FLAMETHROWER_FUEL_MK_II = 382;
        public const ushort SMALL_ENERGY_CELL = 38;
        public const ushort MICRO_FUSION_CELL = 39;
        public const ushort BBS = 163;
        public const ushort HN_NEEDLER_CARTRIDGE = 361;
        public const ushort HN_AP_NEEDLER_CARTRIDGE = 362;

        // Special
        public const ushort ROBO_ROCKET_AMMO = 274;

        /******************************************************************
        ***************       Drugs                         ***************
        ******************************************************************/

        // Preparations
        public const ushort STIMPAK = 40;
        public const ushort RADAWAY = 48;
        public const ushort ANTIDOTE = 49;
        public const ushort RAD_X = 109;
        public const ushort SUPER_STIMPAK = 144;
        public const ushort JET_ANTIDOTE = 260;
        public const ushort HEALING_POWDER = 273;
        public const ushort HYPO = 525;

        // Alcohol
        public const ushort NUKA_COLA = 106;
        public const ushort BEER = 124;
        public const ushort BOOZE = 125;
        public const ushort GAMMA_GULP_BEER = 310;
        public const ushort ROENTGEN_RUM = 311;
        public const ushort ROT_GUT = 469;

        // Drug
        public const ushort MENTATS = 53;
        public const ushort BUFFOUT = 87;
        public const ushort PSYCHO = 110;
        public const ushort JET = 259;

        // Other
        public const ushort MUTATED_FRUIT = 71;
        public const ushort IGUANA_ON_A_STICK = 81;
        public const ushort MEAT_ON_A_STICK = 103;
        public const ushort COOKIE = 378;

        // Special
        public const ushort HYPO_POISON = 334;
        public const ushort MUTATED_TOE = 473;
        public const ushort KITTY_SEX_DRUG_AGILITY = 480; // + 1 agility for 1 hr
        public const ushort KITTY_SEX_DRUG_INTELLIGENCE = 481; // + 1 iq for 1 hr
        public const ushort KITTY_SEX_DRUG_STRENGTH = 482; // + 1 strength for 1 hr
        public const ushort MONUMENT_CHUNCK = 424;
        public const ushort BOX_OF_DOUGHNUTS = 594;

        /******************************************************************
        ***************       Container                     ***************
        ******************************************************************/

        public const ushort FRIDGE = 42;
        public const ushort ICE_CHEST_LEFT = 43;
        public const ushort ICE_CHEST_RIGHT = 44;
        public const ushort BAG = 46;
        public const ushort BACKPACK = 90;
        public const ushort BROWN_BAG = 93;
        public const ushort FOOTLOCKER_CLEAN_LEFT = 128;
        public const ushort FOOTLOCKER_RUSTY_LEFT = 129;
        public const ushort FOOTLOCKER_CLEAN_RIGHT = 130;
        public const ushort FOOTLOCKER_RUSTY_RIGHT = 131;
        public const ushort LOCKER_CLEAN_LEFT = 132;
        public const ushort LOCKER_RUSTY_LEFT = 133;
        public const ushort LOCKER_CLEAN_RIGHT = 134;
        public const ushort LOCKER_RUSTY_RIGHT = 135;
        public const ushort WALL_LOCKER_CLEAN_LEFT = 136;
        public const ushort WALL_LOCKER_CLEAN_RIGHT = 137;
        public const ushort WALL_LOCKER_RUSTY_LEFT = 138;
        public const ushort WALL_LOCKER_RUSTY_RIGHT = 139;
        public const ushort CONTAINER_WOOD_CRATE = 180;
        public const ushort VAULT_DWELLER_BONES = 211;
        public const ushort SMALL_POT = 243;
        public const ushort TALL_POT = 244;
        public const ushort CHEST = 245;
        public const ushort LEFT_ARROYO_BOOKCASE = 246;
        public const ushort RIGHT_ARROYO_BOOKCASE = 247;
        public const ushort OLIVE_POT = 248;
        public const ushort FLOWER_POT = 249;
        public const ushort HUMAN_BONES = 250;
        public const ushort CRASHED_VERTI_BIRD = 330;
        public const ushort GRAVESITE_1 = 344;
        public const ushort GRAVESITE_2 = 345;
        public const ushort GRAVESITE_3 = 346;
        public const ushort GRAVESITE_4 = 347;
        public const ushort LG_LT_AMMO_CRATE = 367;
        public const ushort SM_LT_AMMO_CRATE = 368;
        public const ushort LG_RT_AMMO_CRATE = 369;
        public const ushort SM_RT_AMMO_CRATE = 370;
        public const ushort LF_GRAVESITE_1 = 374;
        public const ushort LF_GRAVESITE_2 = 375;
        public const ushort LF_GRAVESITE_3 = 376;
        public const ushort HIDDEN_CONTAINER = 467;

        // From Misc
        public const ushort ALIEN_SIDE = 107;
        public const ushort ALIEN_FORWARD = 108;
        public const ushort STONE_HEAD = 425;
        public const ushort WAGON_RED = 434;
        public const ushort WAGON_GREY = 435;
        public const ushort CAR_TRUNK = 455;
        public const ushort WALL_SAFE = 501;
        public const ushort FLOOR_SAFE = 502;
        public const ushort POOL_TABLE_1 = 510;
        public const ushort POOL_TABLE_2 = 511;
        public const ushort POOL_TABLE_3 = 512;
        public const ushort POOL_TABLE_4 = 513;
        public const ushort POOL_TABLE_5 = 514;
        public const ushort POOL_TABLE_6 = 515;
        public const ushort POOR_BOX = 521;

        public const ushort EB_LONG_HOR = 850;
        public const ushort EB_LONG_VERT = 851;

        public const ushort EB_SHORT_HOR1 = 855;
        public const ushort EB_SHORT_HOR1_RED = 857;
        public const ushort EB_SHORT_VERT1 = 856;
        public const ushort EB_SHORT_VERT1_RED = 858;

        public const ushort EB_SHORT_HOR2 = 859;
        public const ushort EB_SHORT_HOR2_RED = 861;
        public const ushort EB_SHORT_VERT2 = 860;
        public const ushort EB_SHORT_VERT2_RED = 862;

        public const ushort EB_SHORT_HOR3 = 863;
        public const ushort EB_SHORT_HOR3_RED = 865;
        public const ushort EB_SHORT_VERT3 = 864;
        public const ushort EB_SHORT_VERT3_RED = 866;
        public const ushort EB_EMITTER_HOR1 = 867;
        public const ushort EB_EMITTER_VERT1 = 868;
        public const ushort EB_EMITTER_HOR2 = 869;
        public const ushort EB_EMITTER_VERT2 = 870;
        public const ushort EB_EMITTER_HOR3 = 871;
        public const ushort EB_EMITTER_VERT3 = 872;

        public const ushort EB_BLOCKER = 852;
        public const ushort EB_TRIGGER = 854;

        // New
        public const ushort BOOKCASE_0 = 60;
        public const ushort BOOKCASE_1 = 61;
        public const ushort BOOKCASE_2 = 62;
        public const ushort BOOKCASE_3 = 63;
        public const ushort BOOKCASE_4 = 64;
        public const ushort BOOKCASE_5 = 65;
        public const ushort DESK_0 = 66;
        public const ushort DESK_1 = 67;
        public const ushort DRESSER_0 = 68;
        public const ushort DRESSER_1 = 69;
        public const ushort DRESSER_2 = 70;
        public const ushort BOOKSELF_0 = 145;
        public const ushort BOOKSELF_1 = 146;
        public const ushort BOOKSELF_2 = 147;
        public const ushort BOOKSELF_3 = 149;
        public const ushort SHELVES_0 = 151;
        public const ushort SHELVES_1 = 152;
        public const ushort SHELVES_2 = 153;
        public const ushort SHELVES_3 = 155;
        public const ushort WORKBENCH = 157;
        public const ushort TOOL_BOARD = 158;
        public const ushort IGUANA_STAND = 165;
        public const ushort TABLE_0 = 166;
        public const ushort TABLE_1 = 167;
        public const ushort STUFF_0 = 168;
        public const ushort STUFF_1 = 169;
        public const ushort STUFF_2 = 170;
        public const ushort STUFF_3 = 171;
        public const ushort STUFF_4 = 172;
        public const ushort STUFF_5 = 173;
        public const ushort STUFF_6 = 174;
        public const ushort STUFF_7 = 175;
        public const ushort STUFF_8 = 176;
        public const ushort STUFF_9 = 177;
        public const ushort STUFF_10 = 178;
        public const ushort STUFF_11 = 179;
        public const ushort DESK_3 = 181;
        public const ushort DESK_4 = 182;
        public const ushort DESK_5 = 183;
        public const ushort DESK_6 = 184;
        public const ushort DESK_7 = 185;
        public const ushort DESK_8 = 186;
        public const ushort DESK_9 = 187;
        public const ushort LOCKER_7 = 188;
        public const ushort LOCKER_8 = 189;
        public const ushort BOX_0 = 197;
        public const ushort BOX_1 = 198;
        public const ushort BOX_2 = 199;
        public const ushort BOX_3 = 200;
        public const ushort BOX_4 = 201;
        public const ushort BOX_5 = 202;
        public const ushort BOX_6 = 203;
        public const ushort BOX_7 = 204;
        public const ushort REMAINSOF_GIZMO = 213;
        public const ushort DESK_10 = 214;
        public const ushort DEAD_REDSHIRT_0 = 526;
        public const ushort DEAD_REDSHIRT_1 = 527;
        public const ushort DEAD_REDSHIRT_2 = 528;
        public const ushort MINING_MACHINE = 529;

        // Car bags
        public const ushort HUMMER_BAG = 801;
        public const ushort VERTIBIRD_BAG = 809;
        public const ushort HIGHWAYMAN_BAG = 817;
        public const ushort BUGGY_BAG = 818;
        public const ushort SCOUT_BAG = 819;
        public const ushort BOAT_BAG = 822;

        /******************************************************************
        ***************       Keys                          ***************
        ******************************************************************/

        // From Misc
        public const ushort KEY = 82;
        public const ushort KEYS = 83;
        public const ushort RED_PASS_KEY = 96;
        public const ushort BLUE_PASS_KEY = 97;
        public const ushort NUKE_KEY = 105;
        public const ushort YELLOW_PASS_KEY = 223;
        public const ushort TEMPLE_KEY = 438;
        public const ushort JAIL_KEY = 456; // PID_CELL_DOOR_KEY

        /******************************************************************
        ***************       Misc.                         ***************
        ******************************************************************/

        // Money
        public const ushort BOTTLE_CAPS = 41;
        public const ushort MORNING_STAR_MINE = 420;
        public const ushort KOKOWEEF_MINE_SCRIP = 494;
        public const ushort REAL_BOTTLE_CAPS = 519;

        // Basic parts
        public const ushort PUMP_PARTS = 98; // Trash
        public const ushort SCORPION_TAIL = 92;
        public const ushort ROPE = 127;
        public const ushort BROC_FLOWER = 271;
        public const ushort XANDER_ROOT = 272;
        public const ushort GECKO_PELT = 276;
        public const ushort GOLDEN_GECKO_PELT = 277;
        public const ushort FLINT = 278;
        public const ushort MEAT_JERKY = 284;
        public const ushort RADSCORPION_PARTS = 285;
        public const ushort FIREWOOD = 286;
        public const ushort HYPODERMIC_NEEDLE = 318;
        public const ushort EMPTY_JET = 416;
        public const ushort BOTTLE_GLASS = 542; // Empty glass bottle
        public const ushort BOTTLE_EMPTY = 532; // Пустая бутылка
        public const ushort PART_OF_ROPE = 534; // Кусок веревки
        public const ushort METAL_TRASH = 475; // Металлический мусор
        public const ushort GUNPOWDER = 535; // Коробка пороха
        public const ushort METAL_ORE = 536; // Железная руда
        public const ushort MINERAL = 537; // Минерал
        public const ushort TUBE = 538; // Металлическая труба
        public const ushort STEEL = 50;  // Сталь
        public const ushort BRAHMIN_SKIN = 449; // Шкура брамина
        public const ushort MEAT = 539; // Мясо
        public const ushort CIGARETTES = 541; // Сигареты
        public const ushort RAGS = 572; // Кусок материи
        public const ushort NITROGLYCERIN = 573; // Нитроглицерин
        public const ushort CHEMICALS = 574; // Химикаты
        public const ushort TNT = 575; // Тротил
        public const ushort HEXOGEN = 576; // Гексоген
        public const ushort PLASTIC_EXPLOSIVES_DULL = 577; // Пластит без детонатора

        // Self-special parts
        public const ushort MOTOR = 89;
        public const ushort MOTIVATOR = 229;
        public const ushort PLASMA_TRANSFORMER = 307;
        public const ushort MINE_PART = 419;
        public const ushort EXPLOSIVE_SWITCH = 454;
        public const ushort ROBOT_MOTIVATOR = 364;
        public const ushort EXCAVATOR_CHIP = 422;
        public const ushort NAVCOM_PARTS = 479;
        public const ushort K9_MOTIVATOR = 488;

        // Special parts
        public const ushort WATER_CHIP = 55;
        public const ushort CAR_FUEL_CELL_CONTROLLER = 253; // For car
        public const ushort CAR_FUEL_INJECTION = 254; // For car
        public const ushort HY_MAG_PART = 258;
        public const ushort ROBOT_PARTS = 269;
        public const ushort COMPUTER_VOICE_MODULE = 356;
        public const ushort V15_COMPUTER_PART = 377;

        // Body parts
        public const ushort TANGLERS_HAND = 114;
        public const ushort BONES = 251; // PID_ANNA_BONES
        public const ushort DIXON_EYE = 281;
        public const ushort CLIFTON_EYE = 282;
        public const ushort CYBERNETIC_BRAIN = 321;
        public const ushort HUMAN_BRAIN = 322;
        public const ushort CHIMP_BRAIN = 323;
        public const ushort ABNORMAL_BRAIN = 324;
        public const ushort GOLD_TOOTH = 429;
        public const ushort PLAYERS_EAR = 484;
        public const ushort MASTICATORS_EAR = 485;
        public const ushort DECOMPOSING_BODY = 507; // PID_FN_FAL_HPFA

        // Plans
        public const ushort DR_HENRY_PAPERS = 340;
        public const ushort VERTIBIRD_PLANS = 446;

        // Chips
        public const ushort MEM_CHIP_BLUE = 503; // PID_BLUE_MEMORY_MODULE
        public const ushort MEM_CHIP_GREEN = 504; // PID_GREEN_MEMORY_MODULE
        public const ushort MEM_CHIP_RED = 505; // PID_RED_MEMORY_MODULE
        public const ushort MEM_CHIP_YELLOW = 506; // PID_YELLOW_MEMORY_MODULE
        public const ushort DERMAL_PIP_BOY_DISK = 499;
        public const ushort PIP_BOY_MEDICAL_ENHANCER = 516;

        // Tools
        // Tech
        public const ushort MULTI_TOOL = 75;  // +25 repair
        public const ushort SUPER_TOOL_KIT = 308; // +50 repair
        public const ushort OIL_CAN = 412; // 100% repair
        public const ushort LOCKPICKS = 84;  // +25 lockpick simple
        public const ushort EXP_LOCKPICK_SET = 410; // +50 lockpick simple
        public const ushort ELECTRONIC_LOCKPICKS = 77;  // +25 lockpick electronic
        public const ushort ELEC_LOCKPICK_MKII = 411; // +50 lockpick electronic

        // Medical
        public const ushort FIRST_AID_KIT = 47;  // +25 first aid
        public const ushort FIELD_MEDIC_KIT = 408; // +50 first aid
        public const ushort DOCTORS_BAG = 91;  // +25 doctor
        public const ushort PARAMEDICS_BAG = 409; // +50 doctor
        public const ushort MEDICAL_SUPPLIES = 428;
        public const ushort BIO_GEL = 440;
        public const ushort VACCINE = 593;
        // Other
        public const ushort SHOVEL = 289; // Лопата
        public const ushort PLANK = 297; // Лом
        public const ushort AXE = 543; // Топор
        public const ushort WELDING = 565;
        public const ushort WELDING_USED = 566;
        // Special
        public const ushort GEIGER_COUNTER = 52;
        public const ushort ACTIVE_GEIGER_COUNTER = 207; // Active
        public const ushort STEALTH_BOY = 54;
        public const ushort ACTIVE_STEALTH_BOY = 210; // Active
        public const ushort MOTION_SENSOR = 59;
        public const ushort ACTIVE_MOTION_SENSOR = 208; // Active

        // Books
        public const ushort BIG_BOOK_OF_SCIENCE = 73;
        public const ushort DEANS_ELECTRONICS = 76;
        public const ushort FIRST_AID_BOOK = 80;
        public const ushort SCOUT_HANDBOOK = 86;
        public const ushort GUNS_AND_BULLETS = 102;
        public const ushort CATS_PAW = 225;
        public const ushort TECHNICAL_MANUAL = 228;
        public const ushort CHEMISTRY_MANUAL = 237;
        public const ushort CATS_PAW_ISSUE_5 = 331;
        public const ushort BECKY_BOOK = 471;
        public const ushort HUBOLOGIST_BOOK = 571;

        // Dolls
        public const ushort SMALL_STATUETTE = 224;
        public const ushort MR_NIXON_DOLL = 491;
        public const ushort BLOW_UP_DOLL = 508;
        public const ushort POPPED_BLOW_UP_DOLL = 509;

        // Other
        public const ushort LIGHTER = 101; // Zippo
        public const ushort FLOWER = 117;
        public const ushort WATER_FLASK = 126;
        public const ushort BOX_OF_NOODLES = 226;
        public const ushort FROZEN_DINNER = 227;
        public const ushort RUBBER_BOOTS = 262;
        public const ushort CHEEZY_POOFS = 295;
        public const ushort BLUE_CONDOM = 314;
        public const ushort GREEN_CONDOM = 315;
        public const ushort RED_CONDOM = 316;
        public const ushort COSMETIC_CASE = 317;
        public const ushort DICE = 325; // Play dice
        public const ushort LOADED_DICE = 326; // Play dice
        public const ushort DECK_OF_CARDS = 436; // Play cards
        public const ushort MARKED_DECK_OF_CARDS = 437; // Play cards

        // Special
        public const ushort BRIEFCASE = 72;
        public const ushort MUTAGENIC_SYRUM = 329; // Иньекция от мутации
        public const ushort HEART_PILLS = 333; // Таблетки от серца
        public const ushort SPECTACLES = 415; // Eye glasses
        public const ushort OXYGEN_TANK = 417;
        public const ushort POISON_TANK = 418;
        public const ushort HOWITZER_SHELL = 430; // Ammo of turret in SAD
        public const ushort MIRROR_SHADES = 433; // Eye glasses
        public const ushort POCKET_LINT = 439; // Trash
        public const ushort BALL_GAG = 470; // Кляп
        public const ushort DAISIES = 474; // Горшок с цветами
        public const ushort BOTTLE_FULL = 533; // Полная бутылка

        public const ushort DOG_TAGS = 56;
        public const ushort ELECTRONIC_BUG = 57;
        public const ushort FUZZY_PAINTING = 78;
        public const ushort GOLD_LOCKET = 99;
        public const ushort TAPE_RECORDER = 104;
        public const ushort URN = 112;
        public const ushort NECKLACE = 119; // Ожерелье
        public const ushort PSYCHIC_NULLIFIER = 123;
        public const ushort BLACK_COC_BADGE = 141; // Habbology
        public const ushort RED_COC_BADGE = 142; // Habbology
        public const ushort BARTER_TANDI = 212;
        public const ushort ANNA_GOLD_LOCKET = 252;
        public const ushort DAY_PASS = 255; // Vault-City
        public const ushort FAKE_CITIZENSHIP = 256; // Vault-City
        public const ushort CORNELIUS_GOLD_WATCH = 257;
        public const ushort VIC_RADIO = 266;
        public const ushort VIC_WATER_FLASK = 267;
        public const ushort TROPHY_OF_RECOGNITION = 275;
        public const ushort NEURAL_INTERFACE = 279;
        public const ushort CLIPBOARD = 301;
        public const ushort DECK_OF_TRAGIC_CARDS = 304; // Play cards
        public const ushort TALISMAN = 309;
        public const ushort SLAG_MESSAGE = 263;
        public const ushort PART_REQUISITION_FORM = 312;
        public const ushort EASTER_EGG = 327;
        public const ushort MAGIC_8_BALL = 328;
        public const ushort MOORE_BAD_BRIEFCASE = 335;
        public const ushort MOORE_GOOD_BRIEFCASE = 336;
        public const ushort PRESIDENTIAL_PASS = 341; // Ncr
        public const ushort RANGER_PIN = 342; // Ncr
        public const ushort RANGER_MAP = 343;
        public const ushort GECK = 366;
        public const ushort STABLES_ID_BADGE = 413; // Пропуск в канюшни
        public const ushort RAMIREZ_BOX_CLOSED = 431;
        public const ushort RAMIREZ_BOX_OPEN = 432;
        public const ushort BLONDIE_DOG_TAG = 441; // Бирка
        public const ushort ANGEL_EYES_DOG_TAG = 442; // Бирка
        public const ushort TUCO_DOG_TAG = 443; // Бирка
        public const ushort REBEL_TAG = 579; // Бирка рейнджеров
        public const ushort RAIDERS_MAP = 444;
        public const ushort SHERIFF_BADGE = 445; // Значек шерифа
        public const ushort ACCOUNT_BOOK = 448; // Книга по разборкам в Нью-Рено
        public const ushort TORN_PAPER_1 = 450; // Part1 password
        public const ushort TORN_PAPER_2 = 451; // Part2 password
        public const ushort TORN_PAPER_3 = 452; // Part3 password
        public const ushort PASSWORD_PAPER = 453; // All password
        public const ushort ELRON_FIELD_REP = 457; // Отчет Хаббологов
        public const ushort SMITTY_MEAL = 468; // Еда Смитти
        public const ushort ENLIGHTENED_ONE_LETTER = 476; // Доклад
        public const ushort FALLOUT_2_HINTBOOK = 483;
        public const ushort MOUSE_SKIN = 540; // Шкура мыши
        public const ushort CHOSEN_HOLO = 549;
        public const ushort CHOSEN_HOLO_DECODED = 550;
        public const ushort LETTER_TO_LIN = 551;
        public const ushort LETTER_TO_TODD = 552;
        public const ushort EDWARD_REPORT = 553;
        public const ushort LETTER_TO_CASSIDY = 554;
        public const ushort LETTER_TO_SINDY = 555;
        public const ushort FIRE_GECKO_PELT = 556;
        public const ushort DANTON_POISON = 557;
        public const ushort ARROYO_SYRINGE = 560;
        public const ushort DEATH_STAR_PLANS = 561;
        public const ushort FALLOUT_3_HOLO = 562;
        public const ushort FAKE_GECK = 563;
        public const ushort USED_GECK = 564;
        public const ushort FAKE_LETTER = 600;
        public const ushort POT = 601;

        // Special keys
        public const ushort ACCESS_CARD = 140;
        public const ushort SECURITY_CARD = 221;
        public const ushort TRAPPER_TOWN_KEY = 298;
        public const ushort YELLOW_REACTOR_KEYCARD = 305;
        public const ushort RED_REACTOR_KEYCARD = 306;
        public const ushort V15_KEYCARD = 373;
        public const ushort VAULT_13_SHACK_KEY = 414;
        public const ushort PRES_ACCESS_KEY = 495;
        public const ushort TANKER_FOB = 492;

        // Holodisks
        public const ushort BROTHERHOOD_TAPE = 164;
        public const ushort DISK_FEV = 190;
        public const ushort DISK_SECURITY = 191;
        public const ushort DISK_ALPHA_EXPERIMENT = 192;
        public const ushort DISK_DELTA_EXPERIMENT = 193;
        public const ushort DISK_VREES_EXPERIMENT = 194;
        public const ushort DISK_MUTANT_TRANSMISSIONS = 196;
        public const ushort DISK_BROTHERHOOD_HISTORY = 215;
        public const ushort DISK_MAXSON_HISTORY = 216;
        public const ushort DISK_MAXSON_JOURNAL = 217;
        public const ushort DISK_VAULT_RECORDS = 230;
        public const ushort DISK_MILITARY_BASE_SEC_CODE = 231;
        public const ushort DISK_REGULATOR_TRANSMISSIONS = 238;
        public const ushort HOLODISK_FAKE_V13 = 294;
        public const ushort GECKO_DATA_DISK = 302;
        public const ushort REACTOR_DATA_DISK = 303;
        public const ushort LYNETTE_HOLO = 337;
        public const ushort WESTIN_HOLO = 338;
        public const ushort SPY_HOLO = 339;
        public const ushort BISHOPS_HOLODISK = 447;
        public const ushort ENCLAVE_HOLODISK_5 = 458;
        public const ushort ENCLAVE_HOLODISK_1 = 459;
        public const ushort ENCLAVE_HOLODISK_2 = 460;
        public const ushort ENCLAVE_HOLODISK_3 = 461;
        public const ushort ENCLAVE_HOLODISK_4 = 462;
        public const ushort EVACUATION_HOLODISK = 463;
        public const ushort EXPERIMENT_HOLODISK = 464;
        public const ushort MEDICAL_HOLODISK = 465;
        public const ushort PASSWORD_HOLODISK = 466;
        public const ushort ELRON_MEMBER_HOLO = 472;
        public const ushort BROADCAST_HOLODISK = 477;
        public const ushort SIERRA_MISSION_HOLODISK = 478;
        public const ushort NCR_HISTORY_HOLODISK = 490;
        public const ushort ELRON_TEACH_HOLO = 493;

        // Other, Inventory pic n/a
        public const ushort WATCH = 88;
        public const ushort MISC_BOOKCASE_0 = 148;
        public const ushort MISC_BOOKCASE_1 = 150;
        public const ushort MISC_SHELVES_0 = 154;
        public const ushort MISC_SHELVES_1 = 156;
        public const ushort DISK_BROTHERHOOD_HONOR_CODE = 195;
        public const ushort BARTER_LIGHT_HEALING = 218;
        public const ushort BARTER_MEDIUM_HEALING = 219;
        public const ushort BARTER_HEAVY_HEALING = 220;
        public const ushort SMITH_COOL_ITEM = 264;
        public const ushort CAR_FUEL_CELL = 288;

        // Traps, active see in Misc2
        public const ushort DYNAMITE = 51; // Has timer
        public const ushort PLASTIC_EXPLOSIVES = 85;
        public const ushort MINE = 544;

        // Craft recipes
        public const ushort CHITIN_ARMOR_RECIPE = 597;

        /******************************************************************
        ***************       Misc2                         ***************
        ******************************************************************/

        // Special
        public const ushort HOLODISK = 58;
        public const ushort RADIO = 100;
        public const ushort MEMO = 487; // PID_MEMO_FROM_FRANCIS
        public const ushort MAP = 523; // PID_SURVEY_MAP

        // Cars
        public const ushort HUMMER = 800; // Car
        public const ushort BUGGY = 802; // Car
        public const ushort SCOUT = 805; // Car
        public const ushort VERTIBIRD = 808; // Big Car
        public const ushort HIGHWAYMAN = 816; // Car
        public const ushort BOAT = 821; // Boat

        // Traps
        public const ushort ACTIVE_DYNAMITE = 206;
        public const ushort TOGGLE_SWITCH = 222;
        public const ushort ACTIVE_PLASTIC_EXPLOSIVE = 209;
        public const ushort ACTIVE_MINE = 545;

        // Manufacture, Still
        public const ushort STILL_B_BROKEN = 810;
        public const ushort STILL_S_BROKEN = 811;
        public const ushort STILL_B = 812; // Beer still
        public const ushort STILL_B_ACTIVE = 813;
        public const ushort STILL_S = 814; // Booz (Schlager) still
        public const ushort STILL_S_ACTIVE = 815;

        // Barricades stuff
        public const ushort SANDBAG_1 = 580;
        public const ushort SANDBAG_2 = 581;
        public const ushort SANDBAG_3 = 582;
        public const ushort SANDBAG_4 = 583;
        public const ushort SANDBAG_5 = 584;
        public const ushort SANDBAG_EMPTY = 587;

        // Other
        public const ushort WANTED_SIGN = 3228;
        public const ushort UNVISIBLE_BLOCK = 820;
        public const ushort POSTMAN_LETTER = 548;

        public const ushort ENERGY_BARIER_ACCESS_CARD = 853;

        // Player Locations special items
        public const ushort KOTW_BEER = 588;
        public const ushort KOTW_GRAIL = 589;
        public const ushort KOTW_STATUETTE = 590;

        /******************************************************************
        ***************       Effects                       ***************
        ******************************************************************/

        // Smoke
        public const ushort SMOKE = 606;
        public const ushort MUSTARD_GAS = 607;
        // Explode
        public const ushort EXPLODE_FIRE_BIG = 4027;
        public const ushort EXPLODE_FIRE_SMALL = 4028;
        public const ushort EXPLODE_PLASMA = 4029;
        public const ushort EXPLODE_EMP = 4008;
        public const ushort EXPLODE_ROCKET = 4011;
        // Flying
        public const ushort FLYING_ROCKET = 4001;
        public const ushort FLYING_PLASMA_BALL = 4002;
        public const ushort FLYING_KNIFE = 4006;
        public const ushort FLYING_SPEAR = 4007;
        public const ushort FLYING_LASER_BLAST = 4009;
        public const ushort FLYING_PLASMA_BLAST = 4010;
        public const ushort FLYING_ELECTRICITY_BOLT = 4013;
    }
}

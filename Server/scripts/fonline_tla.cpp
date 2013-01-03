#include "fonline_tla.h"

// Extern data definition
_GlobalVars GlobalVars;

// Slot/parameters allowing
EXPORT bool allowSlot_Hand1( uint8, Item &, Critter &, Critter & toCr );

// Parameters Get behavior
EXPORT int getParam_Strength( CritterMutual & cr, uint );
EXPORT int getParam_Perception( CritterMutual & cr, uint );
EXPORT int getParam_Endurance( CritterMutual & cr, uint );
EXPORT int getParam_Charisma( CritterMutual & cr, uint );
EXPORT int getParam_Intellegence( CritterMutual & cr, uint );
EXPORT int getParam_Agility( CritterMutual & cr, uint );
EXPORT int getParam_Luck( CritterMutual & cr, uint );
EXPORT int getParam_Hp( CritterMutual & cr, uint );
EXPORT int getParam_MaxLife( CritterMutual & cr, uint );
EXPORT int getParam_MaxAp( CritterMutual & cr, uint );
EXPORT int getParam_Ap( CritterMutual & cr, uint );
EXPORT int getParam_MaxMoveAp( CritterMutual & cr, uint );
EXPORT int getParam_MoveAp( CritterMutual & cr, uint );
EXPORT int getParam_MaxWeight( CritterMutual & cr, uint );
EXPORT int getParam_Sequence( CritterMutual & cr, uint );
EXPORT int getParam_MeleeDmg( CritterMutual & cr, uint );
EXPORT int getParam_HealingRate( CritterMutual & cr, uint );
EXPORT int getParam_CriticalChance( CritterMutual & cr, uint );
EXPORT int getParam_MaxCritical( CritterMutual & cr, uint );
EXPORT int getParam_Ac( CritterMutual & cr, uint );
EXPORT int getParam_DamageResistance( CritterMutual& cr, uint index );
EXPORT int getParam_DamageThreshold( CritterMutual& cr, uint index );
EXPORT int getParam_RadiationResist( CritterMutual & cr, uint );
EXPORT int getParam_PoisonResist( CritterMutual & cr, uint );
EXPORT int  getParam_Timeout( CritterMutual& cr, uint index );
EXPORT int  getParam_Reputation( CritterMutual& cr, uint index );
EXPORT void changedParam_Reputation( CritterMutual& cr, uint index, int oldValue );

// Extended methods
EXPORT bool Critter_IsInjured( CritterMutual& cr );
EXPORT bool Critter_IsDmgEye( CritterMutual& cr );
EXPORT bool Critter_IsDmgLeg( CritterMutual& cr );
EXPORT bool Critter_IsDmgTwoLeg( CritterMutual& cr );
EXPORT bool Critter_IsDmgArm( CritterMutual& cr );
EXPORT bool Critter_IsDmgTwoArm( CritterMutual& cr );
EXPORT bool Critter_IsAddicted( CritterMutual& cr );
EXPORT bool Critter_IsOverweight( CritterMutual& cr );
EXPORT bool Item_Weapon_IsHtHAttack( Item& item, uint8 mode );
EXPORT bool Item_Weapon_IsGunAttack( Item& item, uint8 mode );
EXPORT bool Item_Weapon_IsRangedAttack( Item& item, uint8 mode );

// Callbacks
uint GetUseApCost( CritterMutual& cr, Item& item, uint8 mode );
uint GetAttackDistantion( CritterMutual& cr, Item& item, uint8 mode );

// Generic stuff
int  GetNightPersonBonus();
uint GetAimApCost( int hitLocation );
uint GetAimHit( int hitLocation );
uint GetMultihex( CritterMutual& cr );

/************************************************************************/
/* Initialization                                                       */
/************************************************************************/

// In this functions (DllMain and DllLoad) all global variables is NOT initialized, use FONLINE_DLL_ENTRY instead
#if defined ( FO_WINDOWS )
int __stdcall DllMain( void* module, unsigned long reason, void* reserved ) { return 1; }
#elif defined ( FO_LINUX )
void __attribute__( ( constructor ) ) DllLoad()   {}
void __attribute__( ( destructor ) )  DllUnload() {}
#endif

FONLINE_DLL_ENTRY( isCompiler )
{
    if( isCompiler )
        return;

    // Register callbacks
    FOnline->GetUseApCost = &GetUseApCost;
    FOnline->GetAttackDistantion = &GetAttackDistantion;
    // FOnline->GetRainOffset = &GetRainOffset; // Not set because engine do same, uncomment to use

    // Register script global vars
    memset( &GlobalVars, 0, sizeof( GlobalVars ) );
    for( asUINT i = 0; i < ASEngine->GetGlobalPropertyCount(); i++ )
    {
        const char* name;
        void*       ptr;
        if( ASEngine->GetGlobalPropertyByIndex( i, &name, NULL, NULL, NULL, NULL, &ptr ) < 0 )
            continue;

        #define REGISTER_GLOBAL_VAR( type, gvar ) \
            else if( !strcmp( # gvar, name ) )    \
                GlobalVars.gvar = (type*) ptr
        REGISTER_GLOBAL_VAR( int, CurX );
        REGISTER_GLOBAL_VAR( int, CurY );
        REGISTER_GLOBAL_VAR( uint, HitAimEyes );
        REGISTER_GLOBAL_VAR( uint, HitAimHead );
        REGISTER_GLOBAL_VAR( uint, HitAimGroin );
        REGISTER_GLOBAL_VAR( uint, HitAimTorso );
        REGISTER_GLOBAL_VAR( uint, HitAimArms );
        REGISTER_GLOBAL_VAR( uint, HitAimLegs );
    }
}

/************************************************************************/
/* Slot/parameters allowing                                             */
/************************************************************************/

EXPORT bool allowSlot_Hand1( uint8, Item&, Critter&, Critter& toCr )
{
    return toCr.Params[ PE_AWARENESS ] != 0;
}

/************************************************************************/
/* Parameters Get behaviors                                             */
/************************************************************************/

EXPORT int getParam_Strength( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_STRENGTH ] + cr.Params[ ST_STRENGTH_EXT ];
    if( cr.Params[ PE_ADRENALINE_RUSH ] && getParam_Timeout( cr, TO_BATTLE ) && // Adrenaline rush perk
        cr.Params[ ST_CURRENT_HP ] <= ( cr.Params[ ST_MAX_LIFE ] + cr.Params[ ST_STRENGTH ] + cr.Params[ ST_ENDURANCE ] * 2 ) / 2 )
        val++;
    return CLAMP( val, 1, 10 );
}

EXPORT int getParam_Perception( CritterMutual& cr, uint )
{
    int val = ( cr.Params[ DAMAGE_EYE ] ? 1 : cr.Params[ ST_PERCEPTION ] + cr.Params[ ST_PERCEPTION_EXT ] );
    if( cr.Params[ TRAIT_NIGHT_PERSON ] )
        val += GetNightPersonBonus();
    return CLAMP( val, 1, 10 );
}

EXPORT int getParam_Endurance( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_ENDURANCE ] + cr.Params[ ST_ENDURANCE_EXT ];
    return CLAMP( val, 1, 10 );
}

EXPORT int getParam_Charisma( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_CHARISMA ] + cr.Params[ ST_CHARISMA_EXT ];
    return CLAMP( val, 1, 10 );
}

EXPORT int getParam_Intellegence( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_INTELLECT ] + cr.Params[ ST_INTELLECT_EXT ];
    if( cr.Params[ TRAIT_NIGHT_PERSON ] )
        val += GetNightPersonBonus();
    return CLAMP( val, 1, 10 );
}

EXPORT int getParam_Agility( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_AGILITY ] + cr.Params[ ST_AGILITY_EXT ];
    return CLAMP( val, 1, 10 );
}

EXPORT int getParam_Luck( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_LUCK ] + cr.Params[ ST_LUCK_EXT ];
    return CLAMP( val, 1, 10 );
}

EXPORT int getParam_Hp( CritterMutual& cr, uint )
{
    return cr.Params[ ST_CURRENT_HP ];
}

EXPORT int getParam_MaxLife( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_MAX_LIFE ] + cr.Params[ ST_MAX_LIFE_EXT ] + cr.Params[ ST_STRENGTH ] + cr.Params[ ST_ENDURANCE ] * 2;
    return CLAMP( val, 1, 9999 );
}

EXPORT int getParam_MaxAp( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_ACTION_POINTS ] + cr.Params[ ST_ACTION_POINTS_EXT ] + getParam_Agility( cr, 0 ) / 2;
    return CLAMP( val, 1, 9999 );
}

EXPORT int getParam_Ap( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_CURRENT_AP ];
    val /= AP_DIVIDER;
    return CLAMP( val, -9999, 9999 );
}

EXPORT int getParam_MaxMoveAp( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_MAX_MOVE_AP ];
    return CLAMP( val, 0, 9999 );
}

EXPORT int getParam_MoveAp( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_MOVE_AP ];
    return CLAMP( val, 0, 9999 );
}

EXPORT int getParam_MaxWeight( CritterMutual& cr, uint )
{
    int val = max( cr.Params[ ST_CARRY_WEIGHT ] + cr.Params[ ST_CARRY_WEIGHT_EXT ], 0 );
    val += CONVERT_GRAMM( 25 + getParam_Strength( cr, 0 ) * ( 25 - cr.Params[ TRAIT_SMALL_FRAME ] * 10 ) );
    return CLAMP( val, 0, 2000000000 );
}

EXPORT int getParam_Sequence( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_SEQUENCE ] + cr.Params[ ST_SEQUENCE_EXT ] + getParam_Perception( cr, 0 ) * 2;
    return CLAMP( val, 0, 9999 );
}

EXPORT int getParam_MeleeDmg( CritterMutual& cr, uint )
{
    int strength = getParam_Strength( cr, 0 );
    int val = cr.Params[ ST_MELEE_DAMAGE ] + cr.Params[ ST_MELEE_DAMAGE_EXT ] + ( strength > 6 ? strength - 5 : 1 );
    return CLAMP( val, 1, 9999 );
}

EXPORT int getParam_HealingRate( CritterMutual& cr, uint )
{
    int e = getParam_Endurance( cr, 0 );
    int val = cr.Params[ ST_HEALING_RATE ] + cr.Params[ ST_HEALING_RATE_EXT ] + max( 1, e / 3 );
    return CLAMP( val, 0, 9999 );
}

EXPORT int getParam_CriticalChance( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_CRITICAL_CHANCE ] + cr.Params[ ST_CRITICAL_CHANCE_EXT ] + getParam_Luck( cr, 0 );
    return CLAMP( val, 0, 100 );
}

EXPORT int getParam_MaxCritical( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_MAX_CRITICAL ] + cr.Params[ ST_MAX_CRITICAL_EXT ];
    return CLAMP( val, -100, 100 );
}

EXPORT int getParam_Ac( CritterMutual& cr, uint )
{
    int         val = cr.Params[ ST_ARMOR_CLASS ] + cr.Params[ ST_ARMOR_CLASS_EXT ] + getParam_Agility( cr, 0 ) + cr.Params[ ST_TURN_BASED_AC ];
    const Item* armor = cr.ItemSlotArmor;
    if( armor->GetId() && armor->IsArmor() )
        val += armor->Proto->Armor_AC * ( 100 - armor->GetDeteriorationProc() ) / 100;
    return CLAMP( val, 0, 90 );
}

EXPORT int getParam_DamageResistance( CritterMutual& cr, uint index )
{
    int         dmgType = index - ST_NORMAL_RESIST + 1;

    const Item* armor = cr.ItemSlotArmor;
    int         val = 0;
    int         drVal = 0;
    switch( dmgType )
    {
    case DAMAGE_NORMAL:
        val = cr.Params[ ST_NORMAL_RESIST ]  + cr.Params[ ST_NORMAL_RESIST_EXT ];
        drVal = armor->Proto->Armor_DRNormal;
        break;
    case DAMAGE_LASER:
        val = cr.Params[ ST_LASER_RESIST ]   + cr.Params[ ST_LASER_RESIST_EXT ];
        drVal = armor->Proto->Armor_DRLaser;
        break;
    case DAMAGE_FIRE:
        val = cr.Params[ ST_FIRE_RESIST ]    + cr.Params[ ST_FIRE_RESIST_EXT ];
        drVal = armor->Proto->Armor_DRFire;
        break;
    case DAMAGE_PLASMA:
        val = cr.Params[ ST_PLASMA_RESIST ]  + cr.Params[ ST_PLASMA_RESIST_EXT ];
        drVal = armor->Proto->Armor_DRPlasma;
        break;
    case DAMAGE_ELECTR:
        val = cr.Params[ ST_ELECTRO_RESIST ] + cr.Params[ ST_ELECTRO_RESIST_EXT ];
        drVal = armor->Proto->Armor_DRElectr;
        break;
    case DAMAGE_EMP:
        val = cr.Params[ ST_EMP_RESIST ]     + cr.Params[ ST_EMP_RESIST_EXT ];
        drVal = armor->Proto->Armor_DREmp;
        break;
    case DAMAGE_EXPLODE:
        val = cr.Params[ ST_EXPLODE_RESIST ] + cr.Params[ ST_EXPLODE_RESIST_EXT ];
        drVal = armor->Proto->Armor_DRExplode;
        break;
    case DAMAGE_UNCALLED:
    default:
        break;
    }

    if( armor->GetId() && armor->IsArmor() )
        val += drVal * ( 100 - armor->GetDeteriorationProc() ) / 100;

    if( dmgType == DAMAGE_EMP )
        return CLAMP( val, 0, 999 );
    return CLAMP( val, 0, 90 );
}

EXPORT int getParam_DamageThreshold( CritterMutual& cr, uint index )
{
    int         dmgType = index - ST_NORMAL_ABSORB + 1;

    const Item* armor = cr.ItemSlotArmor;
    int         val = 0;
    int         dtVal = 0;
    switch( dmgType )
    {
    case DAMAGE_NORMAL:
        val = cr.Params[ ST_NORMAL_ABSORB ]  + cr.Params[ ST_NORMAL_ABSORB_EXT ];
        dtVal = armor->Proto->Armor_DTNormal;
        break;
    case DAMAGE_LASER:
        val = cr.Params[ ST_LASER_ABSORB ]   + cr.Params[ ST_LASER_ABSORB_EXT ];
        dtVal = armor->Proto->Armor_DTLaser;
        break;
    case DAMAGE_FIRE:
        val = cr.Params[ ST_FIRE_ABSORB ]    + cr.Params[ ST_FIRE_ABSORB_EXT ];
        dtVal = armor->Proto->Armor_DTFire;
        break;
    case DAMAGE_PLASMA:
        val = cr.Params[ ST_PLASMA_ABSORB ]  + cr.Params[ ST_PLASMA_ABSORB_EXT ];
        dtVal = armor->Proto->Armor_DTPlasma;
        break;
    case DAMAGE_ELECTR:
        val = cr.Params[ ST_ELECTRO_ABSORB ] + cr.Params[ ST_ELECTRO_ABSORB_EXT ];
        dtVal = armor->Proto->Armor_DTElectr;
        break;
    case DAMAGE_EMP:
        val = cr.Params[ ST_EMP_ABSORB ]     + cr.Params[ ST_EMP_ABSORB_EXT ];
        dtVal = armor->Proto->Armor_DTEmp;
        break;
    case DAMAGE_EXPLODE:
        val = cr.Params[ ST_EXPLODE_ABSORB ] + cr.Params[ ST_EXPLODE_ABSORB_EXT ];
        dtVal = armor->Proto->Armor_DTExplode;
        break;
    case DAMAGE_UNCALLED:
    default:
        break;
    }

    if( armor->GetId() && armor->IsArmor() )
        val += dtVal * ( 100 - armor->GetDeteriorationProc() ) / 100;

    return CLAMP( val, 0, 999 );
}

EXPORT int getParam_RadiationResist( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_RADIATION_RESISTANCE ] + cr.Params[ ST_RADIATION_RESISTANCE_EXT ] + getParam_Endurance( cr, 0 ) * 2;
    return CLAMP( val, 0, 95 );
}

EXPORT int getParam_PoisonResist( CritterMutual& cr, uint )
{
    int val = cr.Params[ ST_POISON_RESISTANCE ] + cr.Params[ ST_POISON_RESISTANCE_EXT ] + getParam_Endurance( cr, 0 ) * 5;
    return CLAMP( val, 0, 95 );
}

EXPORT int getParam_Timeout( CritterMutual& cr, uint index )
{
    return (uint) cr.Params[ index ] > FOnline->FullSecond ? (uint) cr.Params[ index ] - FOnline->FullSecond : 0;
}

EXPORT int getParam_Reputation( CritterMutual& cr, uint index )
{
    #ifdef __SERVER
    if( cr.Params[ index ] == 0x80000000 )
    {
        FOnline->CritterChangeParameter( cr, index );
        const_cast< int* >( cr.Params )[ index ] = 0;
    }
    #else
    if( cr.Params[ index ] == 0x80000000 )
        return 0;
    #endif
    return cr.Params[ index ];
}

EXPORT void changedParam_Reputation( CritterMutual& cr, uint index, int oldValue )
{
    if( oldValue == 0x80000000 )
        const_cast< int* >( cr.Params )[ index ] += 0x80000000;
}

/************************************************************************/
/* Extended methods                                                     */
/************************************************************************/

EXPORT bool Critter_IsInjured( CritterMutual& cr )
{
    return Critter_IsDmgArm( cr ) || Critter_IsDmgLeg( cr ) || Critter_IsDmgEye( cr );
}

EXPORT bool Critter_IsDmgEye( CritterMutual& cr )
{
    return cr.Params[ DAMAGE_EYE ] != 0;
}

EXPORT bool Critter_IsDmgLeg( CritterMutual& cr )
{
    return cr.Params[ DAMAGE_RIGHT_LEG ] || cr.Params[ DAMAGE_LEFT_LEG ];
}

EXPORT bool Critter_IsDmgTwoLeg( CritterMutual& cr )
{
    return cr.Params[ DAMAGE_RIGHT_LEG ] && cr.Params[ DAMAGE_LEFT_LEG ];
}

EXPORT bool Critter_IsDmgArm( CritterMutual& cr )
{
    return cr.Params[ DAMAGE_RIGHT_ARM ] || cr.Params[ DAMAGE_LEFT_ARM ];
}

EXPORT bool Critter_IsDmgTwoArm( CritterMutual& cr )
{
    return cr.Params[ DAMAGE_RIGHT_ARM ] && cr.Params[ DAMAGE_LEFT_ARM ];
}

EXPORT bool Critter_IsAddicted( CritterMutual& cr )
{
    for( uint i = ADDICTION_BEGIN; i <= ADDICTION_END; i++ )
        if( cr.Params[ i ] )
            return true;
    return false;
}

EXPORT bool Critter_IsOverweight( CritterMutual& cr )
{
    // Calculate inventory items weight
    uint w = 0;
    for( ItemVecIt it = cr.InvItems.begin(), end = cr.InvItems.end(); it != end; ++it )
        w += ( *it )->GetWeight();

    return w > (uint) getParam_MaxWeight( cr, 0 );
}

EXPORT bool Item_Weapon_IsHtHAttack( Item& item, uint8 mode )
{
    if( !item.IsWeapon() || !item.WeapIsUseAviable( mode & 7 ) )
        return false;
    int skill = SKILL_OFFSET( item.Proto->Weapon_Skill[ mode & 7 ] );
    return skill == SK_UNARMED || skill == SK_MELEE_WEAPONS;
}

EXPORT bool Item_Weapon_IsGunAttack( Item& item, uint8 mode )
{
    if( !item.IsWeapon() || !item.WeapIsUseAviable( mode & 7 ) )
        return false;
    int skill = SKILL_OFFSET( item.Proto->Weapon_Skill[ mode & 7 ] );
    return skill == SK_SMALL_GUNS || skill == SK_BIG_GUNS || skill == SK_ENERGY_WEAPONS;
}

EXPORT bool Item_Weapon_IsRangedAttack( Item& item, uint8 mode )
{
    if( !item.IsWeapon() || !item.WeapIsUseAviable( mode & 7 ) )
        return false;
    int skill = SKILL_OFFSET( item.Proto->Weapon_Skill[ mode & 7 ] );
    return skill == SK_SMALL_GUNS || skill == SK_BIG_GUNS || skill == SK_ENERGY_WEAPONS || skill == SK_THROWING;
}

/************************************************************************/
/* Callbacks                                                            */
/************************************************************************/

uint GetUseApCost( CritterMutual& cr, Item& item, uint8 mode )
{
    uint8 use = mode & 0xF;
    uint8 aim = mode >> 4;
    int   apCost = 1;

    if( use == USE_USE )
    {
        if( TB_BATTLE_TIMEOUT_CHECK( getParam_Timeout( cr, TO_BATTLE ) ) )
            apCost = FOnline->TbApCostUseItem;
        else
            apCost = FOnline->RtApCostUseItem;
    }
    else if( use == USE_RELOAD )
    {
        if( TB_BATTLE_TIMEOUT_CHECK( getParam_Timeout( cr, TO_BATTLE ) ) )
            apCost = FOnline->TbApCostReloadWeapon;
        else
            apCost = FOnline->RtApCostReloadWeapon;

        if( item.IsWeapon() && item.Proto->Weapon_Perk == WEAPON_PERK_FAST_RELOAD )
            apCost--;
    }
    else if( use >= USE_PRIMARY && use <= USE_THIRD && item.IsWeapon() )
    {
        int  skill = item.Proto->Weapon_Skill[ use ];
        bool hthAttack = Item_Weapon_IsHtHAttack( item, mode );
        bool rangedAttack = Item_Weapon_IsRangedAttack( item, mode );

        apCost = item.Proto->Weapon_ApCost[ use ];
        if( aim )
            apCost += GetAimApCost( aim );
        if( hthAttack && cr.Params[ PE_BONUS_HTH_ATTACKS ] )
            apCost--;
        if( rangedAttack && cr.Params[ PE_BONUS_RATE_OF_FIRE ] )
            apCost--;
        if( cr.Params[ TRAIT_FAST_SHOT ] && !hthAttack )
            apCost--;
    }

    if( apCost < 1 )
        apCost = 1;
    return apCost;
}

uint GetAttackDistantion( CritterMutual& cr, Item& item, uint8 mode )
{
    uint8 use = mode & 0xF;
    int   dist = item.Proto->Weapon_MaxDist[ use ];
    int   strength = getParam_Strength( cr, 0 );
    if( item.Proto->Weapon_Skill[ use ] == SKILL_OFFSET( SK_THROWING ) )
        dist = min( dist, 3 * min( 10, strength + 2 * cr.Params[ PE_HEAVE_HO ] ) );
    if( Item_Weapon_IsHtHAttack( item, mode ) && cr.Params[ MODE_RANGE_HTH ] )
        dist++;
    dist += GetMultihex( cr );
    if( dist < 0 )
        dist = 0;
    return dist;
}

void GetRainOffset( int16* ox, int16* oy )
{
    *ox = FOnline->Random( -10, 10 );
    *oy = -100 - FOnline->Random( 0, 100 );
}

/************************************************************************/
/* Generic stuff                                                        */
/************************************************************************/

int GetNightPersonBonus()
{
    if( FOnline->Hour < 6 || FOnline->Hour > 18 )
        return 1;
    if( FOnline->Hour == 6 && FOnline->Minute == 0 )
        return 1;
    if( FOnline->Hour == 18 && FOnline->Minute > 0 )
        return 1;
    return -1;
}

uint GetAimApCost( int hitLocation )
{
    switch( hitLocation )
    {
    case HIT_LOCATION_TORSO:
        return FOnline->ApCostAimTorso;
    case HIT_LOCATION_EYES:
        return FOnline->ApCostAimEyes;
    case HIT_LOCATION_HEAD:
        return FOnline->ApCostAimHead;
    case HIT_LOCATION_LEFT_ARM:
    case HIT_LOCATION_RIGHT_ARM:
        return FOnline->ApCostAimArms;
    case HIT_LOCATION_GROIN:
        return FOnline->ApCostAimGroin;
    case HIT_LOCATION_RIGHT_LEG:
    case HIT_LOCATION_LEFT_LEG:
        return FOnline->ApCostAimLegs;
    case HIT_LOCATION_NONE:
    case HIT_LOCATION_UNCALLED:
    default:
        break;
    }
    return 0;
}

uint GetAimHit( int hitLocation )
{
    switch( hitLocation )
    {
    case HIT_LOCATION_TORSO:
        return *GlobalVars.HitAimTorso;
    case HIT_LOCATION_EYES:
        return *GlobalVars.HitAimEyes;
    case HIT_LOCATION_HEAD:
        return *GlobalVars.HitAimHead;
    case HIT_LOCATION_LEFT_ARM:
    case HIT_LOCATION_RIGHT_ARM:
        return *GlobalVars.HitAimArms;
    case HIT_LOCATION_GROIN:
        return *GlobalVars.HitAimGroin;
    case HIT_LOCATION_RIGHT_LEG:
    case HIT_LOCATION_LEFT_LEG:
        return *GlobalVars.HitAimLegs;
    case HIT_LOCATION_NONE:
    case HIT_LOCATION_UNCALLED:
    default:
        break;
    }
    return 0;
}

uint GetMultihex( CritterMutual& cr )
{
    int mh = cr.Multihex;
    if( mh < 0 )
        mh = FOnline->CritterTypes[ cr.BaseType ].Multihex;
    return CLAMP( mh, 0, MAX_HEX_OFFSET );
}

/************************************************************************/
/*                                                                      */
/************************************************************************/

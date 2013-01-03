using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public partial class ProtoItem : IManagedWrapper
    {
        readonly IntPtr thisptr;
        internal ProtoItem(IntPtr ptr)
        {
            thisptr = ptr;
            AddRef();
        }
        ~ProtoItem()
        {
            Release();
        }

        public static explicit operator ProtoItem(IntPtr ptr)
        {
            return new ProtoItem(ptr);
        }
        public IntPtr ThisPtr { get { return thisptr; } }
		public virtual void AddRef()
		{
			AddRef (thisptr);
		}
		public virtual void Release()
		{
			Release (thisptr);
		}
		public bool TwoHands
		{
			get { return (Flags & ItemFlag.TwoHands) != 0; }
		}
		
		public bool WeaponIsHtHAttack(byte use)
		{
			return WeaponSkill(use) == Skills.Unarmed || WeaponSkill(use)== Skills.Melee;
		}
		public bool WeaponIsGunAttack(byte use)
		{
			return WeaponSkill(use) == Skills.SmallGuns || WeaponSkill(use) == Skills.BigGuns || WeaponSkill(use) == Skills.EnergyWeapons;
		}
		public bool WeaponIsRangedAttack(byte use )
		{
			return WeaponIsGunAttack(use) || WeaponSkill(use) == Skills.Throwing;
		}
		public bool WeaponIsPrimaryAviable() 
		{
			return ( Weapon_ActiveUses & 1 ) != 0;
		}
		public bool WeaponIsSecondaryAviable() 
		{
			return ( Weapon_ActiveUses & 2 ) != 0;
		}
		public bool WeaponIsThirdAviable() 
		{
			return ( Weapon_ActiveUses & 4 ) != 0;
		}
		public int WeaponSkill(byte use)
		{ 
			return ( use ) == 0 ? Weapon_Skill_0  : ( ( use ) == 1 ? Weapon_Skill_1  : ( ( use ) == 2 ? Weapon_Skill_2  : 0 ) );
		}
		public Damage WeaponDmgType(byte use)
		{ 
			return (Damage)(( use ) == 0 ? Weapon_DmgType_0 : ( ( use ) == 1 ? Weapon_DmgType_1 : ( ( use ) == 2 ? Weapon_DmgType_2 : 0 ) ));
		}
		public uint WeaponAnim2(byte use)
		{ 
			return ( use ) == 0 ? Weapon_Anim2_0  : ( ( use ) == 1 ? Weapon_Anim2_1  : ( ( use ) == 2 ? Weapon_Anim2_2  : 0 ) );
		}
		public int WeaponDmgMin(byte use)
		{ 
			return ( use ) == 0 ? Weapon_DmgMin_0 : ( ( use ) == 1 ? Weapon_DmgMin_1 : ( ( use ) == 2 ? Weapon_DmgMin_2 : 0 ) );
		}
		public int WeaponDmgMax(byte use)
		{ 
			return ( use ) == 0 ? Weapon_DmgMax_0 : ( ( use ) == 1 ? Weapon_DmgMax_1 : ( ( use ) == 2 ? Weapon_DmgMax_2 : 0 ) );
		}
		public uint WeaponMaxDist(byte use)
		{ 
			return ( use ) == 0 ? Weapon_MaxDist_0 : ( ( use ) == 1 ? Weapon_MaxDist_1 : ( ( use ) == 2 ? Weapon_MaxDist_2 : 0 ) );
		}
		public ushort WeaponEffect(byte use)
		{ 
			return use == 0 ? Weapon_Effect_0 : ( use == 1 ? Weapon_Effect_1 : ( use == 2 ? Weapon_Effect_2 : (ushort)0 ) );
		}
		public uint WeaponRound(byte use)
		{ 
			return ( use ) == 0 ? Weapon_Round_0  : ( ( use ) == 1 ? Weapon_Round_1  : ( ( use ) == 2 ? Weapon_Round_2  : 0 ) );
		}
		public uint WeaponApCost(byte use)
		{ 
			return ( use ) == 0 ? Weapon_ApCost_0 : ( ( use ) == 1 ? Weapon_ApCost_1 : ( ( use ) == 2 ? Weapon_ApCost_2 : 0 ) );
		}
		public int WeaponSoundId(byte use)
		{ 
			return ( use ) == 0 ? Weapon_SoundId_0 : ( ( use ) == 1 ? Weapon_SoundId_1 : ( ( use ) == 2 ? Weapon_SoundId_2 : (byte)0 ) );
		}
		public bool WeaponRemove(byte use)
		{ 
			return ( use ) == 0 ? Weapon_Remove_0 : ( ( use ) == 1 ? Weapon_Remove_1 : ( ( use ) == 2 ? Weapon_Remove_2 : false ) );
		}
		public bool WeaponAim(byte use)
		{ 
			return ( use ) == 0 ? Weapon_Aim_0    : ( ( use ) == 1 ? Weapon_Aim_1    : ( ( use ) == 2 ? Weapon_Aim_2    : false ) );
		}
    }
	
	public enum WeaponPerk
	{
		LongRange = 1,
		Accurate = 2,
		Penetrate = 3,
		Knockback = 4,
		ScopeRange = 5,
		FastReload = 6,
		NightSight = 7,
		Flameboy = 8,
		EnhancedKnockout = 9
	}
	/*
	 * #define ARMOR_PERK_POWERED                       ( 1 )    // +3 strength, +30 radiation resist
#define ARMOR_PERK_COMBAT                        ( 2 )    // +20 radiation resist
#define ARMOR_PERK_ADVANCED_I                    ( 3 )    // +4 strength, +60 radiation resist
#define ARMOR_PERK_ADVANCED_II                   ( 4 )    // +4 strength, +75 radiation resist
#define ARMOR_PERK_CHARISMA                      ( 5 )    // 1+ charisma
#define SLOT_PERK_MIRROR_SHADES                  ( 0x01 ) // +1 charisma
#define SLOT_PERK_COSMETIC_CASE                  ( 0x02 ) // +1 charisma to female
#define SLOT_PERK_MOTION_SENSOR                  ( 0x04 ) // +20 outdoorsman
#define SLOT_PERK_STEALTH_BOY                    ( 0x08 ) // +20 sneak
*/
}

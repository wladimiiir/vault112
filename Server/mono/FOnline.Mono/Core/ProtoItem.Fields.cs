using System;
namespace FOnline
{
    public partial class ProtoItem
    {
        public virtual UInt16 ProtoId { get { return NativeFields.GetUInt16(thisptr, offsetProtoId); }}
        public virtual Int32 Type { get { return NativeFields.GetInt32(thisptr, offsetType); }}
        public virtual UInt32 PicMap { get { return NativeFields.GetUInt32(thisptr, offsetPicMap); }}
        public virtual UInt32 PicInv { get { return NativeFields.GetUInt32(thisptr, offsetPicInv); }}
        public virtual ItemFlag Flags { get { return (ItemFlag)NativeFields.GetUInt32(thisptr, offsetFlags); }}
        public virtual Boolean Stackable { get { return NativeFields.GetBoolean(thisptr, offsetStackable); }}
        public virtual Boolean Deteriorable { get { return NativeFields.GetBoolean(thisptr, offsetDeteriorable); }}
        public virtual Boolean GroundLevel { get { return NativeFields.GetBoolean(thisptr, offsetGroundLevel); }}
        public virtual Int32 Corner { get { return NativeFields.GetInt32(thisptr, offsetCorner); }}
        public virtual Int32 Dir { get { return NativeFields.GetInt32(thisptr, offsetDir); }}
        public virtual Byte Slot { get { return NativeFields.GetByte(thisptr, offsetSlot); }}
        public virtual UInt32 Weight { get { return NativeFields.GetUInt32(thisptr, offsetWeight); }}
        public virtual UInt32 Volume { get { return NativeFields.GetUInt32(thisptr, offsetVolume); }}
        public virtual UInt32 Cost { get { return NativeFields.GetUInt32(thisptr, offsetCost); }}
        public virtual UInt32 StartCount { get { return NativeFields.GetUInt32(thisptr, offsetStartCount); }}
        public virtual Byte SoundId { get { return NativeFields.GetByte(thisptr, offsetSoundId); }}
        public virtual Byte Material { get { return NativeFields.GetByte(thisptr, offsetMaterial); }}
        public virtual Byte LightFlags { get { return NativeFields.GetByte(thisptr, offsetLightFlags); }}
        public virtual Byte LightDistance { get { return NativeFields.GetByte(thisptr, offsetLightDistance); }}
        public virtual SByte LightIntensity { get { return NativeFields.GetSByte(thisptr, offsetLightIntensity); }}
        public virtual UInt32 LightColor { get { return NativeFields.GetUInt32(thisptr, offsetLightColor); }}
        public virtual Boolean DisableEgg { get { return NativeFields.GetBoolean(thisptr, offsetDisableEgg); }}
        public virtual UInt16 AnimWaitBase { get { return NativeFields.GetUInt16(thisptr, offsetAnimWaitBase); }}
        public virtual UInt16 AnimWaitRndMin { get { return NativeFields.GetUInt16(thisptr, offsetAnimWaitRndMin); }}
        public virtual UInt16 AnimWaitRndMax { get { return NativeFields.GetUInt16(thisptr, offsetAnimWaitRndMax); }}
        public virtual Byte AnimStay_0 { get { return NativeFields.GetByte(thisptr, offsetAnimStay_0); }}
        public virtual Byte AnimStay_1 { get { return NativeFields.GetByte(thisptr, offsetAnimStay_1); }}
        public virtual Byte AnimShow_0 { get { return NativeFields.GetByte(thisptr, offsetAnimShow_0); }}
        public virtual Byte AnimShow_1 { get { return NativeFields.GetByte(thisptr, offsetAnimShow_1); }}
        public virtual Byte AnimHide_0 { get { return NativeFields.GetByte(thisptr, offsetAnimHide_0); }}
        public virtual Byte AnimHide_1 { get { return NativeFields.GetByte(thisptr, offsetAnimHide_1); }}
        public virtual Int16 OffsetX { get { return NativeFields.GetInt16(thisptr, offsetOffsetX); }}
        public virtual Int16 OffsetY { get { return NativeFields.GetInt16(thisptr, offsetOffsetY); }}
        public virtual Byte SpriteCut { get { return NativeFields.GetByte(thisptr, offsetSpriteCut); }}
        public virtual SByte DrawOrderOffsetHexY { get { return NativeFields.GetSByte(thisptr, offsetDrawOrderOffsetHexY); }}
        public virtual UInt16 RadioChannel { get { return NativeFields.GetUInt16(thisptr, offsetRadioChannel); }}
        public virtual UInt16 RadioFlags { get { return NativeFields.GetUInt16(thisptr, offsetRadioFlags); }}
        public virtual Byte RadioBroadcastSend { get { return NativeFields.GetByte(thisptr, offsetRadioBroadcastSend); }}
        public virtual Byte RadioBroadcastRecv { get { return NativeFields.GetByte(thisptr, offsetRadioBroadcastRecv); }}
        public virtual Byte IndicatorStart { get { return NativeFields.GetByte(thisptr, offsetIndicatorStart); }}
        public virtual Byte IndicatorMax { get { return NativeFields.GetByte(thisptr, offsetIndicatorMax); }}
        public virtual UInt32 HolodiskNum { get { return NativeFields.GetUInt32(thisptr, offsetHolodiskNum); }}
        public virtual Int32 StartValue_0 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_0); }}
        public virtual Int32 StartValue_1 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_1); }}
        public virtual Int32 StartValue_2 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_2); }}
        public virtual Int32 StartValue_3 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_3); }}
        public virtual Int32 StartValue_4 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_4); }}
        public virtual Int32 StartValue_5 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_5); }}
        public virtual Int32 StartValue_6 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_6); }}
        public virtual Int32 StartValue_7 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_7); }}
        public virtual Int32 StartValue_8 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_8); }}
        public virtual Int32 StartValue_9 { get { return NativeFields.GetInt32(thisptr, offsetStartValue_9); }}
        public virtual Byte BlockLines { get { return NativeFields.GetByte(thisptr, offsetBlockLines); }}
        public virtual UInt16 ChildPid_0 { get { return NativeFields.GetUInt16(thisptr, offsetChildPid_0); }}
        public virtual UInt16 ChildPid_1 { get { return NativeFields.GetUInt16(thisptr, offsetChildPid_1); }}
        public virtual UInt16 ChildPid_2 { get { return NativeFields.GetUInt16(thisptr, offsetChildPid_2); }}
        public virtual UInt16 ChildPid_3 { get { return NativeFields.GetUInt16(thisptr, offsetChildPid_3); }}
        public virtual UInt16 ChildPid_4 { get { return NativeFields.GetUInt16(thisptr, offsetChildPid_4); }}
        public virtual Byte ChildLines_0 { get { return NativeFields.GetByte(thisptr, offsetChildLines_0); }}
        public virtual Byte ChildLines_1 { get { return NativeFields.GetByte(thisptr, offsetChildLines_1); }}
        public virtual Byte ChildLines_2 { get { return NativeFields.GetByte(thisptr, offsetChildLines_2); }}
        public virtual Byte ChildLines_3 { get { return NativeFields.GetByte(thisptr, offsetChildLines_3); }}
        public virtual Byte ChildLines_4 { get { return NativeFields.GetByte(thisptr, offsetChildLines_4); }}
        public virtual Boolean Weapon_IsUnarmed { get { return NativeFields.GetBoolean(thisptr, offsetWeapon_IsUnarmed); }}
        public virtual Int32 Weapon_UnarmedTree { get { return NativeFields.GetInt32(thisptr, offsetWeapon_UnarmedTree); }}
        public virtual Int32 Weapon_UnarmedPriority { get { return NativeFields.GetInt32(thisptr, offsetWeapon_UnarmedPriority); }}
        public virtual Int32 Weapon_UnarmedMinAgility { get { return NativeFields.GetInt32(thisptr, offsetWeapon_UnarmedMinAgility); }}
        public virtual Int32 Weapon_UnarmedMinUnarmed { get { return NativeFields.GetInt32(thisptr, offsetWeapon_UnarmedMinUnarmed); }}
        public virtual Int32 Weapon_UnarmedMinLevel { get { return NativeFields.GetInt32(thisptr, offsetWeapon_UnarmedMinLevel); }}
        public virtual UInt32 Weapon_Anim1 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_Anim1); }}
        public virtual UInt32 Weapon_MaxAmmoCount { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_MaxAmmoCount); }}
        public virtual Int32 Weapon_Caliber { get { return NativeFields.GetInt32(thisptr, offsetWeapon_Caliber); }}
        public virtual UInt16 Weapon_DefaultAmmoPid { get { return NativeFields.GetUInt16(thisptr, offsetWeapon_DefaultAmmoPid); }}
        public virtual Int32 Weapon_MinStrength { get { return NativeFields.GetInt32(thisptr, offsetWeapon_MinStrength); }}
        public virtual WeaponPerk Weapon_Perk { get { return (WeaponPerk)NativeFields.GetInt32(thisptr, offsetWeapon_Perk); }}
        public virtual UInt32 Weapon_ActiveUses { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_ActiveUses); }}
        public virtual Int32 Weapon_Skill_0 { get { return NativeFields.GetInt32(thisptr, offsetWeapon_Skill_0); }}
        public virtual Int32 Weapon_Skill_1 { get { return NativeFields.GetInt32(thisptr, offsetWeapon_Skill_1); }}
        public virtual Int32 Weapon_Skill_2 { get { return NativeFields.GetInt32(thisptr, offsetWeapon_Skill_2); }}
        public virtual UInt32 Weapon_PicUse_0 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_PicUse_0); }}
        public virtual UInt32 Weapon_PicUse_1 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_PicUse_1); }}
        public virtual UInt32 Weapon_PicUse_2 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_PicUse_2); }}
        public virtual UInt32 Weapon_MaxDist_0 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_MaxDist_0); }}
        public virtual UInt32 Weapon_MaxDist_1 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_MaxDist_1); }}
        public virtual UInt32 Weapon_MaxDist_2 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_MaxDist_2); }}
        public virtual UInt32 Weapon_Round_0 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_Round_0); }}
        public virtual UInt32 Weapon_Round_1 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_Round_1); }}
        public virtual UInt32 Weapon_Round_2 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_Round_2); }}
        public virtual UInt32 Weapon_ApCost_0 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_ApCost_0); }}
        public virtual UInt32 Weapon_ApCost_1 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_ApCost_1); }}
        public virtual UInt32 Weapon_ApCost_2 { get { return NativeFields.GetUInt32(thisptr, offsetWeapon_ApCost_2); }}
        public virtual Boolean Weapon_Aim_0 { get { return NativeFields.GetBoolean(thisptr, offsetWeapon_Aim_0); }}
        public virtual Boolean Weapon_Aim_1 { get { return NativeFields.GetBoolean(thisptr, offsetWeapon_Aim_1); }}
        public virtual Boolean Weapon_Aim_2 { get { return NativeFields.GetBoolean(thisptr, offsetWeapon_Aim_2); }}
        public virtual Byte Weapon_SoundId_0 { get { return NativeFields.GetByte(thisptr, offsetWeapon_SoundId_0); }}
        public virtual Byte Weapon_SoundId_1 { get { return NativeFields.GetByte(thisptr, offsetWeapon_SoundId_1); }}
        public virtual Byte Weapon_SoundId_2 { get { return NativeFields.GetByte(thisptr, offsetWeapon_SoundId_2); }}
        public virtual Int32 Ammo_Caliber { get { return NativeFields.GetInt32(thisptr, offsetAmmo_Caliber); }}
        public virtual Boolean Door_NoBlockMove { get { return NativeFields.GetBoolean(thisptr, offsetDoor_NoBlockMove); }}
        public virtual Boolean Door_NoBlockShoot { get { return NativeFields.GetBoolean(thisptr, offsetDoor_NoBlockShoot); }}
        public virtual Boolean Door_NoBlockLight { get { return NativeFields.GetBoolean(thisptr, offsetDoor_NoBlockLight); }}
        public virtual UInt32 Container_Volume { get { return NativeFields.GetUInt32(thisptr, offsetContainer_Volume); }}
        public virtual Boolean Container_Changeble { get { return NativeFields.GetBoolean(thisptr, offsetContainer_Changeble); }}
        public virtual Boolean Container_CannotPickUp { get { return NativeFields.GetBoolean(thisptr, offsetContainer_CannotPickUp); }}
        public virtual Boolean Container_MagicHandsGrnd { get { return NativeFields.GetBoolean(thisptr, offsetContainer_MagicHandsGrnd); }}
        public virtual UInt16 Locker_Condition { get { return NativeFields.GetUInt16(thisptr, offsetLocker_Condition); }}
        public virtual Int32 Grid_Type { get { return NativeFields.GetInt32(thisptr, offsetGrid_Type); }}
        public virtual UInt32 Car_Speed { get { return NativeFields.GetUInt32(thisptr, offsetCar_Speed); }}
        public virtual UInt32 Car_Passability { get { return NativeFields.GetUInt32(thisptr, offsetCar_Passability); }}
        public virtual UInt32 Car_DeteriorationRate { get { return NativeFields.GetUInt32(thisptr, offsetCar_DeteriorationRate); }}
        public virtual UInt32 Car_CrittersCapacity { get { return NativeFields.GetUInt32(thisptr, offsetCar_CrittersCapacity); }}
        public virtual UInt32 Car_TankVolume { get { return NativeFields.GetUInt32(thisptr, offsetCar_TankVolume); }}
        public virtual UInt32 Car_MaxDeterioration { get { return NativeFields.GetUInt32(thisptr, offsetCar_MaxDeterioration); }}
        public virtual UInt32 Car_FuelConsumption { get { return NativeFields.GetUInt32(thisptr, offsetCar_FuelConsumption); }}
        public virtual UInt32 Car_Entrance { get { return NativeFields.GetUInt32(thisptr, offsetCar_Entrance); }}
        public virtual UInt32 Car_MovementType { get { return NativeFields.GetUInt32(thisptr, offsetCar_MovementType); }}
    
		//
		// bindfields
		//
		
		// Common
		public virtual int MagicPower { get {return NativeFields.GetInt32(thisptr, offsetMagicPower); } }
		// Armor
		public virtual uint Armor_CrTypeMale { get {return NativeFields.GetUInt32(thisptr, offsetArmor_CrTypeMale); } }
		public virtual uint Armor_CrTypeFemale { get {return NativeFields.GetUInt32(thisptr, offsetArmor_CrTypeFemale); } }
		public virtual int Armor_AC { get {return NativeFields.GetInt32(thisptr, offsetArmor_AC); } }
		public virtual uint Armor_Perk { get {return NativeFields.GetUInt32(thisptr, offsetArmor_Perk); } }
		public virtual int Armor_DRNormal { get {return NativeFields.GetInt32(thisptr, offsetArmor_DRNormal); } }
		public virtual int Armor_DRLaser { get {return NativeFields.GetInt32(thisptr, offsetArmor_DRLaser); } }
		public virtual int Armor_DRFire { get {return NativeFields.GetInt32(thisptr, offsetArmor_DRFire); } }
		public virtual int Armor_DRPlasma { get {return NativeFields.GetInt32(thisptr, offsetArmor_DRPlasma); } }
		public virtual int Armor_DRElectr { get {return NativeFields.GetInt32(thisptr, offsetArmor_DRElectr); } }
		public virtual int Armor_DREmp { get {return NativeFields.GetInt32(thisptr, offsetArmor_DREmp); } }
		public virtual int Armor_DRExplode { get {return NativeFields.GetInt32(thisptr, offsetArmor_DRExplode); } }
		public virtual int Armor_DTNormal { get {return NativeFields.GetInt32(thisptr, offsetArmor_DTNormal); } }
		public virtual int Armor_DTLaser { get {return NativeFields.GetInt32(thisptr, offsetArmor_DTLaser); } }
		public virtual int Armor_DTFire { get {return NativeFields.GetInt32(thisptr, offsetArmor_DTFire); } }
		public virtual int Armor_DTPlasma { get {return NativeFields.GetInt32(thisptr, offsetArmor_DTPlasma); } }
		public virtual int Armor_DTElectr { get {return NativeFields.GetInt32(thisptr, offsetArmor_DTElectr); } }
		public virtual int Armor_DTEmp { get {return NativeFields.GetInt32(thisptr, offsetArmor_DTEmp); } }
		public virtual int Armor_DTExplode { get {return NativeFields.GetInt32(thisptr, offsetArmor_DTExplode); } }
		// Weapon
		public virtual int Weapon_DmgType_0 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgType_0); } }
		public virtual int Weapon_DmgType_1 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgType_1); } }
		public virtual int Weapon_DmgType_2 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgType_2); } }
		public virtual uint Weapon_Anim2_0 { get {return NativeFields.GetUInt32(thisptr, offsetWeapon_Anim2_0); } }
		public virtual uint Weapon_Anim2_1 { get {return NativeFields.GetUInt32(thisptr, offsetWeapon_Anim2_1); } }
		public virtual uint Weapon_Anim2_2 { get {return NativeFields.GetUInt32(thisptr, offsetWeapon_Anim2_2); } }
		public virtual int Weapon_DmgMin_0 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgMin_0); } }
		public virtual int Weapon_DmgMin_1 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgMin_1); } }
		public virtual int Weapon_DmgMin_2 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgMin_2); } }
		public virtual int Weapon_DmgMax_0 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgMax_0); } }
		public virtual int Weapon_DmgMax_1 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgMax_1); } }
		public virtual int Weapon_DmgMax_2 { get {return NativeFields.GetInt32(thisptr, offsetWeapon_DmgMax_2); } }
		public virtual ushort Weapon_Effect_0 { get {return NativeFields.GetUInt16(thisptr, offsetWeapon_Effect_0); } }
		public virtual ushort Weapon_Effect_1 { get {return NativeFields.GetUInt16(thisptr, offsetWeapon_Effect_1); } }
		public virtual ushort Weapon_Effect_2 { get {return NativeFields.GetUInt16(thisptr, offsetWeapon_Effect_2); } }
		public virtual bool Weapon_Remove_0 { get {return NativeFields.GetBoolean(thisptr, offsetWeapon_Remove_0); } }
		public virtual bool Weapon_Remove_1 { get {return NativeFields.GetBoolean(thisptr, offsetWeapon_Remove_1); } }
		public virtual bool Weapon_Remove_2 { get {return NativeFields.GetBoolean(thisptr, offsetWeapon_Remove_2); } }
		public virtual uint Weapon_ReloadAp { get {return NativeFields.GetUInt32(thisptr, offsetWeapon_ReloadAp); } }
		public virtual int Weapon_UnarmedCriticalBonus { get {return NativeFields.GetInt32(thisptr, offsetWeapon_UnarmedCriticalBonus); } }
		public virtual uint Weapon_CriticalFailture { get {return NativeFields.GetUInt32(thisptr, offsetWeapon_CriticalFailture); } }
		public virtual bool Weapon_UnarmedArmorPiercing { get {return NativeFields.GetBoolean(thisptr, offsetWeapon_UnarmedArmorPiercing); } }
		// Ammo
		public virtual int Ammo_ACMod { get {return NativeFields.GetInt32(thisptr, offsetAmmo_ACMod); } }
		public virtual int Ammo_DRMod { get {return NativeFields.GetInt32(thisptr, offsetAmmo_DRMod); } }
		public virtual uint Ammo_DmgMult { get {return NativeFields.GetUInt32(thisptr, offsetAmmo_DmgMult); } }
		public virtual uint Ammo_DmgDiv { get {return NativeFields.GetUInt32(thisptr, offsetAmmo_DmgDiv); } }
		
#pragma warning disable 649
	    static int offsetProtoId;
        static int offsetType;
        static int offsetPicMap;
        static int offsetPicInv;
        static int offsetFlags;
        static int offsetStackable;
        static int offsetDeteriorable;
        static int offsetGroundLevel;
        static int offsetCorner;
        static int offsetDir;
        static int offsetSlot;
        static int offsetWeight;
        static int offsetVolume;
        static int offsetCost;
        static int offsetStartCount;
        static int offsetSoundId;
        static int offsetMaterial;
        static int offsetLightFlags;
        static int offsetLightDistance;
        static int offsetLightIntensity;
        static int offsetLightColor;
        static int offsetDisableEgg;
        static int offsetAnimWaitBase;
        static int offsetAnimWaitRndMin;
        static int offsetAnimWaitRndMax;
        static int offsetAnimStay_0;
        static int offsetAnimStay_1;
        static int offsetAnimShow_0;
        static int offsetAnimShow_1;
        static int offsetAnimHide_0;
        static int offsetAnimHide_1;
        static int offsetOffsetX;
        static int offsetOffsetY;
        static int offsetSpriteCut;
        static int offsetDrawOrderOffsetHexY;
        static int offsetRadioChannel;
        static int offsetRadioFlags;
        static int offsetRadioBroadcastSend;
        static int offsetRadioBroadcastRecv;
        static int offsetIndicatorStart;
        static int offsetIndicatorMax;
        static int offsetHolodiskNum;
        static int offsetStartValue_0;
        static int offsetStartValue_1;
        static int offsetStartValue_2;
        static int offsetStartValue_3;
        static int offsetStartValue_4;
        static int offsetStartValue_5;
        static int offsetStartValue_6;
        static int offsetStartValue_7;
        static int offsetStartValue_8;
        static int offsetStartValue_9;
        static int offsetBlockLines;
        static int offsetChildPid_0;
        static int offsetChildPid_1;
        static int offsetChildPid_2;
        static int offsetChildPid_3;
        static int offsetChildPid_4;
        static int offsetChildLines_0;
        static int offsetChildLines_1;
        static int offsetChildLines_2;
        static int offsetChildLines_3;
        static int offsetChildLines_4;
        static int offsetWeapon_IsUnarmed;
        static int offsetWeapon_UnarmedTree;
        static int offsetWeapon_UnarmedPriority;
        static int offsetWeapon_UnarmedMinAgility;
        static int offsetWeapon_UnarmedMinUnarmed;
        static int offsetWeapon_UnarmedMinLevel;
        static int offsetWeapon_Anim1;
        static int offsetWeapon_MaxAmmoCount;
        static int offsetWeapon_Caliber;
        static int offsetWeapon_DefaultAmmoPid;
        static int offsetWeapon_MinStrength;
        static int offsetWeapon_Perk;
        static int offsetWeapon_ActiveUses;
        static int offsetWeapon_Skill_0;
        static int offsetWeapon_Skill_1;
        static int offsetWeapon_Skill_2;
        static int offsetWeapon_PicUse_0;
        static int offsetWeapon_PicUse_1;
        static int offsetWeapon_PicUse_2;
        static int offsetWeapon_MaxDist_0;
        static int offsetWeapon_MaxDist_1;
        static int offsetWeapon_MaxDist_2;
        static int offsetWeapon_Round_0;
        static int offsetWeapon_Round_1;
        static int offsetWeapon_Round_2;
        static int offsetWeapon_ApCost_0;
        static int offsetWeapon_ApCost_1;
        static int offsetWeapon_ApCost_2;
        static int offsetWeapon_Aim_0;
        static int offsetWeapon_Aim_1;
        static int offsetWeapon_Aim_2;
        static int offsetWeapon_SoundId_0;
        static int offsetWeapon_SoundId_1;
        static int offsetWeapon_SoundId_2;
        static int offsetAmmo_Caliber;
        static int offsetDoor_NoBlockMove;
        static int offsetDoor_NoBlockShoot;
        static int offsetDoor_NoBlockLight;
        static int offsetContainer_Volume;
        static int offsetContainer_Changeble;
        static int offsetContainer_CannotPickUp;
        static int offsetContainer_MagicHandsGrnd;
        static int offsetLocker_Condition;
        static int offsetGrid_Type;
        static int offsetCar_Speed;
        static int offsetCar_Passability;
        static int offsetCar_DeteriorationRate;
        static int offsetCar_CrittersCapacity;
        static int offsetCar_TankVolume;
        static int offsetCar_MaxDeterioration;
        static int offsetCar_FuelConsumption;
        static int offsetCar_Entrance;
        static int offsetCar_MovementType;
				
		static int offsetMagicPower;
		static int offsetArmor_CrTypeMale;
		static int offsetArmor_CrTypeFemale;
		static int offsetArmor_AC;
		static int offsetArmor_Perk;
		static int offsetArmor_DRNormal;
		static int offsetArmor_DRLaser;
		static int offsetArmor_DRFire;
		static int offsetArmor_DRPlasma;
		static int offsetArmor_DRElectr;
		static int offsetArmor_DREmp;
		static int offsetArmor_DRExplode;
		static int offsetArmor_DTNormal;
		static int offsetArmor_DTLaser;
		static int offsetArmor_DTFire;
		static int offsetArmor_DTPlasma;
		static int offsetArmor_DTElectr;
		static int offsetArmor_DTEmp;
		static int offsetArmor_DTExplode;
		static int offsetWeapon_DmgType_0;
		static int offsetWeapon_DmgType_1;
		static int offsetWeapon_DmgType_2;
		static int offsetWeapon_Anim2_0;
		static int offsetWeapon_Anim2_1;
		static int offsetWeapon_Anim2_2;
		static int offsetWeapon_DmgMin_0;
		static int offsetWeapon_DmgMin_1;
		static int offsetWeapon_DmgMin_2;
		static int offsetWeapon_DmgMax_0;
		static int offsetWeapon_DmgMax_1;
		static int offsetWeapon_DmgMax_2;
		static int offsetWeapon_Effect_0;
		static int offsetWeapon_Effect_1;
		static int offsetWeapon_Effect_2;
		static int offsetWeapon_Remove_0;
		static int offsetWeapon_Remove_1;
		static int offsetWeapon_Remove_2;
		static int offsetWeapon_ReloadAp;
		static int offsetWeapon_UnarmedCriticalBonus;
		static int offsetWeapon_CriticalFailture;
		static int offsetWeapon_UnarmedArmorPiercing;
		static int offsetAmmo_ACMod;
		static int offsetAmmo_DRMod;
		static int offsetAmmo_DmgMult;
		static int offsetAmmo_DmgDiv;
				
#pragma warning restore 649
	}
}

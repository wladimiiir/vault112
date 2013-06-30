using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public partial class Item : IManagedWrapper
    {
        readonly IntPtr thisptr;
        public Item(IntPtr ptr)
        {
            thisptr = ptr;
            AddRef();
            //Program.Log("Item created: (0x{0:x})", (int)ptr);
        }
        ~Item()
        {
            Release();
        }
        public static explicit operator IntPtr(Item self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public static explicit operator Item(IntPtr ptr)
        {
            return Global.ItemManager.FromNative(ptr);
        }
        public IntPtr ThisPtr { get { return thisptr; } }

        // for dev purposes
        static Dictionary<IntPtr, Item> items = new Dictionary<IntPtr, Item>();
        public static IEnumerable<Item> AllItems { get { return items.Values; } }

        static Item Add(IntPtr ptr)
        {
            //Program.Log("Adding item: (0x{0:x})", (int)ptr);
            if(items.ContainsKey(ptr))
                throw new InvalidOperationException(string.Format("Item 0x{0:x} already added.", (int)ptr));
            var item = new Item(ptr);
            items[ptr] = item;
            return item;
        }
        static void Remove(Item item)
        {
            //Program.Log("Removing item: {0}(0x{1:x})", item.Id, (int)item.ThisPtr);
            items.Remove(item.ThisPtr);
        }
        // locker flags
        public virtual bool LockerIsOpen
        {
            get { return (this.LockerCondition & LockerConditions.IsOpen) != 0; }
        }
        public virtual bool LockerIsClose
        {
            get { return !LockerIsOpen; }
        }
        // broken flags
        public virtual bool IsEternal
        {
            get { return (BrokenFlags & BI.Eternal) != 0; }
        }
        public virtual bool IsNotResc
        {
            get { return (BrokenFlags & BI.NotResc) != 0; }
            set { BrokenFlags |= BI.NotResc; }
        }
        public virtual bool IsBroken
        {
            get { return (BrokenFlags & BI.NotResc) != 0; }
            set { BrokenFlags |= BI.Broken; }
        }
        public virtual bool IsLowBroken
        {
            get { return (BrokenFlags & BI.LowBroken) != 0; }
            set { BrokenFlags |= BI.LowBroken; }
        }
        public virtual bool IsNormBroken
        {
            get { return (BrokenFlags & BI.NormBroken) != 0; }
            set { BrokenFlags |= BI.NormBroken; }
        }
        public virtual bool IsHighBroken
        {
            get { return (BrokenFlags & BI.HighBroken) != 0; }
            set { BrokenFlags |= BI.HighBroken; }
        }
        public virtual bool IsService
        {
            get { return (BrokenFlags & BI.Service) != 0; }
            set { BrokenFlags |= BI.Service; }
        }
        // item flags
        public virtual bool IsTrap
        {
            get { return (Flags & ItemFlag.Trap) != 0; }
            set { Flags |= ItemFlag.Trap; }
        }
        public virtual bool IsGag
        {
            get { return (Flags & ItemFlag.Gag) != 0; }
            set { Flags |= ItemFlag.Gag; }
        }
        public virtual bool IsHidden
        {
            get { return (Flags & ItemFlag.Hidden) != 0; }
            set { Flags |= ItemFlag.Hidden; }
        }
    }
    [Flags]
    public enum ItemFlag : uint
    {
        Hidden = 0x00000001,
        Flat = 0x00000002,
        NoBlock = 0x00000004,
        ShootThru = 0x00000008,
        LightThru = 0x00000010,
        MultiHex = 0x00000020, // Not used
        WallTransEnd = 0x00000040, // Not used
        TwoHands = 0x00000080,
        BigGun = 0x00000100,
        AlwaysView = 0x00000200,
        HasTimer = 0x00000400,
        BadItem = 0x00000800,
        NoHighlight = 0x00001000,
        ShowAnim = 0x00002000,
        ShowAnimExt = 0x00004000,
        Light = 0x00008000,
        GECK = 0x00010000,
        Trap = 0x00020000,
        NoLightInfluence = 0x00040000,
        NoLoot = 0x00080000,
        NoSteal = 0x00100000,
        Gag = 0x00200000,
        Colorize = 0x00400000,
        ColorizeInv = 0x00800000,
        CanUseOnSmth = 0x01000000,
        CanLook = 0x02000000,
        CanTalk = 0x04000000,
        CanPickup = 0x08000000,
        CanUse = 0x10000000,
        Holodisk = 0x20000000,
        Radio = 0x40000000,
        Cached = 0x80000000 // Not used
    }

    public static class ItemTypes
    {
        public const byte None = 0;
        public const byte Armor = 1;
        public const byte Drug = 2;
        public const byte Weapon = 3;
        public const byte Ammo = 4;
        public const byte Misc = 5;
        public const byte Key = 7;
        public const byte Container = 8;
        public const byte Door = 9;
        public const byte Grid = 10;
        public const byte Generic = 11;
        public const byte Wall = 12;
        public const byte Car = 13;
    }
    // vs.
    public enum ItemType : byte
    {
        None = 0,
        Armor,
        Drug,
        Weapon,
        Ammo,
        Misc,
        Key = 7,
        Container,
        Door,
        Grid,
        Generic,
        Wall,
        Car
    }
    public static class LockerConditions
    {
        public const ushort IsOpen = 0x01; // Used in engine
        public const ushort Locked = 0x02;
        public const ushort Jammed = 0x04;
        public const ushort Broken = 0x08;
        public const ushort NoOpen = 0x10; // Hardcoded
        public const ushort Electro = 0x20;
    }
    // vs.
    [Flags]
    public enum LockerCondition : ushort
    {
        IsOpen = 0x01,
        Locked,
        Jammed,
        Broken,
        NoOpen,
        Electro
    }
    public enum Accessory : byte
    {
        None = 0,
        Critter = 1,
        Hex = 2,
        Container = 3
    }
    [Flags]
    public enum BI : byte
    {
        LowBroken,
        NormBroken,
        HighBroken,
        NotResc,
        Broken = 0x0F,
        Service,
        ServiceExt,
        Eternal
    }
    /// <summary>
    /// ScriptArray for items.
    /// </summary>
    public sealed class ItemArray : HandleArray<Item>
    {
        static readonly IntPtr type;
        public ItemArray()
            : base(type)
        {

        }
        internal ItemArray(IntPtr ptr)
            : base(ptr, true)
        {
        }
        static ItemArray()
        {
            type = ScriptArray.GetType("array<Item@>");
        }
        public override Item FromNative(IntPtr ptr)
        {
            return (Item)GetObjectAddress(ptr);         
        }
    }
}

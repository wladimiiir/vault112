using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public class ItemEventArgs : EventArgs
    {
        public ItemEventArgs (Item item)
	    {
            this.Item = item;
	    }
        public Item Item { get; private set; }
    }
    public class ItemChainEventArgs : DefaultEventArgs
    {
        public ItemChainEventArgs (Item item)
	    {
            this.Item = item;
	    }
        public Item Item { get; private set; }
    }
    public class ItemFinishEventArgs : ItemEventArgs
    {
        public ItemFinishEventArgs(Item item, bool deleted)
            : base(item)
        {
            this.Deleted = deleted;
        }
        public bool Deleted { get; private set; }
    }
    public class ItemAttackEventArgs : ItemChainEventArgs
    {
        public ItemAttackEventArgs(Item item, Critter cr, Critter target)
            : base(item)
        {
            this.Cr = cr;
            this.Target = target;
        }
        public Critter Cr { get; private set; }
        public Critter Target { get; private set; }
    }
    public class ItemUseEventArgs : ItemChainEventArgs
    {
        public ItemUseEventArgs(Item item, Critter cr, Critter on_critter, Item on_item, Scenery on_scenery)
            : base(item)
        {
            this.Cr = cr;
            this.OnCritter = on_critter;
            this.OnItem = on_item;
            this.OnScenery = on_scenery;
        }
        public Critter Cr { get; private set; }
        public Critter OnCritter { get; private set; }
        public Item OnItem { get; private set; }
        public Scenery OnScenery { get; private set; }
    }
    public class ItemUseOnMeEventArgs : ItemChainEventArgs
    {
        public ItemUseOnMeEventArgs(Item item, Critter cr, Item used_item)
            : base(item)
        {
            this.Cr = cr;
            this.UsedItem = used_item;
        }
        public Critter Cr { get; private set; }
        public Item UsedItem { get; private set; }
    }
    public class ItemSkillEventArgs : ItemChainEventArgs
    {
        public ItemSkillEventArgs(Item item, Critter cr, int skill)
            : base(item)
        {
            this.Cr = cr;
            this.Skill =skill;
        }
        public Critter Cr { get; private set; }
        public int Skill { get; private set; }
    }
    public class ItemDropEventArgs : ItemEventArgs
    {
        public ItemDropEventArgs(Item item, Critter cr)
            : base(item)
        {
            this.Cr = cr;
        }
        public Critter Cr { get; private set; }
    }
    public class ItemMoveEventArgs : ItemEventArgs
    {
        public ItemMoveEventArgs(Item item, Critter cr, ItemSlot from_slot)
            : base(item)
        {
            this.Cr = cr;
            this.FromSlot = from_slot;
        }
        public Critter Cr { get; private set; }
        public ItemSlot FromSlot { get; private set; }
    }
    public class ItemWalkEventArgs : ItemEventArgs
    {
        public ItemWalkEventArgs(Item item, Critter cr, bool entered, Direction dir)
            : base(item)
        {
            this.Cr = cr;
            this.Entered = entered;
            this.Dir = dir;
        }
        public Critter Cr { get; private set; }
        public bool Entered { get; private set; }
        public Direction Dir { get; private set; }
    }

    public partial class Item
    {
        /// <summary>
        /// Raised when item is about to be garbaged.
        /// </summary>
        public event EventHandler<ItemFinishEventArgs> Finish;
        // called by engine
        void RaiseFinish(bool deleted)
        {
            if (Finish != null)
                Finish(this, new ItemFinishEventArgs(this, deleted));
        }
        /// <summary>
        /// Raised when item is used by critter to attack target critter.
        /// </summary>
        public event EventHandler<ItemAttackEventArgs> Attack;
        // called by engine
        bool RaiseAttack(Critter cr, Critter target)
        {
            if (Attack != null)
            {
                var e = new ItemAttackEventArgs(this, cr, target);
                Attack(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when item is used on something.
        /// </summary>
        public event EventHandler<ItemUseEventArgs> Use;
        // called by engine
        public bool RaiseUse(Critter cr, Critter on_critter, Item on_item, IntPtr on_scenery)
        {
            if (Use != null)
            {
                var e = new ItemUseEventArgs(this, cr, on_critter, on_item, Scenery.FromNative(on_scenery));
                Use(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when some item is used on this item.
        /// </summary>
        public event EventHandler<ItemUseOnMeEventArgs> UseOnMe;
        // called by engine
        bool RaiseUseOnMe(Critter cr, Item used_item)
        {
            if (UseOnMe != null)
            {
                var e = new ItemUseOnMeEventArgs(this, cr, used_item);
                UseOnMe(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when critter uses skill on item.
        /// </summary>
        public event EventHandler<ItemSkillEventArgs> Skill;
        // called by engine
        bool RaiseSkill(Critter cr, int skill)
        {
            if (Skill != null)
            {
                var e = new ItemSkillEventArgs(this, cr, skill);
                Skill(this, e);
                return e.Prevent;
            }
            return false;
        }
        /// <summary>
        /// Raised when item is dropped.
        /// </summary>
        public event EventHandler<ItemDropEventArgs> Drop;
        // called by native
        void RaiseDrop(Critter cr)
        {
            if (Drop != null)
                Drop(this, new ItemDropEventArgs(this, cr));
        }
        /// <summary>
        /// Raised when item is moved from slot.
        /// </summary>
        public event EventHandler<ItemMoveEventArgs> Move;
        // called by engine
        void RaiseMove(Critter cr, byte from_slot)
        {
            if (Move != null)
                Move(this, new ItemMoveEventArgs(this, cr, (ItemSlot)from_slot));
        }
        /// <summary>
        /// Raised when critter walks over(enter or leaves) the item lying on ground.
        /// </summary>
        public event EventHandler<ItemWalkEventArgs> Walk;
        // called by engine
        void RaiseWalk(Critter cr, bool entered, byte dir)
        {
            if (Walk != null)
                Walk(this, new ItemWalkEventArgs(this, cr, entered, (Direction)dir));
        }
    }
}

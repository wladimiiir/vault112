using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public partial class Item
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_IsStackable(IntPtr thisptr);
		public virtual bool IsStackable
		{
			get { return Item_IsStackable(thisptr); }
		}
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_IsDeteriorable(IntPtr thisptr);
        /*public virtual bool IsDeteriorable()
        {
            return Item_IsDeteriorable(thisptr);
        }*/
        public virtual bool IsDeteriorable
        {
            get { return Item_IsDeteriorable(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_SetScript(IntPtr thisptr, IntPtr script);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Item_GetScriptId(IntPtr thisptr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_SetEvent(IntPtr thisptr, int event_type, IntPtr func_name);
        // not allowed?

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static byte Item_GetType(IntPtr thisptr);
        public new virtual byte GetType()
        {
            return Item_GetType(thisptr);
        }
        public virtual ItemType Type
        {
            get { return (ItemType)Item_GetType(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Item_GetProtoId(IntPtr thisptr);
        public virtual ushort GetProtoId()
        {
            return Item_GetProtoId(thisptr);
        }
        public virtual ushort ProtoId
        {
            get { return Item_GetProtoId(thisptr); }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Item_GetCount(IntPtr thisptr);
        public virtual uint GetCount()
        {
            return Item_GetCount(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_SetCount(IntPtr thisptr, uint count);
        public virtual void SetCount(uint count)
        {
            Item_SetCount(thisptr, count);
        }
        public virtual uint Count
        {
            get { return Item_GetCount(thisptr); }
            set { Item_SetCount(thisptr, value); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Item_GetCost(IntPtr thisptr);
        public virtual uint GetCost(IntPtr thisptr)
        {
            return Item_GetCost(thisptr);
        }
        /*public virtual uint Cost
        {
            get { return Item_GetCost(thisptr); }
        }*/

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Container_AddItem(IntPtr thisptr, ushort pid, uint count, uint special_id);
        public virtual Item AddItem(ushort pid, uint count, uint special_id)
        {
            return (Item)Container_AddItem(thisptr, pid, count, special_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Container_GetItem(IntPtr thisptr, ushort pid, uint special_id);
        public virtual Item GetItem(ushort pid, uint special_id)
        {
            return (Item)Container_GetItem(thisptr, pid, special_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Container_GetItems(IntPtr thisptr, uint special_id, IntPtr items);
        public virtual uint GetItems(uint special_id, ItemArray items)
        {
            return Container_GetItems(thisptr, special_id, (IntPtr)items);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Item_GetMapPosition(IntPtr thisptr, ref ushort hx, ref ushort hy);
        public virtual Map GetMapPosition(ref ushort hx, ref ushort hy)
        {
            return (Map)Item_GetMapPosition(thisptr, ref hx, ref hy);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_ChangeProto(IntPtr thisptr, ushort pid);
        public virtual bool ChangeProto(ushort pid)
        {
            return Item_ChangeProto(thisptr, pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_Update(IntPtr thisptr);
        public virtual void Update()
        {
            Item_Update(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_Animate(IntPtr thisptr, byte from_frame, byte to_frame);
        public virtual void Animate(byte from_frame, byte to_frame)
        {
            Item_Animate(thisptr, from_frame, to_frame);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_SetLexems(IntPtr thisptr, IntPtr lexems);
        public virtual void SetLexems(string lexems)
        {
			var ss = new ScriptString(lexems);
            Item_SetLexems(thisptr, ss.ThisPtr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Item_GetChild(IntPtr thisptr, uint child_index);
        public virtual Item GetChild(uint child_index)
        {
            return (Item)Item_GetChild(thisptr, child_index);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_EventFinish(IntPtr thisptr, bool deleted);
        public virtual void EventFinish(bool deleted)
        {
            Item_EventFinish(thisptr, deleted);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_EventAttack(IntPtr thisptr, IntPtr attacker, IntPtr target);
        public virtual bool EventAttack(Critter attacker, Critter target)
        {
            return Item_EventAttack(thisptr, attacker.ThisPtr, target.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_EventUse(IntPtr thisptr, IntPtr cr, IntPtr on_critter, IntPtr on_item, IntPtr on_scenery);
        public virtual bool EventUse(Critter cr, Critter on_critter, Item on_item, Scenery on_scenery)
        {
            return Item_EventUse(thisptr, cr.ThisPtr, (IntPtr)on_critter, (IntPtr)on_item, (IntPtr)on_scenery);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_EventUseOnMe(IntPtr thisptr, IntPtr cr, IntPtr used_item);
        public virtual bool EventUseOnMe(Critter cr, Item used_item)
        {
            return Item_EventUseOnMe(thisptr, cr.ThisPtr, (IntPtr)used_item);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Item_EventSkill(IntPtr thisptr, IntPtr cr, int skill);
        public virtual bool EventSkill(Critter cr, int skill)
        {
            return Item_EventSkill(thisptr, cr.ThisPtr, skill);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_EventDrop(IntPtr thisptr, IntPtr cr);
        public virtual void EventDrop(Critter cr)
        {
            Item_EventDrop(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_EventMove(IntPtr thisptr, IntPtr cr, byte from_slot);
        public virtual void EventMove(Critter cr, ItemSlot from_slot)
        {
            Item_EventMove(thisptr, cr.ThisPtr, (byte)from_slot);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Item_EventWalk(IntPtr thisptr, IntPtr cr, bool entered, byte dir);
        public virtual void EvenWalk(Critter cr, bool entered, Direction dir)
        {
            Item_EventWalk(thisptr, cr.ThisPtr, entered, (byte)dir);
        }


        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Item_GetProto(IntPtr thisptr);
        /*public virtual ProtoItem Proto
        {
            get { return new ProtoItem(Item_GetProto(thisptr)); }
        }*/

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern uint Item_get_Flags(IntPtr thisptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern void Item_set_Flags(IntPtr thisptr, uint value);
        public virtual ItemFlag Flags
        {
            get { return (ItemFlag)Item_get_Flags(thisptr); }
            set { Item_set_Flags(thisptr, (uint)value); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern short Item_get_TrapValue(IntPtr thisptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern void Item_set_TrapValue(IntPtr thisptr, short value);
        public virtual short TrapValue
        {
            get { return Item_get_TrapValue(thisptr); }
            set { Item_set_TrapValue(thisptr, value); }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr ptr);
        public virtual void AddRef()
        {
            AddRef(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr ptr);
        public virtual void Release()
        {
            Release(thisptr);
        }
    }
}

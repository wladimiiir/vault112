using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public interface IItemManager
    {
        Item FromNative(IntPtr ptr);
        Item GetItem(uint item_id);
        void MoveItem(Item item, uint count, Critter to_cr);
        void MoveItem(Item item, uint count, Item to_cont, uint stack_id);
        void MoveItem(Item item, uint count, Map to_map, ushort to_hx, ushort to_hy);
        void MoveItems(ItemArray items, Critter to_cr);
        void MoveItems(ItemArray items, Item to_cont, uint stack_id);
        void MoveItems(ItemArray items, Map to_map, ushort to_hx, ushort to_hy);
        void DeleteItem(Item item);
        void DeleteItems(ItemArray items);
        ulong WorldItemCount(ushort pid);
        uint GetAllItems(ushort pid, ItemArray items);
    }
    public class ItemManager : IItemManager
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static Item Item_FromNative(IntPtr ptr);
        public Item FromNative(IntPtr ptr)
        {
            return Item_FromNative(ptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetItem(uint item_id);
        public Item GetItem(uint item_id)
        {
            return (Item)Global_GetItem(item_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_MoveItemCr(IntPtr item, uint count, IntPtr to_cr);
        public void MoveItem(Item item, uint count, Critter to_cr)
        {
            Global_MoveItemCr(item.ThisPtr, count, to_cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_MoveItemCont(IntPtr item, uint count, IntPtr to_cont, uint stack_id);
        public void MoveItem(Item item, uint count, Item to_cont, uint stack_id)
        {
            Global_MoveItemCont(item.ThisPtr, count, to_cont.ThisPtr, stack_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_MoveItemMap(IntPtr item, uint count, IntPtr to_map, ushort to_hx, ushort to_hy);
        public void MoveItem(Item item, uint count, Map to_map, ushort to_hx, ushort to_hy)
        {
            Global_MoveItemMap(item.ThisPtr, count, to_map.ThisPtr, to_hx, to_hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_MoveItemsCr(IntPtr items, IntPtr to_cr);
        public void MoveItems(ItemArray items, Critter to_cr)
        {
            Global_MoveItemsCr(items.ThisPtr, to_cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_MoveItemsCont(IntPtr items, IntPtr to_cont, uint stack_id);
        public void MoveItems(ItemArray items, Item to_cont, uint stack_id)
        {
            Global_MoveItemsCont(items.ThisPtr, to_cont.ThisPtr, stack_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_MoveItemsMap(IntPtr items, IntPtr to_map, ushort to_hx, ushort to_hy);
        public void MoveItems(ItemArray items, Map to_map, ushort to_hx, ushort to_hy)
        {
            Global_MoveItemsMap(items.ThisPtr, to_map.ThisPtr, to_hx, to_hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_DeleteItem(IntPtr item);
        public void DeleteItem(Item item)
        {
            Global_DeleteItem(item.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_DeleteItems(IntPtr items);
        public void DeleteItems(ItemArray items)
        {
            Global_DeleteItems(items.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ulong Global_WorldItemCount(ushort pid);
        public ulong WorldItemCount(ushort pid)
        {
            return Global_WorldItemCount(pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_GetAllItems(ushort pid, IntPtr array);
        public uint GetAllItems(ushort pid, ItemArray items)
        {
            return Global_GetAllItems(pid, (IntPtr)items);
        }
    }
}

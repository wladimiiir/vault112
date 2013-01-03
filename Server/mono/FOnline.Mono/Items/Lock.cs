using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline.Items
{
    public class Lock
    {
        public static Item item_init(IntPtr ptr, bool firstTime)
        {
            var item = new Item(ptr);
            Init(item);
            return item;
        }
        // having this as separate method working on instance of item
        // allows us to substitute it with mock
        public static void Init(Item item)
        {
            item.Use += _Use;
            item.LockerComplexity = (ushort)Global.Random(50, 100);
        }
        static void _Use(object sender, ItemUseEventArgs e)
        {
            var on_item = e.OnItem;
            var item = sender as Item;
 	        if(on_item == null || on_item.Type != ItemType.Container)
		        return; // that does nothing
	
	        // already locked
	        if(on_item.LockerIsClose)
		        return;
	
	        uint lock_id = (uint)Global.Random(1, 65535);
	
	        on_item.LockerId = lock_id;
	        on_item.LockerComplexity = item.LockerComplexity;
	
	        // remove it
	        if (item.GetCount() > 1)
	            item.SetCount(item.GetCount() - 1);
	        else
                Global.DeleteItem(item);
	        // give key
	        var key = e.Cr.AddItem((ushort)Pid.KEY, 1);
	        key.LockerId = lock_id;
	        key.Update();
	        // info
	        e.Cr.SayMsg(Say.NetMsg, TextMsg.Text, 4400);
            e.PreventDefaults();
        }
    }
}

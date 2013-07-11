using System;

namespace FOnline.BT
{
	public abstract class ItemCheckCondition<B> : Condition<B, Item> where B : Blackboard
	{
		//public abstract bool Check(Item checkItem);
	}
}


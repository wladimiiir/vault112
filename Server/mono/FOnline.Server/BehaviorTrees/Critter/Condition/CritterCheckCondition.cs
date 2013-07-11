using System;

namespace FOnline.BT
{
	public abstract class CritterCheckCondition<B> : Condition<B, Critter> where B : Blackboard
	{
		//public abstract bool Check(Critter checkCritter);
	}
}


using System;

namespace FOnline.BT
{
	public abstract class CritterFilter<B> : EntityFilter<B, Critter> where B : Blackboard
	{
	}
}


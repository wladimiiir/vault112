using System;

namespace FOnline.BT
{
	public class AtChance<B> : LeafTask<B> where B : Blackboard
	{
		private uint chance;

		public AtChance (uint chance)
		{
			this.chance = chance;
		}

		public override TaskState Execute ()
		{
			return Global.HasChance (chance) ? TaskState.Success : TaskState.Failed;
		}
	}
}


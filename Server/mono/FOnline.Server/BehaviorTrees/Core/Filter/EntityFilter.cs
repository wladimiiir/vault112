using System;
using System.Collections.Generic;

namespace FOnline.BT
{
	public abstract class Filter<B, T> where B : Blackboard
	{
		private B blackboard;

		public B Blackboard {
			set {
				blackboard = value;
			}
		}

		protected virtual B GetBlackboard ()
		{
			return blackboard;
		}

		public abstract IList<T> FilterEntities (IList<T> entitites);
	}
}


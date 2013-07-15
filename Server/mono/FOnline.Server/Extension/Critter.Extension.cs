using System;

namespace FOnline
{
	public partial class Critter
	{
		public void DeleteItems ()
		{
			var items = new ItemArray ();
			GetItems (null, items);
			Global.DeleteItems (items);
		}
	}
}


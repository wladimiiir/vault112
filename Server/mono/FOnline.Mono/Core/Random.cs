using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
	public interface IRandom
	{
		int Random (int min, int max);
		// something to ease all those simple type conversions (wonder about performance)
		IConvertible Random (IConvertible min, IConvertible max);

		bool HasChance (float chance);
	}

	public class Randomizer : IRandom
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static int _Random (int min, int max);

		public int Random (int min, int max)
		{
			return _Random (min, max);
		}

		public IConvertible Random (IConvertible min, IConvertible max)
		{
			return _Random (min.ToInt32 (null), max.ToInt32 (null));
		}

		public bool HasChance (float chance)
		{
			return (float)_Random (1, 10000) / 100f <= chance;
		}
	}
}

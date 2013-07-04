using System;

namespace FOnline
{
	public interface ICondition<T>
	{
		bool Check(T checkEntity);
	}
}


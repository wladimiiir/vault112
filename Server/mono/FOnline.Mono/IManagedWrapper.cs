using System;

namespace FOnline
{
    public interface IManagedWrapper
    {
        IntPtr ThisPtr { get; }
		void AddRef();
		void Release();
    }
}
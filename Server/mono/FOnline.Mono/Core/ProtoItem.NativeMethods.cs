using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public partial class ProtoItem
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void Release(IntPtr ptr);
    }
}

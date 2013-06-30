using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public partial class Scenery
    {
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //extern static Scenery _FromNative(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr ptr);

        /// <summary>
        /// Retrieves managed object basing on a native pointer to MapObject.
        /// </summary>
        /// <remarks>
        /// This method instantiates managed object every time because we are not storing the reference anywhere
        /// on the outside (like we do for critters/items/maps etc) in this case (to save memory). 
        /// </remarks>
        /// <param name="ptr"></param>
        /// <returns></returns>
        internal static Scenery FromNative(IntPtr ptr)
        {
            return new Scenery(ptr);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public partial class Location
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Location_GetProtoId(IntPtr thisptr);
        public virtual ushort GetProtoId()
        {
            return Location_GetProtoId(thisptr);
        }
        public virtual ushort ProtoId
        {
            get { return Location_GetProtoId(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Location_GetMapCount(IntPtr thisptr);
        public virtual uint GetMapCount()
        {
            return Location_GetMapCount(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Location_GetMap(IntPtr thisptr, ushort pid);
        public virtual Map GetMap(ushort pid)
        {
            return (Map)Location_GetMap(thisptr, pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Location_GetMapByIndex(IntPtr thisptr, uint index);
        public virtual Map GetMapByIndex(uint index)
        {
            return (Map)Location_GetMapByIndex(thisptr, index);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Location_GetMaps(IntPtr thisptr, IntPtr maps);
        public virtual uint GetMaps(MapArray maps)
        {
            return Location_GetMaps(thisptr, (IntPtr)maps);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Location_Reload(IntPtr thisptr);
        public virtual void Reload()
        {
            Location_Reload(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Location_Update(IntPtr thisptr);
        public virtual void Update()
        {
            Location_Update(thisptr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr ptr);
        public virtual void AddRef()
        {
            AddRef(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr ptr);
        public virtual void Release()
        {
            Release(thisptr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetLocation(uint loc_id);
        public static Location GetLocation(uint loc_id)
        {
            return new Location(Global_GetLocation(loc_id));
        }
    }
}

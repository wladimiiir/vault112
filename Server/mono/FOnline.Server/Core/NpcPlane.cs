using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public partial class NpcPlane : IManagedWrapper
    {
        IntPtr thisptr;

        public NpcPlane(IntPtr ptr)
        {
            thisptr = ptr;
            AddRef();
        }
        ~NpcPlane()
        {
            Release();
        }

        public IntPtr ThisPtr { get { return thisptr; } }
    }
    public enum PlaneType
    {
        Misc = 0,
        Attack = 1,
        Walk = 2,
        Pick = 3,
        Patrol = 4, // WIP
        Courier = 5 // WIP
    }
    public static class Priorities
    {
        public const uint Misc = 10;
        public const uint Attack = 50;
        public const uint Walk = 20;
        public const uint Pick = 35;
        public const uint Patrol = 25;
        public const uint Courier = 30;
    }
    public sealed class NpcPlaneArray : HandleArray<NpcPlane>
    {
        static readonly IntPtr type;
        public NpcPlaneArray()
            : base(type)
        {

        }
        internal NpcPlaneArray(IntPtr ptr)
            : base(ptr, true)
        {
        }
        static NpcPlaneArray()
        {
            type = ScriptArray.GetType("array<NpcPlane@>");
        }
        public override NpcPlane FromNative(IntPtr ptr)
        {
            return new NpcPlane(GetObjectAddress(ptr));
        }
    }
}

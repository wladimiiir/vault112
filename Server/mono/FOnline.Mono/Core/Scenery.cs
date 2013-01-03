using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public partial class Scenery : IManagedWrapper
    {
        readonly IntPtr thisptr;
        internal Scenery(IntPtr ptr)
        {
            thisptr = ptr;
            AddRef();
        }
        ~Scenery()
        {
            Release();
        }
        public static explicit operator IntPtr(Scenery self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }

        public IntPtr ThisPtr { get { return thisptr; } }
		public virtual void AddRef ()
		{
			AddRef (thisptr);
		}
		public virtual void Release()
		{
			Release (thisptr);
		}
    }
    public sealed class SceneryArray : HandleArray<Scenery>
    {
        static readonly IntPtr type;
        public SceneryArray()
            : base(type)
        {
            Global.Log("Creating SceneryArray({0})", type);
        }
        internal SceneryArray (IntPtr ptr)
            : base(ptr, true)
	    {

	    }
        static SceneryArray()
        {
            type = ScriptArray.GetType("Scenery@[]");
        }
        public override Scenery FromNative(IntPtr ptr)
        {
            return new Scenery(GetObjectAddress(ptr));
        }
    }
}

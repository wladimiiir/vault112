using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    /// <summary>
    /// String that holds pointer to Angelscript registered string
    /// </summary>
    public class ScriptString : IManagedWrapper
    {
        IntPtr thisptr;

        public ScriptString(string s)
        {
            thisptr = Create(s);
        }
        ~ScriptString()
        {
            Release();
        }
        /// <summary>
        /// Instantiates object that wraps existing instance pointed by pointer.
        /// </summary>
        /// <param name="ptr"></param>
        public ScriptString(IntPtr ptr)
        {
            thisptr = ptr;
            // we need to tell the engine that we're holding one reference to it
            AddRef();
        }

        public static implicit operator IntPtr(ScriptString self) // by having it implicit, it's possible to simply use dynamic type resolution to call SetArgObject(..., IntPtr) when using it as argument for DLR AS call
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public static explicit operator ScriptString(string str)
        {
            return new ScriptString(str);
        }
		public static explicit operator ScriptString(IntPtr ptr)
		{
			return new ScriptString(ptr);
		}
        public IntPtr ThisPtr { get { return thisptr; } }

        public void Set(string str)
        {
            Set(ThisPtr, str);
        }

        public override string ToString()
        {
            return ToString(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Create(string str);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Set(IntPtr thisptr, string str);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static string ToString(IntPtr ptr);
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
    }
	public sealed class ScriptStringHandleArray : HandleArray<ScriptString>
    {
        static readonly IntPtr type;
        public ScriptStringHandleArray() : base(type) {}
        internal ScriptStringHandleArray(IntPtr ptr) : base(ptr, true) {}
        static ScriptStringHandleArray() { type = ScriptArray.GetType("array<string@>"); }
        public override ScriptString FromNative(IntPtr ptr) { return (ScriptString)GetObjectAddress(ptr); }
    }
	public sealed class ScriptStringArray : ScriptArray<ScriptString>
    {
        static readonly IntPtr type;
        public ScriptStringArray() : base(type) {}
        internal ScriptStringArray(IntPtr ptr) : base(ptr, true) {}
        static ScriptStringArray() { type = ScriptArray.GetType("array<string>"); }
        public override ScriptString FromNative(IntPtr ptr) { return (ScriptString)ptr; }
		public override void ToNative(IntPtr ptr, ScriptString value) { throw new NotImplementedException(); }
    }
}

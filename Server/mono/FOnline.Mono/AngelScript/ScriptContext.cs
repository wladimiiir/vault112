using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FOnline.AngelScript
{
    /// <summary>
    /// Exposes the functionality of asIScriptContext implementation.
    /// </summary>
    public partial class ScriptContext : IDisposable
    {
        readonly IntPtr thisptr;
        List<GCHandle> gchandles = new List<GCHandle>();

        public ScriptContext(IntPtr ptr)
        {
            this.thisptr = ptr;
            AddRef();
        }
        ~ScriptContext()
        {
			if(!disposed)
            	Dispose();
        }
		bool disposed = false;
		public void Dispose()
		{
            if(disposed) return;
            foreach(var gch in gchandles)
                gch.Free();
			Release ();
			disposed = true;
		}

        public IntPtr ThisPtr { get { return thisptr; } }

        public static explicit operator IntPtr(ScriptContext self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public static explicit operator ScriptContext(IntPtr ptr)
        {
            return ptr == IntPtr.Zero ? null : new ScriptContext(ptr);
        }
		
		public object GetResult(int ret_tid)
        {
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_VOID)
                return null; // ?
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_BOOL)
                return GetReturnByte () != 0;
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_INT8 || ret_tid == (int)asETypeIdFlags.asTYPEID_UINT8)
                return GetReturnByte ();
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_INT16 || ret_tid == (int)asETypeIdFlags.asTYPEID_UINT16)
                return GetReturnWord ();
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_INT32 || ret_tid == (int)asETypeIdFlags.asTYPEID_UINT32)
                return GetReturnDWord ();
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_INT64 || ret_tid == (int)asETypeIdFlags.asTYPEID_UINT64)
                return GetReturnQWord ();
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_FLOAT)
                return GetReturnFloat ();
            if(ret_tid == (int)asETypeIdFlags.asTYPEID_DOUBLE)
                return GetReturnDouble ();
            // try some predefined types we can handle using our 'static' counterparts
            IntPtr ptr;
            if((ret_tid & (int)asETypeIdFlags.asTYPEID_OBJHANDLE) != 0)
                ptr = GetReturnObject ();
            else
                ptr = GetReturnAddress ();
            if (ptr == IntPtr.Zero)
                return null;
            try
            {
                var obj = ScriptObjectType.Instantiate(ptr, ret_tid);
                if (obj != null)
                    return obj;
                else
                    // just handle it dynamically
                    return new ScriptObject(ptr, ScriptEngine.GetObjectTypeById(ret_tid));
            }
            finally
            {
                if ((ret_tid & (int)asETypeIdFlags.asTYPEID_OBJHANDLE) != 0)
                {
                    // decrease refcounter, we want only newly created wrapper to hold reference
                    ScriptEngine.ReleaseScriptObject(ptr, ret_tid);
                } // otherwise, it's reference
            }
		}
        public void SetArg(uint arg, object value)
        {
            asETypeModifiers pmod;
            var tid = func.GetParamTypeId (arg, out pmod);
            if(pmod == asETypeModifiers.asTM_NONE) {
                switch (tid) 
                {
                case (int)asETypeIdFlags.asTYPEID_BOOL:
                    SetArgBool (arg, Convert.ToBoolean (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_INT8:
                    SetArgByte (arg, (byte)Convert.ToSByte (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_INT16:
                    SetArgWord (arg, (ushort)(short)(value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_INT32:
                    if(pmod == asETypeModifiers.asTM_NONE)
                        SetArgDWord (arg, (uint)Convert.ToInt32 (value));
                    else {
                        var gch = GCHandle.Alloc (value, GCHandleType.Pinned);
                        int i = 0;
                        unsafe {
                            i = *(int*)gch.AddrOfPinnedObject ();
                        }
                        SetArgAddress (arg, gch.AddrOfPinnedObject ());
                        gchandles.Add (gch);
                    }
                    break;
                case (int)asETypeIdFlags.asTYPEID_INT64:
                    SetArgQWord (arg, (ulong)Convert.ToInt64 (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_UINT8:
                    SetArgByte (arg, Convert.ToByte (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_UINT16:
                    SetArgWord (arg, Convert.ToUInt16 (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_UINT32:
                    SetArgDWord (arg, Convert.ToUInt32 (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_UINT64:
                    SetArgQWord (arg, Convert.ToUInt64 (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_FLOAT:
                    SetArgFloat (arg, Convert.ToSingle (value));
                    break;
                case (int)asETypeIdFlags.asTYPEID_DOUBLE:
                    SetArgDouble (arg, Convert.ToDouble (value));
                    break;
                default:
                    if((tid & (int)asETypeIdFlags.asTYPEID_OBJHANDLE) != 0)
                    {
                        gchandles.Add(GCHandle.Alloc(value, GCHandleType.Pinned));
                        var ptr = ((IManagedWrapper)value).ThisPtr;
                        SetArgObject (arg, ptr);
                    }
                    else
                        throw new InvalidOperationException("Unable to handle type: " + tid);
                    break;
                }
            } 
            else // by reference
            {
                switch(tid)
                {
                case (int)asETypeIdFlags.asTYPEID_BOOL:
                case (int)asETypeIdFlags.asTYPEID_INT8:
                case (int)asETypeIdFlags.asTYPEID_INT16:
                case (int)asETypeIdFlags.asTYPEID_INT32:
                case (int)asETypeIdFlags.asTYPEID_INT64:
                case (int)asETypeIdFlags.asTYPEID_UINT8:
                case (int)asETypeIdFlags.asTYPEID_UINT16:
                case (int)asETypeIdFlags.asTYPEID_UINT32:
                case (int)asETypeIdFlags.asTYPEID_UINT64:
                case (int)asETypeIdFlags.asTYPEID_FLOAT:
                case (int)asETypeIdFlags.asTYPEID_DOUBLE:
                    // we need to pin the boxed value types
                    var h = GCHandle.Alloc(value, GCHandleType.Pinned);
                    gchandles.Add(h);
                    SetArgAddress(arg, h.AddrOfPinnedObject()); 
                    break;
                default:
                    if ((tid & (int)asETypeIdFlags.asTYPEID_OBJHANDLE) != 0)
                    {
                        throw new NotImplementedException("This would require ability to alter the ThisPtr after the execution.");
                        unsafe
                        {
                            gchandles.Add(GCHandle.Alloc(value, GCHandleType.Pinned));
                            var ptr = ((IManagedWrapper)value).ThisPtr;
                            SetArgAddress(arg, new IntPtr(&ptr)); // pointer to local var :S
                        }
                    }
                    else if (((tid & (int)asETypeIdFlags.asTYPEID_SCRIPTOBJECT) != 0)
                            || ((tid & (int)asETypeIdFlags.asTYPEID_APPOBJECT) != 0))
                    {
                        var ptr = ((IManagedWrapper)value).ThisPtr;
                        // we could also mimic the AS behaviour here... (copying for in/out refs)
                        SetArgAddress (arg, ptr);
                    }
                    else
                        throw new InvalidOperationException("Cannot handle reference to type: " + tid);
                    break;
                }
            }
        }
    }
}

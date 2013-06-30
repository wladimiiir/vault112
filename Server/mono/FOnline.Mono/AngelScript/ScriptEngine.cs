using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Dynamic;

namespace FOnline.AngelScript
{
    /// <summary>
    /// Exposes functionality of asIScriptEngine collection of functions
    /// </summary>
    /// <remarks>
    /// This is not wrapper, because AngelScript engine is held as singleton in FOserv,
    /// no reasong to wrap a pointer and track references (as engine is created before mono
    /// and destroyed after).
    /// </remarks>
    public enum asERetCodes 
	{ 
		asSUCCESS = 0, 
		asERROR = -1, 
		asCONTEXT_ACTIVE = -2, 
		asCONTEXT_NOT_FINISHED = -3, 
		asCONTEXT_NOT_PREPARED = -4, 
		asINVALID_ARG = -5, 
		asNO_FUNCTION = -6, 
		asNOT_SUPPORTED = -7, 
		asINVALID_NAME = -8, 
		asNAME_TAKEN = -9, 
		asINVALID_DECLARATION = -10, 
		asINVALID_OBJECT = -11, 
		asINVALID_TYPE = -12, 
		asALREADY_REGISTERED = -13, 
		asMULTIPLE_FUNCTIONS = -14, 
		asNO_MODULE = -15, 
		asNO_GLOBAL_VAR = -16, 
		asINVALID_CONFIGURATION = -17, 
		asINVALID_INTERFACE = -18, 
		asCANT_BIND_ALL_FUNCTIONS = -19, 
		asLOWER_ARRAY_DIMENSION_NOT_REGISTERED = -20, 
		asWRONG_CONFIG_GROUP = -21, 
		asCONFIG_GROUP_IS_IN_USE = -22, 
		asILLEGAL_BEHAVIOUR_FOR_TYPE = -23, 
		asWRONG_CALLING_CONV = -24, 
		asBUILD_IN_PROGRESS = -25, 
		asINIT_GLOBAL_VARS_FAILED = -26, 
		asOUT_OF_MEMORY = -27 
	}
	public enum asEContextState 
	{ 
		asEXECUTION_FINISHED = 0, 
		asEXECUTION_SUSPENDED = 1, 
		asEXECUTION_ABORTED = 2, 
		asEXECUTION_EXCEPTION = 3, 
		asEXECUTION_PREPARED = 4, 
		asEXECUTION_UNINITIALIZED = 5, 
		asEXECUTION_ACTIVE = 6, 
		asEXECUTION_ERROR = 7 
	}
	public enum asEGMFlags 
	{ 
  		asGM_ONLY_IF_EXISTS = 0, 
  		asGM_CREATE_IF_NOT_EXISTS = 1, 
  		asGM_ALWAYS_CREATE = 2 
	}
	[Flags]
	public enum asETypeIdFlags 
	{
		asTYPEID_VOID = 0, 
		asTYPEID_BOOL = 1, 
		asTYPEID_INT8 = 2, 
		asTYPEID_INT16 = 3, 
		asTYPEID_INT32 = 4, 
		asTYPEID_INT64 = 5, 
		asTYPEID_UINT8 = 6, 
		asTYPEID_UINT16 = 7, 
		asTYPEID_UINT32 = 8, 
		asTYPEID_UINT64 = 9, 
		asTYPEID_FLOAT = 10, 
		asTYPEID_DOUBLE = 11, 
		asTYPEID_OBJHANDLE = 0x40000000, 
		asTYPEID_HANDLETOCONST = 0x20000000, 
		asTYPEID_MASK_OBJECT = 0x1C000000, 
		asTYPEID_APPOBJECT = 0x04000000, 
		asTYPEID_SCRIPTOBJECT = 0x08000000, 
		asTYPEID_TEMPLATE = 0x10000000, 
		asTYPEID_MASK_SEQNBR = 0x03FFFFFF 
	}
    public enum asETypeModifiers
    {
        asTM_NONE = 0,
        asTM_INREF = 1,
        asTM_OUTREF = 2,    
        asTM_INOUTREF = 3
    }   

	public static partial class ScriptEngine
    {
		public static void SetVariable(IntPtr ptr, int tid, object value)
		{
			unsafe
			{
				if(tid == (int)asETypeIdFlags.asTYPEID_BOOL)
					*(bool*)ptr = Convert.ToBoolean(value);
				else if(tid == (int)asETypeIdFlags.asTYPEID_INT8)
					*(sbyte*)ptr = Convert.ToSByte(value);
				else if(tid == (int)asETypeIdFlags.asTYPEID_UINT8)
					*(byte*)ptr = Convert.ToByte (value);
				else if(tid == (int)asETypeIdFlags.asTYPEID_INT16)
					*(short*)ptr = Convert.ToInt16(value);
				else if(tid == (int)asETypeIdFlags.asTYPEID_UINT16)
					*(ushort*)ptr = Convert.ToUInt16(value);
				else if(tid == (int)asETypeIdFlags.asTYPEID_INT32)
					*(int*)ptr = Convert.ToInt32(value);
				else if(tid == (int)asETypeIdFlags.asTYPEID_UINT32)
					*(uint*)ptr = Convert.ToUInt32(value);
				/*else if(tid == (int)asETypeIdFlags.asTYPEID_INT64)
					*(long*)ptr = Convert.To
				else if(tid == (int)asETypeIdFlags.asTYPEID_UINT64)
					*(ulong*)ptr = (ulong)value;*/
				else if(tid == (int)asETypeIdFlags.asTYPEID_FLOAT)
					*(float*)ptr = Convert.ToSingle(value);
				else if(tid == (int)asETypeIdFlags.asTYPEID_DOUBLE)
					*(double*)ptr = Convert.ToDouble(value);
				else throw new InvalidOperationException("not yet");
			}
		}
		public static object GetVariable(IntPtr ptr, int tid, object cached = null)
		{
			unsafe
			{
				if(tid == (int)asETypeIdFlags.asTYPEID_BOOL)
					return *(bool*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_INT8)
					return *(sbyte*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_UINT8)
					return *(byte*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_INT16)
					return *(short*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_UINT16)
					return *(ushort*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_INT32)
					return *(int*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_UINT32)
					return *(uint*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_INT64)
					return *(long*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_UINT64)
					return *(ulong*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_FLOAT)
					return *(float*)ptr;
				if(tid == (int)asETypeIdFlags.asTYPEID_DOUBLE)
					return *(double*)ptr;
				if((tid & (int)asETypeIdFlags.asTYPEID_OBJHANDLE) != 0)
					unsafe { return GetVariable(*(IntPtr*)ptr.ToPointer(), tid ^ (int)asETypeIdFlags.asTYPEID_OBJHANDLE); }
				// once we handled value types, let's check if we've got an instance cached already
				if(cached != null)
					return cached;
				// try some predefined types we can handle using our 'static' counterparts
				var obj = ScriptObjectType.Instantiate(ptr, tid);
				if(obj != null)
					return obj;
				else if((tid & (int)asETypeIdFlags.asTYPEID_SCRIPTOBJECT) != 0
                        || (tid & (int)asETypeIdFlags.asTYPEID_APPOBJECT) != 0)
					return new ScriptObject(ptr, ScriptEngine.GetObjectTypeById(tid)); // just handle it dynamically
				else 
                    throw new InvalidOperationException("Cannot handle type with id: " + tid);
			}
		}
        static ILogging logger = new Logging();
        public static void Log(string msg)
        {
            logger.Log(msg);
        }
        public static void Log(string msg, params object[] args)
        {
            logger.Log(msg, args);
        }
    }
}

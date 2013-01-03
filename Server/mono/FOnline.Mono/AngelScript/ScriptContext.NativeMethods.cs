using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline.AngelScript
{
    public partial class ScriptContext
    {
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgByte(IntPtr thisptr, uint arg, byte value);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgWord(IntPtr thisptr, uint arg, ushort value);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgDWord(IntPtr thisptr, uint arg, uint value);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgQWord(IntPtr thisptr, uint arg, ulong value);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgFloat(IntPtr thisptr, uint arg, float value);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgDouble(IntPtr thisptr, uint arg, double value);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgAddress(IntPtr thisptr, uint arg, IntPtr addr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetArgObject(IntPtr thisptr, uint arg, IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_SetObject(IntPtr thisptr, IntPtr obj);
		
		void CheckSetArgRetCode(int ret)
		{
			if(ret == (int)asERetCodes.asCONTEXT_NOT_PREPARED)
				throw new InvalidOperationException("The context is not in prepared state.");
			if(ret == (int)asERetCodes.asINVALID_ARG)
				throw new InvalidOperationException("The arg is larger than the number of arguments in the prepared function.");
		}
		public void SetArgByte(uint arg, byte value)
		{
			var ret = ScriptContext_SetArgByte(thisptr, arg, value);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not a 8-bit value.");
		}
		public void SetArgWord(uint arg, ushort value)
		{
			var ret = ScriptContext_SetArgWord(thisptr, arg, value);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not a 16-bit value.");
		}
		public void SetArgDWord(uint arg, uint value)
		{
			var ret = ScriptContext_SetArgDWord(thisptr, arg, value);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not a 32-bit value.");
		}
		public void SetArgQWord(uint arg, ulong value)
		{
			var ret = ScriptContext_SetArgQWord(thisptr, arg, value);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not a 64-bit value.");
		}
		public void SetArgFloat(uint arg, float value)
		{
			var ret = ScriptContext_SetArgFloat(thisptr, arg, value);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not a 32-bit value.");
		}
		public void SetArgDouble(uint arg, double value)
		{
			var ret = ScriptContext_SetArgDouble(thisptr, arg, value);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not a 64-bit value.");
		}
		public void SetArgAddress(uint arg, IntPtr addr)
		{
			var ret = ScriptContext_SetArgAddress(thisptr, arg, addr);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not a reference or an object handle.");
		}
		public void SetArgObject(uint arg, IntPtr ptr)
		{
			var ret = ScriptContext_SetArgObject(thisptr, arg, ptr);
			if(ret == (int)asERetCodes.asINVALID_TYPE)
				throw new InvalidOperationException("The argument is not an object or handle.");
		}
		public int SetArgBool(uint arg, bool value)
		{
			return ScriptContext_SetArgByte(thisptr, arg, (byte)(value ? 1 : 0));
		}
		public void SetObject(IntPtr obj)
		{
			var ret = ScriptContext_SetObject(thisptr, obj);
			if(ret == (int)asERetCodes.asCONTEXT_NOT_PREPARED)
				throw new InvalidOperationException("Context not prepared.");
			if(ret == (int)asERetCodes.asERROR)
				throw new InvalidOperationException("Function is not a class method.");
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static byte ScriptContext_GetReturnByte(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort ScriptContext_GetReturnWord(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint ScriptContext_GetReturnDWord(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static ulong ScriptContext_GetReturnQWord(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static float ScriptContext_GetReturnFloat(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static double ScriptContext_GetReturnDouble(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptContext_GetReturnAddress(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptContext_GetReturnObject(IntPtr thisptr);
		public byte GetReturnByte()
		{
			return ScriptContext_GetReturnByte(thisptr);
		}
		public ushort GetReturnWord()
		{
			return ScriptContext_GetReturnWord(thisptr);
		}
		public uint GetReturnDWord()
		{
			return ScriptContext_GetReturnDWord(thisptr);
		}
		public ulong GetReturnQWord()
		{
			return ScriptContext_GetReturnQWord(thisptr);
		}
		public float GetReturnFloat()
		{
			return ScriptContext_GetReturnFloat(thisptr);
		}
		public double GetReturnDouble()
		{
			return ScriptContext_GetReturnDouble(thisptr);
		}
		public IntPtr GetReturnAddress()
		{
			return ScriptContext_GetReturnAddress(thisptr);
		}
		public IntPtr GetReturnObject()
		{
			return ScriptContext_GetReturnObject(thisptr);
		}

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptContext_SetContextInfo(IntPtr thisptr, string info);
        string info;
        public string ContextInfo
        {
            set { info = value; }
        }

		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_Prepare(IntPtr thisptr, IntPtr func);
        ScriptFunction func;
        public void Prepare(ScriptFunction func)
        {
            ScriptContext_SetContextInfo(thisptr, info ?? "mono");    
            var ret = ScriptContext_Prepare(thisptr, func.ThisPtr);
			if(ret == (int)asERetCodes.asCONTEXT_ACTIVE)
				throw new InvalidOperationException("Context still active or suspended.");
            this.func = func;
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptContext_Execute(IntPtr thisptr);
        public void Execute()
        {
            func = null;
            var ret = ScriptContext_Execute(thisptr);
						
			if(ret == (int)asERetCodes.asERROR)
				throw new InvalidOperationException("Invalid context object, the context is not prepared, or it is not in suspended state.");
			if(ret == (int)asEContextState.asEXECUTION_ABORTED)
				throw new InvalidOperationException("The execution was aborted with a call to Abort.");
			if(ret == (int)asEContextState.asEXECUTION_SUSPENDED)
				throw new InvalidOperationException("The execution was suspended with a call to Suspend.");
			if(ret == (int)asEContextState.asEXECUTION_EXCEPTION)
				throw new InvalidOperationException("The execution ended with an exception.");
        }
		
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptContext_AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptContext_Release(IntPtr ptr);

        public void AddRef()
        {
            ScriptContext_AddRef(thisptr);
        }
        public void Release()
        {
            ScriptContext_Release(thisptr);
        }

    }
}

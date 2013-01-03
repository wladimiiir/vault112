using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline.AngelScript
{
    public partial class ScriptModule
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static string ScriptModule_GetName(IntPtr thisptr);
        
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptModule_GetFunctionByName(IntPtr thisptr, string name);
        public ScriptFunction GetFunctionByName(string name)
        {
            return (ScriptFunction)ScriptModule_GetFunctionByName(thisptr, name);
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptModule_GetObjectTypeByName(IntPtr thisptr, string name);
        public ScriptObjectType GetObjectTypeByName(string name)
        {
            return (ScriptObjectType)ScriptModule_GetObjectTypeByName(thisptr, name);
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptModule_GetGlobalVarIndexByName(IntPtr thisptr, string name);
        public int GetGlobalVarIndexByName(string name)
        {
            var ret = ScriptModule_GetGlobalVarIndexByName(thisptr, name);
			if(ret == (int)asERetCodes.asERROR)
				throw new InvalidOperationException("The module was not built successfully.");
			if(ret == (int)asERetCodes.asNO_GLOBAL_VAR)
				throw new InvalidOperationException("The matching global variable was not found.");
			if(ret < 0)
				throw new Exception("Unhandled return code.");
			return ret;
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptModule_GetAddressOfGlobalVar(IntPtr thisptr, uint index);
        public IntPtr GetAddressOfGlobalVar(int index)
        {
            return ScriptModule_GetAddressOfGlobalVar(thisptr, (uint)index);
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptModule_GetGlobalVar(IntPtr thisptr, uint index, out int tid);
        public void GetGlobalVar(int index, out int tid)
        {
            var ret = ScriptModule_GetGlobalVar(thisptr, (uint)index, out tid);
			if(ret == (int)asERetCodes.asINVALID_ARG)
				throw new InvalidOperationException("The index is out of range.");
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptModule_AddScriptSection(IntPtr thisptr, string name, string code);
		public void AddScriptSection(string name, string code)
		{
			var ret = ScriptModule_AddScriptSection(thisptr, name, code);
			//if(ret == (int)asERetCodes.asMODULE_IS_IN_USE <- docs said there should be something like thiskllkofo
			if(ret == (int)asERetCodes.asINVALID_ARG)
				throw new InvalidOperationException("The code argument is null.");
			if(ret == (int)asERetCodes.asNOT_SUPPORTED)
				throw new InvalidOperationException("Compiler support is disabled in the engine.");
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptModule_Build(IntPtr thisptr);
		public void Build()
		{
			var ret = ScriptModule_Build(thisptr);
			if(ret == (int)asERetCodes.asINVALID_CONFIGURATION)
				throw new InvalidOperationException("The engine configuration is invalid.");
			if(ret == (int)asERetCodes.asERROR)
				throw new InvalidOperationException("The script failed to build.");
			if(ret == (int)asERetCodes.asBUILD_IN_PROGRESS)
				throw new InvalidOperationException("Another thread is currently building.");
			if(ret == (int)asERetCodes.asINIT_GLOBAL_VARS_FAILED)
				throw new InvalidOperationException("It was not possible to initialize at least one of the global variables.");
			if(ret == (int)asERetCodes.asINVALID_CONFIGURATION)
				throw new InvalidOperationException("Compiler support is disabled in the engine.");
		}
		/*
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr ptr);
		 */

        public void AddRef()
        {
            //AddRef(thisptr);
        }
        public void Release()
        {
            //Release(thisptr);
        }
    }
}

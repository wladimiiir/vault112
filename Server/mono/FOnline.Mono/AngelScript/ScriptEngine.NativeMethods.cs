using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline.AngelScript
{
    public static partial class ScriptEngine
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptEngine_GetModule(string name, int flag);
        public static ScriptModule GetModule(string name, asEGMFlags flag = asEGMFlags.asGM_ONLY_IF_EXISTS)
        {
            return (ScriptModule)ScriptEngine_GetModule(name, (int)flag);
        }
	
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptEngine_CreateContext();
        public static ScriptContext CreateContext()
        {
            return (ScriptContext)ScriptEngine_CreateContext();
        }
		
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptEngine_GetTypeIdByDecl(string decl);
		static Dictionary<string, int> TypeDecls = new Dictionary<string, int>();
        public static int GetTypeIdByDecl(string decl)
        {
			int typedecl;
			if(!TypeDecls.TryGetValue(decl, out typedecl)) // TODO: error handling
            	TypeDecls[decl] = typedecl = ScriptEngine_GetTypeIdByDecl(decl);
			return typedecl;
        }
		
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptEngine_GetObjectTypeById(int tid);
		static Dictionary<int, ScriptObjectType> ObjectTypes = new Dictionary<int, ScriptObjectType>();
		public static ScriptObjectType GetObjectTypeById(int tid)
		{
			ScriptObjectType ot = null;
			if(!ObjectTypes.TryGetValue(tid, out ot))
			{
				var ptr = ScriptEngine_GetObjectTypeById(tid);
				if(ptr != IntPtr.Zero)
					ObjectTypes[tid] = ot = new ScriptObjectType(ptr);
			}
			return ot;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static void ScriptEngine_AddRefScriptObject(IntPtr obj, int tid);
		public static void AddRefScriptObject(ScriptObject obj, int tid)
		{
			ScriptEngine_AddRefScriptObject(obj.ThisPtr, tid);
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static void ScriptEngine_ReleaseScriptObject(IntPtr obj, int tid);
		public static void ReleaseScriptObject(ScriptObject obj, int tid)
		{
			ScriptEngine_ReleaseScriptObject(obj.ThisPtr, tid);
		}
        public static void ReleaseScriptObject(IntPtr ptr, int tid)
        {
            ScriptEngine_ReleaseScriptObject(ptr, tid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptEngine_CreateScriptObjectCopy(IntPtr obj, int tid);
        public static ScriptObject CreateScriptObjectCopy(IntPtr ptr, ScriptObjectType ot)
        {
            return new ScriptObject(ScriptEngine_CreateScriptObjectCopy(ptr, ot.TypeId), ot);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptEngine_AssignScriptObject(IntPtr dst, IntPtr src, int tid);
        public static void AssignScriptObject(IntPtr dst, IntPtr src, int tid)
        {
           ScriptEngine_AssignScriptObject(dst, src, tid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptEngine_CreateScriptObject(int tid);
        public static IntPtr CreateScriptObject(ScriptObjectType ot)
        {
            return ScriptEngine_CreateScriptObject(ot.TypeId);
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static void ScriptEngine_CallPragmas(string[] pragmas);
		public static void CallPragmas(string[] pragmas)
		{
			ScriptEngine_CallPragmas(pragmas);
		}
    }
}

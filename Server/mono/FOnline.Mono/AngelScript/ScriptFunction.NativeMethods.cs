using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline.AngelScript
{
    public partial class ScriptFunction
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptFunction_GetId(IntPtr thisptr);
        public int Id
        {
            get { return ScriptFunction_GetId(thisptr); }
        }
		
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptFunction_GetReturnTypeId(IntPtr thisptr);
		public int ReturnTypeId
		{
			get { return ScriptFunction_GetReturnTypeId(thisptr); }
		}
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptFunction_GetParamTypeId(IntPtr thisptr, uint arg, out int modifier);
        public int GetParamTypeId(uint arg, out asETypeModifiers modifier)
        {
            int mod;
            var tid = ScriptFunction_GetParamTypeId(thisptr, arg, out mod);
            modifier = (asETypeModifiers)mod;
            return tid;
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static string ScriptFunction_GetName(IntPtr thisptr);
		public string Name
		{
			get { return ScriptFunction_GetName(thisptr); }
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static string ScriptFunction_GetDeclaration(IntPtr thisptr, bool include_objectname, bool include_namespace);
		public string GetDeclaration(bool include_objectname, bool include_namespace)
		{
			return ScriptFunction_GetDeclaration(thisptr, include_objectname, include_namespace);
		}
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptFunction_AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptFunction_Release(IntPtr ptr);

        public void AddRef()
        {
            ScriptFunction_AddRef(thisptr);
        }
        public void Release()
        {
            ScriptFunction_Release(thisptr);
        }

    }
}

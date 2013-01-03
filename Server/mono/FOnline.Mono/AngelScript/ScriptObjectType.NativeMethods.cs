using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline.AngelScript
{
    public partial class ScriptObjectType
    {
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptObjectType_GetMethodByName(IntPtr thisptr, string name, bool get_virtual);
		public ScriptFunction GetMethodByName(string name, bool get_virtual)
		{
			return (ScriptFunction)ScriptObjectType_GetMethodByName(thisptr, name, get_virtual);
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptObjectType_GetMethodByDecl(IntPtr thisptr, string decl, bool get_virtual);
		public ScriptFunction GetMethodByDecl(string decl, bool get_virtual)
		{
			return (ScriptFunction)ScriptObjectType_GetMethodByDecl(thisptr, decl, get_virtual);
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint ScriptObjectType_GetMethodCount(IntPtr thisptr);
		public uint GetMethodCount()
		{
			return ScriptObjectType_GetMethodCount(thisptr);
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr ScriptObjectType_GetMethodByIndex(IntPtr thisptr, uint index, bool get_virtual);
		public ScriptFunction GetMethodByIndex(uint index, bool get_virtual)
		{
			return (ScriptFunction)ScriptObjectType_GetMethodByIndex(thisptr, index, get_virtual);
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint ScriptObjectType_GetPropertyCount(IntPtr thisptr);
		public int PropertyCount
		{
			get { return (int)ScriptObjectType_GetPropertyCount(thisptr); }
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptObjectType_GetProperty(IntPtr thisptr, uint index, ref string name, ref int tid, ref bool is_private, ref int offset, ref bool is_reference);
		public void GetProperty(int index, ref string name, ref int tid, ref bool is_private, ref int offset, ref bool is_reference)
		{
			var ret = ScriptObjectType_GetProperty(thisptr, (uint)index, ref name, ref tid, ref is_private, ref offset, ref is_reference);
			if(ret == (int)asERetCodes.asINVALID_ARG)
				throw new InvalidOperationException("The index is out of bounds.");
			if(ret < 0)
				throw new InvalidOperationException("Unhandled exception.");
		}
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int ScriptObjectType_GetTypeId(IntPtr thisptr);
        public int TypeId
        {
            get { return ScriptObjectType_GetTypeId(thisptr); }
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static int ScriptObjectType_GetSubTypeId(IntPtr thisptr);
		public int SubTypeId
		{
			get { return ScriptObjectType_GetSubTypeId(thisptr); }
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static IntPtr ScriptObjectType_GetSubType(IntPtr thisptr);
		public ScriptObjectType SubType
		{
			// TODO: memoize!
			get { return (ScriptObjectType)ScriptObjectType_GetSubType(thisptr); }
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static string ScriptObjectType_GetName(IntPtr thisptr);
		public string Name
		{
			get { return ScriptObjectType_GetName(thisptr); }
		}

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptObjectType_AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void ScriptObjectType_Release(IntPtr ptr);

        public void AddRef()
        {
            ScriptObjectType_AddRef(thisptr);
        }
        public void Release()
        {
            ScriptObjectType_Release(thisptr);
        }

    }
}

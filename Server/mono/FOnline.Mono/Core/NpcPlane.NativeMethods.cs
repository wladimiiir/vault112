using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
	public partial class NpcPlane
	{
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr NpcPlane_GetCopy(IntPtr thisptr);
        public virtual NpcPlane GetCopy()
        {
            return new NpcPlane(NpcPlane_GetCopy(thisptr));
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr NpcPlane_SetChild(IntPtr thisptr, IntPtr child);
        public virtual NpcPlane SetChild(NpcPlane child)
        {
            return new NpcPlane(NpcPlane_SetChild(thisptr, child.thisptr));
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr NpcPlane_GetChild(IntPtr thisptr, uint index);
        public virtual NpcPlane GetChild(uint index)
        {
            return new NpcPlane(NpcPlane_GetChild(thisptr, index));
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool NpcPlane_Misc_SetScript(IntPtr thisptr, IntPtr func_name);
        public virtual bool Misc_SetScript(string func_name)
        {
			var ss = new ScriptString(func_name);
            return NpcPlane_Misc_SetScript(thisptr, ss.ThisPtr);
        }

        /*BIND_ASSERT( engine->RegisterObjectMethod( "NpcPlane", "NpcPlane@ GetCopy() const", asFUNCTION( BIND_CLASS NpcPlane_GetCopy ), asCALL_CDECL_OBJFIRST ) );
BIND_ASSERT( engine->RegisterObjectMethod( "NpcPlane", "NpcPlane@+ SetChild(NpcPlane& child)", asFUNCTION( BIND_CLASS NpcPlane_SetChild ), asCALL_CDECL_OBJFIRST ) );
BIND_ASSERT( engine->RegisterObjectMethod( "NpcPlane", "NpcPlane@+ GetChild(uint index) const", asFUNCTION( BIND_CLASS NpcPlane_GetChild ), asCALL_CDECL_OBJFIRST ) );

// BIND_ASSERT( engine->RegisterObjectMethod( "NpcPlane", "uint GetIndex() const", asFUNCTION(BIND_CLASS NpcPlane_GetIndex), asCALL_CDECL_OBJFIRST ) );
BIND_ASSERT( engine->RegisterObjectMethod( "NpcPlane", "bool Misc_SetScript(string& funcName)", asFUNCTION( BIND_CLASS NpcPlane_Misc_SetScript ), asCALL_CDECL_OBJFIRST ) );
        */

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr ptr);
        public virtual void AddRef()
        {
            AddRef(thisptr);
        }
        public virtual void Release()
        {
            Release(thisptr);
        }
	}
}

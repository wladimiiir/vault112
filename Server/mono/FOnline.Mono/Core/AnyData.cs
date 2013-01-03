using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public interface IAnyData
    {
        bool Get(string name, UInt8Array data);
        bool Set(string name, UInt8Array data);
        bool Get(string name, UInt16Array data);
        bool Set(string name, UInt16Array data);
        bool Get(string name, UIntArray data);
        bool Set(string name, UIntArray data);

        bool Set(string name, int[] data);
        bool Get(string name, Int8Array data);
        bool Set(string name, Int8Array data);
        bool Get(string name, Int16Array data);
        bool Set(string name, Int16Array data);
        bool Get(string name, IntArray data);
        bool Set(string name, IntArray data);
        bool IsAnyData(string name);
        void Erase(string name);
    }
    public class AnyData : IAnyData
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_SetAnyData(IntPtr name, IntPtr data);

        public bool Set(string name, int[] data)
        {
			var ss = new ScriptString(name);
            return Global_SetAnyData(ss.ThisPtr, new IntArray(data).ThisPtr);
        }
        public bool Set(string name, Int8Array data)
        {
            var ss = new ScriptString(name);
            return Global_SetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Set(string name, Int16Array data)
        {
            var ss = new ScriptString(name);
            return Global_SetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Set(string name, IntArray data)
        {
			var ss = new ScriptString(name);
            return Global_SetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Set(string name, UInt8Array data)
        {
            var ss = new ScriptString(name);
            return Global_SetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Set(string name, UInt16Array data)
        {
            var ss = new ScriptString(name);
            return Global_SetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Set(string name, UIntArray data)
        {
			var ss = new ScriptString(name);
            return Global_SetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_GetAnyData(IntPtr name, IntPtr data);
        public bool Get(string name, UInt8Array data)
        {
            var ss = new ScriptString(name);
            return Global_GetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Get(string name, UInt16Array data)
        {
            var ss = new ScriptString(name);
            return Global_GetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Get(string name, UIntArray data)
        {
            var ss = new ScriptString(name);
            return Global_GetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Get(string name, Int8Array data)
        {
            var ss = new ScriptString(name);
            return Global_GetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Get(string name, Int16Array data)
        {
			var ss = new ScriptString(name);
            return Global_GetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        public bool Get(string name, IntArray data)
        {
            var ss = new ScriptString(name);
            return Global_GetAnyData(ss.ThisPtr, data.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_IsAnyData(IntPtr name);
        public bool IsAnyData(string name)
        {
			var ss = new ScriptString(name);
            return Global_IsAnyData(ss.ThisPtr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_EraseAnyData(IntPtr name);
        public void Erase(string name)
        {
			var ss = new ScriptString(name);
            Global_EraseAnyData(ss.ThisPtr);
        }
/*  
 * 
"bool SetAnyData(string& name, int64[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, int32[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, int16[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, int8[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint64[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint32[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint16[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint8[]& data)", asFUNCTION( BIND_CLASS Global_SetAnyData ), asCALL_CDECL ) );
"bool SetAnyData(string& name, int64[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool SetAnyData(string& name, int32[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool SetAnyData(string& name, int16[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool SetAnyData(string& name, int8[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint64[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint32[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint16[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool SetAnyData(string& name, uint8[]& data, uint dataSize)", asFUNCTION( BIND_CLASS Global_SetAnyDataSize ), asCALL_CDECL ) );
"bool GetAnyData(string& name, int64[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool GetAnyData(string& name, int32[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool GetAnyData(string& name, int16[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool GetAnyData(string& name, int8[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool GetAnyData(string& name, uint64[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool GetAnyData(string& name, uint32[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool GetAnyData(string& name, uint16[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool GetAnyData(string& name, uint8[]& data)", asFUNCTION( BIND_CLASS Global_GetAnyData ), asCALL_CDECL ) );
"bool IsAnyData(string& name)", asFUNCTION( BIND_CLASS Global_IsAnyData ), asCALL_CDECL ) );
// "bool AnyDataClass(?& storedClass, ?[]& array)", asFUNCTION( BIND_CLASS Global_AnyDataClass ), asCALL_CDECL ) );
"void EraseAnyData(string& name)", asFUNCTION( BIND_CLASS Global_EraseAnyData ), asCALL_CDECL ) );
*/
    }
}

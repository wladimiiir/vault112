using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace FOnline
{
    public interface ITimeEvents
    {
        uint CreateTimeEvent(uint begin_second, string func_name, bool save);
        uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, bool save);
        uint CreateTimeEvent(uint begin_second, string func_name, uint value, bool save);
        uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, uint value, bool save);
        uint CreateTimeEvent(uint begin_second, string func_name, UIntArray values, bool save);
        uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, UIntArray values, bool save);
        uint CreateTimeEvent(uint begin_second, string func_name, IntArray values, bool save);
        uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, IntArray values, bool save);
        bool EraseTimeEvent(uint id);
        bool GetTimeEvent(uint id, out uint duration, UIntArray values);
        bool GetTimeEvent(uint id, out uint duration, IntArray values);
        bool SetTimeEvent(uint id, uint duration, UIntArray values);
        bool SetTimeEvent(uint id, uint duration, IntArray values);
    }
    public class TimeEvents : ITimeEvents
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_CreateTimeEventEmpty(uint begin_second, IntPtr func_name, bool save);
        public uint CreateTimeEvent(uint begin_second, string func_name, bool save)
        {
            return Global_CreateTimeEventEmpty(begin_second, CoreUtils.ParseFuncName(func_name).ThisPtr, save);
        }
        public uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, bool save)
        {
            var type = func.Method.DeclaringType;
            return Global_CreateTimeEventEmpty(begin_second, CoreUtils.ParseFuncName(type.FullName + "::" + func.Method.Name).ThisPtr, save);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_CreateTimeEventValue(uint begin_second, IntPtr func_name, uint value, bool save);
        public uint CreateTimeEvent(uint begin_second, string func_name, uint value, bool save)
        {
            return Global_CreateTimeEventValue(begin_second, CoreUtils.ParseFuncName(func_name).ThisPtr, value, save);
        }
        public uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, uint value, bool save)
        {
            var type = func.Method.DeclaringType;
            return Global_CreateTimeEventValue(begin_second, CoreUtils.ParseFuncName(type.FullName + "::" + func.Method.Name).ThisPtr, value, save);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_CreateTimeEventValues(uint begin_second, IntPtr func_name, IntPtr values, bool save);
        public uint CreateTimeEvent(uint begin_second, string func_name, UIntArray values, bool save)
        {
            return Global_CreateTimeEventValues(begin_second, CoreUtils.ParseFuncName(func_name).ThisPtr, values.ThisPtr, save);
        }
        public uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, UIntArray values, bool save)
        {
            var type = func.Method.DeclaringType;
            return Global_CreateTimeEventValues(begin_second, CoreUtils.ParseFuncName(type.FullName + "::" + func.Method.Name).ThisPtr, values.ThisPtr, save);
        }
        public uint CreateTimeEvent(uint begin_second, string func_name, IntArray values, bool save)
        {
            return Global_CreateTimeEventValues(begin_second, CoreUtils.ParseFuncName(func_name).ThisPtr, values.ThisPtr, save);
        }
        public uint CreateTimeEvent(uint begin_second, Func<IntPtr, uint> func, IntArray values, bool save)
        {
            var type = func.Method.DeclaringType;
            return Global_CreateTimeEventValues(begin_second, CoreUtils.ParseFuncName(type.FullName + "::" + func.Method.Name).ThisPtr, values.ThisPtr, save);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_EraseTimeEvent(uint id);
        public bool EraseTimeEvent(uint id)
        {
            return Global_EraseTimeEvent(id);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_GetTimeEvent(uint id, out uint duration, IntPtr values);
        public bool GetTimeEvent(uint id, out uint duration, IntArray values)
        {
            return Global_GetTimeEvent(id, out duration, values.ThisPtr);
        }
        public bool GetTimeEvent(uint id, out uint duration, UIntArray values)
        {
            return Global_GetTimeEvent(id, out duration, values.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_SetTimeEvent(uint id, uint duration, IntPtr values);
        public bool SetTimeEvent(uint id, uint duration, IntArray values)
        {
            return Global_GetTimeEvent(id, out duration, values.ThisPtr);
        }
        public bool SetTimeEvent(uint id, uint duration, UIntArray values)
        {
            return Global_GetTimeEvent(id, out duration, values.ThisPtr);
        }
    }
}

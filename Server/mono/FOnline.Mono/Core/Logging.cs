using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public interface ILogging
    {
        void Log(string message);
        void Log(string message, params object[] args);
        string GetLastError();
    }
    public class Logging : ILogging
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_Log(IntPtr s);
        public void Log(string s)
        {
			var ss = new ScriptString(s + Environment.NewLine);
            Global_Log(ss.ThisPtr);
        }
        public void Log(string s, params object[] args)
        {
			var ss = new ScriptString(string.Format(s + Environment.NewLine, args));
            Global_Log(ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern IntPtr Global_GetLastError();
        public string GetLastError()
        {
            return new ScriptString(Global_GetLastError()).ToString();
        }
    }
}

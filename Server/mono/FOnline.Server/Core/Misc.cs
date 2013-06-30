using System;
using System.Runtime.CompilerServices;

namespace FOnline
{
	public interface IMisc
	{
        string GetPlayerName(uint id);
        void SetBestScore(int score, Critter cl, string name);
		void RadioMessage(ushort channel, string text);
		void RadioMessageMsg(ushort channel, ushort textMsg, uint strNum);
		void RadioMessageMsg(ushort channel, ushort textMsg, uint strNum, string lexems);
        uint GetBagItems(uint bag_id, UInt16Array pids, UIntArray min_counts, UIntArray max_counts, IntArray slots);
        bool AddTextListener(Say say_type, string first_str, ushort parameter, string script_name);
        void EraseTextListener(Say say_type, string first_str, ushort parameter);
        uint GetScriptId(string script_name, string func_decl);
        string GetScriptName(uint script_id);
    }
	public class Misc : IMisc
	{
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetPlayerName(uint id);
        public string GetPlayerName(uint id)
        {
            return new ScriptString(Global_GetPlayerName(id)).ToString();
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_SetBestScore(int score, IntPtr cl, IntPtr name);
        public void SetBestScore(int score, Critter cl, string name)
        {
            Global_SetBestScore(score, cl.ThisPtr, new ScriptString(name).ThisPtr);
        }
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static void Global_RadioMessage(ushort channel, IntPtr text);
		public void RadioMessage(ushort channel, string text)
		{
			var ss = new ScriptString(text);
			Global_RadioMessage(channel, ss.ThisPtr);
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static void Global_RadioMessageMsg(ushort channel, ushort text_msg, uint str_num);
		public void RadioMessageMsg(ushort channel, ushort text_msg, uint str_num)
		{
			Global_RadioMessageMsg(channel, text_msg, str_num);
		}
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static void Global_RadioMessageMsgLex(ushort channel, ushort text_msg, uint str_num, IntPtr lexems);
		public void RadioMessageMsg(ushort channel, ushort text_msg, uint str_num, string lexems)
		{
			var ss = new ScriptString(lexems);
			Global_RadioMessageMsgLex(channel, text_msg, str_num, ss.ThisPtr);
		}
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_GetBagItems(uint bag_id, IntPtr pids, IntPtr min_counts, IntPtr max_counts, IntPtr slots);
        public uint GetBagItems(uint bag_id, UInt16Array pids, UIntArray min_counts, UIntArray max_counts, IntArray slots)
        {
            return Global_GetBagItems(bag_id, (IntPtr)pids, (IntPtr)min_counts, (IntPtr)max_counts, (IntPtr)slots);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_AddTextListener(int say_type, IntPtr first_str, ushort parameter, IntPtr script_name);
        public bool AddTextListener(Say say_type, string first_str, ushort parameter, string script_name)
        {
            return Global_AddTextListener((int)say_type, new ScriptString(first_str).ThisPtr, parameter, CoreUtils.ParseFuncName(script_name).ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_EraseTextListener(int say_type, IntPtr first_str, ushort parameter);
        public void EraseTextListener(Say say_type, string first_str, ushort parameter)
        {
            Global_EraseTextListener((int)say_type, new ScriptString(first_str).ThisPtr, parameter);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_GetScriptId(IntPtr script_name, IntPtr func_decl);
        public uint GetScriptId(string script_name, string func_decl)
        {
            return Global_GetScriptId(new ScriptString(script_name).ThisPtr, new ScriptString(func_decl).ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetScriptName(uint script_id);
        public string GetScriptName(uint script_id)
        {
            return new ScriptString(Global_GetScriptName(script_id)).ToString();
        }
    }
}

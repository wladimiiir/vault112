using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public interface ICritterManager
    {
        Critter FromNative(IntPtr ptr);
        Critter GetCritter(uint id);
		void DeleteNpc(Critter cr);
        NpcPlane CreatePlane();
        bool SetParameterGetBehaviour(uint index, Func<IntPtr, uint, int> func);
        bool SetParameterGetBehaviour(uint index, string func_name);
        bool SetParameterChangeBehaviour(uint index, Action<IntPtr, uint, int> func);
        bool SetParameterChangeBehaviour(uint index, string func_name);
        void SetRegistrationParameter(uint index, bool enabled);
        void SetChosenSendParameter(int index, bool enabled);
        void SetSendParameter(int index, bool enabled);
        void SetSendParameter(int index, bool enabled, string allow_func);
        bool IsCritterCanWalk(uint cr_type);
        bool IsCritterCanRun(uint cr_type);
        bool IsCritterCanAim(uint cr_type);
        bool IsCritterCanRotate(uint cr_type);
        bool IsCritterCanArmor(uint cr_type);
        bool IsCritterAnim1(uint cr_type);
		uint GetCrittersDistantion(Critter cr1, Critter cr2);
        bool SwapCritters(Critter cr1, Critter cr2, bool with_inv, bool with_vars);
        uint GetAllNpc(ushort pid, CritterArray npc);
    }
    public class CritterManager : ICritterManager
    {
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static IntPtr Global_DeleteNpc(IntPtr thisptr);
		public void DeleteNpc(Critter cr)
		{
			Global_DeleteNpc(cr.ThisPtr);
		}
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_CreatePlane();
        public NpcPlane CreatePlane()
        {
            return new NpcPlane(Global_CreatePlane());
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetCritter(uint id);
        public Critter GetCritter(uint id)
        {
            return FromNative(Global_GetCritter(id));
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static Critter Crit_FromNative(IntPtr ptr);
        public Critter FromNative(IntPtr ptr)
        {
            return Crit_FromNative(ptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_SetParameterGetBehaviour(uint index, IntPtr func_name);
        public bool SetParameterGetBehaviour(uint index, Func<IntPtr, uint, int> func)
        {
            var method = func.Method;
            return Global_SetParameterGetBehaviour(index, 
                CoreUtils.ParseFuncName(method.DeclaringType.FullName + "::" + method.Name).ThisPtr);
        }
        public bool SetParameterGetBehaviour(uint index, string func_name)
        {
            return Global_SetParameterGetBehaviour(index, CoreUtils.ParseFuncName(func_name).ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_SetParameterChangeBehaviour(uint index, IntPtr func_name);
        public bool SetParameterChangeBehaviour(uint index, Action<IntPtr, uint, int> func)
        {
            var method = func.Method;
            return Global_SetParameterChangeBehaviour(index,
                CoreUtils.ParseFuncName(method.DeclaringType.FullName + "::" + method.Name).ThisPtr);
        }
        public bool SetParameterChangeBehaviour(uint index, string func_name)
        {
            return Global_SetParameterChangeBehaviour(index, CoreUtils.ParseFuncName(func_name).ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_SetRegistrationParam(uint index, bool enabled);
        public void SetRegistrationParameter(uint index, bool enabled)
        {
            Global_SetRegistrationParam(index, enabled);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_IsCritterCanWalk(uint cr_type);
        public bool IsCritterCanWalk(uint cr_type)
        {
            return Global_IsCritterCanWalk(cr_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_IsCritterCanRun(uint cr_type);
        public bool IsCritterCanRun(uint cr_type)
        {
            return Global_IsCritterCanRun(cr_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_IsCritterCanRotate(uint cr_type);
        public bool IsCritterCanRotate(uint cr_type)
        {
            return Global_IsCritterCanRotate(cr_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_IsCritterCanAim(uint cr_type);
        public bool IsCritterCanAim(uint cr_type)
        {
            return Global_IsCritterCanAim(cr_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_IsCritterCanArmor(uint cr_type);
        public bool IsCritterCanArmor(uint cr_type)
        {
            return Global_IsCritterCanArmor(cr_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_IsCritterAnim1(uint cr_type);
        public bool IsCritterAnim1(uint cr_type)
        {
            return Global_IsCritterAnim1(cr_type);
        }

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static uint Global_GetCrittersDistantion(IntPtr ptr1, IntPtr ptr2);
		public uint GetCrittersDistantion(Critter cr1, Critter cr2)
		{
			return Global_GetCrittersDistantion(cr1.ThisPtr, cr2.ThisPtr);
		} 
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_SetChosenSendParameter(int index, bool enabled);
        public void SetChosenSendParameter(int index, bool enabled)
        {
            Global_SetChosenSendParameter(index, enabled);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_SetSendParameter(int index, bool enabled);
        public void SetSendParameter(int index, bool enabled)
        {
            Global_SetSendParameter(index, enabled);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_SetSendParameterFunc(int index, bool enabled, IntPtr allow_func);
        public void SetSendParameter(int index, bool enabled, string allow_func)
        {
            Global_SetSendParameterFunc(index, enabled, CoreUtils.ParseFuncName(allow_func).ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Global_SwapCritters(IntPtr cr1, IntPtr cr2, bool with_inv, bool with_vars);
        public bool SwapCritters(Critter cr1, Critter cr2, bool with_inv, bool with_vars)
        {
            return Global_SwapCritters(cr1.ThisPtr, cr2.ThisPtr, with_inv, with_vars);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_GetAllNpc(ushort pid, IntPtr array);
        public uint GetAllNpc(ushort pid, CritterArray npc)
        {
            return Global_GetAllNpc(pid, (IntPtr)npc);
        }
    }
}

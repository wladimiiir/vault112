using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public interface IVarManager
    {
        GameVar GetGlobalVar(ushort var_id);
        GameVar GetLocalVar(ushort var_id, uint master_id);
        GameVar GetUnicumVar(ushort var_id, uint master_id, uint slave_id);
    }
    public class VarManager : IVarManager
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetGlobalVar(ushort var_id);
        public GameVar GetGlobalVar(ushort var_id)
        {
            var ptr = Global_GetGlobalVar(var_id);
            return ptr == IntPtr.Zero ? null : new GameVar(ptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetLocalVar(ushort var_id, uint master_id);
        public GameVar GetLocalVar(ushort var_id, uint master_id)
        {
            var ptr = Global_GetLocalVar(var_id, master_id);
            return ptr == IntPtr.Zero ? null : new GameVar(ptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Global_GetUnicumVar(ushort var_id, uint master_id, uint slave_id);
        public GameVar GetUnicumVar(ushort var_id, uint master_id, uint slave_id)
        {
            var ptr = Global_GetUnicumVar(var_id, master_id, slave_id);
            return ptr == IntPtr.Zero ? null : new GameVar(ptr);
        }
    }
    public class GameVar : IManagedWrapper
    {
        readonly IntPtr thisptr;
        public GameVar(IntPtr ptr)
        {
            thisptr = ptr;
            AddRef();
        }
        ~GameVar()
        {
            Release();
        }

        public IntPtr ThisPtr { get { return thisptr; } }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr thisptr);
        public virtual void AddRef()
        {
            AddRef(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr thisptr);
        public virtual void Release()
        {
            Release(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int GetValue(IntPtr thisptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void SetValue(IntPtr thisptr, int value);
        public virtual int Value
        {
            get { return GetValue(thisptr); }
            set { SetValue(thisptr, value); }
        }
        public static implicit operator int(GameVar var)
        {
            return GetValue(var.thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int GetMin(IntPtr thisptr);
        public virtual int Min
        {
            get { return GetMin(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int GetMax(IntPtr thisptr);
        public virtual int Max
        {
            get { return GetMax(thisptr); }
        }
    }
}

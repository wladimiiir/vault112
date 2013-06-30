using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Collections;

namespace FOnline
{
    /// <summary>
    /// Wrapper for native CScriptArray class.
    /// </summary>
    public class ScriptArray
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static void AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static void Release(IntPtr ptr); // this needs to be thread safe
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern internal static int ScriptArray_GetRefCount(IntPtr thisptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static void ReleaseRefs(IntPtr ptr); // this needs to be thread safe
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static IntPtr Create(IntPtr type);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static IntPtr CreateSize(uint length, IntPtr type);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static IntPtr GetType(string type);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static IntPtr At(IntPtr ptr, uint index);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static uint Length(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static void Resize(IntPtr ptr, uint length);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static void InsertAt(IntPtr thisptr, uint index, IntPtr value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static void RemoveAt(IntPtr thisptr, uint index);
    }    
    /// <summary>
    /// Base class for arrays that keeps handles to the objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class HandleArray<T> : ScriptArray<T> where T: IManagedWrapper
    {
        public HandleArray(IntPtr type)
            : base(type)
        {

        }
        public HandleArray(IntPtr ptr, bool wrap)
            : base(ptr, wrap)
        {
        }
        protected IntPtr GetObjectAddress(IntPtr handle_ptr)
        {
            unsafe
            {
                return *(IntPtr*)handle_ptr;
            }
        }
        public override void ToNative(IntPtr ptr, T value)
        {
            unsafe
            {
                *(IntPtr*)ptr = value != null ? value.ThisPtr : IntPtr.Zero;
            }
        }
		public override T this[uint index]
        {
            get
            {
                return FromNative(ScriptArray.At(thisptr, index));
            }
            set
            {
				var ptr = ScriptArray.At (thisptr, index);
				var obj = FromNative(ptr);
				if(obj != null)
					obj.Release();
				if(value != null)
					value.AddRef();
                ToNative(ptr, value);
            }
        }
    }
	public sealed class BoolArray : ScriptArray<bool>
    {
        static readonly IntPtr type;
        public BoolArray() : base(type) {}
        public BoolArray(IntPtr ptr) : base(ptr, true) {}
        public BoolArray(IList<bool> list) : base(list, type) {}
        public BoolArray(uint size) : base(size, type) {}
        static BoolArray() { type = ScriptArray.GetType("array<bool>"); }
        public override bool FromNative(IntPtr ptr) { unsafe { return *(bool*)ptr; } }
        public override void ToNative(IntPtr ptr, bool value) { unsafe { *(bool*)ptr = value;  } }
	}
	public sealed class Int8Array : ScriptArray<sbyte>
    {
        static readonly IntPtr type;
        public Int8Array() : base(type) {}
        public Int8Array(IntPtr ptr) : base(ptr, true) {}
        public Int8Array(IList<sbyte> list) : base(list, type) {}
        public Int8Array(uint size) : base(size, type) {}
        static Int8Array() { type = ScriptArray.GetType("array<int8>"); }
        public override sbyte FromNative(IntPtr ptr) { unsafe { return *(sbyte*)ptr; } }
        public override void ToNative(IntPtr ptr, sbyte value) { unsafe { *(sbyte*)ptr = value;  } }
	}
	public sealed class Int16Array : ScriptArray<short>
    {
        static readonly IntPtr type;
        public Int16Array() : base(type) {}
        public Int16Array(IntPtr ptr) : base(ptr, true) {}
        public Int16Array(IList<short> list) : base(list, type) {}
        public Int16Array(uint size) : base(size, type) {}
        static Int16Array() { type = ScriptArray.GetType("array<int16>"); }
        public override short FromNative(IntPtr ptr) { unsafe { return *(short*)ptr; } }
        public override void ToNative(IntPtr ptr, short value) { unsafe { *(short*)ptr = value;  } }
	}
    public sealed class IntArray : ScriptArray<int>
    {
        static readonly IntPtr type;
        public IntArray() : base(type) {}
        public IntArray(IntPtr ptr) : base(ptr, true) {}
        public IntArray(IList<int> list) : base(list, type) {}
        public IntArray(uint size) : base(size, type) {}
        static IntArray() { type = ScriptArray.GetType("array<int>"); }
        public override int FromNative(IntPtr ptr) { unsafe { return *(int*)ptr; } }
        public override void ToNative(IntPtr ptr, int value) { unsafe { *(int*)ptr = value;  } }
    }
	public sealed class UInt8Array : ScriptArray<byte>
	{
		static readonly IntPtr type;
		public UInt8Array() : base(type) {}
		public UInt8Array(IntPtr ptr) : base(type) {}
		public UInt8Array(IList<byte> list)	: base(list, type) {}
		public UInt8Array(uint size) : base(size, type) {}
		static UInt8Array() { type = ScriptArray.GetType("array<uint8>"); }
		public override byte FromNative(IntPtr ptr) { unsafe { return *(byte*)ptr; } }
		public override void ToNative(IntPtr ptr, byte value) { unsafe { *(uint*)ptr = value; } }
	}
    public sealed class UInt16Array : ScriptArray<ushort>
    {
        static readonly IntPtr type;
        public UInt16Array() : base(type) {}
        public UInt16Array(IntPtr ptr) : base(ptr, true) {}
        public UInt16Array(IList<ushort> list) : base(list, type) {}
        public UInt16Array(uint size) : base(size, type) {}
        static UInt16Array() { type = ScriptArray.GetType("array<uint16>"); }
        public override ushort FromNative(IntPtr ptr) { unsafe { return *(ushort*)ptr; } }
		public override void ToNative(IntPtr ptr, ushort value) { unsafe { *(ushort*)ptr = value; } }
    }
    public sealed class UIntArray : ScriptArray<uint>
    {
        static readonly IntPtr type;
        public UIntArray() : base(type) {}
        public UIntArray(IntPtr ptr) : base(ptr, true) {}
        public UIntArray(IList<uint> list) : base(list, type) {}
        public UIntArray(uint size) : base(size, type) {}
        static UIntArray() { type = ScriptArray.GetType("array<uint>"); }
        public override uint FromNative(IntPtr ptr) { unsafe { return *(uint*)ptr; } }
        public override void ToNative(IntPtr ptr, uint value) { unsafe { *(uint*)ptr = value; } }
    }
	public sealed class FloatArray : ScriptArray<float>
    {
        static readonly IntPtr type;
        public FloatArray() : base(type) {}
        public FloatArray(IntPtr ptr) : base(ptr, true) {}
        public FloatArray(IList<float> list) : base(list, type) {}
        public FloatArray(uint size) : base(size, type) {}
        static FloatArray() { type = ScriptArray.GetType("array<float>"); }
        public override float FromNative(IntPtr ptr) { unsafe { return *(float*)ptr; } }
        public override void ToNative(IntPtr ptr, float value) { unsafe { *(float*)ptr = value; } }
    }
	public sealed class DoubleArray : ScriptArray<double>
    {
        static readonly IntPtr type;
        public DoubleArray() : base(type) {}
        public DoubleArray(IntPtr ptr) : base(ptr, true) {}
        public DoubleArray(IList<double> list) : base(list, type) {}
        public DoubleArray(uint size) : base(size, type) {}
        static DoubleArray() { type = ScriptArray.GetType("array<double>"); }
        public override double FromNative(IntPtr ptr) { unsafe { return *(double*)ptr; } }
        public override void ToNative(IntPtr ptr, double value) { unsafe { *(double*)ptr = value; } }
    }
    public abstract class ScriptArray<T> : IEnumerable<T>, IList<T>
    {
        protected readonly IntPtr thisptr;
        /// <summary>
        /// Instantiates wrapper given the native pointer.
        /// </summary>
        /// <param name="ptr"></param>
        protected ScriptArray(IntPtr ptr, bool wrap)
        {
            thisptr = ptr;
            ScriptArray.AddRef(thisptr);
        }
        protected ScriptArray(IntPtr type)
        {
            thisptr = ScriptArray.Create(type);
        }
        protected ScriptArray(IList<T> list, IntPtr type)
        {
            thisptr = ScriptArray.CreateSize((uint)list.Count, type);
            for (uint i = 0; i < list.Count; i++)
                this[i] = list[(int)i];
        }
        protected ScriptArray(uint size, IntPtr type)
        {
            thisptr = ScriptArray.CreateSize(size, type);
        }
        ~ScriptArray()
        {
            ScriptArray.Release(thisptr); 
        }
        public IntPtr ThisPtr { get { return thisptr; } }
		public int RefCount
		{
			get { return ScriptArray.ScriptArray_GetRefCount(thisptr); }
		}
        public static explicit operator IntPtr(ScriptArray<T> self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public uint Length
        {
            get
            {
                return ScriptArray.Length(thisptr);
            }
        }
        public virtual T this[uint index]
        {
            get
            {
                if(!CheckRange((int)index))
                    throw new ArgumentOutOfRangeException("index");
                return FromNative(ScriptArray.At(thisptr, index));
            }
            set
            {
                if(IsReadOnly)
                    throw new NotSupportedException("Array is read only");
                var ptr = ScriptArray.At(thisptr, index);
                ToNative(ptr, value);
            }
        }
        public void Resize(uint length)
        {
            ScriptArray.Resize(thisptr, length);
        }
        public void Add(T elem)
        {
            uint index = Length;
            Resize(Length + 1);
            this[index] = elem;
        }
        public void AddRange(IEnumerable<T> elems)
        {
            uint index = Length;
            var arr = elems.ToArray();
            Resize(Length + (uint)arr.Length);
            for (uint i = index; i < Length; i++)
                this[i] = arr[i - index];
        }
        public abstract void ToNative(IntPtr ptr, T value);
        public abstract T FromNative(IntPtr ptr);
        
        public class Enumerator : IDisposable, IEnumerator<T>
        {
            int current = -1;
            readonly ScriptArray<T> array;
            public Enumerator(ScriptArray<T> array)
            {
                this.array = array;
                ScriptArray.AddRef(array.ThisPtr);
            }
            public void Dispose()
            {
                ScriptArray.Release(array.ThisPtr);
            }
            public T Current
            {
                get 
                {
                    IntPtr el = ScriptArray.At(array.ThisPtr, (uint)current);
                    return array.FromNative(el);
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (++current < ScriptArray.Length(array.ThisPtr))
                    return true;
                
                return false;
            }

            public void Reset()
            {
                current = -1;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #region IList
        public int IndexOf(T item)
        {
            for(int i = 0; i < this.Length; i++)
            {
                if(EqualityComparer<T>.Default.Equals(this[i], item))
                    return i;
            }
            return -1;
        }
        bool CheckRange(int index)
        {
            return index >= 0 && index < Length;
        }
        public void Insert(int index, T item)
        {
            uint len = this.Length;
            if(index < 0 || index > len)
                throw new ArgumentOutOfRangeException("index");
            if(IsReadOnly)
                throw new NotSupportedException("Array read-only");
            // we cannot obtain needed ptr in generic way, so we just insert 'manually'
            //ScriptArray.InsertAt(thisptr, (uint)index, ptr);
            ScriptArray.Resize(thisptr, len + 1);
            for (uint i = len; i > (int)index; i--)
                this[i] = this[i-1];
            this[index] = item;
        }

        public void RemoveAt(int index)
        {
            if(!CheckRange(index))
                throw new ArgumentOutOfRangeException("index");
            if(IsReadOnly)
                throw new NotSupportedException("Array is read-only");
            ScriptArray.RemoveAt(thisptr, (uint)index);
        }

        public T this[int index]
        {
            get
            {
                return this[(uint)index];
            }
            set
            {
                this[(uint)index] = value;
            }
        }


        public void Clear()
        {
            ScriptArray.Resize(thisptr, 0);
        }

        public bool Contains(T item)
        {
            for(int i = 0; i < this.Length; i++) 
            {
                if(EqualityComparer<T>.Default.Equals(this[i], item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if(array == null) throw new ArgumentNullException("array");
            if(arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex");
            if(array.Length - arrayIndex < this.Length)
                throw new ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array");
            for(int i = arrayIndex; i < array.Length; i++) 
                array[i] = this[i - arrayIndex];
        }

        public int Count
        {
            get { return (int)Length;  }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            var idx = IndexOf(item);
            if (idx != -1)
            {
                RemoveAt(idx);
                return true;
            }
            return false;
        }
        #endregion
    }
}

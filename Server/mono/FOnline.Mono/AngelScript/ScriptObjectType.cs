using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline.AngelScript
{
    /// <summary>
    /// Exposes the functionality of asIObjectType implementation.
    /// </summary>
    public partial class ScriptObjectType
    {
        readonly IntPtr thisptr;

        public ScriptObjectType(IntPtr ptr)
        {
            this.thisptr = ptr;
            AddRef();
        }
        ~ScriptObjectType()
        {
            Release(); // this needs to be thread safe!
        }
        public IntPtr ThisPtr { get { return thisptr; } }

        public static explicit operator IntPtr(ScriptObjectType self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public static explicit operator ScriptObjectType(IntPtr ptr)
        {
            return ptr == IntPtr.Zero ? null : new ScriptObjectType(ptr);
        }
		
		// typeid -> function that spawns object that will handle it on managed side
		static Dictionary<int, Func<IntPtr, object>> TypeActivators = new Dictionary<int, Func<IntPtr, object>>();

		/*public static void AddTypeActivator(string typedecl, Func<IntPtr, object> activator)
		{
			TypeActivators[ScriptEngine.GetTypeIdByDecl(typedecl)] = activator;
		}*/
		static ScriptObjectType()
		{
			Func<string, int> get_tid = d => ScriptEngine.GetTypeIdByDecl(d);
			TypeActivators[get_tid("string")] = ptr => new ScriptString(ptr);

			TypeActivators[get_tid("array<bool>")] = ptr => new BoolArray(ptr);
			TypeActivators[get_tid("array<int8>")] = ptr => new Int8Array(ptr);
			TypeActivators[get_tid("array<int16>")] = ptr => new Int16Array(ptr);
			TypeActivators[get_tid("array<int>")] = ptr => new IntArray(ptr);
			TypeActivators[get_tid("array<uint8>")] = ptr => new UInt8Array(ptr);
			TypeActivators[get_tid("array<uint16>")] = ptr => new UInt16Array(ptr);
			TypeActivators[get_tid("array<uint>")] = ptr => new UIntArray(ptr);
			TypeActivators[get_tid("array<float>")] = ptr => new FloatArray(ptr);
			TypeActivators[get_tid("array<double>")] = ptr => new DoubleArray(ptr);
			TypeActivators[get_tid("array<string>")] = ptr => new ScriptStringArray(ptr);
			TypeActivators[get_tid("array<string@>")] = ptr => new ScriptStringHandleArray(ptr);
		}
		public static object Instantiate(IntPtr ptr, int tid)
		{
			Func<IntPtr, object> activator = null;
			if(TypeActivators.TryGetValue(tid, out activator))
				return activator(ptr);
			// other hardcoded cases
			var ot = ScriptEngine.GetObjectTypeById(tid);
			if(ot.Name == "array")
			{
				if((ot.SubTypeId & (int)asETypeIdFlags.asTYPEID_OBJHANDLE) != 0)
					return new ScriptObjectHandleArray(ptr, ot.SubType);
				else 
					return new ScriptObjectArray(ptr, ot.SubType);
			}
			return null;
		}

		class FieldDesc
		{
			public int Offset { get; set; }
			public int TypeId { get; set; }
			public FieldDesc (int offset, int tid)
			{
				this.Offset = offset;
				this.TypeId = tid;
			}
		}
		Dictionary<string, FieldDesc> fields = new Dictionary<string, FieldDesc>();
		public bool GetFieldDesc(string name, out int offset, out int tid)
		{
			FieldDesc fd = null;
			if(!fields.TryGetValue(name, out fd))
			{
				// loop over for search of desired property
				for(int i = 0; i < PropertyCount; i++)
				{
					string n = null;
					int _tid = 0;
					int _offset = 0;
					bool is_private = false;
					bool is_reference = false;
					GetProperty(i, ref n, ref _tid, ref is_private, ref _offset, ref is_reference);
					if(n == name)
					{
						// if type is reference, denote it in type id
						if(is_reference)
							_tid = _tid | (int)asETypeIdFlags.asTYPEID_OBJHANDLE;
						// memoize the result
						fd = new FieldDesc(_offset, _tid);
						fields[name] = fd;
						break;
					}
				}
			}
			if(fd != null)
			{
				offset = fd.Offset;
				tid = fd.TypeId;
				return true;
			}
			else
			{
				offset = tid = 0;
				return false;
			}
		}
		Dictionary<string, ScriptFunction> Methods = new Dictionary<string, ScriptFunction>();
		public ScriptFunction GetMethod(string name)
		{
			ScriptFunction func;
			if(!Methods.TryGetValue(name, out func))
				Methods[name] = func = GetMethodByName(name, true);
			return func;
		}
	}
}

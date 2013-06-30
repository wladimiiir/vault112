using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace FOnline.AngelScript
{
    /// <summary>
    /// Exposes the functionality of asIScriptObject implementation.
    /// </summary>
    public partial class ScriptObject : DynamicObject, IManagedWrapper 
    {
        readonly IntPtr thisptr;
		readonly ScriptObjectType objectType;
        bool valuetype;
        public ScriptObject(IntPtr ptr, ScriptObjectType ot, bool valuetype = false)
        {
            //if(valuetype)
            //   throw new NotImplementedException("Value type wrappers are not implemented.");
            this.thisptr = ptr;
			this.objectType = ot;
            this.valuetype = valuetype;
			if(!valuetype)
                AddRef();
        }
        public ScriptObject(ScriptObjectType ot)
        {
            this.thisptr = ScriptEngine.CreateScriptObject(ot);
            this.objectType = ot;
        }
        ~ScriptObject()
        {
            if(valuetype)
                ;//Destroy();
            else
                Release();
        }
        public IntPtr ThisPtr { get { return thisptr; } }

        public static explicit operator IntPtr(ScriptObject self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        /*public static explicit operator ScriptObject(IntPtr ptr)
        {
            return ptr == IntPtr.Zero ? null : new ScriptObject(ptr);
        }*/

		//
		// dynamic stuff
		//
        Dictionary<string, ScriptFunction> methods = new Dictionary<string, ScriptFunction>();
        ScriptFunction GetMethod(string name)
        {
            ScriptFunction func = null;
            if (!methods.TryGetValue(name, out func))
                methods[name] = func = objectType.GetMethod(name);
            return func;
        }
		public override bool TryInvokeMember (InvokeMemberBinder binder, object[] args, out object result)
		{
			try
			{
                var func = GetMethod(binder.Name);
                if (func != null)
                {
				    using(var ctx = ScriptEngine.CreateContext())
				    {
                        ctx.ContextInfo = objectType.Name + "::" + binder.Name;
                        ctx.Prepare(func);
                        ctx.SetObject(thisptr);
                        uint pos = 0;
                        foreach (var arg in args)
                            ctx.SetArg(pos++, arg);
                        ctx.Execute();
                        result = ctx.GetResult(func.ReturnTypeId);
                        return true;
                    }
                }
                else
                {
                    ScriptEngine.Log("Unable to fetch method '{0}'. Maybe it's overloaded?", binder.Name);
                    result = null;
                    return false;
                }
			} 
			catch(Exception ex)
			{
				ScriptEngine.Log ("Exception caught while calling into AngelScript runtime: {0}.", ex.Message);
				result = null;
				return false;
			}
		}
		class Field
		{
			public Field (int offset, int type_id, object instance)
			{
				this.Offset = offset;
				this.TypeId = type_id;
				this.Instance = instance;
			}
			public int Offset { get; private set; }
			public int TypeId { get; private set; }
			public object Instance { get; private set; }
		}
		Dictionary<string, Field> Fields = new Dictionary<string, Field>();
		Field GetField(string name)
		{
			Field field = null;
			if(!Fields.TryGetValue(name, out field))
			{
				int tid;
				int offset;
				if(objectType.GetFieldDesc(name, out offset, out tid))
				{
					var instance = ScriptEngine.GetVariable (thisptr + offset, tid);
					field = new Field(offset, tid, instance); 
				}
                Fields[name] = field;
			}
			return field;
		}
        class Property
        {
            public Property(ScriptFunction getter, ScriptFunction setter)
            {
                this.Getter = getter;
                this.Setter = setter;
            }
            public ScriptFunction Getter { get; private set; }
            public ScriptFunction Setter { get; private set; }
        }
        Dictionary<string, Property> properties = new Dictionary<string, Property>();
        Property GetProperty(string name)
        {
            Property prop = null;
            if (!properties.TryGetValue(name, out prop))
            {
                var getter = objectType.GetMethod("get_" + name);
                var setter = objectType.GetMethod("set_" + name);
                if(getter != null || setter != null)
                    prop = new Property(getter, setter);
                properties[name] = prop;
            }
            return prop;
        }
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			try
			{
                var prop = GetProperty(binder.Name);
                if (prop != null && prop.Getter != null)
                {
                    using (var ctx = ScriptEngine.CreateContext())
                    {
                        ctx.ContextInfo = objectType.Name + "::" + prop.Getter.Name;
                        ctx.Prepare(prop.Getter);
                        ctx.SetObject(thisptr);
                        ctx.Execute();
                        result = ctx.GetResult(prop.Getter.ReturnTypeId);
                        return true;
                    }
                }
				var field = GetField (binder.Name);
				if(field != null)
                {
					result = ScriptEngine.GetVariable(thisptr + field.Offset, field.TypeId, field.Instance);
                    return true;
                }
				ScriptEngine.Log ("Unable to get member '{0}' from object of type '{1}'.", binder.Name, objectType.Name);
				result = null;
				return false;
			}
			catch(Exception ex)
			{
				ScriptEngine.Log ("Exception caught while getting object member: {0}.", ex.Message);
				result = null;
				return false;
			}
		}
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			try
			{
                var prop = GetProperty(binder.Name);
                if (prop != null && prop.Setter != null)
                {
                    using (var ctx = ScriptEngine.CreateContext())
                    {
                        ctx.ContextInfo = objectType.Name + "::" + prop.Setter.Name;
                        ctx.Prepare(prop.Setter);
                        ctx.SetObject(thisptr);
                        ctx.SetArg(0, value);
                        ctx.Execute();
                        return true;
                    }
                }
				var field = GetField(binder.Name);
                if (field != null)
                {
                    ScriptEngine.SetVariable(thisptr + field.Offset, field.TypeId, value);
                    return true;
                }
                ScriptEngine.Log("Unable to set member '{0}' on object of type '{1}'.", binder.Name, objectType.Name);
                return false;
			}
			catch(Exception ex)
			{
				ScriptEngine.Log ("Exception caught while getting object member: {0}.", ex.Message);
				return false;
			}
		}
		/*public override bool TryGetIndex(GetIndexBinder binder,	Object[] indexes, out Object result)
		{
			try
			{
				if(objectType.Name != "array")
					throw new InvalidOperationException("Not an array.");
				// for now only this:
				int index = (int)indexes[0];
				var ptr = ScriptArray.At (thisptr, (uint)index);
				if(ptr == IntPtr.Zero)
					throw new IndexOutOfRangeException();
				result = ScriptEngine.GetVariable(ptr, objectType.SubTypeId);
				return true;
			}
			catch(Exception ex)
			{
				ScriptEngine.Log("Exception caught while indexing object: {0}.", ex.Message);
				result = null;
				return true;
			}
		}*/
    }
	public sealed class ScriptObjectArray : ScriptArray<ScriptObject>
    {
		ScriptObjectType subType;
		// only wrapper
        public ScriptObjectArray(IntPtr ptr, ScriptObjectType sub_type) 
		: base(ptr, true) 
		{
			this.subType = sub_type;
		}

		Dictionary<IntPtr, ScriptObject> Instances = new Dictionary<IntPtr, ScriptObject>();
        public override ScriptObject FromNative(IntPtr ptr)
        { 
            ScriptObject obj;
            if(!Instances.TryGetValue (ptr, out obj)) 
            {
                obj = new ScriptObject(subType);
                ScriptEngine.AssignScriptObject(obj.ThisPtr, ptr, subType.TypeId);
                Instances[ptr] = obj;
            }
			return obj;
		}
        public override void ToNative(IntPtr ptr, ScriptObject value) 
		{ 
			throw new NotImplementedException();
			/*unsafe 
			{ // release
				*ptr = value;

			} */
		}
    }
	public sealed class ScriptObjectHandleArray : HandleArray<ScriptObject>
    {
		ScriptObjectType subType;
        // only wrapper
		public ScriptObjectHandleArray (IntPtr ptr, ScriptObjectType sub_type)
			: base(ptr, true)
		{
			this.subType = sub_type;
		}

		Dictionary<IntPtr, ScriptObject> Instances = new Dictionary<IntPtr, ScriptObject>();
        public override ScriptObject FromNative(IntPtr ptr)
        {
			var addr = GetObjectAddress(ptr);
			if(addr != IntPtr.Zero)
			{
				ScriptObject obj;
				if(!Instances.TryGetValue(ptr, out obj))
					Instances[ptr] = obj = new ScriptObject(addr, subType);
				return obj;
			}
			return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace FOnline.AngelScript
{
    /// <summary>
    /// Exposes the functionality of asIScriptModule implementation.
    /// </summary>
    public partial class ScriptModule : DynamicObject
    {
        readonly IntPtr thisptr;
        
        public ScriptModule(IntPtr ptr)
        {
            this.thisptr = ptr;
            this.Name = ScriptModule_GetName(ptr);
            AddRef();
        }
        ~ScriptModule()
        {
            Release(); // this needs to be thread safe!
        }
        public IntPtr ThisPtr { get { return thisptr; } }
        public string Name { get; private set; }

        public static explicit operator IntPtr(ScriptModule self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public static explicit operator ScriptModule(IntPtr ptr)
        {
            return ptr == IntPtr.Zero ? null : new ScriptModule(ptr);
        }
		
		//
		// dynamic stuff
		//
		class Variable
		{
			public Variable (object instance, IntPtr address, int typeid)
			{
				this.Instance = instance;
				this.Address = address;
				this.TypeId = typeid;
			}
			public object Instance { get; private set; }
			public IntPtr Address { get; private set; }
			public int TypeId { get; private set; }
		}
		Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			try
			{
				Variable variable;
				if(!variables.TryGetValue(binder.Name, out variable))
				{
					int idx = GetGlobalVarIndexByName(binder.Name);
					var ptr = GetAddressOfGlobalVar(idx);
					if(ptr == IntPtr.Zero)
					{
						result = null;
						return true;
					}
					int tid;
					GetGlobalVar(idx, out tid);
					var instance = ScriptEngine.GetVariable(ptr, tid);
					variables[binder.Name] = variable = new Variable(instance, ptr, tid);
				}
				result = ScriptEngine.GetVariable(variable.Address, variable.TypeId, variable.Instance);
				return true;
			}
			catch(Exception ex)
			{
				ScriptEngine.Log ("Exception caught while fetching '{0}' variable of module '{1}': {2}.", binder.Name, Name, ex.Message);
				result = null;
				return false;
			}
		}
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			try
			{
				Variable variable;
				if(!variables.TryGetValue(binder.Name, out variable))
				{
					int idx = GetGlobalVarIndexByName(binder.Name);
					var ptr = GetAddressOfGlobalVar(idx);
					if(ptr == IntPtr.Zero)
					{
						ScriptEngine.Log ("Global variable does not exist.");
						return false;
					}
					int tid;
					GetGlobalVar(idx, out tid);
					var instance = ScriptEngine.GetVariable(ptr, tid);
					variables[binder.Name] = variable = new Variable(instance, ptr, tid);
				}
				ScriptEngine.SetVariable (variable.Address, variable.TypeId, value);
				return true;
			}
			catch(Exception ex)
			{
				ScriptEngine.Log ("Exception caught while fetching '{0}' variable of module '{1}': {2}.", Name, binder.Name, ex.Message);		
				return false;
			}
		}

		Dictionary<string, ScriptFunction> functions = new Dictionary<string, ScriptFunction>();
        ScriptFunction GetFunction(string name)
        {
            ScriptFunction func = null;
            if (!functions.TryGetValue(name, out func))
                functions[name] = func = GetFunctionByName(name);
            return func;
        }
		public override bool TryInvokeMember (InvokeMemberBinder binder, object[] args, out object result)
		{
			try
			{
                var func = GetFunction(binder.Name);
                if (func != null)
                {
                    using (var ctx = ScriptEngine.CreateContext())
                    {
                        ctx.ContextInfo = Name + "@" + binder.Name;
                        ctx.Prepare(func);
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
                    ScriptEngine.Log("Unable to fetch global function: '{0}' from module '{1}'. Maybe it's overloaded?", Name);
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
    }
}

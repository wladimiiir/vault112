using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline.AngelScript
{
    public partial class ScriptObject
    {
		public void AddRef()
		{
			ScriptEngine.AddRefScriptObject(this, objectType.TypeId);
		}
        public void Release()
		{
			ScriptEngine.ReleaseScriptObject(this, objectType.TypeId);
		}
    }
}

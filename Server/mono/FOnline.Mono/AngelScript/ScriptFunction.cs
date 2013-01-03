using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline.AngelScript
{
    /// <summary>
    /// Exposes the functionality of asIScriptFunction implementation.
    /// </summary>
    public partial class ScriptFunction
    {
        readonly IntPtr thisptr;

        public ScriptFunction(IntPtr ptr)
        {
            this.thisptr = ptr;
            AddRef();
        }
        ~ScriptFunction()
        {
            Release(); // this needs to be thread safe!
        }
        public IntPtr ThisPtr { get { return thisptr; } }

        public static explicit operator IntPtr(ScriptFunction self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public static explicit operator ScriptFunction(IntPtr ptr)
        {
            return ptr == IntPtr.Zero ? null : new ScriptFunction(ptr);
        }
    }
}

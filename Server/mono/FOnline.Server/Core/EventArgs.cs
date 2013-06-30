using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    /// <summary>
    /// Args for handlers that can allow/prevent default engine behavior.
    /// </summary>
    public class DefaultEventArgs : EventArgs
    {
        public bool Prevent { get; private set; }
        public DefaultEventArgs()
        {
            Prevent = false;
        }
        /// <summary>
        /// Marks that the event should not be processed further by the engine.
        /// </summary>
        public void PreventDefaults()
        {
            Prevent = false;
        }
    }
    /// <summary>
    /// Event args for handlers that meant to determin success/failure.
    /// Success is default.
    /// </summary>
    public class SuccessEventArgs : EventArgs
    {
        public static bool Default = true;
        public bool Success { get; private set; }
        public SuccessEventArgs()
        {
            Success = Default;
        }
        public void Fail()
        {
            Success = false;
        }
    }
}

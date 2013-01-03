using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public partial class Map : IManagedWrapper
    {
        readonly IntPtr thisptr;

        public Map(IntPtr ptr)
	    {
            thisptr = ptr;
            maps[ptr] = this;
            AddRef();
	    }
        ~Map()
        {
            Release();
        }
        public static explicit operator Map(IntPtr ptr)
        {
            return Global.MapManager.FromNativeMap(ptr);
        }
        public IntPtr ThisPtr { get { return thisptr; } }

        // for dev purposes
        static Dictionary<IntPtr, Map> maps = new Dictionary<IntPtr, Map>();
        public static IEnumerable<Map> AllMaps { get { return maps.Values; } } 
        public static Map Add(IntPtr ptr)
        {
            return new Map(ptr);
        }
        public static void Remove(Map map)
        {
            maps.Remove(map.ThisPtr);
        }
    }
    /// <summary>
    /// ScriptArray for maps.
    /// </summary>
    public sealed class MapArray : HandleArray<Map>
    {
        static readonly IntPtr type;
        public MapArray()
            : base(type)
        {

        }
        internal MapArray(IntPtr ptr)
            : base(ptr, true)
        {
        }
        static MapArray()
        {
            type = ScriptArray.GetType("array<Map@>");
        }
        public override Map FromNative(IntPtr ptr)
        {
            return (Map)GetObjectAddress(ptr);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public partial class Location : IManagedWrapper
    {
        readonly IntPtr thisptr;
        internal Location(IntPtr ptr)
        {
            thisptr = ptr;
            AddRef();
        }
        ~Location()
        {
            Release();
        }

        public IntPtr ThisPtr { get { return thisptr; } }
        
        static Dictionary<uint, Location> locations = new Dictionary<uint, Location>();
        static Location Add(IntPtr ptr)
        {
            var loc = new Location(ptr);
            locations[loc.Id] = loc;
            return loc;
        }
        static void Remove(Location loc)
        {
            locations.Remove(loc.Id);
        }
    }
    public sealed class LocationArray : HandleArray<Location>
    {
        static readonly IntPtr type;
        public LocationArray()
            : base(type)
        {

        }
        internal LocationArray(IntPtr ptr)
            : base(ptr, true)
        {
        }
        static LocationArray()
        {
            type = ScriptArray.GetType("array<Location@>");
        }
        public override Location FromNative(IntPtr ptr)
        {
            return new Location(ptr);
        }
    }
}

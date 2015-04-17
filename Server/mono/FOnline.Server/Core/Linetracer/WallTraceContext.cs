using System;

namespace FOnline
{
	public class WallTraceContext : ITraceContext
	{
		public bool Check (Map map, ushort hexX, ushort hexY)
		{
			if (map.IsHexRaked (hexX, hexY))
				return true;

			var sceneries = new SceneryArray ();
			map.GetSceneries (hexX, hexY, sceneries);

			foreach (var scenery in sceneries) {
				ProtoItem proto = Global.GetProtoItem (scenery.ProtoId);
				if (proto == null)
					continue;
				if ((proto.Flags & ItemFlag.LightThru) != 0)
					return true;
			}

			//cannot see through
			return false;
		}
	}
}


using System;

namespace FOnline
{
	public class Linetracer
	{
		private static readonly float SQRT3_FLOAT = (float)System.Math.Sqrt(3);
		private static readonly float SQRT3T2_FLOAT = SQRT3_FLOAT * 2;
		private static readonly float RAD2DEG = (float)(180 / System.Math.PI);
		private static readonly float BIAS_FLOAT = 0.02f;
		private readonly ITraceContext context;
		
		public Linetracer(ITraceContext context)
		{
			this.context = context;
		}
		
		private float GetDirectionF(ushort fromHexX, ushort fromHexY, ushort toHexX, ushort toHexY)
		{
			float nx = 3 * ((float)toHexX - (float)fromHexX);
			float ny = SQRT3T2_FLOAT * ((float)toHexY - (float)fromHexY) - ((float)(toHexX % 2) - (float)(fromHexX % 2)) * SQRT3_FLOAT;
			return (float)(180.0f + RAD2DEG * System.Math.Atan2(ny, nx)); // in degrees, because cvet loves the degrees
		}
		
		public uint TraceDistance(Map map, ushort fromHexX, ushort fromHexY, ushort toHexX, ushort toHexY, uint maxDistance)
		{
			if (maxDistance == 0)
				maxDistance = Global.GetDistantion(fromHexX, fromHexX, toHexX, toHexY);
			float dir = GetDirectionF(fromHexX, fromHexY, toHexX, toHexY);
			Direction dir1, dir2;
			if ((30.0f <= dir) && (dir < 90.0f)) {
				dir1 = Direction.NorthWest;
				dir2 = Direction.NorthEast;
			} else if ((90.0f <= dir) && (dir < 150.0f)) {
				dir1 = Direction.West;
				dir2 = Direction.NorthWest;
			} else if ((150.0f <= dir) && (dir < 210.0f)) {
				dir1 = Direction.SouthWest;
				dir2 = Direction.West;
			} else if ((210.0f <= dir) && (dir < 270.0f)) {
				dir1 = Direction.SouthEast;
				dir2 = Direction.SouthWest;
			} else if ((270.0f <= dir) && (dir < 330.0f)) {
				dir1 = Direction.East;
				dir2 = Direction.SouthEast;
			} else {
				dir1 = Direction.NorthEast;
				dir2 = Direction.East;
			}
			
			ushort cx = fromHexX;
			ushort cy = fromHexY;
			ushort px = fromHexX;
			ushort py = fromHexY;
			
			ushort t1x, t1y, t2x, t2y;
			
			float x1 = 3 * (float)fromHexX + BIAS_FLOAT;
			float y1 = SQRT3T2_FLOAT * (float)fromHexY - ((float)(fromHexX % 2)) * SQRT3_FLOAT + BIAS_FLOAT;
			
			float x2 = 3 * (float)(toHexX) + BIAS_FLOAT;
			float y2 = SQRT3T2_FLOAT * (float)(toHexY) - ((float)(toHexX % 2)) * SQRT3_FLOAT + BIAS_FLOAT;
			
			float dx = x2 - x1;
			float dy = y2 - y1;
			
			float c1x, c1y, c2x, c2y; // test hex
			float dist1, dist2;
			
			for (uint i = 1; i <= maxDistance; i++) {
				t1x = cx;
				t2x = cx;
				t1y = cy;
				t2y = cy;
				map.MoveHexByDir(ref t1x, ref t1y, dir1, 1);
				map.MoveHexByDir(ref t2x, ref t2y, dir2, 1);
				c1x = 3 * (float)(t1x);
				c1y = SQRT3T2_FLOAT * (float)(t1y) - ((float)(t1x % 2)) * SQRT3_FLOAT;
				c2x = 3 * (float)(t2x);
				c2y = SQRT3T2_FLOAT * (float)(t2y) - ((float)(t2x % 2)) * SQRT3_FLOAT;
				dist1 = dx * (y1 - c1y) - dy * (x1 - c1x);
				dist2 = dx * (y1 - c2y) - dy * (x1 - c2x);
				dist1 = ((dist1 > 0) ? dist1 : -dist1);
				dist2 = ((dist2 > 0) ? dist2 : -dist2);
				if (dist1 <= dist2) {
					cx = t1x;
					cy = t1y;
				} else { // left hand biased
					cx = t2x;
					cy = t2y;
				}
				
				// if MoveHexByDir tried to leave the map and failed:
				if ((cx == px) && (cy == py))
					return i - 1;
				else {
					px = cx;
					py = cy;
				}
				
				if (!context.Check(map, cx, cy))
					return i;
				
			}
			return maxDistance;
			
		}
	}
	
	/// <summary>
	/// Trace context used for checking if given hex is stop for tracing.
	/// </summary>
	public interface ITraceContext
	{
		/// <summary>
		/// Checks hex specified by hexX and hexY on Map and returns true if tracing should continue.
		/// </summary>
		/// <param name="map">Map for tracing</param>
		/// <param name="hexX">X coordinate of hex to check</param>
		/// <param name="hexY">Y coordinate of hex to check</param>
		bool Check(Map map, ushort hexX, ushort hexY);
	}
}
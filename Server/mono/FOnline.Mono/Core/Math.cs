using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    // TODO: merge with IRandom?
    public interface IMath
    {
        uint GetDistantion(ushort hx1, ushort hy1, ushort hx2, ushort hy2);
        Direction GetDirection(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy);
        Direction GetOffsetDir(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float offset);
        float Distance(float x1, float y1, float x2, float y2);
    }
    public class Math : IMath
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_GetDistantion(ushort hx1, ushort hy1, ushort hx2, ushort hy2);
        public uint GetDistantion(ushort hx1, ushort hy1, ushort hx2, ushort hy2)
        {
            return Global_GetDistantion(hx1, hy1, hx2, hy2);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static byte Global_GetDirection(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy);
        public Direction GetDirection(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy)
        {
            return (Direction)Global_GetDirection(from_hx, from_hy, to_hx, to_hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static byte Global_GetOffsetDir(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float offset);
        public Direction GetOffsetDir(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float offset)
        {
            return (Direction)Global_GetOffsetDir(from_hx, from_hy, to_hx, to_hy, offset);
        }
        public float Distance(float x1, float y1, float x2, float y2)
        {
            return (float)System.Math.Sqrt((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2));
        }
   }
}

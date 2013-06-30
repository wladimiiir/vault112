using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;

namespace FOnline
{
    public class Serializator
    {
        readonly int defaultGrow = 128;

        List<byte> buffer;
        int curPos = 0;
        int DataSize { get { return buffer.Count; } }

        public Serializator()
        {
            buffer = new List<byte>();
        }
        public Serializator(int approxSize)
        {
            buffer = new List<byte>(approxSize);
        }

        public void GrowBuffer(int length)
        {
            buffer.Capacity += length;
        }

        public bool Save(string name)
        {
            if(buffer.Count==0) return false;
            bool result=Global.SetAnyData(name,new UInt8Array(buffer));
            Clear();
            return result;
        }

        public bool Load(string name)
        {
            Clear();
            if (!Global.IsAnyData(name)) return false;
            var data = new UInt8Array();
            if(!Global.GetAnyData(name,data)) return false;
            buffer.AddRange(data);
            return true;
        }

        void Clear()
        {
            curPos=0;
            buffer = new List<byte>();
        }

        public Serializator SetCurPos(int pos)
        {
            if(pos>buffer.Count)
                buffer.Capacity += (pos-buffer.Count+defaultGrow);
            curPos=pos;
            return this;
        }

        public Serializator Fill(byte value, int length)
        {
            for(uint i=0;i<length;i++) 
                buffer[curPos++]=value;
            return this;
        }
        public Serializator Set(long value)
        {
            buffer.Add((byte)((value>>56)&0xFF));
            buffer.Add((byte)((value>>48)&0xFF));
            buffer.Add((byte)((value>>40)&0xFF));
            buffer.Add((byte)((value>>32)&0xFF));
            buffer.Add((byte)((value>>24)&0xFF));
            buffer.Add((byte)((value>>16)&0xFF));
            buffer.Add((byte)((value>>8)&0xFF));
            buffer.Add((byte)(value&0xFF));
            curPos+=8;
            return this;
        }

        public Serializator Set(int value)
        {
            buffer.Add((byte)((value>>24)&0xFF));
            buffer.Add((byte)((value>>16)&0xFF));
            buffer.Add((byte)((value>>8)&0xFF));
            buffer.Add((byte)(value&0xFF));
            curPos+=4;
            return this;
        }

        public Serializator Set(short value)
        {
            buffer.Add((byte)((value>>8)&0xFF));
            buffer.Add((byte)(value&0xFF));
            curPos+=2;
            return this;
        }

        public Serializator Set(sbyte value)
        {
            buffer.Add((byte)value);
            curPos+=1;
            return this;
        }

        public Serializator Set(ulong value)
        {
            buffer.Add((byte)((value>>56)&0xFF));
            buffer.Add((byte)((value>>48)&0xFF));
            buffer.Add((byte)((value>>40)&0xFF));
            buffer.Add((byte)((value>>32)&0xFF));
            buffer.Add((byte)((value>>24)&0xFF));
            buffer.Add((byte)((value>>16)&0xFF));
            buffer.Add((byte)((value>>8)&0xFF));
            buffer.Add((byte)(value&0xFF));
            curPos+=8;
            return this;
        }

        public Serializator Set(uint value)
        {
            buffer.Add((byte)((value>>24)&0xFF));
            buffer.Add((byte)((value>>16)&0xFF));
            buffer.Add((byte)((value>>8)&0xFF));
            buffer.Add((byte)(value&0xFF));
            curPos+=4;
            return this;
        }

        public Serializator Set(ushort value)
        {
            buffer.Add((byte)((value>>8)&0xFF));
            buffer.Add((byte)(value&0xFF));
            curPos+=2;
            return this;
        }

        public Serializator Set(byte value)
        {
            buffer.Add(value);
            curPos+=1;
            return this;
        }

        public Serializator Set(bool value)
        {
            buffer.Add((byte)(value?1:0));
            curPos+=1;
            return this;
        }

        /*public Serializator Set(string& value)
        {
            uint len=value.length();
            if(CurPos+len+1>BufSize) GrowBuffer(CurPos+len+1-BufSize+DEFAULT_GROW);
            for(uint i=0;i<len;i++) buffer.Add(value[i];
            buffer.Add(0;
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }*/

        /*public Serializator Set(float& value)
        {
            int dummy=FloatToInt(value);
            return Set(dummy);
        }*/

        /*public Serializator Set(array<int64>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen*8;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<int32>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen*4;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<int16>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen*2;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<int8>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<uint64>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen*8;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<uint32>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen*4;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<uint16>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen*2;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<uint8>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<bool>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<string>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen; // Length and zeros
            for(uint i=0,j=valuesLen;i<j;i++) len+=values[i].length();
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(values[i]);
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(array<float>& values)
        {
            uint valuesLen=values.length();
            uint len=4+valuesLen*4;
            if(CurPos+len>BufSize) GrowBuffer(CurPos+len-BufSize);
            Set(valuesLen);
            for(uint i=0,j=valuesLen;i<j;i++) Set(FloatToInt(values[i]));
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(Critter& cr)
        {
            if(CurPos+4>BufSize) GrowBuffer();
            uint value=cr.Id;
            buffer.Add((value>>24)&0xFF;
            buffer.Add((value>>16)&0xFF;
            buffer.Add((value>>8)&0xFF;
            buffer.Add(value&0xFF;
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }

        public Serializator Set(Item& item)
        {
            if(CurPos+4>BufSize) GrowBuffer();
            uint value=item.Id;
            buffer.Add((value>>24)&0xFF;
            buffer.Add((value>>16)&0xFF;
            buffer.Add((value>>8)&0xFF;
            buffer.Add(value&0xFF;
            if(CurPos>DataSize) DataSize=CurPos;
            return this;
        }*/

        public Serializator Get(out ulong value)
        {
            value=0;
            if(curPos+8>DataSize) return this;
            value|=(ulong)buffer[curPos++]<<56;
            value|=(ulong)buffer[curPos++]<<48;
            value|=(ulong)buffer[curPos++]<<40;
            value|=(ulong)buffer[curPos++]<<32;
            value|=(ulong)buffer[curPos++]<<24;
            value|=(ulong)buffer[curPos++]<<16;
            value|=(ulong)buffer[curPos++]<<8;
            value|=(ulong)buffer[curPos++];
            return this;
        }

        public Serializator Get(out uint value)
        {
            value=0;
            if(curPos+4>DataSize) return this;
            value|=(uint)buffer[curPos++]<<24;
            value|=(uint)buffer[curPos++]<<16;
            value|=(uint)buffer[curPos++]<<8;
            value|=(uint)buffer[curPos++];
            return this;
        }

        public Serializator Get(out ushort value)
        {
            value=0;
            if(curPos+2>DataSize) return this;
            value|=(ushort)(buffer[curPos++]<<8);
            value|=(ushort)buffer[curPos++];
            return this;
        }

        public Serializator Get(out byte value)
        {
            value=0;
            if(curPos+1>DataSize) return this;
            value=buffer[curPos++];
            return this;
        }

        public Serializator Get(out bool value)
        {
            value=false;
            if(curPos+1>DataSize) return this;
            value=buffer[curPos++]==1?true:false;
            return this;
        }

        public Serializator Get(out string str)
        {
            int len=0;
            for(int i=curPos;;i++)
            {
                if(i==DataSize)
                {
                    str="";
                    return this;
                }
                if(buffer[i]==0)
                {
                    len=i-curPos;
                    break;
                }
            }
            var chars = buffer.Skip(curPos).Take (len).Select (b => (char)b).ToArray();
            str = new string(chars, 0, chars.Length);
            curPos++; // Skip zero
            return this;
        }

        /*public Serializator Get(float value)
        {
            int dummy=0;
            Get(dummy);
            value=IntToFloat(dummy);
            return this;
        }*/

        public Serializator Get(IList<Int64> values)
        {
            uint valuesLen;
            Get(out valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                ulong value;
                Get(out value);
                values.Add ((long)value);
            }
            return this;
        }

        public Serializator opShr(IList<Int64> values)
        {
            return Get(values);
        }

        public Serializator Get(out IList<int> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<int>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                uint value;
                Get(out value);
                values.Add((int)value);
            }
            return this;
        }

        /*public Serializator Get(IList<float> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<float>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++)
            {
                int dummy=0;
                Get(dummy);
                values[i]=IntToFloat(dummy);
            }
            return this;
        }*/

        public Serializator Get(IList<short> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<short>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                ushort v;
                Get(out v);
                values.Add ((short)v);
            }
            return this;
        }

        public Serializator opShr(IList<Int16> values)
        {
            return Get(values);
        }

        public Serializator Get(IList<sbyte> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<sbyte>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                byte v;
                Get(out v);
                values.Add ((sbyte)v);
            }
            return this;
        }

        public Serializator opShr(IList<sbyte> values)
        {
            return Get(values);
        }

        public Serializator Get(IList<ulong> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<ulong>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                ulong v;
                Get(out v);
                values.Add (v);
            }
            return this;
        }

        public Serializator Get(IList<uint> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<uint>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                uint v;
                Get (out v);
                values.Add (v);
            }
            return this;
        }

        public Serializator Get(IList<ushort> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<ushort>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                ushort v;
                Get(out v);
                values.Add (v);
            }
            return this;
        }

        public Serializator opShr(IList<UInt16> values)
        {
            return Get(values);
        }

        public Serializator Get(IList<byte> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<byte>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                byte v;
                Get(out v);
                values.Add(v);
            }
            return this;
        }

        public Serializator Get(IList<bool> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<bool>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                bool v;
                Get(out v);
                values.Add (v);
            }
            return this;
        }

        public Serializator Get(IList<string> values)
        {
            uint valuesLen=0;
            Get(out valuesLen);
            values = new List<string>((int)valuesLen);
            for(uint i=0;i<valuesLen;i++) 
            {
                string v;
                Get(out v);
                values.Add (v);
            }
            return this;
        }

        public Serializator opShr(IList<string> values)
        {
            return Get(values);
        }

        public Serializator Get(out Critter cr)
        {
            cr = null;
            if(curPos+4>DataSize) return this;
            uint id=0;
            id|=(uint)buffer[curPos++]<<24;
            id|=(uint)buffer[curPos++]<<16;
            id|=(uint)buffer[curPos++]<<8;
            id|=(uint)buffer[curPos++];
            cr=Global.GetCritter(id);
            return this;
        }

        public Serializator Get(out Item item)
        {
            item=null;
            if(curPos+4>DataSize) return this;
            uint id=0;
            id|=(uint)buffer[curPos++]<<24;
            id|=(uint)buffer[curPos++]<<16;
            id|=(uint)buffer[curPos++]<<8;
            id|=(uint)buffer[curPos++];
            item=Global.GetItem(id);
            return this;
        }
    }
}


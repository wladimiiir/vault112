using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public partial class Map
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr ptr);
        public virtual void AddRef()
        {
            AddRef(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr ptr);
        public virtual void Release()
        {
            Release(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Map_GetProtoId(IntPtr thisptr);
        public virtual ushort GetProtoId()
        {
            return Map_GetProtoId(thisptr);
        }
        public virtual ushort ProtoId
        {
            get { return Map_GetProtoId(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetLocation(IntPtr thisptr);
        public virtual Location GetLocation()
        {
            return new Location(Map_GetLocation(thisptr));
        }
        public virtual Location Location
        {
            get { return new Location(Map_GetLocation(thisptr)); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_SetScript(IntPtr thisptr, IntPtr script);
        public virtual bool SetScript(string script)
        {
			var ss = new ScriptString(script);
            return Map_SetScript(thisptr, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetScriptId(IntPtr thisptr);
        public virtual uint GetScriptId()
        {
            return Map_GetScriptId(thisptr);
        }
        public virtual uint ScriptId
        {
            get { return Map_GetScriptId(thisptr); }
        }
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //extern static bool Map_SetEvent(IntPtr thisptr, int event_type, IntPtr func_name);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetLoopTime(IntPtr thisptr, uint num_loop, uint ms);
        public virtual void SetLoopTime(uint num_loop, uint ms)
        {
            Map_SetLoopTime(thisptr, num_loop, ms);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static byte Map_GetRain(IntPtr thisptr);
        public virtual byte GetRain()
        {
            return Map_GetRain(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetRain(IntPtr thisptr, byte capacity);
        public virtual void SetRain(byte capacity)
        {
            Map_SetRain(thisptr, capacity);
        }
        public virtual byte Rain
        {
            get { return Map_GetRain(thisptr); }
            set { Map_SetRain(thisptr, value); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int Map_GetTime(IntPtr thisptr);
        public virtual int GetTime()
        {
            return Map_GetTime(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetTime(IntPtr thisptr, int time);
        public virtual void SetTime(int time)
        {
            Map_SetTime(thisptr, time);
        }
        public virtual int Time
        {
            get { return Map_GetTime(thisptr); }
            set { Map_SetTime(thisptr, value); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetDayTime(IntPtr thisptr, uint day_part);
        public virtual uint GetDayTime(uint day_part)
        {
            return Map_GetDayTime(thisptr, day_part);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetDayTime(IntPtr thisptr, uint day_part, uint time);
        public virtual void SetDayTime(uint day_part, uint time)
        {
            Map_SetDayTime(thisptr, day_part, time);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_GetDayColor(IntPtr thisptr, uint day_part, ref byte r, ref byte g, ref byte b);
        public virtual void GetDayColor(uint day_part, ref byte r, ref byte g, ref byte b)
        {
            Map_GetDayColor(thisptr, day_part, ref r, ref g, ref b);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetDayColor(IntPtr thisptr, uint day_part, byte r, byte g, byte b);
        public virtual void SetDayColor(uint day_part, byte r, byte g, byte b)
        {
            Map_SetDayColor(thisptr, day_part, r, g, b);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetTurnBasedAvailability(IntPtr thisptr, bool value);
        public virtual void SetTurnBasedAvailability(bool value)
        {
            Map_SetTurnBasedAvailability(thisptr, value);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_IsTurnBasedAvailability(IntPtr thisptr);
        public virtual bool IsTurnBasedAvailability()
        {
            return Map_IsTurnBasedAvailability(thisptr);
        }
        public virtual bool TurnBasedEnabled
        {
            get { return Map_IsTurnBasedAvailability(thisptr); }
            set { Map_SetTurnBasedAvailability(thisptr, value); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_BeginTurnBased(IntPtr thisptr, IntPtr first_turn_crit);
        public virtual void BeginTurnBased(Critter first_turn_crit)
        {
            Map_BeginTurnBased(thisptr, first_turn_crit.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_IsTurnBased(IntPtr thisptr);
        public virtual bool IsTurnBased()
        {
            return Map_IsTurnBased(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EndTurnBased(IntPtr thisptr);
        public virtual void EndTurnBased()
        {
            Map_EndTurnBased(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int Map_GetTurnBasedSequence(IntPtr thisptr, IntPtr critter_ids);
        public virtual int GetTurnBasedSequence(UIntArray critter_ids)
        {
            return Map_GetTurnBasedSequence(thisptr, critter_ids != null ? critter_ids.ThisPtr : IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetData(IntPtr thisptr, uint index, int value);
        public virtual void SetData(uint index, int value)
        {
            Map_SetData(thisptr, index, value);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int Map_GetData(IntPtr thisptr, uint index);
        public virtual int GetData(uint index)
        {
            return Map_GetData(thisptr, index);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_AddItem(IntPtr thisptr, ushort hx, ushort hy, ushort pid, uint count);
        public virtual Item AddItem(ushort hx, ushort hy, ushort pid, uint count)
        {
            return (Item)Map_AddItem(thisptr, hx, hy, pid, count);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetItem(IntPtr thisptr, uint item_id);
        public virtual Item GetItem(uint item_id)
        {
            return (Item)Map_GetItem(thisptr, item_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetItemHex(IntPtr thisptr, ushort hx, ushort hy, ushort pid);
        public virtual Item GetItem(ushort hx, ushort hy, ushort pid)
        {
            return (Item)Map_GetItemHex(thisptr, hx, hy, pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetItemsHex(IntPtr thisptr, ushort hx, ushort hy, IntPtr items);
        public virtual uint GetItems(ushort hx, ushort hy, ItemArray items)
        {
            return Map_GetItemsHex(thisptr, hx, hy, items != null ? items.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetItemsHexEx(IntPtr thisptr, ushort hx, ushort hy, uint radius, ushort pid, IntPtr items);
        public virtual uint GetItems(ushort hx, ushort hy, uint radius, ushort pid, ItemArray items)
        {
            return Map_GetItemsHexEx(thisptr, hx, hy, radius, pid, items != null ? items.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetItemsByPid(IntPtr thisptr, ushort pid, IntPtr items);
        public virtual uint GetItems(ushort pid, ItemArray items)
        {
            return Map_GetItemsByPid(thisptr, pid, items != null ? items.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetItemsByType(IntPtr thisptr, int type, IntPtr items);
        public virtual uint GetItemsByType(int type, ItemArray items)
        {
            return Map_GetItemsByType(thisptr, type, items != null ? items.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetDoor(IntPtr thisptr, ushort hx, ushort hy);
        public virtual Item GetDoor(ushort hx, ushort hy)
        {
            return (Item)Map_GetDoor(thisptr, hx, hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetCar(IntPtr thisptr, ushort hx, ushort hy);
        public virtual Item GetCar(ushort hx, ushort hy)
        {
            return (Item)Map_GetCar(thisptr, hx, hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetSceneriesHex(IntPtr thisptr, ushort hx, ushort hy, IntPtr sceneries);
        public virtual uint GetSceneries(ushort hx, ushort hy, SceneryArray sceneries)
        {
            return Map_GetSceneriesHex(thisptr, hx, hy, sceneries != null ? sceneries.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetSceneriesHexEx(IntPtr thisptr, ushort hx, ushort hy, uint radius, ushort pid, IntPtr sceneries);
        public virtual uint GetSceneries(ushort hx, ushort hy, uint radius, ushort pid, SceneryArray sceneries)
        {
            return Map_GetSceneriesHexEx(thisptr, hx, hy, radius, pid, sceneries != null ? sceneries.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetSceneriesByPid(IntPtr thisptr, ushort pid, IntPtr sceneries);
        public virtual uint GetSceneriesByPid(ushort pid, SceneryArray sceneries)
        {
            return Map_GetSceneriesByPid(thisptr, pid,sceneries != null ? sceneries.ThisPtr : IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetCritterHex(IntPtr thisptr, ushort hx, ushort hy);
        public virtual Critter GetCritter(ushort hx, ushort hy)
        {
            return (Critter)Map_GetCritterHex(thisptr, hx, hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetCritterById(IntPtr thisptr, uint critter_id);
        public virtual Critter GetCritter(uint critter_id)
        {
            return (Critter)Map_GetCritterById(thisptr, critter_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetCritters(IntPtr thisptr, ushort hx, ushort hy, uint radius, int find_type, IntPtr critters);
        public virtual uint GetCrittersHex(ushort hx, ushort hy, uint radius, Find find_type, CritterArray critters)
        {
            return Map_GetCritters(thisptr, hx, hy, radius, (int)find_type, critters != null ? critters.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetCrittersByPids(IntPtr thisptr, ushort pid, int find_type, IntPtr critters);
        public virtual uint GetCritters(ushort pid, Find find_type, CritterArray critters = null)
        {
            return Map_GetCrittersByPids(thisptr, pid, (int)find_type, critters != null ? critters.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetCrittersInPath(IntPtr thisptr, ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float angle, uint dist, int find_type, IntPtr critters);
        public virtual uint GetCrittersPath(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float angle, uint dist, Find find_type, CritterArray critters)
        {
            return Map_GetCrittersInPath(thisptr, from_hx, from_hy, to_hx, to_hy, angle, dist, (int)find_type, critters != null ? critters.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetCrittersInPath(IntPtr thisptr, ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float angle, uint dist, int find_type, IntPtr critters, out ushort pre_block_hx, out ushort pre_block_hy, out ushort block_hx, out ushort block_hy);
        public virtual uint GetCrittersPath(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float angle, uint dist, Find find_type, CritterArray critters, out ushort pre_block_hx, out ushort pre_block_hy, out ushort block_hx, out ushort block_hy)
        {
            return Map_GetCrittersInPath(thisptr, from_hx, from_hy, to_hx, to_hy, angle, dist, (int)find_type, critters != null ? critters.ThisPtr : IntPtr.Zero, out pre_block_hx, out pre_block_hy, out block_hx, out block_hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetCrittersWhoViewPath(IntPtr thisptr, ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float angle, uint dist, int find_type, IntPtr critters);
        public virtual uint GetCrittersWhoViewPath(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, float angle, uint dist, Find find_type, CritterArray critters)
        {
            return Map_GetCrittersWhoViewPath(thisptr, from_hx, from_hy, to_hx, to_hy, angle, dist, (int)find_type, critters != null ? critters.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetCrittersSeeing(IntPtr thisptr, IntPtr critters, bool look_on_them, int find_type, IntPtr critters_result);
        public virtual uint GetCrittersSeeing(CritterArray critters, bool look_on_them, Find find_type, CritterArray critters_result)
        {
            return Map_GetCrittersSeeing(thisptr, critters.ThisPtr, look_on_them, (int)find_type, critters_result != null ? critters_result.ThisPtr : IntPtr.Zero);
        }
 
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_GetHexInPath(IntPtr thisptr, ushort from_hx, ushort from_hy, ref ushort to_hx, ref ushort to_hy, float angle, uint dist);
        public virtual void GetHexCoord(ushort from_hx, ushort from_hy, ref ushort to_hx, ref ushort to_hy, float angle, uint dist)
        {
            Map_GetHexInPath(thisptr, from_hx, from_hy, ref to_hx, ref to_hy, angle, dist);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_GetHexInPathWall(IntPtr thisptr, ushort from_hx, ushort from_hy, ref ushort to_hx, ref ushort to_hy, float angle, uint dist);
        public virtual void GetHexCoordWall(ushort from_hx, ushort from_hy, ref ushort to_hx, ref ushort to_hy, float angle, uint dist)
        {
            Map_GetHexInPathWall(thisptr, from_hx, from_hy, ref to_hx, ref to_hy, angle, dist);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetPathLengthHex(IntPtr thisptr, ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, uint cut);
        public virtual uint GetPathLength(ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy, uint cut)
        {
            return Map_GetPathLengthHex(thisptr, from_hx, from_hy, to_hx, to_hy, cut);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetPathLengthCr(IntPtr thisptr, IntPtr cr, ushort to_hx, ushort to_hy, uint cut);
        public virtual uint GetPathLength(Critter cr, ushort to_hx, ushort to_hy, uint cut)
        {
            return Map_GetPathLengthCr(thisptr, cr.ThisPtr, to_hx, to_hy, cut);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_VerifyTrigger(IntPtr thisptr, IntPtr cr, ushort hx, ushort hy, byte dir);
        public virtual bool VerifyTrigger(Critter cr, ushort hx, ushort hy, Direction dir)
        {
            return Map_VerifyTrigger(thisptr, cr.ThisPtr, hx, hy, (byte)dir);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_AddNpc(IntPtr thisptr, ushort pid, ushort hx, ushort hy, byte dir, IntPtr _params, IntPtr items, IntPtr script);
        public virtual Critter AddNpc(ushort pid, ushort hx, ushort hy, Direction dir, IntArray parameters, IntArray items, string script)
        {
            return (Critter)Map_AddNpc(thisptr, pid, hx, hy, (byte)dir, (IntPtr)parameters, (IntPtr)items, ((ScriptString)script).ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetNpcCount(IntPtr thisptr, int npc_role, int find_type);
        public virtual uint GetNpcCount(int npc_role, int find_type)
        {
            return Map_GetNpcCount(thisptr, npc_role, find_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Map_GetNpc(IntPtr thisptr, int npc_role, int find_type, uint skip_count);
        public virtual Critter GetNpc(int npc_role, int find_type, uint skip_count)
        {
            return (Critter)Map_GetNpc(thisptr, npc_role, find_type, skip_count);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_CountEntire(IntPtr thisptr, int entire);
        public virtual uint CountEntire(int entire)
        {
            return Map_CountEntire(thisptr, entire);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Map_GetEntires(IntPtr thisptr, int entire, IntPtr entires, IntPtr hx, IntPtr hy);
        public virtual uint GetEntires(int entire, UIntArray entires, UInt16Array hx, UInt16Array hy)
        {
            return Map_GetEntires(thisptr, entire, (IntPtr)entires, (IntPtr)hx, (IntPtr)hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_GetEntireCoords(IntPtr thisptr, int entire, uint skip, ref ushort hx, ref ushort hy);
        public virtual bool GetEntireCoords(int entire, uint skip, ref ushort hx, ref ushort hy)
        {
            return Map_GetEntireCoords(thisptr, entire, skip, ref hx, ref hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_GetEntireCoordsDir(IntPtr thisptr, int entire, uint skip, ref ushort hx, ref ushort hy, ref byte dir);
        public virtual bool GetEntireCoords(int entire, uint skip, ref ushort hx, ref ushort hy, ref Direction dir)
        {
            byte dir_ = (byte)dir;
            bool res = Map_GetEntireCoordsDir(thisptr, entire, skip, ref hx, ref hy, ref dir_);
            dir = (Direction)dir_;
            return res;
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_GetNearEntireCoords(IntPtr thisptr, ref int entire, ref ushort hx, ref ushort hy);
        public virtual bool GetNearEntireCoords(ref int entire, ref ushort hx, ref ushort hy)
        {
            return Map_GetNearEntireCoords(thisptr, ref entire, ref hx, ref hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_GetNearEntireCoordsDir(IntPtr thisptr, ref int entire, ref ushort hx, ref ushort hy, ref byte dir);
        public virtual bool GetNearEntireCoords(ref int entire, ref ushort hx, ref ushort hy, ref Direction dir)
        {
            byte dir_ = (byte)dir;
            bool res = Map_GetNearEntireCoordsDir(thisptr, ref entire, ref hx, ref hy, ref dir_);
            dir = (Direction)dir_;
            return res;
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_IsHexPassed(IntPtr thisptr, ushort hx, ushort hy);
        public virtual bool IsHexPassed(ushort hx, ushort hy)
        {
            return Map_IsHexPassed(thisptr, hx, hy);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_IsHexRaked(IntPtr thisptr, ushort hx, ushort hy);
        public virtual bool IsHexRaked(ushort hx, ushort hy)
        {
            return Map_IsHexRaked(thisptr, hx, hy);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetText(IntPtr thisptr, ushort hx, ushort hy, uint color, IntPtr text);
        public virtual void SetText(ushort hx, ushort hy, uint color, string text)
        {
			var ss = new ScriptString(text);
            Map_SetText(thisptr, hx, hy, color, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetTextMsg(IntPtr thisptr, ushort hx, ushort hy, uint color, ushort text_msg, uint str_num);
        public virtual void SetTextMsg(ushort hx, ushort hy, uint color, ushort text_msg, uint str_num)
        {
            Map_SetTextMsg(thisptr, hx, hy, color, text_msg, str_num);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_SetTextMsgLex(IntPtr thisptr, ushort hx, ushort hy, uint color, ushort text_msg, uint str_num, IntPtr lexems);
        public virtual void SetTextMsgLex(ushort hx, ushort hy, uint color, ushort text_msg, uint str_num, string lexems)
        {
			var ss = new ScriptString(lexems);
            Map_SetTextMsgLex(thisptr, hx, hy, color, text_msg, str_num, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_RunEffect(IntPtr thisptr, ushort pid, ushort hx, ushort hy, ushort radius);
        public virtual void RunEffect(ushort pid, ushort hx, ushort hy, ushort radius)
        {
            Map_RunEffect(thisptr, pid, hx, hy, radius);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_RunFlyEffect(IntPtr thisptr, ushort pid, IntPtr from_cr, IntPtr to_cr, ushort from_x, ushort from_y, ushort to_x, ushort to_y);
        public virtual void RunFlyEffect(ushort pid, Critter from_cr, Critter to_cr, ushort from_hx, ushort from_hy, ushort to_hx, ushort to_hy)
        {
            Map_RunFlyEffect(thisptr, pid, (IntPtr)from_cr, (IntPtr)to_cr, from_hx, from_hy, to_hx, to_hy);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Map_CheckPlaceForItem(IntPtr thisptr, ushort hx, ushort hy, ushort pid);
        public virtual bool CheckPlaceForItem(ushort hx, ushort hy, ushort pid)
        {
            return Map_CheckPlaceForItem(thisptr, hx, hy, pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_BlockHex(IntPtr thisptr, ushort hx, ushort hy, bool full);
        public virtual void BlockHex(ushort hx, ushort hy, bool full)
        {
            Map_BlockHex(thisptr, hx, hy, full);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_UnblockHex(IntPtr thisptr, ushort hx, ushort hy);
        public virtual void UnblockHex(ushort hx, ushort hy)
        {
            Map_UnblockHex(thisptr, hx, hy);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_PlaySound(IntPtr thisptr, IntPtr sound_name);
        public virtual void PlaySound(string sound_name)
        {
			var ss = new ScriptString(sound_name);
            Map_PlaySound(thisptr, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_PlaySoundRadius(IntPtr thisptr, IntPtr sound_name, ushort hx, ushort hy, uint radius);
        public virtual void PlaySound(string sound_name, ushort hx, ushort hy, uint radius)
        {
			var ss = new ScriptString(sound_name);
            Map_PlaySoundRadius(thisptr, ss.ThisPtr, hx, hy, radius);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_Reload(IntPtr thisptr);
        public virtual void Reload()
        {
            Map_Reload(thisptr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Map_GetWidth(IntPtr thisptr);
        public virtual ushort GetWidth()
        {
            return Map_GetWidth(thisptr);
        }
        public virtual ushort Width
        {
            get { return Map_GetWidth(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Map_GetHeight(IntPtr thisptr);
        public virtual ushort GetHeight()
        {
            return Map_GetHeight(thisptr);
        }
        public virtual ushort Height
        {
            get { return Map_GetWidth(thisptr); }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_MoveHexByDir(IntPtr thisptr, ref ushort hx, ref ushort hy, byte dir, uint steps);
        public virtual void MoveHexByDir(ref ushort hx, ref ushort hy, Direction dir, uint steps)
        {
            Map_MoveHexByDir(thisptr, ref hx, ref hy, (byte)dir, steps);
        }
    
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventFinish(IntPtr thisptr, bool deleted);
        public virtual void EventFinish(bool deleted)
        {
            Map_EventFinish(thisptr, deleted);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventLoop0(IntPtr thisptr);
        public virtual void EventLoop0()
        {
            Map_EventLoop0(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventLoop1(IntPtr thisptr);
        public virtual void EventLoop1()
        {
            Map_EventLoop1(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventLoop2(IntPtr thisptr);
        public virtual void EventLoop2()
        {
            Map_EventLoop2(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventLoop3(IntPtr thisptr);
        public virtual void EventLoop3()
        {
            Map_EventLoop3(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventLoop4(IntPtr thisptr);
        public virtual void EventLoop4()
        {
            Map_EventLoop4(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventInCritter(IntPtr thisptr, IntPtr cr);
        public virtual void EventInCritter(Critter cr)
        {
            Map_EventInCritter(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventOutCritter(IntPtr thisptr, IntPtr cr);
        public virtual void EventOutCritter(Critter cr)
        {
            Map_EventOutCritter(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventCritterDead(IntPtr thisptr, IntPtr cr, IntPtr killer);
        public virtual void EventCritterDead(Critter cr, Critter killer)
        {
            Map_EventCritterDead(thisptr, cr.ThisPtr, (IntPtr)killer);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventTurnBasedBegin(IntPtr thisptr);
        public virtual void EventTurnBasedBegin()
        {
            Map_EventTurnBasedBegin(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventTurnBasedEnd(IntPtr thisptr);
        public virtual void EventTurnBasedEnd()
        {
            Map_EventTurnBasedEnd(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Map_EventTurnBasedProcess(IntPtr thisptr, bool begin_turn);
        public virtual void EventTurnBasedProcess(bool begin_turn)
        {
            Map_EventTurnBasedProcess(thisptr, begin_turn);
        }
    }
}

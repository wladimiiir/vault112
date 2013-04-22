using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public partial class Critter
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void AddRef(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Release(IntPtr ptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static int GetRefCount(IntPtr ptr);
		[MethodImpl(MethodImplOptions.InternalCall)]
        extern static string GetName(IntPtr ptr);

		public virtual void AddRef()
        {
            AddRef(thisptr);
        }
        public virtual void Release()
        {
            Release(thisptr);
        }
		public virtual int RefCount
		{
			get { return GetRefCount(thisptr); }
		}
		
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsPlayer(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsNpc(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsCanWalk(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsCanRun(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsCanRotate(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsCanAim(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsAnim1(IntPtr ptr, uint index);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int Cl_GetAccess(IntPtr ptr);


        public virtual bool IsPlayer 
        {
            get { return Crit_IsPlayer(thisptr); }
        }
        public virtual bool IsNpc
        { 
            get { return Crit_IsNpc(thisptr); }
        }
        public virtual bool IsCanWalk 
        {
            get { return Crit_IsCanWalk(thisptr); }
        }
        public virtual bool IsCanRun 
        {
            get { return Crit_IsCanRun(thisptr); }
        }
        public virtual bool IsCanRotate 
        { 
            get { return Crit_IsCanRotate(thisptr); }
        }
        public virtual bool IsCanAim 
        {
            get { return Crit_IsCanRotate(thisptr); }
        }
        public virtual bool IsAnim1(uint index) { return Crit_IsAnim1(thisptr, index); }
        public virtual AccessType GetAccess() { return (AccessType)Cl_GetAccess(thisptr); } // maybe move to derived Client class?
        
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetLexems(IntPtr thisptr, IntPtr lexems);
        public virtual void SetLexems(string lexems)
        {
			var ss = new ScriptString(lexems);
            Crit_SetLexems(thisptr, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Crit_GetMap(IntPtr thisptr);
        public virtual Map GetMap()
        {
            return (Map)Crit_GetMap(thisptr);
        }
        public virtual Map Map
        {
            get { return (Map)Crit_GetMap(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetMapId(IntPtr thisptr);
        public virtual uint GetMapId()
        {
            return Crit_GetMapId(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Crit_GetMapProtoId(IntPtr thisptr);
        public virtual ushort GetMapProtoId()
        {
            return Crit_GetMapProtoId(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetHomePos(IntPtr thisptr, ushort hx, ushort hy, byte dir);
        public virtual void SetHomePos(ushort hx, ushort hy, Direction dir)
        {
            Crit_SetHomePos(thisptr, hx, hy, (byte)dir);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_GetHomePos(IntPtr thisptr, out uint map_id, out ushort hx, out ushort hy, out byte dir);
        public virtual void GetHomePos(out uint map_id, out ushort hx, out ushort hy, out Direction dir)
        {
            //hx = hy = 0;
            byte dir_ = 0;
            Crit_GetHomePos(thisptr, out map_id, out hx, out hy, out dir_);
            dir = (Direction)dir_;
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_ChangeCrType(IntPtr thisptr, uint new_type);
        public virtual bool ChangeCrType(uint new_type)
        {
            return Crit_ChangeCrType(thisptr, new_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Cl_DropTimers(IntPtr thisptr);
        public virtual void DropTimers()
        {
            Cl_DropTimers(thisptr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_MoveRandom(IntPtr thisptr);
        public virtual bool MoveRandom()
        {
            return Crit_MoveRandom(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_MoveToDir(IntPtr thisptr, byte dir);
        public virtual bool MoveToDir(Direction dir)
        {
            return Crit_MoveToDir(thisptr, (byte)dir);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_TransitToHex(IntPtr thisptr, ushort hx, ushort hy, byte dir);
        public virtual bool TransitToHex(ushort hx, ushort hy, Direction dir)
        {
            return Crit_TransitToHex(thisptr, hx, hy, (byte)dir);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_TransitToMapHex(IntPtr thisptr, uint map_id, ushort hx, ushort hy, byte dir, bool with_group);
        public virtual bool TransitToMap(uint map_id, ushort hx, ushort hy, Direction dir, bool with_group = false)
        {
            return Crit_TransitToMapHex(thisptr, map_id, hx, hy, (byte)dir, with_group);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_TransitToMapEntire(IntPtr thisptr, uint map_id, int entire_num, bool with_group);
        public virtual bool TransitToMap(uint map_id, int entire_num, bool with_group = false)
        {
            return Crit_TransitToMapEntire(thisptr, map_id, entire_num, with_group);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_TransitToGlobal(IntPtr thisptr, bool request_group);
        public virtual bool TransitToGlobal(bool request_group)
        {
            return Crit_TransitToGlobal(thisptr, request_group);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_TransitToGlobalWithGroup(IntPtr thisptr, IntPtr group);
        public virtual bool TransitToGlobal(CritterArray array)
        {
            return Crit_TransitToGlobalWithGroup(thisptr, array.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_TransitToGlobalGroup(IntPtr thisptr, uint critter_id);
        public virtual bool TransitToGlobalGroup(uint critter_id)
        {
            return Crit_TransitToGlobalGroup(thisptr, critter_id);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_AddScore(IntPtr thisptr, uint score, int val);
        public virtual void AddScore(uint score, int val)
        {
            Crit_AddScore(thisptr, score, val);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int Crit_GetScore(IntPtr thisptr, uint score);
        public virtual int GetScore(uint score)
        {
            return Crit_GetScore(thisptr, score);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_AddHolodiskInfo(IntPtr thisptr, uint holodisk_num);
        public virtual void AddHolodiskInfo(uint holodisk_num)
        {
            Crit_AddHolodiskInfo(thisptr, holodisk_num);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EraseHolodiskInfo(IntPtr thisptr, uint holodisk_num);
        public virtual void EraseHolodiskInfo(uint holodisk_num)
        {
            Crit_EraseHolodiskInfo(thisptr, holodisk_num);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsHolodiskInfo(IntPtr thisptr, uint holodisk_num);
        public virtual bool IsHolodiskInfo(uint holodisk_num)
        {
            return Crit_IsHolodiskInfo(thisptr, holodisk_num);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsLife(IntPtr thisptr);
        public virtual bool IsLife
        {
            get { return Crit_IsLife(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsKnockout(IntPtr thisptr);
        public virtual bool IsKnockout
        {
            get { return Crit_IsKnockout(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsDead(IntPtr thisptr);
        public virtual bool IsDead
        {
            get { return Crit_IsDead(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsFree(IntPtr thisptr);
        public virtual bool IsFree
        {
            get { return Crit_IsFree(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsBusy(IntPtr thisptr);
        public virtual bool IsBusy
        {
            get { return Crit_IsBusy(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_Wait(IntPtr thisptr, uint ms);
        public virtual void Wait(uint ms)
        {
            Crit_Wait(thisptr, ms);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_ToDead(IntPtr thisptr, uint anim2, IntPtr killer);
        public virtual void ToDead(uint anim2, Critter killer = null)
        {
            Crit_ToDead(thisptr, anim2, killer != null ? killer.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_ToLife(IntPtr thisptr);
        public virtual bool ToLife()
        {
            return Crit_ToLife(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_ToKnockout(IntPtr thisptr, uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, ushort knock_hx, ushort knock_hy);
        public virtual bool ToKnockout(uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, ushort knock_hx, ushort knock_hy)
        {
            return Crit_ToKnockout(thisptr, anim2_begin, anim2_idle, anim2_end, lost_ap, knock_hx, knock_hy);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_RefreshVisible(IntPtr thisptr);
        public virtual void RefreshVisible()
        {
            Crit_RefreshVisible(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_ViewMap(IntPtr thisptr, IntPtr map, uint look, ushort hx, ushort hy, byte dir);
        public virtual void ViewMap(Map map, uint look, ushort hx, ushort hy, Direction dir)
        {
            Crit_ViewMap(thisptr, map.ThisPtr, look, hx, hy, (byte)dir);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Crit_AddItem(IntPtr thisptr, ushort pid, uint count);
        public virtual Item AddItem(ushort pid, uint count)
        {
            return (Item)Crit_AddItem(thisptr, pid, count);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_DeleteItem(IntPtr thisptr, ushort pid, uint count);
        public virtual bool DeleteItem(ushort pid, uint count)
        {
            return Crit_DeleteItem(thisptr, pid, count);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_ItemsCount(IntPtr thisptr);
        public virtual uint ItemsCount
        {
            get { return Crit_ItemsCount(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_ItemsWeight(IntPtr thisptr);
        public virtual uint ItemsWeight
        {
            get { return Crit_ItemsWeight(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_ItemsVolume(IntPtr thisptr);
        public virtual uint ItemsVolume
        {
            get { return Crit_ItemsVolume(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_CountItem(IntPtr thisptr, ushort pid);
        public virtual uint CountItem(ushort pid)
        {
            return Crit_CountItem(thisptr, pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Crit_GetItem(IntPtr thisptr, ushort pid, int slot);
        public virtual Item GetItem(ushort pid, Nullable<ItemSlot> slot = null)
        {
            return (Item)Crit_GetItem(thisptr, pid, slot != null ? (int)slot : -1);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Crit_GetItemById(IntPtr thisptr, uint item_id);
        public virtual Item GetItemById(uint item_id)
        {
            return (Item)Crit_GetItemById(thisptr, item_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetItems(IntPtr thisptr, int slot, IntPtr items);
        public virtual uint GetItems(Nullable<ItemSlot> slot, ItemArray items)
        {
            return Crit_GetItems(thisptr, slot != null ? (int)slot : -1, items != null ? items.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetItemsByType(IntPtr thisptr, int type, IntPtr items);
        public virtual uint GetItemsByType(ItemType type, ItemArray items)
        {
            return Crit_GetItems(thisptr, (int)type, items != null ? items.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Crit_GetSlotProto(IntPtr thisptr, int slot, ref byte mode);
        public virtual ProtoItem GetSlotProto(ItemSlot slot, ref byte mode)
        {
            return new ProtoItem(Crit_GetSlotProto(thisptr, (int)slot, ref mode));
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_MoveItem(IntPtr thisptr, uint item_id, uint count, byte to_slot);
        public virtual bool MoveItem(uint item_id, uint count, ItemSlot to_slot)
        {
            return Crit_MoveItem(thisptr, item_id, count, (byte)to_slot);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_PickItem(IntPtr thisptr, ushort hx, ushort hy, ushort pid);
        public virtual bool PickItem(ushort hx, ushort hy, ushort pid)
        {
            return Crit_PickItem(thisptr, hx, hy, pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetFavoriteItem(IntPtr thisptr, int slot, ushort pid);
        public virtual void SetFavoriteItem(ItemSlot slot, ushort pid)
        {
            Crit_SetFavoriteItem(thisptr, (int)slot, pid);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Crit_GetFavoriteItem(IntPtr thisptr, int slot);
        public virtual ushort GetFavoriteItem(ItemSlot slot)
        {
            return Crit_GetFavoriteItem(thisptr, (int)slot);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetCritters(IntPtr thisptr, bool look_on_me, int find_type, IntPtr critters);
        public virtual uint GetCritters(bool look_on_me, Find find_type, CritterArray critters)
        {
            return Crit_GetCritters(thisptr, look_on_me, (int)find_type, critters != null ? critters.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetFollowGroup(IntPtr thisptr, int find_type, IntPtr critters);
        public virtual uint GetFollowGroup(Find find_type, CritterArray critters)
        {
            return Crit_GetFollowGroup(thisptr, (int)find_type, critters != null ? critters.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Crit_GetFollowLeader(IntPtr thisptr);
        public virtual Critter FollowLeader
        {
            get { return (Critter)Crit_GetFollowLeader(thisptr); }
        }
        public virtual Critter GetFollowLeader()
        {
            return (Critter)Crit_GetFollowLeader(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Crit_GetGlobalGroup(IntPtr thisptr);
        public virtual IList<Critter> GlobalGroup
        {
            get { return new CritterArray(Crit_GetGlobalGroup(thisptr)); }
        }
        public virtual IList<Critter> GetGlobalGroup()
        {
            return new CritterArray(Crit_GetGlobalGroup(thisptr));
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsGlobalGroupLeader(IntPtr thisptr);
        public virtual bool IsGlogalGroupLeader
        {
            get { return Crit_IsGlobalGroupLeader(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_LeaveGlobalGroup(IntPtr thisptr);
        public virtual void LeaveGlobalGroup()
        {
            Crit_LeaveGlobalGroup(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_GiveGlobalGroupLead(IntPtr thisptr, IntPtr to_cr);
        public virtual void GiveGlobalGroupLead(Critter to_cr)
        {
            Crit_GiveGlobalGroupLead(thisptr, to_cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Npc_GetTalkedPlayers(IntPtr thisptr, IntPtr players);
        public virtual uint GetTalkedPlayers(CritterArray players)
        {
            return Npc_GetTalkedPlayers(thisptr, players != null ? players.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsSeeCr(IntPtr thisptr, IntPtr cr);
        public virtual bool IsSeeCr(Critter cr)
        {
            return Crit_IsSeeCr(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsSeenByCr(IntPtr thisptr, IntPtr cr);
        public virtual bool IsSeenByCr(Critter cr)
        {
            return Crit_IsSeenByCr(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_IsSeeItem(IntPtr thisptr, IntPtr item);
        public virtual bool IsSee(Item item)
        {
            return Crit_IsSeeItem(thisptr, item.ThisPtr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_Say(IntPtr thisptr, byte how_say, IntPtr text);
        public virtual void Say(Say how_say, string text)
        {
			var ss = new ScriptString(text);
            Crit_Say(thisptr, (byte)how_say, ss.ThisPtr);
        }
        public virtual void Say(Say how_say, string text, params object[] args)
        {
			var ss = new ScriptString(string.Format(text, args));
            Crit_Say(thisptr, (byte)how_say, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SayMsg(IntPtr thisptr, byte how_say, ushort text_msg, uint str_num);
        public virtual void SayMsg(Say how_say, ushort text_msg, uint str_num)
        {
            Crit_SayMsg(thisptr, (byte)how_say, text_msg, str_num);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SayMsgLex(IntPtr thisptr, byte how_say, ushort text_msg, uint str_num, IntPtr lexems);
        public virtual void SayMsg(Say how_say, ushort text_msg, uint str_num, string lexems)
        {
			var ss = new ScriptString(lexems);
            Crit_SayMsgLex(thisptr, (byte)how_say, text_msg, str_num, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetDir(IntPtr thisptr, byte dir);
        public virtual void SetDir(Direction dir)
        {
            Crit_SetDir(thisptr, (byte)dir);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Npc_ErasePlane(IntPtr thisptr, int plane_type, bool all);
        public virtual uint ErasePlane(PlaneType plane_type, bool all)
        {
            return Npc_ErasePlane(thisptr, (int)plane_type, all);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Npc_ErasePlaneIndex(IntPtr thisptr, uint index);
        public virtual bool ErasePlane(uint index)
        {
            return Npc_ErasePlaneIndex(thisptr, index);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Npc_DropPlanes(IntPtr thisptr);
        public virtual void DropPlanes()
        {
            Npc_DropPlanes(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Npc_IsNoPlanes(IntPtr thisptr);
        public virtual bool IsNoPlanes
        {
            get { return Npc_IsNoPlanes(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Npc_IsCurPlane(IntPtr thisptr, int plane_type);
        public virtual bool IsCurPlane(PlaneType plane_type)
        {
            return Npc_IsCurPlane(thisptr, (int)plane_type);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr Npc_GetCurPlane(IntPtr thisptr);
        public virtual NpcPlane GetCurPlane() // maybe the fact that it's method call and not property will be a remainder of performance hit of creating new wrapper every time
        {
            return new NpcPlane(Npc_GetCurPlane(thisptr));
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Npc_GetPlanes(IntPtr thisptr, IntPtr planes);
        public virtual uint GetPlanes(NpcPlaneArray planes)
        {
            return Npc_GetPlanes(thisptr, (IntPtr)planes);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Npc_GetPlanesIdentifier(IntPtr thisptr, int identifier, IntPtr planes);
        public virtual uint GetPlanes(int identifier, NpcPlaneArray planes)
        {
            return Npc_GetPlanesIdentifier(thisptr, identifier, (IntPtr)planes);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Npc_GetPlanesIdentifier2(IntPtr thisptr, int identifier, uint identifier_ext, IntPtr planes);
        public virtual uint GetPlanes(int identifier, uint identifier_ext, NpcPlaneArray planes)
        {
            return Npc_GetPlanesIdentifier2(thisptr, identifier, identifier_ext, (IntPtr)planes);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Npc_AddPlane(IntPtr thisptr, IntPtr plane);
        public virtual bool AddPlane(NpcPlane plane)
        {
            return Npc_AddPlane(thisptr, plane.ThisPtr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SendMessage(IntPtr thisptr, int num, int val, int to);
        public virtual void SendMessage(int num, int val, MessageTo to)
        {
            Crit_SendMessage(thisptr, num, val, (int)to);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_Action(IntPtr thisptr, int action, int action_ext, IntPtr item);
        public virtual void Action(CritterAction action, int action_ext, Item item)
        {
            Crit_Action(thisptr, (int)action, action_ext, (IntPtr)item);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_Animate(IntPtr thisptr, uint anim1, uint anim2, IntPtr item, bool clear_sequence, bool delay_play);
        public virtual void Animate(uint anim1, uint anim2, Item item, bool clear_sequence, bool delay_play)
        {
            Crit_Animate(thisptr, anim1, anim2, item.ThisPtr, clear_sequence, delay_play);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetAnims(IntPtr thisptr, int cond, uint anim1, uint anim2);
        public virtual void SetAnims(int cond, uint anim1, uint anim2)
        {
            Crit_SetAnims(thisptr, cond, anim1, anim2);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_PlaySound(IntPtr thisptr, IntPtr sound_name, bool send_self);
        public virtual void PlaySound(string sound_name, bool send_self)
        {
			var ss = new ScriptString(sound_name);
            Crit_PlaySound(thisptr, ss.ThisPtr, send_self);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_PlaySoundType(IntPtr thisptr, byte sound_type, byte sound_type_ext, byte sound_id, byte sound_id_ext, bool send_self);
        public virtual void PlaySound(byte sound_type, byte sound_type_ext, byte sound_id, byte sound_id_ext, bool send_self)
        {
            Crit_PlaySoundType(thisptr, sound_type, sound_type_ext, sound_id, sound_id_ext, send_self);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SendCombatResult(IntPtr thisptr, IntPtr combat_result);
        public virtual void SendCombatResult(UIntArray combat_result)
        {
            Crit_SendCombatResult(thisptr, combat_result.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Cl_IsKnownLoc(IntPtr thisptr, bool by_id, uint loc_num);
        public virtual bool IsKnownLoc(bool by_id, uint loc_num)
        {
            return Cl_IsKnownLoc(thisptr, by_id, loc_num);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Cl_SetKnownLoc(IntPtr thisptr, bool by_id, uint loc_num);
        public virtual bool SetKnownLoc(bool by_id, uint loc_num)
        {
            return Cl_SetKnownLoc(thisptr, by_id, loc_num);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Cl_UnsetKnownLoc(IntPtr thisptr, bool by_id, uint loc_num);
        public virtual bool UnsetKnownLoc(bool by_id, uint loc_num)
        {
            return Cl_UnsetKnownLoc(thisptr, by_id, loc_num);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Cl_SetFog(IntPtr thisptr, ushort zone_x, ushort zone_y, int fog);
        public virtual void SetFog(ushort zone_x, ushort zone_y, Fog fog)
        {
            Cl_SetFog(thisptr, zone_x, zone_y, (int)fog);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int Cl_GetFog(IntPtr thisptr, ushort zone_x, ushort zone_y);
        public virtual Fog GetFog(ushort zone_x, ushort zone_y)
        {
            return (Fog)Cl_GetFog(thisptr, zone_x, zone_y);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Cl_ShowContainer(IntPtr thisptr, IntPtr cont_cr, IntPtr cont_item, uint transfer_type);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Cl_ShowScreen(IntPtr thisptr, int screen_type, uint param, IntPtr func_name);
        public virtual void ShowScreen(Screen screen_type, uint param, string func_name)
        {
			var ss = new ScriptString(func_name);
            Cl_ShowScreen(thisptr, (int)screen_type, param, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Cl_RunClientScript(IntPtr thisptr, IntPtr func_name, int p0, int p1, int p2, IntPtr p3, IntPtr p4);
        public virtual void RunClientScript(string func_name, int p0, int p1, int p2, string p3, IntArray p4)
        {
			var func_name_ = new ScriptString(func_name);
			var p3_ = p3 != null ? new ScriptString(p3) : null;
            Cl_RunClientScript(thisptr, func_name_.ThisPtr, p0, p1, p2, 
                p3_ != null ? p3_.ThisPtr : IntPtr.Zero,
                p4 != null ? p4.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Cl_Disconnect(IntPtr thisptr);
        public virtual void Disconnect()
        {
            Cl_Disconnect(thisptr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_SetScript(IntPtr thisptr, IntPtr script);
        public virtual bool SetScript(string script)
        {
			var ss = new ScriptString(script);
            return Crit_SetScript(thisptr, ss.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetScriptId(IntPtr thisptr);
        public virtual uint GetScriptId()
        {
            return Crit_GetScriptId(thisptr);
        }
        public virtual uint ScriptId
        {
            get { return Crit_GetScriptId(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetBagRefreshTime(IntPtr thisptr, uint real_minutes);
        public virtual void SetBagRefreshTime(uint real_minutes)
        {
            Crit_SetBagRefreshTime(thisptr, real_minutes);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetBagRefreshTime(IntPtr thisptr);
        public virtual uint GetBagRefreshTime()
        {
            return Crit_GetBagRefreshTime(thisptr);
        }
        public virtual uint BagRefreshTime
        {
            get { return Crit_GetBagRefreshTime(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetInternalBag(IntPtr thisptr, IntPtr pids, IntPtr min_counts, IntPtr max_counts, IntPtr slots);
        public virtual void SetInternalBag(UInt16Array pids, UIntArray min_counts, UIntArray max_counts, IntArray slots)
        {
            Crit_SetInternalBag(thisptr, pids.ThisPtr, min_counts.ThisPtr, max_counts.ThisPtr, slots.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetInternalBag(IntPtr thisptr, IntPtr pids, IntPtr min_counts, IntPtr max_counts, IntPtr slots);
        public virtual uint GetInternalBag(UInt16Array pids, UIntArray min_counts, UIntArray max_counts, IntArray slots)
        {
            return Crit_GetInternalBag(thisptr, 
                pids != null ? pids.ThisPtr : IntPtr.Zero,
                min_counts != null ? min_counts.ThisPtr : IntPtr.Zero,
                max_counts != null ? max_counts.ThisPtr : IntPtr.Zero,
                slots != null ? slots.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort Crit_GetProtoId(IntPtr thisptr);
        public virtual ushort GetProtoId()
        {
            return Crit_GetProtoId(thisptr);
        }
        public virtual ushort ProtoId
        {
            get { return Crit_GetProtoId(thisptr); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetMultihex(IntPtr thisptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_SetMultihex(IntPtr thisptr, int value);
        public virtual uint GetMultihex()
        {
            return Crit_GetMultihex(thisptr);
        }
        public virtual void SetMultihex(int value)
        {
            Crit_SetMultihex(thisptr, value);
        }
        public virtual uint Multihex
        {
            get { return Crit_GetMultihex(thisptr); }
            set { Crit_SetMultihex(thisptr, (int)value); } // so... uint or int?
        }
        
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_AddEnemyInStack(IntPtr thisptr, uint critter_id);
        public virtual void AddEnemyInStack(uint critter_id)
        {
            Crit_AddEnemyInStack(thisptr, critter_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_CheckEnemyInStack(IntPtr thisptr, uint critter_id);
        public virtual bool CheckEnemyInStack(uint critter_id)
        {
            return Crit_CheckEnemyInStack(thisptr, critter_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EraseEnemyFromStack(IntPtr thisptr, uint critter_id);
        public virtual void EraseEnemyFromStack(uint critter_id)
        {
            Crit_EraseEnemyFromStack(thisptr, critter_id);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_ChangeEnemyStackSize(IntPtr thisptr, uint new_size);
        public virtual void ChangeEnemyStackSize(uint new_size)
        {
            Crit_ChangeEnemyStackSize(thisptr, new_size);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_GetEnemyStack(IntPtr thisptr, IntPtr enemy_stack);
        public virtual void GetEnemyStack(UIntArray enemy_stack)
        {
            Crit_GetEnemyStack(thisptr, enemy_stack.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_ClearEnemyStack(IntPtr thisptr);
        public virtual void ClearEnemyStack()
        {
            Crit_ClearEnemyStack(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_ClearEnemyStackNpc(IntPtr thisptr);
        public virtual void ClearEnemyStackNpc()
        {
            Crit_ClearEnemyStackNpc(thisptr);
        }

        public delegate uint CritterTimeEventHandler(IntPtr cr_ptr, int identifier, ref uint rate);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_AddTimeEvent(IntPtr thisptr, IntPtr func_name, uint duration, int identifier);
        public virtual bool AddTimeEvent(string func_name, uint duration, int identifier)
        {
            return Crit_AddTimeEvent(thisptr, CoreUtils.ParseFuncName(func_name).ThisPtr, duration, identifier);
        }
        public virtual bool AddTimeEvent(CritterTimeEventHandler cte, uint duration, int identifier)
        {
            return Crit_AddTimeEvent(thisptr, CoreUtils.ParseFuncName(cte).ThisPtr, duration, identifier);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_AddTimeEventRate(IntPtr thisptr, IntPtr func_name, uint duration, int identifier, uint rate);
        public virtual bool AddTimeEvent(string func_name, uint duration, int identifier, uint rate)
        {
            return Crit_AddTimeEventRate(thisptr, CoreUtils.ParseFuncName(func_name).ThisPtr, duration, identifier, rate);
        }
        public virtual bool AddTimeEvent(CritterTimeEventHandler cte, uint duration, int identifier, uint rate)
        {
            return Crit_AddTimeEventRate(thisptr, CoreUtils.ParseFuncName(cte).ThisPtr, duration, identifier, rate); 
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetTimeEvents(IntPtr thisptr, int identifier, IntPtr indexes, IntPtr durations, IntPtr rates);
        public virtual uint GetTimeEvents(int identifier, UIntArray indexes, UIntArray durations, UIntArray rates)
        {
            return Crit_GetTimeEvents(thisptr, identifier, (IntPtr)indexes, (IntPtr)durations, (IntPtr)rates);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_GetTimeEventsArr(IntPtr thisptr, IntPtr find_identifiers, IntPtr identifiers, IntPtr indexes, IntPtr durations, IntPtr rates);
        public virtual uint GetTimeEvents(IntArray find_identifiers, IntArray identifiers, UIntArray indexes, UIntArray durations, UIntArray rates)
        {
            return Crit_GetTimeEventsArr(thisptr, (IntPtr)find_identifiers, (IntPtr)identifiers, (IntPtr)indexes, (IntPtr)durations, (IntPtr)rates);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_ChangeTimeEvent(IntPtr thisptr, uint index, uint new_duration, uint new_rate);
        public virtual void ChangeTimeEvent(uint index, uint new_duration, uint new_rate)
        {
            Crit_ChangeTimeEvent(thisptr, index, new_duration, new_rate);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EraseTimeEvent(IntPtr thisptr, uint index);
        public virtual void EraseTimeEvent(uint index)
        {
            Crit_EraseTimeEvent(thisptr, index);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_EraseTimeEvents(IntPtr thisptr, int identifier);
        public virtual uint EraseTimeEvents(int identifier)
        {
            return Crit_EraseTimeEvents(thisptr, identifier);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Crit_EraseTimeEventsArr(IntPtr thisptr, IntPtr identifiers);
        public virtual uint EraseTimeEvents(IntArray identifiers)
        {
            return Crit_EraseTimeEventsArr(thisptr, (IntPtr)identifiers);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventIdle(IntPtr thisptr);
        public virtual void EventIdle()
        {
            Crit_EventIdle(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventFinish(IntPtr thisptr, bool deleted);
        public virtual void EventFinish(bool deleted)
        {
            Crit_EventFinish(thisptr, deleted);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventDead(IntPtr thisptr, IntPtr killer);
        public virtual void EventDead(Critter killer)
        {
            Crit_EventDead(thisptr, killer != null ? killer.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventRespawn(IntPtr thisptr);
        public virtual void EventRespawn()
        {
            Crit_EventRespawn(thisptr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventShowCritter(IntPtr thisptr, IntPtr cr);
        public virtual void EventShowCritter(Critter cr)
        {
            Crit_EventShowCritter(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventShowCritter1(IntPtr thisptr, IntPtr cr);
        public virtual void EventShowCritter1(Critter cr)
        {
            Crit_EventShowCritter1(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventShowCritter2(IntPtr thisptr, IntPtr cr);
        public virtual void EventShowCritter2(Critter cr)
        {
            Crit_EventShowCritter2(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventShowCritter3(IntPtr thisptr, IntPtr cr);
        public virtual void EventShowCritter3(Critter cr)
        {
            Crit_EventShowCritter3(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventHideCritter(IntPtr thisptr, IntPtr cr);
        public virtual void EventHideCritter(Critter cr)
        {
            Crit_EventHideCritter(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventHideCritter1(IntPtr thisptr, IntPtr cr);
        public virtual void EventHideCritter1(Critter cr)
        {
            Crit_EventHideCritter1(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventHideCritter2(IntPtr thisptr, IntPtr cr);
        public virtual void EventHideCritter2(Critter cr)
        {
            Crit_EventHideCritter2(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventHideCritter3(IntPtr thisptr, IntPtr cr);
        public virtual void EventHideCritter3(Critter cr)
        {
            Crit_EventHideCritter3(thisptr, cr.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventShowItemOnMap(IntPtr thisptr, IntPtr show_item, bool added, IntPtr dropper);
        public virtual void EventShowItemOnMap(Item show_item, bool added, Critter dropper)
        {
            Crit_EventShowItemOnMap(thisptr, show_item.ThisPtr, added, dropper != null ? dropper.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventChangeItemOnMap(IntPtr thisptr, IntPtr item);
        public virtual void EventChangeItemOnMap(Item item)
        {
            Crit_EventChangeItemOnMap(thisptr, item.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventHideItemOnMap(IntPtr thisptr, IntPtr hide_item, bool removed, IntPtr picker);
        public virtual void EventHideItemOnMap(Item hide_item, bool removed, Critter picker)
        {
            Crit_EventHideItemOnMap(thisptr, hide_item.ThisPtr, removed, picker != null ? picker.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventAttack(IntPtr thisptr, IntPtr target);
        public virtual bool EventAttack(Critter target)
        {
            return Crit_EventAttack(thisptr, target.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventAttacked(IntPtr thisptr, IntPtr attacker);
        public virtual bool EventAttacked(Critter attacker)
        {
            return Crit_EventAttacked(thisptr, attacker.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventStealing(IntPtr thisptr, IntPtr thief, IntPtr item, uint count);
        public virtual bool EventStealing(Critter thief, Item item, uint count)
        {
            return Crit_EventStealing(thisptr, thief.ThisPtr, item.ThisPtr, count);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventMessage(IntPtr thisptr, IntPtr from_cr, int message, int value);
        public virtual void EventMessage(Critter from_cr, int message, int value)
        {
            Crit_EventMessage(thisptr, from_cr.ThisPtr, message, value);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventUseItem(IntPtr thisptr, IntPtr item, IntPtr on_critter, IntPtr on_item, IntPtr on_scenery);
        public virtual bool EventUseItem(Item item, Critter on_critter, Item on_item, Scenery on_scenery)
        {
            return Crit_EventUseItem(thisptr, item.ThisPtr,
                on_critter != null ? on_critter.ThisPtr : IntPtr.Zero,
                on_item != null ? on_item.ThisPtr : IntPtr.Zero,
                on_scenery != null ? on_scenery.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventUseItemOnMe(IntPtr thisptr, IntPtr who_use, IntPtr item);
        public virtual bool EventUseItemOnMe(Critter who_use, Item item)
        {
            return Crit_EventUseItemOnMe(thisptr, who_use.ThisPtr, item.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventUseSkill(IntPtr thisptr, int skill, IntPtr on_critter, IntPtr on_item, IntPtr on_scenery);
        public virtual bool EventUseSkill(int skill, Critter on_critter, Item on_item, Scenery on_scenery)
        {
            return Crit_EventUseSkill(thisptr, skill,
                on_critter != null ? on_critter.ThisPtr : IntPtr.Zero,
                on_item != null ? on_item.ThisPtr : IntPtr.Zero,
                on_scenery != null ? on_scenery.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventUseSkillOnMe(IntPtr thisptr, IntPtr who_use, int skill);
        public virtual bool EventUseSkillOnMe(Critter who_use, int skill)
        {
            return Crit_EventUseSkillOnMe(thisptr, who_use.ThisPtr, skill);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventDropItem(IntPtr thisptr, IntPtr item);
        public virtual void EventDropItem(Item item)
        {
            Crit_EventDropItem(thisptr, item.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventMoveItem(IntPtr thisptr, IntPtr item, byte from_slot);
        public virtual void EventMoveItem(Item item, ItemSlot from_slot)
        {
            Crit_EventMoveItem(thisptr, item.ThisPtr, (byte)from_slot);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventKnockout(IntPtr thisptr, uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist);
        public virtual void EventKnockout(uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist)
        {
            Crit_EventKnockout(thisptr, anim2_begin, anim2_idle, anim2_end, lost_ap, knock_dist);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthDead(IntPtr thisptr, IntPtr from_cr, IntPtr killer);
        public virtual void EventSmthDead(Critter from_cr, Critter killer)
        {
            Crit_EventSmthDead(thisptr, from_cr.thisptr, killer != null ? killer.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthStealing(IntPtr thisptr, IntPtr from_cr, IntPtr thief, bool success, IntPtr item, uint count);
        public virtual void EventSmthStealing(Critter from_cr, Critter thief, bool success, Item item, uint count)
        {
            Crit_EventSmthStealing(thisptr, from_cr.ThisPtr, item.ThisPtr, success, item.ThisPtr, count);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthAttack(IntPtr thisptr, IntPtr from_cr, IntPtr target);
        public virtual void EventSmthAttack(Critter from_cr, Critter target)
        {
            Crit_EventSmthAttack(thisptr, from_cr.ThisPtr, target.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthAttacked(IntPtr thisptr, IntPtr from_cr, IntPtr attacker);
        public virtual void EventSmthAttacked(Critter from_cr, Critter attacker)
        {
            Crit_EventSmthAttacked(thisptr, from_cr.ThisPtr, attacker != null ? attacker.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthUseItem(IntPtr thisptr, IntPtr from_cr, IntPtr item, IntPtr on_critter, IntPtr on_item, IntPtr on_scenery);
        public virtual void EventSmthUseItem(Critter from_cr, Item item, Critter on_critter, Item on_item, Scenery on_scenery)
        {
            Crit_EventSmthUseItem(thisptr, from_cr.ThisPtr, item.ThisPtr,
                on_critter != null ? on_critter.ThisPtr : IntPtr.Zero,
                on_item != null ? on_item.ThisPtr : IntPtr.Zero,
                on_scenery != null ? on_scenery.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthUseSkill(IntPtr thisptr, IntPtr from_cr, int skill, IntPtr on_critter, IntPtr on_item, IntPtr on_scenery);
        public virtual void EventSmthUseSkill(Critter from_cr, int skill, Critter on_critter, Item on_item, Item on_scenery)
        {
            Crit_EventSmthUseSkill(thisptr, from_cr.ThisPtr, skill,
                on_critter != null ? on_critter.ThisPtr : IntPtr.Zero,
                on_item != null ? on_item.ThisPtr : IntPtr.Zero,
                on_scenery != null ? on_scenery.ThisPtr : IntPtr.Zero);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthDropItem(IntPtr thisptr, IntPtr from_cr, IntPtr item);
        public virtual void EventSmthDropItem(Critter from_cr, Item item)
        {
            Crit_EventSmthDropItem(thisptr, from_cr.ThisPtr, item.ThisPtr);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthMoveItem(IntPtr thisptr, IntPtr from_cr, IntPtr item, byte from_slot);
        public virtual void EventSmthMoveItem(Critter from_cr, Item item, ItemSlot slot)
        {
            Crit_EventSmthMoveItem(thisptr, from_cr.ThisPtr, item.ThisPtr, (byte)slot);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthKnockout(IntPtr thisptr, IntPtr from_cr, uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist);
        public virtual void EventSmthKnockout(Critter from_cr, uint anim2_begin, uint anim2_idle, uint anim2_end, uint lost_ap, uint knock_dist)
        {
            Crit_EventSmthKnockout(thisptr, from_cr.ThisPtr, anim2_begin, anim2_end, anim2_idle, lost_ap, knock_dist);
        }
/*"int EventPlaneBegin(NpcPlane& plane, int reason, Critter+ someCr, Item+ someItem)", asFUNCTION( BIND_CLASS Crit_EventPlaneBegin ), asCALL_CDECL_OBJFIRST ) );
"int EventPlaneEnd(NpcPlane& plane, int reason, Critter+ someCr, Item+ someItem)", asFUNCTION( BIND_CLASS Crit_EventPlaneEnd ), asCALL_CDECL_OBJFIRST ) );
"int EventPlaneRun(NpcPlane& plane, int reason, uint& p0, uint& p1, uint& p2)", asFUNCTION( BIND_CLASS Crit_EventPlaneRun ), asCALL_CDECL_OBJFIRST ) );
        */
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventBarter(IntPtr thisptr, IntPtr barter_cr, bool attach, uint barter_count);
        public virtual bool EventBarter(Critter barter_cr, bool attach, uint barter_count)
        {
            return Crit_EventBarter(thisptr, barter_cr.ThisPtr, attach, barter_count);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventTalk(IntPtr thisptr, IntPtr talk_cr, bool attach, uint talk_count);
        public virtual bool EventTalk(Critter talk_cr, bool attach, uint talk_count)
        {
            return Crit_EventTalk(thisptr, talk_cr.ThisPtr, attach, talk_count);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventGlobalProcess(IntPtr thisptr, int type, IntPtr car, ref float x, ref float y, ref float to_x, ref float to_y, ref float speed, ref uint encounter_descriptor, ref bool wait_for_answer);
        public virtual bool EventGlobalProcess(int type, Item car, ref float x, ref float y, ref float to_x, ref float to_y, ref float speed, ref uint encounter_descriptor, ref bool wait_for_answer)
        {
            return Crit_EventGlobalProcess(thisptr, type, car != null ? car.ThisPtr : IntPtr.Zero,
                ref x, ref y, ref to_x, ref to_y, ref speed, ref encounter_descriptor, ref wait_for_answer);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static bool Crit_EventGlobalInvite(IntPtr thisptr, IntPtr car, uint encounter_descriptor, int combat_mode, ref uint map_id, ref ushort hx, ref ushort hy, ref byte dir);
        public virtual bool EventGlobalInvite(Item car, uint encounter_descriptor, int combat_mode, ref uint map_id, ref ushort hx, ref ushort hy, ref Direction dir)
        {
            var dir_ = (byte)dir;
            bool res = Crit_EventGlobalInvite(thisptr, car != null ? car.ThisPtr : IntPtr.Zero, encounter_descriptor, combat_mode,
                ref map_id, ref hx, ref hy, ref dir_);
            dir = (Direction)dir_;
            return res;
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventTurnBasedProcess(IntPtr thisptr, IntPtr map, bool begin_turn);
        public virtual void EventTurnBasedProcess(Map map, bool begin_turn)
        {
            Crit_EventTurnBasedProcess(thisptr, map.ThisPtr, begin_turn);
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Crit_EventSmthTurnBasedProcess( IntPtr thisptr, IntPtr from_cr, IntPtr map, bool begin_turn);
        public virtual void EventSmthTurnBasedProcess(Critter from_cr, Map map, bool begin_turn)
        {
            Crit_EventSmthTurnBasedProcess(thisptr, from_cr.ThisPtr, map.ThisPtr, begin_turn);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int DataVal_Index(IntPtr thisptr, uint index, uint data_index);
        
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static IntPtr DataRef_Index(IntPtr thisptr, uint index, uint data_index);
    }
}
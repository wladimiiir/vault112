using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public partial class Critter : IManagedWrapper
    {
        readonly IntPtr thisptr;
        public Critter(IntPtr ptr)
        {
            this.thisptr = ptr;
            Param = new Data(ptr);
            InitData(ptr);
            AddRef();
        }
        ~Critter()
        {
            //Program.Log("Releasing critter: 0x{0:x}", (int)thisptr);
            Release(); // this should be thread safe
        }
        public static explicit operator IntPtr(Critter self)
        {
            return self != null ? self.ThisPtr : IntPtr.Zero;
        }
        public static explicit operator Critter(IntPtr ptr)
        {
            return Global.CritterManager.FromNative(ptr);
        }
        public IntPtr ThisPtr { get { return thisptr; } }

        // for dev purposes
        static Dictionary<IntPtr, Critter> critters = new Dictionary<IntPtr, Critter>();
        public static IEnumerable<Critter> AllCritters { get { return critters.Values; } }

        static Critter Add(IntPtr ptr)
        {
            if(critters.ContainsKey(ptr))
                throw new InvalidOperationException(string.Format("Critter 0x{0:x} already added.", (int)ptr));
            return new Critter(ptr); // TODO: allow custom factory
        }
        static void Remove(Critter cr)
        {
            critters.Remove(cr.ThisPtr);
        }

        string name = null;
        public string Name
        { 
            get
            {
                if(name == null)
                    name = GetName(thisptr);
                return name;
            }
        }

        public class Data
        {
            IntPtr crptr;
            uint dataIndex;
            public Data()
                : this(IntPtr.Zero, 0)
            {
            }
            public Data(IntPtr ptr)
                : this(ptr, 0)
            {
            }
            public Data(IntPtr ptr, uint data_index)
            {
                this.dataIndex = data_index;
                crptr = ptr;
            }
            public virtual int this[uint idx]
            {
                get 
                {
                    //Program.Log("DataVal_Index: 0x{0:x}, {1}", (int)crptr, idx);
                    return DataVal_Index(crptr, idx, dataIndex);  
                }
                set 
                {
                    //Program.Log("DataRef_Index: 0x{0:x}, {1}: 0x{2:x}", (int)crptr, idx, (int)DataRef_Index(crptr, idx, dataIndex));
                    unsafe
                    {
                        *(int*)(DataRef_Index(crptr, idx, dataIndex)) = value;
                    }
                }
            }
        }

        public virtual Data Param { get; private set; }
		partial void InitData(IntPtr ptr);
		
        public void SetTimeout(uint to, uint seconds)
        {
            Timeout[to] = (int)Time.After(seconds);
        }

        public bool CanDropItemsOnDead()
        {
            return Mode[Modes.NoSteal] == 0 && Mode[Modes.NoDrop] == 0;
        }
		
		public Item GetItemHand()
		{
			return GetItem(0, ItemSlot.Hand1);
		}
		public Item GetItemArmor()
		{
			return GetItem (0, ItemSlot.Armor);
		}
		// Timeouts in real seconds, generic values
/*		
#define FIRST_AID_TIMEOUT                        # (cr) ( __FullSecond + ( __TimeMultiplier * 100 / ( ( ( cr.Skill[ SK_FIRST_AID ] ) > 3 ? cr.Skill[ SK_FIRST_AID ] : 3 ) * 100 / MAX_SKILL_VAL ) ) * 60 ) // Orig 3 time on day, 5 min - sk
#define DOCTOR_TIMEOUT                           # (cr) ( __FullSecond + ( __TimeMultiplier * 100 / ( ( ( cr.Skill[ SK_DOCTOR ] ) > 3 ? cr.Skill[ SK_DOCTOR ] : 3 ) * 100 / MAX_SKILL_VAL ) * 3 ) * 60 )   // Orig 1 time on day, 20 min - sk
#define REPAIR_TIMEOUT                           # (cr) ( __FullSecond + REAL_MINUTE( 1 ) )                                                                                                                // Orig 4 time on day, 1 min
#define SCIENCE_TIMEOUT                          # (cr) ( __FullSecond + REAL_MINUTE( 1 ) )                                                                                                                // Orig 6 time on day, 1 min
#define LOCKPICK_TIMEOUT                         # (cr) ( __FullSecond + REAL_MINUTE( 2 ) )                                                                                                                // Orig 15 minutes, 2 min
#define STEAL_TIMEOUT                            # (cr) ( __FullSecond + REAL_MINUTE( 2 ) )                                                                                                                // 2 minutes
#define TRANSFER_TIMEOUT                         # (cr) ( __FullSecond + __TimeoutTransfer )
*/
		public int BattleTimeout
		{
			get 
			{
				return (int)(Global.FullSecond + System.Math.Max( Global.TimeoutBattle - Time.RealSecond( (uint)Stat[ Stats.ArmorClass ] ), Time.RealSecond(12)));
			}
		}
/*
#define TRAPS_TIMEOUT                            # (cr) ( __FullSecond + REAL_MINUTE( 1 ) )
*/
		public int SneakTimeout
		{
			get { return (int)(Global.FullSecond + Time.RealSecond((uint)System.Math.Max (34 - Stat[Stats.Sequence], 3))); }
		}
/*#define SNEAK_TIMEOUT                            # (cr) ( __FullSecond + REAL_SECOND( MAX( 34 - cr.Stat[ ST_SEQUENCE ], 3 ) ) ) // 34 second - sequence
#define HEALING_TIMEOUT                          # (cr) ( __FullSecond + REAL_MINUTE( 2 ) )                                     // 2 minutes
#define IS_TURN_BASED_TIMEOUT                    # (cr) ( cr.Timeout[ TO_BATTLE ] > 10000000 )
#define MAXIMUM_TIMEOUT                          ( REAL_HOUR( 5 ) )                                                             // Doctor timeout with min skill
*/
    }
	public enum MessageTo
    {
        VisibleMe = 0, // Отослать сообщения всем кто видет криттера.
        WhoSeesMe = 0, // more eng friendly
        IAmVisible = 1, // Отослать сообщения всем кого видит криттер.
        ISee = 1, // more eng friendly?
        AllOnMap = 2 // Отослать всем на карте.
    }
	public enum Cond : byte
    {
        Life = 1,
        Knockout = 2,
        Dead = 3,
        NotInGame = 4
    }
	public enum CritterAction
	{
		MOVE                              = 0,   // l
		RUN                               = 1,   // l
		MOVE_ITEM                         = 2,   // l s      from slot                                                      +
		MOVE_ITEM_SWAP                    = 3,   // l s      from slot                                                      +
		USE_ITEM                          = 4,   // l s                                                                     +
		DROP_ITEM                         = 5,   // l s      from slot                                                      +
		USE_WEAPON                        = 6,   // l        fail attack 8 bit, use index (0-2) 4-7 bits, aim 0-3 bits      +
		RELOAD_WEAPON                     = 7,   // l s                                                                     +
		USE_SKILL                         = 8,   // l s      skill index (see SK_*)
		PICK_ITEM                         = 9,   // l s                                                                     +
		PICK_CRITTER                      = 10,  // l        0 - loot, 1 - steal, 2 - push
		OPERATE_CONTAINER                 = 11,  // l s      transfer type * 10 + [0 - get, 1 - get all, 2 - put]           + (exclude get all)
		BARTER                            = 12,  //   s      0 - item taken, 1 - item given                                 +
		DODGE                             = 13,  //          0 - front, 1 - back
		DAMAGE                            = 14,  //          0 - front, 1 - back
		DAMAGE_FORCE                      = 15,  //          0 - front, 1 - back
		KNOCKOUT                          = 16,  //   s      0 - knockout anim2begin
		STANDUP                           = 17,  //   s      0 - knockout anim2end
		FIDGET                            = 18,  // l
		DEAD                              = 19,  //   s      dead type anim2 (see Anim2 in _animation.fos)
		CONNECT                           = 20,  //
		DISCONNECT                        = 21,  //
		RESPAWN                           = 22,  //   s
		REFRESH                           = 23  //   s
	}
	public enum BodyType
	{
        MEN                                   = 0,
        WOMEN                                 = 1,
        CHILDREN                              = 2,
        SUPER_MUTANT                          = 3,
        GHOUL                                 = 4,
        BRAHMIN                               = 5,
        RADSCORPION                           = 6,
        RAT                                   = 7,
        FLOATER                               = 8,
        CENTAUR                               = 9,
        ROBOT                                 = 10,
        DOG                                   = 11,
        MANTI                                 = 12,
        DEADCLAW                              = 13,
        PLANT                                 = 14,
        GECKO                                 = 15,
        ALIEN                                 = 16,
        GIANT_ANT                             = 17,
        BIG_BAD_BOSS                          = 18,
        GIANT_BEETLE                          = 19,
        GIANT_WASP                            = 20
	}
    public enum Fog
    {
        Full = 0,
        Half = 1,
        HalfEx = 2,
        None = 3
    }
    /// <summary>
    /// ScriptArray for critters.
    /// </summary>
    public sealed class CritterArray : HandleArray<Critter>
    {
        static readonly IntPtr type;
        public CritterArray()
            : base(type)
        {
        }
        internal CritterArray(IntPtr ptr)
            : base(ptr, true)
        {
        }
        static CritterArray()
        {
            type = ScriptArray.GetType("array<Critter@>");
        }
        public override Critter FromNative(IntPtr ptr)
        {
            return (Critter)GetObjectAddress(ptr);
        }
    }
}

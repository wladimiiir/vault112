// Original author: marvi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline.Den
{
    public class Ghost
    {
        class Dialogs
        {
            public const uint Ghost = 110;
            public const uint GhostTreasure = 127;
        }
        class Str
        {
            public const uint Scream = 1;
            public const uint Steal = 2;
            public const uint StealInjure = 3;
        }

        enum State
        {
            Alive = 0,
            Dead = 1
        }

        /*
           Critter initialization
         */
        public static Critter _GhostInit(IntPtr ptr, bool firstTime)
        {
            Critter ghost = new Critter(ptr);
            Init(ghost, firstTime);
            return ghost;
        }
        public static void Init(Critter ghost, bool firstTime)
        {
            if(firstTime)
            {
                // set max possible HP
                ghost.Stat[Stats.MaxLife] = 9999;
                ghost.Stat[Stats.CurrentHP] = 9999;
                // and armor class
                ghost.Stat[Stats.ArmorClass] = 90;
            }

            // syncronize ghost actual state with var value
            if(ghost.Cond == Cond.Dead && GhostState == State.Alive )
            {
                ghost.ToLife();
            }
            else if(ghost.Cond != Cond.Dead && GhostState == State.Dead)
            {
                ghost.ToDead(Anim2.DeadBurnRun, null);
            }

            ghost.Attacked += _GhostAttacked;
            ghost.Dead += _GhostDead;
            ghost.Respawn += (self, e) => GhostState = State.Alive;
            ghost.Stealing += _GhostStealing;
        }

        //
        // Event Handlers
        //

        static void _GhostAttacked(object sender, CritterAttackedEventArgs e)
        {
            var ghost = sender as Critter;
            // regenerate HP
            ghost.Stat[Stats.CurrentHP] = ghost.Stat[Stats.MaxLife];

            // scream to scary player
            ghost.SayMsg(Say.ShoutOnHead, TextMsg.Dlg, TextMsg.DlgStr(Dialogs.Ghost, Str.Scream));

            // do nothing after attack
            e.PreventDefaults();
        }

        static void _GhostDead(object sender, CritterDeadEventArgs e)
        {
            var ghost = sender as Critter;
            // ghost killed not through quest dialog
            if(GhostState != State.Dead)
            {
                // ressurect ghost
                Global.CreateTimeEvent(Time.After(60), e_RessurectGhost, ghost.Id, false);
            }
        }

        /*
           Handles stealing from ghost.
           Stealing attempt fails in any case with a chance for player to cripple right hand.
         */
        static void _GhostStealing(object sender, CritterStealingEventArgs e)
        {
            var thief = e.Thief;

            if(thief.IsPlayer)
            {
                int injureHandChance = 20;
                int injureHandRoll = Global.Random(1, 100);

                if(injureHandRoll <= injureHandChance)
                {
                    thief.Damage[Damages.RightArm] = 1;
                    thief.SayMsg(Say.NetMsg, TextMsg.Dlg, TextMsg.DlgStr(Dialogs.Ghost, Str.StealInjure));
                }
                else
                {
                    thief.SayMsg(Say.NetMsg, TextMsg.Dlg, TextMsg.DlgStr(Dialogs.Ghost, Str.Steal));
                }
            }
        }

        //
        // Dialog
        //

        /*
           Instantly kills ghost
         */
        public static void r_KillGhost(IntPtr _player, IntPtr _ghost, int val)
        {
            var player = (Critter)_player;
            var ghost = (Critter)_ghost;
            // confirm that ghost killed through quest dialog
            GhostState = State.Dead;
            // kill ghost
            ghost.ToDead(Anim2.DeadBurnRun);
            // run new dialog through time event
            Global.CreateTimeEvent(Time.After(60), e_RunTreasureDialog, player.Id, false);
        }

        //
        // Time Event Handlers
        //

        /*
           Ressurects ghost
         */
        static uint e_RessurectGhost(IntPtr ptr)
        {
            var values = new UIntArray(ptr);
            Critter ghost = Global.GetCritter(values[0]);
            if(ghost == null)
            {
                return 0;
            }

            // try to ressurect ghost
            if(ghost.ToLife())
            {
                return 0;
            }
            else
            {
                // restart event
                return 2 * 60;
            }
        }

        /*
           Runs dialog about cursed treasure
         */
        static uint e_RunTreasureDialog(IntPtr ptr)
        {
            var values = new UIntArray(ptr);
            Critter player = Global.GetCritter(values[0]);
            if(player == null)
            {
                return 0;
            }

            Global.RunDialog(player, Dialogs.GhostTreasure, player.HexX, player.HexY, false);

            return 0;
        }

        //
        // Helper functions
        //

        static State GhostState
        {
            get
            {
                GameVar isDead = Global.GetGlobalVar(GVars.den_ghost_is_dead);
                if (isDead == null)
                    throw new Exception("Global var den_ghost_is_dead not found.");
                return (State)isDead.Value;
            }
            set
            {
                GameVar isDead = Global.GetGlobalVar(GVars.den_ghost_is_dead );
                if (isDead == null)
                    throw new Exception("Global var den_ghost_is_dead not found.");
                isDead.Value = (int)value;
            }
        }
    }
}

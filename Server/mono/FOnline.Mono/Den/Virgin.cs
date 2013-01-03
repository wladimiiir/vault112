// Original author: Tab10id
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline.Den
{
    public class Virgin
    {
        // away position
        const ushort AwayX = 131;
        const ushort AwayY = 225;
        
        // home position (near Mom's Diner)
        const ushort HomeX = 134;
        const ushort HomeY = 255;

        // states
        const int IsHome = 0;
        const int IsAway = 1;

        public static Critter _VirginInit(IntPtr ptr, bool firstTime)
        {
            var virgin = new Critter(ptr);
            virgin.Idle += _VirginIdle;
            virgin.Talk += _VirginTalk;

            if(Global.CreateTimeEvent(Time.After(Time.GameMinute((uint)Global.Random(1, 60) * 60)), e_Check, virgin.Id, false) == 0 )
                Global.Log( "Time event Check create fail, {0}", Global.GetLastError() );

            virgin.Stat[Stats.Var0] = 0;
            virgin.Stat[Stats.Var1] = 0;
            return virgin;
        }

        static void _VirginIdle(object sender, CritterEventArgs e)
        {
            var virgin = sender as Critter;
            // if(npc.StatBase[ST_VAR0]>0)
            // {
            //	virgin.MoveToHex(131, 225, 3);
            // }
            if( virgin.HexX == 134 && virgin.HexY == 255 )
            {
                virgin.SendMessage(1220, 1, MessageTo.AllOnMap);
            }
            else
            {
                virgin.SendMessage(1220, 0, MessageTo.AllOnMap);
            }
            // virgin.Say(SAY_NORM_ON_HEAD, "Иди сюда, милашка!");
            virgin.Wait(10000);
        }

        static void _VirginTalk(object sender, CritterTalkEventArgs e)
        {
            var virgin = sender as Critter;
            // handle end of dialog
            if(!e.Attach)
            {
                if(virgin.Stat[Stats.Var1] == IsAway)
                {
                    GameVar virginState = Global.GetGlobalVar(GVars.den_virgin);
                    if(virginState == null)
                    {
                        Global.Log( "GetGlobalVar(GVAR_den_virgin) fail." );
                        // no matter what to return
                    }

                    virginState.Value = IsAway;

                    // reset
                    virgin.Stat[Stats.Var1] = IsHome;

                    virgin.AddWalkPlane(0, AwayX, AwayY, Direction.SouthEast, false, 2);
                }
            }
            e.PreventDefaults();
        }

        static uint e_Check(IntPtr ptr)
        {
            var values = new UIntArray(ptr);
            Critter virgin = Global.GetCritter(values[0]);
            if(virgin == null)
                return 0;

            GameVar virginState = Global.GetGlobalVar(GVars.den_virgin);
            if(virginState == null)
            {
                Global.Log( "GetGlobalVar(GVAR_den_virgin) fail." );
            }
            else
            {
                if(virgin.Stat[Stats.Var0] == 0 && virginState.Value == IsAway)
                {
                    virginState.Value = IsHome;
                    virgin.AddWalkPlane(0, HomeX, HomeY, Direction.SouthEast, false, 2);
                }
                else
                {
                    virgin.Stat[Stats.Var0]--;
                }
            }

            return 60 * 60;
        }

        public static void r_GoAway(IntPtr player, IntPtr _virgin, int val)
        {
            var virgin = (Critter)_virgin;
            virgin.Stat[Stats.Var0] = 3;
            // delay plan execution to dialog end
            virgin.Stat[Stats.Var1] = IsAway;
        }
    }
}

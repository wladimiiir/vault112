using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
    public class MapEventArgs : EventArgs
    {
        public MapEventArgs(Map map)
        {
            this.Map = map;
        }
        public Map Map { get; private set; }
    }
    public class MapChainEventArgs : DefaultEventArgs
    {
        public MapChainEventArgs(Map map)
        {
            this.Map = map;
        }
        public Map Map { get; private set; }
    }
    public class MapFinishEventArgs : MapEventArgs
    {
        public MapFinishEventArgs(Map map, bool to_delete)
            : base(map)
        {
            this.ToDelete = to_delete;
        }
        public bool ToDelete { get; private set; }
    }
    public class MapInOutCritterEventArgs : MapEventArgs
    {
        public MapInOutCritterEventArgs(Map map, Critter cr)
            : base(map)
        {
            this.Cr = cr;
        }
        public Critter Cr { get; private set; }
    }
    public class MapCritterDeadEventArgs : MapEventArgs
    {
        public MapCritterDeadEventArgs(Map map, Critter cr, Critter killer)
            : base(map)
        {
            this.Cr = cr;
            this.Killer = killer;
        }
        public Critter Cr { get; private set; }
        public Critter Killer { get; private set; }
    }
    public class MapTurnBasedProcessEventArgs : MapEventArgs
    {
        public MapTurnBasedProcessEventArgs(Map map, Critter cr, bool begin_turn)
            : base(map)
        {
            this.Cr = cr;
            this.BeginTurn = begin_turn;
        }
        public Critter Cr { get; private set; }
        public bool BeginTurn { get; private set; }
    }

    public partial class Map
    {    
        /// <summary>
        /// Raised when map is about to be garbaged.
        /// </summary>
        public event EventHandler<MapFinishEventArgs> Finish;
        // called by engine
        void RaiseFinish(bool to_delete)
        {
            if (Finish != null)
                Finish(this, new MapFinishEventArgs(this, to_delete));
        }
        public event EventHandler<MapEventArgs> Loop;
        // called by engine
        void RaiseLoop()
        {
            if (Loop != null)
                Loop(this, new MapEventArgs(this));
        }
        public event EventHandler<MapInOutCritterEventArgs> InCritter;
        // called by engine
        void RaiseInCritter(Critter cr)
        {
            if (InCritter != null)
                InCritter(this, new MapInOutCritterEventArgs(this, cr));
        }
        public event EventHandler<MapInOutCritterEventArgs> OutCritter;
        // called by engine
        void RaiseOutCritter(Critter cr)
        {
            if (OutCritter != null)
                OutCritter(this, new MapInOutCritterEventArgs(this, cr));
        }
        public event EventHandler<MapCritterDeadEventArgs> CritterDead;
        // called by engine
        void RaiseCritterDead(Critter cr, Critter killer)
        {
            if (CritterDead != null)
                CritterDead(this, new MapCritterDeadEventArgs(this, cr, killer));
        }
        public event EventHandler<MapEventArgs> TurnBasedBegin;
        // called by engine
        void RaiseTurnBasedBegin()
        {
            if (TurnBasedBegin != null)
                TurnBasedBegin(this, new MapEventArgs(this));
        }
        public event EventHandler<MapEventArgs> TurnBasedEnd;
        // called by engine
        void RaiseTurnBasedEnd()
        {
            if (TurnBasedEnd != null)
                TurnBasedEnd(this, new MapEventArgs(this));
        }
        public event EventHandler<MapTurnBasedProcessEventArgs> TurnBasedProcess;
        // called by engine
        void RaiseTurnBasedProcess(Critter cr, bool begin_turn)
        {
            if (TurnBasedProcess != null)
                TurnBasedProcess(this, new MapTurnBasedProcessEventArgs(this, cr, begin_turn));
        }
    }
}

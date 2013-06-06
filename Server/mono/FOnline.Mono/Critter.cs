using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline
{
	// Custom (project specific) properties
    public partial class Critter
    {
		// some test
		public virtual Data Stat { get; private set; }
        public virtual Data Skill { get; private set; }
        public virtual Data TagSkill { get; private set; }
        public virtual Data Timeout { get; private set; }
        public virtual Data Kill { get; private set; }
        public virtual Data Perk { get; private set; }
        public virtual Data Addiction { get; private set; }
        public virtual Data Karma { get; private set; }
        public virtual Data Damage { get; private set; }
        public virtual Data Mode { get; private set; }
        public virtual Data Trait { get; private set; }
        public virtual Data Profession { get; private set; }
        public virtual Data Reputation { get; private set; }
        public virtual Data GoodEvilList { get; private set; }
        public virtual Data Followers { get; private set; }
        public virtual Data FollowerVar { get; private set; }

        partial void InitData(IntPtr ptr)
        {
            Stat = new Data(ptr, 1);
            Skill = new Data(ptr, 2);
            TagSkill = new Data(ptr, 3);
            Timeout = new Data(ptr, 4);
            Kill = new Data(ptr, 5);
            Perk = new Data(ptr, 6);
            Addiction = new Data(ptr, 7);
            Karma = new Data(ptr, 8);
            Damage = new Data(ptr, 9);
            Mode = new Data(ptr, 10);
            Trait = new Data(ptr, 11);
            Reputation = new Data(ptr, 12);
        }

     	public bool HasExtMode(ModeExt mode)
		{
			return (this.Mode[Modes.Ext] & (int)mode) != 0;
		}
		public void SetExtMode(ModeExt mode)
        {
            this.Mode[Modes.Ext] = this.Mode[Modes.Ext] | (int)mode;
        }
        public void UnsetExtMode(ModeExt mode)
        {
            this.Mode[Modes.Ext] = (this.Mode[Modes.Ext] | (int)mode) ^ (int)mode;
        }
    }
}

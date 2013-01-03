using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace FOnline
{
    public interface ITime
    {
        ushort Year { get; }
        ushort Month { get; }
        ushort Day { get; }
        ushort Hour { get; }
        ushort Minute { get; }
        ushort Second { get; }
        uint FullSecond { get; }
        uint GetFullSecond(ushort year, ushort month, ushort day, ushort hour, ushort minute, ushort second);
        void GetGameTime(uint full_second, ref ushort year, ref ushort month, ref ushort day, ref ushort day_of_week, ref ushort hour, ref ushort minute, ref ushort second);      
    }
    public class Time : ITime
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint Global_GetFullSecond(ushort year, ushort month, ushort day, ushort hour, ushort minute, ushort second);
        public uint GetFullSecond(ushort year, ushort month, ushort day, ushort hour, ushort minute, ushort second)
        {
            return Global_GetFullSecond(year, month, day, hour, minute, second);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void Global_GetGameTime(uint full_second, ref ushort year, ref ushort month, ref ushort day, ref ushort day_of_week, ref ushort hour, ref ushort minute, ref ushort second);
        public void GetGameTime(uint full_second, ref ushort year, ref ushort month, ref ushort day, ref ushort day_of_week, ref ushort hour, ref ushort minute, ref ushort second)
        {
            Global_GetGameTime(full_second, ref year, ref month, ref day, ref day_of_week, ref hour, ref minute, ref second);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort GetYear();
        public ushort Year
        {
            get { return GetYear(); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort GetMonth();
        public ushort Month
        {
            get { return GetMonth(); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort GetDay();
        public ushort Day
        {
            get { return GetDay(); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort GetHour();
        public ushort Hour
        {
            get { return GetHour(); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort GetMinute();
        public ushort Minute
        {
            get { return GetMinute(); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static ushort GetSecond();
        public ushort Second
        {
            get { return GetSecond(); }
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static uint GetFullSecond();
        public uint FullSecond
        {
            get { return GetFullSecond(); }
        }

        // calc helpers

        static uint ElapsedTime
        {
            get { return Global.FullSecond; }
        }
        public static uint After(uint seconds)
        {
            return ElapsedTime + seconds;
        }
		public static uint RealSecond(uint seconds)
		{
			return Global.TimeMultiplier * seconds;
		}
        public static uint RealMinute(uint minutes)
        {
            return Global.TimeMultiplier * minutes * 60;
        }
        public static uint GameMinute(uint minutes)
        {
            return minutes * 60;
        }

        public static bool IsMorning(ushort hour)
        {
            return hour >= 7 && hour <= 11; // 5 hours
        }
        public static bool IsAfternoon(ushort hour)
        {
            return hour >= 12 && hour <= 21; // 10 hours
        }
        public static bool IsNight(ushort hour)
        {
            return hour >= 22 && hour <= 6; // 9 hours
        }

        public static uint GetDaysInMonth(ushort year, ushort month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: // 31
                    return 31;
                case 2: // 28-29
                    if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0)) return 29;
                    return 28;
                default: // 30
                    return 30;
            }
        }
        public static uint GetNearFullSecond(ushort year, ushort month, ushort day, ushort hour, ushort minute, ushort second)
        {
	        int jump=0; // 1 - years, 2 - months, 3 - days
	        if(year==0)
	        {
		        year=Global.Year;
		        jump=1;
	        }
	        if(month==0)
	        {
		        month=Global.Month;
		        jump=2;
	        }
	        if(day==0)
	        {
		        day=Global.Day;
		        jump=3;
	        }

	        uint fullSecond = Global.GetFullSecond(year,month,day,hour,minute,second);
	        while(fullSecond <= ElapsedTime)
	        {
		        switch(jump)
		        {
		        case 0:
			        return ElapsedTime + 1; // Time expired, call faster
		        case 1:
			        year++;
			        break;
		        case 2:
			        month++;
			        if(month>12)
			        {
				        month=1;
				        year++;
			        }
			        break;
		        case 3:
			        day++;
			        if(day>28 && day>GetDaysInMonth(year,month))
			        {
				        day=1;
				        month++;
				        if(month>12)
				        {
					        month=1;
					        year++;
				        }
			        }
			        break;
		        default:
			        break;
		        }

		        fullSecond= Global.GetFullSecond(year,month,day,hour,minute,second);
	        }
	        return fullSecond;
        }
    }
}

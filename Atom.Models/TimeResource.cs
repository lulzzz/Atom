using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models
{
    public class TimeResource : Resource
    {
        /// <summary>
        /// Default: Summer.
        /// </summary>
        public static TimeResource Season = new TimeResource() { Name = "Время года" };
        public static TimeResource Duration = new TimeResource() { Name = "Длительность" };
      
        public TimeResource():base()
        {

        }
    }


    [Flags]
    public enum Month
    {
        None = 0, 
        Jan = 1 << 0, 
        Feb = 1 << 1,  
        March = 1 << 2, 
        April = 1 << 3, 
        May = 1 << 4, 
        June = 1 << 5, 
        July = 1 << 6, 
        Auguast = 1 << 7, 
        Sep = 1 << 8,
        Oct = 1 << 9,
        Nov = 1 << 10,
        Dec = 1 << 11,
        Summer = June | July | Auguast,
        Spring = March | April | May,
        Winter = Jan | Feb | Dec,
        Authom = Sep | Oct | Nov
    }

    [Flags]
    public enum Days
    {
        None = 0b_0000_0000,  // 0
        Monday = 0b_0000_0001,  // 1
        Tuesday = 0b_0000_0010,  // 2
        Wednesday = 0b_0000_0100,  // 4
        Thursday = 0b_0000_1000,  // 8
        Friday = 0b_0001_0000,  // 16
        Saturday = 0b_0010_0000,  // 32
        Sunday = 0b_0100_0000,  // 64
        Weekend = Saturday | Sunday
    }
}

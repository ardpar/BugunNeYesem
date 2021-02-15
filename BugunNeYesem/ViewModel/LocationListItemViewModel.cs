using BugunNeYesem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.ViewModel
{
    public class LocationListItemViewModel
    {
         
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasEatKard { get; set; }
        public bool DayToGoMonday { get; set; }
        public bool DayToGoTuesday { get; set; }
        public bool DayToGoWednesday { get; set; }
        public bool DayToGoThursday { get; set; }
        public bool DayToGoFriday { get; set; }
        public bool DayToGoSaturday { get; set; }
        public bool DayToGoSunday { get; set; }


        public bool CertainDayToGoMonday { get; set; }
        public bool CertainDayToGoTuesday { get; set; }
        public bool CertainDayToGoWednesday { get; set; }
        public bool CertainDayToGoThursday { get; set; }
        public bool CertainDayToGoFriday { get; set; }
        public bool CertainDayToGoSaturday { get; set; }
        public bool CertainDayToGoSunday { get; set; }

        public void SetDayToGo(byte code)
        {
            DayToGoSunday = ((code & (byte)Weekday.Sun) != 0);
            DayToGoMonday = ((code & (byte)Weekday.Mon) != 0);
            DayToGoTuesday = ((code & (byte)Weekday.Tue) != 0);
            DayToGoWednesday = ((code & (byte)Weekday.Wed) != 0);
            DayToGoThursday = ((code & (byte)Weekday.Thu) != 0);
            DayToGoFriday = ((code & (byte)Weekday.Fri) != 0);
            DayToGoSaturday = ((code & (byte)Weekday.Sat) != 0);

        }
     

        public byte GetDayToGo()
        {
            byte Value = 0;
            // from LSB to MSB - Sunday, Monday, Tuesday, 
            // Wednesday, Thursday, Friday, Saturday
            if (DayToGoSunday) Value |= (byte)Weekday.Sun;
            if (DayToGoMonday) Value |= (byte)Weekday.Mon;
            if (DayToGoTuesday) Value |= (byte)Weekday.Tue;
            if (DayToGoWednesday) Value |= (byte)Weekday.Wed;
            if (DayToGoThursday) Value |= (byte)Weekday.Thu;
            if (DayToGoFriday) Value |= (byte)Weekday.Fri;
            if (DayToGoSaturday) Value |= (byte)Weekday.Sat;

            return Value;
        } public void SetCertainDayToGo(byte code)
        {
            CertainDayToGoSunday = ((code & (byte)Weekday.Sun) != 0);
            CertainDayToGoMonday = ((code & (byte)Weekday.Mon) != 0);
            CertainDayToGoTuesday = ((code & (byte)Weekday.Tue) != 0);
            CertainDayToGoWednesday = ((code & (byte)Weekday.Wed) != 0);
            CertainDayToGoThursday = ((code & (byte)Weekday.Thu) != 0);
            CertainDayToGoFriday = ((code & (byte)Weekday.Fri) != 0);
            CertainDayToGoSaturday = ((code & (byte)Weekday.Sat) != 0);
            
        }
        public byte GeCertainDayToGo()
        {
            byte Value = 0;
            // from LSB to MSB - Sunday, Monday, Tuesday, 
            // Wednesday, Thursday, Friday, Saturday
            if (CertainDayToGoSunday) Value |= (byte)Weekday.Sun;
            if (CertainDayToGoMonday) Value |= (byte)Weekday.Mon;
            if (CertainDayToGoTuesday) Value |= (byte)Weekday.Tue;
            if (CertainDayToGoWednesday) Value |= (byte)Weekday.Wed;
            if (CertainDayToGoThursday) Value |= (byte)Weekday.Thu;
            if (CertainDayToGoFriday) Value |= (byte)Weekday.Fri;
            if (CertainDayToGoSaturday) Value |= (byte)Weekday.Sat;

            return Value;
        }
    }
}

using BugunNeYesem.DataLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Entity
{
    public class Location: Concrete.Entity
    {
        public Location()
        {

        }
         public string Name { get; set; }
        public byte DayToGo { get; set; }
        public byte CertainDayToGo { get; set; }
        public bool HasEatKard { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();

    }
}

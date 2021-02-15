using BugunNeYesem.DataLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Entity
{
    public class History: Concrete.Entity
    {
        public History()
        {

        }
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public DateTime Date { get; set; }
    }
}

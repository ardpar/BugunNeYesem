using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Entity
{
    public class EatCardHistory : Concrete.Entity
    {
        public EatCardHistory()
        {

        }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime EndDate { get; set; }
    }
}

using BugunNeYesem.DataLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Entity
{
    public class User : Concrete.Entity, BugunNeYesem.DataLayer.Abstract.IEntity
    {
        public User()
        {

        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public bool IsAdmin { get; set; } 
        public virtual ICollection<Location> Locations { get; set; } = new HashSet<Location>();
        public virtual ICollection<EatCardHistory> EatCardHistories { get; set; } = new HashSet<EatCardHistory>();

    }

}

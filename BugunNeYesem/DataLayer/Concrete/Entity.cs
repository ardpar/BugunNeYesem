using BugunNeYesem.DataLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Concrete
{
    public abstract class Entity: IEntity
    {
        [Key]
        public int Id { get; set; }


    }
}

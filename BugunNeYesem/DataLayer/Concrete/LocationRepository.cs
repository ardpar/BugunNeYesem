using BugunNeYesem.DataLayer.Abstract;
using BugunNeYesem.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Concrete
{
    public class LocationRepository : EntityRepository<Location>, ILocationRepository
    {
        public LocationRepository(DataContext context) : base(context)
        {
        }
    }
}

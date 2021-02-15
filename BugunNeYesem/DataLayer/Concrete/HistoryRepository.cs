using BugunNeYesem.DataLayer.Abstract;
using BugunNeYesem.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Concrete
{
    public class HistoryRepository : EntityRepository<History>, IHistoryRepository
    {
        public HistoryRepository(DataContext context) : base(context)
        {

        }
        
        

    }
}

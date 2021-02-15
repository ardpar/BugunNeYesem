using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Abstract
{
    public interface IEntityRepository<T> where T: class, IEntity, new()
    {
        IQueryable<T> Table { get; }
         
        IQueryable<T> TableAsNoTracking { get; }
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity); 
    }
}

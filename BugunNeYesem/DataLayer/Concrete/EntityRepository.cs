using BugunNeYesem.DataLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.DataLayer.Concrete
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        DataContext _context;
        DbSet<T> _entities;
        public IQueryable<T> Table
        {
            get
            {
                return _entities;
            }
        }

        public IQueryable<T> TableAsNoTracking
        {
            get
            {
                return _entities.AsNoTracking();
            }
        }
        public EntityRepository(DataContext context)
        {
            _context = context;
            _entities = context.Set<T>();

        }
        public bool Add(T entity)
        {
            _context.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}

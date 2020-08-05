using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CarsContext;
using Microsoft.EntityFrameworkCore;

namespace Cars.Repository.Repository
{
   public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected Context Context { get; set; }

        public RepositoryBase(Context context)
        {
            this.Context = context;
        }
        public IQueryable<T> FindAll()
        {
            return this.Context.Set<T>();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression)
        {
            return this.Context.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            this.Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.Context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.Context.Set<T>().Remove(entity);
        }

    }
}

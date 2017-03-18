using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HomeMadeFood.Data.Repositories
{
    public interface IEfRepository<T>
        where T : class
    {
        T GetById(object id);

        IQueryable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeExpressions);
    }
}
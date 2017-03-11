using System.Linq;

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
    }
}
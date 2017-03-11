using Bytes2you.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace HomeMadeFood.Data.Repositories
{
    public class EfRepository<T> : IEfRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public EfRepository(ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;
            this.DbSet = this.dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.DbSet;
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            this.DbSet.Add(entity);
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        protected IDbSet<T> DbSet { get; set; }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }
    }
}
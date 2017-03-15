using Bytes2you.Validation;

using HomeMadeFood.Data.Repositories;
using HomeMadeFood.Models;

namespace HomeMadeFood.Data.Data
{
    public class HomeMadeFoodData : IHomeMadeFoodData
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IEfRepositoryFactory repositoryFactory;

        public HomeMadeFoodData(ApplicationDbContext dbContext, IEfRepositoryFactory repositoryFactory)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;

            Guard.WhenArgument(repositoryFactory, "repositoryFactory").IsNull().Throw();
            this.repositoryFactory = repositoryFactory;
        }

        public IEfRepository<Ingredient> Ingredients
        {
            get
            {
                return this.repositoryFactory.Create<Ingredient>();
            }
        }

        public IEfRepository<Recipie> Recipies
        {
            get
            {
                return this.repositoryFactory.Create<Recipie>();
            }
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}
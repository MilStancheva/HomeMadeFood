using HomeMadeFood.Data.Repositories;
using HomeMadeFood.Models;
using Bytes2you.Validation;

namespace HomeMadeFood.Data.Data
{
    public class HomeMadeFoodData : IHomeMadeFoodData
    {
        private readonly ApplicationDbContext dbContext;

        public HomeMadeFoodData(ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;
        }

        public IEfRepository<Ingredient> Ingredients
        {
            get
            {
                return new EfRepository<Ingredient>(this.dbContext);
            }
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}

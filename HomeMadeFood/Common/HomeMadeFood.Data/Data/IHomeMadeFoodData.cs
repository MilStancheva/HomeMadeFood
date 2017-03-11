using HomeMadeFood.Data.Repositories;
using HomeMadeFood.Models;

namespace HomeMadeFood.Data.Data
{
    public interface IHomeMadeFoodData
    {
        IEfRepository<Ingredient> Ingredients { get; }
        
        void Commit();
    }
}
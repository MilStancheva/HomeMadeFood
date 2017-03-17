using HomeMadeFood.Data.Repositories;
using HomeMadeFood.Models;

namespace HomeMadeFood.Data.Data
{
    public interface IHomeMadeFoodData
    {
        IEfRepository<FoodCategory> FoodCategories { get; }

        IEfRepository<Ingredient> Ingredients { get; }

        IEfRepository<Recipe> Recipes { get; }

        void Commit();
    }
}
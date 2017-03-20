using HomeMadeFood.Data.Repositories;
using HomeMadeFood.Models;

namespace HomeMadeFood.Data.Data
{
    public interface IHomeMadeFoodData
    {
        IEfRepository<FoodCategory> FoodCategories { get; }

        IEfRepository<Ingredient> Ingredients { get; }

        IEfRepository<Recipe> Recipes { get; }

        IEfRepository<DailyMenu> DailyMenus { get; }

        IEfRepository<DailyUserOrder> DailyUserOrders { get; }

        IEfRepository<ApplicationUser> Users { get; }

        void Commit();
    }
}
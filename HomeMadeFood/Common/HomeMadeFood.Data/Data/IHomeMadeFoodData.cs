using HomeMadeFood.Data.Repositories;
using HomeMadeFood.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HomeMadeFood.Data.Data
{
    public interface IHomeMadeFoodData
    {
        IEfRepository<FoodCategory> FoodCategories { get; }

        IEfRepository<Ingredient> Ingredients { get; }

        IEfRepository<Recipe> Recipes { get; }

        IEfRepository<ApplicationUser> Users { get; }

        IEfRepository<IdentityUserRole> Roles { get; }

        void Commit();
    }
}
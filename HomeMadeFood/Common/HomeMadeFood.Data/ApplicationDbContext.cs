using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using HomeMadeFood.Models;

namespace HomeMadeFood.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HomeMadeFoodDb", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<FoodCategory> FoodCategories { get; set; }

        public virtual IDbSet<Ingredient> Ingredients { get; set; }

        public virtual IDbSet<Recipe> Recipes { get; set; }

        public virtual IDbSet<DailyMenu> DailyMenus { get; set; }

        public virtual IDbSet<DailyUserOrder> DailyUserOrders { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
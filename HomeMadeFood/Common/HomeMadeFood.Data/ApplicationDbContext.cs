using HomeMadeFood.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HomeMadeFood.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HomeMadeFoodDb", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Ingredient> Ingredients { get; set; }

        public virtual IDbSet<Recipie> Recipies { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
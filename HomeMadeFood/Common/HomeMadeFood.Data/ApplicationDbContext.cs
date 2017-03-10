using HomeMadeFood.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HomeMadeFood.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HomeMadeFood", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
using HomeMadeFood.Models;
using System.Collections.Generic;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IUsersService
    {
        IEnumerable<ApplicationUser> GetAllUsers();

        IEnumerable<ApplicationUser> GetAllUsersWithRoles();
    }
}

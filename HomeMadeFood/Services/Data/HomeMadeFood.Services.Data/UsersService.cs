using System.Collections.Generic;

using Bytes2you.Validation;

using HomeMadeFood.Models;
using HomeMadeFood.Data.Data;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IHomeMadeFoodData data;

        public UsersService(IHomeMadeFoodData data)
        {
            Guard.WhenArgument(data, "data").IsNull().Throw();
            this.data = data;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return this.data.Users.GetAll();
        }

        public IEnumerable<ApplicationUser> GetAllUsersWithRoles()
        {
            return this.data.Users.GetAllIncluding(x => x.Roles);
        }
    }
}

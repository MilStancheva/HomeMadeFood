using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class UserViewModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    { 
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        public string Role { get; set; }
    }
}
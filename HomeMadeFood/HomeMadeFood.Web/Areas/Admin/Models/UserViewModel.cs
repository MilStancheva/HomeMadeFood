using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class UserViewModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    { 
        public string Username { get; set; }

        public string Email { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly int gridPageSize = 25;

        private readonly IMappingService mappingService;
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService, IMappingService mappingService)
        {
            Guard.WhenArgument(usersService, "usersService").IsNull().Throw();
            this.usersService = usersService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {            
            var users = this.usersService.GetAllUsersWithRoles()
                .Select(u =>
                new UserViewModel
                {
                    Username = u.UserName,
                    Email = u.Email,
                    Role = u.Roles.First().RoleId.Equals("0") ? "Admin" : "User"
                })
                 .ToList();

            var searchModel = new SearchUserViewModel();
            if (users != null)
            {
                searchModel.Users = users;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = users.Count();
            }

            return this.View(searchModel);
        }
        public ActionResult Search(string username)
        {
            var users = this.usersService.GetAllUsersWithRoles()
                .Where(x => x.UserName.ToLower().Contains(username.ToLower()))
                .Select(u =>
                new UserViewModel
                {
                    Username = u.UserName,
                    Email = u.Email,
                    Role = u.Roles.First().RoleId.Equals("0") ? "Admin" : "User"
                })
                .ToList();


            var searchModel = new SearchUserViewModel();
            if (users != null)
            {
                searchModel.Users = users;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = users.Count();
            }

            return this.PartialView("_UsersGridPartial", searchModel);
        }
    }
}
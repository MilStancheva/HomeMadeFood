using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Models;
using HomeMadeFood.Web.Areas.Admin.Models.ShoppingListViewModels;

namespace HomeMadeFood.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IDailyMenuService dailyMenuService;
        private readonly IMappingService mappingService;

        public AdminController(IDailyMenuService dailyMenuService, IMappingService mappingService)
        {
            Guard.WhenArgument(dailyMenuService, "dailyMenuService").IsNull().Throw();
            this.dailyMenuService = dailyMenuService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShoppingList()
        {
            var activeDailyMenus = this.dailyMenuService.GetFiveDailyMenusForTheNextWeek().ToList();
            var shoppingListFoodCategories = this.dailyMenuService
                .GetShoppingListOfFoodCategoriesForActiveDailyMenus(activeDailyMenus);

            var cost = this.dailyMenuService.CalculateShoppingListCostForActiveDailyMenus(shoppingListFoodCategories);
            var shoppingListFoodCategoriesViewModel = shoppingListFoodCategories
                .Select(this.mappingService.Map<FoodCategoryViewModel>);

            var shoppingListViewModel = new ShoppingListViewModel()
            {
                FoodCategories = shoppingListFoodCategoriesViewModel,
                TotalCost = cost
            };

            return this.PartialView("_ShoppingListForActiveDailyMenusPartial", shoppingListViewModel);
        }
    }
}
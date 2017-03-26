using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Models.WeeklyMenu;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDailyMenuService dailyMenuService;
        private readonly IMappingService mappingService;

        public HomeController(IDailyMenuService dailyMenuService, IMappingService mappingService)
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

        [ChildActionOnly]
        public ActionResult BackgroundVideo()
        {
            return this.PartialView("_BackgroundVideoPartial");
        }

        public ActionResult About()
        {
            ViewBag.Message = "About HomeMadeFood";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        [ChildActionOnly]
        public ActionResult WeeklyMenu()
        {
            var dailyMenus = this.dailyMenuService.GetFiveDailyMenusForTheNextWeek();
            var dailyMenuModels = new List<PublicDalilyMenuViewModel>();

            foreach (var menu in dailyMenus)
            {
                var publicDailyMenuModel = this.GetPublicDailyMenuViewModel(menu);
                dailyMenuModels.Add(publicDailyMenuModel);
            }

            WeeklyMenuViewModel weeklyMenuModel = new WeeklyMenuViewModel()
            {
                DailyMenus = dailyMenuModels
            };

            return this.PartialView("_WeeklyMenuPartial", weeklyMenuModel);
        }

        private PublicDalilyMenuViewModel GetPublicDailyMenuViewModel(DailyMenu menu)
        {
            var menuModel = this.mappingService.Map<PublicDalilyMenuViewModel>(menu);

            menuModel.Salads = menu.Recipes.Where(x => x.DishType == DishType.Salad)
                .Select(this.mappingService.Map<PublicRecipeViewModel>)
                .ToList();

            menuModel.BigSalads = menu.Recipes.Where(x => x.DishType == DishType.BigSalad)
                .Select(this.mappingService.Map<PublicRecipeViewModel>);

            menuModel.Soups = menu.Recipes.Where(x => x.DishType == DishType.Soup)
                .Select(this.mappingService.Map<PublicRecipeViewModel>)
                .ToList();

            menuModel.MainDishes = menu.Recipes.Where(x => x.DishType == DishType.MainDish)
                .Select(this.mappingService.Map<PublicRecipeViewModel>)
                .ToList();

            menuModel.Vegetarian = menu.Recipes.Where(x => x.DishType == DishType.Vegetarian)
                .Select(this.mappingService.Map<PublicRecipeViewModel>)
                .ToList();

            menuModel.Pasta = menu.Recipes.Where(x => x.DishType == DishType.Pasta)
                .Select(this.mappingService.Map<PublicRecipeViewModel>)
                .ToList();

            menuModel.BBQ = menu.Recipes.Where(x => x.DishType == DishType.BBQ)
                .Select(this.mappingService.Map<PublicRecipeViewModel>)
                .ToList();

            return menuModel;
        }
    }
}
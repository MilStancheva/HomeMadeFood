using Bytes2you.Validation;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Models;
using HomeMadeFood.Web.Common.Messaging;
using HomeMadeFood.Web.Controllers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeMadeFood.Web.Areas.Admin.Controllers
{
    public class FoodCategoriesController : Controller
    {
        private readonly IFoodCategoriesService foodCategoriesService;
        private readonly IMappingService mappingService;

        public FoodCategoriesController(IFoodCategoriesService foodCategoriesService, IMappingService mappingService)
        {
            Guard.WhenArgument(foodCategoriesService, "foodCategoriesService").IsNull().Throw();
            this.foodCategoriesService = foodCategoriesService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        public ActionResult Index(string name)
        {
            var foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Select(this.mappingService.Map<FoodCategoryViewModel>)
                .ToList();

            if (!string.IsNullOrEmpty(name))
            {
                foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(this.mappingService.Map<FoodCategoryViewModel>)
                .ToList();
            }

            var searchModel = new SearchFoodCategoryViewModel();
            if (foodCategories != null)
            {
                searchModel.FoodCategories = foodCategories;
                searchModel.PageSize = 5;
                searchModel.TotalRecords = foodCategories.Count();
            }

            return this.View(searchModel);
        }

        [HttpGet]
        public ActionResult AddFoodCategory()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFoodCategory(AddFoodCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage("Something went wrong...", $"Ooops! {model.Name} could not be added. Please check again the input data. Thanks!", ToastType.Error);
                return this.View(model);
            }

            var foodCategory = this.mappingService.Map<FoodCategory>(model);
            this.foodCategoriesService.AddFoodCategory(foodCategory);

            this.AddToastMessage("Yeah!", $"{model.Name} is successfully added", ToastType.Success);
            return this.RedirectToAction("Index", "FoodCategories");
        }

        [HttpGet]
        public ActionResult EditFoodCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var foodCategory = this.foodCategoriesService.GetFoodCategoryById(id);
            var model = this.mappingService.Map<FoodCategoryViewModel>(foodCategory);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFoodCategory(FoodCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage("Something went wrong...", $"Ooops! {model.Name} could not be updated. Please check again the input data. Thanks!", ToastType.Error);
                return this.View(model);
            }

            var foodCategory = this.mappingService.Map<FoodCategory>(model);
            this.foodCategoriesService.EditFoodCategory(foodCategory);

            this.AddToastMessage("Yeah!", $"{model.Name} is successfully updated", ToastType.Success);
            return this.RedirectToAction("Index", "FoodCategories");
        }

        [HttpGet]
        public ActionResult DeleteFoodCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var foodCategory = this.foodCategoriesService.GetFoodCategoryById(id);
            var model = this.mappingService.Map<FoodCategoryViewModel>(foodCategory);
            return this.View("DeleteFoodCategory", model);
        }

        [HttpPost, ActionName("DeleteFoodCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFoodCategoryConfirm(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var foodCategory = this.foodCategoriesService.GetFoodCategoryById(id);

            if (foodCategory == null)
            {
                this.AddToastMessage("Something went wrong...", $"Ooops! {foodCategory.Name} could not be deleted.", ToastType.Error);
                var model = this.mappingService.Map<FoodCategoryViewModel>(foodCategory);
                return this.View("DeleteFoodCategory", model.Id);
            }

            this.foodCategoriesService.DeleteFoodCategory(foodCategory);

            this.AddToastMessage("Yeah!", $"{foodCategory.Name} is successfully deleted", ToastType.Success);
            return this.RedirectToAction("Index", "FoodCategories");
        }
    }
}
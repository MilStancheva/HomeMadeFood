using System;
using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using HomeMadeFood.Models;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Models;
using HomeMadeFood.Web.Common.Messaging;
using HomeMadeFood.Web.Controllers.Extensions;

namespace HomeMadeFood.Web.Areas.Admin.Controllers
{
    public class FoodCategoriesController : Controller
    {
        private readonly int gridPageSize = 25;

        private string toastrSuccessTitle = "Yeah!";
        private string toastrAddObjectSuccessMessage = "{0} is successfully added";
        private string toastrUpdateObjectSuccessMessage = "{0} is successfully updated";
        private string toastrDeleteObjectSuccessMessage = "{0} is successfully deleted";

        private string toastrFailureTitle = "Something went wrong...";
        private string toastrAddObjectFailureMessage = "Ooops! {0} could not be added. Please check again the input data. Thanks!";
        private string toastrUpdateObjectFailureMessage = "Ooops! {0} could not be updated. Please check again the input data. Thanks!";
        private string toastrDeleteObjectFailureMessage = "Ooops! {0} could not be deleted.";

        private readonly IFoodCategoriesService foodCategoriesService;
        private readonly IMappingService mappingService;

        public FoodCategoriesController(IFoodCategoriesService foodCategoriesService, IMappingService mappingService)
        {
            Guard.WhenArgument(foodCategoriesService, "foodCategoriesService").IsNull().Throw();
            this.foodCategoriesService = foodCategoriesService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Select(this.mappingService.Map<FoodCategoryViewModel>)
                .ToList();

            var searchModel = new SearchFoodCategoryViewModel();
            if (foodCategories != null)
            {
                searchModel.FoodCategories = foodCategories;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = foodCategories.Count();
            }

            return this.View(searchModel);
        }

        public ActionResult Search(string name)
        {
            var foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(this.mappingService.Map<FoodCategoryViewModel>)
                .ToList();

            var searchModel = new SearchFoodCategoryViewModel();
            if (foodCategories != null)
            {
                searchModel.FoodCategories = foodCategories;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = foodCategories.Count();
            }

            return this.PartialView("_FoodCategoriesGridPartial", searchModel);
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
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrAddObjectFailureMessage, model.Name), ToastType.Error);
                return this.View(model);
            }

            var foodCategory = this.mappingService.Map<FoodCategory>(model);
            this.foodCategoriesService.AddFoodCategory(foodCategory);

            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrAddObjectSuccessMessage, model.Name), ToastType.Success);
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
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrUpdateObjectFailureMessage, model.Name), ToastType.Error);
                return this.View(model);
            }

            var foodCategory = this.mappingService.Map<FoodCategory>(model);
            this.foodCategoriesService.EditFoodCategory(foodCategory);

            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrUpdateObjectSuccessMessage, model.Name), ToastType.Success);
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
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrUpdateObjectFailureMessage, foodCategory.Name), ToastType.Error);
                var model = this.mappingService.Map<FoodCategoryViewModel>(foodCategory);
                return this.View("DeleteFoodCategory", model.Id);
            }

            this.foodCategoriesService.DeleteFoodCategory(foodCategory);

            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrDeleteObjectSuccessMessage, foodCategory.Name), ToastType.Success);
            return this.RedirectToAction("Index", "FoodCategories");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using HomeMadeFood.Models;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Models;
using HomeMadeFood.Web.Common.Messaging;
using HomeMadeFood.Web.Controllers.Extensions;
using HomeMadeFood.Web.CustomFilters;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RecipesController : Controller
    {
        private readonly int gridPageSize = 25;

        private string toastrSuccessTitle = GlobalResources.ТoastrSuccessTitle;
        private string toastrAddObjectSuccessMessage = GlobalResources.ToastrAddObjectSuccessMessage;
        private string toastrUpdateObjectSuccessMessage = GlobalResources.ToastrUpdateObjectSuccessMessage;
        private string toastrDeleteObjectSuccessMessage = GlobalResources.ToastrDeleteObjectSuccessMessage;

        private string toastrFailureTitle = GlobalResources.ToastrFailureTitle;
        private string toastrAddObjectFailureMessage = GlobalResources.ToastrAddObjectFailureMessage;
        private string toastrUpdateObjectFailureMessage = GlobalResources.ToastrUpdateObjectFailureMessage;
        private string toastrDeleteObjectFailureMessage = GlobalResources.ToastrDeleteObjectFailureMessage;

        private readonly IIngredientsService ingredientsService;
        private readonly IMappingService mappingService;
        private readonly IRecipesService recipesService;
        private readonly IFoodCategoriesService foodCategoriesService;

        public RecipesController(IRecipesService recipesService, IIngredientsService ingredientsService, IFoodCategoriesService foodCategoriesService, IMappingService mappingService)
        {
            Guard.WhenArgument(recipesService, "recipesService").IsNull().Throw();
            this.recipesService = recipesService;

            Guard.WhenArgument(ingredientsService, "ingredientsService").IsNull().Throw();
            this.ingredientsService = ingredientsService;

            Guard.WhenArgument(foodCategoriesService, "foodCategoriesService").IsNull().Throw();
            this.foodCategoriesService = foodCategoriesService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        [OutputCache(CacheProfile = "AdminIndex")]
        public ActionResult Index()
        {
            var recipes = this.recipesService.GetAllRecipes()
                .Select(this.mappingService.Map<RecipeViewModel>)
                .ToList();

            var searchModel = new SearchRecipeViewModel();
            if (recipes != null)
            {
                searchModel.Recipes = recipes;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = recipes.Count();
            }

            return this.View(searchModel);
        }

        public ActionResult Search(string title)
        {
            var recipes = this.recipesService.GetAllRecipes()
                .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                .Select(this.mappingService.Map<RecipeViewModel>)
                .ToList();

            var searchModel = new SearchRecipeViewModel();
            if (recipes != null)
            {
                searchModel.Recipes = recipes;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = recipes.Count();
            }

            return this.PartialView("_RecipesGridPartial", searchModel);
        }

        [HttpGet]
        public ActionResult AddRecipe()
        {            
            return this.View();
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server, VaryByParam = "none")]
        [HttpGet]
        public JsonResult GetFoodCategories()
        {
            var foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Select(this.mappingService.Map<FoodCategoryViewModel>);

            return Json(foodCategories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult AutoComplete(string prefix)
        {
            var ingredients = this.ingredientsService
                .GetAllIngredients()
                .Select(x => this.mappingService.Map<IngredientViewModel>(x));

            var ingredientName = ingredients.Where(x => x.Name.ToLower().Contains(prefix.ToLower()));

            return Json(ingredientName, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRecipe(
            [Bind(Exclude = "Ingredients")] AddRecipeViewModel recipeModel, 
            IEnumerable<string> ingredientNames, 
            IEnumerable<double> ingredientQuantities,
            IEnumerable<decimal> ingredientPrices,
            IEnumerable<Guid> foodCategories)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrAddObjectFailureMessage, recipeModel.Title), ToastType.Error);
                return this.View(recipeModel);
            }

            var recipe = this.mappingService.Map<Recipe>(recipeModel);
            this.recipesService.AddRecipe(recipe, ingredientNames, ingredientQuantities, ingredientPrices, foodCategories);            
            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrAddObjectSuccessMessage, recipeModel.Title), ToastType.Success);

            return this.RedirectToAction("Index", "Recipes");
        }

        [HttpGet]
        public ViewResult EditRecipe(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var recipe = this.recipesService.GetRecipeById(id);
            var model = this.mappingService.Map<RecipeViewModel>(recipe);
            
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRecipe([Bind(Exclude ="Ingredients")]RecipeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrUpdateObjectFailureMessage, model.Title), ToastType.Error);
                return this.View(model);
            }

            var recipe = this.mappingService.Map<Recipe>(model);
            this.recipesService.EditRecipe(recipe);

            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrUpdateObjectSuccessMessage, model.Title), ToastType.Success);
            return this.RedirectToAction("Index", "Recipes");
        }

        [HttpGet]
        public ViewResult DeleteRecipe(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var recipe = this.recipesService.GetRecipeById(id);
            var model = this.mappingService.Map<RecipeViewModel>(recipe);

            return this.View(model);
        }

        [HttpPost, ActionName("DeleteRecipe")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecipeConfirm(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var recipe = this.recipesService.GetRecipeById(id);

            if (recipe == null)
            {
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrDeleteObjectFailureMessage, recipe.Title), ToastType.Error);
                var model = this.mappingService.Map<RecipeViewModel>(recipe);
                return this.View("DeleteRecipe", model.Id);
            }

            this.recipesService.DeleteRecipe(recipe);

            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrDeleteObjectSuccessMessage, recipe.Title), ToastType.Success);
            return this.RedirectToAction("Index", "Recipes");
        }

        public ActionResult DetailsRecipe(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var recipe = this.recipesService.GetRecipeById(id);
            var model = this.mappingService.Map<RecipeViewModel>(recipe);

            return this.View(model);
        }
    }
}
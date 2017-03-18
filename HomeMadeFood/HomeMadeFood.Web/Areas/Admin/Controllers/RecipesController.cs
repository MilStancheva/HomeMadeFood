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
    [Authorize(Roles = "Admin")]
    public class RecipesController : Controller
    {
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

        public ActionResult Index(string title)
        {
            var recipes = this.recipesService.GetAllRecipes()
                .Select(this.mappingService.Map<RecipeViewModel>)
                .ToList();

            if (!string.IsNullOrEmpty(title))
            {
                recipes = this.recipesService.GetAllRecipes()
                .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                .Select(this.mappingService.Map<RecipeViewModel>)
                .ToList();
            }

            var searchModel = new SearchRecipeViewModel();
            if (recipes != null)
            {
                searchModel.Recipes = recipes;
                searchModel.PageSize = 5;
                searchModel.TotalRecords = recipes.Count();
            }

            return this.View(searchModel);
        }

        [HttpGet]
        public ActionResult AddRecipe()
        {            
            return this.View();
        }

        [HttpGet]
        public JsonResult GetFoodCategories()
        {
            var foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Select(this.mappingService.Map<FoodCategoryViewModel>);

            return Json(foodCategories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
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
            //if (!this.ModelState.IsValid)
            //{
            //    this.AddToastMessage("Something went wrong...", $"Ooops! {recipeModel.Title} could not be added. Please check again the input data. Thanks!", ToastType.Error);
            //    return this.View(recipeModel);
            //}

            var recipe = this.mappingService.Map<Recipe>(recipeModel);
            this.recipesService.AddRecipe(recipe, ingredientNames, ingredientQuantities, ingredientPrices, foodCategories);            
            this.AddToastMessage("Yeah!", $"{recipeModel.Title} is successfully added", ToastType.Success);

            return this.RedirectToAction("Index", "Recipes");
        }
    }
}
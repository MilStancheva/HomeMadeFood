using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System;

using Bytes2you.Validation;

using HomeMadeFood.Models;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Models;
using HomeMadeFood.Web.Common.Messaging;
using HomeMadeFood.Web.Controllers.Extensions;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IngredientsController : Controller
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
        private readonly IFoodCategoriesService foodCategoriesService;
        private readonly IRecipesService recipesService;
        private readonly IMappingService mappingService;

        public IngredientsController(IIngredientsService ingredientsService, IFoodCategoriesService foodCategoriesService, IRecipesService recipesService, IMappingService mappingService)
        {
            Guard.WhenArgument(ingredientsService, "ingredientsService").IsNull().Throw();
            this.ingredientsService = ingredientsService;

            Guard.WhenArgument(foodCategoriesService, "foodCategoriesService").IsNull().Throw();
            this.foodCategoriesService = foodCategoriesService;

            Guard.WhenArgument(recipesService, "recipesService").IsNull().Throw();
            this.recipesService = recipesService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        [OutputCache(CacheProfile = "AdminIndex")]
        public ActionResult Index()
        {
            var ingredients = this.ingredientsService.GetAllIngredientsIncludingRecipes()
                .Select(this.mappingService.Map<IngredientViewModel>)
                .ToList();

            var searchModel = new SearchIngredientViewModel();
            if (ingredients != null)
            {
                searchModel.Ingredients = ingredients;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = ingredients.Count();
            }

            return this.View(searchModel);
        }

        public ActionResult Search(string name)
        {
            var ingredients = this.ingredientsService.GetAllIngredientsIncludingRecipes()
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(this.mappingService.Map<IngredientViewModel>)
                .ToList();
            
            var searchModel = new SearchIngredientViewModel();
            if (ingredients != null)
            {
                searchModel.Ingredients = ingredients;
                searchModel.PageSize = gridPageSize;
                searchModel.TotalRecords = ingredients.Count();
            }

            return this.PartialView("_IngredientsGridPartial", searchModel);
        }

        [HttpGet]
        public ActionResult AddIngredient()
        {
            var foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Select(this.mappingService.Map<FoodCategoryViewModel>);
            var model = new AddIngredientViewModel();
            model.FoodCategories = foodCategories;

            var recipes = this.recipesService.GetAllRecipes()
                .Select(this.mappingService.Map<RecipeViewModel>);
            model.Recipes = recipes;
            
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient(AddIngredientViewModel ingredientModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrAddObjectFailureMessage, ingredientModel.Name), ToastType.Error);
                return this.View(ingredientModel);
            }

            var foodCategoryId = ingredientModel.SelectedFoodCategoryId;
            var recipeId = ingredientModel.SelectedRecipeId;

            this.ingredientsService.AddIngredient(ingredientModel.Name, foodCategoryId, ingredientModel.PricePerMeasuringUnit, ingredientModel.QuantityInMeasuringUnit, recipeId);

            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrAddObjectSuccessMessage, ingredientModel.Name), ToastType.Success);
            return this.RedirectToAction("Index", "Ingredients");
        }

        [HttpGet]
        public ActionResult EditIngredient(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var ingredient = this.ingredientsService.GetIngredientById(id);
            var ingredientModel = this.mappingService.Map<IngredientViewModel>(ingredient);
            var recipes = this.recipesService.GetAllRecipes()
                .Select(this.mappingService.Map<RecipeViewModel>);
            ingredientModel.Recipes = recipes;

            var foodCategories = this.foodCategoriesService.GetAllFoodCategories()
                .Select(this.mappingService.Map<FoodCategoryViewModel>);
            ingredientModel.FoodCategories = foodCategories;
             
            return this.View(ingredientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIngredient([Bind(Exclude = "FoodCategory,FoodCategories,Recipe,Recipes")]IngredientViewModel ingredientModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrUpdateObjectFailureMessage, ingredientModel.Name), ToastType.Error);
                return this.View(ingredientModel);
            }

            var ingredient = this.mappingService.Map<Ingredient>(ingredientModel);
            this.ingredientsService.EditIngredient(ingredient);

            this.AddToastMessage(toastrSuccessTitle,string.Format(toastrUpdateObjectSuccessMessage, ingredientModel.Name), ToastType.Success);
            return this.RedirectToAction("Index", "Ingredients");
        }

        [HttpGet]
        public ActionResult DeleteIngredient(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var ingredient = this.ingredientsService.GetIngredientById(id);
            var ingredientModel = this.mappingService.Map<IngredientViewModel>(ingredient);
            return this.View("DeleteIngredient", ingredientModel);
        }

        [HttpPost, ActionName("DeleteIngredient")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIngredientConfirm(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.View("404.html");
            }

            var ingredient = this.ingredientsService.GetIngredientById(id);

            if (ingredient == null)
            {
                this.AddToastMessage(toastrFailureTitle, string.Format(toastrDeleteObjectFailureMessage, ingredient.Name), ToastType.Error);
                var ingredientModel = this.mappingService.Map<IngredientViewModel>(ingredient);
                return this.View("DeleteIngredient", ingredientModel.Id);
            }

            this.ingredientsService.DeleteIngredient(ingredient);

            this.AddToastMessage(toastrSuccessTitle, string.Format(toastrDeleteObjectSuccessMessage, ingredient.Name), ToastType.Success);
            return this.RedirectToAction("Index", "Ingredients");
        }
    }
}
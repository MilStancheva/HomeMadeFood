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

        public RecipesController(IRecipesService recipesService, IIngredientsService ingredientsService, IMappingService mappingService)
        {
            Guard.WhenArgument(recipesService, "recipesService").IsNull().Throw();
            this.recipesService = recipesService;

            Guard.WhenArgument(ingredientsService, "ingredientsService").IsNull().Throw();
            this.ingredientsService = ingredientsService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddRecipe()
        {   
            return this.View();
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
        public ActionResult AddRecipe([Bind(Exclude = "Ingredients")] AddRecipeViewModel recipeModel, IEnumerable<string> ingredientNames, IEnumerable<decimal> ingredientQuantities)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    this.AddToastMessage("Something went wrong...", $"Ooops! {recipeModel.Title} could not be added. Please check again the input data. Thanks!", ToastType.Error);
            //    return this.View(recipeModel);
            //}

            var recipe = this.mappingService.Map<Recipe>(recipeModel);
            this.recipesService.AddRecipe(recipe, ingredientNames, ingredientQuantities);            
            this.AddToastMessage("Yeah!", $"{recipeModel.Title} is successfully added", ToastType.Success);

            return this.RedirectToAction("Index", "Recipes");
        }
    }
}
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

namespace HomeMadeFood.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService ingredientsService;
        private readonly IMappingService mappingService;

        public IngredientsController(IIngredientsService ingredientsService, IMappingService mappingService)
        {
            Guard.WhenArgument(ingredientsService, "ingredientsService").IsNull().Throw();
            this.ingredientsService = ingredientsService;

            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            this.mappingService = mappingService;
        }

        public ActionResult Index(string name)
        {
            var ingredients = this.ingredientsService.GetAllIngredients()
                .Select(this.mappingService.Map<IngredientViewModel>)
                .ToList();

            if (!string.IsNullOrEmpty(name))
            {
                ingredients = this.ingredientsService.GetAllIngredients()
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(this.mappingService.Map<IngredientViewModel>)
                .ToList();
            }

            var searchModel = new SearchIngredientViewModel();
            if (ingredients != null)
            {
                searchModel.Ingredients = ingredients;
                searchModel.PageSize = 5;
                searchModel.TotalRecords = ingredients.Count();
            }

            return this.View(searchModel);
        }

        [HttpGet]
        public ActionResult AddIngredient()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient([Bind(Exclude = "Id, Quantity")]IngredientViewModel ingredientModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage("Something went wrong...", $"Ooops! {ingredientModel.Name} could not be added. Please check again the input data. Thanks!", ToastType.Error);

                return this.View(ingredientModel);
            }

            this.ingredientsService.AddIngredient(ingredientModel.Name, ingredientModel.FoodType, ingredientModel.PricePerMeasuringUnit, ingredientModel.MeasuringUnit);

            this.AddToastMessage("Yeah!", $"{ingredientModel.Name} is successfully added", ToastType.Success);

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
            return this.View(ingredientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIngredient([Bind(Exclude ="Id")]IngredientViewModel ingredientModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage("Something went wrong...", $"Ooops! {ingredientModel.Name} could not be updated. Please check again the input data. Thanks!", ToastType.Error);
                return this.View(ingredientModel);
            }

            var ingredient = this.mappingService.Map<Ingredient>(ingredientModel);
            this.ingredientsService.EditIngredient(ingredient);

            this.AddToastMessage("Yeah!", $"{ingredientModel.Name} is successfully updated", ToastType.Success);
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
                this.AddToastMessage("Something went wrong...", $"Ooops! {ingredient.Name} could not be deleted.", ToastType.Error);
                var ingredientModel = this.mappingService.Map<IngredientViewModel>(ingredient);
                return this.View("DeleteIngredient", ingredientModel.Id);
            }

            this.ingredientsService.DeleteIngredient(ingredient);

            this.AddToastMessage("Yeah!", $"{ingredient.Name} is successfully deleted", ToastType.Success);
            return this.RedirectToAction("Index", "Ingredients");
        }
    }
}
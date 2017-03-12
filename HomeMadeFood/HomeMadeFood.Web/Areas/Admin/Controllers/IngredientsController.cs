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

        public ActionResult Index()
        {
            var ingredients = this.ingredientsService.GetAllIngredients();
            var ingredientsModel = new List<IngredientViewModel>();

            foreach (var ingredient in ingredients)
            {
                var ingredientModel = this.mappingService.Map<IngredientViewModel>(ingredient);
                ingredientsModel.Add(ingredientModel);
            }

            this.AddToastMessage("Hello", "You are in admin index", ToastType.Info);
            return this.View(ingredientsModel);
        }

        [HttpGet]
        public ActionResult AddIngredient()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddIngredient(IngredientViewModel ingredientModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddToastMessage("Something went wrong...", $"Ooops! {ingredientModel.Name} could not be added. Please check again the input data. Thanks!", ToastType.Error);

                return this.View(ingredientModel);
            }

            this.ingredientsService.AddIngredient(ingredientModel.Name, ingredientModel.FoodType, ingredientModel.PricePerMeasuringUnit, ingredientModel.MeasuringUnit, ingredientModel.Quantity);

            this.AddToastMessage("Yeah!", $"{ingredientModel.Name} is successfully added", ToastType.Success);

            return this.RedirectToAction("Index", "Ingredients");
        }
    }
}
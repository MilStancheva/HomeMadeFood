using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.RecipesControllerUnitTests
{
    [TestFixture]
    public class AddRecipe_Should
    {
        [Test]
        public void RenderTheRightView_AddRecipe()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.AddRecipe())
                .ShouldRenderView("AddRecipe");
        }

        [Test]
        public void RenderTheRightView_AddRecipe_WithTheCorrectModel_RecipeViewModel_WhenModelStateIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            Guid recipeId = Guid.NewGuid();
            Guid foodCategoryId = Guid.NewGuid();
            IEnumerable<AddIngredientViewModel> ingredients = new List<AddIngredientViewModel>()
                {
                    new AddIngredientViewModel()
                    {
                        Name = "Blueberries"
                    }
                };
            AddRecipeViewModel model = new AddRecipeViewModel()
            {
                Title = null,
                Describtion = "A long describtion",
                Ingredients = ingredients
            };
            IEnumerable<string> ingredientNames = new List<string>();
            IEnumerable<double> ingredientQuantities = new List<double>();
            IEnumerable<decimal> ingredientPrices = new List<decimal>();
            IEnumerable<Guid> foodCategories = new List<Guid>();

            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act & Assert
            controller.WithCallTo(x => x.AddRecipe(model, ingredientNames, ingredientQuantities, ingredientPrices, foodCategories))
                .ShouldRenderView("AddRecipe")
                .WithModel<AddRecipeViewModel>()
                .AndModelError("Instruction");
        }

        [Test]
        public void RedirectToActionIndex_WithTheCorrectModel__WhenModelStateIsValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            Guid recipeId = Guid.NewGuid();
            Guid foodCategoryId = Guid.NewGuid();
            IEnumerable<AddIngredientViewModel> ingredients = new List<AddIngredientViewModel>()
                {
                    new AddIngredientViewModel()
                    {
                        Name = "Blueberries"
                    }
                };
            AddRecipeViewModel model = new AddRecipeViewModel()
            {
                Title = null,
                Describtion = "A long describtion",
                Ingredients = ingredients
            };
            IEnumerable<string> ingredientNames = new List<string>();
            IEnumerable<double> ingredientQuantities = new List<double>();
            IEnumerable<decimal> ingredientPrices = new List<decimal>();
            IEnumerable<Guid> foodCategories = new List<Guid>();


            //Act & Assert
            controller.WithCallTo(x => x.AddRecipe(model, ingredientNames, ingredientQuantities, ingredientPrices, foodCategories))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.RecipesControllerUnitTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void RenderTheRightPartialView()
        {
            //Arrange
            IEnumerable<Ingredient> ingredients = new List<Ingredient>();
            var inredientsServiceMock = new Mock<IIngredientsService>();
            inredientsServiceMock.Setup(x => x.GetAllIngredients()).Returns(ingredients);
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, inredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            string title = "Backed Potatoes";

            //Act & Assert
            controller.WithCallTo(x => x.Search(title))
                .ShouldRenderPartialView("_RecipesGridPartial");
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchRecipeViewModelAndNoModelErrors()
        {
            //Arrange
            IEnumerable<Ingredient> ingredients = new List<Ingredient>();
            var inredientsServiceMock = new Mock<IIngredientsService>();
            inredientsServiceMock.Setup(x => x.GetAllIngredients()).Returns(ingredients);
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, inredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            string title = "Backed Potatoes";

            //Act & Assert
            controller.WithCallTo(x => x.Search(title))
                .ShouldRenderPartialView("_RecipesGridPartial")
                .WithModel<SearchRecipeViewModel>()
                .AndNoModelErrors();
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchRecipeViewModelAndNoModelErrorsAndCorrectContent()
        {
            //Arrange 
            var inredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, inredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            Guid ingredientId = Guid.NewGuid();
            Ingredient ingredient = new Ingredient() { Id = ingredientId, Name = "IngredientName", PricePerMeasuringUnit = 12.60m, QuantityInMeasuringUnit = 0 };
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                ingredient
            };
            inredientsServiceMock.Setup(x => x.GetAllIngredients()).Returns(ingredients);

            List<string> ingredientNames = new List<string>() { "Tomato" };
            List<double> ingredientQuantities = new List<double>() { 1.200 };
            List<decimal> ingredientPrices = new List<decimal>() { 4.80m };
            List<Guid> foodCategories = new List<Guid>() { Guid.NewGuid() };

            Guid recipeId = Guid.NewGuid();
            Guid foodCategoryId = Guid.NewGuid();
            Recipe recipe = new Recipe()
            {
                Id = recipeId,
                Title = "Title Of A New Recipe",
                Describtion = "This is a decsribtion",
                Instruction = "Instructions of the recipe",
                DishType = DishType.MainDish,
                Ingredients = ingredients
            };

            var recipes = new List<RecipeViewModel>()
                {
                    new RecipeViewModel()
                    {
                        Title = recipe.Title,
                        Describtion = recipe.Describtion,
                        Ingredients = new List<IngredientViewModel>()
                        {
                            new IngredientViewModel()
                            {
                                Name = ingredient.Name,
                                RecipeId = recipeId,
                                FoodCategoryId = foodCategoryId
                            }
                        },
                        DishType = recipe.DishType,
                        Instruction = recipe.Instruction
                    }
                };


            var searchModel = new SearchRecipeViewModel()
            {
                PageSize = 5,
                TotalRecords = recipes.Count,
                Recipes = recipes
            };

            string title = "Title Of A New Recipe";

            //Act & Assert
            controller.WithCallTo(x => x.Search(title))
                .ShouldRenderPartialView("_RecipesGridPartial")
                .WithModel<SearchRecipeViewModel>(
                    viewModel => Assert.AreEqual(recipes.ToList()[0].Title, searchModel.Recipes.ToList()[0].Title))
                .AndNoModelErrors();
        }
    }
}
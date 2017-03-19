using System;
using System.Collections.Generic;

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
    public class AutoComplete_Should
    {
        [Test]
        public void ReturnJsonResultWithIngredients()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            var name = "Tomatos";
            var foodType = FoodType.Vegetable;
            var measuringUnit = MeasuringUnitType.Kg;

            var foodcategory = new FoodCategory()
            {
                Id = id,
                Name = name,
                FoodType = foodType,
                MeasuringUnit = measuringUnit
            };
            Guid ingredientId = Guid.NewGuid();
            string ingredientName = "tomato";
            decimal pricePerMeasuringUnit = 1.20m;
            double quantityInMeasuringUnit = 0.250;

            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                FoodCategory = foodcategory,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                QuantityInMeasuringUnit = quantityInMeasuringUnit
            };

            IngredientViewModel ingredientModel = new IngredientViewModel()
            {
                Id = ingredientId,
                Name = ingredientName,
                FoodCategory = foodcategory,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                QuantityInMeasuringUnit = quantityInMeasuringUnit
            }; 

            var ingredients = new List<IngredientViewModel>() { ingredientModel };
            ingredientsServiceMock.Setup(x => x.GetAllIngredients()).Returns(new List<Ingredient> { ingredient });
            string query = "tomato";

            //Act & Assert
            controller.WithCallTo(x => x.AutoComplete(query))
                .ShouldReturnJson();
        }

        [Test]
        public void ReturnJsonResultWithIngredientsAndRightContent()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            var name = "Tomatos";
            var foodType = FoodType.Vegetable;
            var measuringUnit = MeasuringUnitType.Kg;

            var foodcategory = new FoodCategory()
            {
                Id = id,
                Name = name,
                FoodType = foodType,
                MeasuringUnit = measuringUnit
            };
            Guid ingredientId = Guid.NewGuid();
            string ingredientName = "tomato";
            decimal pricePerMeasuringUnit = 1.20m;
            double quantityInMeasuringUnit = 0.250;

            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                FoodCategory = foodcategory,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                QuantityInMeasuringUnit = quantityInMeasuringUnit
            };

            IngredientViewModel ingredientModel = new IngredientViewModel()
            {
                Id = ingredientId,
                Name = ingredientName,
                FoodCategory = foodcategory,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                QuantityInMeasuringUnit = quantityInMeasuringUnit
            };

            var ingredients = new List<IngredientViewModel>() { ingredientModel };
            ingredientsServiceMock.Setup(x => x.GetAllIngredients()).Returns(new List<Ingredient> { ingredient });
            string query = "tomato";

            //Act & Assert
            controller.WithCallTo(x => x.AutoComplete(query))
                .ShouldReturnJson(data =>
                {
                    Assert.That(ingredients[0].Name, Is.EqualTo(ingredientName));
                });
        }
    }
}

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

namespace HomeMadeFood.Controllers.UnitTests.RecipesControllerUnitTests
{
    [TestFixture]
    public class GetFoodCategories_Should
    {
        [Test]
        public void ReturnJsonResultWithFoodCategoies()
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

            var foodCategories = new List<FoodCategory>() { foodcategory };
            foodCategoriesServiceMock.Setup(x => x.GetAllFoodCategories()).Returns(foodCategories);

            //Act & Assert
            controller.WithCallTo(x => x.GetFoodCategories())
                .ShouldReturnJson();
        }

        [Test]
        public void ReturnJsonResultWithFoodCategoiesWithRightData()
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

            var foodCategories = new List<FoodCategory>() { foodcategory };
            foodCategoriesServiceMock.Setup(x => x.GetAllFoodCategories()).Returns(foodCategories);

            //Act & Assert
            controller.WithCallTo(x => x.GetFoodCategories())
                .ShouldReturnJson(data =>
                {
                    Assert.That(foodCategories[0].Name, Is.EqualTo(name));
                });
        }
    }
}
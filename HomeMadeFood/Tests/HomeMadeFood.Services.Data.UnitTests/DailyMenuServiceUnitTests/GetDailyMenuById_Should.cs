using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.DailyMenuServiceUnitTests
{
    [TestFixture]
    public class GetDailyMenuById_Should
    {
        [Test]
        public void ShouldThrow_WhenGuidIdParameterIsEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => dailyMenuService.GetDailyMenuById(Guid.Empty));
        }

        [Test]
        public void ReturnDaliMenu_WhenIdIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            Guid dailyMenuId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();

            var ingredients = new List<Ingredient>()
                {
                    new Ingredient()
                    {
                        Id = Guid.NewGuid(),
                        Name = "IngredientName",
                        FoodcategoryId = Guid.NewGuid(),
                        RecipeId = recipeId,
                        QuantityInMeasuringUnit = 0.200,
                        PricePerMeasuringUnit = 1.29m
                    }
                };

            var recipe = new Recipe()
            {
                Id = recipeId,
                Title = "Title Of A New Recipe",
                Describtion = "This is a decsribtion",
                Instruction = "Instructions of the recipe",
                DishType = DishType.MainDish,
                Ingredients = ingredients
            };

            DailyMenu dailyMenu = new DailyMenu()
            {
                Id = dailyMenuId,
                Date = DateTime.Today,
                DayOfWeek = DateTime.Today.DayOfWeek,
                Recipes = new List<Recipe>()
                {
                    recipe
                }

            };

            dataMock.Setup(c => c.DailyMenus.GetById(dailyMenuId)).Returns(dailyMenu);

            //Act
            DailyMenu dailyMenuResult = dailyMenuService.GetDailyMenuById(dailyMenuId);

            //Assert
            Assert.AreSame(dailyMenu, dailyMenuResult);
        }

        [Test]
        public void ReturnNull_WhenIdIsValidButThereIsNoSuchDailyMenuInTheDatabase()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            Guid dailyMenuId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();

            dataMock.Setup(c => c.DailyMenus.GetById(dailyMenuId)).Returns<DailyMenu>(null);

            //Act
            DailyMenu dailyMenuResult = dailyMenuService.GetDailyMenuById(dailyMenuId);

            //Assert
            Assert.IsNull(dailyMenuResult);
        }
    }
}
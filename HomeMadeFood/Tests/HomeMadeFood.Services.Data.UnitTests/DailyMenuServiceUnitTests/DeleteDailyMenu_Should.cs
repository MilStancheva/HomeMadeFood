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
    public class DeleteDailyMenu_Should
    {
        [Test]
        public void Throw_WhenThePassedDailyMenuIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            dataMock.Setup(x => x.DailyMenus.Delete(It.IsAny<DailyMenu>()));

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => dailyMenuService.DeleteDailyMenu(null));
        }

        [Test]
        public void InvokeDataDailyMenuDeleteOnce_WhenThePassedArgumentIsValid()
        {
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
                        FoodCategoryId = Guid.NewGuid(),
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

            dataMock.Setup(x => x.DailyMenus.Delete(It.IsAny<DailyMenu>()));

            //Act
            dailyMenuService.DeleteDailyMenu(dailyMenu);

            //Assert
            dataMock.Verify(x => x.DailyMenus.Delete(dailyMenu), Times.Once);
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        {
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
                        FoodCategoryId = Guid.NewGuid(),
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

            dataMock.Setup(x => x.DailyMenus.Delete(It.IsAny<DailyMenu>()));

            //Act
            dailyMenuService.DeleteDailyMenu(dailyMenu);

            //Assert
            dataMock.Verify(x => x.Commit(), Times.Once);
        }
    }
}
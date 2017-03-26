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
    public class AddDailyMenu_Should
    {
        [Test]
        public void Throw_WhenThePassedRecipesIdsIsNullOrEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            IEnumerable<Guid> recipesIds = new List<Guid>();
            DateTime date = new DateTime();

            //Act & Assert
            Assert.Throws<ArgumentException>(
                () => dailyMenuService.AddDailyMenu(
                    date,
                    recipesIds));
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            Guid recipeId = Guid.NewGuid();
            IEnumerable<Guid> recipesIds = new List<Guid>() { recipeId };
            DateTime date = new DateTime(2017, 5, 5);

            Guid dailyMenuId = Guid.NewGuid();

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

            var recipes = new List<Recipe>()
                {
                    recipe
                };

            DailyMenu dailyMenu = new DailyMenu()
            {
                Id = dailyMenuId,
                Date =date,
                DayOfWeek = date.DayOfWeek
            };

            recipesServiceMock.Setup(x => x.GetAllRecipes()).Returns(recipes);

            dataMock.Setup(x => x.DailyMenus.Add(dailyMenu));

            //Act
            dailyMenuService.AddDailyMenu(date, recipesIds);

            //Assert
            dataMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void InvokeDataDailyMenusAddOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            Guid recipeId = Guid.NewGuid();
            IEnumerable<Guid> recipesIds = new List<Guid>() { recipeId };
            DateTime date = DateTime.Today;
            Guid dailyMenuId = Guid.NewGuid();

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

            var recipes = new List<Recipe>()
                {
                    recipe
                };

            DailyMenu dailyMenu = new DailyMenu()
            {
                Id = dailyMenuId,
                Date = date,
                DayOfWeek = date.DayOfWeek,
                Recipes = recipes

            };
            
            dataMock.Setup(x => x.DailyMenus.Add(It.IsAny<DailyMenu>()));

            //Act
            dailyMenuService.AddDailyMenu(date, recipesIds);

            //Assert
            dataMock.Verify(x => x.DailyMenus.Add(It.IsAny<DailyMenu>()), Times.Once);
        }
    }
}
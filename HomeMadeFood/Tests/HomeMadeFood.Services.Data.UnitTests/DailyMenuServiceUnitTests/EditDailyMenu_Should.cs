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
    public class EditDailyMenu_Should
    {
        [Test]
        public void Throw_WhenThePassedCollectionOfRecipeIdsIsNullOrEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            IEnumerable<Guid> recipesIds = new List<Guid>();
            DateTime date = new DateTime();
            Guid id = Guid.NewGuid();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => dailyMenuService.EditDailyMenu(id, date, recipesIds));
        }

        [Test]
        public void Throw_WhenThePassedIdIsNullOrEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            Guid recipeId = Guid.NewGuid();
            IEnumerable<Guid> recipesIds = new List<Guid>() { recipeId };
            DateTime date = new DateTime();
            Guid id = Guid.Empty;         
            
            //Act & Assert
            Assert.Throws<ArgumentException>(() => dailyMenuService.EditDailyMenu(id, date, recipesIds));
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
                Date = date,
                DayOfWeek = date.DayOfWeek
            };
            dailyMenu.Recipes.Add(recipe);
            dataMock.Setup(x => x.DailyMenus.GetById(dailyMenuId)).Returns(dailyMenu);
            dataMock.Setup(x => x.DailyMenus.Update(It.IsAny<DailyMenu>()));


            //Act
            dailyMenuService.EditDailyMenu(dailyMenuId, date, recipesIds);

            //Assert
            dataMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void InvokeDataDailyMenusUpdateOnce_WhenThePassedArgumentsAreValid()
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
                Date = date,
                DayOfWeek = date.DayOfWeek
            };
            dailyMenu.Recipes.Add(recipe);
            dataMock.Setup(x => x.DailyMenus.GetById(dailyMenuId)).Returns(dailyMenu);
            dataMock.Setup(x => x.DailyMenus.Update(dailyMenu));


            //Act
            dailyMenuService.EditDailyMenu(dailyMenuId, date, recipesIds);

            //Assert
            dataMock.Verify(x => x.DailyMenus.Update(dailyMenu), Times.Once);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.DailyMenuServiceUnitTests
{
    [TestFixture]
    public class GetShoppingListOfFoodCategoriesForActiveDailyMenus_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenThePassedParameterCollectionOfDailyMenusIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            IEnumerable<DailyMenu> dailyMenus = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
            dailyMenuService.GetShoppingListOfFoodCategoriesForActiveDailyMenus(dailyMenus));
        }

        [Test]
        public void ReturnsAFoodCategriesCollection_WhenThePassedParameterIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            var ingredients = new List<Ingredient>();
            var ingredientId = Guid.NewGuid();
            var ingredientName = "Ingredient Name";
            var ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName
            };
            ingredients.Add(ingredient);
            var recipeId = Guid.NewGuid();
            var recipeTitle = "Title of the recipe";
            var recipe = new Recipe()
            {
                Id = recipeId,
                Title = recipeTitle,
                Ingredients = ingredients
            };
            var dailyMenus = new List<DailyMenu>();
            var dailyMenu = new DailyMenu()
            {
                Recipes = new List<Recipe>()
                {
                    recipe
                }
            };
            dailyMenus.Add(dailyMenu);
            
            dataMock.Setup(x => x.Ingredients.All).Returns(ingredients.AsQueryable());
            var foodcategory = It.IsAny<FoodCategory>();
            var foodCategories = new List<FoodCategory>()
            {
                foodcategory
            };

            var recipes = dailyMenus.SelectMany(x => x.Recipes).ToList();
            var expectedResult = new List<FoodCategory>();
            foreach (var dailyMenuRecipe in recipes)
            {
                foreach (var item in dailyMenuRecipe.Ingredients)
                {
                    dataMock.Setup(x => x.FoodCategories.GetById(item.Id)).Returns(foodcategory);
                    expectedResult.Add(foodcategory);
                }
            }
            dataMock.Setup(x => x.FoodCategories.All).Returns(foodCategories.AsQueryable());

            //Act 
            var actualResult = dailyMenuService.GetShoppingListOfFoodCategoriesForActiveDailyMenus(dailyMenus);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ReturnsNull_WhenThereAreNoFoodcategoriesToReturn()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            var ingredients = new List<Ingredient>();
            var ingredientId = Guid.NewGuid();
            var ingredientName = "Ingredient Name";
            var ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName
            };
            ingredients.Add(ingredient);
            var recipeId = Guid.NewGuid();
            var recipeTitle = "Title of the recipe";
            var recipe = new Recipe()
            {
                Id = recipeId,
                Title = recipeTitle,
                Ingredients = ingredients
            };
            var dailyMenus = new List<DailyMenu>();
            var dailyMenu = new DailyMenu()
            {
                Recipes = new List<Recipe>()
                {
                    recipe
                }
            };
            dailyMenus.Add(dailyMenu);

            dataMock.Setup(x => x.Ingredients.All).Returns(ingredients.AsQueryable());

            var recipes = dailyMenus.SelectMany(x => x.Recipes).ToList();
            IEnumerable<FoodCategory> expectedResult = new List<FoodCategory>() { null };
            foreach (var dailyMenuRecipe in recipes)
            {
                foreach (var item in dailyMenuRecipe.Ingredients)
                {
                    dataMock.Setup(x => x.FoodCategories.GetById(item.Id)).Returns<FoodCategory>(null);
                }
            }
            dataMock.Setup(x => x.FoodCategories.All).Returns(() => 
            {
                return expectedResult.AsQueryable();
            });

            //Act 
            var actualResult = dailyMenuService.GetShoppingListOfFoodCategoriesForActiveDailyMenus(dailyMenus);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
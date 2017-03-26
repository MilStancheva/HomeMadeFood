using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.DailyMenuServiceUnitTests
{
    [TestFixture]
    public class CalculateShoppingListCostForActiveDailyMenus_Should
    {
        [Test]
        public void Throw_WhenThePassedParameter_FoodCategoriesOfActiveDailyMenus_IsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            IEnumerable<FoodCategory> foodCategories = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            dailyMenuService.CalculateShoppingListCostForActiveDailyMenus(foodCategories));
        }

        [Test]
        public void ReturnTheCostOfTheFoodCategoriesPasses_WhenThePassedParameter_FoodCategoriesOfActiveDailyMenus_IsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            var foodCategories = new List<FoodCategory>();

            var firstFoodCategory = new FoodCategory()
            {
                Id = Guid.NewGuid(),
                Name = "First Food Category",
                CostOfAllCategoryIngredients = 9.99m,
                QuantityOfAllCategoryIngredients = 1.234
            };

            var secondFoodCategory = new FoodCategory()
            {
                Id = Guid.NewGuid(),
                Name = "Second Food Category",
                CostOfAllCategoryIngredients = 1m,
                QuantityOfAllCategoryIngredients = 1
            };

            foodCategories.Add(firstFoodCategory);
            foodCategories.Add(secondFoodCategory);

            var expectedResult = firstFoodCategory.CostOfAllCategoryIngredients + secondFoodCategory.CostOfAllCategoryIngredients;

            //Act
            var actualResult = dailyMenuService.CalculateShoppingListCostForActiveDailyMenus(foodCategories);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
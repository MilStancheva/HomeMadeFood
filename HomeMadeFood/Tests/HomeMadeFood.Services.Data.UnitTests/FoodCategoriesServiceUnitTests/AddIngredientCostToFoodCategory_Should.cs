using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.UnitTests.FoodCategoriesServiceUnitTests
{
    [TestFixture]
    public class AddIngredientCostToFoodCategory_Should
    {
        [Test]
        public void Throw_WhenThePassedParameter_Ingredient_IsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            Ingredient ingredient = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => foodCategoriesService.AddIngredientCostToFoodCategory(ingredient));
        }

        [Test]
        public void AddIngredientsPriceToFoodCategory()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            var ingredientId = Guid.NewGuid();
            var ingredientName = "Ingredient name";
            var ingredientPricePerMeasuringUnit = 1.5m;
            var foodCategoryId = Guid.NewGuid();
            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                FoodCategoryId = foodCategoryId,
                PricePerMeasuringUnit = ingredientPricePerMeasuringUnit
            };

            var foodCategoryName = "Food Category Name";
            var costOfAllCategoryIngredients = 0;
            FoodCategory foodCategory = new FoodCategory()
            {
                Id = foodCategoryId,
                Name = foodCategoryName,
                CostOfAllCategoryIngredients = costOfAllCategoryIngredients

            };

            dataMock.Setup(x => x.FoodCategories.GetById(foodCategoryId)).Returns(foodCategory);

            //Act
            foodCategoriesService.AddIngredientCostToFoodCategory(ingredient);

            //Assert
            Assert.AreEqual(ingredient.PricePerMeasuringUnit, foodCategory.CostOfAllCategoryIngredients);
        }

        [Test]
        public void CallDataCommitMethodOnce_WhenParameterIngredientIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            var ingredientId = Guid.NewGuid();
            var ingredientName = "Ingredient name";
            var ingredientPricePerMeasuringUnit = 1.5m;
            var foodCategoryId = Guid.NewGuid();
            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                FoodCategoryId = foodCategoryId,
                PricePerMeasuringUnit = ingredientPricePerMeasuringUnit
            };

            var foodCategoryName = "Food Category Name";
            var costOfAllCategoryIngredients = 0;
            FoodCategory foodCategory = new FoodCategory()
            {
                Id = foodCategoryId,
                Name = foodCategoryName,
                CostOfAllCategoryIngredients = costOfAllCategoryIngredients

            };

            dataMock.Setup(x => x.FoodCategories.GetById(foodCategoryId)).Returns(foodCategory);

            //Act
            foodCategoriesService.AddIngredientCostToFoodCategory(ingredient);

            //Assert
            dataMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
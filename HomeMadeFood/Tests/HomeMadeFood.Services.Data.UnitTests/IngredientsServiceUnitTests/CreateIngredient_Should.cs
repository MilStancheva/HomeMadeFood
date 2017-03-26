using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.IngredientsServiceUnitTests
{
    [TestFixture]
    public class CreateIngredient_Should
    {
        [Test]
        public void ReturnIngredient()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            string ingredientName = "Name of the ingredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            //Act
            var ingredient = ingredientsService.CreateIngredient(ingredientName, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit);

            //Assert
            Assert.IsInstanceOf<Ingredient>(ingredient);
        }

        [Test]
        public void CallFoodCategoriesServiceMethod_AddIngredientCostToFoodCategory()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            string ingredientName = "Name of the ingredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            //Act
            var ingredient = ingredientsService.CreateIngredient(ingredientName, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit);

            //Assert
            foodCategoriesServiceMock.Verify(x => x.AddIngredientCostToFoodCategory(ingredient), Times.Once);
        }

        [Test]
        public void CallFoodCategoriesServiceMethod_AddIngredientQuantityToFoodCategory()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            string ingredientName = "Name of the ingredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            //Act
            var ingredient = ingredientsService.CreateIngredient(ingredientName, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit);

            //Assert
            foodCategoriesServiceMock.Verify(x => x.AddIngredientQuantityToFoodCategory(ingredient), Times.Once);
        }
    }
}
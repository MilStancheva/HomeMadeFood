using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.IngredientsServiceUnitTests
{
    [TestFixture]
    public class GetIngredientById_Should
    {
        [Test]
        public void ShouldThrow_WhenGuidIdParameterIsEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => ingredientsService.GetIngredientById(Guid.Empty));
        }

        [Test]
        public void ReturnIngredient_WhenIdIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            Guid ingredientId = Guid.NewGuid();
            Ingredient ingredient = new Ingredient() { Id = ingredientId, Name = "IngredientName", PricePerMeasuringUnit = 12.60m, QuantityInMeasuringUnit = 0 };

            dataMock.Setup(c => c.Ingredients.GetById(ingredientId)).Returns(ingredient);

            //Act
            Ingredient ingredientResult = ingredientsService.GetIngredientById(ingredientId);

            //Assert
            Assert.AreSame(ingredient, ingredientResult);
        }
        [Test]
        public void ReturnsNull_WhenIdIsValidButThereIsNoSuchIngredientInDatabase()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            Guid ingredientId = Guid.NewGuid();

            dataMock.Setup(c => c.Ingredients.GetById(ingredientId)).Returns<Ingredient>(null);

            //Act
            Ingredient ingredientResult = ingredientsService.GetIngredientById(ingredientId);

            //Assert
            Assert.IsNull(ingredientResult);
        }
    }
}
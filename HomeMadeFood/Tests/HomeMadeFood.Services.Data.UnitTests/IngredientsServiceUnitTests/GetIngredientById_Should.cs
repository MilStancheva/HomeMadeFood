using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

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
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => ingredientsService.GetIngredientById(Guid.Empty));
        }

        [Test]
        public void ReturnIngredient_WhenIdIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            Guid ingredientId = Guid.NewGuid();
            Ingredient ingredient = new Ingredient() { Id = ingredientId, Name = "IngredientName", FoodType = FoodType.Cheese, MeasuringUnit = MeasuringUnitType.PerUnit, PricePerMeasuringUnit = 12.60m, Quantity = 0 };

            dataMock.Setup(c => c.Ingredients.GetById(ingredientId)).Returns(ingredient);

            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);

            //Act
            Ingredient ingredientResult = ingredientsService.GetIngredientById(ingredientId);

            //Assert
            Assert.AreSame(ingredient, ingredientResult);
        }
    }
}
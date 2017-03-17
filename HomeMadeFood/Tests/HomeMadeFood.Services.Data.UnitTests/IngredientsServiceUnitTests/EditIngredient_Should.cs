using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.UnitTests.IngredientsServiceUnitTests
{
    [TestFixture]
    public class EditIngredient_Should
    {
        [Test]
        public void Throw_WhenThePassedIngredientIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);
            dataMock.Setup(x => x.Ingredients.Update(It.IsAny<Ingredient>()));

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => ingredientsService.EditIngredient(null));
        }

        [Test]
        public void InvokeDataIngredientsRepositoryMethodUpdateOnce_WhenThePassedIngredientIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            dataMock.Setup(x => x.Ingredients.Update(It.IsAny<Ingredient>()));
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);
            Guid ingredientId = Guid.NewGuid();
            Ingredient ingredient = new Ingredient() { Id = ingredientId, Name = "IngredientName", PricePerMeasuringUnit = 12.60m, QuantityInMeasuringUnit = 0 };

            //Act
            ingredientsService.EditIngredient(ingredient);

            //Assert
            dataMock.Verify(x => x.Ingredients.Update(ingredient), Times.Once);
        }
    }
}
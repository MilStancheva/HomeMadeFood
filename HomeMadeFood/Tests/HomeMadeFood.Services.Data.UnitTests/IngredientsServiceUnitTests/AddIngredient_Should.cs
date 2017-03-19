using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.UnitTests.IngredientsServiceUnitTests
{
    [TestFixture]
    public class AddIngredient_Should
    {
        [Test]
        public void Throw_WhenThePassedNameIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);
            string name = null;
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => ingredientsService.AddIngredient(name, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit, recipeId));
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);
            string name = "NameOfTheIngredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            dataMock.Setup(x => x.Ingredients.Add(It.IsAny<Ingredient>()));
            //Act
            ingredientsService.AddIngredient(name, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit, recipeId);

            //Assert
            dataMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void InvokeDataIngredientsAddOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);
            string name = "NameOfTheIngredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            dataMock.Setup(x => x.Ingredients.Add(It.IsAny<Ingredient>()));

            //Act
            ingredientsService.AddIngredient(name, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit, recipeId);

            //Assert
            dataMock.Verify(x => x.Ingredients.Add(It.IsAny<Ingredient>()), Times.Once);
        }
    }
}
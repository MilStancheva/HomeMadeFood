using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

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
            FoodType foodType = FoodType.Fruit;
            decimal pricePerMeasuringUnit = 1.19m;
            MeasuringUnitType measuringUnit = MeasuringUnitType.Kg;
            decimal quantity = 1m;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => ingredientsService.AddIngredient(name, foodType, pricePerMeasuringUnit, measuringUnit, quantity));
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);
            string name = "NameOfTheIngredient";
            FoodType foodType = FoodType.Fruit;
            decimal pricePerMeasuringUnit = 1.19m;
            MeasuringUnitType measuringUnit = MeasuringUnitType.Kg;
            decimal quantity = 1m;

            dataMock.Setup(x => x.Ingredients.Add(It.IsAny<Ingredient>()));
            //Act
            ingredientsService.AddIngredient(name, foodType, pricePerMeasuringUnit, measuringUnit, quantity);

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
            FoodType foodType = FoodType.Fruit;
            decimal pricePerMeasuringUnit = 1.19m;
            MeasuringUnitType measuringUnit = MeasuringUnitType.Kg;
            decimal quantity = 1m;

            dataMock.Setup(x => x.Ingredients.Add(It.IsAny<Ingredient>()));

            //Act
            ingredientsService.AddIngredient(name, foodType, pricePerMeasuringUnit, measuringUnit, quantity);

            //Assert
            dataMock.Verify(x => x.Ingredients.Add(It.IsAny<Ingredient>()), Times.Once);
        }
    }
}
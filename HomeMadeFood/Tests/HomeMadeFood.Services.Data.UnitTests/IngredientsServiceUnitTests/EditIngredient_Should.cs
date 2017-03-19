using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;

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
            Ingredient ingredient = new Ingredient()
            {
                Name = name,
                RecipeId = recipeId,
                FoodcategoryId = foodCategoryId,
                QuantityInMeasuringUnit = quantityPerMeasuringUnit,
                PricePerMeasuringUnit = pricePerMeasuringUnit
            };            

            dataMock.Setup(x => x.Ingredients.Update(ingredient));
            //Act
            ingredientsService.EditIngredient(ingredient);

            //Assert
            dataMock.Verify(x => x.Commit(), Times.Once);
        }
    }
}
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.UnitTests.IngredientsServiceUnitTests
{
    [TestFixture]
    public class GetAllIngredients_Should
    {
        [Test]
        public void Invoke_TheDataIngredientsRepositoryMethodGetAll_Once()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);
            dataMock.Setup(x => x.Ingredients.GetAll());
            //Act
            IEnumerable<Ingredient> ingredients = ingredientsService.GetAllIngredients();

            //Assert
            dataMock.Verify(x => x.Ingredients.GetAll(), Times.Once);
        }


        [Test]
        public void ReturnResult_WhenInvokingDataIngredientsRepositoryMethod_GetAll()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();            
            IEnumerable<Ingredient> expectedResultCollection = new List<Ingredient>();

            dataMock.Setup(c => c.Ingredients.GetAll()).Returns(() =>
            {
                return expectedResultCollection.AsQueryable();
            });

            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);

            //Act
            IEnumerable<Ingredient> ingredientsResult = ingredientsService.GetAllIngredients();

            //Assert
            Assert.That(ingredientsResult, Is.EqualTo(expectedResultCollection));
        }

        [Test]
        public void ReturnResultOfCorrectTypeIEnumerableOfIngredient()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.Ingredients.GetAll()).Returns(() =>
            {
                IEnumerable<Ingredient> expectedResultCollection = new List<Ingredient>();
                return expectedResultCollection.AsQueryable();
            });

            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);

            //Act
            IEnumerable<Ingredient> ingredientsResult = ingredientsService.GetAllIngredients();

            //Assert
            Assert.That(ingredientsResult, Is.InstanceOf<IEnumerable<Ingredient>>());
        }

        [Test]
        public void ReturnNull_WhenDataIngredientsReposityMethodGetAll_ReturnsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.Ingredients.GetAll()).Returns(() =>
            {
                return null;
            });

            IngredientsService ingredientsService = new IngredientsService(dataMock.Object);

            //Act
            IEnumerable<Ingredient> ingredientsResult = ingredientsService.GetAllIngredients();

            //Assert
            Assert.IsNull(ingredientsResult);
        }
    }
}
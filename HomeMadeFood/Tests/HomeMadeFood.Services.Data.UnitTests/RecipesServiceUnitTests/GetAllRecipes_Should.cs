using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.RecipesServiceUnitTests
{
    [TestFixture]
    public class GetAllRecipes_Should
    {
        [Test]
        public void Invoke_TheDataRecipesRepositoryMethodGetAll_Once()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
            dataMock.Setup(x => x.Recipes.All);

            //Act
            IEnumerable<Recipe> recipes = recipesService.GetAllRecipes();

            //Assert
            dataMock.Verify(x => x.Recipes.All, Times.Once);
        }

        [Test]
        public void ReturnResult_WhenInvokingDataRecipesRepositoryMethod_GetAll()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
            IEnumerable<Recipe> expectedResultCollection = new List<Recipe>();

            dataMock.Setup(c => c.Recipes.All).Returns(() =>
            {
                return expectedResultCollection.AsQueryable();
            });
            
            //Act
            IEnumerable<Recipe> recipesResult = recipesService.GetAllRecipes();

            //Assert
            Assert.That(recipesResult, Is.EqualTo(expectedResultCollection));
        }

        [Test]
        public void ReturnResultOfCorrectTypeIEnumerableOfRecipe()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
            dataMock.Setup(c => c.Recipes.All).Returns(() =>
            {
                IEnumerable<Recipe> expectedResultCollection = new List<Recipe>();
                return expectedResultCollection.AsQueryable();
            });
            
            //Act
            IEnumerable<Recipe> recipesResult = recipesService.GetAllRecipes();

            //Assert
            Assert.That(recipesResult, Is.InstanceOf<IEnumerable<Recipe>>());
        }

        [Test]
        public void ReturnNull_WhenDataRecipesReposityMethodGetAll_ReturnsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
            dataMock.Setup(c => c.Recipes.All).Returns(() =>
            {
                return null;
            });
            
            //Act
            IEnumerable<Recipe> recipesResult = recipesService.GetAllRecipes();

            //Assert
            Assert.IsNull(recipesResult);
        }
    }
}
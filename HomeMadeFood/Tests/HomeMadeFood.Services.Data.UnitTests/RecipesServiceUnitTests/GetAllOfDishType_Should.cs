using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.RecipesServiceUnitTests
{
    [TestFixture]
    public class GetAllOfDishType_Should
    {
        [Test]
        public void ReturnRecipesOfPassedDishTypeAsParameter()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
            var dishType = DishType.Pasta;
            Recipe recipe = new Recipe()
            {
                Id = Guid.NewGuid(),
                Title = "Recipe's title",
                DishType = DishType.Pasta
            };

            var expectedResultCollection = new List<Recipe>()
            {
                recipe
            };

            dataMock.Setup(x => x.Recipes.All).Returns(expectedResultCollection.AsQueryable());
            //Act
            var recipesOfDishTypePasta = recipesService.GetAllOfDishType(dishType).ToList();
            var actualResult = recipesOfDishTypePasta[0].DishType;

            //Assert
            Assert.AreEqual(dishType, actualResult);
        }
    }
}

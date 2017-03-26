using System;
using Moq;
using NUnit.Framework;
using HomeMadeFood.Data.Data;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.DailyMenuServiceUnitTests
{
    [TestFixture]
    public class DailyMenuServiceConstructor_Should
    {
        [Test]
        public void CreateANewInstanceOfDailymenuService_WhenPassedParameters_AreValid()
        {
            //Arrange
            var recipesServiceMock = new Mock<IRecipesService>();
            var dataMock = new Mock<IHomeMadeFoodData>();

            //Act
            var dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            //Assert
            Assert.IsNotNull(dailyMenuService);
            Assert.IsInstanceOf<DailyMenuService>(dailyMenuService);
        }

        [Test]
        public void ThrowArgumentExceprion_WhenPassedParameter_IHomeMadeFoodData_IsNull()
        {
            //Arrange
            var recipesServiceMock = new Mock<IRecipesService>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DailyMenuService(null, recipesServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentExceprion_WhenPassedParameter_IRecipesService_IsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DailyMenuService(dataMock.Object, null));
        }
    }
}
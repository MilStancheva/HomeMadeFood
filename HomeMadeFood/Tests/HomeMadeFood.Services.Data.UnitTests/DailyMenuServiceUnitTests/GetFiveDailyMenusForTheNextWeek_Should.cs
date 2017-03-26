using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.DailyMenuServiceUnitTests
{
    [TestFixture]
    public class GetFiveDailyMenusForTheNextWeek_Should
    {
        [Test]
        public void ReturnNull_IfThereAreNoDailyMenus()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            var expectedResult = new List<DailyMenu>() { null };
            dataMock.Setup(x => x.DailyMenus.GetAllIncluding(d => d.Recipes)).Returns(expectedResult);

            //Act
            var actualResult = dailyMenuService.GetFiveDailyMenusForTheNextWeek();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ReturnsIEnumerableOfDailyMenus_WithCountOfFive()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            var expectedResult = new List<DailyMenu>();
            var count = 5;
            for (int i = 0; i < count; i++)
            {
                expectedResult.Add(It.IsAny<DailyMenu>());
            }

            dataMock.Setup(x => x.DailyMenus.GetAllIncluding(d => d.Recipes)).Returns(expectedResult);

            //Act
            var actualResult = dailyMenuService.GetFiveDailyMenusForTheNextWeek();

            //Assert
            Assert.AreEqual(expectedResult.Count, actualResult.Count());
        }        
    }
}
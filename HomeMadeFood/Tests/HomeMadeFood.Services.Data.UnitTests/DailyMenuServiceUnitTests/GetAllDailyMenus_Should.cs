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
    public class GetAllDailyMenus_Should
    {
        [Test]
        public void Invoke_TheDataDailyMenusRepositoryMethodGetAll_Once()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);
            dataMock.Setup(x => x.DailyMenus.GetAll());

            //Act
            IEnumerable<DailyMenu> dailyMenus = dailyMenuService.GetAllDailyMenus();

            //Assert
            dataMock.Verify(x => x.DailyMenus.GetAll(), Times.Once);
        }

        [Test]
        public void ReturnResult_WhenInvokingDataDailyMenusRepositoryMethod_GetAll()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IEnumerable<DailyMenu> expectedResultCollection = new List<DailyMenu>();
            var recipesServiceMock = new Mock<IRecipesService>();
            dataMock.Setup(c => c.DailyMenus.GetAll()).Returns(() =>
            {
                return expectedResultCollection.AsQueryable();
            });

            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            //Act
            IEnumerable<DailyMenu> dailyMenuResult = dailyMenuService.GetAllDailyMenus();

            //Assert
            Assert.That(dailyMenuResult, Is.EqualTo(expectedResultCollection));
        }

        [Test]
        public void ReturnResultOfCorrectTypeIEnumerableOfDailyMenu()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();
            dataMock.Setup(c => c.DailyMenus.GetAll()).Returns(() =>
            {
                IEnumerable<DailyMenu> expectedResultCollection = new List<DailyMenu>();
                return expectedResultCollection.AsQueryable();
            });

            DailyMenuService dailyMenuService = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            //Act
            IEnumerable<DailyMenu> dailyMenuResult = dailyMenuService.GetAllDailyMenus();

            //Assert
            Assert.That(dailyMenuResult, Is.InstanceOf<IEnumerable<DailyMenu>>());
        }

        [Test]
        public void ReturnNull_WhenDataDailyMenusReposityMethodGetAll_ReturnsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var recipesServiceMock = new Mock<IRecipesService>();

            dataMock.Setup(c => c.DailyMenus.GetAll()).Returns(() =>
            {
                return null;
            });

            DailyMenuService dailyMenuServce = new DailyMenuService(dataMock.Object, recipesServiceMock.Object);

            //Act
            IEnumerable<DailyMenu> dailyMenusResult = dailyMenuServce.GetAllDailyMenus();

            //Assert
            Assert.IsNull(dailyMenusResult);
        }
    }
}

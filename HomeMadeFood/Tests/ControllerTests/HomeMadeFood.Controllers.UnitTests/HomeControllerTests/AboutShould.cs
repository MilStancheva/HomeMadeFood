using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace HomeMadeFood.Controllers.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class AboutShould
    {
        [Test]
        public void AddTheRightMessageInTheViewBag()
        {
            //Arrange
            var dailyMenuServiceMock = new Mock<IDailyMenuService>();
            var mappringServiceMock = new Mock<IMappingService>();
            HomeController homeController = new HomeController(dailyMenuServiceMock.Object, mappringServiceMock.Object);

            //Act
            ViewResult result = homeController.About() as ViewResult;

            //Assert
            Assert.AreEqual("About HomeMadeFood", result.ViewBag.Message);
        }
    }
}

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Web.Controllers;

namespace HomeMadeFood.Controllers.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class BackgroundVideo_Should
    {
        [Test]
        public void RenderPartialView_BackgroundVideoPartial()
        {
            //Arrange
            var dailyMenuServiceMock = new Mock<IDailyMenuService>();
            var mappringServiceMock = new Mock<IMappingService>();
            HomeController homeController = new HomeController(dailyMenuServiceMock.Object, mappringServiceMock.Object);

            //Act & Assert
            homeController
                .WithCallTo(x => x.BackgroundVideo())
                .ShouldRenderPartialView("_BackgroundVideoPartial");
        }
    }
}
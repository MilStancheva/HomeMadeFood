using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Controllers;
using HomeMadeFood.Web.Models.WeeklyMenu;

namespace HomeMadeFood.Controllers.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class WeeklyMenu_Should
    {
        [Test]
        public void RenderPartialView_WeeklymenuPartial()
        {
            //Arrange
            var dailyMenuServiceMock = new Mock<IDailyMenuService>();
            var mappringServiceMock = new Mock<IMappingService>();
            HomeController homeController = new HomeController(dailyMenuServiceMock.Object, mappringServiceMock.Object);

            //Act & Assert
            homeController.WithCallTo(x => x.WeeklyMenu())
                .ShouldRenderPartialView("_WeeklyMenuPartial");
        }

        [Test]
        public void RenderPartialView_WeeklymenuPartial_WithModel_WeeklyMenuViewModel()
        {
            //Arrange
            var dailyMenuServiceMock = new Mock<IDailyMenuService>();
            var mappringServiceMock = new Mock<IMappingService>();
            HomeController homeController = new HomeController(dailyMenuServiceMock.Object, mappringServiceMock.Object);

            //Act & Assert
            homeController.WithCallTo(x => x.WeeklyMenu())
                .ShouldRenderPartialView("_WeeklyMenuPartial")
                .WithModel<WeeklyMenuViewModel>()
                .AndNoModelErrors();
        }
    }
}

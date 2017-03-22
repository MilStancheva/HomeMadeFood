using System;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;

namespace HomeMadeFood.Controllers.UnitTests.DailyMenusControllerUnitTests
{
    [TestFixture]
    public class DeleteDailyMenu_Should
    {
        [Test]
        public void RenderTheRightView_DeleteDailyMeni()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();

            //Act & Assert
            controller.WithCallTo(x => x.DeleteDailyMenu(id))
                .ShouldRenderView("DeleteDailyMenu");
        }

        [Test]
        public void DeleteDailyMenuConfirm_Should_RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.Empty;

            //Act & Assert
            controller.WithCallTo(x => x.DeleteDailyMenuConfirm(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void DeleteDailyMenu_Should_RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.Empty;

            //Act & Assert
            controller.WithCallTo(x => x.DeleteDailyMenu(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_DeleteDailyMenu__WhenDailyMenuWasNotFoundInDatabase()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            dailyMenusServiceMock.Setup(x => x.GetDailyMenuById(id)).Returns<DailyMenu>(null);

            //Act & Assert
            controller.WithCallTo(x => x.DeleteDailyMenu(id))
                .ShouldRenderView("DeleteDailyMenu");
        }

        [Test]
        public void RedirectToActionIndex__WhenDailyMenuIsSuccessfullyDeleted()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            DateTime date = DateTime.Today;
            DayOfWeek dayOfWeek = date.DayOfWeek;

            DailyMenu dailyMenu = new DailyMenu()
            {
                Id = id,
                Date = date,
                DayOfWeek = dayOfWeek
            };

            dailyMenusServiceMock.Setup(x => x.GetDailyMenuById(id)).Returns(dailyMenu);
            dailyMenusServiceMock.Setup(x => x.DeleteDailyMenu(dailyMenu));

            //Act & Assert
            controller.WithCallTo(x => x.DeleteDailyMenuConfirm(id))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
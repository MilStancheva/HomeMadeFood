using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.DailyMenusControllerUnitTests
{
    [TestFixture]
    public class DetailsDailyMenu_Should
    {
        [Test]
        public void RenderTheRightView_DetailsDailyMenu()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.Today;
            DayOfWeek dayOfWeek = date.DayOfWeek;

            DailyMenu dailyMenu = new DailyMenu()
            {
                Id = id,
                Date = date,
                DayOfWeek = dayOfWeek
            };

            DailyMenuViewModel dailyMenuViewModel = new DailyMenuViewModel()
            {
                Id = dailyMenu.Id,
                Date = dailyMenu.Date,
                DayOfWeek = dailyMenu.DayOfWeek
            };
            var selectedMenu = dailyMenuViewModel;
            var allMenus = new AddDailyMenuViewModel();
            var dailyMenus = new List<DailyMenu>();
            dailyMenusServiceMock.Setup(x => x.GetDailyMenuById(id)).Returns(It.IsAny<DailyMenu>());
            dailyMenusServiceMock.Setup(x => x.GetAllDailyMenus()).Returns(dailyMenus);
            mappingServiceMock.Setup(x => x.Map<DailyMenuViewModel>(dailyMenu)).Returns(dailyMenuViewModel);

            dailyMenusServiceMock.Setup(x => x.GetDailyMenuById(id)).Returns(dailyMenu);

            //Act & Assert
            controller.WithCallTo(x => x.DetailsDailyMenu(id))
                .ShouldRenderView("DetailsDailyMenu");
        }

        [Test]
        public void RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.Empty;

            //Act & Assert
            controller.WithCallTo(x => x.DetailsDailyMenu(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_DetailsDailyMenu_WithModel_DailyMenuViewModel()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.Today;
            DayOfWeek dayOfWeek = date.DayOfWeek;

            DailyMenu dailyMenu = new DailyMenu()
            {
                Id = id,
                Date = date,
                DayOfWeek = dayOfWeek
            };

            DailyMenuViewModel dailyMenuViewModel = new DailyMenuViewModel()
            {
                Id = dailyMenu.Id,
                Date = dailyMenu.Date,
                DayOfWeek = dailyMenu.DayOfWeek
            };
            var selectedMenu = dailyMenuViewModel;
            var allMenus = new AddDailyMenuViewModel();
            var dailyMenus = new List<DailyMenu>();
            dailyMenusServiceMock.Setup(x => x.GetDailyMenuById(id)).Returns(It.IsAny<DailyMenu>());
            dailyMenusServiceMock.Setup(x => x.GetAllDailyMenus()).Returns(dailyMenus);
            mappingServiceMock.Setup(x => x.Map<DailyMenuViewModel>(dailyMenu)).Returns(dailyMenuViewModel);

            dailyMenusServiceMock.Setup(x => x.GetDailyMenuById(id)).Returns(dailyMenu);

            //Act & Assert
            controller.WithCallTo(x => x.DetailsDailyMenu(id))
                .ShouldRenderView("DetailsDailyMenu")
                .WithModel(dailyMenuViewModel);
        }
    }
}
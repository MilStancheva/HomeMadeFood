using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models.ShoppingListViewModels;

namespace HomeMadeFood.Controllers.UnitTests.AdminControllerUnitTests
{
    [TestFixture]
    public class ShoppingList_Should
    {
        [Test]
        public void ReturnRenderPartialView_ShoppingListForActiveDailyMenusPartial()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var mappingServiceMock = new Mock<IMappingService>();
            AdminController adminController = new AdminController(dailyMenusServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert

            adminController.WithCallTo(x => x.ShoppingList())
                .ShouldRenderPartialView("_ShoppingListForActiveDailyMenusPartial");
        }

        [Test]
        public void ReturnRenderPartialView_ShoppingListForActiveDailyMenusPartial_WithModel_ShoppingListViewModel()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var mappingServiceMock = new Mock<IMappingService>();
            AdminController adminController = new AdminController(dailyMenusServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert

            adminController.WithCallTo(x => x.ShoppingList())
                .ShouldRenderPartialView("_ShoppingListForActiveDailyMenusPartial")
                .WithModel<ShoppingListViewModel>()
                .AndNoModelErrors();
        }
    }
}

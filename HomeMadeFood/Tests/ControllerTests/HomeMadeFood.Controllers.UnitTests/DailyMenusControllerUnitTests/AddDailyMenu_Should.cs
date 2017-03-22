using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;

namespace HomeMadeFood.Controllers.UnitTests.DailyMenusControllerUnitTests
{
    [TestFixture]
    public class AddDailyMenu_Should
    {
        [Test]
        public void RenderTheRightView_AddDailyMenu()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.AddDailyMenu())
                .ShouldRenderView("AddDailyMenu");
        }

        [Test]
        public void RenderTheRightView_AddDailyMenu_WhenModelStateIsNotValid()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);

            var date = DateTime.Today;
            var recipeId = Guid.Empty;
            var recipesIds = new List<Guid>() { recipeId };
            var model = new { date, recipesIds };

            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act & Assert
            controller.WithCallTo(x => x.AddDailyMenu())
                .ShouldRenderView("AddDailyMenu");
        }

        [Test]
        public void RedirectToActionIndex_WithTheCorrectModel__WhenModelStateIsValid()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);

            var date = DateTime.Today;
            var recipeId = Guid.NewGuid();
            var recipesIds = new List<Guid>() { recipeId };
            dailyMenusServiceMock.Setup(x => x.AddDailyMenu(date, recipesIds));

            //Act & Assert
            controller.WithCallTo(x => x.AddDailyMenu(date, recipesIds))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
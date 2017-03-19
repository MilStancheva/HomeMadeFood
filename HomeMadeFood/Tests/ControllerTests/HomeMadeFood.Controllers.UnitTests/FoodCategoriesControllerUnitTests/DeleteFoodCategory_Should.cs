using System;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;

namespace HomeMadeFood.Controllers.UnitTests.FoodCategoriesControllerUnitTests
{
    [TestFixture]
    public class DeleteFoodCategory_Should
    {
        [Test]
        public void RenderTheRightView_DeleteFoodCategory()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();

            //Act & Assert
            controller.WithCallTo(x => x.DeleteFoodCategory(id))
                .ShouldRenderView("DeleteFoodCategory");
        }

        [Test]
        public void RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.Empty;

            //Act & Assert
            controller.WithCallTo(x => x.DeleteFoodCategory(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_DeleteFoodCategory__WhenFoodCategoryWasNotFoundInDatabase()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            foodCategoriesServiceMock.Setup(x => x.GetFoodCategoryById(id)).Returns<FoodCategory>(null);

            //Act & Assert
            controller.WithCallTo(x => x.DeleteFoodCategory(id))
                .ShouldRenderView("DeleteFoodCategory");
        }

        [Test]
        public void RedirectToActionIndex__WhenFoodCategoryIsSuccessfullyDeleted()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            string name = null;
            FoodType foodType = FoodType.Vegetable;
            MeasuringUnitType measuringUnit = MeasuringUnitType.Kg;

            var foodcategory = new FoodCategory()
            {
                Id = id,
                Name = name,
                FoodType = foodType,
                MeasuringUnit = measuringUnit
            };

            foodCategoriesServiceMock.Setup(x => x.GetFoodCategoryById(id)).Returns(foodcategory);
            foodCategoriesServiceMock.Setup(x => x.DeleteFoodCategory(foodcategory));

            //Act & Assert
            controller.WithCallTo(x => x.DeleteFoodCategoryConfirm(id))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
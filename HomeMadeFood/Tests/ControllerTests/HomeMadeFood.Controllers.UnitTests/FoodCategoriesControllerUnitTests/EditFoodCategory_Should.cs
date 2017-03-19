using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.FoodCategoriesControllerUnitTests
{
    [TestFixture]
    public class EditFoodCategory_Should
    {
        [Test]
        public void RenderTheRightView_EditFoodCategory_WhenValidGuidIdIsPassed()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            var id = Guid.NewGuid();
            var name = "Tomatos";
            var foodType = FoodType.Vegetable;
            var measuringUnit = MeasuringUnitType.Kg;

            var foodcategory = new FoodCategory()
            {
                Id = id,
                Name = name,
                FoodType = foodType,
                MeasuringUnit = measuringUnit
            };

            foodCategoriesServiceMock.Setup(x => x.GetFoodCategoryById(id)).Returns(foodcategory);
            var model = new FoodCategoryViewModel();
            model.Id = foodcategory.Id;
            model.Name = foodcategory.Name;
            model.FoodType = foodcategory.FoodType;
            model.MeasuringUnit = foodcategory.MeasuringUnit;
            mappingServiceMock.Setup(x => x.Map<FoodCategoryViewModel>(foodcategory)).Returns(model);

            //Act & Assert
            controller.WithCallTo(x => x.EditFoodCategory(id))
                .ShouldRenderView("EditFoodCategory")
                .WithModel(model);
        }

        [Test]
        public void RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.EditFoodCategory(Guid.Empty))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_EditFoodCategory_WithTheCorrectModel_FoodCategoryViewModel_WhenModelStateIsNotValid()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            Guid id = Guid.NewGuid();
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
            var model = new FoodCategoryViewModel();
            model.Id = foodcategory.Id;
            model.Name = foodcategory.Name;
            model.FoodType = foodcategory.FoodType;
            model.MeasuringUnit = foodcategory.MeasuringUnit;
            mappingServiceMock.Setup(x => x.Map<FoodCategoryViewModel>(foodcategory)).Returns(model);

            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act & Assert
            controller.WithCallTo(x => x.EditFoodCategory(model))
                .ShouldRenderView("EditFoodCategory")
                .WithModel<FoodCategoryViewModel>()
                .AndModelError("Name");
        }

        [Test]
        public void RedirectToActionIndex_WithTheCorrectModel__WhenModelStateIsValid()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            Guid id = Guid.NewGuid();
            string name = "Tomatos";
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
            var model = new FoodCategoryViewModel();
            model.Id = foodcategory.Id;
            model.Name = foodcategory.Name;
            model.FoodType = foodcategory.FoodType;
            model.MeasuringUnit = foodcategory.MeasuringUnit;
            mappingServiceMock.Setup(x => x.Map<FoodCategoryViewModel>(foodcategory)).Returns(model);

            //Act & Assert
            controller.WithCallTo(x => x.EditFoodCategory(model))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}

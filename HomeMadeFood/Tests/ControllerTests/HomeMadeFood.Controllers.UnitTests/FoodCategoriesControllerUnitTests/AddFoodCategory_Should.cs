using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.FoodCategoriesControllerUnitTests
{
    [TestFixture]
    public class AddFoodCategory_Should
    {
        [Test]
        public void RenderTheRightView_AddFoodCategory()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.AddFoodCategory()).ShouldRenderView("AddFoodCategory");
        }

        [Test]
        public void RenderTheRightView_AddFoodCategory_WithTheCorrectModel_FoodCategoryViewModel_WhenModelStateIsNotValid()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            AddFoodCategoryViewModel foodCategoryModel = new AddFoodCategoryViewModel();
            foodCategoryModel.Name = null;
            var foodcategoryId = Guid.NewGuid();
            foodCategoryModel.FoodType = FoodType.Cheese;
            foodCategoryModel.MeasuringUnit = MeasuringUnitType.Kg;

            var validationContext = new ValidationContext(foodCategoryModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(foodCategoryModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act & Assert
            controller.WithCallTo(x => x.AddFoodCategory(foodCategoryModel))
                .ShouldRenderView("AddFoodCategory")
                .WithModel<AddFoodCategoryViewModel>()
                .AndModelError("Name");
        }

        [Test]
        public void RedirectToActionIndex_WithTheCorrectModel__WhenModelStateIsValid()
        {
            //Arrange
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new FoodCategoriesController(foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            AddFoodCategoryViewModel foodCategoryModel = new AddFoodCategoryViewModel();
            foodCategoryModel.Name = "Tomatos";
            var foodcategoryId = Guid.NewGuid();
            foodCategoryModel.FoodType = FoodType.Cheese;
            foodCategoryModel.MeasuringUnit = MeasuringUnitType.Kg;

            var validationContext = new ValidationContext(foodCategoryModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(foodCategoryModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act & Assert
            controller.WithCallTo(x => x.AddFoodCategory(foodCategoryModel))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
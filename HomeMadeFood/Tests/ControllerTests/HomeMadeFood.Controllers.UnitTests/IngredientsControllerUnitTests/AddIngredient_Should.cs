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
using HomeMadeFood.Models;
using System;

namespace HomeMadeFood.Controllers.UnitTests.IngredientsControllerUnitTests
{
    [TestFixture]
    public class AddIngredient_Should
    {
        [Test]
        public void RenderTheRightView_AddIngredient()
        {
            //Arrange
            var inredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(inredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.AddIngredient()).ShouldRenderView("AddIngredient");
        }

        [Test]
        public void RenderTheRightView_AddIngredient_WithTheCorrectModel_IngredientViewModel_WhenModelStateIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            AddIngredientViewModel ingredientModel = new AddIngredientViewModel();
            ingredientModel.Name = null;
            var selectedFoodCategoryId = Guid.NewGuid();
            ingredientModel.SelectedFoodCategoryId = selectedFoodCategoryId;
            //ingredientModel.FoodCategory = new FoodCategory()
            //{
            //    Name = "Tomatos",
            //    Id = Guid.NewGuid(),
            //    MeasuringUnit = MeasuringUnitType.Kg,
            //    FoodType = FoodType.Vegetable
            //};

            ingredientModel.PricePerMeasuringUnit = 1.80m;
            ingredientModel.QuantityInMeasuringUnit = 2;

            var validationContext = new ValidationContext(ingredientModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(ingredientModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act & Assert
            controller.WithCallTo(x => x.AddIngredient(ingredientModel))
                .ShouldRenderView("AddIngredient")
                .WithModel<AddIngredientViewModel>()
                .AndModelError("Name");
        }

        [Test]
        public void RedirectToActionIndex_WithTheCorrectModel__WhenModelStateIsValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            AddIngredientViewModel ingredientModel = new AddIngredientViewModel();
            ingredientModel.Name = "Pink Tomato";
            var selectedFoodCategoryId = Guid.NewGuid();
            ingredientModel.SelectedFoodCategoryId = selectedFoodCategoryId;
            //ingredientModel.FoodCategory = new FoodCategory()
            //{
            //    Name = "Tomatos",
            //    Id = Guid.NewGuid(),
            //    MeasuringUnit = MeasuringUnitType.Kg,
            //    FoodType = FoodType.Vegetable
            //};

            ingredientModel.PricePerMeasuringUnit = 1.80m;
            ingredientModel.QuantityInMeasuringUnit = 2;

            //Act & Assert
            controller.WithCallTo(x => x.AddIngredient(ingredientModel))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
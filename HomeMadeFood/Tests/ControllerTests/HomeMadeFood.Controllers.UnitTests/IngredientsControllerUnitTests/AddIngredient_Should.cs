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
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(inredientsServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.AddIngredient()).ShouldRenderView("AddIngredient");
        }

        [Test]
        public void RenderTheRightView_AddIngredient_WithTheCorrectModel_IngredientViewModel_WhenModelStateIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(ingredientsServiceMock.Object, mappingServiceMock.Object);

            IngredientViewModel ingredientModel = new IngredientViewModel();
            ingredientModel.FoodType = FoodType.Spice;
            ingredientModel.MeasuringUnit = MeasuringUnitType.Kg;
            ingredientModel.PricePerMeasuringUnit = -0.30m;
            ingredientModel.Quantity = -0.5m;

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
                .WithModel<IngredientViewModel>()
                .AndModelError("Name");
        }

        [Test]
        public void RedirectToActionIndex_WithTheCorrectModel__WhenModelStateIsValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(ingredientsServiceMock.Object, mappingServiceMock.Object);

            IngredientViewModel ingredientModel = new IngredientViewModel();
            ingredientModel.Name = "Carrot";
            ingredientModel.FoodType = FoodType.Vegetable;
            ingredientModel.MeasuringUnit = MeasuringUnitType.Kg;
            ingredientModel.PricePerMeasuringUnit = 1.80m;
            ingredientModel.Quantity = 2;

            //Act & Assert
            controller.WithCallTo(x => x.AddIngredient(ingredientModel))
                .ShouldRedirectTo(x => x.Index(null));
        }
    }
}
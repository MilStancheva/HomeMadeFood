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

namespace HomeMadeFood.Controllers.UnitTests.IngredientsControllerUnitTests
{
    [TestFixture]
    public class EditIngredient_Should
    {
        [Test]
        public void RenderTheRightView_EditIngredient()
        {
            //Arrange
            var inredientsServiceMock = new Mock<IIngredientsService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(inredientsServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();

            //Act & Assert
            controller.WithCallTo(x => x.EditIngredient(id))
                .ShouldRenderView("EditIngredient");
        }

        [Test]
        //[Ignore("404 page will be added when app is ready for deployment")]
        public void RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var inredientsServiceMock = new Mock<IIngredientsService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(inredientsServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.EditIngredient(Guid.Empty))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_EditIngredient_WithTheCorrectModel_IngredientViewModel_WhenModelStateIsNotValid()
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
            controller.WithCallTo(x => x.EditIngredient(ingredientModel))
                .ShouldRenderView("EditIngredient")
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
            controller.WithCallTo(x => x.EditIngredient(ingredientModel))
                .ShouldRedirectTo(x => x.Index(null));
        }
    }
}
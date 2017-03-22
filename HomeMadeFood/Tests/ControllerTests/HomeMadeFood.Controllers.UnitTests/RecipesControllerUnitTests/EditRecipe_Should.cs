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

namespace HomeMadeFood.Controllers.UnitTests.RecipesControllerUnitTests
{
    [TestFixture]
    public class EditRecipe_Should
    {
        [Test]
        public void RenderTheRightView_EditRecipe_WhenValidGuidIdIsPassed()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var recipe = new Recipe()
            {
                Id = id,
                Title = "Tomato Salad",
                DishType = DishType.Salad,
                Describtion = "Some describtion",
                Instruction = "These are the instructions"
            };
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            recipesServiceMock.Setup(x => x.GetRecipeById(id)).Returns(recipe);

            var model = new RecipeViewModel()
            {
                Title = recipe.Title,
                DishType = recipe.DishType,
                Instruction = recipe.Instruction,
                Describtion = recipe.Describtion
            };
            mappingServiceMock.Setup(x => x.Map<RecipeViewModel>(recipe)).Returns(model);

            //Act & Assert
            controller.WithCallTo(x => x.EditRecipe(id))
                .ShouldRenderView("EditRecipe")
                .WithModel(model);
        }

        [Test]
        public void RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.Empty;

            //Act & Assert
            controller.WithCallTo(x => x.EditRecipe(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_EditRecipe_WithTheCorrectModel_RecipeViewModel_WhenModelStateIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.NewGuid();
            var recipe = new Recipe()
            {
                Id = id,
                Title = "Tomato Salad",
                DishType = DishType.Salad
            };
            var model = new RecipeViewModel()
            {
                Title = recipe.Title,
                DishType = recipe.DishType,
                Instruction = recipe.Instruction,
                Describtion = recipe.Describtion
            };
            mappingServiceMock.Setup(x => x.Map<RecipeViewModel>(recipe)).Returns(model);
            
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act & Assert
            controller.WithCallTo(x => x.EditRecipe(model))
                .ShouldRenderView("EditRecipe")
                .WithModel<RecipeViewModel>()
                .AndModelError("Instruction");
        }

        [Test]
        public void RedirectToActionIndex_WithTheCorrectModel__WhenModelStateIsValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);

            Guid id = Guid.NewGuid();
            var recipe = new Recipe()
            {
                Id = id,
                Title = "Tomato Salad",
                DishType = DishType.Salad,
                Instruction = "These are the instructions",
                Describtion = "The describtion of tge recipe is here"
            };
            var model = new RecipeViewModel()
            {
                Title = recipe.Title,
                DishType = recipe.DishType,
                Instruction = recipe.Instruction,
                Describtion = recipe.Describtion
            };
            mappingServiceMock.Setup(x => x.Map<RecipeViewModel>(recipe)).Returns(model);

            //Act & Assert
            controller.WithCallTo(x => x.EditRecipe(model))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
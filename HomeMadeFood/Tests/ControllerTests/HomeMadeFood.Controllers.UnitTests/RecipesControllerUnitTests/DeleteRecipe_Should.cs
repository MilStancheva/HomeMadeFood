using System;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;

namespace HomeMadeFood.Controllers.UnitTests.RecipesControllerUnitTests
{
    [TestFixture]
    public class DeleteRecipe_Should
    {
        [Test]
        public void RenderTheRightView_DeleteRecipe()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();

            //Act & Assert
            controller.WithCallTo(x => x.DeleteRecipe(id))
                .ShouldRenderView("DeleteRecipe");
        }

        [Test]
        public void DeleteRecipeConfirm_Should_RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.Empty;

            //Act & Assert
            controller.WithCallTo(x => x.DeleteRecipeConfirm(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void DeleteRecipe_Should_RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            Guid id = Guid.Empty;

            //Act & Assert
            controller.WithCallTo(x => x.DeleteRecipe(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_DeleteRecipe__WhenRecipeWasNotFoundInDatabase()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            recipesServiceMock.Setup(x => x.GetRecipeById(id)).Returns<Recipe>(null);

            //Act & Assert
            controller.WithCallTo(x => x.DeleteRecipe(id))
                .ShouldRenderView("DeleteRecipe");
        }

        [Test]
        public void RedirectToActionIndex__WhenRecipeIsSuccessfullyDeleted()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();

            var recipe = new Recipe()
            {
                Id = id,
                Title = "Tomato Salad",
                DishType = DishType.Salad,
                Instruction = "These are the instructions",
                Describtion = "The describtion of tge recipe is here"
            };

            recipesServiceMock.Setup(x => x.GetRecipeById(id)).Returns(recipe);
            recipesServiceMock.Setup(x => x.DeleteRecipe(recipe));

            //Act & Assert
            controller.WithCallTo(x => x.DeleteRecipeConfirm(id))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
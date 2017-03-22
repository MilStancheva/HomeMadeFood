using System;

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
    public class DetailsRecipe_Should
    {
        [Test]
        public void RenderTheRightView_DetailsRecipe()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new RecipesController(recipesServiceMock.Object, ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();

            //Act & Assert
            controller.WithCallTo(x => x.DetailsRecipe(id))
                .ShouldRenderView("DetailsRecipe");
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
            controller.WithCallTo(x => x.DetailsRecipe(id))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_DetailsRecipeWithModel_RecipeViewModel_When_IdGuidIsValid()
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
            recipesServiceMock.Setup(x => x.GetRecipeById(id)).Returns(recipe);
            mappingServiceMock.Setup(x => x.Map<RecipeViewModel>(recipe)).Returns(model);

            //Act & Assert
            controller.WithCallTo(x => x.DetailsRecipe(id))
                .ShouldRenderView("DetailsRecipe")
                .WithModel(model)
                .AndNoModelErrors();
        }
    }
}
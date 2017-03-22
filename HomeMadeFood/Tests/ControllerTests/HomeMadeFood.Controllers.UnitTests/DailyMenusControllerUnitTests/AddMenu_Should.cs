using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.DailyMenusControllerUnitTests
{
    [TestFixture]
    public class AddMenu_Should
    {
        [Test]
        public void RenderTheRightPartialView_AddMenu()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);

            var model = new EditDailyMenuViewModel()
            {
                Id = Guid.NewGuid(),
                SelectedDate = DateTime.Today,
                SelectedDailyMenuViewModel = new DailyMenuViewModel(),
                DailyMenuViewModelWithAllRecipes = new AddDailyMenuViewModel()
            };

            //Act & Assert
            controller.WithCallTo(x => x.AddMenu(model))
                .ShouldRenderPartialView("_AddMenu");
        }

        [Test]
        public void RenderTheRightPartialView_AddMenu_WithEditDailyMenuViewModel_AndNoModelErrors()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);

            var model = new EditDailyMenuViewModel()
            {
                Id = Guid.NewGuid(),
                SelectedDate = DateTime.Today,
                SelectedDailyMenuViewModel = new DailyMenuViewModel(),
                DailyMenuViewModelWithAllRecipes = new AddDailyMenuViewModel()
            };

            //Act & Assert
            controller.WithCallTo(x => x.AddMenu(model))
                .ShouldRenderPartialView("_AddMenu")
                .WithModel<EditDailyMenuViewModel>()
                .AndNoModelErrors();
        }

        [Test]
        public void RenderTheRightPartialView_AddMenu_WithEditDailyMenuViewModel_AndNoModelErrors_AndCorrectContent()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);

            Guid recipeId = Guid.NewGuid();
            IEnumerable<Guid> recipesIds = new List<Guid>() { recipeId };
            DateTime date = new DateTime(2017, 5, 5);
            Guid dailyMenuId = Guid.NewGuid();

            var ingredientId = Guid.NewGuid();
            var name = "IngredientName";
            var foodcategoryId = Guid.NewGuid();
            var quantityInMeasuringUnit = 0.200;
            var pricePerMeasuringUnit = 1.29m;

            var ingredientViewModel = new IngredientViewModel()
            {
                Id = ingredientId,
                Name = name,
                FoodCategoryId = foodcategoryId,
                QuantityInMeasuringUnit = quantityInMeasuringUnit,
                PricePerMeasuringUnit = pricePerMeasuringUnit
            };

            var title = "Title Of A New Recipe";
            var describtion = "This is a decsribtion";
            var instructions = "Instructions of the recipe";
            var dishType = DishType.MainDish;

            var recipeViewModel = new RecipeViewModel()
            {
                Title = title,
                Describtion = describtion,
                Instruction = instructions,
                DishType = dishType,
                Ingredients = new List<IngredientViewModel>() { ingredientViewModel }
            };

            var dailyMenuGuid = Guid.NewGuid();
            var addDailyMenuModel = new AddDailyMenuViewModel()
            {
                Date = date,
                Salads = new List<RecipeViewModel>() { recipeViewModel}
            };

            var model = new EditDailyMenuViewModel()
            {
                Id = Guid.NewGuid(),
                SelectedDate = DateTime.Today,
                SelectedDailyMenuViewModel = new DailyMenuViewModel(),
                DailyMenuViewModelWithAllRecipes = addDailyMenuModel
            };

            //Act & Assert
            controller.WithCallTo(x => x.AddMenu(model))
                        .ShouldRenderPartialView("_AddMenu")
                        .WithModel<EditDailyMenuViewModel>(
                        viewModel => Assert.AreEqual(model.DailyMenuViewModelWithAllRecipes.Date, date))
                        .AndNoModelErrors();
        }
    }
}
using System;
using System.Collections.Generic;
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

namespace HomeMadeFood.Controllers.UnitTests.DailyMenusControllerUnitTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void RenderTheRightPartialView_DailyMenusGridPartial()
        {
            //Arrange
            IEnumerable<DailyMenu> dailyMenus = new List<DailyMenu>();
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            dailyMenusServiceMock.Setup(x => x.GetAllDailyMenus()).Returns(dailyMenus);
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            string recipeTitle = "Backed Potatoes";

            //Act & Assert
            controller.WithCallTo(x => x.Search(recipeTitle))
                .ShouldRenderPartialView("_DailyMenusGridPartial");
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchDailyMenuViewModelAndNoModelErrors()
        {
            //Arrange
            IEnumerable<DailyMenu> dailyMenus = new List<DailyMenu>();
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            dailyMenusServiceMock.Setup(x => x.GetAllDailyMenus()).Returns(dailyMenus);
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);
            string recipeTitle = "Backed Potatoes";

            //Act & Assert
            controller.WithCallTo(x => x.Search(recipeTitle))
                .ShouldRenderPartialView("_DailyMenusGridPartial")
                .WithModel<SearchDailyMenuViewModel>()
                .AndNoModelErrors();
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchDailyMenuViewModelAndNoModelErrorsAndCorrectContent()
        {
            //Arrange 
            IEnumerable<DailyMenu> dailyMenus = new List<DailyMenu>();
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            dailyMenusServiceMock.Setup(x => x.GetAllDailyMenus()).Returns(dailyMenus);
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();

            var controller = new DailyMenusController(recipesServiceMock.Object, dailyMenusServiceMock.Object, mappingServiceMock.Object);

            Guid recipeId = Guid.NewGuid();
            IEnumerable<Guid> recipesIds = new List<Guid>() { recipeId };
            DateTime date = new DateTime(2017, 5, 5);

            Guid dailyMenuId = Guid.NewGuid();

            var ingredients = new List<Ingredient>()
                {
                    new Ingredient()
                    {
                        Id = Guid.NewGuid(),
                        Name = "IngredientName",
                        FoodCategoryId = Guid.NewGuid(),
                        RecipeId = recipeId,
                        QuantityInMeasuringUnit = 0.200,
                        PricePerMeasuringUnit = 1.29m
                    }
                };

            var recipe = new Recipe()
            {
                Id = recipeId,
                Title = "Title Of A New Recipe",
                Describtion = "This is a decsribtion",
                Instruction = "Instructions of the recipe",
                DishType = DishType.MainDish,
                Ingredients = ingredients
            };

            var recipes = new List<Recipe>()
                {
                    recipe
                };

            DailyMenu dailyMenu = new DailyMenu()
            {
                Id = dailyMenuId,
                Date = date,
                DayOfWeek = date.DayOfWeek,
                Recipes = recipes

            };

            var dailyMenuGuid = Guid.NewGuid();
            var dailyMenuModel = new DailyMenuViewModel()
            {
                Id = dailyMenuGuid,
                Date = date,
                Salads = new List<RecipeViewModel>()
            };

            var dailyMenusModelCollection = new List<DailyMenuViewModel>() { dailyMenuModel };

            var searchModel = new SearchDailyMenuViewModel()
            {
                PageSize = 5,
                TotalRecords = recipes.Count,
                DailyMenus = dailyMenusModelCollection
            };
            string recipeTitle = "Backed Potatoes";
            
            //Act & Assert
            controller.WithCallTo(x => x.Search(recipeTitle))
                .ShouldRenderPartialView("_DailyMenusGridPartial")
                .WithModel<SearchDailyMenuViewModel>(
                    viewModel => Assert.AreEqual(dailyMenusModelCollection[0].Date, searchModel.DailyMenus.ToList()[0].Date))
                .AndNoModelErrors();
        }
    }
}
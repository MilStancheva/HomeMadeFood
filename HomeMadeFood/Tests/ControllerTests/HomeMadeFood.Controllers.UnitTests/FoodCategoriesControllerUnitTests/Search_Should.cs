using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Models;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.FoodCategoriesControllerUnitTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void RenderTheRightPartialView()
        {
            //Arrange
            IEnumerable<FoodCategory> foodCategories = new List<FoodCategory>();
            var foodCategorieServiceMock = new Mock<IFoodCategoriesService>();
            foodCategorieServiceMock.Setup(x => x.GetAllFoodCategories()).Returns(foodCategories);
            var mappingServiceMock = new Mock<IMappingService>();
            var foodCategoriesModel = new List<FoodCategoryViewModel>();
            var gridPageSize = 5;
            string name = "Tomato";
            foreach (var foodCategory in foodCategories)
            {
                mappingServiceMock.Setup(x => x.Map<FoodCategoryViewModel>(foodCategory)).Returns(It.IsAny<FoodCategoryViewModel>());
            }

            var controller = new FoodCategoriesController(foodCategorieServiceMock.Object, mappingServiceMock.Object);

            var searchModel = new SearchFoodCategoryViewModel();
            searchModel.FoodCategories = foodCategoriesModel;
            searchModel.PageSize = gridPageSize;
            searchModel.TotalRecords = foodCategoriesModel.Count();

            //Act & Assert
            controller.WithCallTo(x => x.Search(name)).ShouldRenderPartialView("_FoodCategoriesGridPartial");
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchFoodcategoryViewModelAndNoModelErrors()
        {
            //Arrange
            IEnumerable<FoodCategory> foodCategories = new List<FoodCategory>()
            {
                new FoodCategory() { Name = "tomato"}
            };
            var gridPageSize = 5;
            string name = "Tomato";
            var foodCategorieServiceMock = new Mock<IFoodCategoriesService>();
            foodCategorieServiceMock.Setup(x => x.GetAllFoodCategories()).Returns(foodCategories);
            var mappingServiceMock = new Mock<IMappingService>();
            var foodCategoriesModel = new List<FoodCategoryViewModel>() { new FoodCategoryViewModel() { Name = name } };

            foreach (var foodCategory in foodCategories)
            {
                mappingServiceMock.Setup(x => x.Map<FoodCategoryViewModel>(foodCategory))
                    .Returns(It.IsAny<FoodCategoryViewModel>());
            }

            var controller = new FoodCategoriesController(foodCategorieServiceMock.Object, mappingServiceMock.Object);

            var searchModel = new SearchFoodCategoryViewModel();
            searchModel.FoodCategories = foodCategoriesModel;
            searchModel.PageSize = gridPageSize;
            searchModel.TotalRecords = foodCategoriesModel.Count();

            //Act & Assert
            controller.WithCallTo(x => x.Search(name))
                .ShouldRenderPartialView("_FoodCategoriesGridPartial")
                .WithModel<SearchFoodCategoryViewModel>()
                .AndNoModelErrors();
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchFoodCategoryViewModelAndNoModelErrorsAndCorrectContent()
        {
            //Arrange
            IEnumerable<FoodCategory> foodCategories = new List<FoodCategory>()
            {
                new FoodCategory() { Name = "tomato"}
            };
            var gridPageSize = 5;
            string name = "Tomato";
            var foodCategorieServiceMock = new Mock<IFoodCategoriesService>();
            foodCategorieServiceMock.Setup(x => x.GetAllFoodCategories()).Returns(foodCategories);
            var mappingServiceMock = new Mock<IMappingService>();
            var foodCategoriesModel = new List<FoodCategoryViewModel>() { new FoodCategoryViewModel() { Name = name } };

            foreach (var foodCategory in foodCategories)
            {
                mappingServiceMock.Setup(x => x.Map<FoodCategoryViewModel>(foodCategory))
                    .Returns(It.IsAny<FoodCategoryViewModel>());
            }

            var controller = new FoodCategoriesController(foodCategorieServiceMock.Object, mappingServiceMock.Object);

            var searchModel = new SearchFoodCategoryViewModel();
            searchModel.FoodCategories = foodCategoriesModel;
            searchModel.PageSize = gridPageSize;
            searchModel.TotalRecords = foodCategoriesModel.Count();

            //Act & Assert
            controller.WithCallTo(x => x.Search(name))
                .ShouldRenderPartialView("_FoodCategoriesGridPartial")
                .WithModel<SearchFoodCategoryViewModel>(
                viewModel => Assert.AreEqual(foodCategoriesModel.ToList()[0].Name, searchModel.FoodCategories.ToList()[0].Name))
                .AndNoModelErrors();
        }
    }
}

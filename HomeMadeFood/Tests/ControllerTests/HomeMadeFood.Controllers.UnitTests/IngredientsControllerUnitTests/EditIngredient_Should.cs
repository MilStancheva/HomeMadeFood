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
using HomeMadeFood.Models;

namespace HomeMadeFood.Controllers.UnitTests.IngredientsControllerUnitTests
{
    [TestFixture]
    public class EditIngredient_Should
    {
        [Test]
        public void RenderTheRightView_EditIngredient_WhenValidGuidIdIsPassed()
        {
            //Arrange
            var foodCategoriesModels = new List<FoodCategoryViewModel>()
            {
                new FoodCategoryViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Tomatos",
                    FoodType = FoodType.Vegetable,
                    MeasuringUnit = MeasuringUnitType.Kg
                }
            };
            var recipesModels = new List<RecipeViewModel>()
            {
                new RecipeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Title = "Tomato Salad",
                    DishType = DishType.Salad,
                    Describtion = "Some describtion",
                    Instruction = "These are the instructions"
                }
            };

            var foodCategories= new List<FoodCategory>()
            {
                new FoodCategory()
                {
                    Id = Guid.NewGuid(),
                    Name = "Tomatos",
                    FoodType = FoodType.Vegetable,
                    MeasuringUnit = MeasuringUnitType.Kg
                }
            };
            var recipes = new List<Recipe>()
            {
                new Recipe()
                {
                    Id = Guid.NewGuid(),
                    Title = "Tomato Salad",
                    DishType = DishType.Salad,
                    Describtion = "Some describtion",
                    Instruction = "These are the instructions"
                }
            };
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            foodCategoriesServiceMock.Setup(x => x.GetAllFoodCategories()).Returns(foodCategories);
            var recipesServiceMock = new Mock<IRecipesService>();
            recipesServiceMock.Setup(x => x.GetAllRecipes()).Returns(recipes);

            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, recipesServiceMock.Object, mappingServiceMock.Object);
            var id = Guid.NewGuid();
            var name = "Tomato";
            var foodcategoryId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();
            var ingredient = new Ingredient()
            {
                Id = id,
                Name = name,
                FoodcategoryId = foodcategoryId,
                RecipeId = recipeId
            };
            ingredientsServiceMock.Setup(x => x.GetIngredientById(id)).Returns(ingredient);
            var model = new IngredientViewModel();
            model.FoodCategoryId = ingredient.FoodcategoryId;
            model.Name = ingredient.Name;
            model.RecipeId = ingredient.RecipeId;
            model.Id = ingredient.Id;
            model.FoodCategories = foodCategoriesModels;
            model.Recipes = recipesModels;
            mappingServiceMock.Setup(x => x.Map<IngredientViewModel>(ingredient)).Returns(model);

            //Act & Assert
            controller.WithCallTo(x => x.EditIngredient(id))
                .ShouldRenderView("EditIngredient")
                .WithModel(model);
        }

        [Test]
        public void RedirectToErrorPage_When_IdGuidIsNotValid()
        {
            //Arrange
            var inredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(inredientsServiceMock.Object, foodCategoriesServiceMock.Object, recipesServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.EditIngredient(Guid.Empty))
                .ShouldRenderView("404.html");
        }

        [Test]
        public void RenderTheRightView_EditIngredient_WithTheCorrectModel_IngredientViewModel_WhenModelStateIsNotValid()
        {
            //Arrange
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, recipesServiceMock.Object, mappingServiceMock.Object);

            IngredientViewModel ingredientModel = new IngredientViewModel();
            ingredientModel.Name = null;
            ingredientModel.FoodCategory = new FoodCategory()
            {
                Name = "Tomatos",
                Id = Guid.NewGuid(),
                MeasuringUnit = MeasuringUnitType.Kg,
                FoodType = FoodType.Vegetable
            };

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
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            var recipesServiceMock = new Mock<IRecipesService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new IngredientsController(ingredientsServiceMock.Object, foodCategoriesServiceMock.Object, recipesServiceMock.Object, mappingServiceMock.Object);

            IngredientViewModel ingredientModel = new IngredientViewModel();
            ingredientModel.Name = "Carrot";
            ingredientModel.FoodCategory = new FoodCategory()
            {
                Name = "Tomatos",
                Id = Guid.NewGuid(),
                MeasuringUnit = MeasuringUnitType.Kg,
                FoodType = FoodType.Vegetable
            };

            ingredientModel.PricePerMeasuringUnit = 1.80m;
            ingredientModel.QuantityInMeasuringUnit = 2;

            //Act & Assert
            controller.WithCallTo(x => x.EditIngredient(ingredientModel))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
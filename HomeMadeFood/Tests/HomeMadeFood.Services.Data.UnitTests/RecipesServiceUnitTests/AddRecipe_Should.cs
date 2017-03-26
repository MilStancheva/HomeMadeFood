using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Data.Contracts;
using System.Linq;
using System.Data.Entity;

namespace HomeMadeFood.Services.Data.UnitTests.RecipesServiceUnitTests
{
    [TestFixture]
    public class AddRecipe_Should
    {
        [Test]
        public void Throw_WhenThePassedRecipeIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
            Recipe recipe = null;
            IEnumerable<string> ingredientNames = new List<string>() { "Tomato" };
            IEnumerable<double> ingredientQuantities = new List<double>() { 1.200 };
            IEnumerable<decimal> ingredientPrices = new List<decimal>() { 4.80m };
            IEnumerable<Guid> foodCategories = new List<Guid>() { Guid.NewGuid() };

            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => recipesService.AddRecipe(
                    recipe,
                    ingredientNames,
                    ingredientQuantities,
                    ingredientPrices,
                    foodCategories));
        }

        //[Test]
        //public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        //{
        //    //Arrange
        //    var dataMock = new Mock<IHomeMadeFoodData>();
        //    var ingredientsServiceMock = new Mock<IIngredientsService>();
        //    var foodcategoriesServiceMock = new Mock<IFoodCategoriesService>();
        //    RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object, foodcategoriesServiceMock.Object);

        //    List<string> ingredientNames = new List<string>() { "Tomato" };
        //    List<double> ingredientQuantities = new List<double>() { 1.200 };
        //    List<decimal> ingredientPrices = new List<decimal>() { 4.80m };
        //    List<Guid> foodCategories = new List<Guid>() { Guid.NewGuid() };

        //    ingredientsServiceMock.Setup(x => x.CreateIngredient(ingredientNames[0], foodCategories[0], ingredientPrices[0], ingredientQuantities[0]))
        //        .Returns(() =>
        //    {
        //        return new Ingredient()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = ingredientNames[0],
        //            FoodCategoryId = foodCategories[0],
        //            QuantityInMeasuringUnit = ingredientQuantities[0],
        //            PricePerMeasuringUnit = ingredientPrices[0]
        //        };
        //    });

        //    Guid recipeId = Guid.NewGuid();            
        //    Recipe recipe = new Recipe()
        //    {
        //        Id = recipeId,
        //        Title = "Title Of A New Recipe",
        //        Describtion = "This is a decsribtion",
        //        Instruction = "Instructions of the recipe",
        //        DishType = DishType.MainDish
        //    };

        //    dataMock.Setup(x => x.Recipes.Add(recipe));

        //    //Act
        //    recipesService.AddRecipe(recipe,
        //            ingredientNames,
        //            ingredientQuantities,
        //            ingredientPrices,
        //            foodCategories);

        //    //Assert
        //    dataMock.Verify(x => x.Commit(), Times.Once);
        //}

        //[Test]
        //public void InvokeDataRecipesAddOnce_WhenThePassedArgumentsAreValid()
        //{
        //    var dataMock = new Mock<IHomeMadeFoodData>();
        //    var ingredientsServiceMock = new Mock<IIngredientsService>();
        //    var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
        //    RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
        //    List<string> ingredientNames = new List<string>() { "Tomato" };
        //    List<double> ingredientQuantities = new List<double>() { 1.200 };
        //    List<decimal> ingredientPrices = new List<decimal>() { 4.80m };
        //    List<Guid> foodCategories = new List<Guid>() { Guid.NewGuid() };

        //    Guid recipeId = Guid.NewGuid();
        //    var ingredient = new Ingredient()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = ingredientNames[0],
        //        FoodCategoryId = foodCategories[0],
        //        RecipeId = recipeId,
        //        QuantityInMeasuringUnit = ingredientQuantities[0],
        //        PricePerMeasuringUnit = ingredientPrices[0]
        //    };

        //    var ingredients = new List<Ingredient>()
        //        {
        //            ingredient
        //        };
        //    Recipe recipe = new Recipe()
        //    {
        //        Id = recipeId,
        //        Title = "Title Of A New Recipe",
        //        Describtion = "This is a decsribtion",
        //        Instruction = "Instructions of the recipe",
        //        DishType = DishType.MainDish,
        //        Ingredients = ingredients
        //    };
        //    var costPerPortion = ingredients.Select(x => x.PricePerMeasuringUnit).Sum();
        //    var quantityPerPortion = ingredients.Select(x => x.QuantityInMeasuringUnit).Sum();
        //    var costPercentage = 0.30m;
        //    recipe.CostPerPortion = costPerPortion;
        //    recipe.QuantityPerPortion = quantityPerPortion;
        //    recipe.PricePerPortion = costPerPortion / costPercentage;
        //    ingredientsServiceMock.Setup(x => x.CreateIngredient(ingredientNames[0], foodCategories[0], ingredientPrices[0], ingredientQuantities[0])).Returns(ingredient);
        //    foodCategoriesServiceMock.Setup(x => x.AddIngredientCostToFoodCategory(ingredient));
        //    foodCategoriesServiceMock.Setup(x => x.AddIngredientQuantityToFoodCategory(ingredient));
        //    dataMock.Setup(x => x.Recipes.Add(recipe));

        //    //Act

        //    recipesService.AddRecipe(recipe,
        //            ingredientNames,
        //            ingredientQuantities,
        //            ingredientPrices,
        //            foodCategories);

        //    //Assert
        //    dataMock.Verify(x => x.Recipes.Add(recipe), Times.Once);
        //}
    }
}
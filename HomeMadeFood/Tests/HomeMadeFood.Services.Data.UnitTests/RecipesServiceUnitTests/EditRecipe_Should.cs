using HomeMadeFood.Data.Data;
using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.UnitTests.RecipesServiceUnitTests
{
    [TestFixture]
    public class EditRecipe_Should
    {
        [Test]
        public void Throw_WhenThePassedRecipeIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            RecipesService recipesService = new RecipesService(dataMock.Object);
            Recipe recipe = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => recipesService.EditRecipe(recipe));
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            RecipesService recipeService = new RecipesService(dataMock.Object);
            List<string> ingredientNames = new List<string>() { "Tomato" };
            List<double> ingredientQuantities = new List<double>() { 1.200 };
            List<decimal> ingredientPrices = new List<decimal>() { 4.80m };
            List<Guid> foodCategories = new List<Guid>() { Guid.NewGuid() };

            Guid recipeId = Guid.NewGuid();
            Recipe recipe = new Recipe()
            {
                Id = recipeId,
                Title = "Title Of A New Recipe",
                Describtion = "This is a decsribtion",
                Instruction = "Instructions of the recipe",
                DishType = DishType.MainDish,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient()
                    {
                        Id = Guid.NewGuid(),
                        Name = ingredientNames[0],
                        FoodCategoryId = foodCategories[0],
                        RecipeId = recipeId,
                        QuantityInMeasuringUnit = ingredientQuantities[0],
                        PricePerMeasuringUnit = ingredientPrices[0]
                    }
                }
            };

            dataMock.Setup(x => x.Recipes.Update(It.IsAny<Recipe>()));

            //Act
            recipeService.EditRecipe(recipe);

            //Assert
            dataMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void InvokeDataRecipesUpdateOnce_WhenThePassedArgumentsAreValid()
        {
            var dataMock = new Mock<IHomeMadeFoodData>();
            RecipesService recipeService = new RecipesService(dataMock.Object);
            List<string> ingredientNames = new List<string>() { "Tomato" };
            List<double> ingredientQuantities = new List<double>() { 1.200 };
            List<decimal> ingredientPrices = new List<decimal>() { 4.80m };
            List<Guid> foodCategories = new List<Guid>() { Guid.NewGuid() };

            Guid recipeId = Guid.NewGuid();
            Recipe recipe = new Recipe()
            {
                Id = recipeId,
                Title = "Title Of A New Recipe",
                Describtion = "This is a decsribtion",
                Instruction = "Instructions of the recipe",
                DishType = DishType.MainDish,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient()
                    {
                        Id = Guid.NewGuid(),
                        Name = ingredientNames[0],
                        FoodCategoryId = foodCategories[0],
                        RecipeId = recipeId,
                        QuantityInMeasuringUnit = ingredientQuantities[0],
                        PricePerMeasuringUnit = ingredientPrices[0]
                    }
                }
            };

            dataMock.Setup(x => x.Recipes.Update(It.IsAny<Recipe>()));

            //Act
            recipeService.EditRecipe(recipe);

            //Assert
            dataMock.Verify(x => x.Recipes.Update(recipe), Times.Once);
        }
    }
}
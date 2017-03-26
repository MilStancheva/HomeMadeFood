using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.RecipesServiceUnitTests
{
    [TestFixture]
    public class DeleteRecipe_Should
    {
        [Test]
        public void Throw_WhenThePassedRecipeIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
            dataMock.Setup(x => x.Recipes.Delete(It.IsAny<Recipe>()));

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => recipesService.DeleteRecipe(null));
        }

        [Test]
        public void InvokeDataRecipesDeleteOnce_WhenThePassedArgumentIsValid()
        {
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
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

            dataMock.Setup(x => x.Recipes.Delete(It.IsAny<Recipe>()));

            //Act
            recipesService.DeleteRecipe(recipe);

            //Assert
            dataMock.Verify(x => x.Recipes.Delete(recipe), Times.Once);
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var ingredientsServiceMock = new Mock<IIngredientsService>();
            RecipesService recipesService = new RecipesService(dataMock.Object, ingredientsServiceMock.Object);
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

            dataMock.Setup(x => x.Recipes.Delete(It.IsAny<Recipe>()));

            //Act
            recipesService.DeleteRecipe(recipe);

            //Assert
            dataMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
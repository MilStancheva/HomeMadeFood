using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.UnitTests.RecipesServiceUnitTests
{
    [TestFixture]
    public class GetRecipeById_Should
    {
        [Test]
        public void ShouldThrow_WhenGuidIdParameterIsEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            RecipesService recipesService = new RecipesService(dataMock.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => recipesService.GetRecipeById(Guid.Empty));
        }

        [Test]
        public void ReturnRecipe_WhenIdIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
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
                        Name = "Tomato",
                        FoodcategoryId = Guid.NewGuid(),
                        RecipeId = recipeId,
                        QuantityInMeasuringUnit = 0.150,
                        PricePerMeasuringUnit = 0.55m
                    }
                }
            };

            dataMock.Setup(c => c.Recipes.GetById(recipeId)).Returns(recipe);

            RecipesService recipesService = new RecipesService(dataMock.Object);

            //Act
            Recipe recipeResult = recipesService.GetRecipeById(recipeId);

            //Assert
            Assert.AreSame(recipe, recipeResult);
        }
    }
}
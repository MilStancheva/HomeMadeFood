using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using System.Collections.Generic;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.IngredientsServiceUnitTests
{
    [TestFixture]
    public class AddIngredient_Should
    {
        [Test]
        public void Throw_WhenThePassedNameIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            string ingredientName = null;
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => ingredientsService.AddIngredient(ingredientName, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit, recipeId));
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            Guid ingredientId = Guid.NewGuid();
            string ingredientName = "NameOfTheIngredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            string foodCategoryName = "FoodcategoryName";
            ICollection<Ingredient> foodcategoryIngredients = new List<Ingredient>();
            FoodCategory foodcategory = new FoodCategory()
            {
                Id = foodCategoryId,
                Name = foodCategoryName,
                Ingredients = foodcategoryIngredients
            };

            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                FoodCategoryId = foodCategoryId,
                FoodCategory = foodcategory,
                RecipeId = recipeId,
                QuantityInMeasuringUnit = quantityPerMeasuringUnit
            };

            dataMock.Setup(x => x.FoodCategories.GetById(foodCategoryId)).Returns(foodcategory);
            dataMock.Setup(x => x.Ingredients.Add(ingredient));

            //Act
            ingredientsService.AddIngredient(ingredientName, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit, recipeId);

            //Assert
            dataMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void InvokeDataIngredientsAddOnce_WhenThePassedArgumentsAreValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            Guid ingredientId = Guid.NewGuid();
            string ingredientName = "NameOfTheIngredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            string foodCategoryName = "FoodcategoryName";
            ICollection<Ingredient> foodcategoryIngredients = new List<Ingredient>();
            FoodCategory foodcategory = new FoodCategory()
            {
                Id = foodCategoryId,
                Name = foodCategoryName,
                Ingredients = foodcategoryIngredients
            };

            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                FoodCategoryId = foodCategoryId,
                FoodCategory = foodcategory,
                RecipeId = recipeId
            };

            dataMock.Setup(x => x.FoodCategories.GetById(foodCategoryId)).Returns(foodcategory);
            dataMock.Setup(x => x.Ingredients.Add(ingredient));

            //Act
            ingredientsService.AddIngredient(ingredientName, foodCategoryId, pricePerMeasuringUnit, quantityPerMeasuringUnit, recipeId);

            //Assert
            dataMock.Verify(x => x.Ingredients.Add(It.IsAny<Ingredient>()), Times.Once);
        }

        [Test]
        public void Throw_WhenThePassedIngredientIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            Ingredient ingredient = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => ingredientsService.AddIngredient(ingredient));
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedIngredientIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            Guid ingredientId = Guid.NewGuid();
            string ingredientName = "NameOfTheIngredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            string foodCategoryName = "FoodcategoryName";
            ICollection<Ingredient> foodcategoryIngredients = new List<Ingredient>();
            FoodCategory foodcategory = new FoodCategory()
            {
                Id = foodCategoryId,
                Name = foodCategoryName,
                Ingredients = foodcategoryIngredients
            };

            Guid recipeId = Guid.NewGuid();
            double quantityPerMeasuringUnit = 0.250;

            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                FoodCategoryId = foodCategoryId,
                FoodCategory = foodcategory,
                RecipeId = recipeId,
                QuantityInMeasuringUnit = quantityPerMeasuringUnit
            };

            dataMock.Setup(x => x.FoodCategories.GetById(foodCategoryId)).Returns(foodcategory);
            dataMock.Setup(x => x.Ingredients.Add(ingredient));

            //Act
            ingredientsService.AddIngredient(ingredient);

            //Assert
            dataMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void InvokeDataIngredientsAddOnce_WhenThePassedArgumentIngredientIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            Guid ingredientId = Guid.NewGuid();
            string ingredientName = "NameOfTheIngredient";
            decimal pricePerMeasuringUnit = 1.19m;
            Guid foodCategoryId = Guid.NewGuid();
            string foodCategoryName = "FoodcategoryName";
            ICollection<Ingredient> foodcategoryIngredients = new List<Ingredient>();
            FoodCategory foodcategory = new FoodCategory()
            {
                Id = foodCategoryId,
                Name = foodCategoryName,
                Ingredients = foodcategoryIngredients
            };

            Guid recipeId = Guid.NewGuid();
            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientId,
                Name = ingredientName,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                FoodCategoryId = foodCategoryId,
                FoodCategory = foodcategory,
                RecipeId = recipeId
            };

            dataMock.Setup(x => x.FoodCategories.GetById(foodCategoryId)).Returns(foodcategory);
            dataMock.Setup(x => x.Ingredients.Add(ingredient));

            //Act
            ingredientsService.AddIngredient(ingredient);

            //Assert
            dataMock.Verify(x => x.Ingredients.Add(It.IsAny<Ingredient>()), Times.Once);
        }
    }
}
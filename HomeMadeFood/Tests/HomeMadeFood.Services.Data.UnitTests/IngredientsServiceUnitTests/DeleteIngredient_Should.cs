using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data.UnitTests.IngredientsServiceUnitTests
{
    [TestFixture]
    public class DeleteIngredient_Should
    {
        [Test]
        public void Throw_WhenThePassedIngredientIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            dataMock.Setup(x => x.Ingredients.Delete(It.IsAny<Ingredient>()));

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => ingredientsService.DeleteIngredient(null));
        }

        [Test]
        public void InvokeDataIngredientsRepositoryMethodDeleteOnce_WhenThePassedIngredientIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            var foodCategoriesServiceMock = new Mock<IFoodCategoriesService>();
            IngredientsService ingredientsService = new IngredientsService(dataMock.Object, foodCategoriesServiceMock.Object);
            dataMock.Setup(x => x.Ingredients.Delete(It.IsAny<Ingredient>()));

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

            //Act
            ingredientsService.DeleteIngredient(ingredient);

            //Assert
            dataMock.Verify(x => x.Ingredients.Delete(ingredient), Times.Once);
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
            dataMock.Setup(x => x.Ingredients.Delete(ingredient));
            //Act
            ingredientsService.DeleteIngredient(ingredient);

            //Assert
            dataMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
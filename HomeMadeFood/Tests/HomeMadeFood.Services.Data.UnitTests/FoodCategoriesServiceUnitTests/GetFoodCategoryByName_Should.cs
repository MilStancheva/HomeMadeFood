using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.UnitTests.FoodCategoriesServiceUnitTests
{
    [TestFixture]
    public class GetFoodCategoryByName_Should
    {
        [Test]
        public void ShouldThrow_WhenNameParameterIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            string name = null;

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => foodCategoriesService.GetFoodCategoryByName(name));
        }

        [Test]
        public void ShouldThrow_WhenNameParameterIsEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            string name = string.Empty;

            //Act&Assert
            Assert.Throws<ArgumentException>(() => foodCategoriesService.GetFoodCategoryByName(name));
        }

        [Test]
        public void ReturnFoodCategory_WhenPassedNameIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            Guid foodcategoryId = Guid.NewGuid();
            string name = "FoodCategoryName";
            FoodCategory foodCategory = new FoodCategory()
            {
                Id = foodcategoryId,
                Name = name,
                FoodType = FoodType.Mushroom,
                MeasuringUnit = MeasuringUnitType.Kg,
            };
            var collection = new List<FoodCategory>() { foodCategory };

            dataMock.Setup(c => c.FoodCategories.All).Returns(() =>
            {
                return collection.AsQueryable();
            });

            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);

            //Act
            FoodCategory foodCategoryResult = foodCategoriesService.GetFoodCategoryByName(name);

            //Assert
            Assert.AreSame(foodCategory, foodCategoryResult);
        }

        [Test]
        public void ReturnNull_WhenPassedArgumentNameIsValidButThereIsNoSuchFoodCategoryInDatabase()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            string name = "FoodCategoryName";

            var collection = new List<FoodCategory>();

            dataMock.Setup(c => c.FoodCategories.All).Returns(() =>
            {
                return collection.AsQueryable();
            });

            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);

            //Act
            FoodCategory foodCategoryResult = foodCategoriesService.GetFoodCategoryByName(name);

            //Assert
            Assert.IsNull(foodCategoryResult);
        }
    }
}
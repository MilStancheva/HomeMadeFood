using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.UnitTests.FoodCategoriesServiceUnitTests
{
    [TestFixture]
    public class GetFoodCategoryById_Should
    {
        [Test]
        public void ShouldThrow_WhenGuidIdParameterIsEmpty()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => foodCategoriesService.GetFoodCategoryById(Guid.Empty));
        }

        [Test]
        public void ReturnFoodCategory_WhenIdIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            Guid foodcategoryId = Guid.NewGuid();
            FoodCategory foodCategory = new FoodCategory()
            {
                Id = foodcategoryId,
                Name = "FoodCategoryName",
                FoodType = FoodType.Mushroom,
                MeasuringUnit = MeasuringUnitType.Kg,
            };

            dataMock.Setup(c => c.FoodCategories.GetById(foodcategoryId)).Returns(foodCategory);

            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);

            //Act
            FoodCategory foodCategoryResult = foodCategoriesService.GetFoodCategoryById(foodcategoryId);

            //Assert
            Assert.AreSame(foodCategory, foodCategoryResult);
        }
    }
}
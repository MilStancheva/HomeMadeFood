using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.UnitTests.FoodCategoriesServiceUnitTests
{
    [TestFixture]
    public class AddFoodCategory_Should
    {
        [Test]
        public void Throw_WhenThePassedFoodCategoryIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            FoodCategory foodCategory = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => foodCategoriesService.AddFoodCategory(foodCategory));
        }

        [Test]
        public void InvokeDataCommitOnce_WhenThePassedArgumentIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            Guid foodcategoryId = Guid.NewGuid();
            string name = "FoodCategoryName";
            FoodCategory foodCategory = new FoodCategory()
            {
                Id = foodcategoryId,
                Name = name,
                FoodType = FoodType.Mushroom,
                MeasuringUnit = MeasuringUnitType.Kg,
            };

            dataMock.Setup(x => x.FoodCategories.Add(foodCategory));
            //Act
            foodCategoriesService.AddFoodCategory(foodCategory);

            //Assert
            dataMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void InvokeDataFoodCategoryAddOnce_WhenThePassedArgumentIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            Guid foodcategoryId = Guid.NewGuid();
            string name = "FoodCategoryName";
            FoodCategory foodCategory = new FoodCategory()
            {
                Id = foodcategoryId,
                Name = name,
                FoodType = FoodType.Mushroom,
                MeasuringUnit = MeasuringUnitType.Kg,
            };

            dataMock.Setup(x => x.FoodCategories.Add(foodCategory));

            //Act
            foodCategoriesService.AddFoodCategory(foodCategory);

            //Assert
            dataMock.Verify(x => x.FoodCategories.Add(foodCategory), Times.Once);
        }
    }
}
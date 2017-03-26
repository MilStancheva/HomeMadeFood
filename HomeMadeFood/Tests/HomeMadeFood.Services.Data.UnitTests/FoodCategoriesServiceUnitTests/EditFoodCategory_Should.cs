using System;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.UnitTests.FoodCategoriesServiceUnitTests
{
    [TestFixture]
    public class EditFoodCategory_Should
    {
        [Test]
        public void Throw_WhenThePassedFoodCategoryIsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            dataMock.Setup(x => x.FoodCategories.Update(It.IsAny<FoodCategory>()));

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => foodCategoriesService.EditFoodCategory(null));
        }

        [Test]
        public void InvokeDataFoodCategoryRepositoryMethodUpdateOnce_WhenThePassedFoodCategoryIsValid()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            dataMock.Setup(x => x.FoodCategories.Update(It.IsAny<FoodCategory>()));
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

            //Act
            foodCategoriesService.EditFoodCategory(foodCategory);

            //Assert
            dataMock.Verify(x => x.FoodCategories.Update(foodCategory), Times.Once);
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

            dataMock.Setup(x => x.FoodCategories.Update(foodCategory));
            //Act
            foodCategoriesService.EditFoodCategory(foodCategory);

            //Assert
            dataMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
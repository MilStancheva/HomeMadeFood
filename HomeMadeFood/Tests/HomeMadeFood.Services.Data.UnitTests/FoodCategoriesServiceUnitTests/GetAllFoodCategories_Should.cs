using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.UnitTests.FoodCategoriesServiceUnitTests
{
    [TestFixture]
    public class GetAllFoodCategories_Should
    {
        [Test]
        public void Invoke_TheDataFoodCategoriesRepositoryMethodGetAll_Once()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);
            dataMock.Setup(x => x.FoodCategories.GetAll());

            //Act
            IEnumerable<FoodCategory> foodCategories = foodCategoriesService.GetAllFoodCategories();

            //Assert
            dataMock.Verify(x => x.FoodCategories.GetAll(), Times.Once);
        }

        [Test]
        public void ReturnResult_WhenInvokingDataFoodCategoriesRepositoryMethod_GetAll()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IEnumerable<FoodCategory> expectedResultCollection = new List<FoodCategory>();

            dataMock.Setup(c => c.FoodCategories.GetAll()).Returns(() =>
            {
                return expectedResultCollection.AsQueryable();
            });

            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);

            //Act
            IEnumerable<FoodCategory> foodCategoriesResult = foodCategoriesService.GetAllFoodCategories();

            //Assert
            Assert.That(foodCategoriesResult, Is.EqualTo(expectedResultCollection));
        }

        [Test]
        public void ReturnResultOfCorrectTypeIEnumerableOfFoodCategory()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.FoodCategories.GetAll()).Returns(() =>
            {
                IEnumerable<FoodCategory> expectedResultCollection = new List<FoodCategory>();
                return expectedResultCollection.AsQueryable();
            });

            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);

            //Act
            IEnumerable<FoodCategory> foodCategoriesResult = foodCategoriesService.GetAllFoodCategories();

            //Assert
            Assert.That(foodCategoriesResult, Is.InstanceOf<IEnumerable<FoodCategory>>());
        }

        [Test]
        public void ReturnNull_WhenDataFoodCategoryReposityMethodGetAll_ReturnsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.FoodCategories.GetAll()).Returns(() =>
            {
                return null;
            });

            FoodCategoriesService foodCategoriesService = new FoodCategoriesService(dataMock.Object);

            //Act
            IEnumerable<FoodCategory> foodCategoriesResult = foodCategoriesService.GetAllFoodCategories();

            //Assert
            Assert.IsNull(foodCategoriesResult);
        }
    }
}
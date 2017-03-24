using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.UnitTests.UsersServiceUnitTests
{
    [TestFixture]
    public class GetAllUsers_Should
    {
        [Test]
        public void Invoke_TheDataUsersRepositoryMethodGetAll_Once()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            UsersService usersService = new UsersService(dataMock.Object);
            dataMock.Setup(x => x.Users.All);

            //Act
            IEnumerable<ApplicationUser> users = usersService.GetAllUsers();

            //Assert
            dataMock.Verify(x => x.Users.All, Times.Once);
        }

        [Test]
        public void ReturnResult_WhenInvokingDataUsersRepositoryMethod_GetAll()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IEnumerable<ApplicationUser> expectedResultCollection = new List<ApplicationUser>();

            dataMock.Setup(c => c.Users.All).Returns(() =>
            {
                return expectedResultCollection.AsQueryable();
            });

            UsersService usersService = new UsersService(dataMock.Object);

            //Act
            IEnumerable<ApplicationUser> usersResult = usersService.GetAllUsers();

            //Assert
            Assert.That(usersResult, Is.EqualTo(expectedResultCollection));
        }

        [Test]
        public void ReturnResultOfCorrectTypeIEnumerableOfApplicationUser()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.Users.All).Returns(() =>
            {
                IEnumerable<ApplicationUser> expectedResultCollection = new List<ApplicationUser>();
                return expectedResultCollection.AsQueryable();
            });

            UsersService usersService = new UsersService(dataMock.Object);

            //Act
            IEnumerable<ApplicationUser> recipesResult = usersService.GetAllUsers();

            //Assert
            Assert.That(recipesResult, Is.InstanceOf<IEnumerable<ApplicationUser>>());
        }

        [Test]
        public void ReturnNull_WhenDataUsersReposityMethodGetAll_ReturnsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.Users.All).Returns(() =>
            {
                return null;
            });

            UsersService usersService = new UsersService(dataMock.Object);

            //Act
            IEnumerable<ApplicationUser> usersResult = usersService.GetAllUsers();

            //Assert
            Assert.IsNull(usersResult);
        }
    }
}
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.UnitTests.UsersServiceUnitTests
{
    [TestFixture]
    public class GetAllUsersWithRoles_Should
    {
        [Test]
        public void Invoke_TheDataUsersRepositoryMethodGetAllIncluding_Once()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            UsersService usersService = new UsersService(dataMock.Object);
            dataMock.Setup(x => x.Users.GetAllIncluding(u => u.Roles));

            //Act
            IEnumerable<ApplicationUser> users = usersService.GetAllUsersWithRoles();

            //Assert
            dataMock.Verify(x => x.Users.GetAllIncluding(u => u.Roles), Times.Once);
        }

        [Test]
        public void ReturnResult_WhenInvokingDataUsersRepositoryMethod_GetAllIncluding()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();
            IEnumerable<ApplicationUser> expectedResultCollection = new List<ApplicationUser>();

            dataMock.Setup(c => c.Users.GetAllIncluding(u => u.Roles)).Returns(() =>
            {
                return expectedResultCollection.AsQueryable();
            });

            UsersService usersService = new UsersService(dataMock.Object);

            //Act
            IEnumerable<ApplicationUser> usersResult = usersService.GetAllUsersWithRoles();

            //Assert
            Assert.That(usersResult, Is.EqualTo(expectedResultCollection));
        }

        [Test]
        public void ReturnResultOfCorrectTypeIEnumerableOfApplicationUser()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.Users.GetAllIncluding(u => u.Roles)).Returns(() =>
            {
                IEnumerable<ApplicationUser> expectedResultCollection = new List<ApplicationUser>();
                return expectedResultCollection.AsQueryable();
            });

            UsersService usersService = new UsersService(dataMock.Object);

            //Act
            IEnumerable<ApplicationUser> recipesResult = usersService.GetAllUsersWithRoles();

            //Assert
            Assert.That(recipesResult, Is.InstanceOf<IEnumerable<ApplicationUser>>());
        }

        [Test]
        public void ReturnNull_WhenDataUsersReposityMethodGetAllIncluding_ReturnsNull()
        {
            //Arrange
            var dataMock = new Mock<IHomeMadeFoodData>();

            dataMock.Setup(c => c.Users.GetAllIncluding(u => u.Roles)).Returns(() =>
            {
                return null;
            });

            UsersService usersService = new UsersService(dataMock.Object);

            //Act
            IEnumerable<ApplicationUser> usersResult = usersService.GetAllUsersWithRoles();

            //Assert
            Assert.IsNull(usersResult);
        }
    }
}
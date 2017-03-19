using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using HomeMadeFood.Web.Areas.Admin.Models;

namespace HomeMadeFood.Controllers.UnitTests.UsersControllerUnitTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void RenderTheRightPartialView()
        {
            //Arrange
            var usersServiceMock = new Mock<IUsersService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new UsersController(usersServiceMock.Object, mappingServiceMock.Object);
            string username = "pesho@pesho.com";

            //Act & Assert
            controller.WithCallTo(x => x.Search(username))
                .ShouldRenderPartialView("_UsersGridPartial");
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchUserViewModelAndNoModelErrors()
        {
            //Arrange
            var usersServiceMock = new Mock<IUsersService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new UsersController(usersServiceMock.Object, mappingServiceMock.Object);
            string username = "pesho@pesho.com";

            //Act & Assert
            controller.WithCallTo(x => x.Search(username))
                .ShouldRenderPartialView("_UsersGridPartial")
                .WithModel<SearchUserViewModel>()
                .AndNoModelErrors();
        }

        [Test]
        public void RenderTheRightPartialViewWithTheCorrectModel_SearchUserViewModelAndNoModelErrorsAndCorrectContent()
        {
            //Arrange 
            var usersServiceMock = new Mock<IUsersService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new UsersController(usersServiceMock.Object, mappingServiceMock.Object);
            string id = Guid.NewGuid().ToString();

            var user = new UserViewModel()
            {
                Id = id,
                Username = "pesho@pesho.com",
                Email = "pesho@pesho.com",
                Role = "User"
            };

            var users = new List<UserViewModel>() { user };

            var searchModel = new SearchUserViewModel()
            {
                PageSize = 5,
                TotalRecords = users.Count,
                Users = users
            };
            string username = "pesho@pesho.com";

            //Act & Assert
            controller.WithCallTo(x => x.Search(username))
                .ShouldRenderPartialView("_UsersGridPartial")
                .WithModel<SearchUserViewModel>(
                    viewModel => Assert.AreEqual(username, searchModel.Users.ToList()[0].Username))
                .AndNoModelErrors();
        }
    }
}

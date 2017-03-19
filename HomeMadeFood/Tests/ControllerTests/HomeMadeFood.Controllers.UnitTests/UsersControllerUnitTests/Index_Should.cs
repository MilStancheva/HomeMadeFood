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
    public class Index_Should
    {
        [Test]
        public void RenderTheRightView()
        {
            //Arrange
            var usersServiceMock = new Mock<IUsersService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new UsersController(usersServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderView("Index");
        }

        [Test]
        public void RenderTheRightViewWithTheCorrectModel_SearchUsersViewModelAndNoModelErrors()
        {
            //Arrange
            var usersServiceMock = new Mock<IUsersService>();
            var mappingServiceMock = new Mock<IMappingService>();
            var controller = new UsersController(usersServiceMock.Object, mappingServiceMock.Object);

            //Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<SearchUserViewModel>()
                .AndNoModelErrors();
        }

        [Test]
        public void RenderTheRightViewWithTheCorrectModel_SearchUserViewModelAndNoModelErrorsAndCorrectContent()
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

            //Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<SearchUserViewModel>(
                    viewModel => Assert.AreEqual(users.ToList()[0].Username, searchModel.Users.ToList()[0].Username))
                .AndNoModelErrors();
        }
    }
}
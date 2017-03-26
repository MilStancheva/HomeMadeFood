﻿using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Controllers;

namespace HomeMadeFood.Controllers.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class ContactShould
    {
        [Test]
        public void ReturnActionResultThatIsNotNull()
        {
            //Arrange
            var dailyMenuServiceMock = new Mock<IDailyMenuService>();
            var mappringServiceMock = new Mock<IMappingService>();
            HomeController homeController = new HomeController(dailyMenuServiceMock.Object, mappringServiceMock.Object);

            //Act
            ViewResult result = homeController.Contact() as ViewResult;

            //Assert

            Assert.IsNotNull(result);
        }

        [Test]
        public void AddTheRightMessageInTheViewBag()
        {
            //Arrange
            var dailyMenuServiceMock = new Mock<IDailyMenuService>();
            var mappringServiceMock = new Mock<IMappingService>();
            HomeController homeController = new HomeController(dailyMenuServiceMock.Object, mappringServiceMock.Object);

            //Act
            ViewResult result = homeController.Contact() as ViewResult;

            //Assert
            Assert.AreEqual("Contact", result.ViewBag.Message);
        }
    }
}
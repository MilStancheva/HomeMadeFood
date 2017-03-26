using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data.Contracts;
using HomeMadeFood.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeMadeFood.Controllers.UnitTests.AdminControllerUnitTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnActionResultThatIsNotNull()
        {
            //Arrange
            var dailyMenusServiceMock = new Mock<IDailyMenuService>();
            var mappingServiceMock = new Mock<IMappingService>();
            AdminController adminController = new AdminController(dailyMenusServiceMock.Object, mappingServiceMock.Object);

            //Act
            ViewResult result = adminController.Index() as ViewResult;

            //Assert

            Assert.IsNotNull(result);
        }
    }
}

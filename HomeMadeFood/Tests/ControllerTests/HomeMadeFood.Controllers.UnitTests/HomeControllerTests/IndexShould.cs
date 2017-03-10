using HomeMadeFood.Web.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeMadeFood.Controllers.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void ReturnActionResultThatIsNotNull()
        {
            //Arrange
            HomeController homeController = new HomeController();

            //Act
            ViewResult result = homeController.Index() as ViewResult;

            //Assert

            Assert.IsNotNull(result);
        }
    }
}

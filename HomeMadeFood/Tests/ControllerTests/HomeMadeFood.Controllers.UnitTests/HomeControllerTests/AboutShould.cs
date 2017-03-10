using HomeMadeFood.Web.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace HomeMadeFood.Controllers.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class AboutShould
    {
        [Test]
        public void AddTheRightMessageInTheViewBag()
        {
            //Arrange
            HomeController homeController = new HomeController();

            //Act
            ViewResult result = homeController.About() as ViewResult;

            //Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}

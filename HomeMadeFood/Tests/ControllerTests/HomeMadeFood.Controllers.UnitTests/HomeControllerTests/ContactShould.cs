using HomeMadeFood.Web.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace HomeMadeFood.Controllers.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class ContactShould
    {
        [Test]
        public void ReturnActionResultThatIsNotNull()
        {
            //Arrange
            HomeController homeController = new HomeController();

            //Act
            ViewResult result = homeController.Contact() as ViewResult;

            //Assert

            Assert.IsNotNull(result);
        }

        [Test]
        public void AddTheRightMessageInTheViewBag()
        {
            //Arrange
            HomeController homeController = new HomeController();

            //Act
            ViewResult result = homeController.Contact() as ViewResult;

            //Assert
            Assert.AreEqual("Your contact page.", result.ViewBag.Message);
        }
    }
}
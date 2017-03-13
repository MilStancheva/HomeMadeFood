using HomeMadeFood.Web.Areas.Admin.Controllers;
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
            AdminController adminController = new AdminController();

            //Act
            ViewResult result = adminController.Index() as ViewResult;

            //Assert

            Assert.IsNotNull(result);
        }
    }
}

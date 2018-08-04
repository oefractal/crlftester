using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrLfTester;
using CrLfTester.Controllers;

namespace CrLfTester.Tests.Controllers
{
  [TestClass]
  public class HomeControllerTest
  {
    [TestMethod]
    public void Index()
    {
      // Arrange
      CrLfController controller = new CrLfController();

      // Act
      ViewResult result = controller.Index() as ViewResult;

      // Assert
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void About()
    {
      // Arrange
      CrLfController controller = new CrLfController();

      // Act
      ViewResult result = controller.About() as ViewResult;

      // Assert
      Assert.AreEqual("Your application description page.", result.ViewBag.Message);
    }

    [TestMethod]
    public void Contact()
    {
      // Arrange
      CrLfController controller = new CrLfController();

      // Act
      ViewResult result = controller.Contact() as ViewResult;

      // Assert
      Assert.IsNotNull(result);
    }
  }
}

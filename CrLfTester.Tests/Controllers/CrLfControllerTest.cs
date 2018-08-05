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
  /// <summary>
  /// Тест контроллера.
  /// </summary>
  [TestClass]
  public class CrLfControllerTest
  {
    /// <summary>
    /// Тест метода контроллера, возвращающего главное представление.
    /// </summary>
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
  }
}

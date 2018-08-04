using CrLfTester.Classes;
using System.Web.Mvc;

namespace CrLfTester.Controllers
{
  public class CrLfController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult CheckFolder(string folderName)
    {
      var folderChecker = new FolderChecker(folderName);
      var checkResults = folderChecker.CheckFolder();
      return this.View(checkResults);
    }
  }
}
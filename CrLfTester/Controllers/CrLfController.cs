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

    public ActionResult CheckFolder(string folderName, string filter)
    {
      var folderChecker = new FolderChecker(folderName, filter);
      var checkResults = folderChecker.CheckFolder();
      return this.View(checkResults);
    }
  }
}
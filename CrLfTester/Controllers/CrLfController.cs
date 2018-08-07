using CrLfTester.Classes;
using System;
using System.Web.Mvc;

namespace CrLfTester.Controllers
{
  /// <summary>
  /// Контроллер символов перевода строк.
  /// </summary>
  public class CrLfController : Controller
  {
    /// <summary>
    /// Главное представление.
    /// </summary>
    /// <returns>Главное представление.</returns>
    public ActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// Проверить символ перевода строки в папке.
    /// </summary>
    /// <param name="folderName">Имя папки.</param>
    /// <param name="filter">Фильтр по именам файлов.</param>
    /// <returns>Представление с результатами проверки.</returns>
    public ActionResult CheckFolder(string folderName, string filter)
    {
      try
      {
        var folderChecker = new FolderChecker(folderName, filter);
        var checkResults = folderChecker.CheckFolder();
        return this.View(checkResults);
      }
      catch (Exception e)
      {
        return this.View("CheckFolderError", (object)e.Message);
      }
    }

    /// <summary>
    /// Изменить тип символа перевода строки в файле на заданный.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="mode">Целевой тип символа перевода строки.</param>
    /// <returns>Результат выполнения операции.</returns>
    [HttpPost]
    public ActionResult ChangeFileLineEnding(string fileName, CrLfMode mode)
    {
      var changer = new LineEndingChanger(fileName, mode);
      changer.Execute();
      return new HttpStatusCodeResult(200);
    }
  }
}
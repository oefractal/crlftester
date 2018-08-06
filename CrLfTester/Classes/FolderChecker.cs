using System.Collections.Generic;
using System.IO;

namespace CrLfTester.Classes
{
  /// <summary>
  /// Класс для проверки символов перевода строки в файлах папки.
  /// </summary>
  public class FolderChecker
  {
    /// <summary>
    /// Имя папки.
    /// </summary>
    private string folderName;

    /// <summary>
    /// Фильтр.
    /// </summary>
    private string filter;

    /// <summary>
    /// Проверить файл.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Результат проверки файла.</returns>
    public CheckResult CheckFile(string fileName)
    {
      var result = new CheckResult() { FileName = fileName };
      var checker = new LineEndingChecker(fileName);
      result.CrLfMode = checker.Check();
      return result;
    }

    /// <summary>
    /// Проверить файлы в папке.
    /// </summary>
    /// <returns>Список с результатами проверки для каждого файла в папке.</returns>
    public IEnumerable<CheckResult> CheckFolder()
    {
      var result = new List<CheckResult>();
      foreach (var fileName in Directory.EnumerateFiles(this.folderName, this.filter, SearchOption.AllDirectories))
        result.Add(this.CheckFile(fileName));
      return result;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="folderName">Имя папки.</param>
    /// <param name="filter">Фильтр.</param>
    public FolderChecker(string folderName, string filter)
    {
      this.folderName = folderName;
      this.filter = filter;
    }
  }
}
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
    /// Код символа перевода строки.
    /// </summary>
    private const int CarriageReturnByte = 13;

    /// <summary>
    /// Код символа возврата каретки.
    /// </summary>
    private const int LineFeedByte = 10;

    /// <summary>
    /// Имя папки.
    /// </summary>
    private string folderName;

    /// <summary>
    /// Изменить задетекченный режим перевода строки с учетом текущей строки.
    /// </summary>
    /// <param name="prevMode">Затетекченный ранее режим.</param>
    /// <param name="currentMode">Текущий режим.</param>
    /// <returns>Результирующий режим.</returns>
    private CrLfMode ChangeDetectedMode(CrLfMode prevMode, CrLfMode currentMode)
    {
      if (prevMode == CrLfMode.Unknown)
        return currentMode;
      else if (prevMode == currentMode)
        return currentMode;
      else
        return CrLfMode.Mixed;
    }

    /// <summary>
    /// Проверить файл.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Результат проверки файла.</returns>
    private CheckResult CheckFile(string fileName)
    {
      var result = new CheckResult() { FileName = fileName };
      var detectedMode = CrLfMode.Unknown;
      using (var fileStream = new FileStream(fileName, FileMode.Open))
      {
        using (var streamReader = new StreamReader(fileStream))
        {
          var prevPrevByte = -1;
          var prevByte = -1;
          do
          {
            var currentByte = streamReader.Read();
            if (currentByte != CarriageReturnByte && currentByte != LineFeedByte)
            {
              if (prevByte == CarriageReturnByte)
                detectedMode = this.ChangeDetectedMode(detectedMode, CrLfMode.Cr);
              else if (prevByte == LineFeedByte)
              {
                if (prevPrevByte == CarriageReturnByte)
                  detectedMode = this.ChangeDetectedMode(detectedMode, CrLfMode.CrLf);
                else
                  detectedMode = this.ChangeDetectedMode(detectedMode, CrLfMode.Lf);
              }
            }
            prevPrevByte = prevByte;
            prevByte = currentByte;
          }
          while (streamReader.Peek() >= 0);
        }
      }
      result.CrLfMode = detectedMode;
      return result;
    }

    /// <summary>
    /// Проверить файлы в папке.
    /// </summary>
    /// <returns>Список с результатами проверки для каждого файла в папке.</returns>
    public IEnumerable<CheckResult> CheckFolder()
    {
      var result = new List<CheckResult>();
      foreach (var fileName in Directory.EnumerateFiles(this.folderName, "*", SearchOption.AllDirectories))
        result.Add(this.CheckFile(fileName));
      return result;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="folderName">Имя папки.</param>
    public FolderChecker(string folderName)
    {
      this.folderName = folderName;
    }
  }
}
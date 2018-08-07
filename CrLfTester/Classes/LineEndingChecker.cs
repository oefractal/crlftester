using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CrLfTester.Classes
{
  /// <summary>
  /// Классс для проверки символа перевода строки в файле.
  /// </summary>
  public class LineEndingChecker
  {
    /// <summary>
    /// Имя файла.
    /// </summary>
    private string fileName;

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
    /// Проверить символ перевода строки в файле.
    /// </summary>
    /// <returns></returns>
    public CrLfMode Check()
    {
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
            if (currentByte != LineEndingConsts.CarriageReturnByte && currentByte != LineEndingConsts.LineFeedByte)
            {
              if (prevByte == LineEndingConsts.CarriageReturnByte)
                detectedMode = this.ChangeDetectedMode(detectedMode, CrLfMode.Cr);
              else if (prevByte == LineEndingConsts.LineFeedByte)
              {
                if (prevPrevByte == LineEndingConsts.CarriageReturnByte)
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
      return detectedMode;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    public LineEndingChecker(string fileName)
    {
      this.fileName = fileName;
    }
  }
}
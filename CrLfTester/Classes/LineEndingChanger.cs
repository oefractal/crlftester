using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CrLfTester.Classes
{
  /// <summary>
  /// Класс для изменения типа символа перевода строки в файле.
  /// </summary>
  public class LineEndingChanger
  {
    /// <summary>
    /// Имя файла.
    /// </summary>
    private string fileName;

    /// <summary>
    /// Целевой тип символа перевода строки.
    /// </summary>
    private CrLfMode targetMode;

    /// <summary>
    /// Выполнить изменение.
    /// </summary>
    public void Execute()
    {
      var tempFileName = Path.GetTempFileName();
      try
      {
        using (var sourceStream = new FileStream(this.fileName, FileMode.Open))
        {
          using (var sourceReader = new StreamReader(sourceStream))
          {
            using (var destFileStream = new FileStream(tempFileName, FileMode.Open))
            {
              using (var destWriter = new StreamWriter(destFileStream))
              {
                var prevPrevByte = -1; 
                var prevByte = -1;
                while (sourceReader.Peek() != -1)
                {
                  var currentByte = sourceReader.Read();
                  if (currentByte != LineEndingConsts.CarriageReturnByte &&
                      currentByte != LineEndingConsts.LineFeedByte)
                    destWriter.Write(currentByte);
                  prevPrevByte = prevByte;
                  prevByte = currentByte;
                }
              }
            }
          }
        }
        File.Copy(tempFileName, this.fileName, true);
      }
      finally
      {
        File.Delete(tempFileName);
      }
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="targetMode">Целевой тип символа перевода строки.</param>
    public LineEndingChanger(string fileName, CrLfMode targetMode)
    {
      this.fileName = fileName;
      this.targetMode = targetMode;
    }
  }
}
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
    /// Записать целевой тип символа перевода строки.
    /// </summary>
    /// <param name="writer">Объект для записи в поток.</param>
    private void WriteTargetLineEnding(StreamWriter writer)
    {
      var destBytes = LineEndingTools.GetLineEndingBytes(targetMode);
      foreach (var destByte in destBytes)
        writer.Write((char)destByte);
    }

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
                  {
                    if (prevByte == LineEndingConsts.CarriageReturnByte ||
                       (prevPrevByte != LineEndingConsts.CarriageReturnByte &&
                        prevByte == LineEndingConsts.LineFeedByte))
                      this.WriteTargetLineEnding(destWriter);
                    destWriter.Write((char)currentByte);
                  }
                  else
                  {
                    if ((prevByte == LineEndingConsts.CarriageReturnByte &&
                        currentByte == LineEndingConsts.LineFeedByte))
                    {
                      this.WriteTargetLineEnding(destWriter);
                      currentByte = -1;
                    }
                    if ((prevByte == LineEndingConsts.CarriageReturnByte &&
                        currentByte == LineEndingConsts.CarriageReturnByte) ||
                        (prevByte == LineEndingConsts.LineFeedByte &&
                         currentByte == LineEndingConsts.LineFeedByte) ||
                        (prevByte == LineEndingConsts.LineFeedByte &&
                         currentByte == LineEndingConsts.CarriageReturnByte))
                      this.WriteTargetLineEnding(destWriter);
                  }
                  prevByte = currentByte;
                }
                if (prevByte == LineEndingConsts.CarriageReturnByte ||
                   (prevPrevByte != LineEndingConsts.CarriageReturnByte &&
                    prevByte == LineEndingConsts.LineFeedByte))
                  this.WriteTargetLineEnding(destWriter);
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
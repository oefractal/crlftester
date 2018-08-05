using System;
using System.Collections.Generic;
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
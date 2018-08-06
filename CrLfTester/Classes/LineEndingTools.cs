using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrLfTester.Classes
{
  /// <summary>
  /// Класс вспомогательных функций для работы с переводами строк.
  /// </summary>
  public static class LineEndingTools
  {
    /// <summary>
    /// Получит ьмассив байт с символами перевода строки.
    /// </summary>
    /// <param name="mode">Режим перевода строки.</param>
    /// <returns>Массив байт с символами перевода строки.</returns>
    public static byte[] GetLineEndingBytes(CrLfMode mode)
    {
      switch (mode)
      {
        case CrLfMode.Cr:
          return new byte[] { 13 };
        case CrLfMode.Lf:
          return new byte[] { 10 };
        case CrLfMode.CrLf:
          return new byte[] { 13, 10 };
        default:
          throw new Exception("Передан неверный тип символа перевода строки.");
      }
    }
  }
}
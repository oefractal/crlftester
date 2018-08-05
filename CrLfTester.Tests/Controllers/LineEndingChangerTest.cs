using System;
using System.IO;
using System.Text;
using CrLfTester.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrLfTester.Tests.Controllers
{
  /// <summary>
  /// Тест класса, заменяющего символ перевода строки в файле.
  /// </summary>
  [TestClass]
  public class LineEndingChangerTest
  {
    /// <summary>
    /// Получит ьмассив байт с символами перевода строки.
    /// </summary>
    /// <param name="mode">Режим перевода строки.</param>
    /// <returns>Массив байт с символами перевода строки.</returns>
    private byte[] GetLineEndingBytes(CrLfMode mode)
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

    /// <summary>
    /// Создать исходный файл.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="mode">Режим символа перевода строки.</param>
    private void CreateSourceFile(out string fileName, CrLfMode mode)
    {
      var sourceStrings = new string[] { "abcd", "efgh", "jklm" };
      fileName = Path.GetTempFileName();
      using (var fileStream = new FileStream(fileName, FileMode.Open))
      {
        foreach (var sourceString in sourceStrings)
        {
          var bytes = Encoding.UTF8.GetBytes(sourceString);
          fileStream.Write(bytes, 0, bytes.Length);
          var lineEndingBytes = this.GetLineEndingBytes(mode);
          fileStream.Write(lineEndingBytes, 0, lineEndingBytes.Length);
        }
      }
    }

    /// <summary>
    /// Протестировать замену символа перевода строки.
    /// </summary>
    /// <param name="fromMode">Тип перевода строки в исходном файле.</param>
    /// <param name="toMode">Тип перевода строки в целевом файле.</param>
    public void TestChanging(CrLfMode fromMode, CrLfMode toMode)
    {
      string sourceFileName;
      this.CreateSourceFile(out sourceFileName, fromMode);
      try
      {
        var changer = new LineEndingChanger(sourceFileName, toMode);
        changer.Execute();
        var checker = new LineEndingChecker(sourceFileName);
        var resultMode = checker.Check();
        Assert.AreEqual(toMode, resultMode);
      }
      finally
      {
        File.Delete(sourceFileName);
      }
    }

    /// <summary>
    /// Протестировать замену CR/LF на CR.
    /// </summary>
    [TestMethod]
    public void TestCrLfToCr()
    {
      this.TestChanging(CrLfMode.CrLf, CrLfMode.Cr);
    }

    /// <summary>
    /// Протестировать замену CR/LF на LF.
    /// </summary>
    [TestMethod]
    public void TestCrLfToLf()
    {
      this.TestChanging(CrLfMode.CrLf, CrLfMode.Lf);
    }

    /// <summary>
    /// Протестировать замену CR на CR/LF.
    /// </summary>
    [TestMethod]
    public void TestCrToCrLf()
    {
      this.TestChanging(CrLfMode.Cr, CrLfMode.CrLf);
    }

    /// <summary>
    /// Протестировать замену CR на LF.
    /// </summary>
    [TestMethod]
    public void TestCrToLf()
    {
      this.TestChanging(CrLfMode.Cr, CrLfMode.Lf);
    }

    /// <summary>
    /// Протестировать замену LF на CR.
    /// </summary>
    [TestMethod]
    public void TestLfToCr()
    {
      this.TestChanging(CrLfMode.Lf, CrLfMode.Cr);
    }

    /// <summary>
    /// Протестировать замену LF на CR/LF.
    /// </summary>
    [TestMethod]
    public void TestLfToCrLf()
    {
      this.TestChanging(CrLfMode.Lf, CrLfMode.CrLf);
    }
  }
}

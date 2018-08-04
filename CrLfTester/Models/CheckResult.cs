namespace CrLfTester.Classes
{
  /// <summary>
  /// Символы перевода строки в файле.
  /// </summary>
  public enum CrLfMode
  {
    /// <summary>
    /// Неизвестно.
    /// </summary>
    Unknown,
    /// <summary>
    /// Символ перевода строки (CR).
    /// </summary>
    Cr,
    /// <summary>
    /// Символ возврата каретки (LF).
    /// </summary>
    Lf,
    /// <summary>
    /// Символ перевода строки и возврата картеки (CR LF).
    /// </summary>
    CrLf,
    /// <summary>
    /// Смешанное значение.
    /// </summary>
    Mixed
  }

  /// <summary>
  /// Класс с результатом проверки файла.
  /// </summary>
  public class CheckResult
  {
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Тип символов перевода строки в файле.
    /// </summary>
    public CrLfMode CrLfMode { get; set; }
  }
}
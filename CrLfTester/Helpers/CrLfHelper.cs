using CrLfTester.Classes;

namespace CrLfTester.Helpers
{
  public static class CrLfHelper
  {
    public static string CrLfModeToString(CrLfMode mode)
    {
      switch (mode)
      {
        case CrLfMode.Cr:
          return "CR";
        case CrLfMode.Lf:
          return "LF";
        case CrLfMode.CrLf:
          return "CR/LF";
        case CrLfMode.Mixed:
          return "Смешанный";
        case CrLfMode.Unknown:
          return "Неизвестный";
        default:
          return "Ошибка";
      }
    }
  }
}
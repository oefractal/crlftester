using System.Web;
using System.Web.Optimization;

namespace CrLfTester
{
  public class BundleConfig
  {
    // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
        "~/Scripts/jquery-{version}.js"));
      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
        "~/Scripts/jquery.validate*"));
      bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
        "~/Scripts/index.js"));
      bundles.Add(new ScriptBundle("~/bundles/jsgrid").Include(
        "~/Scripts/jsgrid.min.js"));
      bundles.Add(new StyleBundle("~/bundles/jsgridstyles").Include(
        "~/Content/jsgrid.min.css",
        "~/Content/jsgrid-theme.min.css"));
      bundles.Add(new StyleBundle("~/bundles/styles").Include(
        "~/Content/index.css"));
    }
  }
}

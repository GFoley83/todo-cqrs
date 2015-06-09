using System.Web.Optimization;

namespace TodoCqrs.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Lib/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Lib/bootstrap.js",
                      "~/Scripts/Lib/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            var angularBundle = new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/Lib/angular.js")
                .Include("~/Scripts/Lib/angular-resource.js");
            
            bundles.Add(angularBundle);

            var appBundle = new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/App/app.js")
                .Include("~/Scripts/App/task.js");

            bundles.Add(appBundle);
        }
    }
}

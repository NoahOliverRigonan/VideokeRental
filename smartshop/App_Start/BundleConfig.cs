using System.Web;
using System.Web.Optimization;

namespace VideokeRental
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // ================
            // CSS Style Sheets
            // ================
            bundles.Add(new StyleBundle("~/Content/stylesheets").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css",
                      "~/Content/jquery.dataTables.css",
                      "~/Content/jquery.dataTables.min.css"
                      ));

            // ===================
            // Scripts JavaScripts
            // ===================
            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                     "~/Scripts/bootstrap.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/jquery.min.js",
                     //"~/Scripts/jquery-1.10.2.js",
                     "~/Scripts/carousel.js",
                     "~/Scripts/collapse.js",
                     "~/Scripts/dropdown.js",
                     "~/Scripts/transition.js",
                     "~/Scripts/popover.js",
                     "~/Scripts/modal.js",
                     "~/Scripts/jquery.dataTables.js",
                     "~/Scripts/jquery.dataTables.min.js"
                     ));

            bundles.Add(new StyleBundle("~/font-awesome/fonts").Include(
                      "~/font-awesome/css/font-awesome.css",
                      "~/font-awesome/css/font-awesome.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

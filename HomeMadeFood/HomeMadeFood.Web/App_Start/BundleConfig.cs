using System.Web.Optimization;

namespace HomeMadeFood.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            "~/Content/themes/base/all.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/materialize.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/materialize.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/sandstone.bootstrap.min.css",
                      "~/Content/footer-distributed-with-contact-form.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/toastr", "http://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css")
                 .Include("~/Content/toastr.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr", "http://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js")
                            .Include("~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminindexpage").Include(
                     "~/Scripts/Custom/bootstrap.min.js",
                     "~/Scripts/Custom/custom.js",
                     "~/Scripts/Custom/searchform-submit.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminaddpage").Include(
                    "~/Scripts/Custom/bootstrap.min.js",
                    "~/Scripts/Custom/custom.js",
                    "~/Scripts/Custom/get-foodcategories.js",
                    "~/Scripts/Custom/add-ingredients-autocomplete.js"));
        }
    }
}
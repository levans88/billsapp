using System.Web;
using System.Web.Optimization;

namespace billsapp {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {

            // Example (true = include sub folders)
            // bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory(
                        //"~/Scripts", "*.js", true));
            
            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jqueryjs").Include(
                        "~/assets/vendor/jquery/jquery.js"));
            
            // jQuery Validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryvaljs").Include(
                        "~/assets/vendor/jquery-validation/jquery.validate.js",
                        "~/assets/vendor/jquery-validation/additional-methods.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            // jQuery UI
            bundles.Add(new ScriptBundle("~/bundles/jqueryuijs").Include(
                        "~/assets/vendor/jquery-ui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/bundles/jqueryuicss").IncludeDirectory(
                        "~/assets/vendor/jquery-ui", "*.css", true));

            // Nanoscroller
            bundles.Add(new ScriptBundle("~/bundles/nanoscrollerjs").Include(
                        "~/assets/vendor/nanoscroller/nanoscroller.js"));

            bundles.Add(new StyleBundle("~/bundles/nanoscrollercss").Include(
                        "~/assets/vendor/nanoscroller/nanoscroller.css"));

            // Modernizr
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizrjs").Include(
                        "~/assets/vendor/modernizr/modernizr.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/assets/vendor/bootstrap/js/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapcss").Include(
                        "~/assets/vendor/bootstrap/css/bootstrap.css"
                        //"~/assets/vendor/bootstrap/css/bootstrap-theme.css"       // default bootstrap theme, DO NOT USE
                        ));
            
            // Font Awesome
            bundles.Add(new StyleBundle("~/bundles/fontawesomecss").Include(
                        "~/assets/vendor/font-awesome/css/font-awesome.css"));
            
            // Theme
            bundles.Add(new StyleBundle("~/bundles/themecss").Include(
                        "~/assets/stylesheets/theme.css",                           // Basic layout structure styles
                        "~/assets/stylesheets/skins/default.css",                   // Skin
                        "~/assets/stylesheets/skins/extension.css",
                        "~/assets/stylesheets/skins/square-borders.css",
                        "~/assets/stylesheets/theme-custom.css"));                  // Customize the theme in this file

            bundles.Add(new ScriptBundle("~/bundles/themejs").Include(
                        "~/assets/javascripts/theme.js",
                        "~/assets/javascripts/theme.custom.js",
                        "~/assets/javascripts/theme.init.js"));
        }
    }
}

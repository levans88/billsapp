using System.Web;
using System.Web.Optimization;

namespace billsapp {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {

            // Note:
            // Bundles need to be consolidated further once functionality / dependency is verified.

            // Example (true = include sub folders)
            // bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory(
            //"~/Scripts", "*.js", true));

            // GitHub Activity Stream
            bundles.Add(new ScriptBundle("~/bundles/githubactivityjs").Include(
            "~/assets/vendor/github-activity/mustache.js/mustache.js",
            "~/assets/vendor/github-activity/github-activity.js"));

            bundles.Add(new StyleBundle("~/bundles/githubactivitycss").Include(
                        "~/assets/vendor/github-activity/github-activity.css",
                        "~/assets/vendor/github-activity/octicons/octicons.css"));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jqueryjs").Include(
                        "~/assets/vendor/jquery/jquery.js"));
            
            // jQuery Browser Mobile
            bundles.Add(new ScriptBundle("~/bundles/jquerybrowsermobilejs").Include(
                        "~/assets/vendor/jquery-browser-mobile/jquery.browser.mobile.js"));

            // jQuery Validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryvaljs").Include(
                        "~/assets/vendor/jquery-validation/jquery.validate.js"));

            // jQuery UI
            bundles.Add(new ScriptBundle("~/bundles/jqueryuijs").Include(
                        "~/assets/vendor/jquery-ui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/bundles/jqueryuicss").Include(
                        "~/assets/vendor/jquery-ui/jquery-ui.css",
                        "~/assets/vendor/jquery-ui/jquery-ui.theme.css"));

            // Nanoscroller
            bundles.Add(new ScriptBundle("~/bundles/nanoscrollerjs").Include(
                        "~/assets/vendor/nanoscroller/nanoscroller.js"));

            //bundles.Add(new StyleBundle("~/bundles/nanoscrollercss").Include(
            //            "~/assets/vendor/nanoscroller/nanoscroller.css"));        // Already included in theme (or at least not separately loaded)

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

            // pnotify
            bundles.Add(new ScriptBundle("~/bundles/pnotifyjs").Include(
                        "~/assets/vendor/pnotify/pnotify.custom.js"));

            bundles.Add(new StyleBundle("~/bundles/pnotifycss").Include(
                        "~/assets/vendor/pnotify/pnotify.custom.css"));

            // Magnific Popup
            bundles.Add(new ScriptBundle("~/bundles/magnificjs").Include(
                        "~/assets/vendor/magnific-popup/jquery.magnific-popup.js"));

            bundles.Add(new StyleBundle("~/bundles/magnificcss").Include(
                        "~/assets/vendor/magnific-popup/magnific-popup.css"));

            // jQuery Placeholder
            bundles.Add(new ScriptBundle("~/bundles/jqueryplaceholderjs").Include(
                        "~/assets/vendor/jquery-placeholder/jquery-placeholder.js"));

            // Bootstrap Datepicker
            bundles.Add(new ScriptBundle("~/bundles/bootstrapdatepickerjs").Include(
                        "~/assets/vendor/bootstrap-datepicker/js/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapdatepickercss").Include(
                        "~/assets/vendor/bootstrap-datepicker/css/bootstrap-datepicker3.css"));

            // jQuery UI Touch Punch
            bundles.Add(new ScriptBundle("~/bundles/jqueryuitouchpunchjs").Include(
                        "~/assets/vendor/jqueryui-touch-punch/jqueryui-touch-punch.js"));

            // jQuery Appear
            bundles.Add(new ScriptBundle("~/bundles/jqueryappearjs").Include(
                        "~/assets/vendor/jquery-appear/jquery-appear.js"));

            // Bootstrap Multiselect
            bundles.Add(new ScriptBundle("~/bundles/bootstrapmultiselectjs").Include(
                        "~/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapmultiselectcss").Include(
                        "~/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.css"));

            // Snap SVG
            bundles.Add(new ScriptBundle("~/bundles/snapsvgjs").Include(
                        "~/assets/vendor/snap.svg/snap.svg.js"));

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

using System.Web.Optimization;
using TIO.Core.Models;

namespace TIO.Core
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(Constants.Js.General).Include(
                "~/Scripts/Site.js",
                "~/Scripts/addtohomescreen.js"
                ));

            bundles.Add(new ScriptBundle(Constants.Js.Locations).Include(
                "~/Scripts/locations.js"
                ));

            bundles.Add(new ScriptBundle(Constants.Js.Jquery).Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle(Constants.Js.JqueryUI).Include(
                       "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle(Constants.Js.Moderinzr).Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle(Constants.Css.General).Include(
                        "~/Content/Site.css",
                        "~/Content/addtohomescreen.css"
                        ));

            bundles.Add(new StyleBundle(Constants.Css.Locations).Include(
                        "~/Content/locations.min.css"
                        ));
        }

    }
}
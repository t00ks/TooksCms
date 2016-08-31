using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TooksCms.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery/jquery-{version}.js",
                        "~/Scripts/lib/jquery/jquery-migrate-1.2.1.js",
                        "~/Scripts/lib/jquery/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryplugins").Include(
                        "~/Scripts/lib/jquery/jquery.infinitescroll.js",
                        "~/Scripts/lib/jquery/jquery.lazyload.js",
                        "~/Scripts/lib/jquery/jquery.clickoutside.js"));

            bundles.Add(new ScriptBundle("~/bundles/otherplugins").Include(
                        "~/Scripts/lib/sh/shCore.js",
                        "~/Scripts/lib/sh/shAutoloader.js",
                        "~/Scripts/lib/bootstrap.js",
                        "~/Scripts/lib/knockout/knockout-3.2.0.js",
                        "~/Scripts/lib/knockout/knockout.extensions.js",
                        "~/Scripts/lib/magnific-popup.js",
                        "~/Scripts/lib/moment/moment-with-locales.js",
                        "~/Scripts/lib/toastr.js",
                        "~/Scripts/lib/amcharts/amcharts.js",
                        "~/Scripts/lib/amcharts/funnel.js",
                        "~/Scripts/lib/amcharts/gauge.js",
                        "~/Scripts/lib/amcharts/pie.js",
                        "~/Scripts/lib/amcharts/radar.js",
                        "~/Scripts/lib/amcharts/serial.js",
                        "~/Scripts/lib/amcharts/xy.js",
                        "~/Scripts/lib/amcharts/themes/light.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/reset.css",
                        "~/Content/toastr.css",
                        "~/Content/bootstrap/bootstrap.css",
                        "~/Content/bootstrap-override.css", 
                        "~/Content/durandal.css", 
                        "~/Content/font-awesome.css",
                        "~/Content/magnific-popup.css",
                        "~/Content/animations.css",
                        "~/Content/tk/site.css"
                        //"~/Content/tk/layout.css",
                        //"~/Content/tk/global.css",
                        //"~/Content/tk/content.css",
                        //"~/Content/tk/bulletin.css",
                        //"~/Content/tk/article.css",
                        //"~/Content/tk/article-edit.css",
                        //"~/Content/tk/gallery.css",
                        //"~/Content/tk/comments.css",
                        //"~/Content/tk/rating.css",
                        //"~/Content/tk/tags.css",
                        //"~/Content/tk/widgets.css",
                        //"~/Content/tk/admin.css",
                        //"~/Content/tk/contact.css",
                        //"~/Content/tk/wedding.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/lh").Include(
                        "~/Content/shCore.css",
                        "~/Content/shCoreDefault.css",
                        "~/Content/shThemeDefault.css"));

        }
    }
}
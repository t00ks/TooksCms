using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TooksCms.Web.Helpers;

namespace TooksCms.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.json/{*pathInfo}");
            routes.IgnoreRoute("{resource}.manifest/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ttf/{*pathInfo}");

            routes.MapRoute(
                name: "AjaxRequests",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = new IsAjaxConstraint() }
            );

            routes.MapRoute(
                name: "FileRequest",
                url: "FileUpload/PostFile/{filename}",
                defaults: new { controller = "FileUpload", action = "PostFile" }
            );

            routes.MapRoute(
                name: "SiteMapRequest",
                url: "App/Views/sitemap.html",
                defaults: new { controller = "Home", action = "SiteMap" }
            );

            routes.MapRoute(
                name: "SiteMapXmlRequest",
                url: "sitemap.xml",
                defaults: new { controller = "Home", action = "SiteMapXml" }
            );

            routes.MapRoute(
                name: "ViewRequest",
                url: "App/Views/{controller}/{action}.html",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "ResourceRequest",
                url: "App/locales/{lang}/{ns}",
                defaults: new { controller = "Resource", action = "Get", lang = "en", ns = "app" }
            );

            routes.MapRoute(
                name: "HttpRequest",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}

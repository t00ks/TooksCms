using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace TooksCms.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "HomePaging",
                routeTemplate: "api/bulletin/{page}",
                defaults: new { controller = "Bulletin", page = RouteParameter.Optional },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "TagRoute",
                routeTemplate: "api/tags/{type}/{id}",
                defaults: new { controller = "Tags" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "TwitterRoute",
                routeTemplate: "api/gadget/twitter",
                defaults: new { controller = "Gadget", action = "GetTwitter" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "TagsRoute",
                routeTemplate: "api/gadget/tags",
                defaults: new { controller = "Gadget", action = "GetTags" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "GadgetRoute",
                routeTemplate: "api/gadget/{area}",
                defaults: new { controller = "Gadget" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "CommentRoute",
                routeTemplate: "api/comment/{type}/{id}",
                defaults: new { controller = "Comment" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "GalleryAddRoute",
                routeTemplate: "api/gallery/add/{categoryId}",
                defaults: new { controller = "Gallery", action = "Add" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "GalleryListRoute",
                routeTemplate: "api/gallery/list",
                defaults: new { controller = "Gallery", action = "GetList" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "GallerySaveRoute",
                routeTemplate: "api/gallery/save",
                defaults: new { controller = "Gallery", action = "Put" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "GalleryRoute",
                routeTemplate: "api/gallery",
                defaults: new { controller = "Gallery", action = "Get" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "ArticleAddRoute",
                routeTemplate: "api/article/add/{categoryId}/{articleTypeId}",
                defaults: new { controller = "Article", action = "Add" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "ArticleListRoute",
                routeTemplate: "api/article/list/{type}",
                defaults: new { controller = "Article", action = "GetList" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "ArticleReviewRoute",
                routeTemplate: "api/article/review",
                defaults: new { controller = "Article", action = "PutReview" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "ArticleNewsRoute",
                routeTemplate: "api/article/news",
                defaults: new { controller = "Article", action = "PutNews" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "ArticleRoute",
                routeTemplate: "api/article",
                defaults: new { controller = "Article", action = "Get" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "AdminListRoute",
                routeTemplate: "api/admin/routes/{typeId}/{id}",
                defaults: new { controller = "Routes", action = "PostArticleRoute" },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "AdminGalleryRouteRoute",
                routeTemplate: "api/admin/routes/{id}",
                defaults: new { controller = "Routes", action = "PostGalleryRoute" },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "AdminArticleRouteRoute",
                routeTemplate: "api/admin/list/{type}/{id}",
                defaults: new { controller = "List", id = RouteParameter.Optional },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "AdminStatsRoute",
                routeTemplate: "api/admin/stats/{type}",
                defaults: new { controller = "Stats" },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "AdminGetTag",
                routeTemplate: "api/admin/tag/{id}/{type}",
                defaults: new { controller = "Tag", action = "Get" },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "AdminTagDefault",
                routeTemplate: "api/admin/tag/{link}",
                defaults: new { controller = "Tag" },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "AdminDefault",
                routeTemplate: "api/admin/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "StatsRoute",
                routeTemplate: "api/stat/{stat}",
                defaults: new { controller = "Stats" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "SearchRoute",
                routeTemplate: "api/search/{term}",
                defaults: new { controller = "Search" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "AccountRoute",
                routeTemplate: "api/account/{type}",
                defaults: new { controller = "Account" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "WeddingRouteGuest",
                routeTemplate: "api/wedding/guest",
                defaults: new { controller = "Wedding", action = "PostGuest" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "WeddingRouteRsvp",
                routeTemplate: "api/wedding/rsvp",
                defaults: new { controller = "Wedding", action = "PutRSVP" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "WeddingRoute",
                routeTemplate: "api/wedding/{type}",
                defaults: new { controller = "Wedding" },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            config.Routes.MapHttpRoute(
                name: "WeddingAdminRoute",
                routeTemplate: "api/weddingadmin/{type}",
                defaults: new { controller = "WeddingAdmin" },
                constraints: null,
                namespaceTokens: new string[] { "API", "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                namespaceTokens: new string[] { "API" }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(GlobalConfiguration.Configuration));

        }

        private static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, string[] namespaceTokens)
        {
            HttpRouteValueDictionary defaultsDictionary = new HttpRouteValueDictionary(defaults);
            HttpRouteValueDictionary constraintsDictionary = new HttpRouteValueDictionary(constraints);
            IDictionary<string, object> tokens = new Dictionary<string, object>();
            tokens.Add("Namespaces", namespaceTokens);

            IHttpRoute route = routes.CreateRoute(routeTemplate, defaultsDictionary, constraintsDictionary, dataTokens: tokens, handler: null);
            routes.Add(name, route);

            return route;
        }
    }
}

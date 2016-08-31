using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TooksCms.ServiceLayer.Objects;

namespace TooksCms.Web.Helpers
{
    public static class RouteCollectionExtensions
    {
        public static void AddStaticRoute(this RouteCollection routes, StaticRoute sroute, bool needsLock = false)
        {
            var route = new Route(sroute.StaticRouteUrl, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = sroute.Area,
                    action = sroute.Action,
                    id = sroute.Id,
                }),
                Constraints = new RouteValueDictionary(new
                {
                    controller = new IsAjaxConstraint()
                })
            };

            var staticRoute = new Route(sroute.StaticRouteUrl, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new 
                {
                    controller = "Home",
                    action = "StaticRoute",
                    type = sroute.Action,
                    id = sroute.Id
                })
            };

            if (needsLock)
            {
                using (routes.GetWriteLock())
                {
                    routes.Insert(0, staticRoute);
                    routes.Insert(0, route);
                }
            }
            else
            {
                routes.Insert(0, staticRoute);
                routes.Insert(0, route);
            }
        }

        public static void AddArticleRoute(this RouteCollection routes, string url, string action, int id, bool needsLock = false)
        {
            var route = new Route(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Article",
                    action = action,
                    id = id,
                })
            };

            if (needsLock)
            {
                using (routes.GetWriteLock())
                {
                    routes.Insert(0, route);
                }
            }
            else
            {
                routes.Insert(0, route);
            }
        }

        public static void AddGalleryRoute(this RouteCollection routes, string url, int id, bool needsLock = false)
        {
            var route = new Route(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Gallery",
                    action = "StaticView",
                    id = id,
                })
            };

            if (needsLock)
            {
                using (routes.GetWriteLock())
                {
                    routes.Insert(0, route);
                }
            }
            else
            {
                routes.Insert(0, route);
            }
        }
    }
}
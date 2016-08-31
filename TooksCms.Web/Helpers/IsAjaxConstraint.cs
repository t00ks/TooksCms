using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace TooksCms.Web.Helpers
{
    public class IsAjaxConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.Request.IsAjaxRequest() || routeDirection == RouteDirection.UrlGeneration;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TooksCms.ServiceLayer.Attributes
{
    public class AjaxRoute
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Id { get; set; }
    }

    public class AjaxCrawlableAttribute : ActionFilterAttribute
    {
        private const string Fragment = "_escaped_fragment_";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;

            if (string.IsNullOrWhiteSpace(request.QueryString[Fragment]))
                return;

            var parts = request.QueryString[Fragment].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var routeValues = new AjaxRoute();

            if (parts.Length > 0)
                routeValues.Controller = parts[0];

            if (parts.Length > 1)
                routeValues.Action = "Static" + parts[1];

            if (parts.Length > 2)
                routeValues.Id = parts[2];

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(routeValues));
        }

    }

}
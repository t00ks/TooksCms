using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TooksCms.ServiceLayer.Utilities;

namespace TooksCms.ServiceLayer.Attributes
{
    public sealed class SiteFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string host = string.Empty;
            if (filterContext.HttpContext.Request.Url != null)
            {
                host = filterContext.HttpContext.Request.Url.Host;
                StateManager.CurrentSite = StateManager.Sites.FirstOrDefault(s => s.Host.ToLower() == host.ToLower());
            }
        }
    }
}

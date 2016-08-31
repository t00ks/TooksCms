using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TooksCms.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Renders partial view to string containing HTML
        /// </summary>
        /// <param name="viewName">Name of the view to be rendered</param>
        /// <param name="model">Model if strongly typed view</param>
        /// <returns>String: Rendered HTML</returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return base.Json(data, contentType, JsonRequestBehavior.AllowGet);
        }
    }
}

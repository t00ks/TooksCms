using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Enums;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Services;

namespace TooksCms.Web.Controllers.API
{
    public class StatsDTO
    {
        public AreaType Area { get; set; }
        public int? ItemId { get; set; }
        public string SearchTerm { get; set; }
        public string LinkType { get; set; }
        public PageVisit Previous { get; set; }
    }

    public class StatController : ApiController
    {
        public HttpResponseMessage Put (StatsDTO stat)
        {
            try
            {
                var previous = StatisticsService.RegisterPageVisit(System.Web.HttpContext.Current.Request, stat.Area, stat.ItemId, stat.SearchTerm, stat.LinkType, stat.Previous);

                return Request.CreateResponse(HttpStatusCode.OK, previous);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

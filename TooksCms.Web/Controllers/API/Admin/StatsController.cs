using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Services;

namespace TooksCms.Web.Controllers.API.Admin
{
    public class StatsController : ApiController
    {
        private IStatsRepository _statsRepository;
        public StatsController(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        [Authorize]
        public HttpResponseMessage Get(string type)
        {
            try
            {
                switch (type.ToLower())
                {
                    case "pagevisits":
                        var pages = _statsRepository.FetchPageVisits().Select(pv => new PageVisit(pv).GetJSON());
                        return Request.CreateResponse(HttpStatusCode.OK, pages);
                    case "clicks":
                        var visits = StatisticsService.GetPageVisitAggregation(_statsRepository);
                        return Request.CreateResponse(HttpStatusCode.OK, visits);
                    case "browsers":
                        var browsers = StatisticsService.GetBrowserStats();
                        return Request.CreateResponse(HttpStatusCode.OK, browsers);

                    default:
                        throw new ArgumentException("Requestion Type Not Found:" + type);
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

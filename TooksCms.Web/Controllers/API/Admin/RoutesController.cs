using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Models;
using TooksCms.Web.Helpers;

namespace TooksCms.Web.Controllers.API.Admin
{
    public class RoutesController : ApiController
    {
        private readonly IConfigRepository _configRepository;
        private readonly IArticleRepository _articleRepository;

        public RoutesController(IConfigRepository configRepository, IArticleRepository articleRepository)
        {
            _configRepository = configRepository;
            _articleRepository = articleRepository;
        }

        #region Routes

        public HttpResponseMessage Get()
        {
            try
            {
                var list = RoutesModel.List(_configRepository);

                if (list == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostArticleRoute(int typeId, int id, [FromBody]string route)
        {
            try
            {
                if (!RoutesModel.CheckExists(route, _configRepository))
                {
                    var rm = RoutesModel.CreateArticleRoute(typeId, id, route, _configRepository, _articleRepository);

                    RouteTable.Routes.AddArticleRoute(rm.StaticRoute, rm.Action, rm.Id, true);

                    return Request.CreateResponse(HttpStatusCode.OK, rm);
                }

                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostGalleryRoute(int id, [FromBody]string route)
        {
            try
            {
                if (!RoutesModel.CheckExists(route, _configRepository))
                {
                    var rm = RoutesModel.CreateGalleryRoute(id, route, _configRepository);

                    RouteTable.Routes.AddGalleryRoute(rm.StaticRoute, rm.Id, true);

                    return Request.CreateResponse(HttpStatusCode.OK, rm);
                }

                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(RoutesModel route)
        {
            try
            {
                _configRepository.DeleteRoute(route.Id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        #endregion
    }
}

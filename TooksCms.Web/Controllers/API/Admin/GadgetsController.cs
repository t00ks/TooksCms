using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Objects;

namespace TooksCms.Web.Controllers.API.Admin
{
    public struct GadgetPostData
    {
        public int GadgetId { get; set; }
        public int AreaType { get; set; }
        public int RoleId { get; set; }
    }

    public class GadgetsController : ApiController
    {
        private readonly IConfigRepository _repository;
        public GadgetsController(IConfigRepository repository)
        {
            _repository = repository;
        }

        #region Gadgets

        [Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                var list = _repository.FetchGagetInfo().Select(gi => new GadgetInfo(gi));

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

        [Authorize]
        public HttpResponseMessage Post(GadgetPostData data)
        {
            try
            {
                if (!_repository.GadgetLinkExists(data.GadgetId, data.AreaType, data.RoleId))
                {
                    var g = _repository.AddGadgetLink(data.GadgetId, data.AreaType, data.RoleId);

                    return Request.CreateResponse(HttpStatusCode.OK, g);
                }

                return Request.CreateResponse(HttpStatusCode.Conflict);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(GadgetInfo gadget)
        {
            try
            {
                _repository.RemoveGadgetLink(gadget);

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

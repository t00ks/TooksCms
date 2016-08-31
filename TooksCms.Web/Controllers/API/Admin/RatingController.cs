using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.ServiceLayer.Models;

namespace TooksCms.Web.Controllers.API.Admin
{
    public class RatingController : ApiController
    {
        [Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                var list = RatingModel.GetList();

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
        public HttpResponseMessage Post([FromBody]string name)
        {
            try
            {
                var model = RatingModel.Create(name);
                model.MarkDirty();
                model.Save();

                return Request.CreateResponse(HttpStatusCode.OK, model);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

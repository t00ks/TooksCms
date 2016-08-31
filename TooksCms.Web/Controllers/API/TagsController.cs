using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.ServiceLayer.Models;

namespace TooksCms.Web.Controllers.API
{
    public class TagsController : ApiController
    {
        public HttpResponseMessage Get(string type, int id)
        {
            try
            {
                var tags = TagModel.ListTags(id, type);

                if (tags == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, tags);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

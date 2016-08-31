using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.ServiceLayer.Models.Lookup;

namespace TooksCms.Web.Controllers.API.Admin
{
    public class CategoryController : ApiController
    {
        [Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                var list = CategoryModel.GetList();

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

        [System.Web.Http.Authorize]
        public HttpResponseMessage Put(List<CategoryModel> list)
        {
            try
            {
                list.ForEach(c => c.Save());

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

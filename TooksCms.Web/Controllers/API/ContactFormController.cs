using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.ServiceLayer.Models;

namespace TooksCms.Web.Controllers.API
{
    public class ContactFormController : ApiController
    {
        public HttpResponseMessage Put(ContactFormModel model)
        {
            try
            {
                model.Date = DateTime.Now;
                model.MarkDirty();
                model.Save();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

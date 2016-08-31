using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.ServiceLayer.Models;

namespace TooksCms.Web.Controllers.API.Admin
{
    public struct ContactFormInfo
    {
        public bool Checked { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
    }

    public class ContactController : ApiController
    {
        [Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                var list = ContactFormModel.GetList();

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
        public HttpResponseMessage Put(ContactFormInfo form)
        {
            try
            {
                var model = ContactFormModel.Load(form.Id);
                model.MarkOld();
                switch (form.Type)
                {
                    case "public":
                        model.Public = form.Checked;
                        break;
                    case "read":
                        model.Read = form.Checked;
                        break;
                }
                model.MarkDirty();
                model.Save();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

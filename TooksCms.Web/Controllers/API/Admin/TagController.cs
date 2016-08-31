using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.ServiceLayer.Models;

namespace TooksCms.Web.Controllers.API.Admin
{
    public struct TagReference
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class TagController : ApiController
    {
        [Authorize]
        public HttpResponseMessage Get(int id, string type)
        {
            try
            {
                var result = new
                {
                    included = TagModel.ListTags(id, type),
                    common = TagModel.ListCommon(id, type)
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Post(string link, TagReference tag)
        {
            try
            {
                TagModel.AddTag(link, tag.Id, tag.Type);
                var result = new
                {
                    included = TagModel.ListTags(tag.Id, tag.Type),
                    common = TagModel.ListCommon(tag.Id, tag.Type)
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int link, TagReference tag)
        {
            try
            {
                TagModel.RegisterTag(tag.Id, link, tag.Type);
                var result = new
                {
                    included = TagModel.ListTags(tag.Id, tag.Type),
                    common = TagModel.ListCommon(tag.Id, tag.Type)
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int link, TagReference tag)
        {
            try
            {
                TagModel.UnRegisterTag(tag.Id, link, tag.Type);
                var result = new
                {
                    included = TagModel.ListTags(tag.Id, tag.Type),
                    common = TagModel.ListCommon(tag.Id, tag.Type)
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

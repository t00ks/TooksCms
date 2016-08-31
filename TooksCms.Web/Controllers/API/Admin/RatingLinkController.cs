using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.ServiceLayer.Models;

namespace TooksCms.Web.Controllers.API.Admin
{
    public struct RatingLinkPostData
    {
        public int ArticleTypeId { get; set; }
        public int CategoryId { get; set; }
    }

    public class RatingLinkController : ApiController
    {
        [Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                var list = RatingLinkModel.GetList().Select(rl => rl.GetLite()).ToList();

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
        public HttpResponseMessage Post(RatingLinkPostData newRating)
        {
            try
            {
                if (!RatingLinkModel.CheckExists(newRating.ArticleTypeId, newRating.CategoryId))
                {
                    var model = RatingLinkModel.CreateNew(newRating.ArticleTypeId, newRating.CategoryId);
                    return Request.CreateResponse(HttpStatusCode.OK, model.GetLite());
                }

                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Rating Link Already Exists");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(RatingLinkModelLite model)
        {
            try
            {
                new RatingLinkModel(model).Save();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

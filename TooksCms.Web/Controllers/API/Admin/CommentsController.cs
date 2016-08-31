using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Models;

namespace TooksCms.Web.Controllers.API.Admin
{
    public class CommentsController : ApiController
    {
        private readonly IArticleRepository _articleRepository;

        public CommentsController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                var comments = CommentModel.GetAll(_articleRepository);

                return Request.CreateResponse(HttpStatusCode.OK, comments);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _articleRepository.DeleteComment(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

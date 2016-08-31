using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Bases;

namespace TooksCms.Web.Controllers.API
{
    public class SearchController : ApiController
    {
        private IArticleRepository _articleRepository;

        public SearchController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public HttpResponseMessage Get(string term)
        {
            try
            {
                var search = _articleRepository.SearchArticleInfos(term);

                return Request.CreateResponse(HttpStatusCode.OK, search);

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

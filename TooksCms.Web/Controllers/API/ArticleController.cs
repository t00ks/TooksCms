using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Bases;
using TooksCms.ServiceLayer.Models;
using TooksCms.ServiceLayer.Objects;

namespace TooksCms.Web.Controllers.API
{
    public class ArticleController : ApiController
    {
        private IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                var articleInfos = _articleRepository.FetchArticleInfos((int?)15).Select(ai => new ArticleInfo(ai).GetJSON());

                if (articleInfos == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, articleInfos);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                var article = ArticleBase.Load(id, _articleRepository).GetJSONModel();

                if (article == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, article);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage GetList(string type)
        {
            try
            {
                var articleInfos = _articleRepository.FetchArticleInfos(type).Select(ai => new ArticleInfo(ai).GetJSON());

                if (articleInfos == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, articleInfos);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage PutReview(ReviewArticle article)
        {
            try
            {
                if (article.Id > 0) { article.MarkOld(); article.MarkDirty(); }
                article.Save(_articleRepository);

                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage PutNews(NewsArticle article)
        {
            try
            {
                if (article.Id > 0) { article.MarkOld(); article.MarkDirty(); }
                article.Save(_articleRepository);

                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Add(int categoryId, int articleTypeId)
        {
            try
            {
                var articleType = _articleRepository.FetchType(articleTypeId);
                if (articleType.Name == "News")
                {
                    var obj = NewsArticle.NewNewsArticle(categoryId, _articleRepository);
                    return Request.CreateResponse(HttpStatusCode.OK, obj.GetJSONModel());
                }

                if (articleType.Name == "Review")
                {
                    var obj = ReviewArticle.NewReviewArticle(categoryId);
                    return Request.CreateResponse(HttpStatusCode.OK, obj.GetJSONModel());
                }

                throw new ArgumentException("Unknown Article Type");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (_articleRepository.Exists(id))
                {
                    ArticleBase.Delete(id, _articleRepository);

                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.Gone);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Services;

namespace TooksCms.Web.Controllers.API.Admin
{
    public class ListController : ApiController
    {
        private IConfigRepository _configRepository;
        private ISecurityRepository _secutiryReposiory;
        private ILookupRepository _lookupRepository;
        private IArticleRepository _articleRepository;
        private IGalleryRepository _galleryRepsitory;

        public ListController(IConfigRepository configRepository, ISecurityRepository secutiryRepository,
            ILookupRepository lookupRepository, IArticleRepository articleRepository, IGalleryRepository galleryRepsitory)
        {
            _configRepository = configRepository;
            _secutiryReposiory = secutiryRepository;
            _lookupRepository = lookupRepository;
            _articleRepository = articleRepository;
            _galleryRepsitory = galleryRepsitory;
        }

        [System.Web.Http.Authorize]
        public HttpResponseMessage Get(string type)
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();

                switch (type.ToLower())
                {
                    case "countries":
                        list = ListService.GetCountries(null, _lookupRepository);
                        break;
                    case "gadgets":
                        list = ListService.GetGadgets(_configRepository);
                        break;
                    case "roles":
                        list = ListService.GetRoles(_secutiryReposiory);
                        break;
                    case "areatypes":
                        list = ListService.GetAreaTypes();
                        break;
                    case "galleries":
                        list = ListService.GetGalleries(_galleryRepsitory);
                        break;
                    case "articletypes":
                        list = ListService.GetArticleTypes(null, _configRepository);
                        break;
                    case "parentcategories":
                        list = ListService.GetParentCategories(null, _lookupRepository);
                        break;
                    case "categories":
                        list = ListService.GetCategories(null, _lookupRepository);
                        break;
                    case "flags":
                        return Request.CreateResponse(HttpStatusCode.OK, ListService.GetFlags(""));
                    case "phonetypes":
                        list = ListService.GetPhoneTypes(null);
                        break;
                }

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
        public HttpResponseMessage Get(string type, int id)
        {
            try
            {
                List<SelectListItem> list = ListService.GetArticles(id, _articleRepository);
                
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
    }
}

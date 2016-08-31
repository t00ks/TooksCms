using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Authentication;
using TooksCms.ServiceLayer.Models;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Utilities;

namespace TooksCms.Web.Controllers.API
{
    public class GalleryController : ApiController
    {
        private IGalleryRepository _galleryRepository;
        private IAccountRepository _accountRepository;

        public GalleryController(IGalleryRepository galleryRepository, IAccountRepository accountRepository)
        {
            _galleryRepository = galleryRepository;
            _accountRepository = accountRepository;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                var galleryInfos = _galleryRepository.FetchGalleryInfos((int?)15).Select(gi => new GalleryInfo(gi)).ToList();

                if (galleryInfos == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, galleryInfos);
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
                var model = GalleryModel.Load(id, _galleryRepository).GetJSONModel();

                if (model == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage GetList()
        {
            try
            {
                var previews = GalleryModel.List(_galleryRepository).Select(g => new
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageCount = g.Images.Count,
                    CreatedDate = g.CreatedDate,
                    UsersName = g.UsersName,
                    Thumbnail = VirtualPathUtility.ToAbsolute("~/Uploads/Images/Galleries/" + g.Uid + "/" + g.Images[0].Thumbnail)
                });

                if (previews == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, previews);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(GalleryDTO gallery)
        {
            try
            {
                var model = new GalleryModel(gallery);

                if (model.Id > 0) { model.MarkOld(); model.MarkDirty(); }
                model.Save();

                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Add(int categoryId)
        {
            try
            {
                if (User != null && User.Identity.IsAuthenticated)
                {
                    var currentUser = new UserPrincipal(User.Identity.Name, _accountRepository);

                    var model = GalleryModel.New(currentUser.User.UserId, categoryId);

                    return Request.CreateResponse(HttpStatusCode.OK, model.GetJSONModel());
                }

                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
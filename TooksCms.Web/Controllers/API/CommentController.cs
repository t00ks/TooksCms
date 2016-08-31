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

namespace TooksCms.Web.Controllers.API
{
    public class CommentController : ApiController
    {
        private IArticleRepository _articleRepository;
        private IAccountRepository _accountRepository;

        public CommentController(IArticleRepository articleRepository, IAccountRepository accountRepository)
        {
            _articleRepository = articleRepository;
            _accountRepository = accountRepository;
        }

        public HttpResponseMessage Get(string type, int id)
        {
            try
            {
                List<CommentModel> comments = new List<CommentModel>();

                switch (type)
                {
                    case "article":

                        comments = CommentModel.GetList(id, _articleRepository);

                        break;

                    case "gallery":
                        throw new NotImplementedException();

                    default:
                        throw new ArgumentException("type not recognised");
                }

                if (comments == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, comments);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(CommentModel model)
        {
            try
            {
                model.Date = DateTime.Now;
                if (User != null && User.Identity.IsAuthenticated)
                {
                    var currentUser = new UserPrincipal(User.Identity.Name, _accountRepository);
                    model.Save(currentUser.User);
                }
                else
                {
                    var currentGuest = new GuestPrincipal(HttpContext.Current.Request.UserHostAddress);

                    if (currentGuest.Guest.IsNew)
                    {

                        if (!string.IsNullOrWhiteSpace(model.Website) && !model.Website.ToLower().Contains("http://"))
                        {
                            var website = model.Website.ToLower().Replace("http://", "").Replace("http:/", "").Replace("http:", "");
                            model.Website = "http://" + website;
                        }
                        var guest = currentGuest.Guest;
                        guest.Date = DateTime.Now;
                        guest.Email = model.Email;
                        guest.Name = model.Name;
                        guest.Website = model.Website;
                        guest.Save();

                        currentGuest = new GuestPrincipal(guest.IpAddress);
                    }

                    model.Save(guest: currentGuest.Guest);
                }

                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

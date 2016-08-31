using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Authentication;
using TooksCms.ServiceLayer.Utilities;
using TooksCms.Web.Helpers;
using TooksCms.Web.Models;

namespace TooksCms.Web.Controllers
{
    public class HomeController : Controller
    {
        IAccountRepository _accountRepository;
        ISnapshotRepository _snapshotRepository;
        IArticleRepository _articleRepository;
        IGalleryRepository _galleryRepository;

        public HomeController(IAccountRepository accountRepository, ISnapshotRepository snapshotRepository, IArticleRepository articleRepository, IGalleryRepository galleryRepository)
        {
            _accountRepository = accountRepository;
            _snapshotRepository = snapshotRepository;
            _articleRepository = articleRepository;
            _galleryRepository = galleryRepository;
        }

        // GET: Home
        public ActionResult Index()
        { // If the request is not from a bot => control goes to Durandal app
            if (Request.QueryString["_escaped_fragment_"] == null)
            {
                return View();
            }

            // If the request contains the _escaped_fragment_, then we return an HTML Snapshot to the bot
            try
            {
                StateManager.RegisterPageVisit(Core.Enums.AreaType.Home);

                var result = _snapshotRepository.Fetch(Request.Url.AbsolutePath);
                if (result != null)
                {
                    return Content(result.Html);
                }
                else
                {
                    result = _snapshotRepository.Fetch("/");
                    if (result != null)
                    {
                        return Content(result.Html);
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ContentResult LoggedInUsers()
        {
            return Content(StateManager.UsersOnline.ToString());
        }

        [ChildActionOnly]
        public ActionResult Menu(bool isFlyout = false)
        {
            string userInfo = "";
            UserPrincipal up = null;
            if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
            {
                up = new UserPrincipal(HttpContext.User.Identity.Name, _accountRepository);

                userInfo = up.User.ContactInfo.FirstName + " " + up.User.ContactInfo.LastName;
            }
            string menu = MenuBuilder.Build(Server.MapPath("~/Content/xml/menu.blog.xml"),
                                            Server.MapPath("~/Content/xsl/menu.xslt"),
                                            up,
                                            isFlyout,
                                            userInfo);
            return Content(menu);
        }

        [HttpPost]
        public JsonResult Menu()
        {
            string userInfo = "";
            UserPrincipal up = null;
            if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
            {
                up = new UserPrincipal(HttpContext.User.Identity.Name, _accountRepository);

                userInfo = up.User.ContactInfo.FirstName + " " + up.User.ContactInfo.LastName;
            }

            string mainMenu = MenuBuilder.Build(Server.MapPath("~/Content/xml/menu.blog.xml"),
                                            Server.MapPath("~/Content/xsl/menu.xslt"),
                                            up,
                                            false,
                                            userInfo);

            string flyout = MenuBuilder.Build(Server.MapPath("~/Content/xml/menu.blog.xml"),
                                            Server.MapPath("~/Content/xsl/menu.xslt"),
                                            up,
                                            true,
                                            userInfo);

            return Json(new { flyout, mainMenu });
        }

        public ActionResult SiteMap()
        {
            var model = GetSiteMapModel();

            return View(model);
        }

        private SiteMapModel GetSiteMapModel()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');

            var model = new SiteMapModel()
            {
                HomeUrl = baseUrl
            };

            var articleInfos = _articleRepository.FetchArticleInfos().ToList();
            var galleryInfos = _galleryRepository.FetchGalleryInfos();

            model.Galleries = galleryInfos.ToDictionary(g => g.Title, g => "gallery/view/" + g.GalleryId.ToString());

            model.News = articleInfos.Where(a => a.TypeName == "News").ToDictionary(a => a.Title, a => "article/view/" + a.ArticleId.ToString());
            model.Reviews = articleInfos.Where(a => a.TypeName == "Review").ToDictionary(a => a.Title, a => "article/review/" + a.ArticleId.ToString());

            return model;
        }

        public ContentResult SiteMapXml()
        {
            var model = GetSiteMapModel();

            XDocument xml = new XDocument();
            XNamespace ns = @"http://www.sitemaps.org/schemas/sitemap/0.9";

            XElement urlset = new XElement(ns + "urlset");
            xml.Add(urlset);

            urlset.Add(model.BuildUrlElement(string.Empty, "daily"));
            urlset.Add(model.BuildUrlElement("article/list/news", "daily"));
            urlset.Add(model.BuildUrlElement("article/list/review", "daily"));
            urlset.Add(model.BuildUrlElement("gallery/list", "daily"));
            urlset.Add(model.BuildUrlElement("contact"));
            urlset.Add(model.BuildUrlElement("about"));
            urlset.Add(model.BuildUrlElement("about/prowork"));
            urlset.Add(model.BuildUrlElement("about/personal"));

            foreach (var kvp in model.News)
            {
                urlset.Add(model.BuildUrlElement(kvp.Value));
            }

            foreach (var kvp in model.Reviews)
            {
                urlset.Add(model.BuildUrlElement(kvp.Value));
            }

            foreach (var kvp in model.Galleries)
            {
                urlset.Add(model.BuildUrlElement(kvp.Value));
            }

            return Content(xml.ToString());
        }
    }
}
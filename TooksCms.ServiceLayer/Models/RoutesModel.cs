using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using System.Web.Mvc;

namespace TooksCms.ServiceLayer.Models
{
    public class RoutesModel
    {
        [Dependency]
        private IGalleryRepository _galleryRepository { get; set; }
        [Dependency]
        private IArticleRepository _articleRepository { get; set; }

        public RoutesModel() { }

        public RoutesModel(IStaticRoute data)
        {
            _articleRepository = DependencyResolver.Current.GetService<IArticleRepository>();
            _galleryRepository = DependencyResolver.Current.GetService<IGalleryRepository>();

            this.StaticRoute = data.StaticRouteUrl;
            this.Area = data.Area;
            this.Action = data.Action;
            this.Id = data.Id;
            switch (Area)
            {
                case "Gallery":
                    var ginfo = GalleryModel.Load(this.Id, _galleryRepository);
                    this.Description = ginfo.Title;
                    break;
                default:
                    var info = new ArticleInfo(_articleRepository.FetchArticleInfo(this.Id));
                    this.Description = info.CategoryName + " - " + info.Title;
                    break;
            }
        }

        public string StaticRoute { get; set; }

        public string Area { get; set; }

        public string Action { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public static List<RoutesModel> List(IConfigRepository rep)
        {
            return rep.FetchRoutes().Select(r_ => new RoutesModel(r_)).ToList();
        }

        public static RoutesModel CreateArticleRoute(int articleTypeId, int articleId, string route, IConfigRepository cRep, IArticleRepository arRep)
        {
            var at = arRep.FetchType(articleTypeId);
            var r = Objects.StaticRoute.Create(route, "Article", at.Action, articleId);

            var rm = new RoutesModel(cRep.CreateRoute(r));
            return rm;
        }

        public static RoutesModel CreateGalleryRoute(int galleryId, string route, IConfigRepository cRep)
        {
            var r = Objects.StaticRoute.Create(route, "Gallery", "StaticView", galleryId);

            var rm = new RoutesModel(cRep.CreateRoute(r));
            return rm;
        }

        public static bool CheckExists(string route, IConfigRepository rep)
        {
            return rep.CheckRouteExists(route);
        }
    }
}

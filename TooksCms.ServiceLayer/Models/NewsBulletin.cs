using System;
using TooksCms.ServiceLayer.Bases;
using TooksCms.Core.Bases;
using TooksCms.Core.Objects.Xml;
using TooksCms.ServiceLayer.Objects;
using System.Web;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using System.Web.Mvc;

namespace TooksCms.ServiceLayer.Models
{
    public class NewsBulletin : ArticleBulletin
    {
        public NewsBulletin() { }

        public NewsBulletin(IBulletin data) : base(data) { }

        public static void Create(int articleId, string title, string url, string linkName, EditableDivProperty content, DateTime date, ImageProperty image)
        {
            var rep = DependencyResolver.Current.GetService<IBulletinRepository>();

            url = HttpUtility.HtmlEncode(url);
            var bulletin = new NewsBulletin
            {
                ArticleId = articleId,
                Title = new TitleTextBoxProperty { Value = title },
                Link = new ReadMoreLinkProperty { Value = linkName, Link = url },
                ViewContent = new StandardTextProperty { Value = content.Value, Type = content.Type, CssClass = content.CssClass },
                SiteId = 1,
                Date = date,
                BulletinType = new BulletinType(rep.FetchType("news")),
                Image = image
            };
            bulletin.MarkNew();
            bulletin.Save(rep);
        }

        public static void Update(int articleId, string title, string url, string linkName, EditableDivProperty content, DateTime date, ImageProperty image)
        {
            var bRep = DependencyResolver.Current.GetService<IBulletinRepository>();

            url = HttpUtility.HtmlEncode(url);
            var bulletin = (NewsBulletin)LoadForArticle(articleId, bRep);

            bulletin.Title.Value = title;
            bulletin.Link.Value = linkName;
            bulletin.Link.Link = url;
            bulletin.ViewContent.Value = content.Value;
            bulletin.ViewContent.Type = content.Type;
            bulletin.ViewContent.CssClass = content.CssClass;
            bulletin.Date = date;
            bulletin.Image = image;

            bulletin.MarkDirty();
            bulletin.Save(bRep);
        }

        #region Overrides of ObjectBase

        public override string GetImageLink()
        {
            var articleUid = Image.Value.Substring(0, Image.Value.IndexOf("_"));
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/NewsArticle/" + articleUid + "/" + Image.Value);
        }

        public override string GetImageThumbnail()
        {
            var articleUid = Image.Value.Substring(0, Image.Value.IndexOf("_"));
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/NewsArticle/" + articleUid + "/" + Image.Thumbnail);
        }

        public override string GetImage()
        {
            var articleUid = Image.Value.Substring(0, Image.Value.IndexOf("_"));
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/NewsArticle/" + articleUid + "/" + Image.Value);
        }

        public override object GetJSONModel()
        {
            return new
            {
                Uid = this.Uid,
                Id = this.Id,
                ViewContent = this.ViewContent,
                Title = this.Title,
                Image = Image != null ? new { ImagePath = GetImage(), Thumbnail = this.GetImageThumbnail() } : null,
                CommentCount = this.CommentCount,
                CategoryInfo = this.CategoryInfo,
                BulletinType = this.BulletinType.Name,
                Date = this.Date,
                ArticleId = this.ArticleId,
                Link = this.Link
            };
        }
        
        #endregion
    }
}

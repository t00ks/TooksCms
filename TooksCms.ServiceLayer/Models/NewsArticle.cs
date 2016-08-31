using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TooksCms.ServiceLayer.Bases;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Objects.Xml;
using System.Web;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Models
{
    public class NewsArticle : ArticleBase
    {
        public NewsArticle() : base() { }

        public static NewsArticle NewNewsArticle(int categoryId, IArticleRepository rep)
        {
            IArticleType articleType = new Objects.ArticleType(rep.FetchType(ArticleTypeEnum.News.ToString()));
            var newsArticle = new NewsArticle
            {
                EditableContent =
                    new List<EditableDivProperty>(),
                Title = new TitleTextBoxProperty { Value = "" },
                Images = new List<ImageProperty>(),
                State = Core.Enums.ArticleState.Incomplete,
                SiteId = 1,
                Date = DateTime.Today,
                ArticleTypeId = articleType.ArticleTypeId,
                CategoryId = categoryId
            };
            newsArticle.MarkNew();
            return newsArticle;
        }

        internal override void CreateBulletin()
        {
            var content = EditableContent[0];
            NewsBulletin.Create(Id, Title.Value, "Article/View/" + Id, "Read More", content, Date, GetBulletinImage());
        }

        internal override void UpdateBulletin()
        {
            var content = EditableContent[0];
            NewsBulletin.Update(Id, Title.Value, "Article/View/" + Id, "Read More", content, Date, GetBulletinImage());
        }

        internal string GetImageLink(ImageProperty image)
        {
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/NewsArticle/" + this.Uid + "/" + image.Value);
        }

        internal string GetImageThumbnail(ImageProperty image)
        {
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/NewsArticle/" + this.Uid + "/" + image.Thumbnail);
        }

        public override string EditAction
        {
            get { return "EditNews"; }
        }

        public override string SaveAction
        {
            get { return "SaveNews"; }
        }

        public override string ArticleTypeName
        {
            get { return "NewsArticle"; }
        }

        public override object GetJSONModel()
        {
            return new
            {
                Uid = this.Uid,
                Id = this.Id,
                Type = "news",
                EditableContent = this.EditableContent,
                CategoryInfo = this.CategoryInfo,
                Title = this.Title,
                Images = this.Images.Select(i => new
                {
                    Id = i.Id,
                    Value = i.Value,
                    ThumbValue = i.Thumbnail,
                    ImagePath = GetImageLink(i),
                    Thumbnail = GetImageThumbnail(i),
                    Position = i.Position,
                    Size = i.Size
                }),
                State = this.State,
                SiteId = this.SiteId,
                Date = this.Date,
                ArticleTypeId = this.ArticleTypeId,
                ArticleTypeName = this.ArticleTypeName,
                CategoryId = this.CategoryId,
                EditAction = this.EditAction,
                SaveAction = this.SaveAction
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.ServiceLayer.Bases;
using TooksCms.Core.Objects.Xml;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces.Repository;
using System.Web.Mvc;
using System.Web;

namespace TooksCms.ServiceLayer.Models
{
    public class ReviewArticle : ArticleBase
    {
        public ReviewArticle() : base() {
            OnBeforeSave += new ArticleEventHandler(ReviewArticle_OnBeforeSave);
        }

        protected void ReviewArticle_OnBeforeSave(object sender, ArticleEventArgs e)
        {
            var total = 0M;
            Ratings.Where(r_ => r_.Type != "Summary").ToList().ForEach(r => total += r.Rating);
            Ratings.Single(r_ => r_.Type == "Summary").Rating = Math.Round(total / Ratings.Count(r_ => r_.Type != "Summary"), 1);
        }

        public List<RatingProperty> Ratings { get; set; }

        public static ReviewArticle NewReviewArticle(int categoryId)
        {
            var aRep = DependencyResolver.Current.GetService<IArticleRepository>();
            var cRep = DependencyResolver.Current.GetService<IConfigRepository>();

            var articleType = new Objects.ArticleType(aRep.FetchType(ArticleTypeEnum.Review.ToString()));
            var reviewArticle = new ReviewArticle
            {
                EditableContent = new List<EditableDivProperty>(),
                Title = new TitleTextBoxProperty { Value = "" },
                Images = new List<ImageProperty>(),
                Ratings = RatingProperty.BuildXmlProperty(cRep.FetchRatings(articleType.ArticleTypeId, categoryId)).ToList(),
                State = Core.Enums.ArticleState.Incomplete,
                SiteId = 1,
                Date = DateTime.Today,
                ArticleTypeId = articleType.ArticleTypeId,
                CategoryId = categoryId
            };
            reviewArticle.Ratings.Add(new RatingProperty("Summary") { Name = "Summary" });
            reviewArticle.MarkNew();
            return reviewArticle;
        }

        internal override void CreateBulletin()
        {
            var content = EditableContent[0];
            var summary = Ratings.Single(r => r.Type == "Summary");
            ReviewBulletin.Create(Id, Title.Value, "Article/Review/" + Id, "Read More", content, Date, GetBulletinImage(), summary);
        }

        internal override void UpdateBulletin()
        {
            var content = EditableContent[0];
            var summary = Ratings.Single(r => r.Type == "Summary");
            ReviewBulletin.Update(Id, Title.Value, "Article/Review/" + Id, "Read More", content, Date, GetBulletinImage(), summary);
        }

        internal string GetImageLink(ImageProperty image)
        {
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/Review/" + this.Uid + "/" + image.Value);
        }

        internal string GetImageThumbnail(ImageProperty image)
        {
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/Review/" + this.Uid + "/" + image.Thumbnail);
        }

        public override string EditAction
        {
            get { return "EditReview"; }
        }

        public override string SaveAction
        {
            get { return "SaveReview"; }
        }

        public override string ArticleTypeName
        {
            get { return "Review"; }
        }

        public override object GetJSONModel()
        {
            return new
            {
                Uid = this.Uid,
                Id = this.Id,
                Type = "review",
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
                Ratings = this.Ratings,
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

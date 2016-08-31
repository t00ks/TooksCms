using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Article : IArticle
    {
        public ArticleState State { get { return (ArticleState)Status; } set { Status = (int)value; } }

        public XElement Content { get { return XElement.Parse(this.ArticleContents.OrderBy(c_ => c_.Version).Last().Content); } set { } }

        public static Article CreateArticle(IArticle data)
        {
            return new Article
            {
                ArticleUid = data.ArticleUid,
                ArticleTypeId = data.ArticleType.ArticleTypeId,
                CategoryId = data.CategoryId,
                SiteId = data.SiteId,
                Status = (int)data.State,
                Date = data.Date
            };

        }

        public void UpdateContent(IArticle data)
        {
            var newVersion = ArticleContents.Count + 1;
            var content = new ArticleContent()
                                    {
                                        Article = this,
                                        ArticleContentUid = Guid.NewGuid(),
                                        ArticleId = ArticleId,
                                        Content = data.Content.ToString(),
                                        Version = newVersion
                                    };
            ArticleContents.Add(content);
        }

        public void Update(IArticle data)
        {
            CategoryId = data.CategoryId;
            ArticleTypeId = data.ArticleType.ArticleTypeId;
            SiteId = data.SiteId;
            State = data.State;
        }

        IArticleType IArticle.ArticleType
        {
            get { return this.ArticleType; }
            set { }
        }

        IEnumerable<IArticleImage> IArticle.ArticleImages
        {
            get { return this.ArticleImages; }
            set { }
        }
    }

    public partial class ArticleType : IArticleType
    {
        public ArticleType(IArticleType data) : base()
        {
            ArticleTypeId = data.ArticleTypeId;
            ArticleTypeUid = data.ArticleTypeUid;
            Assembly = data.Assembly;
            Class = data.Class;
            Description = data.Description;
            Name = data.Name;
            Action = data.Action;
        }
    }
}

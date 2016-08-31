using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Enums;
using System.Xml.Linq;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Objects
{
    public class Article : InterfacingBase, IArticle
    {
        private Article() { }

        public Article(IArticle data) :
            base(data, typeof(IArticle))
        {

        }

        public int ArticleId { get; set; }

        public Guid ArticleUid { get; set; }

        public IArticleType ArticleType { get; set; }

        public ArticleState State { get; set; }

        public int SiteId { get; set; }

        public XElement Content { get; set; }

        public int CategoryId { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<IArticleImage> ArticleImages { get; set; }

        public static Article CreateArticle(int id, Guid uid, int articleTypeId, ArticleState state, int siteId, XElement content,
            int categoryId, DateTime date, IArticleRepository rep)
        {
            return new Article
            {
                ArticleId = id,
                ArticleUid = uid,
                ArticleType = rep.FetchType(articleTypeId),
                State = state,
                SiteId = siteId,
                Content = content,
                CategoryId = categoryId,
                Date = date
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TooksCms.Core.Enums;

namespace TooksCms.Core.Interfaces
{
    public interface IArticle : IInterfacingBase
    {
        int ArticleId { get; set; }
        Guid ArticleUid { get; set; }
        IArticleType ArticleType { get; set; }
        ArticleState State { get; set; }
        int SiteId { get; set; }
        XElement Content { get; set; }
        int CategoryId { get; set; }
        DateTime Date { get; set; }
        IEnumerable<IArticleImage> ArticleImages { get; set; }
    }
}

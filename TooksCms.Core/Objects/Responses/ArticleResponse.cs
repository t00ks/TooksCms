using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;

namespace TooksCms.Core.Objects.Responses
{
    public class ArticleResponse : ResponseBase
    {
        public ArticleResponse()
        {
            this.area = Enums.AreaType.Article;
        }

        public bool IsEditable { get; set; }
    }
}

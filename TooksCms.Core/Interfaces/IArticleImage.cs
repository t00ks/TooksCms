using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IArticleImage : IInterfacingBase
    {
        int ArticleImageId { get; set; }
        Guid ArticleImageUid { get; set; }
        int ArticleId { get; set; }
        string Image { get; set; }
        string Thumbnail { get; set; }
        string Position { get; set; }
        string Size { get; set; }
    }
}

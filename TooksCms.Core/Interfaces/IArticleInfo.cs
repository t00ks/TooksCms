using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IArticleInfo : IInterfacingBase
    {
        int ArticleId { get; set; }
        Guid ArticleUid { get; set; }
        int Status { get; set; }
        int CategoryId { get; set; }
        string CategoryName { get; set; }
        string CategoryImage { get; set; }
        DateTime Date { get; set; }
        string TypeName { get; set; }
        int ArticleTypeId { get; set; }
        int Version { get; set; }
        string Title { get; set; }
        bool? HasImages { get; set; }
        string ImageThumbnail { get; set; }
    }
}

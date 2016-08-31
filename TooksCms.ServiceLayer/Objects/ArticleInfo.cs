using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class ArticleInfo : InterfacingBase, IArticleInfo
    {
        public ArticleInfo(IArticleInfo data)
            : base(data, typeof(IArticleInfo)) { }

        #region Implementation of IArticleInfo

        public int ArticleId { get; set; }
        public Guid ArticleUid { get; set; }
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public DateTime Date { get; set; }
        public string TypeName { get; set; }
        public int ArticleTypeId { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public bool? HasImages { get; set; }
        public string ImageThumbnail { get; set; }

        #endregion

        public object GetJSON()
        {
            return new
            {
                ArticleId,
                ArticleUid,
                Status = ((ArticleState)Status).ToString(),
                CategoryId,
                CategoryName,
                CategoryImage,
                Date = Date,
                TypeName,
                ArticleTypeId,
                Version,
                Title,
                HasImages,
                ImageThumbnail,
            };
        }
    }
}

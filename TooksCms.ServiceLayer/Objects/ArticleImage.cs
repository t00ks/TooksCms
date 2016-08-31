using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class ArticleImage : InterfacingBase, IArticleImage
    {
        private ArticleImage() { }

        public ArticleImage(IArticleImage data) :
            base(data, typeof(IArticleImage))
        { }

        #region IArticleImage Members

        public int ArticleImageId { get; set; }

        public Guid ArticleImageUid { get; set; }

        public int ArticleId { get; set; }

        public string Image { get; set; }

        public string Thumbnail { get; set; }

        public string Position { get; set; }

        public string Size { get; set; }

        #endregion

        public static ArticleImage CreateArticleImage(int imageId, int articleId, string image, string thumbnail, string position, string size)
        {
            return new ArticleImage
            {
                ArticleImageId = imageId,
                ArticleImageUid = Guid.NewGuid(),
                ArticleId = articleId,
                Image = image,
                Thumbnail = thumbnail,
                Position = position, 
                Size = size
            };
        }
    }
}

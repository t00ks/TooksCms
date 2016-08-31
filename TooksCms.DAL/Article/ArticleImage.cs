using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class ArticleImage : IArticleImage
    {
        public static ArticleImage CreateArticleImage(IArticleImage data)
        {
            return new ArticleImage
            {
                ArticleId = data.ArticleId,
                ArticleImageUid = data.ArticleImageUid,
                Image = data.Image,
                Thumbnail = data.Thumbnail,
                Position = data.Position,
                Size = data.Size
            };
        }

        public void Update(IArticleImage data)
        {
            this.ArticleId = data.ArticleId;
            this.Image = data.Image;
            this.Thumbnail = data.Thumbnail;
            this.Position = data.Position;
            this.Size = data.Size;
        }
    }
}

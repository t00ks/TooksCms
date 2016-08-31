using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public class RatingLink : IRatingLink
    {
        public RatingLink(Rating2ArticleType2Category entity, IRating rating)
        {
            this.ArticleTypeId = entity.ArticleTypeId;
            this.CategoryId = entity.CategoryId;
            this.RatingIds = new Dictionary<int, IRating> { { entity.Ordinal, rating } };
        }

        #region Implementation of IRatingLink

        public Dictionary<int, IRating> RatingIds { get; private set; }
        public int ArticleTypeId { get; private set; }
        public int CategoryId { get; private set; }

        #endregion

        public void AddLink(IRating rating, int ordinal)
        {
            this.RatingIds.Add(ordinal, rating);
        }
    }
}

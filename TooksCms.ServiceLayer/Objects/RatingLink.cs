using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class RatingLink : InterfacingBase, IRatingLink
    {
        private RatingLink() { }

        public RatingLink(IRatingLink data) :
            base(data, typeof(IRatingLink)) { }

        public Dictionary<int,IRating> RatingIds { get; private set; }

        public int ArticleTypeId { get; private set; }

        public int CategoryId { get; private set; }

        public static RatingLink CreateRatingLink(Dictionary<int, IRating> ratingIds, int articletypeid, int categoryId)
        {
            return new RatingLink
            {
                RatingIds = ratingIds,
                ArticleTypeId = articletypeid,
                CategoryId = categoryId
            };
        }
    }
}

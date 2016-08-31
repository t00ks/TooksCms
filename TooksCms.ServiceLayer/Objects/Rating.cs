using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Bases;

namespace TooksCms.ServiceLayer.Objects
{
    public class Rating : InterfacingBase, IRating
    {
        private Rating() { }

        public Rating(IRating data)
            : base(data, typeof(IRating))
        { }

        public int RatingId { get; private set; }

        public Guid RatingUid { get; private set; }

        public string Name { get; private set; }

        public static Rating CreateRating(int id, Guid uid, string name)
        {
            return new Rating
            {
                RatingId = id,
                RatingUid = uid,
                Name = name
            };
        }
    }
}

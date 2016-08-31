using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Rating : IRating
    {
        public static Rating CreateRating(IRating data)
        {
            return new Rating
            {
                RatingUid = data.RatingUid,
                Name = data.Name
            };
        }

        public void Update(IRating data)
        {
            this.Name = data.Name;
        }
    }
}

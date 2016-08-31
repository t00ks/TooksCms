using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public class RankedTag : IRankedTag
    {
        #region Implementation of ITagRank

        public ITag Tag { get; set; }
        public int Rank { get; set; }

        #endregion
    }
}

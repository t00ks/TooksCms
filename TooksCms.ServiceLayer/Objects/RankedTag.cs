using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class RankedTag : InterfacingBase, IRankedTag
    {
        public RankedTag(IRankedTag data)
            : base(data, typeof(IRankedTag))
        { }

        #region Implementation of IRankedTag

        public ITag Tag { get; set; }
        public int Rank { get; set; }

        #endregion

        public object GetJSONModel()
        {
            return new
            {
                Rank = Rank,
                Tag = new
                {
                    Id = Tag.TagId,
                    Uid = Tag.TagUid,
                    Name = Tag.Name
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Tag : ITag
    {
        public static Tag CreateTag(ITag data)
        {
            return new Tag
            {
                TagUid = data.TagUid,
                Name = data.Name
            };
        }
    }
}

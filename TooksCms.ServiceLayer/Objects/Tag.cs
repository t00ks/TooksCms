using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Bases;

namespace TooksCms.ServiceLayer.Objects
{
    public class Tag : InterfacingBase, ITag
    {
        private Tag() { }

        public Tag(ITag data)
            : base(data, typeof(ITag))
        { }

        public int TagId { get; set; }

        public Guid TagUid { get; set; }

        public string Name { get; set; }

        public static Tag CreateRating(int id, Guid uid, string name)
        {
            return new Tag { TagId = id, TagUid = uid, Name = name };
        }
    }
}

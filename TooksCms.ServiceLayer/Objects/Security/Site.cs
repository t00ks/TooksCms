using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Security
{
    public class Site : InterfacingBase, ISite
    {
        public Site(ISite data) : base(data, typeof(ISite)) { }

        public int SiteId { get; set; }

        public Guid SiteUid { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public string Host { get; set; }
    }
}

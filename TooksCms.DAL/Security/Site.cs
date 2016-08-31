using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Site : ISite
    {
        public static Site CreateSite(ISite data)
        {
            return new Site
                       {
                           SiteId = data.SiteId,
                           SiteUid = data.SiteUid,
                           Name = data.Name,
                           URL = data.URL,
                           Host = data.Host
                       };
        }

        public void Update(ISite data)
        {
            this.Name = data.Name;
            this.URL = data.URL;
            this.Host = data.Host;
        }
    }
}

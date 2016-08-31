using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface ISite : IInterfacingBase
    {
        int SiteId { get; set; }
        Guid SiteUid { get; set; }
        string Name { get; set; }
        string URL { get; set; }
        string Host { get; set; }
    }
}

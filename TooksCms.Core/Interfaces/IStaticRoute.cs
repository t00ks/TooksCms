using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IStaticRoute : IInterfacingBase
    {
        string StaticRouteUrl { get; set; }
        string Area { get; set; }
        string Action { get; set; }
        int Id { get; set; }
    }
}

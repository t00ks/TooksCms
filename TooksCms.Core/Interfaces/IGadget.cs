using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IGadget : IInterfacingBase
    {
        int GadgetId { get; set; }
        Guid GadgetUid { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string View { get; set; }
        int SiteId { get; set; }
        int DefaultColumn { get; set; }
    }
}

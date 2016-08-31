using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IGadgetInfo : IInterfacingBase
    {
        int GadgetId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string View { get; set; }
        int DefaultColumn { get; set; }
        string RoleName { get; set; }
        string AreaType { get; set; }
    }
}

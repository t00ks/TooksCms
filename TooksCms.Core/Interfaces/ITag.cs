using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface ITag : IInterfacingBase
    {
        int TagId { get; set; }
        Guid TagUid { get; set; }
        string Name { get; set; }
    }
}

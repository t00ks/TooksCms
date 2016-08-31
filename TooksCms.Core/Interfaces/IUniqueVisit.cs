using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IUniqueVisit : IInterfacingBase
    {
        string IpAddress { get; set; }
        DateTime? Date { get; set; }
        int? Count { get; set; }
    }
}

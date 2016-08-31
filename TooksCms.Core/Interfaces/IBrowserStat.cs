using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IBrowserStat : IInterfacingBase
    {
        string UserAgent { get; set; }
        string BrowserName { get; set; }
        string BrowserVersion { get; set; }
        int? Count { get; set; }
    }
}

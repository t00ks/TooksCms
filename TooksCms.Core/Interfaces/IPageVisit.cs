using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Enums;

namespace TooksCms.Core.Interfaces
{
    public interface IPageVisit : IInterfacingBase
    {
        int Id { get; set; }
        AreaType AreaType { get; set; }
        int? ItemId { get; set; }
        string SearchTerm { get; set; }
        string Referer { get; set; }
        string Url { get; set; }
        string LinkType { get; set; }
        int? PreviousId { get; set; }
        string UserAgent { get; set; }
        string UserLanguages { get; set; }
        string BrowserVersion { get; set; }
        string BrowserName { get; set; }
        string IpAddress { get; set; }
        DateTime DateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Enums;

namespace TooksCms.ServiceLayer.Objects
{
    public class PageVisit : InterfacingBase, IPageVisit
    {
        public PageVisit() { }

        public PageVisit(IPageVisit data) : base(data, typeof(IPageVisit)) { }

        #region IPageVisit Implementation

        public int Id { get; set; }

        public AreaType AreaType { get; set; }

        public int? ItemId { get; set; }

        public string SearchTerm { get; set; }

        public string Referer { get; set; }

        public string Url { get; set; }

        public string LinkType { get; set; }

        public int? PreviousId { get; set; }

        public string UserAgent { get; set; }

        public string UserLanguages { get; set; }

        public string BrowserVersion { get; set; }

        public string BrowserName { get; set; }

        public string IpAddress { get; set; }

        public DateTime DateTime { get; set; }

        #endregion

        public object GetJSON()
        {
            return new
            {
                Id,
                AreaType = AreaType.ToString(),
                ItemId,
                SearchTerm,
                Referer,
                Url,
                LinkType,
                PreviousId,
                UserAgent,
                UserLanguages,
                BrowserVersion,
                BrowserName,
                IpAddress,
                DateTime
            };

        }
    }
}

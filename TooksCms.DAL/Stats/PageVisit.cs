using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class PageVisit : IPageVisit
    {
        public static PageVisit CreatePageVisit(IPageVisit data)
        {
            return new PageVisit
            {
                Id = data.Id,
                AreaType = (byte)data.AreaType,
                ItemId = data.ItemId,
                Referer = data.Referer,
                Url = data.Url,
                LinkType = data.LinkType,
                PreviousId = data.PreviousId,
                UserAgent = data.UserAgent,
                UserLanguages = data.UserLanguages,
                BrowserVersion = data.BrowserVersion,
                BrowserName = data.BrowserName,
                IpAddress = data.IpAddress,
                DateTime = data.DateTime
            };
        }

        Core.Enums.AreaType IPageVisit.AreaType
        {
            get
            {
                return (Core.Enums.AreaType)this.AreaType;
            }
            set
            {
                this.AreaType = (byte)value;
            }
        }
    }
}

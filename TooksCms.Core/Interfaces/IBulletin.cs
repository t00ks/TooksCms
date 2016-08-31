using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TooksCms.Core.Interfaces
{
    public interface IBulletin : IInterfacingBase
    {
        int BulletinId { get; set; }
        Guid BulletinUid { get; set; }
        int? ArticleId { get; set; }
        int? GalleryId { get; set; }
        IBulletinType BulletinType { get; set; }
        int SiteId { get; set; }
        DateTime Date { get; set; }
        XElement Content { get; set; }
        int CommentCount { get; set; }
    }
}
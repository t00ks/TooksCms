using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using System.Xml.Linq;

namespace TooksCms.ServiceLayer.Objects
{
    public class Bulletin : InterfacingBase, IBulletin
    {
        public Bulletin() { }

        public Bulletin(IBulletin data) :
            base(data, typeof(IBulletin))
        { }

        #region IBulletin Members

        public int BulletinId { get; set; }

        public Guid BulletinUid { get; set; }

        public int? ArticleId { get; set; }

        public int? GalleryId { get; set; }

        public IBulletinType BulletinType { get; set; }

        public int SiteId { get; set; }

        public DateTime Date { get; set; }

        public XElement Content { get; set; }

        public int CommentCount { get; set; }

        #endregion

        public static Bulletin CreateBulletin(int id, Guid uid, int? articleId, int? galleryId, BulletinType type, int siteId, XElement content, DateTime date)
        {
            return new Bulletin
            {
                BulletinId = id,
                BulletinUid = uid,
                ArticleId = articleId,
                GalleryId = galleryId,
                BulletinType = type,
                SiteId = siteId,
                Date = date,
                Content = content
            };
        }
    }
}

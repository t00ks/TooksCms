using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Bulletin : IBulletin
    {
        public XElement Content { get { return XElement.Parse(this.BulletinContents.Last().Content); } set { } }

        public static Bulletin CreateBulletin(IBulletin data)
        {
            return new Bulletin
                       {
                           ArticleId = data.ArticleId,
                           GalleryId = data.GalleryId,
                           BulletinUid = data.BulletinUid,
                           BulletinTypeId = data.BulletinType.BulletinTypeId,
                           Date = data.Date,
                           SiteId = data.SiteId
                       };
        }

        public int CommentCount
        {
            get
            {
                if (this.Article != null)
                {
                    return this.Article.ArticleComments.Count;
                }
                return 0;
            }
            set{}
        }

        public void UpdateContent(IBulletin data)
        {
            BulletinContents.First().UpdateContent(data);
        }

        public void Update(IBulletin data)
        {
            BulletinTypeId = data.BulletinType.BulletinTypeId;
            SiteId = data.SiteId;
        }

        IBulletinType IBulletin.BulletinType
        {
            get { return BulletinType; }
            set { BulletinTypeId = value.BulletinTypeId; }
        }
    }

    public partial class BulletinContent
    {
        public void UpdateContent(IBulletin data)
        {
            Content = data.Content.ToString();
        }

        public static BulletinContent CreateBulletinContent(IBulletin data)
        {
            return new BulletinContent
            {
                Content = data.Content.ToString(),
                BulletinContentUid = Guid.NewGuid(),
                BulletinId = data.BulletinId
            };
        }
    }

    public partial class BulletinType : IBulletinType
    {
        public BulletinType(IBulletinType data) : base()
        {
            BulletinTypeId = data.BulletinTypeId;
            BulletinTypeUid = data.BulletinTypeUid;
            Class = data.Class;
            Assembly = data.Assembly;
            Name = data.Name;
            Description = data.Description;
        }
    }
}

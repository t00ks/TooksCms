using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Objects.Xml;
using TooksCms.Core.Reflection;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Objects.Lookup;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Bases
{
    public abstract class BulletinBase : ModelBase
    {
        protected BulletinBase() { }

        protected BulletinBase(IBulletin data)
        {
            CommentCount = data.CommentCount;
            BulletinType = new BulletinType(data.BulletinType);
        }

        #region Properties

        [XmlIgnore]
        public BulletinType BulletinType { get; set; }

        [XmlAttribute("siteId")]
        public int SiteId { get; set; }

        public DateTime Date { get; set; }

        public ReadMoreLinkProperty Link { get; set; }

        public TitleTextBoxProperty Title { get; set; }

        [XmlIgnore]
        public int CommentCount { get; set; }

        [XmlIgnore]
        public CategoryInfo CategoryInfo { get; set; }

        #endregion Properties

        #region Xml Serialization

        public new string Serialize()
        {
            var xsr = new XmlSerializer(GetType());
            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            xsr.Serialize(tw, this);
            return sb.ToString();
        }
        public new BulletinBase Desrialize(string xml)
        {
            var xsr = new XmlSerializer(GetType());
            var sr = new StringReader(xml);
            var obj = xsr.Deserialize(sr);
            ((BulletinBase)obj).MarkOld();
            ((BulletinBase)obj).CommentCount = this.CommentCount;
            ((BulletinBase)obj).BulletinType = this.BulletinType;
            return (BulletinBase)obj;
        }

        #endregion Xml Serialization

        #region Load

        public static CollectionBase<BulletinBase> GetAll(ILookupRepository lRep, IBulletinRepository bRep)
        {
            var bulletins = new CollectionBase<BulletinBase>();
            bulletins.AddRange(bRep.FetchList().Select(bulletin =>
                    ((BulletinBase)Reflector.CreateObject(bulletin.BulletinType.Assembly, bulletin.BulletinType.Class, new[] { bulletin })).
                    Desrialize(bulletin.Content.ToString())));
            bulletins.ForEach(b =>
            {
                b.MarkOld();
                b.LoadCategoryInfo(lRep);
            });
            return bulletins;
        }

        public static CollectionBase<BulletinBase> GetList(ILookupRepository lRep, IBulletinRepository bRep, int count)
        {
            var bulletins = new CollectionBase<BulletinBase>();
            bulletins.AddRange(bRep.FetchList(count).Select(bulletin =>
                    ((BulletinBase)Reflector.CreateObject(bulletin.BulletinType.Assembly, bulletin.BulletinType.Class, new[] { bulletin })).
                    Desrialize(bulletin.Content.ToString())));
            bulletins.ForEach(b =>
            {
                b.MarkOld();
                b.LoadCategoryInfo(lRep);
            });
            return bulletins;
        }

        public static CollectionBase<BulletinBase> GetList(ILookupRepository lRep, IBulletinRepository bRep, int count, int skip)
        {
            var bulletins = new CollectionBase<BulletinBase>();
            bulletins.AddRange(bRep.FetchList(count, skip).Select(bulletin =>
                    ((BulletinBase)Reflector.CreateObject(bulletin.BulletinType.Assembly, bulletin.BulletinType.Class, new[] { bulletin })).
                    Desrialize(bulletin.Content.ToString())));
            bulletins.ForEach(b =>
            {
                b.MarkOld();
                b.LoadCategoryInfo(lRep);
            });
            return bulletins;
        }

        public static BulletinBase LoadForArticle(int articleId, IBulletinRepository bRep)
        {
            var bulletin = bRep.FetchOnArticleId(articleId);
            var obj = ((BulletinBase)Reflector.CreateObject(bulletin.BulletinType.Assembly, bulletin.BulletinType.Class, new[] { bulletin })).Desrialize(bulletin.Content.ToString());
            obj.BulletinType = new BulletinType(bRep.FetchType(bulletin.BulletinType.BulletinTypeId));
            obj.MarkOld();
            return obj;
        }

        public static BulletinBase LoadForGallery(int galleryId, IBulletinRepository bRep)
        {
            var bulletin = bRep.FetchOnGalleryId(galleryId);
            var obj = ((BulletinBase)Reflector.CreateObject(bulletin.BulletinType.Assembly, bulletin.BulletinType.Class, new[] { bulletin })).Desrialize(bulletin.Content.ToString());
            obj.BulletinType = new BulletinType(bRep.FetchType(bulletin.BulletinType.BulletinTypeId));
            obj.MarkOld();
            return obj;
        }

        #endregion Load

        #region CRUD
        public void Save(IBulletinRepository rep)
        {
            try
            {
                if (!IsNew & IsDeleted)
                {
                    /* [Delete] an existing object marked for deletion */
                    //dc.BulletinDelete(_id);
                }
                else
                {
                    /* Exception will cause the transaction to rollback */
                    if (IsNew)
                    {
                        /* [Insert] a new and valid object to be saved */
                        this.Id = rep.Insert(BuildInteface(false));
                        rep.InsertContent(BuildInteface(true));
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        rep.Update(BuildInteface(true));
                    }
                }
                MarkOld();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion CRUD


        public virtual string GetCommentCount()
        {
            return CommentCount.ToString();
        }

        public virtual string GetCategoryName()
        {
            return CategoryInfo != null ? CategoryInfo.FullCategoryName : "";
        }

        abstract internal Bulletin BuildInteface(bool parseContent);

        public abstract override object GetJSONModel();

        public abstract void LoadCategoryInfo(ILookupRepository lRep);
    }
}

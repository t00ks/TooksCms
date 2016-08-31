using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using TooksCms.ServiceLayer.Bases;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Objects.Xml;
using System.Web;

namespace TooksCms.ServiceLayer.Models
{
    public class GalleryBulletin : BulletinBase
    {
        public GalleryBulletin() { }
        public GalleryBulletin(IBulletin data) : base(data) { }

        [XmlAttribute("galleryId")]
        public int GalleryId { get; set; }

        public List<GalleryImageModel> Images { get; set; }

        public static void Create(int galleryId, string title, string url, string linkName, DateTime date, IEnumerable<GalleryImageModel> images)
        {
            var rep = DependencyResolver.Current.GetService<IBulletinRepository>();

            url = HttpUtility.HtmlEncode(url);
            var bulletin = new GalleryBulletin
            {
                GalleryId = galleryId,
                Title = new TitleTextBoxProperty { Value = title },
                Link = new ReadMoreLinkProperty { Value = linkName, Link = url },
                SiteId = 1,
                Date = date,
                BulletinType = new BulletinType(rep.FetchType("Gallery")),
                Images = images.ToList()
            };
            bulletin.MarkNew();
            bulletin.Save(rep);
        }

        public static void Update(int galleryId, string title, string url, string linkName, DateTime date, IEnumerable<GalleryImageModel> images)
        {
            var bRep = DependencyResolver.Current.GetService<IBulletinRepository>();

            url = HttpUtility.HtmlEncode(url);
            var bulletin = (GalleryBulletin)LoadForGallery(galleryId, bRep);

            bulletin.Title.Value = title;
            bulletin.Link.Value = linkName;
            bulletin.Link.Link = url;
            bulletin.Date = date;
            bulletin.Images = images.ToList();

            bulletin.MarkDirty();
            bulletin.Save(bRep);
        }

        #region Overrides of BulletinBase

        internal override Bulletin BuildInteface(bool parseContent)
        {
            var content = parseContent ? XElement.Parse(this.Serialize()) : XElement.Parse("<temp></temp>");
            return TooksCms.ServiceLayer.Objects.Bulletin.CreateBulletin(this._id, this._uid, null, this.GalleryId, this.BulletinType, this.SiteId, content, this.Date);
        }

        #endregion

        public string GetImage(int id)
        {
            var image = Images.Single(i_ => i_.Id == id);
            var galleryUid = image.Image.Substring(0, image.Image.IndexOf("_"));
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/Galleries/" + galleryUid + "/" + image.Image);
        }

        public string GetImageThumbnail(int id)
        {
            var image = Images.Single(i_ => i_.Id == id);
            var galleryUid = image.Image.Substring(0, image.Image.IndexOf("_"));
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/Galleries/" + galleryUid + "/" + image.Thumbnail);
        }

        public override void LoadCategoryInfo(ILookupRepository lookupRepository)
        {
            var gRep = DependencyResolver.Current.GetService<IGalleryRepository>();

            var gallery = gRep.FetchGallery(this.GalleryId);
            this.CategoryInfo = new Objects.CategoryInfo(lookupRepository.FetchCategoryInfo(gallery.CategoryId));
        }

        public override object GetJSONModel()
        {
            if (CategoryInfo == null)
            {
                LoadCategoryInfo(DependencyResolver.Current.GetService<ILookupRepository>());
            }

            return new
            {
                Uid = this.Uid,
                Id = this.Id,
                Title = this.Title,
                Images = this.Images.Select(i => new {
                    ImagePath = GetImage(i.Id),
                    Thumbnail = GetImageThumbnail(i.Id)
                }),
                CommentCount = this.CommentCount,
                CategoryInfo = this.CategoryInfo,
                BulletinType = this.BulletinType.Name,
                Date = this.Date,
                GalleryId = this.GalleryId,
                Link = this.Link
            };
        }
    }
}

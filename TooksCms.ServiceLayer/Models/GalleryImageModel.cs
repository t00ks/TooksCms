using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Interfaces;
using System.Web.Mvc;
using System.Web;

namespace TooksCms.ServiceLayer.Models
{
    public class GalleryImageModel : ModelBase
    {
        public GalleryImageModel() { }

        public GalleryImageModel(IGalleryImage data)
        {
            this.Id = data.GalleryImageId;
            this.Uid = data.GalleryImageUid;
            this.Image = data.Image;
            this.Thumbnail = data.Thumbnail;
        }

        public string Image { get; set; }

        public string Thumbnail { get; set; }

        public GalleryImage BuildInterface(int galleryId)
        {
            return GalleryImage.Create(this.Id, this.Uid, galleryId, this.Image, this.Thumbnail);
        }

        public object GetJSONModel()
        {
            var galleryUid = this.Image.Substring(0, this.Image.IndexOf("_"));
            var image = VirtualPathUtility.ToAbsolute("~/Uploads/Images/Galleries/" + galleryUid + "/" + this.Image);
            var thumbnail = VirtualPathUtility.ToAbsolute("~/Uploads/Images/Galleries/" + galleryUid + "/" + this.Thumbnail);

            return new
            {
                id = Id,
                uid = Uid,
                image = image,
                thumbnail = thumbnail,
                holding = VirtualPathUtility.ToAbsolute("~/Content/Images/grey.png")
            };
        }
    }
}

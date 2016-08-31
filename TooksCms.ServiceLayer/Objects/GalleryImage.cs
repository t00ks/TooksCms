using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Bases;

namespace TooksCms.ServiceLayer.Objects
{
    public class GalleryImage : InterfacingBase, IGalleryImage
    {
        public GalleryImage() { }

        public GalleryImage(IGalleryImage data) :
            base(data, typeof(IGalleryImage)) { }

        public int GalleryImageId { get; set; }

        public Guid GalleryImageUid { get; set; }

        public int GalleryId { get; set; }

        public string Image { get; set; }

        public string Thumbnail { get; set; }

        public static GalleryImage Create(int id, Guid uid, int galleryId, string image, string thumbnail)
        {
            return new GalleryImage
            {
                GalleryImageId = id,
                GalleryImageUid = uid,
                GalleryId = galleryId,
                Image = image,
                Thumbnail = thumbnail
            };
        }
    }
}

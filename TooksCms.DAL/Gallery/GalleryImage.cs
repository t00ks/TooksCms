using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class GalleryImage : IGalleryImage
    {
        public static GalleryImage CreateGalleryImage(IGalleryImage data)
        {
            return new GalleryImage
            {
                GalleryImageId = data.GalleryImageId,
                GalleryImageUid = data.GalleryImageUid,
                GalleryId = data.GalleryId,
                Image = data.Image,
                Thumbnail = data.Thumbnail
            };
        }

        public void Update (IGalleryImage data)
        {
            GalleryId = data.GalleryId;
            Image = data.Image;
            Thumbnail = data.Thumbnail;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Gallery : IGallery
    {
        public static Gallery CreateGallery(IGallery data)
        {
            return new Gallery
            {
                GalleryId = data.GalleryId,
                GalleryUid = data.GalleryUid,
                Title = data.Title,
                CreatedDate = data.CreatedDate,
                CreatedByUserId = data.CreatedByUserId,
                CategoryId = data.CategoryId
            };
        }

        public void Update (IGallery data)
        {
            this.Title = data.Title;
            this.CreatedDate = data.CreatedDate;
            this.CreatedByUserId = data.CreatedByUserId;
            this.CategoryId = data.CategoryId;
        }
    }
}

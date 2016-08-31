using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class Gallery : InterfacingBase, IGallery
    {
        public Gallery() { }

        public Gallery(IGallery data) :
            base(data, typeof(IGallery)) { }

        public int GalleryId { get; set; }

        public Guid GalleryUid { get; set; }

        public string Title { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }

        public static Gallery Create(int id, Guid uid, string title, int userId, DateTime date, int categoryId)
        {
            return new Gallery
            {
                GalleryId = id,
                GalleryUid = uid,
                Title = title,
                CreatedByUserId = userId,
                CreatedDate = date,
                CategoryId = categoryId
            };
        }
    }
}

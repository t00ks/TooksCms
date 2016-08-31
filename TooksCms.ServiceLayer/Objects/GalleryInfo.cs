using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Bases;

namespace TooksCms.ServiceLayer.Objects
{
    public class GalleryInfo : InterfacingBase, IGalleryInfo
    {
        public GalleryInfo(IGalleryInfo data)
            : base(data, typeof(IGalleryInfo)) { }

        #region Implementation of IGalleryInfo

        public int GalleryId { get; set; }

        public Guid GalleryUid { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ImageThumbnail { get; set; }

        #endregion
    }
}

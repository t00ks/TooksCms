using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IGalleryInfo : IInterfacingBase
    {
        int GalleryId { get; set; }
        Guid GalleryUid { get; set; }
        string Title { get; set; }
        int CategoryId { get; set; }
        string CategoryName { get; set; }
        DateTime CreatedDate { get; set; }
        string ImageThumbnail { get; set; }
    }
}

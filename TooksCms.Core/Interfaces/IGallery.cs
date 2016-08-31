using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IGallery : IInterfacingBase
    {
        int GalleryId { get; set; }
        Guid GalleryUid { get; set; }
        string Title { get; set; }
        int CreatedByUserId { get; set; }
        DateTime CreatedDate { get; set; }
        int CategoryId { get; set; }
    }
}

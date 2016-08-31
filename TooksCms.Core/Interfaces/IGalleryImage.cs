using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IGalleryImage : IInterfacingBase
    {
        int GalleryImageId { get; set; }
        Guid GalleryImageUid { get; set; }
        int GalleryId { get; set; }
        string Image { get; set; }
        string Thumbnail { get; set; }
    }
}

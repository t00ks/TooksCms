using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;

namespace TooksCms.Core.Objects.Responses
{
    public class GalleryResponse : ResponseBase
    {
        public GalleryResponse()
        {
            this.area = Enums.AreaType.GalleryEdit;
        }
    }
}

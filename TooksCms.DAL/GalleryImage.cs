//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TooksCms.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class GalleryImage
    {
        public int GalleryImageId { get; set; }
        public System.Guid GalleryImageUid { get; set; }
        public int GalleryId { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
    
        public virtual Gallery Gallery { get; set; }
    }
}

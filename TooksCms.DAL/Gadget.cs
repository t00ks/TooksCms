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
    
    public partial class Gadget
    {
        public Gadget()
        {
            this.Gadget2Role2AreaType = new HashSet<Gadget2Role2AreaType>();
        }
    
        public int GadgetId { get; set; }
        public System.Guid GadgetUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string View { get; set; }
        public int DefaultColumn { get; set; }
        public int SiteId { get; set; }
    
        public virtual Site Site { get; set; }
        public virtual ICollection<Gadget2Role2AreaType> Gadget2Role2AreaType { get; set; }
    }
}

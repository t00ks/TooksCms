using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Gadgets
{
    public class Gadget : InterfacingBase, IGadget
    {
        public Gadget() { }

        public Gadget(IGadget data) : 
            base(data, typeof(IGadget))
        {
        
        }

        #region IGadget Members

        public int GadgetId { get; set; }

        public Guid GadgetUid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string View { get; set; }

        public int SiteId { get; set; }

        public int DefaultColumn { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class GadgetInfo : InterfacingBase , IGadgetInfo
    {
        public GadgetInfo() { }

        public GadgetInfo(IGadgetInfo data) :
            base(data, typeof(IGadgetInfo)) { }

        #region Implementation of IGadgetInfo

        public int GadgetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string View { get; set; }
        public int DefaultColumn { get; set; }
        public string RoleName { get; set; }
        public string AreaType { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Enums;

namespace TooksCms.Core.Bases
{
    public class ResponseBase
    {
        public string html { get; set; }
        public virtual object model { get; set; }
        public AreaType area { get; set; }
        public string pageTitle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Objects.Responses
{
    public class GadgetResponse : ResponseBase
    {
        public string col1 { get; set; }
        public string col2 { get; set; }
        public List<string> Gadgets { get; set; }
    }
}

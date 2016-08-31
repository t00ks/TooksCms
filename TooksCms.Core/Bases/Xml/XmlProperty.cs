using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TooksCms.Core.Bases.Xml
{
    public class XmlProperty
    {
        protected XmlProperty() { }

        [XmlAttribute("data-type")]
        public string Type { get; set; }
        [XmlAttribute("cssclass")]
        public string CssClass { get; set; }

        protected object PropertyValue;

        [XmlIgnore]
        public bool Deleted { get; set; }
    }
}

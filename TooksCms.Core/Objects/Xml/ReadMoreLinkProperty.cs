using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TooksCms.Core.Bases.Xml;

namespace TooksCms.Core.Objects.Xml
{
    public class ReadMoreLinkProperty : XmlProperty
    {
        public ReadMoreLinkProperty()
        {
            CssClass = "tkf-link-readmore";
            Type = "ReadMoreLink";
        }

        [XmlText]
        public string Value
        {
            get { return PropertyValue == null ? String.Empty : PropertyValue.ToString(); }
            set { PropertyValue = value; }
        }

        [XmlAttribute("link")]
        public string Link { get; set; }
    }
}

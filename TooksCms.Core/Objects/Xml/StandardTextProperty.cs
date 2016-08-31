using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TooksCms.Core.Bases.Xml;

namespace TooksCms.Core.Objects.Xml
{
    public class StandardTextProperty : XmlProperty
    {
        public StandardTextProperty()
        {
            CssClass = "tkf-div-standard";
            Type = "StandardDiv";
        }

        [XmlText]
        public string Value
        {
            get { return PropertyValue == null ? String.Empty : PropertyValue.ToString(); }
            set { PropertyValue = value; }
        }
    }
}

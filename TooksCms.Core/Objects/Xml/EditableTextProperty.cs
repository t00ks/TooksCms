using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases.Xml;
using System.Xml.Serialization;

namespace TooksCms.Core.Objects.Xml
{
    public class EditableDivProperty : XmlProperty
    {
        public EditableDivProperty()
        {
            CssClass = "rich-content";
            Type = "EditableDiv";
        }

        [XmlText]
        public string Value
        {
            get { return PropertyValue == null ? String.Empty : PropertyValue.ToString(); }
            set { PropertyValue = value; }
        }
    }
}

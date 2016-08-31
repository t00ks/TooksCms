using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TooksCms.Core.Bases.Xml;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Objects.Xml
{
    public class RatingProperty : XmlProperty
    {
        public RatingProperty() { }

        public RatingProperty(string type)
        {
            Type = type;
        }

        [XmlText]
        public string Text
        {
            get { return PropertyValue == null ? String.Empty : PropertyValue.ToString(); }
            set { PropertyValue = value; }
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("rating")]
        public decimal Rating { get; set; }

        public static IEnumerable<RatingProperty> BuildXmlProperty(IEnumerable<IRating> ratings)
        {
            return ratings.Select(rating => new RatingProperty
            {
                Name = rating.Name,
                Type = "Rating"
            });
        }
    }
}

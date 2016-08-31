using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TooksCms.Core.Bases.Xml;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Objects.Xml
{
    public class ImageProperty : XmlProperty
    {
        public ImageProperty()
        {
            CssClass = "image-div";
            Type = "tkfImageDiv";
        }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("position")]
        public string Position { get; set; }

        [XmlAttribute("thumbnail")]
        public string Thumbnail { get; set;}
         
        [XmlAttribute("size")]
        public string Size { get; set; }

        [XmlText]
        public string Value
        {
            get { return PropertyValue == null ? String.Empty : PropertyValue.ToString(); }
            set { PropertyValue = value; }
        }

        public static IEnumerable<ImageProperty> BuildXmlProperty(IEnumerable<IArticleImage> images)
        {
            return images.Select(image => new ImageProperty
                                              {
                                                  Id = image.ArticleImageId,
                                                  Position = image.Position,
                                                  Thumbnail = image.Thumbnail,
                                                  Value = image.Image,
                                                  Size = image.Size
                                              });
        }
    }
}

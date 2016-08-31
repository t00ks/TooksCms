using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.Xml;
using System.IO;

namespace TooksCms.Core.Xml
{
    public class XmlTransformer
    {
        private XslCompiledTransform _transformer = null;
        private XmlDocument _xmlDocument = null;

        private XmlTransformer() { }

        public XmlTransformer(string xml, FileInfo xslFile)
        {
            _transformer = new XslCompiledTransform();

            FileStream stream = File.Open(xslFile.FullName, FileMode.Open, FileAccess.Read);
            XmlReader reader = XmlReader.Create(stream);
            _transformer.Load(reader);
            reader.Close();
            stream.Close();

            _xmlDocument = new XmlDocument();
            _xmlDocument.CreateXmlDeclaration("1.0", "utf-16", "yes");
            _xmlDocument.LoadXml(xml);
        }

        public XmlTransformer(string xml, string xsl)
        {
            _transformer = new XslCompiledTransform();
            XmlReader reader = XmlReader.Create(new StringReader(xsl));
            _transformer.Load(reader);
            reader.Close();
            _xmlDocument = new XmlDocument();
            _xmlDocument.LoadXml(xml);
        }

        public string Transform(XsltArgumentList xslarg)
        {
            MemoryStream outputStream = new MemoryStream();
            _transformer.Transform(_xmlDocument.CreateNavigator(), xslarg, outputStream);
            outputStream.Position = 0;
            StreamReader outputReader = new StreamReader(outputStream);
            return outputReader.ReadToEnd();
        }

        public override string ToString()
        {
            MemoryStream outputStream = new MemoryStream();
            _transformer.Transform(_xmlDocument.CreateNavigator(), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            StreamReader outputReader = new StreamReader(outputStream);
            return outputReader.ReadToEnd();
        }
    }
}

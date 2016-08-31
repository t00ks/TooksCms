using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using TooksCms.Core.Xml;

namespace TooksCms.Web.Helpers
{
    public static class MenuBuilder
    {
        public static string Build(string xmlpath, string xslpath, IPrincipal user, bool isFlyout, string userInfo)
        {
            var xslargs = new XsltArgumentList();
            xslargs.AddParam("isFlyout", "", isFlyout);
            xslargs.AddParam("userInfo", "", userInfo);

            XElement xml = XElement.Load(xmlpath);
            xml.Descendants().ToList().ForEach((xe) =>
            {
                if (xe.Attributes().Any(xe_ => xe_.Name == "roles"))
                {
                    var roles = xe.Attributes().First(a_ => a_.Name == "roles").Value;
                    if (roles != "*" && (user == null || !user.IsInRole(roles)))
                    {
                        xe.Remove();
                    }
                }
            });

            var xsl = new XmlDocument();
            xsl.Load(xslpath);

            var xslt = new XmlTransformer(xml.ToString(SaveOptions.DisableFormatting), xsl.OuterXml);
            return xslt.Transform(xslargs);
        }
    }
}
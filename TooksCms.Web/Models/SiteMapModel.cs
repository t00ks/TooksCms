using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TooksCms.Web.Models
{
    public class SiteMapModel
    {
        public string HomeUrl { get; set; }
        public Dictionary<string, string> News { get; set; }
        public Dictionary<string, string> Reviews { get; set; }
        public Dictionary<string, string> Galleries { get; set; }

        public string MakeAbsolute(string relativePage)
        {
            return HomeUrl + "/" + relativePage;
        }

        public XElement BuildUrlElement(string url, string changefreq = "monthly")
        {
            XElement xurl = new XElement("url");

            xurl.Add(new XElement("loc", MakeAbsolute(url)));
            xurl.Add(new XElement("changefreq", changefreq));

            return xurl;
        }
    }
}
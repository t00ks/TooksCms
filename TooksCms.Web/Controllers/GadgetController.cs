using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Objects.Responses;
using TooksCms.ServiceLayer.Authentication;
using TooksCms.ServiceLayer.Gadgets;
using TooksCms.ServiceLayer.Models;
using TooksCms.ServiceLayer.Utilities;

namespace TooksCms.Web.Controllers
{
    public struct CaptchaResponse
    {
        public bool Passed { get; set; }
        public string Message { get; set; }
    }

    public class GadgetController : BaseController
    {
        private IAccountRepository _accountRepository;
        private ISecurityRepository _securityRepository;
        private IConfigRepository _configRepository;

        public GadgetController(IAccountRepository accountRepository, ISecurityRepository securityRepository, IConfigRepository configRepository)
        {
            _accountRepository = accountRepository;
            _securityRepository = securityRepository;
            _configRepository = configRepository;
        }

        public JsonResult CheckCaptcha(string challange, string response)
        {
            if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return Json(new CaptchaResponse { Passed = true });
            }

            var currentGuest = new GuestPrincipal(Request.ServerVariables["REMOTE_ADDR"]);

            if (!currentGuest.Guest.IsNew)
            {
                return Json(new CaptchaResponse { Passed = true });
            }

            var wrequest = (HttpWebRequest)WebRequest.Create("http://www.google.com/recaptcha/api/verify");
            wrequest.Method = "POST";

            var encoding = new ASCIIEncoding();

            var postData = "privatekey=6LcMWsUSAAAAAFjqSKfKVHtaBLBQD_uDCxBXNJGN";
            postData += "&remoteip=" + Request.ServerVariables["REMOTE_ADDR"];
            postData += "&challenge=" + challange;
            postData += "&response=" + response;

            var data = encoding.GetBytes(postData);

            wrequest.ContentType = "application/x-www-form-urlencoded";
            wrequest.ContentLength = data.Length;

            var stream = wrequest.GetRequestStream();
            // Send the data.
            stream.Write(data, 0, data.Length);
            stream.Close();

            var wresponse = (HttpWebResponse)wrequest.GetResponse();
            var rStream = wresponse.GetResponseStream();
            if (rStream == null) { return Json(false); }

            var s = string.Empty;
            using (var reader = new StreamReader(rStream, Encoding.Default))
            {
                s = reader.ReadToEnd();
            }

            var r = s.Split('\n');

            var capRsp = new CaptchaResponse
            {
                Passed = Convert.ToBoolean(r[0]),
                Message = r[1]
            };

            return Json(capRsp);
        }


        #region Html Request

        public ActionResult LatestPosts()
        {
            return View("Gadget.LatestPosts");
        }

        public ActionResult LatestGallerys()
        {
            return View("Gadget.LatestGallerys");
        }

        public ActionResult Twitter()
        {
            return View("Gadget.Twitter");
        }

        public ActionResult TagCloud()
        {
            return View("Gadget.TagCloud");
        }

        public ActionResult ImageViewer()
        {
            return View("Gadget.ImageViewer");
        }

        public ActionResult TagsEdit()
        {
            return View("Gadget.TagsEdit");
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using TooksCms.Core.Bases;
using TooksCms.Core.Enums;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Objects.Responses;
using TooksCms.ServiceLayer.Authentication;
using TooksCms.ServiceLayer.Models.Account;
using TooksCms.ServiceLayer.Models.Lookup;
using TooksCms.ServiceLayer.Support;
using TooksCms.ServiceLayer.Utilities;

namespace TooksCms.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountController(ILookupRepository lookupRepository, IAccountRepository accountRepository)
        {
            _lookupRepository = lookupRepository;
            _accountRepository = accountRepository;
        }

        public JsonResult SignIn([FromBody]SignInModel model)
        {
            try
            {
                var principal = new UserPrincipal(model.UserName, _accountRepository);
                if (principal.AuthenticatePassword(model.Password))
                {
                    FormsAuthentication.SetAuthCookie(principal.User.LoginName, false);
                    return Json(true);
                }
                return Json(false);
            }
            catch (DataNotFoundException dnfex)
            {
                var message = string.Format("Login attempt from IP {0} with usernam {1} and password {2} : Exception - {3}",
                                       Request.ServerVariables["REMOTE_ADDR"], model.UserName, model.Password,
                                       Logger.GetExceptionMessage(dnfex, "SignIn"));
                Logger.LogMessage(EventLogType.Warning, "SignIn", message, 0);
                return Json(false);
            }
        }

        public void SignOut(bool isGuest)
        {
            if (isGuest)
            {
                var currentGuest = new GuestPrincipal(Request.ServerVariables["REMOTE_ADDR"]);
                currentGuest.Guest.Archive();
            }
            FormsAuthentication.SignOut();
            Session.Abandon();
        }

        public JsonResult IsAuthenticated()
        {
            return Json(new
            {
                isAuthenticated = (User != null && User.Identity.IsAuthenticated)
            });
        }
    }
}

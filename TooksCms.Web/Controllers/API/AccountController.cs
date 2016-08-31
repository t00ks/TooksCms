using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Authentication;

namespace TooksCms.Web.Controllers.API
{
    public class AccountController : ApiController
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public HttpResponseMessage Get(string type)
        {
            try
            {
                switch (type.ToLower())
                {
                    case "guest":
                        var currentGuest = new GuestPrincipal(HttpContext.Current.Request.UserHostAddress);
                        return Request.CreateResponse(HttpStatusCode.OK, currentGuest.Guest.GetJSONModel());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "Unknown User Type");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

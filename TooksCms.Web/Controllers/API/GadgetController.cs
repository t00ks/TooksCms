using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Authentication;
using TooksCms.ServiceLayer.Gadgets;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Utilities;

namespace TooksCms.Web.Controllers.API
{

    internal class GadgetModel
    {
        internal GadgetModel()
        {
            col1 = new List<string>();
            col2 = new List<string>();
        }

        public List<string> col1 { get; set; }
        public List<string> col2 { get; set; }
    }

    public class GadgetController : ApiController
    {
        private IAccountRepository _accountRepository;
        private ISecurityRepository _securityRepository;
        private IConfigRepository _configRepository;
        private ILookupRepository _lookupRepository;

        public GadgetController(IAccountRepository accountRepository, ISecurityRepository securityRepository, IConfigRepository configRepository, ILookupRepository lookupRepository)
        {
            _accountRepository = accountRepository;
            _securityRepository = securityRepository;
            _configRepository = configRepository;
            _lookupRepository = lookupRepository;
        }

        public HttpResponseMessage Get(int area)
        {
            try
            {
                var areaType = (AreaType)area;
                var response = new GadgetModel();

                var gadgets = new GadgetCollection();

                if (User != null && User.Identity.IsAuthenticated)
                {
                    var currentUser = new UserPrincipal(User.Identity.Name, _accountRepository);

                    //response.col1.Add("gadget.userinfo");

                    gadgets.GetGadgets(currentUser.User.Roles, areaType, _securityRepository, _configRepository).ForEach((g) =>
                    {
                        if (g.DefaultColumn == 1)
                        {
                            response.col1.Add(g.View.ToLower());
                        }
                        else
                        {
                            response.col2.Add(g.View.ToLower());
                        }
                    });
                }
                else
                {
                    var guest = new GuestPrincipal(GetClientIp(Request));

                    //if (!guest.Guest.IsNew)
                    //{
                    //    response.col1.Add("gadget.userinfo");
                    //}
                    gadgets.GetGadgets(guest.Guest.Roles, areaType, _securityRepository, _configRepository).ForEach((g) =>
                    {
                        if (g.DefaultColumn == 1)
                        {
                            response.col1.Add(g.View.ToLower());
                        }
                        else
                        {
                            response.col2.Add(g.View.ToLower());
                        }
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage GetTwitter()
        {
            try
            {
                string jsonString = TwitterApiClient.GetUserTimelineJson(StateManager.TwitterBearerToken, "tooksnet", 20, true);

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, jsonString);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage GetTags()
        {
            try
            {
                var tags = _lookupRepository.FetchRankedTags(20).Select(t => new RankedTag(t).GetJSONModel()).ToList();

                if (tags == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return Request.CreateResponse(HttpStatusCode.OK, tags);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else
            {
                return null;
            }
        }
    }

}

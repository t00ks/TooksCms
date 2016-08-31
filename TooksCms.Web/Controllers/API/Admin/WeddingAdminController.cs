using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Objects.Wedding;

namespace TooksCms.Web.Controllers.API.Admin
{
    public class WeddingAdminController : ApiController
    {
        public IWeddingRepository _repo;
        public WeddingAdminController(IWeddingRepository repo)
        {
            _repo = repo;
        }

        [Authorize]
        public HttpResponseMessage Get(string type, string code = "")
        {
            switch (type.ToLower())
            {
                case "guests":
                    List<WeddingGuest> guests = _repo.FetchGuests().Select(g => new WeddingGuest(g)).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, guests);
                case "groups":
                    List<WeddingGuestGroup> groups = _repo.FetchGroups().Select(g => new WeddingGuestGroup(g)).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, groups);
                case "food":
                    List<FoodChoice> food2 = _repo.FetchFoodChoices().Select(f => new FoodChoice(f)).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, food2.ToDictionary(k => k.GuestId));
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}

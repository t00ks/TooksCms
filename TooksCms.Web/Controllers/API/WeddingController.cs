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

namespace TooksCms.Web.Controllers.API
{
    public class WeddingController : ApiController
    {
        public IWeddingRepository _repo;
        public WeddingController(IWeddingRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(string type, string code = "")
        {
            switch (type.ToLower())
            {
                case "hotels":
                    List<Hotel> hotels = _repo.FetchHotels().Select(h => new Hotel(h)).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, hotels);
                //case "guests":
                //    List<WeddingGuest> guests = _repo.FetchGuests().Select(g => new WeddingGuest(g)).ToList();
                //    return Request.CreateResponse(HttpStatusCode.OK, guests);
                //case "groups":
                //    List<WeddingGuestGroup> groups = _repo.FetchGroups().Select(g => new WeddingGuestGroup(g)).ToList();
                //    return Request.CreateResponse(HttpStatusCode.OK, groups);
                //case "food":
                //    List<FoodChoice> food2 = _repo.FetchFoodChoices().Select(f => new FoodChoice(f)).ToList();
                //    return Request.CreateResponse(HttpStatusCode.OK, food2.ToDictionary(k => k.GuestId));
                case "login":
                    IWeddingGuestGroup igroup;
                    List<WeddingGuest> guests2 = _repo.FetchGuests(code, out igroup).Select(g => new WeddingGuest(g)).ToList();
                    List<FoodChoice> food = _repo.FetchFoodChoicesForGuests(guests2).Select(f => new FoodChoice(f)).ToList();

                    WeddingGuestGroup group = null;
                    if (igroup != null)
                    {
                        group = new WeddingGuestGroup(igroup);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        granted = guests2.Count() > 0,
                        guests = guests2,
                        group = group,
                        food = food
                    });
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Authorize]
        public HttpResponseMessage PostGuest(List<WeddingGuest> guests)
        {
            List<WeddingGuest> result = new List<WeddingGuest>();

            guests.ForEach((guest) =>
            {
                if (guest.GuestId < 0)
                {
                    result.Add(new WeddingGuest(_repo.AddGuest(guest)));
                }
                else
                {
                    result.Add(new WeddingGuest(_repo.SaveGuest(guest)));
                }
            });

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage PutRSVP(Rsvp rsvp)
        {
            rsvp.IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            rsvp.Date = DateTime.UtcNow;

            _repo.Rsvp(rsvp);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        //public HttpResponseMessage PostFood(FoodChoice foodChoice)
        //{
        //    List<FoodChoice> result = new List<FoodChoice>();
        //    if (foodChoice.FoodChoiceId < 0)
        //    {
        //        result.Add(new FoodChoice(_repo.AddFoodChoice(foodChoice)));
        //    }

        //    if (result != null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.NoContent);
        //}
    }
}

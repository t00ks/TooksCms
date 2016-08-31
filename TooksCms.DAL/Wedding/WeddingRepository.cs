using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class WeddingRepository : IWeddingRepository
    {
        public IEnumerable<IHotel> FetchHotels()
        {
            var db = new TooksCmsDAL();
            return db.Hotels;
        }

        public IEnumerable<IWeddingGuest> FetchGuests()
        {
            var db = new TooksCmsDAL();
            return db.Guest1;
        }

        public IEnumerable<IWeddingGuestGroup> FetchGroups()
        {
            var db = new TooksCmsDAL();
            return db.GuestGroups;
        }

        public IEnumerable<IWeddingGuest> FetchGuests(string code, out IWeddingGuestGroup group)
        {
            var db = new TooksCmsDAL();

            var guests = db.Guest1.Where(g => g.Code == code);

            if (guests.Count() == 0)
            {
                group = null;
                return new List<IWeddingGuest>();
            }

            if (guests.Select(g => g.GuestGroupId).Distinct().Count() > 1)
            {
                throw new IndexOutOfRangeException("Guests in select are assosiated to more than one group");
            }

            group = guests.FirstOrDefault().GuestGroup;

            var groupGuests = db.Guest1.Where(g => g.GuestGroupId == guests.FirstOrDefault().GuestGroup.GuestGroupId);

            return guests.Concat(groupGuests).Distinct();

        }

        public IEnumerable<IFoodChoice> FetchFoodChoices()
        {
            var db = new TooksCmsDAL();

            return db.FoodChoices;
        }

        public IEnumerable<IFoodChoice> FetchFoodChoicesForGuests(IEnumerable<IWeddingGuest> guests)
        {
            var db = new TooksCmsDAL();
            var guestIds = guests.Select(g => g.GuestId).ToList();

            return db.FoodChoices.Where(_fc => guestIds.Contains(_fc.GuestId));
        }

        public IFoodChoice SaveFoodChoice(IFoodChoice choice)
        {
            var db = new TooksCmsDAL();

            var fc = db.FoodChoices.Single(_fc => _fc.FoodChoiceId == choice.FoodChoiceId);
            fc.Update(choice);

            db.SaveChanges();

            return fc;
        }

        public IFoodChoice AddFoodChoice(IFoodChoice choice)
        {
            var db = new TooksCmsDAL();

            var f = db.FoodChoices.Add(FoodChoice.CreateFoodChoice(choice));
            db.SaveChanges();

            return f;
        }

        public IWeddingGuest SaveGuest(IWeddingGuest guest)
        {
            var db = new TooksCmsDAL();

            var g = db.Guest1.Single(_g => _g.GuestId == guest.GuestId);
            g.Update(guest);

            db.SaveChanges();

            return g;
        }

        public IWeddingGuest AddGuest(IWeddingGuest guest)
        {
            var db = new TooksCmsDAL();

            var g = db.Guest1.Add(Guest1.CreatGuest(guest));
            db.SaveChanges();

            return g;
        }


        public void Rsvp(IRsvp rsvp)
        {
            var db = new TooksCmsDAL();

            var f = db.FoodChoices.Add(FoodChoice.CreateFoodChoice(rsvp.FoodChoice));

            var g = db.Guest1.Single(_g => _g.GuestId == rsvp.GuestId);
            g.RSVP(rsvp);

            db.SaveChanges();

        }
    }
}

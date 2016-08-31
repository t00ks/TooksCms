using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IWeddingRepository
    {
        IEnumerable<IHotel> FetchHotels();
        IEnumerable<IWeddingGuest> FetchGuests();
        IEnumerable<IWeddingGuest> FetchGuests(string code, out IWeddingGuestGroup group);
        IEnumerable<IWeddingGuestGroup> FetchGroups();

        IWeddingGuest SaveGuest(IWeddingGuest guest);
        IWeddingGuest AddGuest(IWeddingGuest guest);

        IEnumerable<IFoodChoice> FetchFoodChoices();
        IEnumerable<IFoodChoice> FetchFoodChoicesForGuests(IEnumerable<IWeddingGuest> guestIds);
        IFoodChoice SaveFoodChoice(IFoodChoice choice);
        IFoodChoice AddFoodChoice(IFoodChoice choice);

        void Rsvp(IRsvp rsvp);
    }
}

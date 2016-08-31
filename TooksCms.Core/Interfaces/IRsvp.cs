using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IRsvp
    {
        int GuestId { get; set; }
        IFoodChoice FoodChoice { get; set; }
        bool Attending { get; set; }
        string DietaryRequirements { get; set; }
        DateTime Date { get; set; }
        string IpAddress { get; set; }
    }
}

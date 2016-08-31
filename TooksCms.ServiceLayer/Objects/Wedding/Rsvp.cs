using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Wedding
{
    public class Rsvp : IRsvp
    {
        public int GuestId { get; set; }
        public FoodChoice FoodChoice { get; set; }
        public bool Attending { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; }
        public string DietaryRequirements { get; set; }

        IFoodChoice IRsvp.FoodChoice
        {
            get
            {
                return this.FoodChoice;
            }
            set
            {
                this.FoodChoice = new FoodChoice(value);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Wedding
{
    public class FoodChoice : InterfacingBase, IFoodChoice
    {
        public FoodChoice() { }

        public FoodChoice(IFoodChoice data)
            : base(data, typeof(IFoodChoice))
        {

        }

        public int FoodChoiceId { get; set; }

        public int GuestId { get; set; }

        public FoodStarter Starter { get; set; }

        public FoodMain Main { get; set; }

        public FoodDessert Dessert { get; set; }

        public bool? IsVeggie { get; set; }
    }
}

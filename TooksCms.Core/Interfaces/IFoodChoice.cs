using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooksCms.Core.Interfaces
{
    public enum FoodStarter
    {
        ham,
        hal
    }

    public enum FoodMain
    {
        beef,
        bream,
        gnocchi
    }

    public enum FoodDessert
    {
        eton,
        tart,
        brulee
    }

    public interface IFoodChoice : IInterfacingBase
    {
        int FoodChoiceId { get; set; }
        int GuestId { get; set; }
        FoodStarter Starter { get; set; }
        FoodMain Main { get; set; }
        FoodDessert Dessert { get; set; }
        bool? IsVeggie { get; set; }
    }
}

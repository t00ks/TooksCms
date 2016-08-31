using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class FoodChoice : IFoodChoice
    {
        FoodStarter IFoodChoice.Starter
        {
            get
            {
                return (FoodStarter)Enum.Parse(typeof(FoodStarter), this.Starter);
            }
            set
            {
                this.Starter = value.ToString();
            }
        }

        FoodMain IFoodChoice.Main
        {
            get
            {
                return (FoodMain)Enum.Parse(typeof(FoodMain), this.Main);
            }
            set
            {
                this.Main = value.ToString();
            }
        }

        FoodDessert IFoodChoice.Dessert
        {
            get
            {
                return (FoodDessert)Enum.Parse(typeof(FoodDessert), this.Dessert);
            }
            set
            {
                this.Dessert = value.ToString();
            }
        }

        public void Update(IFoodChoice data)
        {
            this.Starter = data.Starter.ToString();
            this.Main = data.Main.ToString();
            this.Dessert = data.Dessert.ToString();
            this.IsVeggie = data.IsVeggie;
        }

        public static FoodChoice CreateFoodChoice(IFoodChoice data)
        {
            return new FoodChoice
            {
                GuestId = data.GuestId,
                Starter = data.Starter.ToString(),
                Main = data.Main.ToString(),
                Dessert = data.Dessert.ToString(),
                IsVeggie = data.IsVeggie
            };
        }
    }
}

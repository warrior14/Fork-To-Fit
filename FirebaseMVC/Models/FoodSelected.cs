using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ForkToFit.Models
{
    public class FoodSelected : DisplayedFood
    {
        public int Id { get; set; }

        public int MealPlanId { get; set; }

        public int DayCategoryId { get; set; }

        public int MealTimeId { get; set; }

        public int DisplayedFoodId { get; set; }

    }
}

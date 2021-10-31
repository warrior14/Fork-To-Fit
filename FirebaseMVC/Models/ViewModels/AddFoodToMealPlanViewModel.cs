using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models.ViewModels
{
    public class AddFoodToMealPlanViewModel
    {
        //public DisplayedFood DisplayedFood { get; set; }

        public List<DisplayedFood> DisplayedFoods { get; set; }

        public List<MealPlan> MealPlans { get; set; }

        public FoodSelected FoodSelected { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForkToFit.Models;

namespace ForkToFit.Models.ViewModels
{
    public class AddFoodToMealPlanViewModel
    {
        //public DisplayedFood DisplayedFood { get; set; }

        public List<DisplayedFood> DisplayedFoods { get; set; }

        public List<MealPlan> MealPlans { get; set; }

        public FoodSelected FoodSelected { get; set; }

        public List<DayCategory> DayCategories { get; set; }

        public List<MealTime> MealTimes { get; set; }

        public double FoodCalories { get; set; }

    }
}

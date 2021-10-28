using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models.ViewModels
{
    public class AddFoodToMealPlanViewModel
    {
        public int Id { get; set; }

        public List<DisplayedFood> DisplayedFoods { get; set; }
        public List<MealPlan> MealPlans { get; set; }
    }
}

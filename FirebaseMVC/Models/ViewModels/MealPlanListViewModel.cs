using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models.ViewModels
{
    public class MealPlanListViewModel
    {
        public MealPlan MealPlan { get; set; }
        public List<MealPlan> MealPlans { get; set; }
        //public List<MealPlanType> MealPlanTypes { get; set; }
    }
}

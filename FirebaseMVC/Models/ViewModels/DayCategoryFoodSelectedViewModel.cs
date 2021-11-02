using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models.ViewModels
{
    public class DayCategoryFoodSelectedViewModel
    {
        public List<FoodSelected> MealPlanFoods { get;  set; }

        public List<DayCategory> ListOfDays { get; set; }

        public int MealPlanId { get; set; }
        
        public int DaySelectedId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models.ViewModels
{
    public class AddFoodToMealPlanViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ServingSize { get; set; }

        public int Calories { get; set; }

        public string Description { get; set; }

        public string MacroCategoryId { get; set; }

        public string ImageUrl { get; set; }


        public List<DisplayedFood> DisplayedFoods { get; set; }
        public List<MealPlan> MealPlans { get; set; }

    }
}

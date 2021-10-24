using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFitModels
{
    public class MealPlan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserProfileId { get; set; }

        public int MealPlanTypeId { get; set; }

        public int CalorieTracker { get; set; }

    }
}

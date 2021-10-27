using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ForkToFit.Models
{
    public class MealPlan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserProfileId { get; set; }

        [DisplayName("Meal Plan Type")]
        public int MealPlanTypeId { get; set; }

        public MealPlanType MealPlanType { get; set; }

        [DisplayName("Calories Needed")]
        public int CalorieTracker { get; set; }

    }
}

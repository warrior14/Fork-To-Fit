﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models.ViewModels
{
    public class EditMealPlanFormViewModel
    {
        public MealPlan MealPlan { get; set; }
        public List<MealPlanType> MealPlanTypes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models
{
    public class DisplayedFood : MacroCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Serving Size (oz)")]
        public int ServingSize { get; set; }

        public int Calories { get; set; } 

        public string Description { get; set; }

        [DisplayName("Macronutrient Catergory")]
        public int MacroCategoryId { get; set; }

        public MacroCategory MacroCategory { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        public FoodSelected FoodSelected { get; set; }
        public MealPlan MealPlan { get; set; }


    }
}

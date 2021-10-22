using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Calories { get; set; }

        public string Description { get; set; } 

        public int MacroCategoryId { get; set; }

        public int DayCategoryId { get; set; }

        public int MealTimeId { get; set; }


    }
}

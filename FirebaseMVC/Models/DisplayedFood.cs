﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Models
{
    public class DisplayedFood
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ServingSize { get; set; }

        public int Calories { get; set; } 

        public string Description { get; set; }

        public int MacroCategoryId { get; set; }


    }
}
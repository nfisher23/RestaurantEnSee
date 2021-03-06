﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public class FoodCategory
    {
        public int FoodCategoryId { get; set; }
        public string Title { get; set; }
        public List<MenuItem> FoodItems { get; set; } = new List<MenuItem>();
    }
}

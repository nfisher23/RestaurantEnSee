﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public List<FoodCategory> Categories { get; set; }
    }
}

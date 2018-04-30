using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.Models.ManageFoodCategoryModels
{
    public class ManageFoodCategoryModel
    {
        public FoodCategory Category { get; set; }
        public List<MenuItem> MenuItemsNotInCategory { get; set; }
    }
}

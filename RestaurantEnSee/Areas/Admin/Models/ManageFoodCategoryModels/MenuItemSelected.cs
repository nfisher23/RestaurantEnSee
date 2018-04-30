using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.Models.ManageFoodCategoryModels
{
    public class MenuItemSelected : MenuItem
    {
        public bool IsSelected { get; set; }

        public MenuItemSelected(MenuItem other) : base(other)
        { }

        public MenuItemSelected(MenuItemSelected other) : base(other)
        { this.IsSelected = other.IsSelected; }

    }
}

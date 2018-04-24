using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Photo Picture { get; set; }
    }
}

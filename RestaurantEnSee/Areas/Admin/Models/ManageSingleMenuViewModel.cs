using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.Models
{
    public class ManageSingleMenuViewModel
    {
        public Menu Menu { get; set; }

        public string CategoryNameToAdd { get; set; }
    }
}

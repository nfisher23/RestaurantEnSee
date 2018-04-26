using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Home.Models.ViewComponentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Components
{
    public class NavHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // create service for session to insert vals here
            NavHeaderViewComponentModel model = new NavHeaderViewComponentModel();
            return View(model);
        }
    }
}

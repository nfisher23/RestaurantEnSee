using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Home.Models.ViewComponentModels;
using RestaurantEnSee.Areas.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Components
{
    public class NavHeaderViewComponent : ViewComponent
    {
        ShoppingCart cart;
        public NavHeaderViewComponent(ShoppingCart cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            // create service for session to insert vals here
            NavHeaderViewComponentModel model = new NavHeaderViewComponentModel
            {
                Cart = cart
            };
            return View(model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Controllers
{
    [Area("Order")]
    public class OrderController : Controller
    {
        public ViewResult CartSummary()
        {
            return View();
        }
    }
}

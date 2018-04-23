using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        public ViewResult Menu()
        {
            return View();
        }

    }
}

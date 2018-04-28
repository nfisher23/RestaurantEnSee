using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public ViewResult Dashboard()
        {
            return View();
        }
    }
}

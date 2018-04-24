using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        private IMenuRepository menuRepository;
        public HomeController(IMenuRepository repo)
        {
            menuRepository = repo;
        }

        public ViewResult Menu()
        {
            var m = menuRepository.GetFullMenu(1);
            return View();
        }

    }
}

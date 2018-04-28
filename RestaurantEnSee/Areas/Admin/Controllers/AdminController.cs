using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Admin.Models;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        IMenuRepository menuRepository;
        public AdminController(IMenuRepository repo)
        {
            menuRepository = repo;
        }

        public ViewResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ManageAllMenus()
        {
            var menus = menuRepository.Menus.ToList();
            var model = new ManageAllMenusViewModel
            {
                AllMenus = menus
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ManageAllMenus(ManageAllMenusViewModel model)
        {
            throw new NotImplementedException();
        }
        
        public IActionResult ManageSingleMenu(object fillWithModelSoon)
        {
            throw new NotImplementedException();
        }

    }
}

using Microsoft.AspNetCore.Http;
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
            IFormCollection form = this.Request.Form;
            FillInSelectedMenu(model, form);
            if (ModelState.IsValid)
            {
                var newActive = model.AllMenus.Where(m => m.IsActiveMenu).FirstOrDefault();
                menuRepository.SetActiveMenu(newActive);
                TempData["message"] = "Your Menu Configuration was successfully updated";
                return RedirectToAction(nameof(ManageAllMenus));
            }
            else
                return View(model);
        }
        
        public IActionResult ManageSingleMenu(object fillWithModelSoon)
        {
            throw new NotImplementedException();
        }

        private void FillInSelectedMenu(ManageAllMenusViewModel model, 
            IFormCollection form)
        {
            for (int i = 0; i < model.AllMenus.Count; i++)
            {
                var menu = model.AllMenus[i];
                menu.IsActiveMenu = false;
                if (menu.MenuName == form["IsActiveMenu"])
                {
                    menu.IsActiveMenu = true;
                }
            }
        }
    }
}

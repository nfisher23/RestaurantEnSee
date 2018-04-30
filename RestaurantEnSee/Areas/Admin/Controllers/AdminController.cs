using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Admin.Models;
using RestaurantEnSee.Areas.Admin.Models.ManageFoodCategoryModels;
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

        public IActionResult ManageSingleMenu(string menuName)
        {
            var menu = menuRepository.GetFullMenuByName(menuName);
            var model = new ManageSingleMenuViewModel
            {
                Menu = menu
            };
            return View("ManageSingleMenu", model);
        }

        public IActionResult ManageActiveMenu()
        {
            var active = menuRepository.ActiveMenu;
            return ManageSingleMenu(active.MenuName);
        }

        public IActionResult ManageFoodCategory(int foodCategoryId)
        {
            var cat = menuRepository.GetFullFoodCategoryById(foodCategoryId);
            var items = menuRepository.GetAllMenuItems().Where(i => !cat.FoodItems.Contains(i)).ToList();
            var model = new ManageFoodCategoryModel
            {
                Category = cat,
                MenuItemsNotInCategory = items
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeFoodCategoryName(ManageFoodCategoryModel model)
        {
            menuRepository.ChangeCategoryName(model.Category.FoodCategoryId, model.Category.Title);
            TempData["message"] = "name changed";
            return RedirectToAction(nameof(ManageFoodCategory), new { foodCategoryId = model.Category.FoodCategoryId });
        }

        [HttpPost] 
        public IActionResult AddMenuItemToCategory(int foodCategoryId, int menuItemId)
        {
            menuRepository.AddMenuItemToCategory(foodCategoryId, menuItemId);
            TempData["message"] = "We successfully added your menu item";
            return RedirectToAction(nameof(ManageFoodCategory), new { foodCategoryId });
        }

        [HttpPost]
        public IActionResult RemoveMenuItemFromCategory(int foodCategoryId, int menuItemId)
        {
            menuRepository.RemoveMenuItemFromCategory(foodCategoryId, menuItemId);
            TempData["message"] = "We successfully added removed your menu item";
            return RedirectToAction(nameof(ManageFoodCategory), new { foodCategoryId });
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

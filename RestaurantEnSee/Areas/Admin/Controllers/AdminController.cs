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

        [HttpGet]
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
            TempData["message"] = "We successfully added your menu item to this section.\r\n" +
                "Note that it can only exist in one section at a time";
            return RedirectToAction(nameof(ManageFoodCategory), new { foodCategoryId });
        }

        [HttpPost]
        public IActionResult RemoveMenuItemFromCategory(int foodCategoryId, int menuItemId)
        {
            menuRepository.RemoveMenuItemFromCategory(foodCategoryId, menuItemId);
            TempData["message"] = "We successfully added removed your menu item";
            return RedirectToAction(nameof(ManageFoodCategory), new { foodCategoryId });
        }

        [HttpPost]
        public IActionResult AddFoodCategory(ManageSingleMenuViewModel model)
        {
            if (model.Menu.Categories == null)
            {
                model.Menu = menuRepository.GetFullMenuByName(model.Menu.MenuName);
            }
            var id = menuRepository.CreateNewFoodCategory(model.CategoryNameToAdd, model.Menu);

            return RedirectToAction(nameof(ManageFoodCategory), new { foodCategoryId = id });
        }

        [HttpPost]
        public IActionResult RemoveFoodCategory(int foodCategoryId)
        {
            menuRepository.RemoveFoodCategoryById(foodCategoryId);
            TempData["message"] = "Your category was successfully removed";
            return RedirectToAction(nameof(ManageAllMenus));
        }

        public IActionResult ManageMenuItem(int menuItemId)
        {
            var item = menuRepository.GetMenuItemById(menuItemId);
            var model = new ManageMenuItemModel
            {
                Item = item
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ManageMenuItem(ManageMenuItemModel model)
        {
            if (ModelState.IsValid)
            {
                menuRepository.UpdateMenuItem(model.Item);
                TempData["message"] = "Your changes were saved";
                return RedirectToAction(nameof(ManageMenuItem), new { menuItemId = model.Item.MenuItemId });
            }
            else
            {
                if (model.Item.Picture == null)
                {
                    model.Item = menuRepository.GetMenuItemById(model.Item.MenuItemId);
                }
                return View(model);
            }
        }

        public IActionResult CreateMenuItem()
        {
            var model = new ManageMenuItemModel
            {
                Item = new MenuItem { MenuItemId = 0 }
            };
            return View(nameof(ManageMenuItem), model);
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

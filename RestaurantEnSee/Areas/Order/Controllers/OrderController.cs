using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Home.Models;
using RestaurantEnSee.Areas.Order.Infrastructure;
using RestaurantEnSee.Areas.Order.Models;
using RestaurantEnSee.Areas.Order.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Controllers
{
    [Area("Order")]
    public class OrderController : Controller
    {
        private IMenuRepository menuRepository;
        private ShoppingCart cart;
        public OrderController(IMenuRepository repo, ShoppingCart cartService)
        {
            menuRepository = repo;
            cart = cartService;
        }

        public ViewResult OrderSummary(string returnUrl = "/")
        {
            var model = new OrderSummaryViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        public ViewResult Checkout()
        {
            var model = new CheckoutViewModel
            {
                Cart = cart
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToOrder(int menuItemId, string returnUrl)
        {
            var menuItem = menuRepository.GetMenuItemById(menuItemId);
            if (menuItem != null)
            {
                cart.AddItem(menuItem, 1);
            }

            return RedirectToAction(nameof(OrderSummary), new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromOrder(int menuItemId, string returnUrl)
        {
            var menuItem = menuRepository.GetMenuItemById(menuItemId);
            if (menuItem != null)
            {
                cart.RemoveItem(menuItem);
            }

            return RedirectToAction(nameof(OrderSummary), new { returnUrl });
        }

        [HttpPost]
        public IActionResult SendOrder(CheckoutViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}

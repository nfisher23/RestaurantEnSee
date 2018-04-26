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
        private const string DefaultCartKey = "VisitorCart";
        public OrderController(IMenuRepository repo)
        {
            menuRepository = repo;
        }

        public ViewResult OrderSummary(string returnUrl = "/")
        {
            var model = new OrderSummaryViewModel
            {
                Cart = HttpContext.Session.GetJson<ShoppingCart>(DefaultCartKey) ?? new ShoppingCart(),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToOrder(int menuItemId, string returnUrl)
        {
            var menuItem = menuRepository.GetMenuItemById(menuItemId);
            if (menuItem != null)
            {
                ShoppingCart cart = GetCart();
                cart.AddItem(menuItem, 1);
                SaveCart(cart);
            }

            return RedirectToAction(nameof(OrderSummary), new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromOrder(int menuItemId, string returnUrl)
        {
            var menuItem = menuRepository.GetMenuItemById(menuItemId);
            if (menuItem != null)
            {
                ShoppingCart cart = GetCart();
                cart.RemoveItem(menuItem);
                SaveCart(cart);
            }

            return RedirectToAction(nameof(OrderSummary), new { returnUrl });
        }

        private ShoppingCart GetCart()
        {
            ShoppingCart c = HttpContext.Session.GetJson<ShoppingCart>(DefaultCartKey);
            return c ?? new ShoppingCart();
        }

        private void SaveCart(ShoppingCart cart)
        {
            HttpContext.Session.SetJson(DefaultCartKey, cart);
        }
    }
}

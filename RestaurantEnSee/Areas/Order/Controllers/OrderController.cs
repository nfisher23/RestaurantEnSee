using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Admin.Models;
using RestaurantEnSee.Areas.Admin.Models.Email;
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
        private IOrderCommunicationRepository orderCommunicationRepository;
        private ShoppingCart cart;
        private IEmailService emailService;

        public OrderController(IMenuRepository menuRepo, ShoppingCart cartService,
            IOrderCommunicationRepository orderCRepo, IEmailService email)
        {
            menuRepository = menuRepo;
            cart = cartService;
            orderCommunicationRepository = orderCRepo;
            emailService = email;
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
            var msg = GenerateOrderMessage(model);
            try
            {
                emailService.Send(msg);
            }
            catch (Exception e)
            {
                // write to log, should implement backup plan
                return RedirectToAction("Mistake"); // whoops
            }
            return RedirectToAction("Thanks"); // thank you screen
        }

        public ViewResult Thanks()
        {
            return View();
        }

        public ViewResult Mistake()
        {
            return View();
        }


        private EmailMessage GenerateOrderMessage(CheckoutViewModel model)
        {
            var defaultEmail = orderCommunicationRepository.DefaultEmailConfiguration;
            EmailAddress address = new EmailAddress
            {
                Address = defaultEmail.SmtpUsername,
                Name = "RestaurantEnSee"
            };

            EmailMessage msg = new EmailMessage
            {
                FromAddresses = new List<EmailAddress> { address },
                ToAddresses = new List<EmailAddress> { address },
                Subject = "RestaurantEnSee - Order Received",
                Content = GenerateOrderMessageContent(model)
            };

            return msg;
        }

        private string GenerateOrderMessageContent(CheckoutViewModel model)
        {
            string msg = "Hello, \r\n\r\nYou have received an online order with the" +
                $"following details at {DateTime.Now.ToUniversalTime()} (universal time):\r\n\r\n" +
                $"Name on order: {model?.NameOnOrder}\r\n" +
                $"Special Instructions: {model.SpecialInstructions}\r\n\r\n";

            foreach (var item in cart.CartItems)
            {
                msg += $"Item: {item.MenuItem}, Quantity: {item.Quantity}, Subtotal: {item.SubtotalBeforeTax}";
            }

            msg += $"\r\n\r\nThe total cost of this order is {cart.TotalBeforeCoupons}";

            return msg;
        }
    }
}

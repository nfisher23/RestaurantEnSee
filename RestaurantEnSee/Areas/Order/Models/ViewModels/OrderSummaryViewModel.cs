using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Models.ViewModels
{
    public class OrderSummaryViewModel
    {
        public string ReturnUrl { get; set; }
        public ShoppingCart Cart { get; set; }
        public decimal TotalBeforeCoupons { get
            {
                return Cart.CartItems.Sum(i => i.Quantity * i.MenuItem.PriceBeforeTax);
            }
        }
    }
}

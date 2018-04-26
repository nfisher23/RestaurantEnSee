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
    }
}
